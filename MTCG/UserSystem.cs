using System;
using System.Data;
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

        public void register(string uname, string pwd)
        {
            //Insert user into database
            var cmd = new NpgsqlCommand("INSERT INTO mtcg.public.user (user_name, user_password) VALUES (@username, @password)", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", uname));
            cmd.Parameters.Add(new NpgsqlParameter("password", pwd));
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
                player.userid = (int)reader["user_ID"];
                player.username = (string)reader["user_name"];
                player.passwd = (string)reader["user_password"];
                player.Coins = (int)reader["user_coins"];
                player.Points = (int)reader["user_ELO"];
            }
            _conn.Close();
        }

        public bool UserExists(string uname)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM mtcg.public.user WHERE user_name=@username", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", uname));
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            return reader.Read();
        }

    }
}