// 
//  ProgessWindow.cs
//  
//  Author:
//       John Bentley <pcguy49@yahoo.com>
//  
//  Copyright (c) 2011 John Bentley
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Gtk;
using Xenon.PluginUtil;
using Mono.Unix;

namespace Xenon.FileManager.GtkUI {
	public class ProgressWindow : Window {
		
		public ProgressWindow() : base(Catalog.GetString("File Operation Progress")) {
			vbox = new VBox();
			
			
			this.Add(vbox);
			vbox.ShowAll();
			
			DeleteEvent += OnDeleteEvent;
		}
		
		VBox vbox;
		
		private string FormatLabel(FileOperationType type, int current, int max) {
			string typestr = "";
			switch(type) {
				case FileOperationType.Copy:
					typestr = Catalog.GetString("Copying");
					break;
				
				case FileOperationType.Move:
					typestr = Catalog.GetString("Moving");
					break;
				
				case FileOperationType.Delete:
					typestr = Catalog.GetString("Deleting");
					break;
			}
			
			return string.Format(Catalog.GetPluralString("{0} {1} of {2} file/folder", "{0} {1} of {2} files/folders", max), typestr, current + 1, max);
		}
		
		public void AddOperation(GtkFileOperationProgress progress) {
			VBox subvbox = new VBox();
			subvbox.PackStart(new Label(), false, false, 0);
			ProgressBar bar = new ProgressBar();
			bar.Adjustment = new Adjustment(0.0, 0.0, 1.0, 1.0, 1.0, 1.0);
			subvbox.PackStart(bar, true, true, 0);
			vbox.PackStart(subvbox, false, true, 0);
			subvbox.ShowAll();
			
			progress.DisplayWidget = subvbox;
			progress.Adjustment = bar.Adjustment;
			this.ShowAll();
		}
		
		public void ProgressUpdate(GtkFileOperationProgress progress) {
			
		}
		
		public void Finish(GtkFileOperationProgress progress) {
			vbox.Remove(progress.DisplayWidget);
			if(vbox.Children.Length == 0) this.Hide();
		}
		
		
		protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
			a.RetVal = true;
		}
	}
}

