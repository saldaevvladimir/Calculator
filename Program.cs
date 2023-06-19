using System;
using Gtk;

namespace Calculator
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var calculatorApp = new Application("org.Calculator.Calculator", GLib.ApplicationFlags.None);
            calculatorApp.Register(GLib.Cancellable.Current);

            var win = new CalculatorWindow();
            calculatorApp.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
