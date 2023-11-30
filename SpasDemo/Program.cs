using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SpasDemo
{ 
    /// <summary> 
    /// This is the program of the little demo. It should help 
    /// you to understand how you could communicate with Spiroson-AS.
    /// </summary>
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FormSpasDemoMain());
    }
  }
}