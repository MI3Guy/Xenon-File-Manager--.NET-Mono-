using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Xenon.PluginUtil {
	public static class CommonUtil {
		public const int IconCacheMaxSize = 2048;
		public const int IconCacheTrimSize = 1024;
		
		public const int HistoryNumItems = 300;
		public const int HistoryTrimNum = 200;
		
		public static string ExecutablePath {
			get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location); }
		}
		
		static CommonUtil() {
		    switch (Environment.OSVersion.Platform)  {
			    case PlatformID.Win32NT:
			    case PlatformID.Win32S:
			    case PlatformID.Win32Windows:
			    case PlatformID.WinCE:
			        OSType = PluginOSType.Windows;
			        break;
			    case PlatformID.Unix:
			        OSType = PluginOSType.Unix;
			        break;
			    default:
			        
			        break;
			}
		}
		
		public static event DirectoryChangedEventHandler DirectoryChanged;
		public static event XeErrorHandler ErrorOccurred;
		//public static event TabOpenedEventHandler TabOpened;
		
		public static readonly PluginOSType OSType;
		private static bool hasSetUI = false;
		private static PluginUIType uiType;
		public static PluginUIType UIType {
			get { return uiType; }
			set { if(hasSetUI) return; hasSetUI = true; uiType = value; }
		}
		
#region Plugin Loading
		private static List<FileSystemHandler> fsHandlers = new List<FileSystemHandler>();
		private static List<DisplayInterfaceHandler> dispInterfaceHandlers = new List<DisplayInterfaceHandler>();
		private static List<FileTypeIconHandler> fileTypeIconHandlers = new List<FileTypeIconHandler>();
		private static List<ClipboardHandler> clipboardHandlers = new List<ClipboardHandler>();
		
		public static void LoadPlugins() {
			DirectoryInfo directory = new DirectoryInfo(Path.Combine(ExecutablePath, "plugins"));
			FileInfo[] files = directory.GetFiles("*.dll");
			foreach(FileInfo file in files) {
				string className = Path.GetFileNameWithoutExtension(file.Name);
				try {
					Assembly assembly = Assembly.LoadFrom(file.FullName);
					Type type = assembly.GetType("Xenon.Plugin." + className + "." + className);
					
					PluginHandler instance = (PluginHandler)Activator.CreateInstance(type);
					if(!instance.DoLoad(uiType, OSType)) continue;
					
					if(instance is FileSystemHandler) {
						fsHandlers.Add((FileSystemHandler)instance);
						instance.InitPlugin();
					}
					else if(instance is DisplayInterfaceHandler) {
						dispInterfaceHandlers.Add((DisplayInterfaceHandler)instance);
						instance.InitPlugin();
					}
					else if(instance is FileTypeIconHandler) {
						fileTypeIconHandlers.Add((FileTypeIconHandler)instance);
						instance.InitPlugin();
					}
					else if(instance is ClipboardHandler) {
						clipboardHandlers.Add((ClipboardHandler)instance);
						instance.InitPlugin();
					}
				}
				catch(Exception ex) {
					Console.WriteLine("Error loading plugin: {0}", ex);
				}
			}
		}
#endregion
		
		public static void NotifyDirectoryChanged(object sender, DirectoryChangedEventArgs e) {
			DirectoryChanged(sender, e);
		}
		
#region Plugin Determination
		public static bool HandlesDirectory(Uri path) {
			foreach(FileSystemHandler handler in fsHandlers) {
				if(handler.HandlesUriType(path)) {
					return true;
				}
			}
			return false;
		}
		
		public static XeFileInfo[] DirectoryListing(string dir, out Uri pathOut) {
			Uri path = new Uri(dir);
			XeFileInfo[] fi = DirectoryListing(ref path);
			pathOut = path;
			return fi;
		}
		
		public static XeFileInfo[] DirectoryListing(ref Uri path) {
			foreach(FileSystemHandler handler in fsHandlers) {
				if(handler.HandlesUriType(path)) {
					XeFileInfo[] fi = handler.LoadDirectory(ref path);
					if(fi == null && !handler.HandlesUriType(path)) return DirectoryListing(ref path);
					return fi;
				}
			}
			throw new PluginNotFoundException();
		}
		
		public static Uri ParentDirectoryFor(Uri path) {
			foreach(FileSystemHandler handler in fsHandlers) {
				if(handler.HandlesUriType(path)) {
					return handler.ParentDirectory(path);
				}
			}
			throw new PluginNotFoundException();
		}
		
		public static IDisplayInterfaceControl LoadControlInstance() {
			foreach(DisplayInterfaceHandler handler in dispInterfaceHandlers) {
				return handler.InitControl();
			}
			throw new PluginNotFoundException();
		}
		
		public static object GetIconForFile(XeFileInfo file) {
			foreach(FileTypeIconHandler handler in fileTypeIconHandlers) {
				return handler.FindIcon(file.FullPath, Path.GetExtension(file.Name), null);
			}
			throw new PluginNotFoundException();
		}
		
		public static object GetIconForDirectory(XeFileInfo file) {
			foreach(FileTypeIconHandler handler in fileTypeIconHandlers) {
				return handler.FindIconDir(file.FullPath, Path.GetExtension(file.Name), null);
			}
			throw new PluginNotFoundException();
		}
		
		public static ClipboardHandler PrimaryClipboardHandler {
			get {
				if(clipboardHandlers.Count == 0) throw new PluginNotFoundException();
				return clipboardHandlers[0];
			}
		}
