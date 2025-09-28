# Student Dashboard - Full UI Project (.NET Core 8)

A comprehensive student management system built with .NET Core 8, featuring a modern dashboard, authentication, real-time notifications, and full CRUD operations.

## 🚀 Features

### Core Dashboard
- **Interactive Dashboard**: Real-time data visualization with charts and widgets
- **Progress Tracking**: GPA monitoring, attendance tracking, homework management
- **Calendar Integration**: Schedule management with daily/weekly views
- **Leaderboard System**: Student rankings and achievements
- **Course Management**: Enrollment, progress tracking, and course details

### Authentication & Security
- **JWT Authentication**: Secure token-based authentication
- **User Registration/Login**: Complete auth flow with validation
- **Role-based Access**: Student, Admin, and Instructor roles
- **Password Security**: BCrypt hashing for secure password storage

### Real-time Features
- **SignalR Integration**: Real-time notifications and updates
- **Live Dashboard Updates**: Automatic data refresh
- **Notification System**: In-app and email notifications

### Data Management
- **Entity Framework Core**: Full ORM with SQL Server
- **Database Migrations**: Automated schema management
- **API Controllers**: RESTful API endpoints
- **Data Validation**: Comprehensive input validation

### UI/UX
- **Responsive Design**: Mobile-first, works on all devices
- **Modern Interface**: Clean, professional design
- **Interactive Elements**: Hover effects, animations, modals
- **Accessibility**: WCAG compliant design

## 🛠️ Technology Stack

### Backend
- **.NET Core 8**: Latest framework with performance improvements
- **Entity Framework Core 8**: ORM with SQL Server
- **JWT Authentication**: Secure token-based auth
- **SignalR**: Real-time communication
- **AutoMapper**: Object mapping
- **Serilog**: Structured logging
- **Swagger**: API documentation

### Frontend
- **HTML5/CSS3**: Modern web standards
- **JavaScript ES6+**: Interactive functionality
- **Chart.js**: Data visualization
- **Font Awesome**: Icon library
- **Responsive Grid**: CSS Grid and Flexbox

### Database
- **SQL Server LocalDB**: Development database
- **Entity Framework Migrations**: Schema management
- **Indexed Queries**: Optimized performance

## 📁 Project Structure

```
StudentDashboard/
├── Controllers/
│   ├── AuthController.cs          # Authentication endpoints
│   ├── DashboardController.cs     # Dashboard API
│   ├── ProfileController.cs       # User profile management
│   └── CoursesController.cs       # Course management
├── Data/
│   └── ApplicationDbContext.cs    # Entity Framework context
├── Models/
│   ├── DashboardModels.cs         # Dashboard view models
│   └── EntityModels.cs            # Database entities
├── Services/
│   ├── AuthService.cs             # Authentication logic
│   └── DashboardService.cs        # Dashboard business logic
├── Hubs/
│   └── NotificationHub.cs         # SignalR hub
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           # Main dashboard
│   ├── Profile/
│   │   └── Index.cshtml           # User profile page
│   ├── Courses/
│   │   └── Index.cshtml           # Courses management
│   ├── Auth/
│   │   └── Login.cshtml           # Login/Register page
│   └── Shared/
│       └── _Layout.cshtml         # Main layout
├── wwwroot/
│   ├── css/
│   │   ├── site.css              # Main styles
│   │   ├── profile.css           # Profile page styles
│   │   └── courses.css           # Courses page styles
│   ├── js/
│   │   └── site.js               # JavaScript functionality
│   └── images/                   # Static assets
└── Program.cs                    # Application startup
```

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server LocalDB (or SQL Server)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository:**
```bash
git clone <repository-url>
cd StudentDashboard
```

2. **Restore dependencies:**
```bash
dotnet restore
```

3. **Update connection string** in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentDashboardDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

4. **Build the project:**
```bash
dotnet build
```

5. **Run the application:**
```bash
dotnet run
```

6. **Open your browser:**
```
https://localhost:5001
```

## 📊 Database Schema

### Core Entities
- **Users**: User accounts and authentication
- **Students**: Student-specific information
- **Courses**: Course catalog and details
- **Enrollments**: Student-course relationships
- **Assignments**: Course assignments and homework
- **Tests**: Examinations and quizzes
- **Grades**: Academic performance tracking
- **Attendance**: Class attendance records
- **Notifications**: System notifications
- **Rewards**: Achievement system
- **Schedules**: Class schedules and timetables

