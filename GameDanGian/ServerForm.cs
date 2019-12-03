using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

/// <summary>
/// Server là player 1;
/// </summary>
namespace Server
{
    public partial class ServerForm : Form, UpDateUI, ServerReceive
    {
        private GameManager game;
        private Button[] buttons;
        private Label mesLable;
        private int mPlayer = 1;
        private int mPosition = -1;

        private const int startX = 150;
        private const int startY = 100;
        private const int SIZE_BUTTON = 100;

        private ServerConnection serverConnection;
        
        public ServerForm()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            serverConnection = new ServerConnection(this);
            game = new GameManager(this);
            buttons = new Button[12];

            // 5 ô của người chơi one
            for(int i=0; i<5; i++)
            {
                buttons[i] = new Button();
                buttons[i].Click += new EventHandler(this.button_Click);
                buttons[i].Location = new Point(startX + i*SIZE_BUTTON, startY);
                buttons[i].Size = new System.Drawing.Size(100, 100);
                buttons[i].UseVisualStyleBackColor = true;
                buttons[i].Name = i.ToString();
                this.Controls.Add(buttons[i]);
            }

            // 5 ô của người chơi two
            for(int i=10; i >= 6; i--)
            {
                buttons[i] = new Button();
                //buttons[i].Click += new EventHandler(this.button_Click);
                buttons[i].Location = new Point(startX + (10 - i) * SIZE_BUTTON, startY + SIZE_BUTTON);
                buttons[i].Size = new System.Drawing.Size(100, 100);
                buttons[i].UseVisualStyleBackColor = true;
                buttons[i].Name = i.ToString();
                this.Controls.Add(buttons[i]);
            }

            // Thêm bên trái
            buttons[11] = new Button();
            buttons[11].Location = new Point(startX - SIZE_BUTTON, startY + SIZE_BUTTON/2);
            buttons[11].Size = new System.Drawing.Size(100, 100);
            buttons[11].UseVisualStyleBackColor = true;
            this.Controls.Add(buttons[11]);

            // Thêm bên phải
            buttons[5] = new Button();
            buttons[5].Location = new Point(startX + 5 * SIZE_BUTTON, startY + SIZE_BUTTON / 2);
            buttons[5].Size = new System.Drawing.Size(100, 100);
            buttons[5].UseVisualStyleBackColor = true;
            this.Controls.Add(buttons[5]);

            // Messenger Lable
            mesLable = new Label();
            mesLable.Size = new Size(200, 30);
            mesLable.Location = new Point(250, 50);
            mesLable.Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(mesLable);

            mPlayer = 2;
            upDateUI();
            PlayGame();
        }

        /// <summary>
        /// Lắng nghe từ client
        /// </summary>
        /// <param name="content"></param>
        public void ServerReceive(Content content)
        {
            // Cho client đánh ở server
            if (content.isRight)
            {
                game.playerTwoLeft(content.index);              
            }
            else
            {
                game.playerTwoRight(content.index);            
            }

            mPlayer = 1;
            PlayGame();
        }

        public void ErrorReceive(String msg)
        {
            MessengerForm messenger = new MessengerForm();
            messenger.sendMesseger(msg);
            messenger.Show();
        }

        public void upDateUI()
        {
            for(int i=0; i<12; i++)
            {
                int num = game.arr[i];
                buttons[i].Image = Utils.getImage(num);
            }

            label1.Text = "Score of player1: " + game.playerOne.score;
            label2.Text = "Score of player2: " + game.playerTwo.score;           
        }

        public void delay()
        {
            Application.DoEvents();
            Thread thread = new Thread(sleep);
            thread.Start();
            thread.Join();
            thread.Abort();
        }

        private void sleep()
        {
            Thread.Sleep(500);
        }

        private void PlayGame()
        {
            upDateUI();

            if(mPlayer == 1)
            {
                PlayerOnePlay();
            }
            else
            {
                PlayerTwoPlay();
            }

            if (game.isWin())
            {
                EndGame();
            }         
        }

        private void PlayerTwoPlay()
        {
            button13.Enabled = false;
            button14.Enabled = false;
            mesLable.Text = "Lượt của của player 2 !";
        }

        private void PlayerOnePlay()
        {
            mPosition = -1;
            button13.Enabled = true;
            button14.Enabled = true;
            mesLable.Text = "Lượt của của player 1 !";
        }

        private void EndGame()
        {
            // Thông báo người chiến thắng
            if (game.playerOne.score > game.playerTwo.score)
            {
                MessengerForm msg = new MessengerForm();
                msg.sendMesseger("Player 1 win!");
                msg.Show();
            }
            else if (game.playerOne.score < game.playerTwo.score)
            {
                MessengerForm msg = new MessengerForm();
                msg.sendMesseger("Player 2 win!");
                msg.Show();
            }
            else
            {
                MessengerForm msg = new MessengerForm();
                msg.sendMesseger("Two player have the same score!");
                msg.Show();
            }
            // Vô hiệu hết các button
            for(int i=0; i< 12; i++)
            {
                buttons[i].Enabled = false;
            }

            button14.Enabled = false;
            button13.Enabled = false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.Blue;
            mPosition = Int32.Parse(button.Name);
        }

        private void right_Click(object sender, EventArgs e)
        {
            if(mPosition < 0)
            {
                MessengerForm msg = new MessengerForm();
                msg.sendMesseger("Bạn phải chọn ô đánh!");
                msg.Show();
                return;
            }

            Content content = new Content();
            content.index = mPosition;
            content.isRight = true;
            serverConnection.Send(content);

            game.playerOneRight(mPosition);

            // Chuyển qua client chơi
            mPlayer = 2;
            PlayGame();
              
        }

        private void left_Click(object sender, EventArgs e)
        {

            if (mPosition < 0)
            {
                MessengerForm msg = new MessengerForm();
                msg.sendMesseger("Bạn phải chọn ô đánh!");
                msg.Show();
                return;
            }

            Content content = new Content();
            content.index = mPosition;
            content.isRight = false;
            serverConnection.Send(content);

            game.playerOneLeft(mPosition);

            mPlayer = 2;
            PlayGame();

        }

       
    }
}
