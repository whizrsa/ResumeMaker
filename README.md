# Resume Maker - Professional Resume Builder

A modern, professional ASP.NET Core MVC web application for creating and managing resumes with PDF export functionality.

## Features

### ? Core Features
- **User Authentication** - Secure login and registration using ASP.NET Core Identity
- **Resume Creation** - Create multiple professional resumes
- **Resume Editing** - Edit and update resume information easily
- **Resume Preview** - View your resume in a professional format
- **PDF Export** - Print or save your resume as PDF
- **Delete Resumes** - Remove unwanted resumes

### ?? Resume Sections
- **Personal Information** - Name, email, phone, address
- **Professional Summary** - Brief professional overview
- **Work Experience** - Job title, company, location, dates, and description
- **Education** - Degree, institution, and dates
- **Certifications** - Certification name and acquisition date
- **Skills** - Comma-separated list of skills with visual tags

### ?? Design Features
- Modern gradient color scheme (Purple/Blue theme)
- Responsive design that works on all devices
- Clean and professional resume layout
- Card-based editing interface
- Smooth transitions and hover effects
- Print-optimized styling for PDF generation

## Technology Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **Authentication**: ASP.NET Core Identity
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Bootstrap 5, Custom CSS with Gradients
- **PDF Generation**: Browser print functionality

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full SQL Server)
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
2. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ResumeMakerDb;Trusted_Connection=true;MultipleActiveResultSets=true"
   }
   ```

3. Run database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Navigate to `https://localhost:5001` in your browser

## Usage Guide

### Creating Your First Resume

1. **Register/Login** - Create an account or log in
2. **Create Resume** - Click "Create New Resume" button
3. **Fill Personal Information** - Enter your basic details
4. **Add Work Experience** - Click "+ Add Work Experience" and fill in details
5. **Add Education** - Click "+ Add Education" and enter your degrees
6. **Add Certifications** - Click "+ Add Certification" for any certifications
7. **Add Skills** - Enter comma-separated skills
8. **Save** - Click "Save Changes"

### Exporting to PDF

1. Go to your resume list
2. Click "View" on the resume you want to export
3. Click "Print / Save as PDF"
4. In the print dialog, select "Save as PDF"
5. Choose your location and save

### Managing Resumes

- **View** - Preview your resume in professional format
- **Edit** - Modify any section of your resume
- **Delete** - Remove resumes you no longer need (with confirmation)

## Database Schema

### Resume
- Id, FullName, Email, CellPhoneNumber, Address, Summary, Skills (JSON)

### Experience
- Id, JobTitle, Company, Location, StartDate, EndDate, Description, ResumeId

### Education
- Id, Institution, Degree, StartDate, EndDate, ResumeId

### Certification
- Id, CertificationName, DateAcquired, ResumeId

## Features Highlights

### Professional Design
- Modern gradient backgrounds
- Card-based layout for better organization
- Responsive tables for resume listing
- Beautiful resume preview with professional formatting
- Emoji icons for visual appeal

### User Experience
- Intuitive navigation
- Breadcrumb-style workflow
- Confirmation dialogs for deletions
- Form validation
- Success/Error messaging
- Back navigation from all pages

### Security
- Authentication required for all resume operations
- CSRF protection on forms
- Role-based authorization ready

## Credits

**Made with ?? by Elvis Chimuse**

## License

This project is available for educational and personal use.

## Support

For issues or questions, please create an issue in the repository.
