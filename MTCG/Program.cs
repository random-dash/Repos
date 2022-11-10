using System;
using System.Data;
using System.Net;

namespace MTCG
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CardManager cardManager = new CardManager();
            cardManager.uploadAllCards();
            cardManager.LoadCardsFromDb();
            cardManager.PrintCards();
            //cardManager.PrintCards();
            /*User player = new User();
            UI game = new UI();
            game.Menu(player);
            Console.WriteLine("user: " + player.username);
            Console.WriteLine("userid: " + player.userid);
            Console.WriteLine("coins: " + player.Coins);
            Console.WriteLine("points: " + player.Points);
            player.BuyPack(cardManager.GetCards());*/
            //player.BuyPack(cardManager.AllCards());
            //player.Arena(player.Stack);
            //Console.WriteLine(player.Stack.Monsters() + " " + player.Stack.Spells());
        }
    }
}