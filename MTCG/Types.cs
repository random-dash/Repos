namespace MTCG
{
    public enum CardType
    {
        Monster = 1,
        Spell = 2
    }

    public enum ElementType
    {
        Fire = 1,
        Water = 2,
        Normal = 3
    }

    public struct Trade
    {
        public int TradeID;
        public int UserID;
        public int CardID;
        public CardType rct;
        public int rd;
    }
}