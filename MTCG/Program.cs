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
            cardManager.LoadCardsFromDb();
            User player = new User();
            UI game = new UI(player);
            game.MainMenu(player, cardManager);
        }
    }
}