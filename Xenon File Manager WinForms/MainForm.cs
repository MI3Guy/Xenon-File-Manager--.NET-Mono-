//  
//  MainForm.cs
//  
//  Author:
//       John Bentley <pcguy49@yahoo.com>
// 
//  Copyright (c) 2011 John Bentley
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows.Forms;
using System.Drawing;
using Mono.Unix;
using WinTild;
using Xenon.PluginUtil;

namespace Xenon.FileManager.WinForms {
	public class MainForm : Form {
		public MainForm() {
			this.Text = Catalog.GetString("Xenon File Manager");
			this.Size = new Size(600, 450);
			this.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("XenonFileManagerWinForms.xenon.ico"));
			
			
			MenuStrip menuStrip = new MenuStrip();
			menuStrip.Parent = this;
			ToolStripMenuItem file = new ToolStripMenuItem("&File");
			menuStrip.Items.Add(file);
			ToolStripMenuItem newTab = new ToolStripMenuItem("New &Tab");
			newTab.ShortcutKeys = Keys.Control | Keys.T;
			file.DropDownItems.Add(newTab);
			MainMenuStrip = menuStrip;
			//menuStrip.RenderMode = ToolStripRenderMode.Custom;
			//Szotar.WindowsForms.ToolStripAeroRenderer rende = ;
			//menuStrip.Renderer = new Szotar.WindowsForms.ToolStripAeroRenderer(Szotar.WindowsForms.ToolbarTheme.Toolbar);
			//menuStrip.Renderer.
			
			ToolStripContainer toolStripContainer = new ToolStripContainer();
			toolStripContainer.Dock = DockStyle.Fill;
			//toolStripContainer.
			ToolStrip toolStrip = new ToolStrip();
			toolStrip.Items.Add("Back");
			toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
			toolStripContainer.TopToolStripPanel.Controls.Add(menuStrip);
			
			locationBar = new TextBox();
			locationBar.Dock = DockStyle.Top;
			toolStripContainer.ContentPanel.Controls.Add(locationBar);
			
			tabs = new XeTabControl();
			tabs.DrawTabButtonBox = false;
			tabs.HighlightTextInSelectedTab = true;
			tabs.ShowButtons = LazzyTab.ShowButtonEnum.Quit;
			tabs.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;
			tabs.Top = locationBar.Height;
			tabs.Height = toolStripContainer.ContentPanel.Height - toolStripContainer.ContentPanel.Padding.Top
				- toolStripContainer.ContentPanel.Padding.Bottom - locationBar.Height;
			tabs.Width = toolStripContainer.ContentPanel.Width - toolStripContainer.ContentPanel.Padding.Right
				- toolStripContainer.ContentPanel.Padding.Left + 2;
			toolStripContainer.ContentPanel.Controls.Add(tabs);
			
			this.Controls.Add(toolStripContainer);
			
			//Controls.Add(toolContainer);
		
			
			CommonUtil.DirectoryChanged += new DirectoryChangedEventHandler(this.OnDirectoryChanged);
			
			TabPage page = (TabPage)CommonUtil.LoadControlInstance();
			tabs.TabPages.Add(page);
		}
		
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			CommonUtil.HomeButtonClicked((IDisplayInterfaceControl)tabs.SelectedTab);
		}
		
		TextBox locationBar;
		XeTabControl tabs;
		
		protected void OnDirectoryChanged(object sender, DirectoryChangedEventArgs e) {
			//((TabLabel)nb.GetTabLabel((Widget)e.Control)).TextLabel.Text = e.DisplayPath;
			//locationBar.Text = e.FullPath;
			
			//SetActionStates();
			
			((TabPage)e.Control).Text = e.ShortPath;
			locationBar.Text = e.DisplayPath;
		}
	}
}

