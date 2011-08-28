/// LazzyTab: a flexible firefox-like tab control for windows forms.
/// Initial Project created by Peter Sbarski
/// Licensed under LGPL
///

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

internal class resfinder { }

namespace WinTild
{
    [ToolboxBitmap(typeof(resfinder), "WinTild.LazzyTab.png")]
    public class LazzyTab : System.Windows.Forms.TabControl
    {
        private int selectedQuitButton = -1;
        private int selectedOptionsButton = -1;
        private int mouseOverTab = -1;

        public enum ShowButtonEnum
        {
            None = 0x01,
            Quit = 0x02, //draw quit button
            Options = 0x04, //draw options button
            All = Quit | Options
        }

        private System.ComponentModel.Container components = null;

        public LazzyTab()
        {
            SizeMode = TabSizeMode.Fixed; //default fixed
            DrawMode = TabDrawMode.OwnerDrawFixed;
            //Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ItemSize = new Size(150, 18); //default item size
            HotTrack = true; //enable hot-track by default
            DrawTabButtonBox = true;
            AllowDrop = true;
            Multiline = false;

            // Draw string. Center the text.
            StringFlags.LineAlignment = StringAlignment.Center;
            StringFlags.Trimming = StringTrimming.EllipsisCharacter;
            StringFlags.FormatFlags = StringFormatFlags.NoWrap;
            
            InitializeComponent();
        }

        /// <summary>
        /// Clean up resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Context Menu for the Options Button

        protected ContextMenuStrip contextMenu;

        protected ToolStripMenuItem closeMenuItem;
        protected ToolStripMenuItem closeOtherTabsMenuItem;
        protected ToolStripMenuItem newTabMenuItem;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            contextMenu = new ContextMenuStrip();

            newTabMenuItem = new ToolStripMenuItem("&New Tab", null, NewTab_Click);
            closeMenuItem = new ToolStripMenuItem("&Close Tab", null, CloseTab_Click);
            closeOtherTabsMenuItem = new ToolStripMenuItem("Close &Other Tabs", null, CloseOtherTabs_Click);

            contextMenu.Items.Add(newTabMenuItem);
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(closeMenuItem);
            contextMenu.Items.Add(closeOtherTabsMenuItem);
        }

        private void NewTab_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void CloseTab_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void CloseOtherTabs_Click(object sender, EventArgs e)
        {
            TabPage CurrentTab = SelectedTab;

            TabPages.Clear();

            TabPages.Add(CurrentTab);
        }

        private void ShowContextMenu(Point MousePos)
        {
            //Disable "Close All Others" if there is only one tab open
            closeOtherTabsMenuItem.Enabled = true;
            closeMenuItem.Enabled = true;

            if (TabPages.Count <= 1)
            {
                closeOtherTabsMenuItem.Enabled = false;
                closeMenuItem.Enabled = false;
            }

            contextMenu.Show(this, MousePos);
        }

        #endregion

        #region Tab Operations
        public virtual void AddTab()
        {
            TabPage NewTab = new TabPage("Tab: " + TabPages.Count.ToString());
            NewTab.UseVisualStyleBackColor = true;

            TabPages.Add(NewTab);
            SelectedTab = NewTab;
        }

        public void RemoveTab()
        {
            int CurrentIndex = SelectedIndex;

            if (TabPages.Count > 1)
            {
                TabPages.RemoveAt(SelectedIndex);
            }
			
			// MI3: This is needed due to a mono redrawing bug.
			if(TabPages.Count < 2)
			{
				TabPages.Add("TMP");
				SelectedTab = TabPages[1];
				TabPages.RemoveAt(TabPages.Count - 1);
			}
			else
			{
				SelectedTab = TabPages[1];
			}
			
            // Focus on the next to the right of the one that was closed
            if (TabPages.Count > 1 && CurrentIndex > 0)
            {
                SelectedTab = TabPages[CurrentIndex - 1];
            }
			else {
				SelectedTab = TabPages[0];
			}
			
        }
        #endregion

        #region Properties

        private StringFormat _stringFlags = new StringFormat();
        public StringFormat StringFlags
        {
            get { return _stringFlags; }
            set { _stringFlags = value; }
        }

