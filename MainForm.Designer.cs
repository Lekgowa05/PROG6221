namespace CybersecurityChatbot
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageChat;
        private System.Windows.Forms.TabPage tabPageTasks;
        private System.Windows.Forms.TabPage tabPageGame;
        private System.Windows.Forms.TabPage tabPageLog;
        
        // Chat tab controls
        private System.Windows.Forms.RichTextBox rtbChatDisplay;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSend;
        
        // Task tab controls
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.TextBox txtTaskDescription;
        private System.Windows.Forms.DateTimePicker dtpReminder;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnCompleteTask;
        private System.Windows.Forms.Label lblTaskCount;
        
        // Quiz tab controls
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblQuestionCategory;
        private System.Windows.Forms.RadioButton rbOption1;
        private System.Windows.Forms.RadioButton rbOption2;
        private System.Windows.Forms.RadioButton rbOption3;
        private System.Windows.Forms.RadioButton rbOption4;
        private System.Windows.Forms.Button btnSubmitAnswer;
        private System.Windows.Forms.Button btnStartQuiz;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblFeedback;
        
        // Activity log controls
        private System.Windows.Forms.ListView lvActivityLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Set form properties
            this.Text = "🛡️ Cybersecurity Awareness Chatbot";
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            this.Icon = Properties.Resources.security_icon;

            // Create TabControl
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Initialize tabs
            InitializeChatTab();
            InitializeTaskTab();
            InitializeGameTab();
            InitializeLogTab();

            // Add tabs to control
            this.tabControl1.TabPages.Add(tabPageChat);
            this.tabControl1.TabPages.Add(tabPageTasks);
            this.tabControl1.TabPages.Add(tabPageGame);
            this.tabControl1.TabPages.Add(tabPageLog);

            // Add to form
            this.Controls.Add(this.tabControl1);
        }

        private void InitializeChatTab()
        {
            this.tabPageChat = new System.Windows.Forms.TabPage("💬 Chat");
            this.tabPageChat.BackColor = System.Drawing.Color.WhiteSmoke;

            // Chat display
            this.rtbChatDisplay = new System.Windows.Forms.RichTextBox();
            this.rtbChatDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChatDisplay.ReadOnly = true;
            this.rtbChatDisplay.BackColor = System.Drawing.Color.White;
            this.rtbChatDisplay.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rtbChatDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Input panel (bottom)
            System.Windows.Forms.Panel panelInput = new System.Windows.Forms.Panel();
            panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelInput.Height = 60;
            panelInput.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtInput.Location = new System.Drawing.Point(10, 15);
            this.txtInput.Size = new System.Drawing.Size(800, 30);
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtInput.PlaceholderText = "Type your message here...";

            this.btnSend = new System.Windows.Forms.Button();
            this.btnSend.Location = new System.Drawing.Point(820, 12);
            this.btnSend.Size = new System.Drawing.Size(100, 35);
            this.btnSend.Text = "Send ➤";
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // Add controls to panel
            panelInput.Controls.Add(this.txtInput);
            panelInput.Controls.Add(this.btnSend);

            // Add controls to tab
            this.tabPageChat.Controls.Add(this.rtbChatDisplay);
            this.tabPageChat.Controls.Add(panelInput);
        }

        private void InitializeTaskTab()
        {
            this.tabPageTasks = new System.Windows.Forms.TabPage("📝 Task Assistant");
            this.tabPageTasks.BackColor = System.Drawing.Color.WhiteSmoke;

            // Main layout: Split into top (input) and bottom (list)
            System.Windows.Forms.SplitContainer splitContainer = new System.Windows.Forms.SplitContainer();
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitContainer.SplitterDistance = 150;

            // === TOP PANEL - Add Task ===
            System.Windows.Forms.Panel panelAddTask = new System.Windows.Forms.Panel();
            panelAddTask.Dock = System.Windows.Forms.DockStyle.Fill;
            panelAddTask.Padding = new System.Windows.Forms.Padding(10);

            // Title label and textbox
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = "Task Title:";
            lblTitle.Location = new System.Drawing.Point(10, 15);
            lblTitle.Size = new System.Drawing.Size(100, 25);

            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.txtTaskTitle.Location = new System.Drawing.Point(120, 12);
            this.txtTaskTitle.Size = new System.Drawing.Size(300, 25);

            // Description label and textbox
            System.Windows.Forms.Label lblDesc = new System.Windows.Forms.Label();
            lblDesc.Text = "Description:";
            lblDesc.Location = new System.Drawing.Point(10, 50);
            lblDesc.Size = new System.Drawing.Size(100, 25);

            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.txtTaskDescription.Location = new System.Drawing.Point(120, 47);
            this.txtTaskDescription.Size = new System.Drawing.Size(300, 25);

            // Reminder date
            System.Windows.Forms.Label lblReminder = new System.Windows.Forms.Label();
            lblReminder.Text = "Reminder Date:";
            lblReminder.Location = new System.Drawing.Point(10, 85);
            lblReminder.Size = new System.Drawing.Size(100, 25);

            this.dtpReminder = new System.Windows.Forms.DateTimePicker();
            this.dtpReminder.Location = new System.Drawing.Point(120, 82);
            this.dtpReminder.Size = new System.Drawing.Size(200, 25);
            this.dtpReminder.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // Add button
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnAddTask.Text = "➕ Add Task";
            this.btnAddTask.Location = new System.Drawing.Point(350, 80);
            this.btnAddTask.Size = new System.Drawing.Size(120, 30);
            this.btnAddTask.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);

            // Add controls to panel
            panelAddTask.Controls.Add(lblTitle);
            panelAddTask.Controls.Add(this.txtTaskTitle);
            panelAddTask.Controls.Add(lblDesc);
            panelAddTask.Controls.Add(this.txtTaskDescription);
            panelAddTask.Controls.Add(lblReminder);
            panelAddTask.Controls.Add(this.dtpReminder);
            panelAddTask.Controls.Add(this.btnAddTask);

            // === BOTTOM PANEL - Task List ===
            System.Windows.Forms.Panel panelTaskList = new System.Windows.Forms.Panel();
            panelTaskList.Dock = System.Windows.Forms.DockStyle.Fill;
            panelTaskList.Padding = new System.Windows.Forms.Padding(10);

            // Task count label
            this.lblTaskCount = new System.Windows.Forms.Label();
            this.lblTaskCount.Text = "Total Tasks: 0";
            this.lblTaskCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTaskCount.Height = 30;
            this.lblTaskCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // DataGridView
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTasks.BackgroundColor = System.Drawing.Color.White;
            this.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.MultiSelect = false;

            // Delete and Complete buttons
            System.Windows.Forms.Panel panelButtons = new System.Windows.Forms.Panel();
            panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelButtons.Height = 50;

            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnDeleteTask.Text = "🗑️ Delete Selected";
            this.btnDeleteTask.Location = new System.Drawing.Point(10, 10);
            this.btnDeleteTask.Size = new System.Drawing.Size(130, 30);
            this.btnDeleteTask.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDeleteTask.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);

            this.btnCompleteTask = new System.Windows.Forms.Button();
            this.btnCompleteTask.Text = "✅ Mark as Done";
            this.btnCompleteTask.Location = new System.Drawing.Point(150, 10);
            this.btnCompleteTask.Size = new System.Drawing.Size(130, 30);
            this.btnCompleteTask.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnCompleteTask.ForeColor = System.Drawing.Color.White;
            this.btnCompleteTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompleteTask.Click += new System.EventHandler(this.btnCompleteTask_Click);

            panelButtons.Controls.Add(this.btnDeleteTask);
            panelButtons.Controls.Add(this.btnCompleteTask);

            panelTaskList.Controls.Add(this.dgvTasks);
            panelTaskList.Controls.Add(this.lblTaskCount);
            panelTaskList.Controls.Add(panelButtons);

            // Add to split container
            splitContainer.Panel1.Controls.Add(panelAddTask);
            splitContainer.Panel2.Controls.Add(panelTaskList);

            this.tabPageTasks.Controls.Add(splitContainer);
        }

        private void InitializeGameTab()
        {
            this.tabPageGame = new System.Windows.Forms.TabPage("🎮 Mini-Game");
            this.tabPageGame.BackColor = System.Drawing.Color.WhiteSmoke;

            System.Windows.Forms.TableLayoutPanel tableLayout = new System.Windows.Forms.TableLayoutPanel();
            tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayout.ColumnCount = 1;
            tableLayout.RowCount = 6;
            tableLayout.Padding = new System.Windows.Forms.Padding(20);

            // Header
            System.Windows.Forms.Label lblHeader = new System.Windows.Forms.Label();
            lblHeader.Text = "🛡️ Cybersecurity Knowledge Quiz";
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(0, 70, 140);
            lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Question category
            this.lblQuestionCategory = new System.Windows.Forms.Label();
            this.lblQuestionCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblQuestionCategory.ForeColor = System.Drawing.Color.Gray;
            this.lblQuestionCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuestionCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Question
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblQuestion.Text = "Click 'Start Quiz' to begin!";

            // Options
            System.Windows.Forms.Panel panelOptions = new System.Windows.Forms.Panel();
            panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;

            this.rbOption1 = new System.Windows.Forms.RadioButton();
            this.rbOption1.Location = new System.Drawing.Point(20, 10);
            this.rbOption1.Size = new System.Drawing.Size(400, 30);

            this.rbOption2 = new System.Windows.Forms.RadioButton();
            this.rbOption2.Location = new System.Drawing.Point(20, 45);
            this.rbOption2.Size = new System.Drawing.Size(400, 30);

            this.rbOption3 = new System.Windows.Forms.RadioButton();
            this.rbOption3.Location = new System.Drawing.Point(20, 80);
            this.rbOption3.Size = new System.Drawing.Size(400, 30);

            this.rbOption4 = new System.Windows.Forms.RadioButton();
            this.rbOption4.Location = new System.Drawing.Point(20, 115);
            this.rbOption4.Size = new System.Drawing.Size(400, 30);

            panelOptions.Controls.Add(this.rbOption1);
            panelOptions.Controls.Add(this.rbOption2);
            panelOptions.Controls.Add(this.rbOption3);
            panelOptions.Controls.Add(this.rbOption4);

            // Feedback
            this.lblFeedback = new System.Windows.Forms.Label();
            this.lblFeedback.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFeedback.Height = 40;

            // Score and buttons panel
            System.Windows.Forms.FlowLayoutPanel flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            flowPanel.Padding = new System.Windows.Forms.Padding(10);

            this.lblScore = new System.Windows.Forms.Label();
            this.lblScore.Text = "Score: 0/0";
            this.lblScore.Font = new System.Windows.Forms.Font("Segoe UI", 11F, System.Windows.Forms.FontStyle.Bold);
            this.lblScore.Size = new System.Drawing.Size(120, 35);

            this.btnStartQuiz = new System.Windows.Forms.Button();
            this.btnStartQuiz.Text = "🔄 Start Quiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(120, 35);
            this.btnStartQuiz.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnStartQuiz.ForeColor = System.Drawing.Color.White;
            this.btnStartQuiz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click);

            this.btnSubmitAnswer = new System.Windows.Forms.Button();
            this.btnSubmitAnswer.Text = "✅ Submit Answer";
            this.btnSubmitAnswer.Size = new System.Drawing.Size(130, 35);
            this.btnSubmitAnswer.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSubmitAnswer.ForeColor = System.Drawing.Color.White;
            this.btnSubmitAnswer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmitAnswer.Enabled = false;
            this.btnSubmitAnswer.Click += new System.EventHandler(this.btnSubmitAnswer_Click);

            flowPanel.Controls.Add(this.lblScore);
            flowPanel.Controls.Add(this.btnStartQuiz);
            flowPanel.Controls.Add(this.btnSubmitAnswer);

            // Add to table layout
            tableLayout.Controls.Add(lblHeader, 0, 0);
            tableLayout.Controls.Add(this.lblQuestionCategory, 0, 1);
            tableLayout.Controls.Add(this.lblQuestion, 0, 2);
            tableLayout.Controls.Add(panelOptions, 0, 3);
            tableLayout.Controls.Add(this.lblFeedback, 0, 4);
            tableLayout.Controls.Add(flowPanel, 0, 5);

            // Set row heights
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));

            this.tabPageGame.Controls.Add(tableLayout);
        }

        private void InitializeLogTab()
        {
            this.tabPageLog = new System.Windows.Forms.TabPage("📊 Activity Log");
            this.tabPageLog.BackColor = System.Drawing.Color.WhiteSmoke;

            this.lvActivityLog = new System.Windows.Forms.ListView();
            this.lvActivityLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvActivityLog.View = System.Windows.Forms.View.Details;
            this.lvActivityLog.FullRowSelect = true;
            this.lvActivityLog.BackColor = System.Drawing.Color.White;
            
            // Create columns
            this.lvActivityLog.Columns.Add("Time", 100);
            this.lvActivityLog.Columns.Add("Action", 600);

            // Add label at bottom
            System.Windows.Forms.Label lblLogInfo = new System.Windows.Forms.Label();
            lblLogInfo.Text = "Showing latest activities first";
            lblLogInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblLogInfo.Height = 25;
            lblLogInfo.ForeColor = System.Drawing.Color.Gray;
            lblLogInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblLogInfo.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);

            this.tabPageLog.Controls.Add(this.lvActivityLog);
            this.tabPageLog.Controls.Add(lblLogInfo);
        }
    }
}