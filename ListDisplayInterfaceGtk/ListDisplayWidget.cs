using System;
using System.Collections.Generic;
using Gtk;
using PluginUtil;

namespace ListDisplayInterfaceGtk {
	public class ListDisplayWidget : ScrolledWindow, IDisplayInterfaceControl {
		public ListDisplayWidget() {
			historyBack = new Stack<Uri>();
			historyForward = new Stack<Uri>();
			
			mainTreeView = new TreeView();
				TreeViewColumn col = new TreeViewColumn();
				col.Title = "Name";
				col.Sizing = TreeViewColumnSizing.Autosize;
				mainTreeView.AppendColumn(col);
				Gtk.CellRendererText nameCell = new Gtk.CellRendererText();
				col.PackStart(nameCell, true);
				//col.AddAttribute(nameCell, "text", 0);
				col.SetCellDataFunc(nameCell, new TreeCellDataFunc(RenderFileName));
				col = new TreeViewColumn();
				col.Title = "Size";
				mainTreeView.AppendColumn(col);
				Gtk.CellRendererText sizeCell = new Gtk.CellRendererText();
				col.PackStart(sizeCell, true);
				//col.AddAttribute(sizeCell, "text", 1);
				col.SetCellDataFunc(sizeCell, new TreeCellDataFunc(RenderFileSize));
				col = new TreeViewColumn();
				col.Title = "Date";
				mainTreeView.AppendColumn(col);
				Gtk.CellRendererText dateCell = new Gtk.CellRendererText();
				col.PackStart(dateCell, true);
				col.AddAttribute(dateCell, "text", 2);
				col.SetCellDataFunc(dateCell, new TreeCellDataFunc(RenderFileDateModified));
				listStore = new ListStore(typeof(XeFileInfo));
				//listStore.AppendValues("test.txt", "50 KB", "Today");
				//listStore.AppendValues("test2.txt", "51 KB", "Yesterday");
			mainTreeView.Model = listStore;
			TreeModelFilter filter = new TreeModelFilter(listStore, null);
			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc(FilterList);
			mainTreeView.Model = filter;
			mainTreeView.RowActivated += new RowActivatedHandler(OnRowActivated);
			
			Add(mainTreeView);
		}
		
		private TreeView mainTreeView;
		private ListStore listStore;
		private Stack<Uri> historyBack;
		private Stack<Uri> historyForward;
		private Uri currentLocation;
		
		public void SetContent(IEnumerable<XeFileInfo> fileList, Uri path) {
			listStore.Clear();
			foreach(XeFileInfo fi in fileList) {
				listStore.AppendValues(fi);
			}
			CommonUtil.NotifyDirectoryChanged(this, new DirectoryChangedEventArgs(path, this));
		}
		
		public Stack<Uri> HistoryBack {
			get { return historyBack; }
			set { historyBack = value; }
		}
		
		public Stack<Uri> HistoryForward {
			get { return historyForward; }
			set { historyForward = value; }
		}
		
		public Uri CurrentLocation {
			get { return currentLocation; }
			set { currentLocation = value; }
		}
		
		public void OnRowActivated(object sender, RowActivatedArgs args) {
			TreeIter iter;
			
			if(!mainTreeView.Model.GetIter(out iter, args.Path)) { return; }
			
			CommonUtil.LoadDirectory(((XeFileInfo)mainTreeView.Model.GetValue(iter, 0)).FullPath, this);
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
			try {
				var text = model.GetValue(iter, 0);
				string text2 = (text as XeFileInfo).Name;//.ToString();
				Console.WriteLine("NotHERE");
				if(text2[0] == '.')
					return false;
				else
					return true;
			}
			catch { Console.WriteLine("HERE"); return true; }
		}
	}
}

