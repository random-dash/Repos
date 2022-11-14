using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.NetworkInformation;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Create Thread whenever a new client connects
            TcpListener server = new TcpListener(IPAddress.Any, 10001);
            server.Start();
            Console.WriteLine("Server running");
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                //Await Thread to finish
                Thread thread = new(() => HandleClient(client));
                thread.Start();
            }

        }

        public static void HandleClient(TcpClient client)
        {
            while (true)
            {
                try
                {
                    using var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    long result = IPGlobalProperties.GetIPGlobalProperties()
                                    .GetTcpIPv4Statistics()
                                    .CurrentConnections;
                    writer.WriteLine("Welcome to the Arena!");
                    writer.WriteLine("Calmy chill in the Lobby with " + result + " others.");

                    using var reader = new StreamReader(client.GetStream());
                    string message;
                    do
                    {
                        message = reader.ReadLine();
                        Console.WriteLine("received: " + message);
                    } while (message != "quit");
                }
                catch (Exception exc)
                {
                    Console.WriteLine("error occurred: " + exc.Message);
                    break;
                }
            }
        }
    }
}
