create table public.employees (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  full_name text not null check (length(btrim(full_name)) > 0),
  email text,
  title text,
  notes text,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now()
);

create table public.hardware_assets (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  asset_type text not null check (length(btrim(asset_type)) > 0),
  model text not null check (length(btrim(model)) > 0),
  serial_number text,
  status text not null default 'available' check (status in ('available', 'in_use', 'retired')),
  notes text,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now()
);

create table public.software_licenses (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  name text not null check (length(btrim(name)) > 0),
  vendor text,
  seats integer check (seats is null or seats > 0),
  cost_amount numeric(12, 2) check (cost_amount is null or cost_amount >= 0),
  cost_period text not null default 'monthly' check (cost_period in ('monthly', 'annual')),
  renewal_date date,
  notes text,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now()
);

create index employees_company_id_idx on public.employees (company_id);
create index hardware_assets_company_id_idx on public.hardware_assets (company_id);
create index software_licenses_company_id_idx on public.software_licenses (company_id);
create index software_licenses_company_renewal_idx on public.software_licenses (company_id, renewal_date);

create trigger employees_set_updated_at
before update on public.employees
for each row
execute function public.set_updated_at();

create trigger hardware_assets_set_updated_at
before update on public.hardware_assets
for each row
execute function public.set_updated_at();

create trigger software_licenses_set_updated_at
before update on public.software_licenses
for each row
execute function public.set_updated_at();

alter table public.employees enable row level security;
alter table public.hardware_assets enable row level security;
alter table public.software_licenses enable row level security;

grant select, insert, update on public.employees to authenticated;
grant select, insert, update, delete on public.hardware_assets to authenticated;
grant select, insert, update on public.software_licenses to authenticated;

create policy "owners can read employees"
on public.employees
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = employees.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can insert employees"
on public.employees
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = employees.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can update employees"
on public.employees
for update
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = employees.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
)
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = employees.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can read hardware assets"
on public.hardware_assets
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = hardware_assets.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can insert hardware assets"
on public.hardware_assets
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = hardware_assets.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can update hardware assets"
on public.hardware_assets
for update
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = hardware_assets.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
)
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = hardware_assets.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can delete hardware assets"
on public.hardware_assets
for delete
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = hardware_assets.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can read software licenses"
on public.software_licenses
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = software_licenses.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can insert software licenses"
on public.software_licenses
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = software_licenses.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can update software licenses"
on public.software_licenses
for update
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = software_licenses.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
)
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = software_licenses.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);
