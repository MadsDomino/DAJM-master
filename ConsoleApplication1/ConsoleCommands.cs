using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ConsoleCommands
    {
        public void InputReader(string userInput, SQLConnection SQLCon)
        {
            Console.Clear();
            switch (userInput)
            {
                case "logind":
                        if(!SQLCon.LoggedInd())
                            SQLCon.LogInd();
                        break;

                case "logud":
                        if (SQLCon.LoggedInd())
                            SQLCon.LogUd();
                        break;

                case "loggedind":
                        if (SQLCon.LoggedInd())
                            Console.WriteLine("Du er logged ind");
                        else if (!SQLCon.LoggedInd())
                            Console.WriteLine("Du er logged ud");
                        break;
                case "TilføjElev":
                    if(!SQLCon.LoggedInd())
                    {
                        Console.WriteLine("Du skal være logged ind for at kunne tilføje en elev");
                        break;
                    }
                    TilføjElev(SQLCon);
                    break;
                case "hjælp":
                    Console.WriteLine("De nuværende funktioner er: \r\n" + " TilføjElev");
                    break;
                default:
                        break;
            }
        }

        private void TilføjElev(SQLConnection SQLCon)
        {
            bool enteringInformation = true;
            string currentInformationState = "Efternavn";
            string userInput;
            string Efternavn = ""; string Fornavn = ""; string Klasse = "";
            Console.WriteLine("Intast Efternavn, eller skriv 'cancel' for at afbryde");

            while(enteringInformation)
            {
                userInput = Console.ReadLine();

                switch (currentInformationState)
                {
                    case "Efternavn":

                        if (userInput == "cancel")
                        {
                            enteringInformation = false;
                            Console.WriteLine("TilføjElev var afbrudt");
                            break;
                        }

                        Efternavn = userInput;
                        currentInformationState = "Fornavn";
                        Console.WriteLine("Intast Fornavn, eller skriv 'cancel' for at afbryde");
                        break;

                    case "Fornavn":

                        if (userInput == "cancel")
                        {
                            enteringInformation = false;
                            Console.WriteLine("TilføjELev var afbrudt");
                            break;
                        }

                        Fornavn = userInput;
                        currentInformationState = "Klasse";
                        Console.WriteLine("Intast Klasse, eller skriv 'cancel' for at afbryde");
                        break;

                    case "Klasse":

                        if(userInput == "cancel")
                        {
                            enteringInformation = false;
                            Console.WriteLine("TilføjElev var afbrudt");
                            break;
                        }

                        Klasse = userInput;
                        currentInformationState = "Bekræft";
                        Console.WriteLine("Du har nu intastet:" 
                            + "\r\n Efternavn: " + Efternavn 
                            + "\r\n Fornavn: " + Fornavn 
                            + "\r\n Klasse: " + Klasse 
                            + "\r\n Tryk enter for at tilføje eller cancel for at afbryde");
                        break;

                    case "bekræft":

                        if (userInput == "cancel")
                        {
                            enteringInformation = false;
                            Console.WriteLine("TilføjElev var afbrudt");
                            break;
                        }

                        SQLCon.TilføjElev(Efternavn, Fornavn, Klasse);
                        enteringInformation = false;
                        Console.WriteLine("Eleven " + Fornavn + " " + Efternavn + " blev tilføjet");
                        break;
                }
            }
        }
    }
}
