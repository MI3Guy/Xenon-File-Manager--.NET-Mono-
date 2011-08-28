using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestForm
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
			
			foreach(TabPage page in lazzyTab1.TabPages) {
				page.Controls.Add(new Button());
				
			}
        }

        private void showCloseButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!showQuitButton.Checked)
            {
                lazzyTab1.ShowButtons = lazzyTab1.ShowButtons ^ WinTild.LazzyTab.ShowButtonEnum.Quit;
            }
            else
            {
                lazzyTab1.ShowButtons = lazzyTab1.ShowButtons | WinTild.LazzyTab.ShowButtonEnum.Quit;
            }

            lazzyTab1.Invalidate();
        }

        private void showOptionsButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!showOptionsButton.Checked)
            {
                lazzyTab1.ShowButtons = lazzyTab1.ShowButtons ^ WinTild.LazzyTab.ShowButtonEnum.Options;
            }
            else
            {
                lazzyTab1.ShowButtons = lazzyTab1.ShowButtons | WinTild.LazzyTab.ShowButtonEnum.Options;
            }

            lazzyTab1.Invalidate();
        }

        private void drawButtonBoxes_CheckedChanged(object sender, EventArgs e)
        {
            lazzyTab1.DrawTabButtonBox = drawButtonBoxes.Checked;

            lazzyTab1.Invalidate();
        }

        private void highlightTextInSelected_CheckedChanged(object sender, EventArgs e)
        {
            lazzyTab1.HighlightTextInSelectedTab = highlightTextInSelected.Checked;

            lazzyTab1.Invalidate();
        }

        private void enableContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            lazzyTab1.ShowRightClickMenu = enableRightClickMenu.Checked;

            lazzyTab1.Invalidate();
        }

        private void addTab_Click(object sender, EventArgs e)
        {
            lazzyTab1.AddTab();

            if (lazzyTab1.TabPages.Count > 1 && removeTab.Enabled == false)
            {
                removeTab.Enabled = true;
            }
        }

        private void removeTab_Click(object sender, EventArgs e)
        {
            lazzyTab1.RemoveTab();

            if (lazzyTab1.TabPages.Count < 2)
            {
                removeTab.Enabled = false;
            }
        }

        private void hotTrack_CheckedChanged(object sender, EventArgs e)
        {
            lazzyTab1.HotTrack = hotTrack.Checked;

            lazzyTab1.Invalidate();
        }

        private void ownerDrawFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (ownerDrawFixed.Checked)
            {
                lazzyTab1.DrawMode = TabDrawMode.OwnerDrawFixed;
            }
            else
            {
                lazzyTab1.DrawMode = TabDrawMode.Normal;
            }
        }
    }
}
