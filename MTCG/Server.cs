using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

//Server with socket programming and multithreading 
namespace Serverr
{

    public class Server
    {
        public static async Task Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 8000);
            listener.Start(5);

            Console.CancelKeyPress += (sender, e) => Environment.Exit(0);

            while (true)
            {
                try
                {
                    var socket = await listener.AcceptTcpClientAsync();
                    using var writer = new StreamWriter(socket.GetStream()) { AutoFlush = true };
                    writer.WriteLine("Welcome to the Arena!");

                    using var reader = new StreamReader(socket.GetStream());
                    string message;
                    do
                    {
                        message = await reader.ReadLineAsync();
                        Console.WriteLine(message);
                        await writer.WriteLineAsync(message);
                        while (message != null) ;
                    } while (message != "quit");
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occured: " + exc.Message);
                }
            }
        }
    }

    

}
