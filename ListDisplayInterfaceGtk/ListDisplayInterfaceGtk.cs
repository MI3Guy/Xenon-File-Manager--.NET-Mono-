using System;
using PluginUtil;

namespace ListDisplayInterfaceGtk {
	public class ListDisplayInterfaceGtk : DisplayInterfaceHandler {
		public ListDisplayInterfaceGtk() {
			
		}
		
		public override bool DoLoad(PluginUIType ui, PluginOSType os) {
			return ui == PluginUIType.Gtk;
		}
		
		public override IDisplayInterfaceControl InitControl() {
			return new ListDisplayWidget();
		}
	}
}

