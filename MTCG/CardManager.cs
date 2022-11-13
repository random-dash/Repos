using System;
using System.Collections.Generic;
using MTCG;
using Npgsql;

public class CardManager
{
    //List of all cards
    private List<Card> GameCards = new List<Card>();
    public List<Trade> Trades = new List<Trade>();
    
    /*public ValueTuple<CardType, ElementType, string, int>[] AllCards()
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        ValueTuple<CardType, ElementType, string, int>[] cards =
        {

            //Elemental
            //Monsters
            (CardType.Monster, ElementType.Water, "Dracle", 12),
            (CardType.Monster, ElementType.Water, "Cympot", 20),
            (CardType.Monster, ElementType.Water, "Basichea", 32),
            //Spells
            (CardType.Spell, ElementType.Water, "Hirbaun", 14),
            (CardType.Spell, ElementType.Water, "Arulpu", 26),
            //Fire
            //Monsters
            (CardType.Monster, ElementType.Fire, "Gierion", 16),
            (CardType.Monster, ElementType.Fire, "Wepire", 18),
            (CardType.Monster, ElementType.Fire, "Eilez", 34),
            //Spells
            (CardType.Spell, ElementType.Fire, "Yadorm", 20),
            (CardType.Spell, ElementType.Fire, "Knuagd", 22),
            //Normal
            //Monsters
            (CardType.Monster, ElementType.Normal, "Haihaod", 14),
            (CardType.Monster, ElementType.Normal, "Tarasdraosk", 20),
            (CardType.Monster, ElementType.Normal, "Bihue", 28),
            //Spells
            (CardType.Spell, ElementType.Normal, "Hardaan", 16),
            (CardType.Spell, ElementType.Normal, "Sicio", 28),

        };


        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        return cards;
    }

    public void AddCardToDb(CardType cardType, ElementType elementType, string name, int damage)
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("INSERT INTO mtcg.public.card_collection (card_name, card_type, card_element_type, card_damage) VALUES (@cardname, @cardtype, @elementtype, @carddamage)", userSystem._conn);
        
        // ReSharper disable HeapView.BoxingAllocation
        cmd.Parameters.Add(new NpgsqlParameter("cardname", name));
        //put cardtype as int
        cmd.Parameters.Add(new NpgsqlParameter("cardtype", (int)cardType));
        cmd.Parameters.Add(new NpgsqlParameter("elementtype", (int)elementType));
        cmd.Parameters.Add(new NpgsqlParameter("carddamage", value: damage));
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        //close connection
        userSystem._conn.Close();
    }*/

