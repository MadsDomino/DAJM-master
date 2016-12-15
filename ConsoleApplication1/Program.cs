using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Program
    {
        SQLConnection SQLCon = new SQLConnection();
        CommandHandler CH = new CommandHandler();

        static void Main(string[] args)
        {
            Program Prog = new Program();
            Prog.Run();
        }


        private void Run()
        {
            bool running = true;
            Console.WriteLine("Skriv 'help' for en liste af kommandoer. Eller 'quit' for at lukke programmet");
            while (running)
            {
                string userinput = Console.ReadLine();
                if (userinput == "quit")
                {
                    running = false;
                    break;
                }
                else
                {
                    CH.Validate(SQLCon, userinput);
                }
            }
        }
    }
}
