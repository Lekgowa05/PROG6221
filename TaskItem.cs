using System;

namespace CybersecurityChatbot.Classes
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status => IsCompleted ? "✅ Completed" : "⏳ Pending";

        public string DisplayText
        {
            get
            {
                string reminder = ReminderDate.HasValue
                    ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})"
                    : "";
                return $"{Title}{reminder} - {Status}";
            }
        }

        public bool IsOverdue()
        {
            return !IsCompleted && ReminderDate.HasValue && ReminderDate.Value < DateTime.Now;
        }
    }
}