# Student Dashboard

A comprehensive ASP.NET Core 8.0 web application for managing student academic information, courses, assignments, and progress tracking.

## Features

### 🎓 Student Management
- User registration and authentication with JWT
- Student profile management
- Academic progress tracking
- GPA calculation and monitoring

### 📚 Course Management
- Course enrollment and management
- Course details and information
- Instructor information
- Course schedules and timetables

### 📝 Assignment & Assessment
- Assignment submission and tracking
- Test and quiz management
- Grade recording and feedback
- Progress monitoring

### 📊 Dashboard & Analytics
- Interactive dashboard with charts and graphs
- GPA progress tracking
- Attendance monitoring
- Homework and test statistics
- Calendar integration
- Leaderboard system
- Rewards and achievements

### 🔔 Notifications
- Real-time notifications using SignalR
- Assignment due date reminders
- Grade posting notifications
- Course announcements

### 🎨 Modern UI/UX
- Responsive design for all devices
- Interactive charts using Chart.js
- Modern CSS with animations
- Intuitive navigation

## Technology Stack

- **Backend**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT Bearer tokens
- **Frontend**: Razor Pages with HTML, CSS, JavaScript
- **Real-time**: SignalR for notifications
- **Charts**: Chart.js for data visualization
- **Logging**: Serilog
- **Password Hashing**: BCrypt

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd StudentDashboard
   ```

2. **Update connection string**
   Edit `appsettings.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentDashboardDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Restore packages**
   ```bash
   dotnet restore
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Default Login Credentials

The application comes with pre-seeded data. You can use these credentials to log in:

- **Student Account**: `john.doe@student.com` / `password123`
- **Admin Account**: `admin@studentdashboard.com` / `admin123`

## Project Structure

```
StudentDashboard/
├── Controllers/          # API and MVC controllers
├── Data/                # Entity Framework context and models
├── Hubs/                # SignalR hubs for real-time features
├── Models/              # Data models and view models
├── Services/            # Business logic services
├── Views/               # Razor views
├── wwwroot/             # Static files (CSS, JS, images)
├── Program.cs           # Application entry point
└── appsettings.json     # Configuration
```

## Key Features Explained

### Dashboard
- **Progress GPA**: Interactive bar chart showing GPA progress over time
- **Attendance**: Line chart displaying attendance percentage by month
- **Homework**: Donut chart showing homework completion status
- **Tests**: Donut chart displaying test performance
- **Calendar**: Weekly calendar with schedule information
- **Rating**: Student ranking within group and overall
- **Course Info**: Current course details and payment information
- **Rewards**: Recent achievements and points earned
- **Leaderboard**: Student rankings and scores

### Course Management
- View enrolled courses
- Course details and curriculum
- Assignment and test schedules
- Progress tracking
- Enrollment in new courses

### Profile Management
- Personal information editing
- Academic progress overview
- Password change functionality
- Avatar upload
- Notification preferences

## API Endpoints

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
- `GET /api/dashboard/course` - Get course information
- `GET /api/dashboard/rewards` - Get rewards data
- `GET /api/dashboard/leaderboard` - Get leaderboard data

### Courses
- `GET /courses` - View all courses
- `GET /courses/{id}` - View course details
- `POST /courses/enroll` - Enroll in course
- `POST /courses/{id}/complete` - Mark course as completed

### Profile
- `GET /profile` - View profile
- `POST /profile` - Update profile
- `POST /profile/change-password` - Change password
- `POST /profile/upload-avatar` - Upload avatar

## Database Schema

The application uses Entity Framework Core with the following main entities:

- **User**: User accounts and authentication
- **Student**: Student-specific information
- **Course**: Course details and information
- **Enrollment**: Student-course relationships
- **Assignment**: Course assignments
- **Submission**: Student assignment submissions
- **Test**: Course tests and quizzes
- **TestResult**: Student test results
- **Grade**: Student grades and scores
- **Attendance**: Class attendance records
- **Notification**: System notifications
- **Reward**: Student achievements and rewards
- **Schedule**: Course schedules and timetables

## Configuration

### JWT Settings
Configure JWT settings in `appsettings.json`:
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

### Logging
The application uses Serilog for structured logging. Logs are written to:
- Console output
- File: `logs/student-dashboard-{date}.txt`

## Development

### Adding New Features

1. **Models**: Add new entities in `Models/EntityModels.cs`
2. **Database**: Update `Data/ApplicationDbContext.cs` with new DbSets
3. **Controllers**: Create new controllers in `Controllers/`
4. **Services**: Add business logic in `Services/`
5. **Views**: Create Razor views in `Views/`
6. **API**: Add new endpoints as needed

### Database Migrations

To create a new migration:
```bash
dotnet ef migrations add MigrationName
```

To update the database:
```bash
dotnet ef database update
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions, please contact the development team or create an issue in the repository.

## Changelog

### Version 1.0.0
- Initial release
- Student dashboard with comprehensive features
- JWT authentication
- Real-time notifications
- Interactive charts and analytics
- Course management
- Assignment and test tracking
- Profile management
- Responsive design