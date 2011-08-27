using System;

namespace Xenon.PluginUtil {
	public abstract class PluginHandler {
		public abstract bool DoLoad(PluginUIType ui, PluginOSType os);
	}
}

