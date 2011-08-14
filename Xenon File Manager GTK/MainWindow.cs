using System;
using System.IO;
using Gtk;
using PluginUtil;

public partial class MainWindow : Gtk.Window {
	public MainWindow() : base(Gtk.WindowType.Toplevel) {
		//Build();
		
		this.Title = "Xenon File Manager";
		this.SetSizeRequest(600, 450);
		//this.DefaultWidth = 600;
		//this.DefaultHeight = 450;
		
		VBox vbox1 = new VBox(false, 0);
			menuBar = new MenuBar();
				MenuItem menuItem = new MenuItem("_File");
				Menu menu = new Menu();
					MenuItem menuItem2 = new MenuItem("_New");
					menu.Append(menuItem2);
				menuItem.Submenu = menu;
			menuBar.Append(menuItem);
			vbox1.PackStart(menuBar, false, false, 0);
			toolbar = new Toolbar();
				ToolButton btn;
				toolbar.Insert(btn = new ToolButton(Gtk.Stock.GoBack), 0);
				btn.Clicked += new EventHandler(this.OnBackEvent);
				toolbar.Insert(new ToolButton(Gtk.Stock.GoForward), 1);
				toolbar.Insert(new ToolButton(Gtk.Stock.GoUp), 2);
				toolbar.Insert(new SeparatorToolItem(), 3);
				toolbar.Insert(new ToolButton(Stock.Home), 4);
				toolbar.Insert(new ToolButton(Gtk.Stock.Refresh), 5);
				toolbar.Insert(new ToolButton(Gtk.Image.NewFromIconName("computer", IconSize.Button), "Computer"), 6);
				toolbar.Insert(new SeparatorToolItem(), 7);
				toolbar.Insert(new ToolButton(Stock.Cut), 8);
				toolbar.Insert(new ToolButton(Stock.Copy), 9);
				toolbar.Insert(new ToolButton(Stock.Paste), 10);
			vbox1.PackStart(toolbar, false, false, 0);
			
		    locationBar = new Entry();
			locationBar.Activated += new EventHandler(LoadDirectory);
			vbox1.PackStart(locationBar, false, true, 0);
			
			
			Notebook nb = new Notebook();
				ScrolledWindow sw = new ScrolledWindow();
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
						listStore = new Gtk.ListStore(typeof(XeFileInfo));
						//listStore.AppendValues("test.txt", "50 KB", "Today");
						//listStore.AppendValues("test2.txt", "51 KB", "Yesterday");
					mainTreeView.Model = listStore;
					TreeModelFilter filter = new TreeModelFilter(listStore, null);
					filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc(FilterList);
					mainTreeView.Model = filter;
				sw.Add(mainTreeView);
			nb.AppendPage(sw, new Label("john"));
			vbox1.PackStart(nb, true, true, 0);
		
		this.Add(vbox1);
		
		this.ShowAll();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
	}
	
	
	MenuBar menuBar;
	Toolbar toolbar;
	TreeView mainTreeView;
	ListStore listStore;
	Entry locationBar;
	
	void LoadDir(string name) {
		listStore.Clear();
		
		//FileInfo[] fi2 = new DirectoryInfo("/home/john").GetFiles();
		foreach(XeFileInfo fi in CommonUtil.DirectoryListing(name)) {
			//if(!fi.IsHidden)
			Console.WriteLine(fi.Name);
			listStore.AppendValues(fi);
		}
		
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

	protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
		Application.Quit();
		a.RetVal = true;
	}
		
	protected void OnBackEvent(object sender, EventArgs e) {
		int width, height;
		locationBar.GetSizeRequest(out width, out height);
		locationBar.SetSizeRequest(598, height);
		LoadDir("/home/john");
	}
	
	protected void LoadDirectory(object sender, EventArgs e) {
		LoadDir(locationBar.Text);
	}
}

