using System;
using System.Windows.Forms;
using Xenon.PluginUtil;

namespace Xenon.FileManager.WinForms {
	public static class Program {
		public static void Main() {
			CommonUtil.UIType = PluginUtil.PluginUIType.WinForms;
			CommonUtil.LoadPlugins();
			
			Application.Run(new MainForm());
		}
	}
}

