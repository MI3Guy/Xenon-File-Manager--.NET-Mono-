//  
//  XeTabControl.cs
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
using WinTild;
using Xenon.PluginUtil;

namespace Xenon.FileManager.WinForms {
	public class XeTabControl : LazzyTab {
		public XeTabControl() : base() {
			//newTabMenuItem.Click += new EventHandler(NewTab_Clicked2);
		}
		
		public override void AddTab() {
			//Console.WriteLine("HERE");
			TabPage newTab = (TabPage)CommonUtil.LoadControlInstance();
			newTab.UseVisualStyleBackColor = true;
			
			TabPages.Add(newTab);
			CommonUtil.HomeButtonClicked((IDisplayInterfaceControl)newTab);
			SelectedTab = newTab;
		}
	}
}

