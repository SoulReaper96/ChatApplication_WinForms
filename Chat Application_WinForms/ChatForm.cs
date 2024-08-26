using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Application_WinForms
{
    public partial class ChatApplication : Form
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private CancellationTokenSource _cancellationTokenSource;

        public ChatApplication()
        {
            InitializeComponent();
        }

        private async void ConnectTo_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string _server = ServerAddr_tb.Text;
                int _port = int.Parse(PortNum_tb.Text);
                _client = new TcpClient();
                await _client.ConnectAsync(_server, _port);
                _stream = _client.GetStream();

                _cancellationTokenSource = new CancellationTokenSource();
                _ = Task.Run(() => ReceiveMessages(_cancellationTokenSource.Token));

                ConnectionStatus_lbl.Text = "Connected";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisconnectFrom_btn_Click(object sender, EventArgs e)
        {
            if (_client != null)
            {
                _cancellationTokenSource?.Cancel();
                _client.Close();
                ConnectionStatus_lbl.Text = "Disconnected";
            }
        }

        private async void SendMessage_btn_Click(object sender, EventArgs e)
        {
            if (_client != null && _client.Connected)
            {
                string _message = InputMessage_tb.Text;
                byte[] _data = Encoding.UTF8.GetBytes(_message);
                await _stream.WriteAsync(_data, 0, _data.Length);
                ChatMessages_rtb.AppendText("Me: " + _message + Environment.NewLine);
                InputMessage_tb.Clear();
            }
        }

        private async Task ReceiveMessages(CancellationToken cancellationToken)
        {
            try
            {
                byte[] _data = new byte[1024];
                while (!cancellationToken.IsCancellationRequested)
                {
                    int _bytes = await _stream.ReadAsync(_data, 0, _data.Length, cancellationToken);
                    if (_bytes == 0) break; // Connection closed

                    string _message = Encoding.UTF8.GetString(_data, 0, _bytes);
                    Invoke(new Action(() => ChatMessages_rtb.AppendText("Server: " + _message + Environment.NewLine)));
                }
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                Invoke(new Action(() => MessageBox.Show("Error: " + ex.Message)));
            }
        }
    }
}
