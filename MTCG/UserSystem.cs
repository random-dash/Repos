using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using MTCG;
using Npgsql;

namespace UserSystem
{
    public class UserSystem
    {
        //Connect to ElephantSQL
        private readonly string _connectionString = "Host=185.65.234.37;Username=underline;Password=underline;Database=mtcg";
        public NpgsqlConnection _conn;
        public UserSystem()
        {
            _conn = new NpgsqlConnection(this._connectionString);
            _conn.Open();
        }

        public bool register(string uname, string pwd)
        {
            //Insert user into database and get inserted user id
            var cmd = new NpgsqlCommand("INSERT INTO mtcg.public.user (user_name, user_password) VALUES (@username, @password);", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", uname));
            cmd.Parameters.Add(new NpgsqlParameter("password", pwd));
            cmd.Prepare();
            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("User successfully registered!");
                return true;
            }
            else
            {
                return false;
            }
        }

        //Create Stack entry for new user in database
        public void createStack(string username)
        {
            //Get user id from database and create stack entry
            var cmd = new NpgsqlCommand("INSERT INTO mtcg.public.stack (userid) SELECT userID FROM mtcg.public.user WHERE user_name=@username", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", username));
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void login(User player)
        {
            _conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM mtcg.public.user WHERE user_name=@username", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", player.username));
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                player.userid = (int)reader["userid"];
                player.username = (string)reader["user_name"];
                player.passwd = (string)reader["user_password"];
                player.Coins = (int)reader["user_coins"];
                player.Points = (int)reader["user_ELO"];
            }
            reader.Close();
        }

        public bool UserExists(string uname)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM mtcg.public.user WHERE user_name=@username", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", uname));
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }
        public void UpdateUser(User player)
        {
            var cmd = new NpgsqlCommand("UPDATE mtcg.public.user SET user_coins=@coins, user_ELO=@points WHERE userid=@userid", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("coins", player.Coins));
            cmd.Parameters.Add(new NpgsqlParameter("points", player.Points));
            cmd.Parameters.Add(new NpgsqlParameter("userid", player.userid));
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        //DELETE cards of user from stack_cards and insert new cards into stack_cards
        public void UpdateStack(User player)
        {
            //get stackid of user
            var cmd2 = new NpgsqlCommand("SELECT stackid FROM mtcg.public.stack WHERE userid=@userid", _conn);
            cmd2.Parameters.Add(new NpgsqlParameter("userid", player.userid));
            cmd2.Prepare();
            var reader = cmd2.ExecuteReader();
            int stackid = 0;
            while (reader.Read())
            {
                stackid = (int)reader["stackid"];
            }
            reader.Close();
            var cmd = new NpgsqlCommand("DELETE FROM mtcg.public.stack_cards WHERE stackid=@stackid", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("stackid", stackid));
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            foreach (Card card in player.Stack.Cards)
            {
                cmd = new NpgsqlCommand("INSERT INTO mtcg.public.stack_cards (stackid, cardcollectionid) VALUES (@stackid, @cardid)", _conn);
                cmd.Parameters.Add(new NpgsqlParameter("stackid", stackid));
                cmd.Parameters.Add(new NpgsqlParameter("cardid", card.CardID));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        //get cards from stack_cards and insert them into stack using CardManager
        /*public void GetStack(User player, CardManager cardManager)
        {
            player.Stack.Cards.Clear();
            var cmd2 = new NpgsqlCommand("SELECT stackid FROM mtcg.public.stack WHERE userid=@userid", _conn);
            cmd2.Parameters.Add(new NpgsqlParameter("userid", player.userid));
            cmd2.Prepare();
            var reader = cmd2.ExecuteReader();
            int stackid = 0;
            while (reader.Read())
            {
                stackid = (int)reader["stackid"];
            }
            reader.Close();
            var cmd = new NpgsqlCommand("SELECT cardcollectionid FROM mtcg.public.stack_cards WHERE stackid=@stackid", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("stackid", stackid));
            cmd.Prepare();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                player.Stack.Cards.Add(cardManager.GetCard((int)reader["cardcollectionid"]));
            }
            reader.Close();
        }*/


        


        public void Logout(User player)
        {
            UpdateUser(player);
            UpdateStack(player);
        }

    }
}