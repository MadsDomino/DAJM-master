using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class CommandHandler
    {
        List<ICommandObject> Commands = new List<ICommandObject>();
        int HowManyErrors = 0;
        string precondition = "front page";
        string currentcommand;
        string parameters;

        public CommandHandler()
        {
            AddCommands();
        }

        public void Validate(SQLConnection SQLConnection, string command)
        {
            bool foundcurrentcommand = false;
            splitCommandIntoParametersAndCommand(command);

            if (currentcommand == "help" && parameters == "")
            {
                Console.WriteLine("De kommandoer du har til rådighed lige nu er: ");
                foreach (ICommandObject commandInList in Commands)
                {
                    if (commandInList.preconditions == precondition || commandInList.preconditions == "none")
                    {
                        Console.WriteLine(commandInList.name);
                    }
                }
                Console.WriteLine("For at få flere informationer om en enkelt kommando skal du skrive 'help <kommando>' uden <>");
                foundcurrentcommand = true;
            }
            else if (currentcommand == "help" && parameters != "")
            {
                foreach (ICommandObject commandInList in Commands)
                {
                    if (commandInList.name == parameters)
                    {
                        Console.WriteLine("Udskriver informationer om: " + commandInList.name);
                        Console.WriteLine(commandInList.commandDescription);
                        foundcurrentcommand = true;
                    }
                }
                if (!foundcurrentcommand)
                {
                    Console.WriteLine("Kan ikke finde kommandoen '" + parameters + "'");
                    foundcurrentcommand = true;
                }
            }
            else
            {
                foreach (ICommandObject commandInList in Commands)
                {
                    if (commandInList.name == currentcommand)
                    {
                        if (commandInList.preconditions == precondition || commandInList.preconditions == "none")
                        {
                            commandInList.execute(SQLConnection, parameters);
                        }
                        else
                        {
                            Console.WriteLine("Du kan ikke bruge denne kommando i denne sammenhæng. Skriv 'help' for at få en liste af kommandoer, eller 'help <kommando>' for at få mere at vide om en enkelt kommando");
                        }
                        foundcurrentcommand = true;
                    }
                }
            }
            if (foundcurrentcommand == false)
                Console.WriteLine("kunne ikke finde kommandoen '" + currentcommand + "' skriv 'help' for at få en liste af kommandoer til rådighed");
        }

        private void splitCommandIntoParametersAndCommand(string command)
        {
            string[] commandarray = command.Split(' ');
            parameters = "";
            for (int parameternumber = 1; parameternumber < commandarray.Length; parameternumber++)
            {
                if (parameternumber != commandarray.Length - 1)
                    parameters = parameters + commandarray[parameternumber] + " ";
                else
                    parameters = parameters + commandarray[parameternumber];
            }
            parameters.Trim();
            currentcommand = commandarray[0];
        }

        private void AddCommands()
        {
            Console.WriteLine("Loader kommando listen.");

            loadcommandLogin();
            loadcommandLogout();
            loadcommandLoggedIn();
            loadcommandAddStudent();

            Console.WriteLine("Loading af kommandoer blev færdig gjort med " + HowManyErrors + " fejl.");
        }

        private void loadcommandLogin()
        {
            try
            {
                Console.WriteLine("Loader kommandoen: commandLogin");
                CommandLogin commandLogin = new CommandLogin();
                Commands.Add(commandLogin);
                LoadingCompleteText();
            }
            catch
            {
                Console.WriteLine("Fejl ved loading af kommandoen commandLogin");
                HowManyErrors++;
            }
        }

        private void loadcommandLogout()
        {
            try
            {
                Console.WriteLine("Loader kommandoen: commandLogout");
                CommandLogout commandLogout = new CommandLogout();
                Commands.Add(commandLogout);
                LoadingCompleteText();
            }
            catch
            {
                Console.WriteLine("Fejl ved loading af kommandoen commandLogout");
                HowManyErrors++;
            }
        }

        private void loadcommandLoggedIn()
        {
            try
            {
                Console.WriteLine("Loader kommandoen: commandLoggedIn");
                CommandLoggedIn commandLoggedIn = new CommandLoggedIn();
                Commands.Add(commandLoggedIn);
                LoadingCompleteText();
            }
            catch
            {
                Console.WriteLine("Fejl ved loading af kommandoen commandLoggedIn");
                HowManyErrors++;
            }
        }

        private void loadcommandAddStudent()
        {
            try
            {
                Console.WriteLine("Loader kommandoen: commandAddStudent");
                CommandAddStudent commandAddStudent = new CommandAddStudent();
                Commands.Add(commandAddStudent);
                LoadingCompleteText();
            }
            catch
            {
                Console.WriteLine("Fejl ved loading af kommandoen commandAddStudent");
                HowManyErrors++;
            }
        }

        private void LoadingCompleteText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Complete");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
