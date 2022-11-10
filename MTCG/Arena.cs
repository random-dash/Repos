using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    internal class Arena
    {
        public Arena()
        {
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
