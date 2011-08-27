using System;
using System.Collections.Generic;
using Gtk;
using PluginUtil;
using System.Diagnostics;
using System.IO;
namespace ListDisplayInterfaceGtk {
	public class ListDisplayWidget : ScrolledWindow, IDisplayInterfaceControl {
		public ListDisplayWidget() {
			historyBack = new CacheStack<Uri>(CommonUtil.HistoryNumItems, CommonUtil.HistoryTrimNum);
			historyForward = new CacheStack<Uri>(CommonUtil.HistoryNumItems, CommonUtil.HistoryTrimNum);
			
			mainTreeView = new TreeView();
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
		private CacheStack<Uri> historyBack;
		private CacheStack<Uri> historyForward;
		private Uri currentLocation;
		
		public void SetContent(IEnumerable<XeFileInfo> fileList, Uri path) {
			listStore.Clear();
			foreach(XeFileInfo fi in fileList) {
				listStore.AppendValues(fi);
			}
			CommonUtil.NotifyDirectoryChanged(this, new DirectoryChangedEventArgs(path, this));
		}
		
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
				//Console.WriteLine(((Gtk.Image)Image.NewFromIconName("computer", IconSize.Dialog)).StorageType.ToString());
				Console.WriteLine("HERE2");
				
				/*ProcessStartInfo psi = new ProcessStartInfo("xdg-mime", string.Format("query filetype '{0}'", file.FullPath));
				psi.RedirectStandardOutput = true;
				psi.UseShellExecute = false;
				Process p = Process.Start(psi);
				p.WaitForExit();
				StreamReader sr = p.StandardOutput;
				
				string text = sr.ReadLine();
				
				IconSet iconset = new IconSet();
				IconSource source = new Gtk.IconSource();
				source.IconName = text.Replace('/', '-'); //"inode-directory";
				//Console.WriteLine("{0}: {1}", psi.Arguments, text);
				iconset.AddSource(source);*/
				
				(cell as CellRendererPixbuf).Pixbuf = ((IconSet)file.Icon).RenderIcon(new Style(), TextDirection.None, StateType.Normal, IconSize.SmallToolbar, mainTreeView, ""); //iconset.RenderIcon(new Style(), TextDirection.None, StateType.Normal, IconSize.SmallToolbar, mainTreeView, ""); //IconTheme.GetForScreen(this.ParentWindow.Screen) //new Gdk.Pixbuf("icons/xenon16.png"); //((Gtk.Image)Image.NewFromIconName("computer", IconSize.Dialog)).Pixbuf;
				//Console.WriteLine("HERE");
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

