// 
//  IFileOperationProgress.cs
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

namespace Xenon.PluginUtil {
	public interface IFileOperationProgress {
		FileOperationType Operation { get; set; }
		void Start();
		void UpdateProgress(int current, int max, double progress, double? bitrate, TimeSpan? etr);
		FileErrorAction OnError(Uri src, Uri dest);
		bool? OnOverwrite(Uri src, Uri dest, out bool applytoall);
		void Finish();
	}
	
	public enum FileOperationType {
		Copy,
		Move,
		Delete
	}
	
	public enum FileErrorAction {
		Abort,
		Retry,
		Ignore
	}
}

