using System;

namespace PluginUtil {
	public abstract class ClipboardHandler : PluginHandler {
		public abstract void InitClipboard(object obj);
		public abstract void CopyPath(Uri path);
		public abstract void GetPath(Uri path);
	}
}

