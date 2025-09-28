// Dashboard JavaScript functionality with real-time updates

class DashboardManager {
    constructor() {
        this.connection = null;
        this.studentId = 1; // For demo purposes
        this.charts = {};
        this.init();
    }

    async init() {
        await this.initializeSignalR();
        this.initializeFilters();
        this.initializeSidebar();
        this.initializeWidgets();
        this.initializeRealTimeUpdates();
    }

    async initializeSignalR() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("/dashboardHub")
            .build();

        this.connection.on("DashboardUpdate", (data) => {
            this.updateDashboard(data);
        });

        this.connection.on("GradeUpdate", (gradeData) => {
            this.updateGrades(gradeData);
        });

        this.connection.on("AttendanceUpdate", (attendanceData) => {
            this.updateAttendance(attendanceData);
        });

        this.connection.on("ReceiveNotification", (message) => {
            this.showNotification(message, 'info');
        });

        try {
            await this.connection.start();
            console.log("SignalR Connected");
            
            // Join the student's group
            await this.connection.invoke("JoinGroup", `student_${this.studentId}`);
        } catch (err) {
            console.error("SignalR Connection Error:", err);
        }
    }

    initializeFilters() {
        const filterButtons = document.querySelectorAll('.filter-btn');
        
        filterButtons.forEach(button => {
            button.addEventListener('click', async (e) => {
                e.preventDefault();
                
                // Remove active class from siblings
                const parent = button.parentElement;
                const siblings = parent.querySelectorAll('.filter-btn');
                siblings.forEach(sibling => sibling.classList.remove('active'));
                
                // Add active class to clicked button
                button.classList.add('active');
                
                // Get the widget type from the parent widget
                const widget = button.closest('.widget');
                const widgetType = this.getWidgetType(widget);
                
                // Update the widget data
                await this.updateWidgetData(widgetType, button.textContent.trim());
            });
        });
    }

    initializeSidebar() {
        const sidebarToggle = document.querySelector('.sidebar-toggle');
        const sidebar = document.querySelector('.sidebar');
        
        if (sidebarToggle && sidebar) {
            sidebarToggle.addEventListener('click', function() {
                sidebar.classList.toggle('collapsed');
            });
        }
        
        // Menu item click handlers
        const menuItems = document.querySelectorAll('.menu-item');
        menuItems.forEach(item => {
            item.addEventListener('click', function(e) {
                // Don't prevent default if it's a link
                if (this.querySelector('a')) {
                    return;
                }
                
                e.preventDefault();
                
                // Remove active class from all menu items
                menuItems.forEach(mi => mi.classList.remove('active'));
                
                // Add active class to clicked item
                this.classList.add('active');
                
                // Handle navigation
                const iconClass = this.querySelector('i').className;
                console.log('Menu item clicked:', iconClass);
            });
        });
    }

    initializeWidgets() {
        // Calendar day selection
        const calendarDays = document.querySelectorAll('.calendar-day');
        calendarDays.forEach(day => {
            day.addEventListener('click', async function() {
                // Remove today class from all days
                calendarDays.forEach(d => d.classList.remove('today'));
                
                // Add today class to clicked day
                this.classList.add('today');
                
                // Update calendar info
                const dayName = this.querySelector('.day-name').textContent;
                const dayNumber = this.querySelector('.day-number').textContent;
                const calendarInfo = document.querySelector('.calendar-info p');
                if (calendarInfo) {
                    calendarInfo.textContent = `${dayNumber} January, ${dayName}`;
                }

                // Update schedule for selected day
                await dashboardManager.updateSchedule(dayNumber);
            });
        });
        
        // Rating button functionality
        const ratingBtn = document.querySelector('.rating-btn');
        if (ratingBtn) {
            ratingBtn.addEventListener('click', function() {
                dashboardManager.showNotification('Learn more about rewards and rating system!', 'info');
            });
        }
        
        // Learn more button functionality
        const learnMoreBtn = document.querySelector('.learn-more-btn');
        if (learnMoreBtn) {
            learnMoreBtn.addEventListener('click', function() {
                dashboardManager.showNotification('Learn more about payment options!', 'info');
            });
        }
        
        // User profile dropdown
        const userProfile = document.querySelector('.user-profile');
        if (userProfile) {
            userProfile.addEventListener('click', function() {
                console.log('Profile clicked');
            });
        }
        
        // Language selector
        const languageSelector = document.querySelector('.language-selector');
        if (languageSelector) {
            languageSelector.addEventListener('click', function() {
                console.log('Language selector clicked');
            });
        }
    }

    initializeRealTimeUpdates() {
        // Set up periodic updates for dashboard data
        setInterval(async () => {
            await this.refreshDashboardData();
        }, 30000); // Update every 30 seconds

        // Set up periodic updates for notifications
        setInterval(async () => {
            await this.refreshNotifications();
        }, 60000); // Update every minute
    }

    getWidgetType(widget) {
        if (widget.classList.contains('progress-gpa-widget')) return 'progress-gpa';
        if (widget.classList.contains('attendance-widget')) return 'attendance';
        if (widget.classList.contains('homework-widget')) return 'homework';
        if (widget.classList.contains('test-widget')) return 'test';
        if (widget.classList.contains('calendar-widget')) return 'calendar';
        if (widget.classList.contains('rating-widget')) return 'rating';
        if (widget.classList.contains('course-widget')) return 'course-info';
        if (widget.classList.contains('rewards-widget')) return 'rewards';
        if (widget.classList.contains('leaderboard-widget')) return 'leaderboard';
        return 'unknown';
    }

    async updateWidgetData(widgetType, period) {
        try {
            let response;
            switch (widgetType) {
                case 'progress-gpa':
                    response = await fetch(`/api/DashboardApi/progress-gpa/${this.studentId}?period=${period}`);
                    break;
                case 'attendance':
                    response = await fetch(`/api/DashboardApi/attendance/${this.studentId}?period=${period}`);
                    break;
                case 'rewards':
                    response = await fetch(`/api/DashboardApi/rewards/${this.studentId}?period=${period}`);
                    break;
                case 'leaderboard':
                    response = await fetch(`/api/DashboardApi/leaderboard/${this.studentId}?filter=${period}`);
                    break;
                default:
                    return;
            }

            if (response.ok) {
                const data = await response.json();
                this.updateWidgetDisplay(widgetType, data);
            }
        } catch (error) {
            console.error(`Error updating ${widgetType}:`, error);
        }
    }

    updateWidgetDisplay(widgetType, data) {
        switch (widgetType) {
            case 'progress-gpa':
                this.updateGpaChart(data);
                break;
            case 'attendance':
                this.updateAttendanceChart(data);
                break;
            case 'rewards':
                this.updateRewardsList(data);
                break;
            case 'leaderboard':
                this.updateLeaderboard(data);
                break;
        }
    }

    updateGpaChart(data) {
        if (this.charts.gpaChart) {
            this.charts.gpaChart.data.labels = data.dataPoints.map(d => d.day);
            this.charts.gpaChart.data.datasets[0].data = data.dataPoints.map(d => d.value);
            this.charts.gpaChart.update();
        }
    }

    updateAttendanceChart(data) {
        if (this.charts.attendanceChart) {
            this.charts.attendanceChart.data.labels = data.dataPoints.map(d => d.month);
            this.charts.attendanceChart.data.datasets[0].data = data.dataPoints.map(d => d.percentage);
            this.charts.attendanceChart.update();
        }
    }

    updateRewardsList(data) {
        const rewardsList = document.querySelector('.rewards-list');
        if (rewardsList) {
            rewardsList.innerHTML = data.rewards.map(reward => `
                <div class="reward-item">
                    <i class="fas fa-${reward.icon === 'checkmark' ? 'check' : reward.icon}"></i>
                    <div class="reward-info">
                        <span class="reward-type">${reward.type}</span>
                        <span class="reward-date">(${reward.date})</span>
                    </div>
                    <span class="reward-points">+${reward.points}</span>
                </div>
            `).join('');
        }
    }

    updateLeaderboard(data) {
        const leaderboardList = document.querySelector('.leaderboard-list');
        if (leaderboardList) {
            leaderboardList.innerHTML = data.entries.map(entry => `
                <div class="leaderboard-entry">
                    <span class="rank">${entry.rank}</span>
                    <img src="${entry.avatar}" alt="${entry.name}" class="entry-avatar" />
                    <span class="entry-name">${entry.name}</span>
                    <span class="entry-score">${entry.score}</span>
                    <i class="fas fa-star"></i>
                </div>
            `).join('');
        }
    }

    async updateSchedule(dayNumber) {
        try {
            const response = await fetch(`/api/DashboardApi/calendar/${this.studentId}?month=${new Date().toISOString()}`);
            if (response.ok) {
                const data = await response.json();
                this.updateScheduleDisplay(data.todaySchedule);
            }
        } catch (error) {
            console.error('Error updating schedule:', error);
        }
    }

    updateScheduleDisplay(schedule) {
        const scheduleList = document.querySelector('.schedule-list');
        if (scheduleList) {
            scheduleList.innerHTML = schedule.map(item => `
                <div class="schedule-item ${item.color}">
                    <span class="schedule-time">${item.time}</span>
                    <span class="schedule-type">${item.type}</span>
                    <span class="schedule-title">${item.title}</span>
                </div>
            `).join('');
        }
    }

    async refreshDashboardData() {
        try {
            const response = await fetch(`/api/DashboardApi/data/${this.studentId}`);
            if (response.ok) {
                const data = await response.json();
                this.updateDashboard(data);
            }
        } catch (error) {
            console.error('Error refreshing dashboard data:', error);
        }
    }

    async refreshNotifications() {
        try {
            const response = await fetch(`/api/DashboardApi/notifications/${this.studentId}`);
            if (response.ok) {
                const notifications = await response.json();
                this.updateNotificationCount(notifications.length);
            }
        } catch (error) {
            console.error('Error refreshing notifications:', error);
        }
    }

    updateNotificationCount(count) {
        const notificationBadge = document.querySelector('.icon-badge:last-child span');
        if (notificationBadge) {
            notificationBadge.textContent = count;
        }
    }

    updateDashboard(data) {
        // Update various dashboard components with new data
        console.log('Dashboard updated with new data:', data);
    }

    updateGrades(gradeData) {
        console.log('Grade updated:', gradeData);
        this.showNotification('New grade received!', 'success');
    }

    updateAttendance(attendanceData) {
        console.log('Attendance updated:', attendanceData);
    }

    showNotification(message, type = 'info') {
        const notification = document.createElement('div');
        notification.className = `notification notification-${type}`;
        notification.textContent = message;
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            padding: 15px 20px;
            background: ${type === 'success' ? '#28a745' : type === 'error' ? '#dc3545' : '#4A90E2'};
            color: white;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            z-index: 1000;
            animation: slideIn 0.3s ease;
        `;
        
        document.body.appendChild(notification);
        
        setTimeout(() => {
            notification.style.animation = 'slideOut 0.3s ease';
            setTimeout(() => notification.remove(), 300);
        }, 3000);
    }
}

// Initialize dashboard when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    window.dashboardManager = new DashboardManager();
});
