# Product Requirements Document (PRD) - ResumeMaker Enhancements

## Overview
Enhance ResumeMaker with theme selection at creation time, external login (Google), email verification, account deletion, password visibility toggle, and improved PDF themes.

## Goals
- Let users choose a resume theme (Minimal, Classic, Modern) during creation and persist it.
- Simplify sign-in/registration with Google OAuth.
- Ensure account security and trust via email verification.
- Improve UX with password visibility toggles.
- Provide multiple professional PDF themes.

## Success Metrics
- Increased resume creation completion rate.
- Reduced login friction via Google sign-in usage.
- Verified email rate > 80% within 24h.
- Lower support issues related to unreadable PDF/export.

## Functional Requirements
1. Theme Selection
   - Radio options in `Views/Home/Create.cshtml`: Minimal, Classic, Modern.
   - Persist to `Resume.Theme` (string, max 20; default minimal).
   - `HomeController.View` uses stored theme unless query overrides.

2. PDF Themes
   - `Views/Home/View.cshtml` supports theme switcher buttons for preview (non-paywalled).
   - Theme-specific CSS blocks: minimal/classic/modern.

3. External Login (Google)
   - Register Google auth in Program.cs.
   - Add Google buttons to Login/Register pages.
   - Requires `Authentication:Google:ClientId` and `ClientSecret` in configuration.

4. Email Verification
   - Enable `RequireConfirmedAccount = true`.
   - Implement `IEmailSender` with SMTP settings.
   - Send confirmation email in Register flow (already handled by Identity scaffold).

5. Password Visibility Toggle
   - Add eye button to login/register password inputs.

6. Account Deletion
   - Ensure Identity Manage area exposes delete personal data; if not, add a delete page.

7. Email Validity Check
   - Add server-side validation pipeline for email deliverability (MX lookup + SMTP probe). Phase 2.

## Non-Functional Requirements
- Keep filenames and routes unchanged.
- Maintain consistent visual design.
- Backwards compatible migrations.
- Secure handling of secrets.

## Config
- SMTP: appsettings.json keys `Smtp:Host`, `Smtp:Port`, `Smtp:User`, `Smtp:Pass`, `Smtp:From`.
- Google OAuth: `Authentication:Google:ClientId`, `Authentication:Google:ClientSecret`.

## Risks
- Email deliverability checks can be slow; must be async/background.
- Misconfiguration of Google/SMTP could block sign-in/registration.

## Out of Scope (for now)
- Payments for themes.
- Advanced email verification beyond confirmation token.
- Server-side PDF generator.
