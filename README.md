# ITventory

![](./public/template.png)

ITventory is a small-company IT inventory workspace for tracking hardware, software licenses, assignments, and renewal risk from one company-scoped dashboard.

## Tech Stack

- [Astro](https://astro.build/) v6 - Modern web framework with server-first rendering
- [React](https://react.dev/) v19 - UI library for interactive components
- [TypeScript](https://www.typescriptlang.org/) v5 - Type-safe JavaScript
- [Tailwind CSS](https://tailwindcss.com/) v4 - Utility-first CSS framework
- [Supabase](https://supabase.com/) - Authentication and backend-as-a-service
- [Cloudflare Workers](https://workers.cloudflare.com/) - Edge deployment runtime

## Prerequisites

- Node.js v22.14.0 (as specified in `.nvmrc`)
- npm (comes with Node.js)

## Getting Started

1. Clone the repository:

```bash
git clone <repository-url>
cd ITventory
```

2. Install dependencies:

```bash
npm install
```

3. Set up Supabase and configure environment variables — see [Supabase Configuration](#supabase-configuration) below.

4. Create a `.dev.vars` file for local Cloudflare dev secrets:

```bash
cp .env.example .dev.vars
```

5. Run the development server:

```bash
npm run dev
```

## Available Scripts

- `npm run dev` - Start development server (Cloudflare workerd runtime)
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint with type-checked rules
- `npm run lint:fix` - Auto-fix ESLint issues
- `npm run format` - Run Prettier

## Project Structure

```md
.
├── src/
│ ├── layouts/ # Astro layouts
│ ├── pages/ # Astro pages
│ │ └── api/ # API endpoints
│ ├── components/ # UI components (Astro & React)
│ └── assets/ # Static assets
├── public/ # Public assets
├── wrangler.jsonc # Cloudflare Workers config
```

## Supabase Configuration

This project uses [Supabase](https://supabase.com/) for authentication. Environment variables are declared via Astro's `astro:env` schema and are treated as **server-only secrets** — they are never exposed to the client.

### First-time setup (local, no cloud project needed)

Requires [Docker](https://www.docker.com/) and ~7 GB RAM.

1. Create your `.env` file:

```bash
cp .env.example .env
```

2. Initialize the local Supabase project (creates a `supabase/` config folder):

```bash
npx supabase init
```

3. Start the local stack (downloads Docker images on first run):

```bash
npx supabase start
```

4. Copy the credentials printed by the CLI into your `.env` and `.dev.vars`:

```
SUPABASE_URL=http://127.0.0.1:54321
SUPABASE_KEY=<anon key from CLI output>
```

5. To stop the stack when done:

```bash
npx supabase stop
```

The local Studio UI is available at `http://localhost:54323`.

This project uses Supabase Auth plus application tables managed through migrations. Apply local schema changes with:

```bash
npx supabase db reset
```

The first domain migration creates company and membership tables used to isolate data per company and assign the initial `admin` role.

The reminder guardrail migration adds a safe contract for future renewal alerts:

- reminder identity is deduplicated by `(company_id, license_ref, reminder_date, recipient_email)`
- reminder lifecycle is tracked with `pending | sent | failed`
- delivery attempts are tracked in an append-only attempts table
- this does **not** send emails yet; provider delivery is implemented later

### Reminder contract (F-02)

F-02 introduces the delivery guardrail only. It does not run a scheduler and does not send real emails.

- lifecycle: `pending -> sent` or `pending -> failed`
- every transition increments `attempt_count` and updates `last_attempted_at`
- each delivery attempt is written to append-only `license_renewal_reminder_attempts`
- dedup key is `(company_id, license_ref, reminder_date, recipient_email)`

S-06 is responsible for the actual email delivery pipeline and scheduling. It should use the F-02 internal reminder API instead of bypassing this contract.

After resetting the local database, verify the company boundary and admin start path with this smoke path:

1. Run the app with `npm run dev`.
2. Open `/auth/signup`.
3. Create an account with a company name, email, and password.
4. If Supabase returns an active session, confirm the app redirects to `/dashboard`.
5. If email confirmation prevents an immediate session, confirm the app redirects to `/auth/confirm-email`.
6. Confirm Supabase contains one `companies` row and one `company_memberships` row with role `admin`.
7. Sign in with the created account and confirm the app redirects to `/dashboard`.
8. Confirm `/dashboard` shows the company name and role.
9. Confirm an authenticated user without a membership is redirected away from company-scoped dashboard content.

Preview and production Supabase schema changes are applied separately from Cloudflare deploys. Get human approval before running remote database migrations, because Worker rollback does not roll back Supabase schema or data changes.

### Using a cloud Supabase project instead

If you prefer to use a hosted Supabase project, add these variables to your `.env` and `.dev.vars` files:

| Variable       | Description                                                |
| -------------- | ---------------------------------------------------------- |
| `SUPABASE_URL` | Project URL from Supabase dashboard → Settings → API       |
| `SUPABASE_KEY` | `anon` public key from Supabase dashboard → Settings → API |

```
SUPABASE_URL=https://<project-ref>.supabase.co
SUPABASE_KEY=<anon-key>
```

### Email confirmation in local development

By default Supabase requires email confirmation before a user can sign in. To skip this during local development:

1. Open the Supabase dashboard for your project
2. Go to **Authentication → Email → Confirm email**
3. Toggle it **off**

Users can then sign in immediately after sign-up without clicking a confirmation link.

### Auth routes

| Route                 | Description                                                             |
| --------------------- | ----------------------------------------------------------------------- |
| `/`                   | ITventory public entry with sign-up and sign-in calls to action         |
| `/auth/signin`        | Email/password sign-in form                                             |
| `/auth/signup`        | Email/password sign-up form with required company name                  |
| `/auth/confirm-email` | Post-signup "check your inbox" page                                     |
| `/auth/company-required` | Signed-in recovery page for accounts without supported company access |
| `/dashboard`          | Empty company workspace requiring authentication and membership          |

Route protection is handled in `src/middleware.ts`. Add paths to the `PROTECTED_ROUTES` array there to require authentication.

## Deployment

This project deploys to [Cloudflare Workers](https://workers.cloudflare.com/).

1. Build the project:

```bash
npm run build
```

2. Deploy with Wrangler:

```bash
npx wrangler deploy
```

Set `SUPABASE_URL` and `SUPABASE_KEY` as secrets in your Cloudflare dashboard or via `npx wrangler secret put`.

## CI

GitHub Actions runs lint + build on every push and PR to `master`. Configure `SUPABASE_URL` and `SUPABASE_KEY` as repository secrets in GitHub for the build step.

## License

MIT
