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
        private List<string> _users;
        private Dictionary<string, List<string>> _userMessages;
        private string? Timestamp = DateTime.Now.ToString("HH:mm:ss");

        public ChatApplication()
        {
            InitializeComponent();
            _chatServer = new ChatServer(5080);
            _chatServer.MessageReceived += OnMessageReceived;
            _users = new List<string>();
            _userMessages = new Dictionary<string, List<string>>();
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

                string? _selectedUser = Users_lstbox.SelectedItem?.ToString();
                if (_selectedUser != null)
                {
                    if (_userMessages.ContainsKey(_selectedUser))
                    {
                        _userMessages[_selectedUser] = new List<string>();
                    }
                    _userMessages[_selectedUser].Add($"Me [{Timestamp}]: {message}");
                }

                if (ChatMessages_rtb != null)
                {
                    ChatMessages_rtb.AppendText($"Me [{Timestamp}]: {message}" + Environment.NewLine);
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

        private async void ServerMessage_btn_Click(object sender, EventArgs e)
        {
            if (ServerMessage_tb == null)
            {
                MessageBox.Show("Input message textbox is not initialized.");
                return;
            }

            string message = ServerMessage_tb.Text;
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Message cannot be empty.");
                return;
            }

            try
            {
                await _chatServer.SendMessageToAllClientsAsync(message);

                if (ChatMessages_rtb != null)
                {
                    ChatMessages_rtb.AppendText($"Server [{Timestamp}]: {message}" + Environment.NewLine);
                }
                else
                {
                    MessageBox.Show("Chat messages textbox is not initialized.");
                }

                ServerMessage_tb.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
                    Invoke(new Action(() =>
                    {
                        string? _selectedUser = Users_lstbox.SelectedItem?.ToString();
                        if (_selectedUser != null)
                        {
                            if (!_userMessages.ContainsKey(_selectedUser))
                            {
                                _userMessages[_selectedUser] = new List<string>();
                            }
                            _userMessages[_selectedUser].Add($"Server [{Timestamp}]: {message}");
                        }

                        ChatMessages_rtb?.AppendText($"Server [{Timestamp}]: {message}" + Environment.NewLine);
                    }));
                }
            }
            catch (OperationCanceledException)
            {
                // Handle the cancellation
            }
            catch (Exception ex)
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
                    string? _selectedUser = Users_lstbox.SelectedItem?.ToString();
                    if (_selectedUser != null)
                    {
                        if (!_userMessages.ContainsKey(_selectedUser))
                        {
                            _userMessages[_selectedUser] = new List<string>();
                        }
                        _userMessages[_selectedUser].Add($"Server [{Timestamp}]: {message}");
                    }

                    if (ChatMessages_rtb != null)
                    {
                        ChatMessages_rtb.AppendText($"Server [{Timestamp}]: {message}" + Environment.NewLine);
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
            LoadUsers();
        }

        private void ChatApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            _chatServer.Stop();
            SaveUsers();
        }

        private void Disconnect()
        {
            if (_client != null)
            {
                _cancellationTokenSource?.Cancel();
                _client.Close();
                _client = null;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                ConnectionStatus_lbl.Text = "Connection Status: Disconnected";
                Connection_pbar.Value = 0;
            }
        }

        private void AddUserToolItem_Click(object sender, EventArgs e)
        {
            using (var addUserForm = new AddUserForm())
            {
                if (addUserForm.ShowDialog() == DialogResult.OK && addUserForm._userName != null)
                {
                    AddNewUser(addUserForm._userName);
                }
            }
        }

        private void AddNewUser(string userName)
        {
            if (_users != null && !_users.Contains(userName))
            {
                _users.Add(userName);
                Users_lstbox.Items.Add(userName);
                SaveUsers();
            }
        }

        private void LoadUsers()
        {
            string filePath = "G:\\Studies\\Chat Application_WinForms\\Users.txt"; // Path to your users file

            if (File.Exists(filePath))
            {
                var users = File.ReadAllLines(filePath);
                foreach (var user in users)
                {
                    if (!string.IsNullOrEmpty(user) && !_users.Contains(user))
                    {
                        _users.Add(user);
                        Users_lstbox.Items.Add(user);
                        _userMessages[user] = new List<string>(); // Add this line to create an entry in the dictionary
                    }
                }
            }
        }

        private void SaveUsers()
        {
            string filePath = "G:\\Studies\\Chat Application_WinForms\\Users.txt"; // Path to your users file
            File.WriteAllLines(filePath, _users);
        }

        private void Users_lstbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedUserMessages();
        }

        private void DisplaySelectedUserMessages()
        {
            string? selectedUser = Users_lstbox.SelectedItem?.ToString();
            if (selectedUser != null && _userMessages.ContainsKey(selectedUser))
            {
                ChatMessages_rtb.Clear();
                foreach (var message in _userMessages[selectedUser])
                {
                    ChatMessages_rtb.AppendText(message + Environment.NewLine);
                }
            }
        }
    }
}
