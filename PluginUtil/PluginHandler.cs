using System;

namespace Xenon.PluginUtil {
	public abstract class PluginHandler {
		public virtual void InitPlugin() {}
		public abstract bool DoLoad(PluginUIType ui, PluginOSType os);
	}
}

