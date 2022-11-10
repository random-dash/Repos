using System;
using System.Collections.Generic;

namespace MTCG
{
    public class User
    {
        public User()
        {
            this.Coins = 20;
            this.Points = 100;
            this.Stack = new Stack();
        }
        //private >
        //prop.
        public int userid;
        public string username;
        public string passwd;

        public Stack Stack;
        public int Coins;
        public int Points;

        public void BuyPack(List<Card> GameCards)
        {
            Random rand = new Random();
            Console.WriteLine("Opening Pack");

            for (int i = 0; i < 5; i++)
            {
                int randomCard = rand.Next(1, GameCards.Count);
                Card newCard;
                if (GameCards[randomCard].CardType == CardType.Monster)
                {
                    newCard = new Monster();
                }
                else
                {
                    newCard = new Spell();
                }

                newCard.ElementType = GameCards[randomCard].ElementType;
                newCard.Name = GameCards[randomCard].Name;
                newCard.Damage = GameCards[randomCard].Damage;
                
                Console.WriteLine("\nCard " + (i + 1) + ":");
                Console.WriteLine("Name: " + newCard.Name);
                Console.WriteLine("CardType: " + newCard.CardType);
                Console.WriteLine("ElementType: " + newCard.ElementType);
                Console.WriteLine("Damage: " + newCard.Damage);
                
                Stack.AddCard(newCard);
                Coins -= 5;
            }
        }
        public void Arena(User player1, User player2)
        {
            Console.WriteLine("Player 1: " + player1.username);
            Console.WriteLine("Player 2: " + player2.username);
            
            //Fight randomly until one player has no cards left
            
            
        }
        
        //Fight using Water > Fire > Normal > Water and Damage
        
        
        
        
        
        
    }
}