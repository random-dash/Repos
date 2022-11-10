using System.Linq.Expressions;

namespace MTCG
{
    public class Spell : Card
    {
        public Spell()
        {
            CardType = CardType.Spell;
        }
        
        //public override from Card
        public override CardType CardType { get; set; }
        public override ElementType ElementType { get; set; }
        public override string Name { get; set; }
        public override int Damage { get; set; }
    }
}