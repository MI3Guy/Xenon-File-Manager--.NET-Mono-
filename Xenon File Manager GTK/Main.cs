using System;
using Gtk;

namespace XenonFileManager {
	class MainClass {
		public static void Main(string[] args) {
			Application.Init();
			
			PluginUtil.CommonUtil.UIType = PluginUtil.PluginUIType.Gtk;
			PluginUtil.CommonUtil.LoadPlugins();
			
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}

