using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace PluginUtil {
	public static class CommonUtil {
		
		private static List<FileSystemHandler> fsHandlers = new List<FileSystemHandler>();
		
		public static void LoadFileSystemHandlers(PluginUIType uitype, PluginOSType ostype) {
			string location = System.Reflection.Assembly.GetEntryAssembly().Location;
			DirectoryInfo directory = new DirectoryInfo(System.IO.Path.GetDirectoryName(location));
			FileInfo[] files = directory.GetFiles("*.dll");
			foreach(FileInfo file in files) {
				string className = Path.GetFileNameWithoutExtension(file.Name);
				try {
					Assembly assembly = Assembly.LoadFrom(file.FullName);
					Type type = assembly.GetType(className + "." + className);
					FileSystemHandler instanceOfMyType = (FileSystemHandler)Activator.CreateInstance(type);
					if(instanceOfMyType.DoLoad(uitype, ostype)) fsHandlers.Add(instanceOfMyType);
				}
				catch(Exception ex) {
					Console.WriteLine("Error loading plugin: {0}", ex);
				}
			}
		}
		
		public static string[][] DirectoryListing(string dir) {
			try {
				Uri path = new Uri(dir);
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						return handler.LoadDirectory(path);
					}
				}
			}
			catch { return null; }
		}
		
	}
}

