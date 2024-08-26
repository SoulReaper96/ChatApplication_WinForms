using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApplication
{
    internal class ChatServer
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object lockObj = new object();
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

            Thread acceptThread = new Thread(AcceptClients);
            acceptThread.Start();
        }

        private void AcceptClients()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                lock (lockObj)
                {
                    clients.Add(client);
                }
                Console.WriteLine("Client connected");

                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + message);
                    BroadcastMessage(message, client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                lock (lockObj)
                {
                    clients.Remove(client);
                }
                client.Close();
                Console.WriteLine("Client disconnected");
            }
        }

        private void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    if (client != sender)
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
        }
    }
}
