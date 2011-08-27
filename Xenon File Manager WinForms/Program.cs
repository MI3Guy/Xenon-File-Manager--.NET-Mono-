using System;
using System.Windows.Forms;

namespace XenonFileManagerWinForms {
	public static class Program {
		public static void Main() {
			PluginUtil.CommonUtil.UIType = PluginUtil.PluginUIType.WinForms;
			PluginUtil.CommonUtil.LoadPlugins();
			
			Application.Run(new MainForm());
		}
	}
}

