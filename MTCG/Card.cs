namespace MTCG
{
    public class Card
    {
        //for Database
        public virtual int CardID { get; set; }
        public virtual CardType CardType { get; set; }
        public virtual ElementType ElementType { get; set; }

        public virtual string Name { get; set; }
        public virtual int Damage { get; set; }
    }
}