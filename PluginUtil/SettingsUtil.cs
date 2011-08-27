using System;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public static class SettingsUtil {
		
		static SettingsUtil() {
			AllSettings = new Dictionary<string, Dictionary<string, object>>();
			MainSettings = new Dictionary<string, object>();
			PluginSettings = new Dictionary<string, object>();
			
			AllSettings.Add("Main Settings", MainSettings);
			AllSettings.Add("Plugins", PluginSettings);
			
			MainSettings.Add("home", new Uri("file:///home/john"));
		}
		
		public static Dictionary<string, Dictionary<string, object>> AllSettings;
		
		public static Dictionary<string, object> MainSettings;
		public static Dictionary<string, object> PluginSettings;
	}
}

