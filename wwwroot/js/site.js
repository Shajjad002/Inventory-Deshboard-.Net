// Dashboard JavaScript functionality

document.addEventListener('DOMContentLoaded', function() {
    // Initialize dashboard interactions
    initializeFilters();
    initializeSidebar();
    initializeWidgets();
});

// Filter functionality
function initializeFilters() {
    const filterButtons = document.querySelectorAll('.filter-btn');
    
    filterButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Remove active class from siblings
            const parent = this.parentElement;
            const siblings = parent.querySelectorAll('.filter-btn');
            siblings.forEach(sibling => sibling.classList.remove('active'));
            
            // Add active class to clicked button
            this.classList.add('active');
            
            // Here you would typically make an AJAX call to update the data
            console.log('Filter changed:', this.textContent);
        });
    });
}

// Sidebar functionality
function initializeSidebar() {
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
        item.addEventListener('click', function() {
            // Remove active class from all menu items
            menuItems.forEach(mi => mi.classList.remove('active'));
            
            // Add active class to clicked item
            this.classList.add('active');
            
            // Handle navigation (you would implement actual navigation here)
            console.log('Menu item clicked:', this.querySelector('i').className);
        });
    });
}

// Widget functionality
function initializeWidgets() {
    // Calendar day selection
    const calendarDays = document.querySelectorAll('.calendar-day');
    calendarDays.forEach(day => {
        day.addEventListener('click', function() {
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
        });
    });
    
    // Rating button functionality
    const ratingBtn = document.querySelector('.rating-btn');
    if (ratingBtn) {
        ratingBtn.addEventListener('click', function() {
            alert('Learn more about rewards and rating system!');
        });
    }
    
    // Learn more button functionality
    const learnMoreBtn = document.querySelector('.learn-more-btn');
    if (learnMoreBtn) {
        learnMoreBtn.addEventListener('click', function() {
            alert('Learn more about payment options!');
        });
    }
    
    // User profile dropdown
    const userProfile = document.querySelector('.user-profile');
    if (userProfile) {
        userProfile.addEventListener('click', function() {
            // Toggle profile dropdown (you would implement actual dropdown here)
            console.log('Profile clicked');
        });
    }
    
    // Language selector
    const languageSelector = document.querySelector('.language-selector');
    if (languageSelector) {
        languageSelector.addEventListener('click', function() {
            // Toggle language dropdown (you would implement actual dropdown here)
            console.log('Language selector clicked');
        });
    }
}

// Utility functions
function updateWidgetData(widgetType, data) {
    // This function would be used to update widget data dynamically
    console.log(`Updating ${widgetType} with data:`, data);
}

function showNotification(message, type = 'info') {
    // Simple notification system
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

// Add CSS animations
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    @keyframes slideOut {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(100%);
            opacity: 0;
        }
    }
    
    .sidebar.collapsed {
        width: 60px;
    }
    
    .sidebar.collapsed .menu-item {
        width: 40px;
        height: 40px;
    }
`;
document.head.appendChild(style);
