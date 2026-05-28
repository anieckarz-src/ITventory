alter table public.company_memberships
add constraint company_memberships_user_id_key unique (user_id);

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

  perform pg_advisory_xact_lock(hashtext(current_user_id::text));

  if exists (
    select 1
    from public.company_memberships
    where user_id = current_user_id
  ) then
    raise exception 'user already has a company membership'
      using errcode = '23505';
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
