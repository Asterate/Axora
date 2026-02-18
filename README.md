## Project description of language platform

Project general purpose is to create platform for different language schools to use to enhance learning planning and make processes going with it more efficient.

### General requirements

- System level
    - The **platform operator** runs the SaaS. System-level roles manage the platform itself.
    - **SystemAdmin**
        - Full access. Manage companies, subscriptions, billing, system config, feature flags. View cross-tenant analytics. Impersonate company users for support.
    - **SystemSupport**
        - View-only access to company data for troubleshooting. Create support tickets. Cannot modify billing or system config.
    - **SystemBilling**
        - Manage subscription plans, pricing tiers, invoices, payment status. Cannot access company operational data.
- Comapany level
    - Each **company** is an isolated tenant. Company-level roles manage operations within that tenant.
    - **CompanyOwner**
        - Full control within tenant. Manage company settings, users, roles, subscription tier. Transfer ownership. Cannot access other tenants.
    - **CompanyAdmin**
        - Manage users, roles, and all operational data within tenant. Cannot change subscription or billing.
    - **CompanyManager**
        - Full CRUD on operational entities (business-specific data). Can view reports. Cannot manage users or company settings.
    - **CompanyEmployee**
        - Limited CRUD — create and view own work, edit assigned records. Read-only on shared reference data.
- Multi tendants requirements
    - **Data isolation**: Tenant data is strictly isolated. Queries always filter by `CompanyId`. No cross-tenant data leaks.
    - **Path-based routing**: `bikerental.io/acme`
    - **Company registration & onboarding**: Self-service signup creates a new tenant with a CompanyOwner user.
    - **Subscription tiers**: Free (limited), Standard, Premium — affecting feature access, entity limits, or user counts.
    - **Audit trail**: Log who changed what and when, per tenant.
    - **Soft delete**: No hard deletes on business entities. Deactivated companies retain data but lose access.
- Identity and authentication
    - ASP.NET Core Identity with per-tenant user management
    - Users can belong to multiple companies (e.g., a person working for multiple rental shops)
    - Role-based authorization via `[Authorize(Roles = "...")]`
    - Login, registration, password reset

### Platform specific requirements

- It has to be able to accommodate different language schools
    - different language courses
    - CEFR levels
    - durations
    - class size config
    - placement test: each language school own or platform certified test - teachers can change in 2 weeks

- Track student progress
    - levels
    - languages
    - schedule
    - retakes
    - attendance tracking
    - certificates

- Teachers
    - teacher certificate: native, non-native
    - availability
    - schedule