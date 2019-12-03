using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Server
{
    class Utils
    {
        private const String url = "C:\\Users\\LAptop\\source\\repos\\GameDanGian\\Images\\";
        
        public static Image getImage(int count)
        {
            if(count < 10)
            {
                return Image.FromFile(url + count + ".png");
            }

            return Image.FromFile(url + "10.png");
        }
    }
}
