// 
//  ListDisplayWidget.cs
//  
//  Author:
//       John Bentley <pcguy49@yahoo.com>
//  
//  Copyright (c) 2011 John Bentley
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using Xenon.PluginUtil;

namespace Xenon.Plugin.ListDisplayInterfaceGtk {
	public class ListDisplayWidget : ScrolledWindow, IDisplayInterfaceControl {
		public ListDisplayWidget() {
			historyBack = new CacheStack<Uri>(CommonUtil.HistoryNumItems, CommonUtil.HistoryTrimNum);
			historyForward = new CacheStack<Uri>(CommonUtil.HistoryNumItems, CommonUtil.HistoryTrimNum);
			
			mainTreeView = new TreeView();
			mainTreeView.Selection.Mode = SelectionMode.Multiple;
				TreeViewColumn col = new TreeViewColumn();
				col.Title = "";
				col.Sizing = TreeViewColumnSizing.Autosize;
				mainTreeView.AppendColumn(col);
				CellRendererPixbuf iconCell = new CellRendererPixbuf();
				col.PackStart(iconCell, true);
				col.SetCellDataFunc(iconCell, new TreeCellDataFunc(RenderIcon));
				col = new TreeViewColumn();
				col.Title = "Name";
				col.Sizing = TreeViewColumnSizing.Autosize;
				mainTreeView.AppendColumn(col);
				Gtk.CellRendererText nameCell = new Gtk.CellRendererText();
				col.PackStart(nameCell, true);
				col.SetCellDataFunc(nameCell, new TreeCellDataFunc(RenderFileName));
				col = new TreeViewColumn();
				col.Title = "Size";
				mainTreeView.AppendColumn(col);
				Gtk.CellRendererText sizeCell = new Gtk.CellRendererText();
				col.PackStart(sizeCell, true);
				col.SetCellDataFunc(sizeCell, new TreeCellDataFunc(RenderFileSize));
				col = new TreeViewColumn();
				col.Title = "Date";
				mainTreeView.AppendColumn(col);
				Gtk.CellRendererText dateCell = new Gtk.CellRendererText();
				col.PackStart(dateCell, true);
				col.AddAttribute(dateCell, "text", 2);
				col.SetCellDataFunc(dateCell, new TreeCellDataFunc(RenderFileDateModified));
				listStore = new ListStore(typeof(XeFileInfo));
			mainTreeView.Model = listStore;
			TreeModelFilter filter = new TreeModelFilter(listStore, null);
			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc(FilterList);
			mainTreeView.Model = filter;
			mainTreeView.RowActivated += new RowActivatedHandler(OnRowActivated);
			
			Add(mainTreeView);
		}
		
		private TreeView mainTreeView;
		private ListStore listStore;
		private CacheStack<Uri> historyBack;
		private CacheStack<Uri> historyForward;
		private Uri currentLocation;
		
		public CacheStack<Uri> HistoryBack {
			get { return historyBack; }
			set { historyBack = value; }
		}
		
		public CacheStack<Uri> HistoryForward {
			get { return historyForward; }
			set { historyForward = value; }
		}
		
		public Uri CurrentLocation {
			get { return currentLocation; }
			set { currentLocation = value; }
		}
		
		public void SetContent(IEnumerable<XeFileInfo> fileList, Uri path) {
			listStore.Clear();
			int i = 0;
			foreach(XeFileInfo fi in fileList) {
				listStore.AppendValues(fi);
				++i;
			}
			CommonUtil.NotifyDirectoryChanged(this, new DirectoryChangedEventArgs(path, this));
		}
		
		public IEnumerable<XeFileInfo> SelectedFiles {
			get { return from row in mainTreeView.Selection.GetSelectedRows() where ((XeFileInfo)mainTreeView.Model.GetValue(GetIterForPath(mainTreeView.Model, row), 0)).Name != ".." select (XeFileInfo)mainTreeView.Model.GetValue(GetIterForPath(mainTreeView.Model, row), 0); }
		}
		
		public TreeIter GetIterForPath(this TreeModel model, TreePath path) {
			TreeIter iter;
			mainTreeView.Model.GetIter(out iter, path);
			return iter;
		}
		
		public void Rename() {
			
		}
			
		
		public void SelectAll() {
			mainTreeView.Selection.SelectAll();
			TreeIter first;
			mainTreeView.Model.GetIterFirst(out first);
			//Console.WriteLine("HEAR");
			if(((XeFileInfo)mainTreeView.Model.GetValue(first, 0)).Name == "..") mainTreeView.Selection.UnselectIter(first);
		}
		
		public void SelectNone() {
			mainTreeView.Selection.UnselectAll();
		}
		
		public void OnRowActivated(object sender, RowActivatedArgs args) {
			TreeIter iter;
			
			if(!mainTreeView.Model.GetIter(out iter, args.Path)) { return; }
			XeFileInfo file = (XeFileInfo)mainTreeView.Model.GetValue(iter, 0);
			if(file.IsFile) return;
			
			CommonUtil.LoadDirectory(file.FullPath, this);
		}
			
		private void RenderIcon(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter) {
			try {
				XeFileInfo file = (XeFileInfo) model.GetValue(iter, 0);
				(cell as CellRendererPixbuf).Pixbuf = ((IconSet)file.Icon).RenderIcon(new Style(), TextDirection.None, StateType.Normal, IconSize.SmallToolbar, mainTreeView, "");
			}
			catch(Exception ex) { Console.WriteLine(ex);}
		}
		
		private void RenderFileName(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter) {
			XeFileInfo file = (XeFileInfo) model.GetValue(iter, 0);
			(cell as Gtk.CellRendererText).Text = file.Name;
		}
		
		private void RenderFileSize(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter) {
			XeFileInfo file = (XeFileInfo) model.GetValue(iter, 0);
			(cell as Gtk.CellRendererText).Text = file.FormattedSize;
		}
		
		private void RenderFileDateModified(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter) {
			XeFileInfo file = (XeFileInfo) model.GetValue(iter, 0);
			(cell as Gtk.CellRendererText).Text = file.DateModified.ToString();
		}
		
		private bool FilterList(Gtk.TreeModel model, Gtk.TreeIter iter) {
			if((bool)SettingsUtil.MainSettings["showhidden"].data) { return true; }
			try {
				var item = model.GetValue(iter, 0);
				XeFileInfo xeiitem = item as XeFileInfo;
				if(xeiitem.IsHidden)
					return false;
				else
					return true;
			}
			catch { return true; }
		}
	}
}

