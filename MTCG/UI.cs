using System;
using System.Media;

//using UserSystem = UserSystem.UserSystem;
namespace MTCG
{
    public class UI
    {
        private string choice;

        //constructor
        public UI(User player)
        {
            this.choice = "";
        }

        public void MainMenu(User player, CardManager cardManager)
        {
            UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
            Console.WriteLine("Welcome to Monster Trade Card Game!\n");

            do
            {
                Console.WriteLine("Do you wish to (l)ogin or (r)egister?");
                choice = Console.ReadLine();
                if (choice == "l")
                {
                    Console.WriteLine("Please Login\n");
                    Console.WriteLine("Username: ");
                    player.username = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    player.passwd = Console.ReadLine();
                    if (userSystem.UserExists(player.username))
                    {
                        userSystem._conn.Close();
                        userSystem.login(player);
                        cardManager.GetStack(player);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("User Does Not exist!");
                        choice = "error";
                    }
                }
                else if (choice == "r")
                {
                    Console.WriteLine("Username: ");
                    player.username = Console.ReadLine();
                    Console.WriteLine("Password: ");
                    player.passwd = Console.ReadLine();
                    if (userSystem.register(player.username, player.passwd))
                    {
                        userSystem.createStack(player.username);
                        Console.WriteLine("Please Login\n");
                        Console.WriteLine("Username: ");
                        player.username = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        player.passwd = Console.ReadLine();
                        if (userSystem.UserExists(player.username))
                        {
                            userSystem._conn.Close();
                            userSystem.login(player);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("User Does Not exist!");
                            choice = "error";
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("User could not be registered!");
                        choice = "error";
                    }
                    
                }
            } while (choice != "l" || choice != "r" || choice != "error");

            IngameMenu(player, cardManager);
            userSystem.Logout(player);
            userSystem._conn.Close();
        }

        //Ingame Menu
        public void IngameMenu(User player, CardManager cardManager)
        {
            do
            {
                Console.WriteLine("Welcome to the Ingame Menu!\n");
                Console.WriteLine("Coins: ".PadRight(5) + player.Coins.ToString().PadRight(10) + "Points: ".PadRight(5) + player.Points);
                Console.WriteLine("(a)rena");
                Console.WriteLine("(s)tore");
                Console.WriteLine("(m)y stack");
                Console.WriteLine("(e)xit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "a":
                        Console.WriteLine("\nEntering the Arena!");
                        ArenaMenu(player, cardManager);
                        break;
                    case "m":
                        //cardManager.GetStack(player);
                        Console.WriteLine("\nThis is your stack!");
                        player.Stack.ListStack();
                        Console.WriteLine("\nStack size: " + player.Stack.Cards.Count);
                        break;
                    case "s":
                        Console.WriteLine("\nEntering the Trading Market!");
                        TradingMarketMenu(player, cardManager);
                        break;
                    case "e":
                        Console.WriteLine("\nYou exited the game! Thanks for playing!");
                        
                        break;
                }
            } while (choice != "e");
        }

        public void ArenaMenu(User player, CardManager cardManager)
        {
            string Achoice = "";
            do
            {
                Console.WriteLine("Welcome to the Arena!\n");
                Console.WriteLine("You have to create a Deck first!");
                //Choose 5 cards from stack and put them in deck
                cardManager.CreateDeck(player.Deck, player.Stack);
                Console.WriteLine("Searching for an opponent...");
                Console.WriteLine("Deck in Use:");
                player.Deck.ListStack();
                //TODO : Search for an opponent or exit
                Console.WriteLine("Do you want to (s)tart the game or (e)xit?");
                Achoice = Console.ReadLine();
                if (choice == "s")
                {
                    //TODO : Start the game 
                }
            } while (Achoice != "e");
        }

        public void TradingMarketMenu(User player, CardManager cardManager)
        {
            do
            {
                Console.WriteLine("\nWelcome to the Store!");
                Console.WriteLine("You can trade or buy cards here!");
                Console.WriteLine("(b)uy a pack (5 coins)");
                Console.WriteLine("(s)how cards for a trade");
                Console.WriteLine("(p)ut a card for a trade");
                Console.WriteLine("(t)ake back a trade");
                Console.WriteLine("(l)eave");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "b":
                        Console.WriteLine("\nBuying a pack!");
                        if (player.Coins >= 5)
                        {
                            cardManager.BuyPack(player);
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough coins!");
                        }

                        Console.WriteLine("\nPack bought successfully!");
                        break;
                    case "s":
                        Console.WriteLine("\nThese are the cards for a trade!");
                        cardManager.GetTrades();
                        cardManager.PrintTrades();
                        Console.WriteLine("\nDo you want to (t)rade a card or (c)ancel?");
                        choice = Console.ReadLine();
                        if (choice == "t")
                        {
                            Console.WriteLine("\nWhich card do you want to have? (Trade ID)");
                            int tradeId = Convert.ToInt32(Console.ReadLine());
                            cardManager.Trade(tradeId, player);
                        }

                        break;
                    case "p":
                        Console.WriteLine("\nPut a card for a trade!");
                        cardManager.SendCardToStore(player);
                        break;
                    case "t":
                        Console.WriteLine("\nTake back a trade!");
                        //cardManager.TakeBackTrade(player);
                        break;
                }
            } while (choice != "l");
        }

        

        
    }
}
