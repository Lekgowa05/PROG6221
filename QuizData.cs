using System.Collections.Generic;

namespace CybersecurityChatbot.Classes
{
    public static class QuizData
    {
        public static List<QuizQuestion> GetQuestions()
        {
            return new List<QuizQuestion>
            {
                // 1. Phishing
                new QuizQuestion
                {
                    Question = "What is a common sign of a phishing email?",
                    Options = new string[] {
                        "It uses your full name correctly",
                        "It has spelling and grammar mistakes",
                        "It comes from a trusted sender",
                        "It has attachments from a known source"
                    },
                    CorrectIndex = 1,
                    Category = "Phishing",
                    Explanation = "Phishing emails often contain spelling and grammar mistakes, and they try to create urgency."
                },
                // 2. Passwords
                new QuizQuestion
                {
                    Question = "What is considered a strong password practice?",
                    Options = new string[] {
                        "Using your birthday",
                        "Using the same password for all accounts",
                        "Using a mix of uppercase, lowercase, numbers, and special characters",
                        "Using your pet's name"
                    },
                    CorrectIndex = 2,
                    Category = "Password Safety",
                    Explanation = "A strong password uses a combination of character types and is at least 12 characters long."
                },
                // 3. Two-Factor Authentication
                new QuizQuestion
                {
                    Question = "What is Two-Factor Authentication (2FA)?",
                    Options = new string[] {
                        "A password manager",
                        "A security method requiring two different verification methods",
                        "A type of antivirus software",
                        "A firewall configuration"
                    },
                    CorrectIndex = 1,
                    Category = "Authentication",
                    Explanation = "2FA adds an extra layer of security by requiring two different verification methods (e.g., password + SMS code)."
                },
                // 4. Social Engineering (True/False)
                new QuizQuestion
                {
                    Question = "Social engineering attacks rely on human psychology rather than technical hacking.",
                    Options = new string[] { "True", "False" },
                    CorrectIndex = 0,
                    Category = "Social Engineering",
                    Explanation = "True. Social engineering manipulates people into divulging confidential information."
                },
                // 5. Malware
                new QuizQuestion
                {
                    Question = "Which of these is NOT a type of malware?",
                    Options = new string[] {
                        "Virus",
                        "Ransomware",
                        "Firewall",
                        "Trojan"
                    },
                    CorrectIndex = 2,
                    Category = "Malware",
                    Explanation = "A firewall is a security tool that monitors network traffic, not a type of malware."
                },
                // 6. Safe Browsing
                new QuizQuestion
                {
                    Question = "When visiting a website, how can you check if your connection is secure?",
                    Options = new string[] {
                        "Look for a green address bar",
                        "Check for 'https://' and a padlock icon",
                        "Look for a 'secure' badge",
                        "All of the above"
                    },
                    CorrectIndex = 3,
                    Category = "Safe Browsing",
                    Explanation = "All of these indicators can help verify a secure connection."
                },
                // 7. Password Reuse (True/False)
                new QuizQuestion
                {
                    Question = "Using the same password for multiple accounts is safe if the password is strong.",
                    Options = new string[] { "True", "False" },
                    CorrectIndex = 1,
                    Category = "Password Safety",
                    Explanation = "False. If one account is compromised, all accounts using the same password are at risk."
                },
                // 8. Ransomware
                new QuizQuestion
                {
                    Question = "What does ransomware do?",
                    Options = new string[] {
                        "Encrypts your files and demands payment",
                        "Deletes all your files",
                        "Steals your passwords",
                        "Sends spam emails from your account"
                    },
                    CorrectIndex = 0,
                    Category = "Malware",
                    Explanation = "Ransomware encrypts files and demands a ransom payment to unlock them."
                },
                // 9. Public Wi-Fi
                new QuizQuestion
                {
                    Question = "What is the safest way to use public Wi-Fi?",
                    Options = new string[] {
                        "Use a VPN",
                        "Disable your antivirus",
                        "Share your location",
                        "Keep your files open to share"
                    },
                    CorrectIndex = 0,
                    Category = "Safe Browsing",
                    Explanation = "Using a VPN encrypts your data, making it safe to use public Wi-Fi."
                },
                // 10. Phishing (True/False)
                new QuizQuestion
                {
                    Question = "Phishing attacks only happen via email.",
                    Options = new string[] { "True", "False" },
                    CorrectIndex = 1,
                    Category = "Phishing",
                    Explanation = "False. Phishing can also occur via SMS (smishing), phone calls (vishing), and social media."
                },
                // 11. Backups
                new QuizQuestion
                {
                    Question = "What is the 3-2-1 backup rule?",
                    Options = new string[] {
                        "3 copies, 2 different media, 1 offsite location",
                        "3 devices, 2 users, 1 backup",
                        "3 passwords, 2-factor auth, 1 day to remember",
                        "None of the above"
                    },
                    CorrectIndex = 0,
                    Category = "Data Protection",
                    Explanation = "The 3-2-1 backup rule: 3 copies of data, 2 different storage media, 1 copy offsite."
                },
                // 12. Firewall
                new QuizQuestion
                {
                    Question = "What is the purpose of a firewall?",
                    Options = new string[] {
                        "To block viruses",
                        "To monitor and control network traffic",
                        "To encrypt emails",
                        "To store passwords"
                    },
                    CorrectIndex = 1,
                    Category = "Network Security",
                    Explanation = "A firewall monitors and controls incoming and outgoing network traffic based on security rules."
                }
            };
        }
    }
}