        private ShowButtonEnum _showButtons = ShowButtonEnum.All;
        public ShowButtonEnum ShowButtons
        {
            get { return _showButtons; }
            set { _showButtons = value; }
        }

        private bool _highlightTextInSelectedTab = false;
        public bool HighlightTextInSelectedTab
        {
            get { return _highlightTextInSelectedTab; }
            set { _highlightTextInSelectedTab = value; }
        }

        private bool _drawTabButtonBox = false;
        public bool DrawTabButtonBox
        {
            get { return _drawTabButtonBox; }
            set { _drawTabButtonBox = value; }
        }

        private bool _showRightClickMenu = true;
        public bool ShowRightClickMenu
        {
            get { return _showRightClickMenu; }
            set { _showRightClickMenu = value; }
        }

        private Color _QuitButtonColour = Color.LightPink;
        public Color QuitButtonColour
        {
            get { return _QuitButtonColour; }
            set { _QuitButtonColour = value; }
        }

        private Color _optionsButtonColour = Color.LightGreen;
        public Color OptionsButtonColour
        {
            get { return _optionsButtonColour; }
            set { _optionsButtonColour = value; }
        }

        #endregion

        #region Tab Button Drawing

        /// <summary>
        /// Returns the required width for a tab button based on the tab's width.
        /// Typically we only want to take approximately 10% of the height
        /// </summary>
        /// <param name="TabPage"></param>
        /// <returns></returns>
        private int GetTabButtonWidth(Size TabItemSize)
        {
            return Convert.ToInt32(TabItemSize.Width * 0.065);
        }

        /// <summary>
        /// Returns the required height for a tab button based on the tab's width.
        /// Typically we only want to take approximately 80% of the height
        /// </summary>
        /// <param name="TabPage"></param>
        /// <returns></returns>
        private int GetTabButtonHeight(Size TabItemSize)
        {
            return Convert.ToInt32(TabItemSize.Height * 0.55);
        }

        /// <summary>
        /// Typically we want there to be a 15% separation between the border and the button
        /// </summary>
        /// <param name="TabItemSize"></param>
        /// <returns></returns>
        private int GetTabButtonHorizontalMarginSeparation(Size TabItemSize)
        {
            return Convert.ToInt32(TabItemSize.Width * 0.12);
        }

