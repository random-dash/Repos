using System;
using System.Collections.Generic;
using System.Net.Configuration;

namespace MTCG
{
    public class Stack
    {
        public Stack()
        {
            this.Cards = new List<Card>();
        }

        public List<Card> Cards;
        //Count of Monsters in Stack
        public int MonsterCount
        {
            get
            {
                int count = 0;
                foreach (Card card in Cards)
                {
                    if (card.CardType == CardType.Monster)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        //Count of Spells in Stack
        public int SpellCount
        {
            get
            {
                int count = 0;
                foreach (Card card in Cards)
                {
                    if (card.CardType == CardType.Spell)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public void AddCard(Card newCard)
        {
            Cards.Add(newCard);
        }

        /*public void ListStack()
        {
            //List all Cards in Stack in a table with Name, Type, Element, Damage with borders
            Console.WriteLine("ID".PadRight(5) + "Name".PadRight(20) + "Type".PadRight(10) + "Element".PadRight(10) + "Damage".PadRight(10));
            Console.WriteLine("".PadRight(50, '-'));
            foreach (Card card in Cards)
            {
                Console.WriteLine(card.CardID.ToString().PadRight(5) + card.Name.PadRight(20) + card.CardType.ToString().PadRight(10) + card.ElementType.ToString().PadRight(10) + card.Damage.ToString().PadRight(10));
            }
        }*/

        public void ListStack()
        {
            List<int> printedIDs = new List<int>();
            int CardAmount = 0;

            Console.WriteLine("ID".PadRight(5) + "Name".PadRight(15) + "Type".PadRight(10) + "Element".PadRight(10) + "Damage".PadRight(10) + "Amount".PadRight(10));
            Console.WriteLine("".PadRight(57, '-'));
            foreach (Card card in Cards)
            {
                //Check if Card is already printed
                if (!printedIDs.Contains(card.CardID))
                {
                    //Count how many Cards of this Type are in Stack
                    foreach (Card card2 in Cards)
                    {
                        if (card2.CardID == card.CardID)
                        {
                            CardAmount++;
                        }
                    }
                    Console.WriteLine(card.CardID.ToString().PadRight(5) + card.Name.PadRight(15) + card.CardType.ToString().PadRight(10) + card.ElementType.ToString().PadRight(10) + card.Damage.ToString().PadRight(10) + CardAmount.ToString().PadRight(10));
                    printedIDs.Add(card.CardID);
                    CardAmount = 0;
                }

            }
        }

        //return Card by ID
        public Card GetCard(int id)
        {
            foreach (Card card in Cards)
            {
                if (card.CardID == id)
                {
                    return card;
                }
            }
            return null;
        }

        //remove Card by ID
        public void RemoveCard(int id)
        {
            foreach (Card card in Cards)
            {
                if (card.CardID == id)
                {
                    Cards.Remove(card);
                    break;
                }
            }
        }

        public void PrintCardsMeetingReq(CardType cardType, int minDamage)
        {
            List<int> printedIDs = new List<int>();
            int CardAmount = 0;
            
            Console.WriteLine("ID".PadRight(5) + "Name".PadRight(15) + "Type".PadRight(10) + "Element".PadRight(10) + "Damage".PadRight(10) + "Amount".PadRight(10));
            Console.WriteLine("".PadRight(57, '-'));
            foreach (var card in Cards)
            {
                
                if (card.CardType == cardType && card.Damage >= minDamage)
                {
                    //Check if Card is already printed
                    if (!printedIDs.Contains(card.CardID))
                    {
                        //Count how many Cards of this Type are in Stack
                        foreach (Card card2 in Cards)
                        {
                            if (card2.CardID == card.CardID)
                            {
                                CardAmount++;
                            }
                        }
                        Console.WriteLine(card.CardID.ToString().PadRight(5) + card.Name.PadRight(15) + card.CardType.ToString().PadRight(10) + card.ElementType.ToString().PadRight(10) + card.Damage.ToString().PadRight(10) + CardAmount.ToString().PadRight(10));
                        printedIDs.Add(card.CardID);
                        CardAmount = 0;
                    }
                }
            }
        }

        //check if card is in Stack
        public bool ContainsCard(int id)
        {
            foreach (Card card in Cards)
            {
                if (card.CardID == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}