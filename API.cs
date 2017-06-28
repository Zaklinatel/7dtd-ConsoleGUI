/**
MIT License

Copyright (c) 2017 Ivan L. Sennov @Zaklinatel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the folloswing conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.IO;
using System.Reflection;
using ConsoleGUI.ConsoleApplication;

namespace ConsoleGUI
{
    public class API : ModApiAbstract
    {
        public static readonly string ResourcesDirPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName + "\\Resources";
        
        public static API Instance {
            get {
                var mod = ModManager.GetMod ("ConsoleGUI");
                return mod.ApiInstance as API;
            }
        }

        public API ()
        {
            Log("Initializing...");
            ConsoleApplicationManager.Init();
            
            Log("Start application...");
            ConsoleApplicationManager.Start();
            
            Log("Waiting for server start done...");
        }

        public override void GameStartDone()
        {
            ConsoleApplicationManager.GameStartDone();
            Log("Mod initialized");
        }

        public override void GameShutdown()
        {
            Log("Server shutdown!");
            ConsoleApplicationManager.GameShutdown();
        }

        public static void Log (string msg)
        {
            global::Log.Out($"[ConsoleGUI] {msg}");
        }
    }
}