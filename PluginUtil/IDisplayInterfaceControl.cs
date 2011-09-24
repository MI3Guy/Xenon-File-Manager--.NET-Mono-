using System;
using System.Collections.Generic;

namespace Xenon.PluginUtil
{
	public interface IDisplayInterfaceControl {
		CacheStack<Uri> HistoryBack { get; set; }
		CacheStack<Uri> HistoryForward { get; set; }
		Uri CurrentLocation { get; set; }
		
		void SetContent(IEnumerable<XeFileInfo> fileList, Uri path);
		IEnumerable<XeFileInfo> SelectedFiles { get; }
		void NewFolder();
		void Rename();
		void Recycle();
		void Delete();
		void SelectAll();
		void SelectNone();
	}
}

