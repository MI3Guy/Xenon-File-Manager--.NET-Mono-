namespace TestForm
{
    partial class TestForm
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
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ownerDrawFixed = new System.Windows.Forms.CheckBox();
            this.hotTrack = new System.Windows.Forms.CheckBox();
            this.removeTab = new System.Windows.Forms.Button();
            this.addTab = new System.Windows.Forms.Button();
            this.enableRightClickMenu = new System.Windows.Forms.CheckBox();
            this.highlightTextInSelected = new System.Windows.Forms.CheckBox();
            this.drawButtonBoxes = new System.Windows.Forms.CheckBox();
            this.showOptionsButton = new System.Windows.Forms.CheckBox();
            this.showQuitButton = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.lazzyTab1 = new WinTild.LazzyTab();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.lazzyTab1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lazzyTab1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(456, 359);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ownerDrawFixed);
            this.groupBox1.Controls.Add(this.hotTrack);
            this.groupBox1.Controls.Add(this.removeTab);
            this.groupBox1.Controls.Add(this.addTab);
            this.groupBox1.Controls.Add(this.enableRightClickMenu);
            this.groupBox1.Controls.Add(this.highlightTextInSelected);
            this.groupBox1.Controls.Add(this.drawButtonBoxes);
            this.groupBox1.Controls.Add(this.showOptionsButton);
            this.groupBox1.Controls.Add(this.showQuitButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // ownerDrawFixed
            // 
            this.ownerDrawFixed.AutoSize = true;
            this.ownerDrawFixed.Checked = true;
            this.ownerDrawFixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ownerDrawFixed.Location = new System.Drawing.Point(203, 20);
            this.ownerDrawFixed.Name = "ownerDrawFixed";
            this.ownerDrawFixed.Size = new System.Drawing.Size(113, 17);
            this.ownerDrawFixed.TabIndex = 8;
            this.ownerDrawFixed.Text = "Owner Draw Fixed";
            this.ownerDrawFixed.UseVisualStyleBackColor = true;
            this.ownerDrawFixed.CheckedChanged += new System.EventHandler(this.ownerDrawFixed_CheckedChanged);
            // 
            // hotTrack
            // 
            this.hotTrack.AutoSize = true;
            this.hotTrack.Checked = true;
            this.hotTrack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hotTrack.Location = new System.Drawing.Point(7, 139);
            this.hotTrack.Name = "hotTrack";
            this.hotTrack.Size = new System.Drawing.Size(71, 17);
            this.hotTrack.TabIndex = 7;
            this.hotTrack.Text = "HotTrack";
            this.hotTrack.UseVisualStyleBackColor = true;
            this.hotTrack.CheckedChanged += new System.EventHandler(this.hotTrack_CheckedChanged);
            // 
            // removeTab
            // 
            this.removeTab.Location = new System.Drawing.Point(336, 49);
            this.removeTab.Name = "removeTab";
            this.removeTab.Size = new System.Drawing.Size(111, 23);
            this.removeTab.TabIndex = 6;
            this.removeTab.Text = "Remove Tab";
            this.removeTab.UseVisualStyleBackColor = true;
            this.removeTab.Click += new System.EventHandler(this.removeTab_Click);
            // 
            // addTab
            // 
            this.addTab.Location = new System.Drawing.Point(336, 20);
            this.addTab.Name = "addTab";
            this.addTab.Size = new System.Drawing.Size(111, 23);
            this.addTab.TabIndex = 5;
            this.addTab.Text = "Add Tab";
            this.addTab.UseVisualStyleBackColor = true;
            this.addTab.Click += new System.EventHandler(this.addTab_Click);
            // 
            // enableRightClickMenu
            // 
            this.enableRightClickMenu.AutoSize = true;
            this.enableRightClickMenu.Checked = true;
            this.enableRightClickMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableRightClickMenu.Location = new System.Drawing.Point(7, 116);
            this.enableRightClickMenu.Name = "enableRightClickMenu";
            this.enableRightClickMenu.Size = new System.Drawing.Size(143, 17);
            this.enableRightClickMenu.TabIndex = 4;
            this.enableRightClickMenu.Text = "Enable Right Click Menu";
            this.enableRightClickMenu.UseVisualStyleBackColor = true;
            this.enableRightClickMenu.CheckedChanged += new System.EventHandler(this.enableContextMenu_CheckedChanged);
            // 
            // highlightTextInSelected
            // 
            this.highlightTextInSelected.AutoSize = true;
            this.highlightTextInSelected.Location = new System.Drawing.Point(7, 92);
            this.highlightTextInSelected.Name = "highlightTextInSelected";
            this.highlightTextInSelected.Size = new System.Drawing.Size(147, 17);
            this.highlightTextInSelected.TabIndex = 3;
            this.highlightTextInSelected.Text = "Highlight Text in Selected";
            this.highlightTextInSelected.UseVisualStyleBackColor = true;
            this.highlightTextInSelected.CheckedChanged += new System.EventHandler(this.highlightTextInSelected_CheckedChanged);
            // 
            // drawButtonBoxes
            // 
            this.drawButtonBoxes.AutoSize = true;
            this.drawButtonBoxes.Checked = true;
            this.drawButtonBoxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawButtonBoxes.Location = new System.Drawing.Point(7, 68);
            this.drawButtonBoxes.Name = "drawButtonBoxes";
            this.drawButtonBoxes.Size = new System.Drawing.Size(117, 17);
            this.drawButtonBoxes.TabIndex = 2;
            this.drawButtonBoxes.Text = "Draw Button Boxes";
            this.drawButtonBoxes.UseVisualStyleBackColor = true;
            this.drawButtonBoxes.CheckedChanged += new System.EventHandler(this.drawButtonBoxes_CheckedChanged);
            // 
            // showOptionsButton
            // 
            this.showOptionsButton.AutoSize = true;
            this.showOptionsButton.Checked = true;
            this.showOptionsButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOptionsButton.Location = new System.Drawing.Point(7, 44);
            this.showOptionsButton.Name = "showOptionsButton";
            this.showOptionsButton.Size = new System.Drawing.Size(126, 17);
            this.showOptionsButton.TabIndex = 1;
            this.showOptionsButton.Text = "Show Options Button";
            this.showOptionsButton.UseVisualStyleBackColor = true;
            this.showOptionsButton.CheckedChanged += new System.EventHandler(this.showOptionsButton_CheckedChanged);
            // 
            // showQuitButton
            // 
            this.showQuitButton.AutoSize = true;
            this.showQuitButton.Checked = true;
            this.showQuitButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showQuitButton.Location = new System.Drawing.Point(7, 20);
            this.showQuitButton.Name = "showQuitButton";
            this.showQuitButton.Size = new System.Drawing.Size(109, 17);
            this.showQuitButton.TabIndex = 0;
            this.showQuitButton.Text = "Show Quit Button";
            this.showQuitButton.UseVisualStyleBackColor = true;
            this.showQuitButton.CheckedChanged += new System.EventHandler(this.showCloseButton_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(858, 170);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(858, 170);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(858, 170);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(858, 170);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(858, 170);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(858, 170);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(858, 170);
            this.tabPage7.TabIndex = 5;
            this.tabPage7.Text = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // lazzyTab1
            // 
            this.lazzyTab1.AllowDrop = true;
            this.lazzyTab1.Controls.Add(this.tabPage8);
            this.lazzyTab1.Controls.Add(this.tabPage9);
            this.lazzyTab1.Controls.Add(this.tabPage10);
            this.lazzyTab1.Controls.Add(this.tabPage11);
            this.lazzyTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lazzyTab1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.lazzyTab1.DrawTabButtonBox = true;
            this.lazzyTab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lazzyTab1.HighlightTextInSelectedTab = false;
            this.lazzyTab1.HotTrack = true;
            this.lazzyTab1.ItemSize = new System.Drawing.Size(150, 18);
            this.lazzyTab1.Location = new System.Drawing.Point(0, 0);
            this.lazzyTab1.Name = "lazzyTab1";
            this.lazzyTab1.OptionsButtonColour = System.Drawing.Color.LightGreen;
            this.lazzyTab1.QuitButtonColour = System.Drawing.Color.LightPink;
            this.lazzyTab1.SelectedIndex = 0;
            this.lazzyTab1.ShowButtons = WinTild.LazzyTab.ShowButtonEnum.All;
            this.lazzyTab1.ShowRightClickMenu = true;
            this.lazzyTab1.Size = new System.Drawing.Size(456, 196);
            this.lazzyTab1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
            stringFormat1.FormatFlags = System.Drawing.StringFormatFlags.NoWrap;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
            this.lazzyTab1.StringFlags = stringFormat1;
            this.lazzyTab1.TabIndex = 0;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(448, 170);
            this.tabPage8.TabIndex = 0;
            this.tabPage8.Text = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(192, 74);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "tabPage9";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(448, 170);
            this.tabPage10.TabIndex = 2;
            this.tabPage10.Text = "tabPage10";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(448, 170);
            this.tabPage11.TabIndex = 3;
            this.tabPage11.Text = "tabPage11";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 359);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestForm";
            this.Text = "LazzyTab TestForm";
            this.TransparencyKey = System.Drawing.Color.GreenYellow;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.lazzyTab1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void splitContainer1_Resize(object sender, System.EventArgs e)
        {
            lazzyTab1.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox showOptionsButton;
        private System.Windows.Forms.CheckBox showQuitButton;
        private System.Windows.Forms.CheckBox drawButtonBoxes;
        private System.Windows.Forms.CheckBox highlightTextInSelected;
        private System.Windows.Forms.CheckBox enableRightClickMenu;
        private System.Windows.Forms.Button addTab;
        private System.Windows.Forms.Button removeTab;
        private System.Windows.Forms.CheckBox hotTrack;
        private System.Windows.Forms.CheckBox ownerDrawFixed;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private WinTild.LazzyTab lazzyTab1;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;






    }
}

