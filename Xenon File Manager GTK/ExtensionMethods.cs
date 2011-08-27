using System;
using Gtk;

namespace Xenon.FileManager.GtkUI {
	public static class ExtensionMethods {
		public static void RemovePageByLabel(this Notebook nb, Widget label) {
			if(nb.NPages < 2) return;
			for(int i = 0; i < nb.NPages; ++i) {
				if(nb.GetTabLabel(nb.GetNthPage(i)) == label) {
					nb.RemovePage(i);
					break;
				}
			}
		}
	}
}

