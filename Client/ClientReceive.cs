using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    interface ClientReceive
    {
        /// <summary>
        /// Truyền về một vị trí mà server sẽ chơi (index, righr or left)
        /// </summary>
        void ClientReceive(Content content);
    }
}
