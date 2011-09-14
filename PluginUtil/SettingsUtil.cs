using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Xenon.PluginUtil {
	public static class SettingsUtil {
		
		public enum SettingType {
			None,
			Text,
			Path,
			Integer,
			Enumeration,
			Bool
		}
		
		public static readonly ConstSettingEntry[] ValidSettings = new ConstSettingEntry[] {
			new ConstSettingEntry { name = "home", displayName = "Home", type = SettingType.Path },
			new ConstSettingEntry { name = "show..item", displayName = "Show .. Item", type = SettingType.Bool },
			new ConstSettingEntry { name = "showhidden", displayName = "Show Hidden Files", type = SettingType.Bool }
			//new ConstSettingEntry { name = "" }
		};
		
		static SettingsUtil() {
			AllSettings = new Dictionary<string, Dictionary<string, SettingEntry>>();
			MainSettings = new Dictionary<string, SettingEntry>();
			PluginSettings = new Dictionary<string, SettingEntry>();
			
			string datadir = Environment.GetEnvironmentVariable("XENON_DATA_DIR");
			if(datadir == null) {
				datadir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XenonFileManager");
			}
			if(!Directory.Exists(datadir)) Directory.CreateDirectory(datadir);
			DataDirectory = datadir;
			
			AllSettings.Add("Main Settings", MainSettings);
			AllSettings.Add("Plugins", PluginSettings);
			
			//MainSettings.Add("home", new Uri("file:///home/john"));
			//MainSettings.Add("home", new SettingEntry { data = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), writetofile = false });

			LoadSettings();
			
			foreach(ConstSettingEntry entry in ValidSettings) {
				if(MainSettings.ContainsKey(entry.name)) continue;
				MainSettings.Add(entry.name, DefaultSetting(entry.name));
			}
			
			var tmpMainSettings = from kv in MainSettings orderby kv.Key select kv;
			MainSettings = new Dictionary<string, SettingEntry>();
			foreach(KeyValuePair<string, SettingEntry> kv in tmpMainSettings) {
				MainSettings.Add(kv.Key, kv.Value);
			}
		}
		
		public static SettingEntry DefaultSetting(string name) {
			switch(name) {
				case "home":
					return new SettingEntry { data = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Personal)), writetofile = false };
				case "show..item":
					return new SettingEntry { data = false, writetofile = false };
				case "showhidden":
					return new SettingEntry { data = false, writetofile = false };
				default:
					return null;
			}
		}
		
		public static Dictionary<string, Dictionary<string, SettingEntry>> AllSettings;
		
		public static Dictionary<string, SettingEntry> MainSettings;
		public static Dictionary<string, SettingEntry> PluginSettings;
		
		public class ConstSettingEntry {
			public string name;
			public string displayName;
			public SettingType type;
			public string[] enumValues;
		}
		
		public class SettingEntry {
			public object data; // Could be string, int, enum, or bool.
			public bool writetofile;
		}
		
		private static readonly string DataDirectory;
		
		private static DictionaryEntry[] AllSettingsSerializable {
			get {
				DictionaryEntry[] entries = new DictionaryEntry[AllSettings.Count];
				int i = 0;
				foreach(KeyValuePair<string, Dictionary<string, SettingEntry>> settingType in AllSettings) {
					DictionaryEntry[] subEntries = new DictionaryEntry[settingType.Value.Count];
					int j = 0;
					foreach(KeyValuePair<string, SettingEntry> setting in settingType.Value) {
						if(setting.Value.writetofile) {
							object data = setting.Value.data;
							if(data is Uri) data = data.ToString();
							subEntries[j] = new DictionaryEntry(setting.Key, data);
							++j;
						}
					}
					entries[i] = new DictionaryEntry(settingType.Key, subEntries.Take(j).ToArray());
					++i;
				}
				return entries;
			}
			set {
				try {
					foreach(DictionaryEntry entry in value) {
						Dictionary<string, SettingEntry> subEntries = new Dictionary<string, SettingEntry>();
						foreach(DictionaryEntry subEntry in (DictionaryEntry[])entry.Value) {
							object val = null;
							switch((from x in ValidSettings where x.name == (string)subEntry.Key select x.type).First()) {
								case SettingType.Path:
									val = new Uri(subEntry.Value.ToString());
									break;
								case SettingType.Text:
									val = subEntry.Value.ToString();
									break;
								case SettingType.Bool:
									val = bool.Parse(subEntry.Value.ToString());
									break;
								case SettingType.Integer:
									val = int.Parse(subEntry.Value.ToString());
									break;
							}
							subEntries[(string)subEntry.Key] = new SettingEntry { data = val, writetofile = true };
						}
						AllSettings[(string)entry.Key] = subEntries;
						
						switch((string)entry.Key) {
							case "Main Settings":
								MainSettings = subEntries;
								break;
							
							case "Plugins":
								PluginSettings = subEntries;
								break;
						}
					}
				}
				catch { throw; }
			}
		}
		
		private static void LoadSettings() {
			XmlSerializer serializer = new XmlSerializer(typeof(DictionaryEntry[]));
			object obj;
			using(StreamReader reader = new StreamReader(Path.Combine(DataDirectory, "config.xml"))) {
				obj = serializer.Deserialize(reader);
			}
			
			if(!(obj is DictionaryEntry[])) return;
			
			AllSettingsSerializable = (DictionaryEntry[])obj;
		}
		
		public static void SaveSettings() {
			XmlSerializer serializer = new XmlSerializer(typeof(DictionaryEntry[]));
			using(StreamWriter writer = new StreamWriter(Path.Combine(DataDirectory, "config.xml"))) {
				serializer.Serialize(writer, AllSettingsSerializable);
			}
		}
	}
}

