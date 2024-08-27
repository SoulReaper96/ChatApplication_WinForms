using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApplication
{
    internal class ChatServer
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object lockObj = new object();
        private int port;
        private CancellationTokenSource cancellationTokenSource;
        internal Action<string> MessageReceived;

        public ChatServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started on port " + port);

            cancellationTokenSource = new CancellationTokenSource();
            _ = Task.Run(() => AcceptClients(cancellationTokenSource.Token));
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            server.Stop();
            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    client.Close();
                }
                clients.Clear();
            }
            Console.WriteLine("Server stopped");
        }

        private async Task AcceptClients(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    lock (lockObj)
                    {
                        clients.Add(client);
                    }
                    Console.WriteLine("Client connected");

                    _ = Task.Run(() => HandleClient(client, cancellationToken));
                }
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is OperationCanceledException)
            {
                // Server stopped
            }
        }

        private async Task HandleClient(TcpClient client, CancellationToken cancellationToken)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + message);
                    await BroadcastMessage(message, client);
                }
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is OperationCanceledException)
            {
                // Client disconnected
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

        private async Task BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            List<Task> tasks = new List<Task>();

            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    if (client != sender)
                    {
                        NetworkStream stream = client.GetStream();
                        tasks.Add(stream.WriteAsync(data, 0, data.Length));
                    }
                }
            }

            await Task.WhenAll(tasks);
        }

        public async Task SendMessageToAllClientsAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            List<Task> tasks = new List<Task>();

            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    NetworkStream stream = client.GetStream();
                    tasks.Add(stream.WriteAsync(data, 0, data.Length));
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
