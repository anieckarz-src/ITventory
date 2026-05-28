create table public.license_renewal_reminders (
  id uuid primary key default gen_random_uuid(),
  company_id uuid not null references public.companies(id) on delete cascade,
  license_ref text not null check (length(btrim(license_ref)) > 0),
  recipient_email text not null check (position('@' in recipient_email) > 1),
  reminder_date date not null,
  status text not null default 'pending' check (status in ('pending', 'sent', 'failed')),
  attempt_count integer not null default 0 check (attempt_count >= 0),
  last_error text,
  last_attempted_at timestamptz,
  sent_at timestamptz,
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now(),
  constraint license_renewal_reminders_dedup_key unique (company_id, license_ref, reminder_date, recipient_email),
  constraint license_renewal_reminders_id_company_unique unique (id, company_id)
);

create index license_renewal_reminders_company_id_idx on public.license_renewal_reminders (company_id);
create index license_renewal_reminders_reminder_date_idx on public.license_renewal_reminders (reminder_date);
create index license_renewal_reminders_status_idx on public.license_renewal_reminders (status);

create table public.license_renewal_reminder_attempts (
  id uuid primary key default gen_random_uuid(),
  reminder_id uuid not null,
  company_id uuid not null,
  attempt_no integer not null check (attempt_no > 0),
  status text not null check (status in ('sent', 'failed')),
  error_message text,
  attempted_at timestamptz not null default now(),
  provider_message_id text,
  constraint license_renewal_reminder_attempts_reminder_fk
    foreign key (reminder_id, company_id)
    references public.license_renewal_reminders(id, company_id)
    on delete cascade
);

create index license_renewal_reminder_attempts_reminder_id_idx on public.license_renewal_reminder_attempts (reminder_id);
create index license_renewal_reminder_attempts_company_id_idx on public.license_renewal_reminder_attempts (company_id);

create trigger license_renewal_reminders_set_updated_at
before update on public.license_renewal_reminders
for each row
execute function public.set_updated_at();

alter table public.license_renewal_reminders enable row level security;
alter table public.license_renewal_reminder_attempts enable row level security;

grant select, insert, update on public.license_renewal_reminders to authenticated;
grant select, insert on public.license_renewal_reminder_attempts to authenticated;

create policy "members can read their company reminders"
on public.license_renewal_reminders
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = license_renewal_reminders.company_id
      and company_memberships.user_id = auth.uid()
  )
);

create policy "members can create reminders in their company"
on public.license_renewal_reminders
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = license_renewal_reminders.company_id
      and company_memberships.user_id = auth.uid()
  )
);

create policy "members can update reminders in their company"
on public.license_renewal_reminders
for update
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = license_renewal_reminders.company_id
      and company_memberships.user_id = auth.uid()
  )
)
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = license_renewal_reminders.company_id
      and company_memberships.user_id = auth.uid()
  )
);

create policy "members can read reminder attempts in their company"
on public.license_renewal_reminder_attempts
for select
to authenticated
using (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = license_renewal_reminder_attempts.company_id
      and company_memberships.user_id = auth.uid()
  )
);

create policy "members can insert reminder attempts in their company"
on public.license_renewal_reminder_attempts
for insert
to authenticated
with check (
  exists (
    select 1
    from public.company_memberships
    where company_memberships.company_id = license_renewal_reminder_attempts.company_id
      and company_memberships.user_id = auth.uid()
  )
);
