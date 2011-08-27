using System;
using Gtk;

namespace Xenon.FileManager.GtkUI {
	class MainClass {
		public static void Main(string[] args) {
			Application.Init();
			
			Xenon.PluginUtil.CommonUtil.UIType = Xenon.PluginUtil.PluginUIType.Gtk;
			Xenon.PluginUtil.CommonUtil.LoadPlugins();
			
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}

