using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnityEngine;
using Color = System.Drawing.Color;
using UnityApplication = UnityEngine.Application;

namespace ConsoleGUI.ConsoleApplication
{
    public class ConsoleForm : Form
    {
        private bool _onForceClosing = false;
        
        public TextBox LogOutputTextBox { get; private set; }
        public CommandTextBox CmdTextBox { get; private set; }
        public Button SendButton { get; private set; }

        private Panel _bottomPanel;
        
        public ConsoleForm()
        {
            initializeComponent();
        }
        
        public void InitCallbacks()
        {
            Logger.Main.LogCallbacks += onLoggerAction;
        }

        public void EnableCommandSend()
        {
            CmdTextBox.CommandPerformed += sendCommand;

            SendButton.Click += (sender, args) =>
            {
                CmdTextBox.PerformCommand();
            };

            SendButton.Enabled = true;
            
            CmdTextBox.Clear();
            CmdTextBox.Enabled = true;
            CmdTextBox.Focus();
        } 

        public void InitAutoComplete()
        {
            CmdTextBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            var commands = SdtdConsole.Instance.GetCommands();
            
            foreach (var cmd in commands)
            {
                CmdTextBox.AutoCompleteCustomSource.AddRange(cmd.GetCommands());
            }
        }

        public void ForceClose()
        {
            _onForceClosing = true;
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            
            if (_onForceClosing) return;
                
            var result = MessageBox.Show(
                "Shutdown server?",
                "Shutdown",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation
            );

            if (result == DialogResult.Yes)
                UnityApplication.Quit();
            else
                e.Cancel = true;
        }

        private void initializeComponent()
        {
            LogOutputTextBox = new TextBox();
            CmdTextBox = new CommandTextBox();
            SendButton = new Button();
            _bottomPanel = new Panel();
            MainMenuStrip = new MenuStrip();
            
            SuspendLayout();
            
            BackColor = Color.FromArgb(255, 60, 63, 65);
            ForeColor = Color.FromArgb(255, 220, 220, 220);

            ClientSize = new Size(800, 600);
            Text = "7 Days to Die Dedicated Server Console (Mod by Zaklinatel)";
            Icon = new Icon(API.ResourcesDirPath + "\\7DaysToDieServer.ico");
            
            MainMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Shutdown", SystemIcons.Warning.ToBitmap(), (sender, args) => Close())
            });

            LogOutputTextBox.Dock = DockStyle.Fill;
            LogOutputTextBox.BackColor = BackColor;
            LogOutputTextBox.ForeColor = ForeColor;
            LogOutputTextBox.BorderStyle = BorderStyle.None;
            LogOutputTextBox.Multiline = true;
            LogOutputTextBox.ScrollBars = ScrollBars.Vertical;
            LogOutputTextBox.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 10);
            LogOutputTextBox.ReadOnly = true;
            
            CmdTextBox.Dock = DockStyle.Fill;
            CmdTextBox.BackColor = BackColor;
            CmdTextBox.ForeColor = ForeColor;
            CmdTextBox.BorderStyle = BorderStyle.FixedSingle;
            CmdTextBox.Text = "Server loading...";
            CmdTextBox.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 12);
            CmdTextBox.Enabled = false;

            SendButton.Dock = DockStyle.Right;
            SendButton.BackColor = BackColor;
            SendButton.ForeColor = ForeColor;
            SendButton.Text = "Send";
            SendButton.Enabled = false;
            
            _bottomPanel.Dock = DockStyle.Bottom;
            _bottomPanel.AutoSize = true;
            _bottomPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            
            _bottomPanel.Controls.Add(CmdTextBox);
            _bottomPanel.Controls.Add(SendButton);

            Controls.Add(LogOutputTextBox);
            Controls.Add(_bottomPanel);
            Controls.Add(MainMenuStrip);
            
            ResumeLayout(false);
            PerformLayout();
        }

        private void sendCommand(string cmd)
        {
            List<string> response = SdtdConsole.Instance.ExecuteSync(cmd, null);
            
            if (response.Count == 0)
                return;
            
            printLinesList(response);
            
            if (!response[0].StartsWith("*** ERROR: unknown command") && !CmdTextBox.AutoCompleteCustomSource.Contains(cmd))
            {
                CmdTextBox.AutoCompleteCustomSource.Add(cmd);
            }
        }

        private void appendLogOutput(string text)
        {
            LogOutputTextBox.AppendText(text);
            LogOutputTextBox.SelectionStart = LogOutputTextBox.TextLength;
            LogOutputTextBox.ScrollToCaret();
        }

        private void printLinesList(IEnumerable<string> list)
        {
            var sb = new StringBuilder();
            foreach (var line in list)
            {
                sb.AppendLine(line);
            }

            appendLogOutput(sb.ToString());
        }

        private void onLoggerAction(string msg, string trace, LogType type)
        {
            appendLogOutput(msg + "\n");
        }
    }
}