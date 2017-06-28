using System.Threading;
using System.Windows.Forms;
using UnityApplication = UnityEngine.Application;

namespace ConsoleGUI.ConsoleApplication
{
    public static class ConsoleApplicationManager
    {
        private static ThreadManager.ThreadInfo _threadInfo;
        private static ConsoleForm _consoleForm;

        public static void Init()
        {
            _consoleForm = new ConsoleForm();
        }

        public static void Start()
        {
            _consoleForm.InitCallbacks();
            _threadInfo = ThreadManager.StartThread(
                "ConsoleApp",
                info =>
                {
                    Application.Run(_consoleForm);
                },
                ThreadPriority.Normal
            );
        }

        public static void GameStartDone()
        {
            _consoleForm.InitAutoComplete();
            _consoleForm.EnableCommandSend();
        }

        public static void GameShutdown()
        {
            _consoleForm.ForceClose();
            Application.Exit();
            _threadInfo.thread.Abort();
        }
    }
}