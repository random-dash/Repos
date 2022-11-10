using System;
using System.Runtime.CompilerServices;

namespace MTCG
{
    public class Monster : Card
    {
        public Monster()
        {
            CardType = CardType.Monster;
        } 
        
        //inherit Properties from Card
        public override CardType CardType { get; set; } 
        public override ElementType ElementType { get; set; }
       
        public override string Name { get; set; }
        public override int Damage { get; set; }
        
    }
}