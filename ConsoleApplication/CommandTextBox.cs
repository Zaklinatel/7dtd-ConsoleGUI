using System.Windows.Forms;

namespace ConsoleGUI.ConsoleApplication
{
    public class CommandTextBox : TextBox
    {
        public event OnCommandPerformed CommandPerformed;
        
        public CommandTextBox()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            Multiline = false;
            AutoCompleteMode = AutoCompleteMode.Append;
            AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void PerformCommand()
        {
            if (!Enabled || Text.Length == 0)
                return;
                    
            CommandPerformed?.Invoke(Text);
            Clear();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!Enabled)
                return;
            
            if (e.KeyCode == Keys.Enter)
                PerformCommand();
            
            base.OnKeyDown(e);
        }

        public delegate void OnCommandPerformed(string cmd);
    }
}