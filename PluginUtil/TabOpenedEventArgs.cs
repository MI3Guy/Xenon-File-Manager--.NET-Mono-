using System;

namespace Xenon.PluginUtil {
	public delegate void TabOpenedEventHandler(object sender, TabOpenedEventArgs e);
	public class TabOpenedEventArgs {
		public readonly IDisplayInterfaceControl Control;
		public TabOpenedEventArgs(IDisplayInterfaceControl control) {
			Control = control;
		}
	}
}

