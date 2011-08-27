using System;
using System.IO;
using Gtk;
using Mono.Unix;
using Xenon.PluginUtil;

namespace Xenon.FileManager.GtkUI {
	class MainClass {
		public static void Main(string[] args) {
			Application.Init();
			//Environment.SetEnvironmentVariable("LANGUAGE", "es");
			Catalog.Init("xenon", Path.Combine(CommonUtil.ExecutablePath, "locale"));
			
			CommonUtil.UIType = Xenon.PluginUtil.PluginUIType.Gtk;
			CommonUtil.LoadPlugins();
			
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}

