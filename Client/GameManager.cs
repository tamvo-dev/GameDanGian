using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    // Điều khiển logic game
    class GameManager
    {
        public const int MAX = 12;
        // Có hai người chơi
        public Player playerOne;
        public Player playerTwo;

        // Hai quân chúa đã bị mất chưa
        bool isLeft = false;
        bool isRight = false;

        // Mảng chứa dân của các người chơi
        // 0 1 2 3 4 => PlayerOne
        // 10 9 8 7 6 => PlayerTwo
        // 11 5 Hai ô chúa
        public int[] arr;

        private UpDateUI upDateUI;
        public GameManager(UpDateUI updateUI)
        {
            this.upDateUI = updateUI;
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

        // Đánh tại một vị trí nhất định ngược theo chiều đồng hồ
        // Trả về số điểm của người chơi đã ăn được
        // Hoặc trả về 0 nếu không thể ăn
        private int runLeft(int index)
        {
            bool isPlay = true;
            int result = 0;

            do
            {
                // Tiến hành rải quân
                int num = arr[index];
                arr[index] = 0;
                int start = index - 1;
                start = formatIndex(start);

                while (num > 0)
                {
                    num--;
                    arr[start]++;
                    start--;
                    start = formatIndex(start);
                    upDateUI.upDateUI();
                    upDateUI.delay();
                }

                if(arr[start] == 0)
                {
                    start--;
                    start = formatIndex(start);
                    result = arr[start];
                    arr[start] = 0;
                    return result;
                }
                else
                {
                    if(start == 5 || start == 11)
                    {
                        // Kết thúc tại ô quan thì kết thúc
                        return 0;
                    }
                    else
                    {
                        index = start;
                    }
                }

            } while (isPlay);

            return result;
        }


        // Đánh tại một vị trí nhất định theo chiều đồng hồ
        // Trả về số điểm của người chơi đã ăn được
        // Hoặc trả về 0 nếu không thể ăn
        private int runRight(int index)
        {
            bool isPlay = true;
            int result = 0;

            do
            {
                // Tiến hành rải quân
                int num = arr[index];
                arr[index] = 0;
                int start = index + 1;
                start = formatIndex(start);

                while (num > 0)
                {
                    num--;
                    arr[start]++;
                    start++;
                    start = formatIndex(start);
                    upDateUI.upDateUI();
                    upDateUI.delay();
                }

                if (arr[start] == 0)
                {
                    start++;
                    start = formatIndex(start);
                    result = arr[start];
                    arr[start] = 0;
                    return result;
                }
                else
                {
                    if (start == 5 || start == 11)
                    {
                        // Kết thúc tại ô quan thì kết thúc
                        return 0;
                    }
                    else
                    {
                        index = start;
                    }
                }

            } while (isPlay);

            return result;
        }

        // Người chơi một đánh left tại vị trí index
        public void playerOneLeft(int index)
        {
            playerOne.score += runLeft(index);
            overPlayerOne();
            overPlayerTwo();
        }

        // Người chơi một đánh right tại vị trí index
        public void playerOneRight(int index)
        {
            playerOne.score += runRight(index);
            overPlayerOne();
            overPlayerTwo();
        }

        // Người chơi hai đánh left tại vị trí index
       
        public void playerTwoLeft (int index)
        {
            playerTwo.score += runRight(index);
            overPlayerTwo();
            overPlayerOne();
        }

        // Người chơi hai đánh right tại vị trí index
        public void playerTwoRight(int index)
        {
            playerTwo.score += runLeft(index);
            overPlayerTwo();
            overPlayerOne();
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
