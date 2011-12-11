// 
//  MainWindow.cs
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
using System.IO;
using System.Reflection;
using System.Threading;
using Gtk;
using Mono.Unix;
using Xenon.PluginUtil;
using Microsoft.VisualBasic;

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
					Gtk.Action NewTabAction = new Gtk.Action("NewTabAction", Catalog.GetString ("New _Tab"), null, null);
					NewTabAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("New _Tab");
					w1.Add (NewTabAction, "<Control>t");
					Gtk.Action CloseTabAction = new Gtk.Action("CloseTabAction", Catalog.GetString("_Close Tab"), null, null);
					CloseTabAction.ShortLabel = Catalog.GetString("_Close Tab");
					w1.Add(CloseTabAction, "<Control>F4");
					Gtk.Action NewFolderAction = new Gtk.Action("NewFolderAction", global::Mono.Unix.Catalog.GetString("New _Folder"), null, null);
					NewFolderAction.ShortLabel = global::Mono.Unix.Catalog.GetString("New _Folder");
					w1.Add(NewFolderAction, "F7");
					Gtk.Action NewFileAction = new Gtk.Action("NewFileAction", null, null, Stock.New);
					w1.Add(NewFileAction, "");
					Gtk.Action SearchAction = new Gtk.Action("SearchAction", Catalog.GetString("_Search"), null, null);
					w1.Add(SearchAction, "<Control>F");
					Gtk.Action RenameAction = new Gtk.Action("RenameAction", Catalog.GetString("_Rename"), null, null);
					w1.Add(RenameAction, "F2");
					Gtk.Action DeleteAction = new Gtk.Action("DeleteAction", null, null, Stock.Delete);
					w1.Add(DeleteAction, "Delete");
					Gtk.Action ExitAction = new Gtk.Action("ExitAction", null, null, Stock.Quit);
					w1.Add(ExitAction, null);
			
					Gtk.Action EditAction = new Gtk.Action("EditAction", global::Mono.Unix.Catalog.GetString ("_Edit"), null, null);
					w1.Add(EditAction, null);
					Gtk.Action CutAction = new Gtk.Action("CutAction", null, null, Stock.Cut);
					w1.Add(CutAction, null);
					Gtk.Action CopyAction = new Gtk.Action("CopyAction", null, null, Stock.Copy);
					w1.Add(CopyAction, null);
					Gtk.Action PasteAction = new Gtk.Action("PasteAction", null, null, Stock.Paste);
					w1.Add(PasteAction, null);
					Gtk.Action SelectAllAction = new Gtk.Action("SelectAllAction", null, null, Stock.SelectAll);
					w1.Add(SelectAllAction, "<Control>a");
					Gtk.Action SelectNoneAction = new Gtk.Action("SelectNoneAction", Catalog.GetString("Select _None"), null, null);
					w1.Add(SelectNoneAction, "<Control><Shift>a");
					Gtk.Action SettingsAction = new Gtk.Action("SettingsAction", null, null, Stock.Preferences);
					w1.Add(SettingsAction, null);
			
					Gtk.Action ViewAction = new Gtk.Action("ViewAction", Catalog.GetString("_View"), null, null);
					w1.Add(ViewAction, null);
					Gtk.Action RefreshAction = new Gtk.Action("RefreshAction", null, null, Stock.Refresh);
					w1.Add(RefreshAction, "F5");
			
					Gtk.Action HelpMenuAction = new Gtk.Action("HelpMenuAction", Catalog.GetString("_Help"), null, null);
					w1.Add(HelpMenuAction, null);
					Gtk.Action HelpAction = new Gtk.Action("HelpAction", null, null, Stock.Help);
					w1.Add(HelpAction, null);
					Gtk.Action AboutAction = new Gtk.Action("AboutAction", null, null, Stock.About);
					w1.Add(AboutAction, null);
			
					uiMgr.InsertActionGroup (w1, 0);
					this.AddAccelGroup (uiMgr.AccelGroup);
					uiMgr.AddUiFromString (
@"<ui><menubar name='menubar1'>
<menu name='FileAction' action='FileAction'><menuitem name='NewTabAction' action='NewTabAction'/><menuitem name='CloseTabAction' action='CloseTabAction'/><separator/>
<menuitem name='NewFolderAction' action='NewFolderAction'/><menu name='NewFileAction' action='NewFileAction'/><separator/>
<menuitem name='SearchAction' action='SearchAction'/><separator/>
<menuitem name='RenameAction' action='RenameAction'/><menuitem name='DeleteAction' action='DeleteAction'/><separator/>
<menuitem name='ExitAction' action='ExitAction'/></menu>
<menu name='EditAction' action='EditAction'><menuitem name='CutAction' action='CutAction'/><menuitem name='CopyAction' action='CopyAction'/><menuitem name='PasteAction' action='PasteAction'/><separator/>
<menuitem name='SelectAllAction' action='SelectAllAction'/><menuitem name='SelectNoneAction' action='SelectNoneAction'/><separator/>
<menuitem name='SettingsAction' action='SettingsAction'/></menu>
<menu name='ViewAction' action='ViewAction'><menuitem name='RefreshAction' action='RefreshAction'/></menu>
<menu name='HelpMenuAction' action='HelpMenuAction'><menuitem name='HelpAction' action='HelpAction'/><menuitem name='AboutAction' action='AboutAction'/></menu>
</menubar></ui>"
			        );
					MenuBar menubar1 = (MenuBar)uiMgr.GetWidget("/menubar1");
					menubar1.Name = "menubar1";
				vbox1.PackStart(menubar1, false, false, 0);
				toolbar = new Toolbar();
					toolbar.Insert(backButton = new ToolButton(Stock.GoBack), 0);
					toolbar.Insert(forwardButton = new ToolButton(Stock.GoForward), 1);
					toolbar.Insert(parentButton = new ToolButton(Stock.GoUp), 2);
					toolbar.Insert(new SeparatorToolItem(), 3);
					toolbar.Insert(computerButton = new ToolButton(Image.NewFromIconName("computer", IconSize.Button), Catalog.GetString("Computer")), 4);
					toolbar.Insert(homeButton = new ToolButton(Stock.Home), 5);
					toolbar.Insert(refreshButton = new ToolButton(Stock.Refresh), 6);
					computerButton.Sensitive = CommonUtil.CanLoadComputer();
					toolbar.Insert(new SeparatorToolItem(), 7);
					toolbar.Insert(cutButton = new ToolButton(Stock.Cut), 8);
					toolbar.Insert(copyButton = new ToolButton(Stock.Copy), 9);
					toolbar.Insert(pasteButton = new ToolButton(Stock.Paste), 10);
					//toolbar.Insert(new ToolButton(Stock.Preferences), 11);
				vbox1.PackStart(toolbar, false, false, 0);
				
			    locationBar = new Entry();
				vbox1.PackStart(locationBar, false, true, 0);
				
				
				nb = new Notebook();
				
				//nb.AppendPage((Widget)CommonUtil.LoadControlInstance(), new Label(""));
				nb.AppendPage((Widget)CommonUtil.LoadControlInstance(), new TabLabel(string.Empty, nb));
				vbox1.PackStart(nb, true, true, 0);
			
			this.Add(vbox1);
			
			this.DeleteEvent += OnDeleteEvent;
			locationBar.Activated += new EventHandler(LoadDirectory);
			nb.SwitchPage += new SwitchPageHandler(OnTabChanged);
			NewTabAction.Activated += new EventHandler(this.OnNewTabActionActivated);
			CloseTabAction.Activated += OnCloseTabActionActivated;
			NewFolderAction.Activated += new EventHandler(this.OnNewFolderActionActivated);
			RenameAction.Activated += OnRenameActionActivated;
			DeleteAction.Activated += OnDeleteActionActivated;
			ExitAction.Activated += OnExitActionActivated;
			CutAction.Activated += OnCutEvent;
			CopyAction.Activated += OnCopyEvent;
			PasteAction.Activated += OnPasteEvent;
			SelectAllAction.Activated += OnSelectAllActionActivated;
			SelectNoneAction.Activated += OnSelectNoneActionActivated;
			SettingsAction.Activated += new EventHandler(this.OnSettingActionActivated);
			RefreshAction.Activated += OnRefreshEvent;
			AboutAction.Activated += OnAboutActionActivated;
			
			backButton.Clicked += new EventHandler(this.OnBackEvent);
			forwardButton.Clicked += new EventHandler(this.OnForwardEvent);
			parentButton.Clicked += new EventHandler(this.OnParentEvent);
			computerButton.Clicked += new EventHandler(this.OnComputerEvent);
			homeButton.Clicked += new EventHandler(this.OnHomeEvent);
			refreshButton.Clicked += new EventHandler(this.OnRefreshEvent);
			cutButton.Clicked += OnCutEvent;
			copyButton.Clicked += OnCopyEvent;
			pasteButton.Clicked += OnPasteEvent;
			CommonUtil.DirectoryChanged += OnDirectoryChanged;
			
			
			CommonUtil.HomeButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
			while(((IDisplayInterfaceControl)nb.CurrentPageWidget).CurrentLocation == null) Thread.Sleep(100);
			SetActionStates();
			
			
			progress = new ProgressWindow();
			progress.IconList = this.IconList;
			
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
		ProgressWindow progress;
	
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
			while(((IDisplayInterfaceControl)nb.GetNthPage(num)).CurrentLocation == null) Thread.Sleep(100);
			nb.CurrentPage = num;
		}
		
		protected void OnCloseTabActionActivated(object sender, EventArgs e) {
			nb.RemovePage(nb.CurrentPage);
		}
		
		protected void OnNewFolderActionActivated(object sender, EventArgs e) {
			((IDisplayInterfaceControl)nb.CurrentPageWidget).NewFolder();
		}
		
		protected void OnRenameActionActivated(object sender, EventArgs e) {
			((IDisplayInterfaceControl)nb.CurrentPageWidget).Rename();
		}
		
		protected void OnDeleteActionActivated(object sender, EventArgs e) {
			((IDisplayInterfaceControl)nb.CurrentPageWidget).Recycle();
		}
		
		protected void OnExitActionActivated(object sender, EventArgs e) {
			Application.Quit();
		}
		
		protected void OnSelectAllActionActivated(object sender, EventArgs e) {
			((IDisplayInterfaceControl)nb.CurrentPageWidget).SelectAll();
		}
		
		protected void OnSelectNoneActionActivated(object sender, EventArgs e) {
			((IDisplayInterfaceControl)nb.CurrentPageWidget).SelectNone();
		}
		
		protected void OnSettingActionActivated(object sender, EventArgs e) {
			new SettingsDialog().ShowAll();
		}
		
		protected void OnAboutActionActivated(object sender, EventArgs e) {
			AboutDialog dialog = new AboutDialog ();
			Assembly asm = Assembly.GetExecutingAssembly ();
			
			dialog.ProgramName = (asm.GetCustomAttributes (
				typeof (AssemblyTitleAttribute), false) [0]
				as AssemblyTitleAttribute).Title;
			
			dialog.Version = asm.GetName ().Version.ToString ();
			
			dialog.Comments = (asm.GetCustomAttributes (
				typeof (AssemblyDescriptionAttribute), false) [0]
				as AssemblyDescriptionAttribute).Description;
			
			dialog.Copyright = (asm.GetCustomAttributes (
				typeof (AssemblyCopyrightAttribute), false) [0]
				as AssemblyCopyrightAttribute).Copyright;
			
			dialog.License = @"This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.";
			
			dialog.Authors = new string[] { "John Bentley <pcguy49@yahoo.com>" };
			
			dialog.Run();
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
		
		protected void OnCutEvent(object sender, EventArgs e) {
			CommonUtil.CutButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnCopyEvent(object sencer, EventArgs e) {
			CommonUtil.CopyButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget);
		}
		
		protected void OnPasteEvent(object sender, EventArgs e) {
			CommonUtil.PasteButtonClicked((IDisplayInterfaceControl)nb.CurrentPageWidget, new GtkFileOperationProgress(progress));
		}
		
		
		protected void OnDirectoryChanged(object sender, DirectoryChangedEventArgs e) {
			((TabLabel)nb.GetTabLabel((Widget)e.Control)).TextLabel.Text = e.ShortPath;
			locationBar.Text = e.DisplayPath;
			
			SetActionStates();
			IDisplayInterfaceControl[] controls = new IDisplayInterfaceControl[nb.NPages];
			for(int i = 0; i < nb.NPages; ++i) { controls[i] = (IDisplayInterfaceControl)nb.GetNthPage(i); }
			CommonUtil.FileSystem.UpdateWatchList(controls);
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