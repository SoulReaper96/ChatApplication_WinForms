using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication
{
    internal class ChatServer
    {
        private TcpListener server;
        private ConcurrentBag<TcpClient> clients = new ConcurrentBag<TcpClient>();
        private int port;

        public ChatServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started on port " + port);

            AcceptClientsAsync();
        }

        private async void AcceptClientsAsync()
        {
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                clients.Add(client);
                Console.WriteLine("Client connected");

                HandleClientAsync(client);
            }
        }

        private async void HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            try
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + message);
                    await BroadcastMessageAsync(message, client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                clients.TryTake(out client);
                client.Close();
                Console.WriteLine("Client disconnected");
            }
        }

        private async Task BroadcastMessageAsync(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    NetworkStream stream = client.GetStream();
                    await stream.WriteAsync(data, 0, data.Length);
                }
            }
        }
    }
}
