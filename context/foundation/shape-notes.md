---
project: "ITventory"
version: 2
status: shaped
created: 2026-05-25
updated: 2026-06-06
context_type: greenfield
product_type: web-app
decision_update: owner-only-role-model
---

# ITventory Shape Notes

## Product Direction

ITventory is a small-company IT workspace for one person responsible for operational IT. The MVP helps the company owner keep hardware, software licenses, assignments, renewal dates, and onboarding templates in one company-scoped place.

## Updated Role Decision

The MVP has one flat company role:

- `owner`: the creator of the company workspace and the only supported company user in MVP.

There is no manager role, no invitation flow, no role boundary matrix, and no custom RBAC in MVP. These are parked until there is evidence that multi-user delegation is worth the added product and implementation complexity.

## Primary Persona

Primary persona: the company owner or single person responsible for operational IT in a small company.

This person needs to answer practical questions:

- Which licenses renew soon?
- What software and hardware does the company track?
- Who or what is each asset assigned to?
- What should be prepared for onboarding?

## Core MVP Flow

1. Owner creates a company workspace.
2. Owner adds employees.
3. Owner adds hardware and software licenses.
4. Owner assigns hardware and licenses to employees or devices.
5. Owner reviews subscription cost and renewal risk on the dashboard.
6. System emails the owner about upcoming license renewal.
7. Owner uses a simple onboarding template to prepare a recurring bundle of assets.

## Success Signals

- Owner sees hardware, licenses, assignments, and upcoming renewals in one place.
- Owner receives a useful email reminder before a license renewal date.
- Owner can prepare a simple onboarding package without external provisioning.
- MVP remains small enough for after-hours delivery.

## Access Control

- Authentication uses email and password.
- Each company has isolated data.
- The creator of a company receives role `owner`.
- Only `owner` is valid in MVP.
- No invitations are built in MVP.

## Functional Scope

- Owner can create a company account.
- Owner can sign in.
- System isolates each company's data.
- System enforces the single `owner` role.
- Owner can create and maintain employees, hardware, and software licenses.
- Owner can assign hardware to employees.
- Owner can assign licenses to employees or devices.
- System tracks license renewal dates.
- System sends owner email alerts for upcoming renewals.
- Owner can view renewal and cost dashboard summaries.
- Owner can create and apply simple onboarding templates.

## Parked

- Multi-user companies.
- Manager role.
- User invitations.
- Custom RBAC.
- External integrations.
- Automatic discovery of licenses or hardware.
- Multi-currency support.
- Full hardware lifecycle, repair history, depreciation, and physical inventory labels.
