namespace MTCG
{
    public class Card
    {
        //for Database
        public int CardID { get; set; }
        public CardType CardType { get; set; }
        public ElementType ElementType { get; set; }

        public string Name { get; set; }
        public int Damage { get; set; }
    }
}