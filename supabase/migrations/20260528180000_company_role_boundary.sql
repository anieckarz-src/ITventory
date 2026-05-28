create extension if not exists pgcrypto with schema extensions;

create table public.companies (
  id uuid primary key default gen_random_uuid(),
  name text not null check (length(btrim(name)) > 0),
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now()
);

create table public.company_memberships (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  user_id uuid not null references auth.users(id) on delete cascade,
  role text not null check (role in ('admin', 'manager')),
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now(),
  constraint company_memberships_company_id_user_id_key unique (company_id, user_id)
);

create index company_memberships_user_id_idx on public.company_memberships (user_id);
create index company_memberships_company_id_idx on public.company_memberships (company_id);

create or replace function public.set_updated_at()
returns trigger
language plpgsql
set search_path = public
as $$
begin
  new.updated_at = now();
  return new;
end;
$$;

create trigger companies_set_updated_at
before update on public.companies
for each row
execute function public.set_updated_at();

create trigger company_memberships_set_updated_at
before update on public.company_memberships
for each row
execute function public.set_updated_at();

alter table public.companies enable row level security;
alter table public.company_memberships enable row level security;

grant select on public.companies to authenticated;
grant select on public.company_memberships to authenticated;

create policy "members can read their companies"
on public.companies
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = companies.id
      and company_memberships.user_id = auth.uid()
  )
);

create policy "users can read their memberships"
on public.company_memberships
for select
to authenticated
using (user_id = auth.uid());

create or replace function public.create_company_for_current_user(company_name text)
returns table (
  company_id uuid,
  membership_id uuid,
  role text
)
language plpgsql
security definer
set search_path = public
as $$
declare
  current_user_id uuid := auth.uid();
  normalized_name text := nullif(btrim(company_name), '');
begin
  if current_user_id is null then
    raise exception 'create_company_for_current_user requires an authenticated user'
      using errcode = '28000';
  end if;

  if normalized_name is null then
    raise exception 'company_name is required'
      using errcode = '22023';
  end if;

  insert into public.companies (name)
  values (normalized_name)
  returning id into company_id;

  insert into public.company_memberships (company_id, user_id, role)
  values (company_id, current_user_id, 'admin')
  returning id, company_memberships.role into membership_id, role;

  return next;
end;
$$;

revoke all on function public.create_company_for_current_user(text) from public;
grant execute on function public.create_company_for_current_user(text) to authenticated;
