using System;
using System.Text.RegularExpressions;

namespace CybersecurityChatbot.Classes
{
    public static class NLPEngine
    {
        public enum Intent
        {
            AddTask,
            ShowTasks,
            DeleteTask,
            CompleteTask,
            Quiz,
            Help,
            Greeting,
            None
        }

        public static Intent DetectIntent(string userInput)
        {
            string input = userInput.ToLower().Trim();

            // Greeting detection
            if (Regex.IsMatch(input, @"^(hello|hi|hey|good morning|good afternoon|good evening|greetings)"))
                return Intent.Greeting;

            // Task Addition
            if (Regex.IsMatch(input, @"(add|create|new|set up|enable)\s+(task|reminder|2fa|two.?factor)|remind me to"))
                return Intent.AddTask;

            // Show Tasks
            if (Regex.IsMatch(input, @"(show|list|view|display|get)\s+(tasks|reminders|my tasks)"))
                return Intent.ShowTasks;

            // Delete Task
            if (Regex.IsMatch(input, @"(delete|remove|clear|erase)\s+(task|reminder)"))
                return Intent.DeleteTask;

            // Complete Task
            if (Regex.IsMatch(input, @"(complete|finish|done|mark)\s+(task|reminder)"))
                return Intent.CompleteTask;

            // Quiz
            if (Regex.IsMatch(input, @"(quiz|game|play|test|challenge|knowledge)"))
                return Intent.Quiz;

            // Help
            if (Regex.IsMatch(input, @"(help|assist|support|what can you do)"))
                return Intent.Help;

            return Intent.None;
        }

        public static string ExtractTaskDetails(string input, out string title, out string description, out DateTime? reminderDate)
        {
            title = string.Empty;
            description = string.Empty;
            reminderDate = null;

            string lower = input.ToLower();

            // Extract task title - everything after "add task" or "remind me to"
            Match taskMatch = Regex.Match(input, @"(add task|remind me to|create task|new task)\s*(.*?)(?=remind|$)", RegexOptions.IgnoreCase);
            if (taskMatch.Success)
            {
                title = taskMatch.Groups[2].Value.Trim();
                if (!string.IsNullOrEmpty(title))
                    title = char.ToUpper(title[0]) + title.Substring(1);
            }

            // Extract description (if any)
            Match descMatch = Regex.Match(input, @"(about|for|description:?)\s*(.*?)(?=remind|$)", RegexOptions.IgnoreCase);
            if (descMatch.Success)
            {
                description = descMatch.Groups[2].Value.Trim();
            }

            // Extract reminder date
            Match dateMatch = Regex.Match(input, @"(remind|in|on)\s*(\d+)\s*(day|days|week|weeks|month|months)", RegexOptions.IgnoreCase);
            if (dateMatch.Success)
            {
                int number = int.Parse(dateMatch.Groups[2].Value);
                string unit = dateMatch.Groups[3].Value.ToLower();

                if (unit.StartsWith("day"))
                    reminderDate = DateTime.Now.AddDays(number);
                else if (unit.StartsWith("week"))
                    reminderDate = DateTime.Now.AddDays(number * 7);
                else if (unit.StartsWith("month"))
                    reminderDate = DateTime.Now.AddMonths(number);
            }
            else
            {
                // Default reminder: 7 days if not specified
                if (!string.IsNullOrEmpty(title))
                    reminderDate = DateTime.Now.AddDays(7);
            }

            return title;
        }

        public static string ExtractTaskIdentifier(string input)
        {
            // Try to extract a task ID or title for delete/complete operations
            Match idMatch = Regex.Match(input, @"(task|#)\s*(\d+)", RegexOptions.IgnoreCase);
            if (idMatch.Success)
                return idMatch.Groups[2].Value;

            // Try to extract task title
            Match titleMatch = Regex.Match(input, @"(task|reminder)\s*['\""]?([^'\""]+)['\""]?", RegexOptions.IgnoreCase);
            if (titleMatch.Success)
                return titleMatch.Groups[2].Value.Trim();

            return string.Empty;
        }

        public static bool ContainsCybersecurityKeyword(string input)
        {
            string[] keywords = {
                "password", "phishing", "2fa", "two factor", "authentication",
                "firewall", "antivirus", "malware", "ransomware", "encryption",
                "security", "privacy", "vpn", "certificate", "ssl", "tls",
                "breach", "attack", "threat", "vulnerability", "patch",
                "backup", "recovery", "social engineering"
            };

            string lower = input.ToLower();
            foreach (string keyword in keywords)
            {
                if (lower.Contains(keyword))
                    return true;
            }
            return false;
        }

        public static string GetCybersecurityFact(string input)
        {
            // Return a random cybersecurity fact based on detected keyword
            string lower = input.ToLower();

            if (lower.Contains("password"))
                return "🔐 Tip: Use a password manager and enable two-factor authentication for all your accounts!";
            if (lower.Contains("phishing"))
                return "🎣 Be careful: Always verify the sender's email address before clicking on links!";
            if (lower.Contains("malware") || lower.Contains("ransomware"))
                return "🛡️ Keep your antivirus software updated and don't download files from untrusted sources!";
            if (lower.Contains("privacy"))
                return "👁️ Regularly review your privacy settings on social media and other online platforms!";

            return "💡 Remember: Cybersecurity is everyone's responsibility. Stay safe online!";
        }
    }
}