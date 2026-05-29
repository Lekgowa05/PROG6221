using System;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    public partial class Form1 : Form
    {
        private ListBox chatHistory;
        private TextBox questionInput;
        private Button sendButton;
        private Label titleLabel;
        private CyberSecurityBot bot;

        public Form1()
        {
            InitializeComponent();
            bot = new CyberSecurityBot();
            SetupChatHistory();
            AddWelcomeMessage();
        }

        private void InitializeComponent()
        {
            this.chatHistory = new ListBox();
            this.questionInput = new TextBox();
            this.sendButton = new Button();
            this.titleLabel = new Label();
            this.SuspendLayout();

            // titleLabel
            this.titleLabel.Text = "🤖 Cybersecurity Assistant Chatbot";
            this.titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.FromArgb(0, 120, 215);
            this.titleLabel.Dock = DockStyle.Top;
            this.titleLabel.Height = 50;
            this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.titleLabel.BackColor = Color.FromArgb(240, 248, 255);

            // chatHistory
            this.chatHistory.Dock = DockStyle.Fill;
            this.chatHistory.Font = new Font("Consolas", 10);
            this.chatHistory.BackColor = Color.White;
            this.chatHistory.ForeColor = Color.Black;
            this.chatHistory.IntegralHeight = false;
            this.chatHistory.DrawMode = DrawMode.OwnerDrawVariable;
            this.chatHistory.MeasureItem += ChatHistory_MeasureItem;
            this.chatHistory.DrawItem += ChatHistory_DrawItem;

            // questionInput
            this.questionInput.Dock = DockStyle.Bottom;
            this.questionInput.Font = new Font("Segoe UI", 11);
            this.questionInput.Height = 40;
            this.questionInput.PlaceholderText = "Type your cybersecurity question here...";
            this.questionInput.KeyPress += QuestionInput_KeyPress;

            // sendButton
            this.sendButton.Dock = DockStyle.Bottom;
            this.sendButton.Text = "Send Question ➤";
            this.sendButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.sendButton.BackColor = Color.FromArgb(0, 120, 215);
            this.sendButton.ForeColor = Color.White;
            this.sendButton.FlatStyle = FlatStyle.Flat;
            this.sendButton.Height = 40;
            this.sendButton.Click += SendButton_Click;

            // Form
            this.Text = "Cybersecurity Chatbot";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.MinimumSize = new Size(500, 400);

            // Add controls
            this.Controls.Add(this.chatHistory);
            this.Controls.Add(this.questionInput);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.titleLabel);

            this.ResumeLayout(false);
        }

        private void SetupChatHistory()
        {
            chatHistory.DrawMode = DrawMode.OwnerDrawVariable;
            chatHistory.BackColor = Color.White;
            chatHistory.ForeColor = Color.Black;
            chatHistory.Font = new Font("Segoe UI", 10);
        }

        private void ChatHistory_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index >= 0 && e.Index < chatHistory.Items.Count)
            {
                string text = chatHistory.Items[e.Index].ToString();
                Size textSize = TextRenderer.MeasureText(text, chatHistory.Font);
                e.ItemHeight = textSize.Height + 10;
            }
            else
            {
                e.ItemHeight = 30;
            }
        }

        private void ChatHistory_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= chatHistory.Items.Count) return;

            string message = chatHistory.Items[e.Index].ToString();
            e.DrawBackground();

            bool isUser = message.StartsWith("You:");
            Color textColor = isUser ? Color.FromArgb(0, 102, 204) : Color.FromArgb(0, 128, 0);

            using (Brush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(message, chatHistory.Font, textBrush, e.Bounds.X + 5, e.Bounds.Y + 5);
            }

            e.DrawFocusRectangle();
        }

        private void AddWelcomeMessage()
        {
            string welcome = "🤖 Bot: Hello! I'm your Cybersecurity Assistant.\n\n" +
                            "I can answer questions about:\n" +
                            "• Encryption & Cryptography\n" +
                            "• Malware Types & Prevention\n" +
                            "• Network Security & Firewalls\n" +
                            "• Password Security\n" +
                            "• Phishing Attacks\n" +
                            "• Two-Factor Authentication (2FA)\n" +
                            "• VPNs & Secure Communication\n" +
                            "• Security Best Practices\n\n" +
                            "Ask me anything about cybersecurity!";
            chatHistory.Items.Add(welcome);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            ProcessQuestion();
        }

        private void QuestionInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProcessQuestion();
                e.Handled = true;
            }
        }

        private void ProcessQuestion()
        {
            string question = questionInput.Text.Trim();
            if (string.IsNullOrEmpty(question))
            {
                MessageBox.Show("Please enter a question.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add user question to chat
            chatHistory.Items.Add($"👤 You: {question}");

            // Get bot response
            string response = bot.GetResponse(question);
            chatHistory.Items.Add($"🤖 Bot: {response}");

            // Clear input and scroll to bottom
            questionInput.Clear();
            chatHistory.TopIndex = chatHistory.Items.Count - 1;
        }
    }
}