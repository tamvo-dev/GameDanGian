using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    /// <summary>
    /// Thực hiện kết nối server
    /// </summary>
    class ServerConnection
    {
        private ServerReceive serverReceive;
        private IPEndPoint IP;
        private Socket server;
        private Socket client;
        public ServerConnection(ServerReceive serverReceive)
        {
            this.serverReceive = serverReceive;
            Connect();
        }

        /// <summary>
        /// Connect server
        /// </summary>
        public void Connect()
        {
            IP = new IPEndPoint(IPAddress.Any, 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            server.Bind(IP);

            Thread listen = new Thread( () => {

                try
                {
                    while (true)
                    {
                        // Chỉ nghe một thằng
                        server.Listen(100);
                        client = server.Accept();

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start();
                    }
                }
                catch
                {
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                }
            });

            listen.IsBackground = true;
            listen.Start();
        }
        
        /// <summary>
        /// Close connect
        /// </summary>
        public void Close()
        {
            server.Close();
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
                    serverReceive.ServerReceive(content);
                }
            }
            catch (Exception ex)
            {
                serverReceive.ErrorReceive(ex.Message);
            }
        }

        /// <summary>
        /// Gửi content lên server
        /// </summary>
        /// <param name="content"></param>
        public void Send(Content content)
        {
            try
            {
                client.Send(Serialize(content));
            }
            catch
            {
                //client.Close();
            }
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
            data[0] = (byte) content.index;
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
            if(data[1] == 1)
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
