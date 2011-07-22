using System;
using Gtk;

public partial class MainWindow : Gtk.Window {
	public MainWindow() : base(Gtk.WindowType.Toplevel) {
		//Build();
		
		this.Title = "Xenon File Manager";
		this.DefaultWidth = 400;
		this.DefaultHeight = 300;
		
		VBox vbox1 = new VBox(false, 0);
			menuBar = new MenuBar();
				MenuItem menuItem = new MenuItem("_File");
				Menu menu = new Menu();
					MenuItem menuItem2 = new MenuItem("_New");
					menu.Append(menuItem2);
				menuItem.Submenu = menu;
			menuBar.Append(menuItem);
			vbox1.PackStart(menuBar, false, false, 0);
			toolBar = new Toolbar();
				toolBar.Insert(new ToolButton(Gtk.Stock.GoBack), 0);
				toolBar.Insert(new ToolButton(Gtk.Stock.GoForward), 1);
				toolBar.Insert(new ToolButton(Gtk.Stock.GoUp), 2);
				toolBar.Insert(new SeparatorToolItem(), 3);
				toolBar.Insert(new ToolButton(Stock.Home), 4);
				toolBar.Insert(new ToolButton(Gtk.Stock.Refresh), 5);
				toolBar.Insert(new SeparatorToolItem(), 6);
				toolBar.Insert(new ToolButton(Stock.Cut), 7);
				toolBar.Insert(new ToolButton(Stock.Copy), 8);
				toolBar.Insert(new ToolButton(Stock.Paste), 9);
			vbox1.PackStart(toolBar, false, false, 0);
			//toolBar2 = new Toolbar();
			//	toolBar2.AppendWidget(new TextView(), "", "");
			//	toolBar2.Insert(new ToolButton(Stock.GotoLast), 1);
			//vbox1.PackStart(toolBar2, false, false, 0);
			mainTreeView = new TreeView();
			listStore = new Gtk.ListStore (typeof(string), typeof(string), typeof(string));
			mainTreeView.Model = listStore;
				TreeViewColumn col = new TreeViewColumn();
				col.Title = "Name";
				col.Sizing = TreeViewColumnSizing.Autosize;
				mainTreeView.AppendColumn(col);
				col = new TreeViewColumn();
				col.Title = "Size";
				mainTreeView.AppendColumn(col);
				col = new TreeViewColumn();
				col.Title = "Date";
				mainTreeView.AppendColumn(col);
				listStore.AppendValues("test.txt", "50 KB", "Today");
			vbox1.PackStart(mainTreeView, true, true, 0);
		
		this.Add(vbox1);
		
		this.ShowAll();
		
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
	}
	
	
	MenuBar menuBar;
	Toolbar toolBar;
	Toolbar toolBar2;
	TreeView mainTreeView;
	ListStore listStore;
	
	void LoadDir(string name) {
		listStore.Clear();
		foreach(
		listStore.AppendValues();
	}
	

	protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
		Application.Quit();
		a.RetVal = true;
	}
	
	protected virtual void OnGoBackActionActivated (object sender, System.EventArgs e) {
		
	}
	
	
}

