using System;
using System.IO;
using Gtk;
using Mono.Unix;
using Xenon.PluginUtil;

namespace Xenon.FileManager.GtkUI {
	public class MainWindow : Window {
		public MainWindow() : base(Gtk.WindowType.Toplevel) {
			//Build();
			
			this.IconList = new Gdk.Pixbuf[] { new Gdk.Pixbuf(null, "XenonFileManager.icons.xenon16.png"), new Gdk.Pixbuf(null, "XenonFileManager.icons.xenon256.png") };
			
			this.Title = Catalog.GetString("Xenon File Manager");
			this.SetSizeRequest(600, 450);
			//this.DefaultWidth = 600;
			//this.DefaultHeight = 450;
			
			VBox vbox1 = new VBox(false, 0);
					UIManager uiMgr = new UIManager();
					ActionGroup w1 = new ActionGroup("Default");
					Gtk.Action FileAction = new Gtk.Action("FileAction", global::Mono.Unix.Catalog.GetString ("_File"), null, null);
					FileAction.ShortLabel = Mono.Unix.Catalog.GetString("_File");
					w1.Add (FileAction, null);
					Gtk.Action NewTabAction = new Gtk.Action("NewTabAction", global::Mono.Unix.Catalog.GetString ("New _Tab"), null, null);
					NewTabAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("New _Tab");
					w1.Add (NewTabAction, "<Control>t");
					Gtk.Action NewFolderAction = new Gtk.Action("NewFolderAction", global::Mono.Unix.Catalog.GetString ("New _Folder"), null, null);
					NewFolderAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("New _Folder");
					w1.Add (NewFolderAction, "F7");
			
					Gtk.Action EditAction = new Gtk.Action("EditAction", global::Mono.Unix.Catalog.GetString ("_Edit"), null, null);
					FileAction.ShortLabel = Mono.Unix.Catalog.GetString("_Edit");
					w1.Add (EditAction, null);
			
					Gtk.Action SettingsAction = new Gtk.Action("SettingsAction", null, null, "gtk-preferences");
					//SettingsAction.ShortLabel = Catalog.GetString("_Preferences");
					w1.Add(SettingsAction, null);
			
					uiMgr.InsertActionGroup (w1, 0);
					this.AddAccelGroup (uiMgr.AccelGroup);
					uiMgr.AddUiFromString ("<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='NewTabAction' action='NewTabAction'/><menuitem name='NewFolderAction' action='NewFolderAction'/></menu><menu name='EditAction' action='EditAction'><menuitem name='SettingsAction' action='SettingsAction'/></menu></menubar></ui>");
					MenuBar menubar1 = (MenuBar)uiMgr.GetWidget("/menubar1");
					menubar1.Name = "menubar1";
				vbox1.PackStart(menubar1, false, false, 0);
				toolbar = new Toolbar();
					toolbar.Insert(backButton = new ToolButton(Gtk.Stock.GoBack), 0);
					toolbar.Insert(forwardButton = new ToolButton(Gtk.Stock.GoForward), 1);
					toolbar.Insert(parentButton = new ToolButton(Gtk.Stock.GoUp), 2);
					toolbar.Insert(new SeparatorToolItem(), 3);
					toolbar.Insert(homeButton = new ToolButton(Stock.Home), 4);
					toolbar.Insert(refreshButton = new ToolButton(Gtk.Stock.Refresh), 5);
					toolbar.Insert(computerButton = new ToolButton(Gtk.Image.NewFromIconName("computer", IconSize.Button), Catalog.GetString("Computer")), 6);
					computerButton.Sensitive = CommonUtil.CanLoadComputer();
					toolbar.Insert(new SeparatorToolItem(), 7);
					toolbar.Insert(cutButton = new ToolButton(Stock.Cut), 8);
					cutButton.Sensitive = false;
					toolbar.Insert(copyButton = new ToolButton(Stock.Copy), 9);
					copyButton.Sensitive = false;
					toolbar.Insert(pasteButton = new ToolButton(Stock.Paste), 10);
					pasteButton.Sensitive = false;
					toolbar.Insert(new ToolButton(Stock.Preferences), 11);
				vbox1.PackStart(toolbar, false, false, 0);
				
			    locationBar = new Entry();
				vbox1.PackStart(locationBar, false, true, 0);
				
				
				nb = new Notebook();
				
				//nb.AppendPage((Widget)CommonUtil.LoadControlInstance(), new Label(""));
				nb.AppendPage((Widget)CommonUtil.LoadControlInstance(), new TabLabel(string.Empty, nb));
				vbox1.PackStart(nb, true, true, 0);
			
			this.Add(vbox1);
			
			this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			locationBar.Activated += new EventHandler(LoadDirectory);
			nb.SwitchPage += new SwitchPageHandler(OnTabChanged);
			NewFolderAction.Activated += new EventHandler(this.OnNewFolderActionActivated);
			NewTabAction.Activated += new EventHandler(this.OnNewTabActionActivated);
			backButton.Clicked += new EventHandler(this.OnBackEvent);
			forwardButton.Clicked += new EventHandler(this.OnForwardEvent);
			parentButton.Clicked += new EventHandler(this.OnParentEvent);
			homeButton.Clicked += new EventHandler(this.OnHomeEvent);
			refreshButton.Clicked += new EventHandler(this.OnRefreshEvent);
			computerButton.Clicked += new EventHandler(this.OnComputerEvent);
			CommonUtil.DirectoryChanged += new DirectoryChangedEventHandler(this.OnDirectoryChanged);
			
			
			SetActionStates();
			CommonUtil.HomeButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
			this.ShowAll();
		}
		
		
		//MenuBar menuBar;
		Toolbar toolbar;
		Entry locationBar;
		Notebook nb;
		
