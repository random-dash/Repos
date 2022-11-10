using System.Collections.Generic;

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
        
        //

        public void AddCard(Card newCard)
        {
            Cards.Add(newCard);
        }
    }
}