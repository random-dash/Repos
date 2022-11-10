using System;
//using UserSystem = UserSystem.UserSystem;
namespace MTCG
{
    public class UI
    {
        private string choice;
        public void Menu(User player)
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
                    userSystem.register(player.username, player.passwd);
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
                }
            } while (choice != "l" || choice != "r");

        }


    }
}