		ToolButton backButton;
		ToolButton forwardButton;
		ToolButton parentButton;
		ToolButton homeButton;
		ToolButton refreshButton;
		ToolButton computerButton;
		ToolButton cutButton;
		ToolButton copyButton;
		ToolButton pasteButton;
	
		protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
			Application.Quit();
			a.RetVal = true;
		}
		
		protected void LoadDirectory(object sender, EventArgs e) {
			// TODO: Handle 0 Pages
			//if(nb.NPages == 0) // add tabj
			CommonUtil.LoadDirectory(locationBar.Text, (IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnTabChanged(object sender, SwitchPageArgs e) {
			CommonUtil.TabChanged((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		
		protected void OnNewTabActionActivated(object sender, EventArgs e) {
			int num = nb.AppendPage((Widget)CommonUtil.LoadControlInstance(), new TabLabel(string.Empty, nb));
			nb.ShowAll();
			CommonUtil.HomeButtonClicked((IDisplayInterfaceControl)nb.GetNthPage(num));
			nb.CurrentPage = num;
		}
		
		protected void OnNewFolderActionActivated(object sender, EventArgs e) {
			
		}
		
			
		protected void OnBackEvent(object sender, EventArgs e) {
			// TODO: Handle no tab
			CommonUtil.BackButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnForwardEvent(object sender, EventArgs e) {
			// TODO: Handle no tab
			CommonUtil.ForwardButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnParentEvent(object sender, EventArgs e) {
			CommonUtil.ParentButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnHomeEvent(object sender, EventArgs e) {
			CommonUtil.HomeButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnRefreshEvent(object sender, EventArgs e) {
			CommonUtil.RefreshButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnComputerEvent(object sender, EventArgs e) {
			CommonUtil.ComputerButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		
		protected void OnDirectoryChanged(object sender, DirectoryChangedEventArgs e) {
			((TabLabel)nb.GetTabLabel((Widget)e.Control)).TextLabel.Text = e.DisplayPath;
			locationBar.Text = e.FullPath;
			
			SetActionStates();
		}
		
		
		
		protected void SetActionStates() {
			// TODO: Handle no tab
			//if(nb.Cur) {
			//	
			//}
			backButton.Sensitive = CommonUtil.HasBackHistory((IDisplayInterfaceControl)nb.CurrentPageWidget);
			forwardButton.Sensitive = CommonUtil.HasForwardHistory((IDisplayInterfaceControl)nb.CurrentPageWidget);
			parentButton.Sensitive = CommonUtil.HasParentDirectory((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
	}
}