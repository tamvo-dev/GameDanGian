namespace Client
{
    /// <summary>
    /// Mặc định client là Player 1;
    /// </summary>
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(676, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Score player2: 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(676, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Score player1: 1";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(191, 316);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(100, 30);
            this.button14.TabIndex = 4;
            this.button14.Text = "Left";
            this.button14.Click += new System.EventHandler(this.left_Click);
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(391, 316);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(100, 30);
            this.button13.TabIndex = 5;
            this.button13.Text = "Right";
            this.button13.Click += new System.EventHandler(this.right_Click);
            this.button13.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
    }
}

