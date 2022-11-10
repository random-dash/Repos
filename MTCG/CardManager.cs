using System;
using System.Collections.Generic;
using MTCG;
using Npgsql;

public class CardManager
{
    //List of all cards
    private List<Card> GameCards = new List<Card>();
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
            card.CardID = (int)reader["card_id"];
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

    /*internal void uploadAllCards()
    {
        foreach (var card in AllCards())
        {
            AddCardToDb(card.Item1, card.Item2, card.Item3, card.Item4);
        }
    }*/
}

