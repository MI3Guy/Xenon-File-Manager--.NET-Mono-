using System;
using Gtk;

namespace XenonFileManager
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			
			PluginUtil.CommonUtil.LoadFileSystemHandlers(PluginUtil.PluginUIType.Gtk, PluginUtil.PluginOSType.Linux);
			
			MainWindow win = new MainWindow();
			win.Show ();
			Application.Run ();
		}
	}
}