        /// <summary>
        /// Separation between buttons on the tab
        /// We usually want this to be a fixed amount
        /// </summary>
        /// <param name="TabItemSize"></param>
        /// <returns></returns>
        private int GetTabButtonHorizontalSeparation(Size TabItemSize)
        {
            const int HorizontalSeparation = 5;

            if (TabItemSize.Width > (GetTabButtonWidth(TabItemSize) * 2))
            {
                return HorizontalSeparation;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Typically we want there to be a 10% separation between the top of the button and the button
        /// </summary>
        /// <param name="TabItemSize"></param>
        /// <returns></returns>
        private int GetTabButtonVerticalMarginSeparation(Size TabItemSize)
        {
            return Convert.ToInt32((TabItemSize.Height - GetTabButtonHeight(TabItemSize)) / (1.5f));
        }

        /// <summary>
        /// Returns the rectangle which will be used to draw the quit button
        /// </summary>
        /// <param name="CurrentTabRect"></param>
        /// <returns></returns>
        private Rectangle GetButtonRect(Rectangle CurrentTabRect, ShowButtonEnum Button)
        {
            int TabBoundsRightMargin = 0;
            Rectangle tabButtonRect;

            if (Alignment == TabAlignment.Bottom || Alignment == TabAlignment.Top)
            {
                TabBoundsRightMargin = CurrentTabRect.Right - GetTabButtonHorizontalMarginSeparation(ItemSize);
            }
            else
            {
                TabBoundsRightMargin = (CurrentTabRect.X + (CurrentTabRect.Width / 2)) - (GetTabButtonWidth(ItemSize) / 2);
            }

            tabButtonRect = new Rectangle(TabBoundsRightMargin, CurrentTabRect.Top + GetTabButtonVerticalMarginSeparation(ItemSize), GetTabButtonWidth(ItemSize), GetTabButtonHeight(ItemSize));

            if (Button == ShowButtonEnum.Quit)
            {
                return tabButtonRect;
            }

            if ((ShowButtons & ShowButtonEnum.Quit) == ShowButtonEnum.Quit)
            {
                if (Alignment == TabAlignment.Bottom || Alignment == TabAlignment.Top) //if the quit button exists then we should move the options button out of the way
                {
                    tabButtonRect.X -= GetTabButtonWidth(ItemSize) + GetTabButtonHorizontalSeparation(ItemSize);
                }
                else
                {
                    tabButtonRect.Y += GetTabButtonHeight(ItemSize) + GetTabButtonHorizontalSeparation(ItemSize);
                }
            }

            return tabButtonRect;
        }

        private void DrawTab(int index, Graphics g)
        {
            Brush textBrush = new System.Drawing.SolidBrush(Color.Black);
            VisualStyleRenderer render = null;

            // Get the item from the collection.
            TabPage currentTabPage = this.TabPages[index];

            // Get the real bounds for the tab rectangle.
            Rectangle tabBounds = this.GetTabRect(index);

            // Get the real bounds for the area where we can paint our text
            // For time being this is the same as the tab rectangle
            // However if we add buttons the width of this rectangle will become smaller
            Rectangle textTabBounds = tabBounds;

            if (index == SelectedIndex)
            {
                render = new VisualStyleRenderer(VisualStyleElement.Tab.TopTabItem.Hot);
            }
            else if (index == mouseOverTab && HotTrack) //if the mouse is over the tab and HotTrack is enabled
            {
                render = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Hot);
                tabBounds.Height += 1;
                tabBounds.Y -= 1;
            }
            else
            {
                render = new VisualStyleRenderer(VisualStyleElement.Tab.TabItem.Normal);
                tabBounds.Height += 1;
                tabBounds.Y -= 1;
            }

            render.DrawBackground(g, tabBounds);

            // Draw the quit button if the user wants to
            if ((ShowButtons & ShowButtonEnum.Quit) == ShowButtonEnum.Quit && TabPages.Count > 1)
            {
                DrawTabButton(index, g, ref tabBounds, ref textTabBounds, QuitButtonColour, Color.Red, selectedQuitButton, ShowButtonEnum.Quit);
            }

            if ((ShowButtons & ShowButtonEnum.Options) == ShowButtonEnum.Options)
            {
                DrawTabButton(index, g, ref tabBounds, ref textTabBounds, OptionsButtonColour, Color.Blue, selectedOptionsButton, ShowButtonEnum.Options);
            }

            Font DrawingFont = this.Font;

            if (this.SelectedIndex == index && HighlightTextInSelectedTab)
            {
                //DrawingFont = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
				DrawingFont = new Font(this.Font, FontStyle.Bold);
            }

            StringFormat writeStringFlag = StringFlags;

            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
            {
                writeStringFlag.FormatFlags |= StringFormatFlags.DirectionVertical;
            }
            else
            {
                if ((writeStringFlag.FormatFlags & StringFormatFlags.DirectionVertical) == StringFormatFlags.DirectionVertical)
                {
                    writeStringFlag.FormatFlags ^= StringFormatFlags.DirectionVertical;
                }
            }

            g.DrawString(currentTabPage.Text, DrawingFont, textBrush, textTabBounds, new StringFormat(writeStringFlag));
        }

        private void DrawTabButton(int index, Graphics g, ref Rectangle tabBounds, ref Rectangle textTabBounds, Color ButtonColour, Color SelectedButtonColour, int ButtonSelected, ShowButtonEnum Button)
        {
            Pen buttonPen;

            // Get the bounds of the quit button based on the size of the tab
            Rectangle buttonBounds = GetButtonRect(tabBounds, Button);

            // If the user has his mouse over the button then set the foreground pen to...
            if (index == ButtonSelected)
            {
                buttonPen = new Pen(SelectedButtonColour); //set it to red
            }
            else
            {
                buttonPen = new Pen(Color.Black); //otherwise set it to standard black
            }

            // If the user wants to draw the background and the box, do it
            if (DrawTabButtonBox)
            {
                g.FillRectangle(new SolidBrush(ButtonColour), buttonBounds);
                g.DrawRectangle(buttonPen, buttonBounds);
            }

            // Make the rectangle a little bit smaller for painting the crosshair
            buttonBounds = Rectangle.Inflate(buttonBounds, -2, -2);

            if (Button == ShowButtonEnum.Quit)
            {
                // Draw the crosshair
                g.DrawLine(buttonPen, new Point(buttonBounds.Left, buttonBounds.Top), new Point(buttonBounds.Right, buttonBounds.Bottom));
                g.DrawLine(buttonPen, new Point(buttonBounds.Left, buttonBounds.Bottom), new Point(buttonBounds.Right, buttonBounds.Top));
            }
            else
            {
                g.DrawLine(buttonPen, new Point(buttonBounds.Left, buttonBounds.Top), new Point(buttonBounds.X + buttonBounds.Width / 2, buttonBounds.Bottom));
                g.DrawLine(buttonPen, new Point(buttonBounds.Right, buttonBounds.Top), new Point(buttonBounds.X + buttonBounds.Width / 2, buttonBounds.Bottom));
            }

            // Set the text boundary rectangle to
            if (Alignment == TabAlignment.Right || Alignment == TabAlignment.Left)
            {
                textTabBounds = new Rectangle(new Point(textTabBounds.X, buttonBounds.Bottom + GetTabButtonVerticalMarginSeparation(ItemSize)), new Size(textTabBounds.Width, textTabBounds.Height - buttonBounds.Height));
            }
            else
            {
                textTabBounds = new Rectangle(textTabBounds.Location, new Size(buttonBounds.X - textTabBounds.X, textTabBounds.Height));
            }
        }

        #endregion

        #region Overriden Methods

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            DrawTab(e.Index, e.Graphics);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Point MousePos = new Point(e.X, e.Y);

            //If the user clicked on the mouse button, close it.
            for (int i = 0; i < TabPages.Count; i++)
            {
                Rectangle CurrentTabRect = this.GetTabRect(i);

                if ((ShowButtons & ShowButtonEnum.Quit) == ShowButtonEnum.Quit)
                {
                    if (GetButtonRect(CurrentTabRect, ShowButtonEnum.Quit).Contains(MousePos))
                    {
                        RemoveTab();

                        break;
                    }
                }

                if ((ShowButtons & ShowButtonEnum.Options) == ShowButtonEnum.Options)
                {
                    if (GetButtonRect(CurrentTabRect, ShowButtonEnum.Options).Contains(MousePos))
                    {
                        ShowContextMenu(MousePos);

                        break;
                    }                       
                }

                //Right mouse click is true
                if (e.Button == MouseButtons.Right && ShowRightClickMenu == true)
                {
                    ShowContextMenu(MousePos);

                    break;
                }
            }
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            // de-hottrack the bar
            mouseOverTab = -1; 
            
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouseOverTab = -1;

            int oldQuitButtonState = selectedQuitButton;
            int oldOptionsButtonState = selectedOptionsButton;

            base.OnMouseMove(e);

            Point MousePos = new Point(e.X, e.Y);

            //Check if the mouse is over the close button or the drop-down button
            for (int i = 0; i < TabPages.Count; i++)
            {
                Rectangle CurrentTabRect = this.GetTabRect(i);

                if (CurrentTabRect.Contains(MousePos))
                {
                    mouseOverTab = i;
                }

                if ((ShowButtons & ShowButtonEnum.Quit) == ShowButtonEnum.Quit)
                {
                    if (GetButtonRect(CurrentTabRect, ShowButtonEnum.Quit).Contains(MousePos))
                    {
                        selectedQuitButton = i;
                        break;
                    }
                }

                if ((ShowButtons & ShowButtonEnum.Options) == ShowButtonEnum.Options)
                {
                    if (GetButtonRect(CurrentTabRect, ShowButtonEnum.Options).Contains(MousePos))
                    {
                        selectedOptionsButton = i;
                        break;
                    }
                }

                selectedQuitButton = -1;
                selectedOptionsButton = -1;
            }

            //Really, don't invalidate needlessly
            if (selectedQuitButton != oldQuitButtonState || selectedOptionsButton != oldOptionsButtonState)
            {
                this.Invalidate();
            }
        }

        #endregion
    }
}
