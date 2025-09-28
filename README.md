# Student Dashboard - .NET Core 8

A comprehensive student dashboard application built with .NET Core 8, featuring a modern UI design with interactive widgets and responsive layout.

## Features

### Dashboard Widgets
- **Progress GPA**: Interactive bar chart showing weekly GPA performance
- **Attendance**: Line chart displaying monthly attendance trends
- **Homework**: Donut chart with homework status breakdown
- **Test**: Donut chart showing test results distribution
- **Calendar**: Weekly calendar view with daily schedule
- **Rating**: Student ranking system with group and flow positions
- **Course Info**: Detailed course information and payment details
- **Rewards**: Recent achievements and point system
- **Leaderboard**: Student rankings with avatars and scores

### UI Components
- **Header**: Search bar, notification icons, user profile, language selector
- **Sidebar**: Navigation menu with collapsible design
- **Responsive Layout**: Mobile-friendly design with adaptive grid system
- **Interactive Elements**: Clickable filters, calendar days, and navigation items

## Technology Stack

- **Backend**: .NET Core 8 MVC
- **Frontend**: HTML5, CSS3, JavaScript (ES6+)
- **Charts**: Chart.js for data visualization
- **Icons**: Font Awesome 6.0
- **Styling**: Custom CSS with modern design principles

## Project Structure

```
StudentDashboard/
├── Controllers/
│   └── HomeController.cs          # Main dashboard controller
├── Models/
│   └── DashboardModels.cs         # Data models for dashboard widgets
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           # Main dashboard view
│   └── Shared/
│       └── _Layout.cshtml         # Main layout template
├── wwwroot/
│   ├── css/
│   │   └── site.css              # Main stylesheet
│   ├── js/
│   │   └── site.js               # JavaScript functionality
│   └── images/                   # Avatar and icon images
├── Program.cs                    # Application startup
└── StudentDashboard.csproj      # Project file
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd StudentDashboard
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

5. Open your browser and navigate to `https://localhost:5001`

## Features Overview

### Dashboard Layout
The dashboard features a clean, modern design with:
- Fixed header with search functionality
- Collapsible sidebar navigation
- Responsive grid layout for widgets
- Interactive elements with hover effects

### Data Visualization
- **Charts**: Bar charts, line charts, and donut charts using Chart.js
- **Real-time Updates**: JavaScript-powered interactive elements
- **Responsive Design**: Adapts to different screen sizes

### Sample Data
The application includes sample data that demonstrates:
- Weekly GPA progression
- Monthly attendance trends
- Homework and test status distributions
- Daily schedule with time slots
- Student rankings and rewards

## Customization

### Adding New Widgets
1. Create a new model in `DashboardModels.cs`
2. Add data to the controller's `CreateSampleData()` method
3. Create the widget HTML in `Index.cshtml`
4. Add corresponding CSS styles in `site.css`

### Modifying Charts
Charts are configured in the JavaScript section of `Index.cshtml`. You can:
- Change chart types (bar, line, doughnut)
- Modify colors and styling
- Add new data points
- Implement real-time updates

### Styling
The CSS is organized with:
- CSS Grid for layout
- Flexbox for component alignment
- CSS variables for consistent theming
- Media queries for responsive design

## Browser Support
- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

## License
This project is licensed under the MIT License.

## Contributing
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## Support
For questions or support, please open an issue in the repository.
