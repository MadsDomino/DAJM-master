using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    interface ICommandObject
    {
        string name { get; set; }
        string preconditions { get; set; }
        string commandDescription { get; set; }
        string forceNextCommandID { get; set; }
        string commandID { get; set; }
        void execute(SQLConnection SQLConnection, string parameters);
    }

    class CommandTemplate
    {
        public string name { get; set; }
        public string preconditions { get; set; }
        public string forceNextCommandID { get; set; }
        public string commandDescription { get; set; }
        public string commandID { get; set; }
    }

    class CommandLogin : CommandTemplate, ICommandObject
    {
        public CommandLogin()
        {
            name = "login";
            preconditions = "none";
            forceNextCommandID = "none";
            commandID = "01";
            commandDescription = "Denne kommando vil konnekte dig til databasen.";
        }
        public void execute(SQLConnection SQLConnection, string parameters)
        {
            if (!SQLConnection.LoggedInd())
            {
                Console.WriteLine("Logger ind i databasen.");
                SQLConnection.LogInd();
            }
            else
            {
                Console.WriteLine("Du er allerede logget ind.");
            }
        }
    }

    class CommandLogout : CommandTemplate, ICommandObject
    {
        public CommandLogout()
        {
            name = "logout";
            preconditions = "none";
            forceNextCommandID = "none";
            commandID = "02";
            commandDescription = "Denne kommando vil logge dig ud af databasen";
        }
        public void execute(SQLConnection SQLConnection, string parameters)
        {
            if (SQLConnection.LoggedInd())
            {
                Console.WriteLine("Logger ud af databsen.");
                SQLConnection.LogUd();
            }
            else
            {
                Console.WriteLine("Du er allerede logget ud af databasen.");
            }
        }
    }

    class CommandLoggedIn : CommandTemplate, ICommandObject
    {
        public CommandLoggedIn()
        {
            name = "loggedin";
            preconditions = "none";
            forceNextCommandID = "none";
            commandID = "03";
            commandDescription = "Tjekker om du er logget ind i databasen.";
        }
        public void execute(SQLConnection SQLConnection, string parameters)
        {
            if (SQLConnection.LoggedInd())
                Console.WriteLine("Du er logget ind i databasen.");
            else
                Console.WriteLine("Du er logget ud af databasen.");
        }
    }

    class CommandAddStudent : CommandTemplate, ICommandObject
    {
        public CommandAddStudent()
        {
            name = "tilføjelev";
            preconditions = "front page";
            forceNextCommandID = "none";
            commandID = "04";
            commandDescription = "Tilføjer en student til databasen, for at tilføje skriv 'tilføjelev <efternavn> <fornavn> <klasse>' uden <>";
        }
        public void execute(SQLConnection SQLConnection, string parameters)
        {
            string[] parameterArray = parameters.Split(' ');
            if (parameterArray.Length != 3)
                Console.WriteLine("Fejl, Der er skrevet flere parametre end efternavn, fornavn, klasse. Indtast venligst kun 3 ting.");
            else
            {
                if (!SQLConnection.LoggedInd())
                {
                    Console.WriteLine("Du er ikke logget ind i databasen.");
                }
                else
                    SQLConnection.TilføjElev(parameterArray[0], parameterArray[1], parameterArray[2]);
            }
        }
    }

    class CommandClear : CommandTemplate, ICommandObject
    {
        public CommandClear()
        {
            name = "clear";
            preconditions = "none";
            forceNextCommandID = "none";
            commandID = "05";
            commandDescription = "Rydder konsollen for text.";
        }

        public void execute(SQLConnection SQLConnection, string parameters)
        {
            Console.Clear();
        }
    }
}
