using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Thread.Sleep(2000);

            using TcpClient client = new TcpClient("localhost", 8000);
            using StreamReader reader = new StreamReader(client.GetStream());
            Console.WriteLine($"{ reader.ReadLine() }{ Environment.NewLine }{ reader.ReadLine() }");
            using StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            string input = null;
            while((input = Console.ReadLine()) != "quit")
            {
                writer.WriteLine(input);
            }
            writer.WriteLine("quit");
        }
    }
}
