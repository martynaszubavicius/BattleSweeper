using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient
{
    static class Program
    {
        private const string endPoint = "https://localhost";
        private const string port = "44337";

        public static string URL { get { return string.Format("{0}:{1}/", endPoint, port); } }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            new Task(() => Application.Run(new ChatWindow())).Start();
            new Task(() => Application.Run(new ChatWindow())).Start();
            //new Task(() => Application.Run(new BattleSweeperWindow())).Start();
            Application.Run(new BattleSweeperWindow());
        }
    }
}
