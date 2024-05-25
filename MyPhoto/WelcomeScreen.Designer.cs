namespace MyPhoto
{
    partial class WelcomeScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog = new OpenFileDialog();
            ChooseImageButton = new RoundedButton();
            WelcomeLabel = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // ChooseImageButton
            // 
            ChooseImageButton.FlatStyle = FlatStyle.Flat;
            ChooseImageButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChooseImageButton.ForeColor = SystemColors.Control;
            ChooseImageButton.Location = new Point(72, 326);
            ChooseImageButton.Name = "ChooseImageButton";
            ChooseImageButton.Size = new Size(222, 52);
            ChooseImageButton.TabIndex = 1;
            ChooseImageButton.Text = "Choose Image";
            ChooseImageButton.UseVisualStyleBackColor = true;
            ChooseImageButton.Click += RoundedButton_Click;
            // 
            // WelcomeLabel
            // 
            WelcomeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            WelcomeLabel.AutoSize = true;
            WelcomeLabel.BackColor = Color.Transparent;
            WelcomeLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            WelcomeLabel.ForeColor = Color.Snow;
            WelcomeLabel.Location = new Point(24, 118);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(356, 45);
            WelcomeLabel.TabIndex = 0;
            WelcomeLabel.Text = "Welcome to My Photo";
            // 
            // panel1
            // 
            panel1.Controls.Add(WelcomeLabel);
            panel1.Controls.Add(ChooseImageButton);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(404, 630);
            panel1.TabIndex = 2;
            // 
            // WelcomeScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImage = Properties.Resources.WelcomeScreen;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1072, 630);
            Controls.Add(panel1);
            Name = "WelcomeScreen";
            Text = "MyPhoto";
            Load += WelcomeScreen_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        private void WelcomeScreen_Load(object sender, EventArgs e)
        {

        }

#endregion
        private OpenFileDialog openFileDialog;
        private RoundedButton ChooseImageButton;
        private Label WelcomeLabel;
        private Panel panel1;
    }
}