### Relationships
- One-to-One: User ↔ Student
- One-to-Many: Course → Enrollments, Assignments, Tests
- Many-to-Many: Students ↔ Courses (via Enrollments)

## 🔧 API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/auth/profile` - Get user profile

### Dashboard
- `GET /api/dashboard` - Get dashboard data
- `GET /api/dashboard/gpa` - Get GPA data
- `GET /api/dashboard/attendance` - Get attendance data
- `GET /api/dashboard/homework` - Get homework data
- `GET /api/dashboard/tests` - Get test data
- `GET /api/dashboard/calendar` - Get calendar data
- `GET /api/dashboard/rating` - Get rating data
- `GET /api/dashboard/course` - Get course info
- `GET /api/dashboard/rewards` - Get rewards data
- `GET /api/dashboard/leaderboard` - Get leaderboard data

### Profile Management
- `GET /Profile` - Profile page
- `POST /Profile/UpdateProfile` - Update profile
- `POST /Profile/ChangePassword` - Change password
- `POST /Profile/UploadAvatar` - Upload avatar

### Course Management
- `GET /Courses` - Courses page
- `GET /Courses/{id}` - Course details
- `POST /Courses/enroll` - Enroll in course
- `POST /Courses/{id}/complete` - Complete course

## 🎨 UI Components

### Dashboard Widgets
1. **Progress GPA**: Bar chart showing weekly GPA performance
2. **Attendance**: Line chart with monthly attendance trends
3. **Homework**: Donut chart with status breakdown
4. **Tests**: Donut chart showing test results
5. **Calendar**: Weekly view with daily schedule
6. **Rating**: Student ranking system
7. **Course Info**: Detailed course information
8. **Rewards**: Recent achievements
9. **Leaderboard**: Student rankings

### Interactive Features
- **Real-time Updates**: Live data refresh
- **Filter Controls**: Time period and category filters
- **Modal Dialogs**: Course details and enrollment
- **Form Validation**: Client and server-side validation
- **File Upload**: Avatar and document upload
- **Responsive Design**: Mobile-optimized interface

## 🔒 Security Features

- **JWT Tokens**: Secure authentication
- **Password Hashing**: BCrypt encryption
- **Input Validation**: XSS and injection prevention
- **CORS Configuration**: Cross-origin security
- **SQL Injection Protection**: Parameterized queries
- **File Upload Security**: Type and size validation

## 📱 Responsive Design

- **Mobile First**: Optimized for mobile devices
- **Breakpoints**: 768px, 1024px, 1200px
- **Flexible Grid**: CSS Grid and Flexbox
- **Touch Friendly**: Large touch targets
- **Progressive Enhancement**: Works without JavaScript

## 🚀 Performance Optimizations

- **Database Indexing**: Optimized queries
- **Caching**: Response caching
- **Minification**: CSS and JS minification
- **Image Optimization**: Compressed assets
- **Lazy Loading**: On-demand content loading
- **CDN Integration**: External library delivery

## 🧪 Testing

### Manual Testing
1. **Authentication Flow**: Login, registration, logout
2. **Dashboard Functionality**: Widget interactions, data updates
3. **Profile Management**: Update profile, change password
4. **Course Management**: Enroll, view details, complete
5. **Responsive Design**: Test on different screen sizes

### API Testing
Use Swagger UI at `https://localhost:5001/swagger` for API testing.

## 📈 Monitoring & Logging

- **Serilog Integration**: Structured logging
- **File Logging**: Daily log files
- **Console Logging**: Development debugging
- **Error Tracking**: Exception handling
- **Performance Monitoring**: Request timing

## 🔧 Configuration

### JWT Settings
```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLongForJWTTokenGeneration",
    "Issuer": "StudentDashboard",
    "Audience": "StudentDashboardUsers",
    "ExpiryInDays": 7
  }
}
```

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentDashboardDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## 🚀 Deployment

### Development
```bash
dotnet run
```

### Production
```bash
dotnet publish -c Release -o ./publish
```

### Docker (Optional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY ./publish /app
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "StudentDashboard.dll"]
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For questions or support:
- Create an issue in the repository
- Check the documentation
- Review the API documentation at `/swagger`

## 🔄 Version History

- **v1.0.0**: Initial release with core dashboard functionality
- **v1.1.0**: Added authentication and user management
- **v1.2.0**: Implemented real-time features with SignalR
- **v1.3.0**: Added course management and profile features
- **v2.0.0**: Full UI project with comprehensive features

---

**Built with ❤️ using .NET Core 8**