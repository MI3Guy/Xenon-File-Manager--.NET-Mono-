using System;
using System.Collections.Generic;

namespace PluginUtil
{
	public interface IDisplayInterfaceControl {
		void SetContent(IEnumerable<XeFileInfo> fileList, Uri path);
		CacheStack<Uri> HistoryBack { get; set; }
		CacheStack<Uri> HistoryForward { get; set; }
		Uri CurrentLocation { get; set; }
	}
}