    //load all cards from DB to GameCards using Type and Element
    public void LoadCardsFromDb()
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("SELECT * FROM mtcg.public.card_collection", userSystem._conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Card card = new Card();
            // ReSharper disable HeapView.BoxingAllocation
            card.CardID = (int)reader["card_ID"];
            card.Name = (string)reader["card_name"];
            card.CardType = (CardType)reader["card_type"];
            card.ElementType = (ElementType)reader["card_element_type"];
            card.Damage = (int)reader["card_damage"];
            GameCards.Add(card);
        }
    }

    

    //print GameCards to Console
    public void PrintCards()
    {
        foreach (var card in GameCards)
        {
            Console.WriteLine("ID: " + card.CardID + " Name: " + card.Name + " Type: " + card.CardType + " Element: " + card.ElementType + " Damage: " + card.Damage);
        }
    }
    
    //return GameCards
    public List<Card> GetCards()
    {
        return GameCards;
    }

    public void GetStack(User player)
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd2 = new NpgsqlCommand("SELECT stackid FROM mtcg.public.stack WHERE userid=@userid", userSystem._conn);
        cmd2.Parameters.Add(new NpgsqlParameter("userid", player.userid));
        cmd2.Prepare();
        var reader = cmd2.ExecuteReader();
        int stackid = 0;
        while (reader.Read())
        {
            stackid = (int)reader["stackid"];
        }
        reader.Close();
        var cmd = new NpgsqlCommand("SELECT cardcollectionid FROM mtcg.public.stack_cards WHERE stackid=@stackid", userSystem._conn);
        cmd.Parameters.Add(new NpgsqlParameter("stackid", stackid));
        cmd.Prepare();
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            player.Stack.Cards.Add(GetCard((int)reader["cardcollectionid"]));
        }
        reader.Close();
    }

    /*internal void uploadAllCards()
    {
        foreach (var card in AllCards())
        {
            AddCardToDb(card.Item1, card.Item2, card.Item3, card.Item4);
        }
    }*/

    public void BuyPack(User User)
    {
        Random rand = new Random();
        Console.WriteLine("Opening Pack");

        for (int i = 0; i < 5; i++)
        {
            int randomCard = rand.Next(1, GameCards.Count);
            Card newCard = new Card();
            newCard.CardID = GameCards[randomCard].CardID;
            newCard.CardType = GameCards[randomCard].CardType;
            newCard.ElementType = GameCards[randomCard].ElementType;
            newCard.Name = GameCards[randomCard].Name;
            newCard.Damage = GameCards[randomCard].Damage;

            Console.WriteLine("\nCard " + (i + 1) + ":");
            Console.WriteLine("Name: " + newCard.Name);
            Console.WriteLine("CardType: " + newCard.CardType);
            Console.WriteLine("ElementType: " + newCard.ElementType);
            Console.WriteLine("Damage: " + newCard.Damage);

            User.Stack.AddCard(newCard);

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
        User.Coins -= 5;
    }

    public void CreateDeck(Stack deck, Stack stack)
    {
        stack.ListStack();
        Console.WriteLine("This is your stack. Choose 5 Cards for your Deck by typing in the ID of the Card");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Card " + (i + 1) + ":");
            int cardID = Convert.ToInt32(Console.ReadLine());
            deck.AddCard(stack.GetCard(cardID));
        }

        Console.WriteLine("Your Deck is now:");
        deck.ListStack();
        Console.WriteLine("Confirm? (y/n)");
        string answer = Console.ReadLine();
        if (answer == "n")
        {
            CreateDeck(deck, stack);
        }
        else
        {
            foreach (var card in deck.Cards)
            {
                stack.RemoveCard(card.CardID);
            }
        }
        Console.WriteLine("Deck created");
    }

    //return card by ID
    public Card GetCard(int cardID)
    {
        foreach (var card in GameCards)
        {
            if (card.CardID == cardID)
            {
                return card;
            }
        }

        return null;
    }

    //Get all trades from database and print them
    public void GetTrades()
    {
        Trades.Clear();
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("SELECT * FROM mtcg.public.store", userSystem._conn);
        cmd.Prepare();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            //Empty Trades if there are any
            
            Trade newTrade = new Trade();
            newTrade.TradeID = (int)reader["tradeid"];
            newTrade.UserID = (int)reader["userid"];
            newTrade.CardID = (int)reader["cardid"];
            newTrade.rct = (CardType)reader["rct"];
            newTrade.rd = (int)reader["rd"];
            Trades.Add(newTrade);
        }
        reader.Close();
        userSystem._conn.Close();
    }

    //print all trades
    public void PrintTrades()
    {
        Console.WriteLine("ID".PadRight(5) + "User".PadRight(15) + "Card Name".PadRight(15) + "Type".PadRight(10) +
                          "Element Type".PadRight(15) + "Damage".PadRight(10) + "Requested Type".PadRight(18) + "Requested Damage".PadRight(15));

        foreach (var trade in Trades)
        {
            Console.WriteLine(trade.TradeID.ToString().PadRight(5) + GetUserName(trade.UserID).PadRight(15) + GetCard(trade.CardID).Name.PadRight(15) +
                              GetCard(trade.CardID).CardType.ToString().PadRight(10) + GetCard(trade.CardID).ElementType.ToString().PadRight(15) +
                              GetCard(trade.CardID).Damage.ToString().PadRight(10) + trade.rct.ToString().PadRight(18) + trade.rd.ToString().PadRight(15));
        }
    }

    //return username by ID
    public string GetUserName(int userID)
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("SELECT user_name FROM mtcg.public.user WHERE userid = @userid", userSystem._conn);
        cmd.Parameters.Add(new NpgsqlParameter("userid", userID));
        cmd.Prepare();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            return (string)reader["user_name"];
        }
        reader.Close();
        userSystem._conn.Close();
        return null;
    }

    public void Trade(int tradeID, User player)
    {
        Trade trade = GetTrade(tradeID);
        player.Stack.PrintCardsMeetingReq(trade.rct, trade.rd);
        Console.WriteLine("These are your Cards you can trade that meet the requirements for this trade. Type in the ID of the Card you want to trade");
        Console.WriteLine("If you want to cancel the trade type in 0");
        int cardID = Convert.ToInt32(Console.ReadLine());
        if (cardID == 0)
        {
            return;
        }
        player.Stack.RemoveCard(cardID);
        //Give Card to other player
        GiveCardToUser(trade.UserID, cardID);
        //Get Card from other player
        player.Stack.AddCard(GetCard(trade.CardID));
        //Remove Trade from Database
        RemoveTrade(tradeID);
        Console.WriteLine("Trade successful");
    }

    public void GiveCardToUser(int userID, int cardID)
    {
        //get stackid of user
        int stackID = GetStackID(userID);
        Console.WriteLine("StackID: " + stackID);
        //add card to stack in stack_cards
        AddCardToStack(stackID, cardID);
    }

    public void RemoveTrade(int tradeID)
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("DELETE FROM mtcg.public.store WHERE tradeid = @tradeid", userSystem._conn);
        cmd.Parameters.Add(new NpgsqlParameter("tradeid", tradeID));
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        userSystem._conn.Close();
    }

    public void AddCardToStack(int stackID, int cardID)
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("INSERT INTO mtcg.public.stack_cards (stackid, cardcollectionid) VALUES (@stackid, @cardid)", userSystem._conn);
        cmd.Parameters.Add(new NpgsqlParameter("stackid", stackID));
        cmd.Parameters.Add(new NpgsqlParameter("cardid", cardID));
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        userSystem._conn.Close();
    }

    public int GetStackID(int userID)
    {
        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("SELECT stackid FROM mtcg.public.stack WHERE userid = @userid", userSystem._conn);
        cmd.Parameters.Add(new NpgsqlParameter("userid", userID));
        cmd.Prepare();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            return (int)reader["stackid"];
        }
        reader.Close();
        userSystem._conn.Close();
        return 0;
    }

    //return trade by ID
    public Trade GetTrade(int tradeID)
    {
        foreach (var trade in Trades)
        {
            if (trade.TradeID == tradeID)
            {
                return trade;
            }
        }

        return new Trade();
    }

    //Send card to store
    public void SendCardToStore(User player)
    {
        int cardID;
        player.Stack.ListStack();
        do
        {
            Console.WriteLine("Type in the ID of the Card you want to send for trade");
            cardID = Convert.ToInt32(Console.ReadLine());
        } while (!player.Stack.ContainsCard(cardID));
        int rct = 0;
        do
        {
            Console.WriteLine("Type in the requested Card Type (1 for Monster, 2 for Spell)");
            rct = Convert.ToInt32(Console.ReadLine());
        } while (rct != 1 && rct != 2);
        Console.WriteLine("Type in the requested minimal Damage");
        int rd = Convert.ToInt32(Console.ReadLine());

        UserSystem.UserSystem userSystem = new UserSystem.UserSystem();
        var cmd = new NpgsqlCommand("INSERT INTO mtcg.public.store (userid, cardid, rct, rd) VALUES (@userid, @cardid, @rct, @rd)", userSystem._conn);
        cmd.Parameters.Add(new NpgsqlParameter("userid", player.userid));
        cmd.Parameters.Add(new NpgsqlParameter("cardid", cardID));
        cmd.Parameters.Add(new NpgsqlParameter("rct", rct));
        cmd.Parameters.Add(new NpgsqlParameter("rd", rd));
        cmd.Prepare();
        cmd.ExecuteNonQuery();
        userSystem._conn.Close();
        player.Stack.RemoveCard(cardID);
    }


}

