create table public.asset_assignments (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  assignment_type text not null check (assignment_type in ('hardware_employee', 'license_employee', 'license_hardware')),
  employee_id uuid references public.employees(id) on delete cascade,
  hardware_asset_id uuid references public.hardware_assets(id) on delete cascade,
  software_license_id uuid references public.software_licenses(id) on delete cascade,
  notes text,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now(),
  check (
    (
      assignment_type = 'hardware_employee'
      and employee_id is not null
      and hardware_asset_id is not null
      and software_license_id is null
    )
    or (
      assignment_type = 'license_employee'
      and employee_id is not null
      and hardware_asset_id is null
      and software_license_id is not null
    )
    or (
      assignment_type = 'license_hardware'
      and employee_id is null
      and hardware_asset_id is not null
      and software_license_id is not null
    )
  )
);

create index asset_assignments_company_id_idx on public.asset_assignments (company_id);
create index asset_assignments_employee_id_idx on public.asset_assignments (employee_id);
create index asset_assignments_hardware_asset_id_idx on public.asset_assignments (hardware_asset_id);
create index asset_assignments_software_license_id_idx on public.asset_assignments (software_license_id);

create unique index asset_assignments_one_employee_per_hardware_idx
on public.asset_assignments (company_id, hardware_asset_id)
where assignment_type = 'hardware_employee';

create unique index asset_assignments_unique_license_employee_idx
on public.asset_assignments (company_id, software_license_id, employee_id)
where assignment_type = 'license_employee';

create unique index asset_assignments_unique_license_hardware_idx
on public.asset_assignments (company_id, software_license_id, hardware_asset_id)
where assignment_type = 'license_hardware';

create function public.validate_asset_assignment_company()
returns trigger
language plpgsql
security definer
set search_path = public
as $$
begin
  if new.employee_id is not null and not exists (
    select 1 from public.employees
    where id = new.employee_id and company_id = new.company_id
  ) then
    raise exception 'Employee does not belong to assignment company';
  end if;

  if new.hardware_asset_id is not null and not exists (
    select 1 from public.hardware_assets
    where id = new.hardware_asset_id and company_id = new.company_id
  ) then
    raise exception 'Hardware asset does not belong to assignment company';
  end if;

  if new.software_license_id is not null and not exists (
    select 1 from public.software_licenses
    where id = new.software_license_id and company_id = new.company_id
  ) then
    raise exception 'Software license does not belong to assignment company';
  end if;

  return new;
end;
$$;

create trigger asset_assignments_validate_company
before insert or update on public.asset_assignments
for each row
execute function public.validate_asset_assignment_company();

create trigger asset_assignments_set_updated_at
before update on public.asset_assignments
for each row
execute function public.set_updated_at();

alter table public.asset_assignments enable row level security;

grant select, insert, delete on public.asset_assignments to authenticated;

create policy "owners can read asset assignments"
on public.asset_assignments
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = asset_assignments.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can insert asset assignments"
on public.asset_assignments
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = asset_assignments.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can delete asset assignments"
on public.asset_assignments
for delete
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = asset_assignments.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);
