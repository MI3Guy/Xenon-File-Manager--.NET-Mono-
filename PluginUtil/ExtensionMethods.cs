using System;
using System.Web;

namespace Xenon.PluginUtil {
	public static class ExtensionMethods {
		public static string GetScrubbedLocalPath(this Uri path) {
			return HttpUtility.UrlDecode(path.LocalPath);
		}
	}
}

