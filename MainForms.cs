using CybersecurityChatbot.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    public partial class MainForm : Form
    {
        private DatabaseHelper dbHelper;
        private int currentQuizIndex;
        private int quizScore;
        private List<QuizQuestion> quizQuestions;

        public MainForm()
        {
            InitializeComponent();
            InitializeChatbot();
        }

        private void InitializeChatbot()
        {
            dbHelper = new DatabaseHelper();

            // Check database connection
            if (!dbHelper.TestConnection())
            {
                MessageBox.Show("⚠️ Database connection failed! Please check your MySQL server.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Initialize quiz
            quizQuestions = QuizData.GetQuestions();
            currentQuizIndex = 0;
            quizScore = 0;

            // Load tasks
            LoadTasks();

            // Initial greeting
            AppendChat("Bot", "👋 Hello! I'm your Cybersecurity Awareness Assistant.");
            AppendChat("Bot", "I can help you with:");
            AppendChat("Bot", "• 📝 Manage cybersecurity tasks");
            AppendChat("Bot", "• 🎮 Test your knowledge with quizzes");
            AppendChat("Bot", "• 🔍 Answer cybersecurity questions");
            AppendChat("Bot", "• 📊 Show activity log");
            AppendChat("Bot", "Try saying: 'Add task', 'Show tasks', or 'Start quiz'!");

            LogActivity("Chatbot initialized successfully.");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
                return;

            // Show user message
            AppendChat("You", userInput);
            LogActivity($"User: {userInput}");

            // Process input
            string response = ProcessUserInput(userInput);

            // Show bot response
            AppendChat("Bot", response);
            LogActivity($"Bot: {response}");

            txtInput.Clear();
            txtInput.Focus();

            // Switch to appropriate tab if needed
            var intent = NLPEngine.DetectIntent(userInput);
            if (intent == NLPEngine.Intent.Quiz)
            {
                tabControl1.SelectedTab = tabPageGame;
            }
            else if (intent == NLPEngine.Intent.ShowTasks ||
                     intent == NLPEngine.Intent.AddTask ||
                     intent == NLPEngine.Intent.DeleteTask ||
                     intent == NLPEngine.Intent.CompleteTask)
            {
                tabControl1.SelectedTab = tabPageTasks;
            }
        }

        private string ProcessUserInput(string input)
        {
            var intent = NLPEngine.DetectIntent(input);

            switch (intent)
            {
                case NLPEngine.Intent.Greeting:
                    return "👋 Hello! How can I help you with cybersecurity today?";

                case NLPEngine.Intent.AddTask:
                    return HandleAddTask(input);

                case NLPEngine.Intent.ShowTasks:
                    return HandleShowTasks();

                case NLPEngine.Intent.DeleteTask:
                    return HandleDeleteTask(input);

                case NLPEngine.Intent.CompleteTask:
                    return HandleCompleteTask(input);

                case NLPEngine.Intent.Quiz:
                    return HandleQuizRequest();

                case NLPEngine.Intent.Help:
                    return GetHelpMessage();

                default:
                    return HandleDefaultResponse(input);
            }
        }

        private string HandleAddTask(string input)
        {
            string title = NLPEngine.ExtractTaskDetails(input, out string taskTitle, out string description, out DateTime? reminderDate);

            if (string.IsNullOrEmpty(taskTitle))
                return "❌ I didn't understand the task. Please say: 'Add task [your task title]'";

            try
            {
                dbHelper.AddTask(taskTitle, description, reminderDate);
                LoadTasks();
                return $"✅ Task '{taskTitle}' added successfully! {(reminderDate.HasValue ? $"Reminder set for {reminderDate.Value.ToShortDateString()}" : "No reminder set.")}";
            }
            catch (Exception ex)
            {
                LogActivity($"Error adding task: {ex.Message}");
                return $"❌ Error adding task: {ex.Message}";
            }
        }

        private string HandleShowTasks()
        {
            var tasks = dbHelper.GetTasks();
            if (tasks.Rows.Count == 0)
                return "📋 You have no tasks. Add a task to get started!";

            LoadTasks();
            return $"📋 I've loaded your tasks ({tasks.Rows.Count} total). Check the Task Assistant tab!";
        }

        private string HandleDeleteTask(string input)
        {
            string identifier = NLPEngine.ExtractTaskIdentifier(input);
            if (string.IsNullOrEmpty(identifier))
                return "❌ Please specify which task to delete (by ID or title).";

            try
            {
                if (int.TryParse(identifier, out int taskId))
                {
                    dbHelper.DeleteTask(taskId);
                    LoadTasks();
                    return $"✅ Task #{taskId} deleted successfully!";
                }
                else
                {
                    // Find task by title
                    var tasks = dbHelper.GetTasks();
                    foreach (DataRow row in tasks.Rows)
                    {
                        if (row["title"].ToString().ToLower().Contains(identifier.ToLower()))
                        {
                            int id = Convert.ToInt32(row["id"]);
                            dbHelper.DeleteTask(id);
                            LoadTasks();
                            return $"✅ Task '{row["title"]}' deleted successfully!";
                        }
                    }
                    return $"❌ Task '{identifier}' not found.";
                }
            }
            catch (Exception ex)
            {
                return $"❌ Error deleting task: {ex.Message}";
            }
        }

        private string HandleCompleteTask(string input)
        {
            string identifier = NLPEngine.ExtractTaskIdentifier(input);
            if (string.IsNullOrEmpty(identifier))
                return "❌ Please specify which task to mark as completed.";

            try
            {
                if (int.TryParse(identifier, out int taskId))
                {
                    dbHelper.UpdateTaskStatus(taskId, true);
                    LoadTasks();
                    return $"✅ Task #{taskId} marked as completed!";
                }
                else
                {
                    var tasks = dbHelper.GetTasks();
                    foreach (DataRow row in tasks.Rows)
                    {
                        if (row["title"].ToString().ToLower().Contains(identifier.ToLower()))
                        {
                            int id = Convert.ToInt32(row["id"]);
                            dbHelper.UpdateTaskStatus(id, true);
                            LoadTasks();
                            return $"✅ Task '{row["title"]}' marked as completed!";
                        }
                    }
                    return $"❌ Task '{identifier}' not found.";
                }
            }
            catch (Exception ex)
            {
                return $"❌ Error completing task: {ex.Message}";
            }
        }

        private string HandleQuizRequest()
        {
            if (quizQuestions == null || quizQuestions.Count == 0)
                return "❌ No quiz questions available. Please try again later.";

            currentQuizIndex = 0;
            quizScore = 0;
            DisplayQuestion();
            return "🎮 Let's start the Cybersecurity Quiz! Answer the questions in the Game tab.";
        }

        private string HandleDefaultResponse(string input)
        {
            // Check for cybersecurity keywords
            if (NLPEngine.ContainsCybersecurityKeyword(input))
            {
                return NLPEngine.GetCybersecurityFact(input);
            }

            // Check if it's a question
            if (input.EndsWith("?"))
                return "🤔 That's a good question! I'm still learning, but I can help with tasks, quizzes, and cybersecurity facts. Try saying 'Add task' or 'Start quiz'!";

            return "🤖 I'm not sure I understand. I can help you manage tasks, take quizzes, or provide cybersecurity tips. Try saying 'Help' to see what I can do!";
        }

        private string GetHelpMessage()
        {
            return @"
🆘 **Here's what I can do:**

📝 **Task Management:**
• 'Add task [title]' - Add a new task
• 'Show tasks' - View all tasks
• 'Delete task [id/title]' - Remove a task
• 'Complete task [id/title]' - Mark as done

🎮 **Quiz:**
• 'Start quiz' - Begin cybersecurity quiz
• 'Play game' - Same as quiz

🔍 **Cybersecurity Tips:**
• Mention 'password', 'phishing', or 'malware' for tips

📊 **Other:**
• 'Help' - Show this message
• 'Activity log' - View chatbot actions

Feel free to ask in natural language!";
        }

        private void AppendChat(string sender, string message)
        {
            // Use Invoke for thread safety (if needed)
            if (rtbChatDisplay.InvokeRequired)
            {
                rtbChatDisplay.Invoke(new Action(() => AppendChat(sender, message)));
                return;
            }

            string prefix = sender == "Bot" ? "🤖" : "👤";
            string formattedMessage = $"\n{prefix} [{sender}] {DateTime.Now:HH:mm:ss}\n{message}\n";

            rtbChatDisplay.SelectionColor = sender == "Bot" ? Color.DarkGreen : Color.DarkBlue;
            rtbChatDisplay.AppendText(formattedMessage);
            rtbChatDisplay.ScrollToCaret();

            // Auto-log chat messages
            LogActivity($"{sender}: {message}");
        }

        public void LogActivity(string action)
        {
            if (lvActivityLog.InvokeRequired)
            {
                lvActivityLog.Invoke(new Action(() => LogActivity(action)));
                return;
            }

            ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
            item.SubItems.Add(action);
            lvActivityLog.Items.Insert(0, item); // Show latest first

            // Keep only last 1000 entries to prevent memory issues
            while (lvActivityLog.Items.Count > 1000)
                lvActivityLog.Items.RemoveAt(lvActivityLog.Items.Count - 1);
        }

        private void LoadTasks()
        {
            try
            {
                DataTable tasks = dbHelper.GetTasks();
                dgvTasks.DataSource = tasks;

                // Format columns
                if (dgvTasks.Columns.Count > 0)
                {
                    dgvTasks.Columns["id"].HeaderText = "ID";
                    dgvTasks.Columns["id"].Width = 50;
                    dgvTasks.Columns["title"].HeaderText = "Task Title";
                    dgvTasks.Columns["title"].Width = 200;
                    dgvTasks.Columns["description"].HeaderText = "Description";
                    dgvTasks.Columns["description"].Width = 250;
                    dgvTasks.Columns["reminder_date"].HeaderText = "Reminder Date";
                    dgvTasks.Columns["reminder_date"].Width = 120;
                    dgvTasks.Columns["status"].HeaderText = "Status";
                    dgvTasks.Columns["status"].Width = 100;
                }

                lblTaskCount.Text = $"Total Tasks: {tasks.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogActivity($"Error loading tasks: {ex.Message}");
            }
        }

        // Quiz Methods
        private void DisplayQuestion()
        {
            if (currentQuizIndex >= quizQuestions.Count)
            {
                ShowQuizResult();
                return;
            }

            var question = quizQuestions[currentQuizIndex];
            lblQuestion.Text = $"Q{currentQuizIndex + 1}/{quizQuestions.Count}: {question.Question}";
            lblQuestionCategory.Text = $"Category: {question.Category} | Type: {question.GetQuestionType()}";

            // Populate options
            for (int i = 0; i < 4; i++)
            {
                var radioButton = GetRadioButton(i);
                if (i < question.Options.Length)
                {
                    radioButton.Text = question.Options[i];
                    radioButton.Visible = true;
                }
                else
                {
                    radioButton.Visible = false;
                }
                radioButton.Checked = false;
                radioButton.BackColor = Color.Transparent; // Reset color
            }

            btnSubmitAnswer.Enabled = true;
            lblFeedback.Text = string.Empty;
        }

        private RadioButton GetRadioButton(int index)
        {
            switch (index)
            {
                case 0: return rbOption1;
                case 1: return rbOption2;
                case 2: return rbOption3;
                case 3: return rbOption4;
                default: return null;
            }
        }

        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (currentQuizIndex >= quizQuestions.Count)
                return;

            // Check which option is selected
            int selectedIndex = -1;
            for (int i = 0; i < 4; i++)
            {
                var rb = GetRadioButton(i);
                if (rb.Checked && rb.Visible)
                {
                    selectedIndex = i;
                    break;
                }
            }

            if (selectedIndex == -1)
            {
                lblFeedback.Text = "⚠️ Please select an answer first!";
                lblFeedback.ForeColor = Color.Orange;
                return;
            }

            var question = quizQuestions[currentQuizIndex];
            bool isCorrect = (selectedIndex == question.CorrectIndex);

            // Highlight correct/incorrect answer
            for (int i = 0; i < 4; i++)
            {
                var rb = GetRadioButton(i);
                if (!rb.Visible) continue;

                if (i == question.CorrectIndex)
                    rb.BackColor = Color.LightGreen;
                else if (i == selectedIndex && !isCorrect)
                    rb.BackColor = Color.LightPink;
            }

            if (isCorrect)
            {
                quizScore++;
                lblFeedback.Text = "✅ Correct! " + (question.Explanation ?? "Great job!");
                lblFeedback.ForeColor = Color.Green;
            }
            else
            {
                lblFeedback.Text = "❌ Incorrect. The correct answer was: " + question.Options[question.CorrectIndex] +
                    (question.Explanation != null ? "\n" + question.Explanation : "");
                lblFeedback.ForeColor = Color.Red;
            }

            lblScore.Text = $"Score: {quizScore}/{quizQuestions.Count}";
            btnSubmitAnswer.Enabled = false;

            // Move to next question after delay
            if (currentQuizIndex < quizQuestions.Count - 1)
            {
                Timer timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (s, ev) =>
                {
                    timer.Stop();
                    currentQuizIndex++;
                    DisplayQuestion();
                    LogActivity($"Quiz: Moved to question {currentQuizIndex + 1}");
                };
                timer.Start();
            }
            else
            {
                // Last question - show results after delay
                Timer timer = new Timer();
                timer.Interval = 3000;
                timer.Tick += (s, ev) =>
                {
                    timer.Stop();
                    ShowQuizResult();
                };
                timer.Start();
            }
        }

        private void ShowQuizResult()
        {
            lblQuestion.Text = "🏆 Quiz Complete!";
            lblQuestionCategory.Text = string.Empty;

            for (int i = 0; i < 4; i++)
                GetRadioButton(i).Visible = false;

            btnSubmitAnswer.Enabled = false;

            string message = "";
            double percentage = (double)quizScore / quizQuestions.Count * 100;

            if (percentage >= 80)
                message = "🌟 Excellent! You're a cybersecurity expert!";
            else if (percentage >= 60)
                message = "👍 Good job! Keep learning to improve!";
            else if (percentage >= 40)
                message = "📚 Not bad! Review the topics and try again.";
            else
                message = "💪 Keep studying! Cybersecurity is important.";

            lblFeedback.Text = $"{message}\n\nFinal Score: {quizScore}/{quizQuestions.Count} ({percentage:F0}%)";
            lblFeedback.ForeColor = Color.DarkBlue;
            lblScore.Text = $"Final Score: {quizScore}/{quizQuestions.Count}";

            LogActivity($"Quiz completed. Score: {quizScore}/{quizQuestions.Count} ({percentage:F0}%)");
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            quizQuestions = QuizData.GetQuestions();
            currentQuizIndex = 0;
            quizScore = 0;
            lblScore.Text = "Score: 0/0";
            DisplayQuestion();
            LogActivity("Quiz started");
        }

        // Task management button handlers
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTaskTitle.Text.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Please enter a task title.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dbHelper.AddTask(title, txtTaskDescription.Text.Trim(), dtpReminder.Value);
                LoadTasks();
                txtTaskTitle.Clear();
                txtTaskDescription.Clear();

                MessageBox.Show("✅ Task added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogActivity($"Added task: {title}");
                AppendChat("Bot", $"✅ Task '{title}' added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogActivity($"Error adding task: {ex.Message}");
            }
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["id"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["title"].Value.ToString();

            DialogResult result = MessageBox.Show($"Delete task '{title}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    dbHelper.DeleteTask(taskId);
                    LoadTasks();
                    MessageBox.Show("✅ Task deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogActivity($"Deleted task: {title}");
                    AppendChat("Bot", $"🗑️ Task '{title}' deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting task: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogActivity($"Error deleting task: {ex.Message}");
                }
            }
        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to complete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["id"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["title"].Value.ToString();

            try
            {
                dbHelper.UpdateTaskStatus(taskId, true);
                LoadTasks();
                MessageBox.Show("✅ Task marked as completed!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogActivity($"Completed task: {title}");
                AppendChat("Bot", $"✅ Task '{title}' marked as completed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing task: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogActivity($"Error completing task: {ex.Message}");
            }
        }

        // Log activity when form loads
        private void MainForm_Load(object sender, EventArgs e)
        {
            LogActivity("Application started");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogActivity("Application closed");
        }
    }
}