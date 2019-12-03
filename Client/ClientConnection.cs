using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    /// <summary>
    /// Thực hiện kết nối server
    /// </summary>
    class ClientConnection
    {
        private ClientReceive clientReceive;
        private IPEndPoint IP;
        private Socket client;
        public ClientConnection(ClientReceive clientReceive)
        {
            this.clientReceive = clientReceive;
            Connect();
        }

        /// <summary>
        /// Connect server
        /// </summary>
        public void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                client.Connect(IP);
            }
            catch
            {
                MessengerForm msg = new MessengerForm();
                msg.sendMesseger("Không thể kết nối!");
                msg.Show();
            }

            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
            
        }
        
        /// <summary>
        /// Close connect
        /// </summary>
        public void Close()
        {
            client.Close();
        }

        /// <summary>
        /// Nhận kết quả từ server
        /// </summary>
        public void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 1000];
                    client.Receive(data);

                    Content content = Deserialize(data);
                    clientReceive.ClientReceive(content);
                }
            }
            catch (Exception)
            {
                Close();
            }
        }

        /// <summary>
        /// Gửi conten lên server
        /// </summary>
        /// <param name="content"></param>
        public void Send(Content content)
        {
            byte[] data = new byte[5];

            client.Send(Serialize(content));
        }

        /// <summary>
        /// Giải mã content thành byte[]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public byte[] Serialize(Content content)
        {
            // trả về mảng byte[2]
            // byte đầu tiền là vị trí đánh
            // byte thứ hai quy ước 0 là ngược chiều kim đồng hồ
            // byte thứ hai quy ước 1 là thuận chiều kim đồng hồ
            byte[] data = new byte[2];
            data[0] = (byte)content.index;
            if (content.isRight)
            {
                data[1] = 1;
            }
            else
            {
                data[1] = 0;
            }

            return data;
        }

        /// <summary>
        /// Giải mã byte[] thành content
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Content Deserialize(byte[] data)
        {
            Content content = new Content();
            content.index = data[0];
            if (data[1] == 1)
            {
                content.isRight = true;
            }
            else
            {
                content.isRight = false;
            }
            return content;
        }
    }
}
