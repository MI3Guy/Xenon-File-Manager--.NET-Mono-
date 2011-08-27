using System;
namespace Xenon.PluginUtil {
	public abstract class DisplayInterfaceHandler : PluginHandler {
		public abstract IDisplayInterfaceControl InitControl();
	}
}

