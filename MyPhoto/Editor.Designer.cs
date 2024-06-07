using System.Drawing.Imaging;

namespace MyPhoto
{
    partial class Editor
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
            Environment.Exit(0);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PictureBox = new PictureBox();
            RightPanel = new Panel();
            Filters = new Panel();
            PurplePictureBox = new PictureBox();
            PurpleCheckBox = new CheckBox();
            BluePictureBox = new PictureBox();
            BlueCheckBox = new CheckBox();
            DarkPictureBox = new PictureBox();
            DarkCheckBox = new CheckBox();
            TransparencyPictureBox = new PictureBox();
            GrayscalePictureBox = new PictureBox();
            NegativePictureBox = new PictureBox();
            SepiaPictureBox = new PictureBox();
            TransparencyCheckBox = new CheckBox();
            GrayscaleCheckBox = new CheckBox();
            NegativeCheckBox = new CheckBox();
            SepiaCheckBox = new CheckBox();
            ChannelsBox = new GroupBox();
            BlueChannelLabel = new Label();
            GreenChannelLabel = new Label();
            RedChannelLabel = new Label();
            BlueChannelSlider = new TrackBar();
            GreenChannelSlider = new TrackBar();
            RedChannelSlider = new TrackBar();
            TopPanel = new Panel();
            RedoButton = new Button();
            UndoButton = new Button();
            PencilButton = new Button();
            MedianBlurButton = new Button();
            GaussianBlurButton = new Button();
            SaveButton = new Button();
            OpenButton = new Button();
            BottomPanel = new Panel();
            ResetAllButton = new RoundedButton();
            SaturationLabel = new Label();
            ContrastLabel = new Label();
            BrightnessLabel = new Label();
            SaturationSlider = new TrackBar();
            ContrastSlider = new TrackBar();
            BrightnessSlider = new TrackBar();
            LeftPanel = new Panel();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)PictureBox).BeginInit();
            RightPanel.SuspendLayout();
            Filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PurplePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BluePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DarkPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TransparencyPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GrayscalePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NegativePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SepiaPictureBox).BeginInit();
            ChannelsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BlueChannelSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GreenChannelSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RedChannelSlider).BeginInit();
            TopPanel.SuspendLayout();
            BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SaturationSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ContrastSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BrightnessSlider).BeginInit();
            SuspendLayout();
            // 
            // PictureBox
            // 
            PictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PictureBox.BackColor = Color.FromArgb(30, 30, 30);
            PictureBox.BackgroundImageLayout = ImageLayout.Zoom;
            PictureBox.Image = Properties.Resources.WelcomeScreen;
            PictureBox.Location = new Point(53, 33);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new Size(600, 600);
            PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox.TabIndex = 0;
            PictureBox.TabStop = false;
            PictureBox.Click += PictureBox_Click;
            PictureBox.MouseDown += PictureBox_Down;
            PictureBox.MouseUp += PictureBox_Up;
            // 
            // RightPanel
            // 
            RightPanel.Controls.Add(Filters);
            RightPanel.Controls.Add(ChannelsBox);
            RightPanel.Dock = DockStyle.Right;
            RightPanel.Location = new Point(659, 27);
            RightPanel.Name = "RightPanel";
            RightPanel.Size = new Size(286, 596);
            RightPanel.TabIndex = 5;
            // 
            // Filters
            // 
            Filters.AutoScroll = true;
            Filters.Controls.Add(PurplePictureBox);
            Filters.Controls.Add(PurpleCheckBox);
            Filters.Controls.Add(BluePictureBox);
            Filters.Controls.Add(BlueCheckBox);
            Filters.Controls.Add(DarkPictureBox);
            Filters.Controls.Add(DarkCheckBox);
            Filters.Controls.Add(TransparencyPictureBox);
            Filters.Controls.Add(GrayscalePictureBox);
            Filters.Controls.Add(NegativePictureBox);
            Filters.Controls.Add(SepiaPictureBox);
            Filters.Controls.Add(TransparencyCheckBox);
            Filters.Controls.Add(GrayscaleCheckBox);
            Filters.Controls.Add(NegativeCheckBox);
            Filters.Controls.Add(SepiaCheckBox);
            Filters.Location = new Point(3, 229);
            Filters.Name = "Filters";
            Filters.Size = new Size(271, 361);
            Filters.TabIndex = 1;
            // 
            // PurplePictureBox
            // 
            PurplePictureBox.Image = Properties.Resources.Purple;
            PurplePictureBox.Location = new Point(14, 340);
            PurplePictureBox.Name = "PurplePictureBox";
            PurplePictureBox.Size = new Size(85, 81);
            PurplePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            PurplePictureBox.TabIndex = 13;
            PurplePictureBox.TabStop = false;
            // 
            // PurpleCheckBox
            // 
            PurpleCheckBox.AutoSize = true;
            PurpleCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PurpleCheckBox.ForeColor = SystemColors.Control;
            PurpleCheckBox.Location = new Point(14, 419);
            PurpleCheckBox.Name = "PurpleCheckBox";
            PurpleCheckBox.Size = new Size(67, 21);
            PurpleCheckBox.TabIndex = 12;
            PurpleCheckBox.Text = "Purple";
            PurpleCheckBox.UseVisualStyleBackColor = true;
            PurpleCheckBox.MouseClick += PurpleCheckBox_CheckedChanged;
            // 
            // BluePictureBox
            // 
            BluePictureBox.Image = Properties.Resources.Blue;
            BluePictureBox.Location = new Point(150, 234);
            BluePictureBox.Name = "BluePictureBox";
            BluePictureBox.Size = new Size(85, 81);
            BluePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            BluePictureBox.TabIndex = 11;
            BluePictureBox.TabStop = false;
            // 
            // BlueCheckBox
            // 
            BlueCheckBox.AutoSize = true;
            BlueCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BlueCheckBox.ForeColor = SystemColors.Control;
            BlueCheckBox.Location = new Point(150, 313);
            BlueCheckBox.Name = "BlueCheckBox";
            BlueCheckBox.Size = new Size(54, 21);
            BlueCheckBox.TabIndex = 10;
            BlueCheckBox.Text = "Blue";
            BlueCheckBox.UseVisualStyleBackColor = true;
            BlueCheckBox.MouseClick += BlueCheckBox_CheckedChanged;
            // 
            // DarkPictureBox
            // 
            DarkPictureBox.Image = Properties.Resources.Dark;
            DarkPictureBox.Location = new Point(14, 234);
            DarkPictureBox.Name = "DarkPictureBox";
            DarkPictureBox.Size = new Size(85, 81);
            DarkPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            DarkPictureBox.TabIndex = 9;
            DarkPictureBox.TabStop = false;
            // 
            // DarkCheckBox
            // 
            DarkCheckBox.AutoSize = true;
            DarkCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DarkCheckBox.ForeColor = SystemColors.Control;
            DarkCheckBox.Location = new Point(14, 313);
            DarkCheckBox.Name = "DarkCheckBox";
            DarkCheckBox.Size = new Size(56, 21);
            DarkCheckBox.TabIndex = 8;
            DarkCheckBox.Text = "Dark";
            DarkCheckBox.UseVisualStyleBackColor = true;
            DarkCheckBox.MouseClick += DarkCheckBox_CheckedChanged;
            // 
            // TransparencyPictureBox
            // 
            TransparencyPictureBox.Image = Properties.Resources.Transparency;
            TransparencyPictureBox.Location = new Point(153, 117);
            TransparencyPictureBox.Name = "TransparencyPictureBox";
            TransparencyPictureBox.Size = new Size(85, 81);
            TransparencyPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            TransparencyPictureBox.TabIndex = 7;
            TransparencyPictureBox.TabStop = false;
            // 
            // GrayscalePictureBox
            // 
            GrayscalePictureBox.Image = Properties.Resources.Grayscale;
            GrayscalePictureBox.Location = new Point(14, 117);
            GrayscalePictureBox.Name = "GrayscalePictureBox";
            GrayscalePictureBox.Size = new Size(85, 81);
            GrayscalePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            GrayscalePictureBox.TabIndex = 6;
            GrayscalePictureBox.TabStop = false;
            // 
            // NegativePictureBox
            // 
            NegativePictureBox.Image = Properties.Resources.Negative;
            NegativePictureBox.Location = new Point(153, 3);
            NegativePictureBox.Name = "NegativePictureBox";
            NegativePictureBox.Size = new Size(85, 81);
            NegativePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            NegativePictureBox.TabIndex = 5;
            NegativePictureBox.TabStop = false;
            // 
            // SepiaPictureBox
            // 
            SepiaPictureBox.Image = Properties.Resources.Sepia;
            SepiaPictureBox.Location = new Point(14, 3);
            SepiaPictureBox.Name = "SepiaPictureBox";
            SepiaPictureBox.Size = new Size(85, 81);
            SepiaPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            SepiaPictureBox.TabIndex = 4;
            SepiaPictureBox.TabStop = false;
            // 
            // TransparencyCheckBox
            // 
            TransparencyCheckBox.AutoSize = true;
            TransparencyCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TransparencyCheckBox.ForeColor = SystemColors.Control;
            TransparencyCheckBox.Location = new Point(150, 196);
            TransparencyCheckBox.Name = "TransparencyCheckBox";
            TransparencyCheckBox.Size = new Size(108, 21);
            TransparencyCheckBox.TabIndex = 3;
            TransparencyCheckBox.Text = "Transparency";
            TransparencyCheckBox.UseVisualStyleBackColor = true;
            TransparencyCheckBox.MouseClick += TransparencyCheckBox_CheckedChanged;
            // 
            // GrayscaleCheckBox
            // 
            GrayscaleCheckBox.AutoSize = true;
            GrayscaleCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            GrayscaleCheckBox.ForeColor = SystemColors.Control;
            GrayscaleCheckBox.Location = new Point(14, 196);
            GrayscaleCheckBox.Name = "GrayscaleCheckBox";
            GrayscaleCheckBox.Size = new Size(85, 21);
            GrayscaleCheckBox.TabIndex = 2;
            GrayscaleCheckBox.Text = "Grayscale";
            GrayscaleCheckBox.UseVisualStyleBackColor = true;
            GrayscaleCheckBox.MouseClick += GrayscaleCheckBox_CheckedChanged;
            // 
            // NegativeCheckBox
            // 
            NegativeCheckBox.AutoSize = true;
            NegativeCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NegativeCheckBox.ForeColor = SystemColors.Control;
            NegativeCheckBox.Location = new Point(159, 90);
            NegativeCheckBox.Name = "NegativeCheckBox";
            NegativeCheckBox.Size = new Size(82, 21);
            NegativeCheckBox.TabIndex = 1;
            NegativeCheckBox.Text = "Negative";
            NegativeCheckBox.UseVisualStyleBackColor = true;
            NegativeCheckBox.MouseClick += NegativeCheckBox_CheckedChanged;
            // 
            // SepiaCheckBox
            // 
            SepiaCheckBox.AutoSize = true;
            SepiaCheckBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SepiaCheckBox.ForeColor = SystemColors.Control;
            SepiaCheckBox.Location = new Point(14, 90);
            SepiaCheckBox.Name = "SepiaCheckBox";
            SepiaCheckBox.Size = new Size(60, 21);
            SepiaCheckBox.TabIndex = 0;
            SepiaCheckBox.Text = "Sepia";
            SepiaCheckBox.UseVisualStyleBackColor = true;
            SepiaCheckBox.MouseClick += SepiaCheckBox_CheckedChanged;
            // 
            // ChannelsBox
            // 
            ChannelsBox.Controls.Add(BlueChannelLabel);
            ChannelsBox.Controls.Add(GreenChannelLabel);
            ChannelsBox.Controls.Add(RedChannelLabel);
            ChannelsBox.Controls.Add(BlueChannelSlider);
            ChannelsBox.Controls.Add(GreenChannelSlider);
            ChannelsBox.Controls.Add(RedChannelSlider);
            ChannelsBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ChannelsBox.ForeColor = SystemColors.Control;
            ChannelsBox.Location = new Point(3, 6);
            ChannelsBox.Name = "ChannelsBox";
            ChannelsBox.Padding = new Padding(6);
            ChannelsBox.Size = new Size(271, 217);
            ChannelsBox.TabIndex = 0;
            ChannelsBox.TabStop = false;
            ChannelsBox.Text = "Channels";
            // 
            // BlueChannelLabel
            // 
            BlueChannelLabel.AutoSize = true;
            BlueChannelLabel.Location = new Point(203, 185);
            BlueChannelLabel.Name = "BlueChannelLabel";
            BlueChannelLabel.Size = new Size(35, 17);
            BlueChannelLabel.TabIndex = 5;
            BlueChannelLabel.Text = "Blue";
            // 
            // GreenChannelLabel
            // 
            GreenChannelLabel.AutoSize = true;
            GreenChannelLabel.Location = new Point(118, 185);
            GreenChannelLabel.Name = "GreenChannelLabel";
            GreenChannelLabel.Size = new Size(44, 17);
            GreenChannelLabel.TabIndex = 4;
            GreenChannelLabel.Text = "Green";
            // 
            // RedChannelLabel
            // 
            RedChannelLabel.AutoSize = true;
            RedChannelLabel.Location = new Point(23, 185);
            RedChannelLabel.Name = "RedChannelLabel";
            RedChannelLabel.Size = new Size(31, 17);
            RedChannelLabel.TabIndex = 3;
            RedChannelLabel.Text = "Red";
            // 
            // BlueChannelSlider
            // 
            BlueChannelSlider.BackColor = Color.SteelBlue;
            BlueChannelSlider.Location = new Point(203, 27);
            BlueChannelSlider.Maximum = 100;
            BlueChannelSlider.Name = "BlueChannelSlider";
            BlueChannelSlider.Orientation = Orientation.Vertical;
            BlueChannelSlider.Size = new Size(45, 155);
            BlueChannelSlider.TabIndex = 2;
            BlueChannelSlider.TickStyle = TickStyle.None;
            BlueChannelSlider.Value = 100;
            BlueChannelSlider.MouseUp += Blue_Scroll;
            // 
            // GreenChannelSlider
            // 
            GreenChannelSlider.BackColor = Color.FromArgb(46, 103, 54);
            GreenChannelSlider.Location = new Point(118, 24);
            GreenChannelSlider.Maximum = 100;
            GreenChannelSlider.Name = "GreenChannelSlider";
            GreenChannelSlider.Orientation = Orientation.Vertical;
            GreenChannelSlider.Size = new Size(45, 158);
            GreenChannelSlider.TabIndex = 1;
            GreenChannelSlider.TickStyle = TickStyle.None;
            GreenChannelSlider.Value = 100;
            GreenChannelSlider.MouseUp += Green_Scroll;
            // 
            // RedChannelSlider
            // 
            RedChannelSlider.BackColor = Color.FromArgb(122, 73, 73);
            RedChannelSlider.Location = new Point(23, 24);
            RedChannelSlider.Maximum = 100;
            RedChannelSlider.Name = "RedChannelSlider";
            RedChannelSlider.Orientation = Orientation.Vertical;
            RedChannelSlider.RightToLeft = RightToLeft.No;
            RedChannelSlider.Size = new Size(45, 158);
            RedChannelSlider.TabIndex = 0;
            RedChannelSlider.TickStyle = TickStyle.None;
            RedChannelSlider.Value = 100;
            RedChannelSlider.MouseUp += Red_Scroll;
            // 
            // TopPanel
            // 
            TopPanel.Controls.Add(RedoButton);
            TopPanel.Controls.Add(UndoButton);
            TopPanel.Controls.Add(PencilButton);
            TopPanel.Controls.Add(MedianBlurButton);
            TopPanel.Controls.Add(GaussianBlurButton);
            TopPanel.Controls.Add(SaveButton);
            TopPanel.Controls.Add(OpenButton);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.Location = new Point(0, 0);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new Size(945, 27);
            TopPanel.TabIndex = 6;
            // 
            // RedoButton
            // 
            RedoButton.Enabled = false;
            RedoButton.FlatStyle = FlatStyle.Flat;
            RedoButton.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RedoButton.ForeColor = SystemColors.Control;
            RedoButton.Location = new Point(887, 3);
            RedoButton.Name = "RedoButton";
            RedoButton.Size = new Size(55, 23);
            RedoButton.TabIndex = 6;
            RedoButton.Text = "→";
            RedoButton.UseVisualStyleBackColor = true;
            RedoButton.Click += RedoButton_Click;
            // 
            // UndoButton
            // 
            UndoButton.Enabled = false;
            UndoButton.FlatStyle = FlatStyle.Flat;
            UndoButton.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UndoButton.ForeColor = SystemColors.Control;
            UndoButton.Location = new Point(825, 3);
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(59, 23);
            UndoButton.TabIndex = 5;
            UndoButton.Text = "←";
            UndoButton.UseVisualStyleBackColor = true;
            UndoButton.Click += UndoButton_Click;
            // 
            // PencilButton
            // 
            PencilButton.FlatStyle = FlatStyle.Flat;
            PencilButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PencilButton.ForeColor = SystemColors.Control;
            PencilButton.Location = new Point(381, 4);
            PencilButton.Name = "PencilButton";
            PencilButton.Size = new Size(102, 23);
            PencilButton.TabIndex = 4;
            PencilButton.Text = "Pencil";
            PencilButton.UseVisualStyleBackColor = true;
            PencilButton.Click += CartoonButton_Click;
            // 
            // MedianBlurButton
            // 
            MedianBlurButton.FlatStyle = FlatStyle.Flat;
            MedianBlurButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MedianBlurButton.ForeColor = SystemColors.Control;
            MedianBlurButton.Location = new Point(273, 4);
            MedianBlurButton.Name = "MedianBlurButton";
            MedianBlurButton.Size = new Size(102, 23);
            MedianBlurButton.TabIndex = 3;
            MedianBlurButton.Text = "Median Blur";
            MedianBlurButton.UseVisualStyleBackColor = true;
            MedianBlurButton.Click += MedianBlurButton_Click;
            // 
            // GaussianBlurButton
            // 
            GaussianBlurButton.FlatStyle = FlatStyle.Flat;
            GaussianBlurButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            GaussianBlurButton.ForeColor = SystemColors.Control;
            GaussianBlurButton.Location = new Point(165, 4);
            GaussianBlurButton.Name = "GaussianBlurButton";
            GaussianBlurButton.Size = new Size(102, 23);
            GaussianBlurButton.TabIndex = 2;
            GaussianBlurButton.Text = "Gaussian Blur";
            GaussianBlurButton.UseVisualStyleBackColor = true;
            GaussianBlurButton.Click += GaussianBlurButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SaveButton.ForeColor = SystemColors.Control;
            SaveButton.Location = new Point(84, 4);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 1;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // OpenButton
            // 
            OpenButton.FlatStyle = FlatStyle.Flat;
            OpenButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OpenButton.ForeColor = SystemColors.Control;
            OpenButton.Location = new Point(3, 4);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(75, 23);
            OpenButton.TabIndex = 0;
            OpenButton.Text = "Open";
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // BottomPanel
            // 
            BottomPanel.Controls.Add(ResetAllButton);
            BottomPanel.Controls.Add(SaturationLabel);
            BottomPanel.Controls.Add(ContrastLabel);
            BottomPanel.Controls.Add(BrightnessLabel);
            BottomPanel.Controls.Add(SaturationSlider);
            BottomPanel.Controls.Add(ContrastSlider);
            BottomPanel.Controls.Add(BrightnessSlider);
            BottomPanel.Dock = DockStyle.Bottom;
            BottomPanel.Location = new Point(0, 623);
            BottomPanel.Name = "BottomPanel";
            BottomPanel.Size = new Size(945, 118);
            BottomPanel.TabIndex = 7;
            // 
            // ResetAllButton
            // 
            ResetAllButton.FlatStyle = FlatStyle.Flat;
            ResetAllButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ResetAllButton.ForeColor = SystemColors.Control;
            ResetAllButton.Location = new Point(659, 19);
            ResetAllButton.Name = "ResetAllButton";
            ResetAllButton.Size = new Size(125, 40);
            ResetAllButton.TabIndex = 6;
            ResetAllButton.Text = "Reset All";
            ResetAllButton.UseVisualStyleBackColor = true;
            ResetAllButton.Click += ResetAllButton_Click;
            // 
            // SaturationLabel
            // 
            SaturationLabel.AutoSize = true;
            SaturationLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SaturationLabel.ForeColor = SystemColors.Control;
            SaturationLabel.Location = new Point(12, 73);
            SaturationLabel.Name = "SaturationLabel";
            SaturationLabel.Size = new Size(77, 20);
            SaturationLabel.TabIndex = 5;
            SaturationLabel.Text = "Saturation";
            // 
            // ContrastLabel
            // 
            ContrastLabel.AutoSize = true;
            ContrastLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ContrastLabel.ForeColor = SystemColors.Control;
            ContrastLabel.Location = new Point(12, 39);
            ContrastLabel.Name = "ContrastLabel";
            ContrastLabel.Size = new Size(64, 20);
            ContrastLabel.TabIndex = 4;
            ContrastLabel.Text = "Contrast";
            // 
            // BrightnessLabel
            // 
            BrightnessLabel.AutoSize = true;
            BrightnessLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BrightnessLabel.ForeColor = SystemColors.Control;
            BrightnessLabel.Location = new Point(12, 6);
            BrightnessLabel.Name = "BrightnessLabel";
            BrightnessLabel.Size = new Size(83, 21);
            BrightnessLabel.TabIndex = 3;
            BrightnessLabel.Text = "Brightness";
            // 
            // SaturationSlider
            // 
            SaturationSlider.Location = new Point(89, 73);
            SaturationSlider.Maximum = 200;
            SaturationSlider.Name = "SaturationSlider";
            SaturationSlider.Size = new Size(564, 45);
            SaturationSlider.TabIndex = 2;
            SaturationSlider.TickStyle = TickStyle.None;
            SaturationSlider.Value = 100;
            SaturationSlider.MouseUp += SaturationSlider_Scroll;
            // 
            // ContrastSlider
            // 
            ContrastSlider.Location = new Point(89, 39);
            ContrastSlider.Maximum = 200;
            ContrastSlider.Name = "ContrastSlider";
            ContrastSlider.Size = new Size(564, 45);
            ContrastSlider.TabIndex = 1;
            ContrastSlider.TickStyle = TickStyle.None;
            ContrastSlider.Value = 100;
            ContrastSlider.MouseUp += ContrastSlider_Scroll;
            // 
            // BrightnessSlider
            // 
            BrightnessSlider.Location = new Point(89, 6);
            BrightnessSlider.Maximum = 100;
            BrightnessSlider.Minimum = -100;
            BrightnessSlider.Name = "BrightnessSlider";
            BrightnessSlider.Size = new Size(564, 45);
            BrightnessSlider.TabIndex = 0;
            BrightnessSlider.TabStop = false;
            BrightnessSlider.TickStyle = TickStyle.None;
            BrightnessSlider.MouseUp += BrightnessSlider_Scroll;
            // 
            // LeftPanel
            // 
            LeftPanel.Dock = DockStyle.Left;
            LeftPanel.Location = new Point(0, 27);
            LeftPanel.Name = "LeftPanel";
            LeftPanel.Size = new Size(47, 596);
            LeftPanel.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(945, 741);
            Controls.Add(LeftPanel);
            Controls.Add(RightPanel);
            Controls.Add(TopPanel);
            Controls.Add(BottomPanel);
            Controls.Add(PictureBox);
            Name = "Editor";
            Text = "Editor";
            Load += Editor_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBox).EndInit();
            RightPanel.ResumeLayout(false);
            Filters.ResumeLayout(false);
            Filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PurplePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)BluePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DarkPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)TransparencyPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)GrayscalePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)NegativePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)SepiaPictureBox).EndInit();
            ChannelsBox.ResumeLayout(false);
            ChannelsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BlueChannelSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)GreenChannelSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)RedChannelSlider).EndInit();
            TopPanel.ResumeLayout(false);
            BottomPanel.ResumeLayout(false);
            BottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SaturationSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)ContrastSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)BrightnessSlider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PictureBox;
        private Panel RightPanel;
        private Panel TopPanel;
        private Panel BottomPanel;
        private Panel LeftPanel;
        private TrackBar BrightnessSlider;
        private TrackBar SaturationSlider;
        private TrackBar ContrastSlider;
        private Label ContrastLabel;
        private Label BrightnessLabel;
        private Label SaturationLabel;
        private GroupBox ChannelsBox;
        private TrackBar RedChannelSlider;
        private TrackBar BlueChannelSlider;
        private TrackBar GreenChannelSlider;
        private Label RedChannelLabel;
        private Label GreenChannelLabel;
        private Label BlueChannelLabel;
        private Panel Filters;
        private CheckBox SepiaCheckBox;
        private CheckBox NegativeCheckBox;
        private CheckBox TransparencyCheckBox;
        private CheckBox GrayscaleCheckBox;
        private RoundedButton ResetAllButton;
        private Button OpenButton;
        private OpenFileDialog openFileDialog1;
        private Button SaveButton;
        private PictureBox TransparencyPictureBox;
        private PictureBox GrayscalePictureBox;
        private PictureBox NegativePictureBox;
        private PictureBox SepiaPictureBox;
        private Button GaussianBlurButton;
        private Button MedianBlurButton;
        private Button PencilButton;
        private Button UndoButton;
        private Button RedoButton;

        private History history;
        private ImageOriginator originator;
        private PictureBox BluePictureBox;
        private CheckBox BlueCheckBox;
        private PictureBox DarkPictureBox;
        private CheckBox DarkCheckBox;
        private PictureBox PurplePictureBox;
        private CheckBox PurpleCheckBox;
    }
}