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
            this.Deck = new Stack();
        }

        
        public int userid;
        public string username;
        public string passwd;

        public Stack Stack;
        public Stack Deck;
        public int Coins;
        public int Points;
        
        
        
        
        
        
    }
}