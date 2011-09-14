using System;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public abstract class ClipboardHandler : PluginHandler {
		public abstract void ExposePaths(ClipboardData paths);
		public virtual void RequestPaths(EventHandler evt) {
			evt(null, null);
		}
		public abstract ClipboardData PastePaths();
	}
}

