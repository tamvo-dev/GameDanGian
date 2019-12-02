using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDanGian
{
    // Điều khiển logic game
    class GameManager
    {
        public const int MAX = 12;
        // Có hai người chơi
        Player playerOne;
        Player playerTwo;

        // Hai quân chúa đã bị mất chưa
        bool isLeft = false;
        bool isRight = false;

        // Mảng chứa dân của các người chơi
        // 0 1 2 3 4 => PlayerOne
        // 10 9 8 7 6 => PlayerTwo
        // 11 5 Hai ô chúa
        public int[] arr;

        public GameManager()
        {
            playerOne = new Player();
            playerOne.name = "Player 1";
            playerTwo = new Player();
            playerTwo.name = "Player 2";

            // Khoi tao cac quan cua moi o
            arr = new int[MAX];
            for(int i=0; i<MAX; i++)
            {
                if(i == 5 || i == 11)
                {
                    arr[i] = 1;
                }
                else
                {
                    arr[i] = 5;
                }
            }
        }

       // Kiểm tra đã kết thúc game hay chưa
        public bool isWin()
        {
            if( isLeft == true && isRight == true)
            {
                return true;
            }
            return false;
        }

        // Kiểm tra tại vị trí index thì người chơi một có đánh được hay không
        // Thuộc 0..4
        // Phải có ít nhất 1 quân trong ô đó
        public bool isPlayerOne(int index)
        {
            if(index >= 0 && index < 5)
            {
                if(arr[index] > 0)
                    return true;
            }
            return false;
        }

        // Kiểm tra tại vị trí index thì người chơi hai có đánh được hay không
        // Thuộc 6..10
        // Phải có ít nhất 1 quân trong ô đó
        public bool isPlayerTwo(int index)
        {
            if (index >= 6 && index < 11)
            {
                if (arr[index] > 0)
                    return true;
            }
            return false;
        }

        private int formatIndex(int index)
        {
            if(index < 0)
            {
                return 11;
            }
            else if(index > 11)
            {
                return 0;
            }
            return index;
        }

        // Người chơi một đánh left tại vị trí index
        public void playerOneLeft(int index)
        {
            int num = arr[index];
            arr[index] = 0;

            // Tiến hành rải quân
            int start = index - 1;
            start = formatIndex(start);
               
            while(num > 0)
            {
                num--;
                arr[start]++;
                start--;
                start = formatIndex(start);
            }

            // Sau khi rải xong quân thì sét xem tại vị trí đó có ăn được, đánh tiếp hay mất lượt
            if( arr[start] == 0)
            {
                start--;
                start = formatIndex(start);

                if (start == 5)
                {
                    isRight = true;
                }
                else if (start == 11)
                {
                    isLeft = true;
                }
                playerOne.score += arr[start];
                arr[start] = 0;
                return;
            }
            else
            {
                // arr[start] != 0
                if (isPlayerOne(start))
                {
                    // Nếu tại vị trí này là của player one thì cho đánh tiếp
                    playerOneLeft(start);
                }
                // Không phải thì kết thúc lượt
            }
        }

        // Người chơi một đánh right tại vị trí index
        public void playerOneRight(int index)
        {
            int num = arr[index];
            arr[index] = 0;

            // Tiến hành rải quân
            int start = index + 1;

            while (num > 0)
            {
                num--;
                arr[start]++;
                start++;
                start = formatIndex(start);
            }

            // Sau khi rải xong quân thì sét xem tại vị trí đó có ăn được, đánh tiếp hay mất lượt
            if (arr[start] == 0)
            {
                start++;
                start = formatIndex(start);

                if (start == 5)
                {
                    isRight = true;
                }
                else if (start == 11)
                {
                    isLeft = true;
                }
                playerOne.score += arr[start];
                arr[start] = 0;
                return;
            }
            else
            {
                // arr[start] != 0
                if (isPlayerOne(start))
                {
                    // Nếu tại vị trí này là của player one thì cho đánh tiếp
                    playerOneRight(start);
                }
                // Không phải thì kết thúc lượt
            }
        }

        // Người chơi hai đánh left tại vị trí index
        public void playerTwoLeft (int index)
        {
            int num = arr[index];
            arr[index] = 0;

            // Tiến hành rải quân
            int start = index + 1;
            start = formatIndex(start);

            while (num > 0)
            {
                num--;
                arr[start]++;
                start++;
                start = formatIndex(start);
            }

            // Sau khi rải xong quân thì sét xem tại vị trí đó có ăn được, đánh tiếp hay mất lượt
            if (arr[start] == 0)
            {
                start++;
                start = formatIndex(start);

                if (start == 5)
                {
                    isRight = true;
                }
                else if (start == 11)
                {
                    isLeft = true;
                }
                playerTwo.score += arr[start];
                arr[start] = 0;
                return;
            }
            else
            {
                // arr[start] != 0
                if (isPlayerTwo(start))
                {
                    // Nếu tại vị trí này là của player one thì cho đánh tiếp
                    playerTwoLeft(start);
                }
                // Không phải thì kết thúc lượt
            }
        }

        // Người chơi hai đánh right tại vị trí index
        public void playerTwoRight(int index)
        {
            int num = arr[index];
            arr[index] = 0;

            // Tiến hành rải quân
            int start = index - 1;
            start = formatIndex(start);

            while (num > 0)
            {
                num--;
                arr[start]++;
                start--;
                start = formatIndex(start);
            }

            // Sau khi rải xong quân thì sét xem tại vị trí đó có ăn được, đánh tiếp hay mất lượt
            if (arr[start] == 0)
            {
                start--;
                start = formatIndex(start);

                if(start == 5)
                {
                    isRight = true;
                }
                else if(start == 11)
                {
                    isLeft = true;
                }
                playerTwo.score += arr[start];
                arr[start] = 0;
                return;
            }
            else
            {
                // arr[start] != 0
                if (isPlayerTwo(start))
                {
                    // Nếu tại vị trí này là của player one thì cho đánh tiếp
                    playerTwoRight(start);
                }
                // Không phải thì kết thúc lượt
            }
        }

        // Kiểm tra xem người chơi one đã hết quân chưa, nếu hết thì phải rải lại
        public void overPlayerOne()
        {
            for(int i=0; i<5; i++)
            {
                if(arr[i] > 0)
                {
                    return;
                }
            }

            // Đã hết quân thì phải rải ra
            if(playerOne.score >= 5) 
            {
                playerOne.score -= 5;
            }
            else
            {
                playerOne.score -= 5;
                playerTwo.score -= 5;
            }

            for(int i=0; i < 5; i++)
            {
                arr[i] = 1;
            }
        }

        // Kiểm tra xem người chơi two đã hết quân chưa, nếu hết thì phải rải lại
        public void overPlayerTwo()
        {
            for (int i = 6; i < 11; i++)
            {
                if (arr[i] > 0)
                {
                    return;
                }
            }

            // Đã hết quân thì phải rải ra
            if (playerTwo.score >= 5)
            {
                playerTwo.score -= 5;
            }
            else
            {
                playerOne.score -= 5;
                playerTwo.score -= 5;
            }

            for (int i = 6; i < 11; i++)
            {
                arr[i] = 1;
            }
        }



    }
}
