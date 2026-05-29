using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    public class CyberSecurityBot
    {
        private Dictionary<string, List<string>> knowledgeBase;

        public CyberSecurityBot()
        {
            InitializeKnowledgeBase();
        }

        private void InitializeKnowledgeBase()
        {
            knowledgeBase = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                // Encryption
                ["encryption"] = new List<string> { "aes", "rsa", "encrypt", "decrypt", "cipher" },
                ["what is encryption"] = new List<string> { "encryption", "encrypt" },
                ["aes"] = new List<string> { "aes", "advanced encryption standard" },
                ["rsa"] = new List<string> { "rsa", "rivest-shamir-adleman" },

                // Malware
                ["malware"] = new List<string> { "malware", "virus", "trojan", "ransomware", "worm" },
                ["virus"] = new List<string> { "virus", "malware" },
                ["ransomware"] = new List<string> { "ransomware", "ransom", "cryptolocker" },
                ["trojan"] = new List<string> { "trojan", "trojan horse" },

                // Firewall & Network Security
                ["firewall"] = new List<string> { "firewall", "network security", "packet filter" },
                ["vpn"] = new List<string> { "vpn", "virtual private network", "secure tunnel" },

                // Password Security
                ["password"] = new List<string> { "password", "passphrase", "password manager" },
                ["2fa"] = new List<string> { "2fa", "two factor authentication", "multi factor", "mfa" },

                // Phishing
                ["phishing"] = new List<string> { "phishing", "spear phishing", "email scam" },

                // General Security
                ["best practices"] = new List<string> { "best practices", "security tips", "cybersecurity tips" },
                ["https"] = new List<string> { "https", "ssl", "tls", "secure connection" },
                ["ddos"] = new List<string> { "ddos", "distributed denial of service" }
            };
        }

        public string GetResponse(string question)
        {
            question = question.ToLower().Trim();

            // Check for specific keywords
            if (ContainsKeyword(question, new[] { "encryption", "aes", "rsa", "encrypt" }))
                return GetEncryptionAnswer(question);

            if (ContainsKeyword(question, new[] { "malware", "virus", "ransomware", "trojan", "worm" }))
                return GetMalwareAnswer(question);

            if (ContainsKeyword(question, new[] { "firewall", "network security" }))
                return GetFirewallAnswer();

            if (ContainsKeyword(question, new[] { "vpn", "virtual private network" }))
                return GetVPNAnswer();

            if (ContainsKeyword(question, new[] { "password", "passphrase" }))
                return GetPasswordAnswer();

            if (ContainsKeyword(question, new[] { "2fa", "two factor", "mfa", "multi factor" }))
                return Get2FAAnswer();

            if (ContainsKeyword(question, new[] { "phishing", "email scam" }))
                return GetPhishingAnswer();

            if (ContainsKeyword(question, new[] { "best practices", "tips" }))
                return GetBestPracticesAnswer();

            if (ContainsKeyword(question, new[] { "https", "ssl", "tls" }))
                return GetHTTPSAnswer();

            if (ContainsKeyword(question, new[] { "ddos", "dos attack" }))
                return GetDDoSAnswer();

            return GetDefaultAnswer();
        }

        private bool ContainsKeyword(string text, string[] keywords)
        {
            return keywords.Any(keyword => text.Contains(keyword));
        }

        private string GetEncryptionAnswer(string question)
        {
            if (question.Contains("aes"))
                return "AES (Advanced Encryption Standard) is a symmetric encryption algorithm widely used worldwide. It's secure, fast, and comes in key sizes of 128, 192, or 256 bits. AES-256 is considered military-grade encryption.";

            if (question.Contains("rsa"))
                return "RSA is an asymmetric encryption algorithm used for secure data transmission. It uses public and private keys. While slower than AES, it's excellent for digital signatures and key exchange.";

            return "Encryption converts data into a coded format to prevent unauthorized access. There are two main types: symmetric (same key for encryption/decryption) like AES, and asymmetric (public/private key pairs) like RSA. Always use strong, industry-standard encryption algorithms.";
        }

        private string GetMalwareAnswer(string question)
        {
            if (question.Contains("ransomware"))
                return "Ransomware is malware that encrypts your files and demands payment for decryption. Prevention: regular backups, keep systems updated, use strong antivirus, and be cautious with email attachments. Never pay the ransom!";

            if (question.Contains("trojan"))
                return "A Trojan horse is malware disguised as legitimate software. Unlike viruses, they don't self-replicate. Protection: download only from official sources, use antivirus software, and verify file signatures.";

            return "Malware (Malicious Software) includes viruses, worms, trojans, ransomware, and spyware. Best protection: use reputable antivirus, keep software updated, avoid suspicious downloads, and practice safe browsing habits.";
        }

        private string GetFirewallAnswer()
        {
            return "A firewall monitors and controls incoming/outgoing network traffic based on security rules. Types include: packet-filtering, stateful inspection, and next-generation firewalls (NGFW). Always keep your firewall enabled, whether hardware or software-based.";
        }

        private string GetVPNAnswer()
        {
            return "VPN (Virtual Private Network) creates an encrypted tunnel for your internet traffic, hiding your IP address and protecting data from eavesdropping. Use reputable VPN services with no-log policies, strong encryption (AES-256), and kill-switch features.";
        }

        private string GetPasswordAnswer()
        {
            return "Password Security Best Practices:\n• Use long passphrases (12+ characters)\n• Unique passwords for each account\n• Use a password manager\n• Enable 2FA whenever possible\n• Avoid dictionary words and personal info\n• Change passwords only when compromised (not arbitrarily)";
        }

        private string Get2FAAnswer()
        {
            return "Two-Factor Authentication (2FA) adds an extra security layer. Types include: SMS codes, authenticator apps (Google Authenticator, Authy), hardware tokens (YubiKey), and biometrics. Always prefer app-based or hardware tokens over SMS when available.";
        }

        private string GetPhishingAnswer()
        {
            return "Phishing attacks trick you into revealing sensitive info. Warning signs:\n• Urgent/ threatening language\n• Spelling/grammar errors\n• Suspicious links/attachments\n• Requests for personal data\n• Mismatched email addresses\n\nNever click suspicious links—verify through official channels.";
        }

        private string GetBestPracticesAnswer()
        {
            return "Cybersecurity Best Practices:\n✅ Keep software updated\n✅ Use strong, unique passwords + password manager\n✅ Enable 2FA everywhere\n✅ Backup important data regularly (3-2-1 rule)\n✅ Be cautious with emails/links\n✅ Use antivirus and firewall\n✅ Encrypt sensitive data\n✅ Use VPN on public Wi-Fi";
        }

        private string GetHTTPSAnswer()
        {
            return "HTTPS (HTTP Secure) uses SSL/TLS encryption to secure communication between browser and website. Look for the padlock icon in address bar. Never enter sensitive info on HTTP sites. TLS 1.3 is the current standard—avoid sites using outdated TLS 1.0/1.1.";
        }

        private string GetDDoSAnswer()
        {
            return "DDoS (Distributed Denial of Service) attacks flood a target with traffic to disrupt services. Protection includes: using DDoS mitigation services (Cloudflare, AWS Shield), rate limiting, web application firewalls (WAF), and having redundant infrastructure.";
        }

        private string GetDefaultAnswer()
        {
            return "I can help with cybersecurity topics like:\n• Encryption (AES, RSA)\n• Malware (viruses, ransomware)\n• Firewalls & Network Security\n• Password Security & 2FA\n• Phishing attacks\n• VPNs & HTTPS\n• Security best practices\n\nPlease ask a specific question about any of these areas!";
        }
    }
}