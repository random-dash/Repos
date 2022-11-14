using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTCG
{
    internal class Arena
    {
        public Arena()
        {

            
        }

        public void ConnectToServer(User player)
        {
            //Thread.Sleep(2000); // 2 sec so the server is up and running

            using TcpClient client = new TcpClient("localhost", 8000);
            using StreamReader reader = new StreamReader(client.GetStream());
            Console.WriteLine($"{reader.ReadLine()}{Environment.NewLine}{reader.ReadLine()}");
            using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            string input = null;
            while ((input = Console.ReadLine()) != "quit")
            {
                writer.WriteLine(input);
            }
            writer.WriteLine("quit");
        }






        public int Fight(Card card1, Card card2)
        {
            if (card1.CardType == CardType.Monster && card2.CardType == CardType.Monster)
            {
                if (card1.Damage > card2.Damage)
                {
                    Console.WriteLine(card1.Name + " wins");
                    return 1;
                }
                else if (card1.Damage < card2.Damage)
                {
                    Console.WriteLine(card2.Name + " wins");
                    return 2;
                }
                else
                {
                    Console.WriteLine("Draw");
                    return 0;
                }
            }
            else
            {
                if (card1.ElementType == ElementType.Fire && card2.ElementType == ElementType.Water)
                {
                    if (card1.Damage / 2 < card2.Damage * 2)
                    {
                        Console.WriteLine(card2.Name + " wins");
                        return 2;
                    }
                    else if (card1.Damage / 2 > card2.Damage * 2)
                    {
                        Console.WriteLine(card1.Name + " wins");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Draw");
                        return 0;
                    }
                }
                else if (card1.ElementType == ElementType.Water && card2.ElementType == ElementType.Normal)
                {
                    if (card1.Damage / 2 < card2.Damage * 2)
                    {
                        Console.WriteLine(card2.Name + " wins");
                        return 2;
                    }
                    else if (card1.Damage / 2 > card2.Damage * 2)
                    {
                        Console.WriteLine(card1.Name + " wins");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Draw");
                        return 0;
                    }
                }
                else if (card1.ElementType == ElementType.Normal && card2.ElementType == ElementType.Fire)
                {
                    if (card1.Damage / 2 < card2.Damage * 2)
                    {
                        Console.WriteLine(card2.Name + " wins");
                        return 2;
                    }
                    else if (card1.Damage / 2 > card2.Damage * 2)
                    {
                        Console.WriteLine(card1.Name + " wins");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Draw");
                        return 0;
                    }
                }
                else
                {
                    if (card1.Damage < card2.Damage)
                    {
                        Console.WriteLine(card2.Name + " wins");
                        return 2;
                    }
                    else if (card1.Damage > card2.Damage)
                    {
                        Console.WriteLine(card1.Name + " wins");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Draw");
                        return 0;
                    }
                }
            }
        }
    }
}
