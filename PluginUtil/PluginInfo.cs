using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace Xenon.PluginUtil {
	public class PluginInfo {
		public static PluginInfo FromFile(string file) {
			XmlSerializer serializer = new XmlSerializer(typeof(PluginInfo));
			PluginInfo obj;
			using(StreamReader reader = new StreamReader(file)) {
				obj = (PluginInfo)serializer.Deserialize(reader);
			}
			obj.DllName = Path.GetFileNameWithoutExtension(file);
			return obj;
		}
		
		public PluginInfo() {}
		
		private string name;
		private int revision;
		private string author;
		private string description;
		
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		public int Revision {
			get { return revision; }
			set { revision = value; }
		}
		
		public string Author {
			get { return author; }
			set { author = value; }
		}
		
		public string Description {
			get { return description; }
			set { description = value; }
		}
		
		[XmlIgnore()]
		public string DllName {
			get;
			set;
		}
		
		public static IEnumerable<PluginInfo> AllPlugins {
			get {
				DirectoryInfo directory = new DirectoryInfo(Path.Combine(CommonUtil.ExecutablePath, "plugins"));
				FileInfo[] files = directory.GetFiles("*.dll.xml");
				return from f in files select PluginInfo.FromFile(f.FullName);
			}
		}
	}
}

