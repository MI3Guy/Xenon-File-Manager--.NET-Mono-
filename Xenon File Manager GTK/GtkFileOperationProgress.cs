// 
//  GtkFileOperationProgress.cs
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
using Gtk;
using Xenon.PluginUtil;

namespace Xenon.FileManager.GtkUI {
	public class GtkFileOperationProgress : IFileOperationProgress {
		public GtkFileOperationProgress(ProgressWindow progress) {
			this.progress = progress;
		}
		
		private ProgressWindow progress;
		
		public ProgressBar DisplayWidget { get; set; }
		public Label Label { get; set; }
		public Adjustment Adjustment { get; set; }
		
		public FileOperationType Operation {
			get;
			set;
		}
		
		public void Start() {
			progress.AddOperation(this);
		}
		
		public void UpdateProgress(int current, int max, double proportion, double? bitrate, TimeSpan? etr) {
			Application.Invoke(delegate(object sender, EventArgs e) {
				progress.ProgressUpdate(this, current, max, proportion, bitrate, etr);
			});
		}
		
		public FileErrorAction OnError(Uri src, Uri dest) {
			return FileErrorAction.Abort;
		}
		
		public bool? OnOverwrite(Uri src, Uri dest, out bool applytoall) {
			applytoall = false;
			return null;
		}  
		
		public void Finish() {
			Application.Invoke(delegate(object sender, EventArgs e) {
				progress.Finish(this);
			});
		}
		
	}
}

