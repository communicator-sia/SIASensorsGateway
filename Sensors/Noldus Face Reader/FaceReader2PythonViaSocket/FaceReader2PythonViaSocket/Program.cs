using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTestSocket_01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Boolean autostart = false;
            if (args.Length > 0)
            {
                Console.WriteLine("arg[0]=" + args[0]);
                if (args[0].Equals("/autostart"))
                {
                    //System.Environment.Exit(0);
                    autostart = true;
                    Console.WriteLine("Detected /autostart parameter");
                }

            }
            else
            {
                Console.WriteLine("No input args.");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(autostart));
        }
    }
}