#endregion
		
		public static bool HasBackHistory(IDisplayInterfaceControl control) {
			return control.HistoryBack.Count != 0;
		}
		
		public static void BackButtonClicked(IDisplayInterfaceControl control) {
			if(control.CurrentLocation == null || control.HistoryBack.Count == 0) return;
			control.HistoryForward.Push(control.CurrentLocation);
			control.CurrentLocation = control.HistoryBack.Pop();
			LoadDirectory(control.CurrentLocation, control, false);
		}
		
		public static bool HasForwardHistory(IDisplayInterfaceControl control) {
			return control.HistoryForward.Count != 0;
		}
		
		public static void ForwardButtonClicked(IDisplayInterfaceControl control) {
			if(control.CurrentLocation == null || control.HistoryForward.Count == 0) return;
			control.HistoryBack.Push(control.CurrentLocation);
			control.CurrentLocation = control.HistoryForward.Pop();
			LoadDirectory(control.CurrentLocation, control, false);
		}
		
		public static bool HasParentDirectory(IDisplayInterfaceControl control) {
			return ParentDirectoryFor(control.CurrentLocation) != null;
		}
		
		public static void ParentButtonClicked(IDisplayInterfaceControl control) {
			Uri path = ParentDirectoryFor(control.CurrentLocation);
			if(path == null) return;
			LoadDirectory(path, control);
		}
		
		public static void HomeButtonClicked(IDisplayInterfaceControl control) {
			try {
				LoadDirectory((Uri)SettingsUtil.MainSettings["home"].data, control);
			}
			catch(Exception ex) { Console.WriteLine(ex); }
		}
		
		public static bool CanLoadComputer() {
			return HandlesDirectory(new Uri("about:computer"));
		}
		
		public static void ComputerButtonClicked(IDisplayInterfaceControl control) {
			LoadDirectory("about:computer", control);
		}
		
		public static void RefreshButtonClicked(IDisplayInterfaceControl control) {
			LoadDirectory(control.CurrentLocation, control, false);
		}
		
		public static void CutButtonClicked(IDisplayInterfaceControl control) {
			ClipboardHandler handler = PrimaryClipboardHandler;
			if(handler == null) return;
			handler.ExposePaths(new ClipboardData(from file in control.SelectedFiles select file.FullPath, OperationType.Cut));
		}
		
		public static void CopyButtonClicked(IDisplayInterfaceControl control) {
			ClipboardHandler handler = PrimaryClipboardHandler;
			if(handler == null) return;
			handler.ExposePaths(new ClipboardData(from file in control.SelectedFiles select file.FullPath, OperationType.Copy));
		}
		
		public static void PasteButtonClicked(IDisplayInterfaceControl control) {
			ClipboardHandler handler = PrimaryClipboardHandler;
			if(handler == null) return;
			handler.RequestPaths(delegate(object sender, EventArgs e) {});
		}
		
		public static void LoadDirectory(string text, IDisplayInterfaceControl control) {
			if(text == "/") text = "file:///";
			Uri path = new Uri(text);
			LoadDirectory(path, control);
			/*XeFileInfo[] files = DirectoryListing(text, out path);
			if(files == null || path == null) return;
			if(control.CurrentLocation != null) control.HistoryBack.Push(control.CurrentLocation);
			control.CurrentLocation = path;
			control.SetContent(files, path);*/
			
		}
		
		public static void LoadDirectory(Uri path, IDisplayInterfaceControl control) {
			LoadDirectory(path, control, true);
		}
		
		private static void LoadDirectory(Uri path, IDisplayInterfaceControl control, bool handleHistory) {
			XeFileInfo[] files = DirectoryListing(ref path);
			if(files == null || path == null) return;
			if(handleHistory) {
				if(control.CurrentLocation != null) control.HistoryBack.Push(control.CurrentLocation);
				control.CurrentLocation = path;
			}
			control.SetContent(files, path);
		}
		
		public static void TabChanged(IDisplayInterfaceControl control) {
			NotifyDirectoryChanged(control, new DirectoryChangedEventArgs(control.CurrentLocation, control));
		}
	}
}

