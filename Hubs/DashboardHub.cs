using Microsoft.AspNetCore.SignalR;

namespace StudentDashboard.Hubs
{
    public class DashboardHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public async Task SendDashboardUpdate(string groupName, object data)
        {
            await Clients.Group(groupName).SendAsync("DashboardUpdate", data);
        }

        public async Task SendGradeUpdate(string studentId, object gradeData)
        {
            await Clients.User(studentId).SendAsync("GradeUpdate", gradeData);
        }

        public async Task SendAttendanceUpdate(string groupName, object attendanceData)
        {
            await Clients.Group(groupName).SendAsync("AttendanceUpdate", attendanceData);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
