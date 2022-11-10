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
        private readonly string _connectionString = "Host=ella.db.elephantsql.com;Username=ydfkbntp;Password=hJvKH7XewBsiQqZ5RtoT7lsNs6l80bm-;Database=ydfkbntp";
        //private readonly string _connectionString = "Host=172.17.0.3;Username=postgres;Password=postrges;Database=mtcg";
        //private readonly string _connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=mtcg";
        public NpgsqlConnection _conn;
        public UserSystem()
        {
            _conn = new NpgsqlConnection(this._connectionString);
            _conn.Open();
        }

        public void register(string uname, string pwd)
        {
            //Insert user into database
            var cmd = new NpgsqlCommand("INSERT INTO ydfkbntp.public.user (username, password) VALUES (@username, @password)", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", uname));
            cmd.Parameters.Add(new NpgsqlParameter("password", pwd));
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void login(User player)
        {
            _conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM ydfkbntp.public.user WHERE username=@username", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", player.username));
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                player.userid = (int)reader["userID"];
                player.username = (string)reader["username"];
                player.passwd = (string)reader["user_pwd"];
                player.Coins = (int)reader["coins"];
                player.Points = (int)reader["ELO"];
            }
            _conn.Close();
        }

        public bool UserExists(string uname)
        {
            var cmd = new NpgsqlCommand("SELECT * FROM ydfkbntp.public.user WHERE username=@username", _conn);
            cmd.Parameters.Add(new NpgsqlParameter("username", uname));
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            return reader.Read();
        }
        

    }
}