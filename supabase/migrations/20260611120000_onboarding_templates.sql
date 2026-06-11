create table public.onboarding_templates (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  name text not null check (length(btrim(name)) > 0),
  description text,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now()
);

create table public.onboarding_template_items (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  template_id uuid not null references public.onboarding_templates(id) on delete cascade,
  item_type text not null check (item_type in ('hardware_requirement', 'software_license')),
  hardware_label text,
  software_license_id uuid references public.software_licenses(id) on delete cascade,
  notes text,
  position integer not null default 0,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now(),
  check (
    (
      item_type = 'hardware_requirement'
      and hardware_label is not null
      and length(btrim(hardware_label)) > 0
      and software_license_id is null
    )
    or (
      item_type = 'software_license'
      and hardware_label is null
      and software_license_id is not null
    )
  )
);

create index onboarding_templates_company_id_idx on public.onboarding_templates (company_id);
create index onboarding_template_items_company_id_idx on public.onboarding_template_items (company_id);
create index onboarding_template_items_template_position_idx on public.onboarding_template_items (template_id, position);
create index onboarding_template_items_software_license_id_idx
on public.onboarding_template_items (software_license_id)
where software_license_id is not null;

create function public.validate_onboarding_template_item_company()
returns trigger
language plpgsql
security definer
set search_path = public
as $$
begin
  if not exists (
    select 1 from public.onboarding_templates
    where id = new.template_id and company_id = new.company_id
  ) then
    raise exception 'Onboarding template does not belong to item company';
  end if;

  if new.software_license_id is not null and not exists (
    select 1 from public.software_licenses
    where id = new.software_license_id and company_id = new.company_id
  ) then
    raise exception 'Software license does not belong to item company';
  end if;

  return new;
end;
$$;

create trigger onboarding_template_items_validate_company
before insert or update on public.onboarding_template_items
for each row
execute function public.validate_onboarding_template_item_company();

create trigger onboarding_templates_set_updated_at
before update on public.onboarding_templates
for each row
execute function public.set_updated_at();

create trigger onboarding_template_items_set_updated_at
before update on public.onboarding_template_items
for each row
execute function public.set_updated_at();

alter table public.onboarding_templates enable row level security;
alter table public.onboarding_template_items enable row level security;

grant select, insert, update, delete on public.onboarding_templates to authenticated;
grant select, insert, update, delete on public.onboarding_template_items to authenticated;

create policy "owners can read onboarding templates"
on public.onboarding_templates
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_templates.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can insert onboarding templates"
on public.onboarding_templates
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_templates.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can update onboarding templates"
on public.onboarding_templates
for update
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_templates.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
)
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_templates.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can delete onboarding templates"
on public.onboarding_templates
for delete
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_templates.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can read onboarding template items"
on public.onboarding_template_items
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_template_items.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can insert onboarding template items"
on public.onboarding_template_items
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_template_items.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can update onboarding template items"
on public.onboarding_template_items
for update
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_template_items.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
)
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_template_items.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);

create policy "owners can delete onboarding template items"
on public.onboarding_template_items
for delete
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = onboarding_template_items.company_id
      and company_memberships.user_id = auth.uid()
      and company_memberships.role = 'owner'
  )
);
