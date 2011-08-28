//  
//  ListDisplayWidgetWinForms.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using Xenon.PluginUtil;

namespace Xenon.Plugin.ListDisplayInterfaceWinForms {
	public class ListDisplayControl : TabPage, IDisplayInterfaceControl {
		public ListDisplayControl() : base("Page1") {
			HistoryBack = new CacheStack<Uri>(CommonUtil.HistoryNumItems, CommonUtil.HistoryTrimNum);
			HistoryForward = new CacheStack<Uri>(CommonUtil.HistoryNumItems, CommonUtil.HistoryTrimNum);
			
			lv = new ListView();
			lv.Dock = DockStyle.Fill;
			lv.View = View.Details;
			lv.AllowColumnReorder = false;
			lv.FullRowSelect = true;
			
			lv.Columns.Add("Name", 200);
			lv.Columns.Add("Size", 100);
			lv.Columns.Add("Date Modified", 150);
			
			lv.ItemActivate += new EventHandler(OnItemActivated);
			
			this.Controls.Add(lv);
		}
		
		public CacheStack<Uri> HistoryBack { get; set; }
		public CacheStack<Uri> HistoryForward { get; set; }
		public Uri CurrentLocation { get; set; }
		
		public ListView lv;
		List<XeFileInfo> localFileList = new List<XeFileInfo>();
		
		public void SetContent(IEnumerable<XeFileInfo> fileList, Uri path) {
			//this.DataSource = from file in fileList select new {
			//	Name = file.Name,
			//	Size = file.FormattedSize,
			//	Date_Modified = file.DateModified.ToString()
			//};
			lv.Items.Clear();
			localFileList = fileList as List<XeFileInfo> ?? fileList.ToList();
			foreach(XeFileInfo fi in fileList) lv.Items.Add(new ListViewItem(new string[] { fi.Name, fi.FormattedSize, fi.DateModified.ToString() }));
			CommonUtil.NotifyDirectoryChanged(this, new DirectoryChangedEventArgs(path, this));
			lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		public void OnItemActivated(object sender, EventArgs e) {
			CommonUtil.LoadDirectory(localFileList[lv.SelectedIndices[0]].FullPath, this);
		}
	}
}

