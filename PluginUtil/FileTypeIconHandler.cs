using System;

namespace Xenon.PluginUtil {
	public abstract class FileTypeIconHandler : PluginHandler {
		public abstract object FindIcon(Uri path, string ext, string mimetype);
		public virtual object FindIconDir(Uri path, string ext, string mimetype) { return FindIcon(path, ext, mimetype); }
	}
}

