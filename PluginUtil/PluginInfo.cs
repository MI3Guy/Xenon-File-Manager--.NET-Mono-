using System;
using System.Xml;

namespace Xenon.PluginUtil {
	public class PluginInfo {
		public PluginInfo(string file) {
			XmlTextReader xmlFile = new XmlTextReader(file); 
			string tmpName = null;
		    while(xmlFile.Read()) {
				switch(xmlFile.NodeType) {
					case XmlNodeType.Element:
						tmpName = xmlFile.Name;
						break;
					case XmlNodeType.Text:
						switch(tmpName) {
							case "name":
								name = xmlFile.Value;
								break;
							case "version":
								try {
									version = int.Parse(xmlFile.Value);
								}
								catch { }
								break;
							case "author":
								author = xmlFile.Value;
								break;
							case "description":
								description = xmlFile.Value;
								break;
						}
						break;
				}
			}
		}
		
		private string name;
		private int version;
		private string author;
		private string description;
		
		
		
	}
}

