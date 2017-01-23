using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
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
            // Initialize Tobii SDK eyetracking library
            Library.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(autostart));
            
        }
    }
}
