using ChatApplication;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Application_WinForms
{
    public partial class ChatApplication : Form
    {
        private TcpClient? _client;
        private NetworkStream? _stream;
        private CancellationTokenSource? _cancellationTokenSource;
        private ChatServer _chatServer;

        public ChatApplication()
        {
            InitializeComponent();
            _chatServer = new ChatServer(5080);
            _chatServer.MessageReceived += OnMessageReceived;
        }

        private async void ConnectTo_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string server = ServerAddr_tb.Text;
                if (string.IsNullOrEmpty(server))
                {
                    MessageBox.Show("Server address cannot be empty.");
                    return;
                }

                if (!int.TryParse(PortNum_tb.Text, out int port))
                {
                    MessageBox.Show("Invalid port number.");
                    return;
                }

                _client = new TcpClient();
                await _client.ConnectAsync(server, port);
                _stream = _client.GetStream();

                _cancellationTokenSource = new CancellationTokenSource();
                _ = Task.Run(() => ReceiveMessages(_cancellationTokenSource.Token));

                ConnectionStatus_lbl.Text = "Connection Status: Connected";
                Connection_pbar.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisconnectFrom_btn_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private async void SendMessage_btn_Click(object sender, EventArgs e)
        {
            if (_client != null && _client.Connected)
            {
                if (InputMessage_tb == null)
                {
                    MessageBox.Show("Input message textbox is not initialized.");
                    return;
                }

                string message = InputMessage_tb.Text;
                if (string.IsNullOrEmpty(message))
                {
                    MessageBox.Show("Message cannot be empty.");
                    return;
                }

                if (_stream == null)
                {
                    MessageBox.Show("Network stream is not initialized.");
                    return;
                }

                byte[] data = Encoding.UTF8.GetBytes(message);
                await _stream.WriteAsync(data, 0, data.Length);

                if (ChatMessages_rtb != null)
                {
                    ChatMessages_rtb.AppendText("Me: " + message + Environment.NewLine);
                }
                else
                {
                    MessageBox.Show("Chat messages textbox is not initialized.");
                }

                InputMessage_tb.Clear();
            }
            else
            {
                MessageBox.Show("Not connected to the server.");
            }
        }

        private async Task ReceiveMessages(CancellationToken cancellationToken)
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_stream == null)
                    {
                        MessageBox.Show("Network stream is null.");
                        break;
                    }

                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    if (bytesRead == 0) break; // Connection closed

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke(new Action(() => ChatMessages_rtb?.AppendText("Server: " + message + Environment.NewLine)));
                }
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                Invoke(new Action(() => MessageBox.Show("Error: " + ex.Message)));
            }
        }

        private void OnMessageReceived(string message)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (ChatMessages_rtb != null)
                    {
                        ChatMessages_rtb.AppendText("Server: " + message + Environment.NewLine);
                    }
                }));
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                MessageBox.Show("An error occurred while updating the chat messages: " + ex.Message);
            }
        }

        private void ChatApplication_Load(object sender, EventArgs e)
        {
            _chatServer.Start();
        }

        private void ChatApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            _chatServer.Stop();
        }

        private void Disconnect()
        {
            if (_client != null)
            {
                _cancellationTokenSource?.Cancel();
                _client.Close();
                _client = null;
                ConnectionStatus_lbl.Text = "Connection Status: Disconnected";
                Connection_pbar.Value = 0;
            }
        }
    }
}
