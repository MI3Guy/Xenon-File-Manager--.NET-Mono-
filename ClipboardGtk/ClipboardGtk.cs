using System;
using Gtk;
using Xenon.PluginUtil;

namespace ClipboardGtk
{
	public class ClipboardGtk : ClipboardHandler {
		
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			return ui == PluginUIType.Gtk;
		}
		
		private Gdk.Display display;
		
		public override void InitClipboard(object obj) {
			try {
				display = (Gdk.Display)obj;
			}
			catch {}
		}
		
		public override void CopyPath (Uri path) {
			//if(display == null) return;
			//Clipboard clip = Clipboard.GetForDisplay(display, Gdk.Selection.Clipboard);
			//clip.
		}
		
		public override void GetPath (Uri path)
		{
			throw new NotImplementedException ();
		}
		
	}
}

