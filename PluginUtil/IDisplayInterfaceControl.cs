using System;
using System.Collections.Generic;

namespace PluginUtil
{
	public interface IDisplayInterfaceControl {
		void SetContent(IEnumerable<XeFileInfo> fileList);
	}
}

