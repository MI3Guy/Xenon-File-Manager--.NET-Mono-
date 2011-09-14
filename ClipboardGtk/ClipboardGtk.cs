using System;
using System.Text;
using System.Collections.Generic;
using Gtk;
using Xenon.PluginUtil;

namespace Xenon.Plugin.ClipboardGtk {
	public class ClipboardGtk : ClipboardHandler {
		
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			return ui == PluginUIType.Gtk;
		}
		
		private Gtk.Clipboard clip;
		private ClipboardData inData;
		private ClipboardData outData;
		private EventHandler evt;
		
		public override void InitPlugin() {
			clip = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
		}
		
		public override void ExposePaths(ClipboardData paths) {
			TargetEntry target0 = new TargetEntry("x-special/gnome-copied-files", 0, 0);
        	TargetEntry target1 = new TargetEntry("text/uri-list", 0, 0);
			
			outData = paths;

        	clip.SetWithData(new TargetEntry[] {target0, target1}, ClearGet, ClearFunc);
		}
		
		public override void RequestPaths(EventHandler evt) {
			this.evt = evt;
        	clip.RequestContents(Gdk.Atom.Intern("x-special/gnome-copied-files", false), ReceivedFunc);
		}
		
		private void ReceivedFunc(Clipboard clipboard, SelectionData selection) {
			string temp = Encoding.ASCII.GetString(selection.Data);
			if (temp==null) return;
			
			string[] items = temp.Split();
			List<Uri> paths = new List<Uri>(items.Length);
			for(int i = 1; i < items.Length; ++i) {
				if(items[i] == string.Empty) continue;
				Uri fileFrom = new Uri(items[i]);
				paths.Add(fileFrom);
			}
			
			inData = new ClipboardData(paths, items[0] == "cut" ? OperationType.Cut : OperationType.Copy);
			evt(null, null);
		}
		
		public override ClipboardData PastePaths() {
			return inData;
		}
		
		
		
		private void ClearGet(Gtk.Clipboard clipboard, Gtk.SelectionData selection, uint info) {
	        StringBuilder temp = new StringBuilder();
			temp.Append(outData.Operation == OperationType.Cut ? "cut" : "copy");
			foreach(Uri path in outData.Paths) {
				temp.Append('\n');
				temp.Append(path.ToString());
			}
	        selection.Set(selection.Target, 8, Encoding.ASCII.GetBytes(temp.ToString())); 
	    }
	
	    private void ClearFunc(Gtk.Clipboard clipboard) {
	        outData = null;
	    }
		
	}
}

