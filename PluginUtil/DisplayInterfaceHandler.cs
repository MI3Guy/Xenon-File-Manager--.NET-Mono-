using System;
namespace PluginUtil {
	public abstract class DisplayInterfaceHandler : PluginHandler {
		public abstract IDisplayInterfaceControl InitControl();
	}
}

