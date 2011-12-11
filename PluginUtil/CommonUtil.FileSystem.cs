// 
//  CommonUtil.FileSystem.cs
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
using System.Linq;
using System.Threading;
using Mono.Unix;

namespace Xenon.PluginUtil {
	
	public static partial class CommonUtil {
		public static class FileSystem {
			
			public static bool HandlesDirectory(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.LoadsUriType(path)) {
						return true;
					}
				}
				return false;
			}
			
			public static XeFileInfo[] DirectoryListing(string dir, out Uri pathOut) {
				Uri path = new Uri(dir);
				XeFileInfo[] fi = DirectoryListing(ref path);
				pathOut = path;
				return fi;
			}
			
			public static XeFileInfo[] DirectoryListing(ref Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.LoadsUriType(path)) {
						XeFileInfo[] fi = handler.LoadDirectory(ref path);
						if(fi == null && !handler.HandlesUriType(path)) return DirectoryListing(ref path);
						return fi;
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static void LoadFile(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						handler.LoadFile(path);
						return;
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static Uri Combine(Uri path, string part) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.LoadsUriType(path)) {
						return handler.Combine(path, part);
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static Uri ParentDirectoryFor(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						return handler.ParentDirectory(path);
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static string[] FileNameFor(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						return handler.FileName(path);
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static bool Exists(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						return handler.Exists(path);
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static string DisplayPath(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						return handler.DisplayPath(path);
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static string ShortPath(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						return handler.ShortPath(path);
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static void CreateDirectory(Uri path) {
				Uri parent = new Uri(path, "..");
				foreach(FileSystemHandler handler in CommonUtil.fsHandlers) {
					if(handler.LoadsUriType(parent)) {
						handler.CreateDirectory(path);
						return;
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static void CopyAsync(Uri[] src, Uri[] dest, IFileOperationProgress progress) {
				FileSystemHandler fsSrc = null, fsDst = null;
				FileSystemHandler fsSrc2 = null, fsDst2 = null;
				
				Console.WriteLine("{0}, {1}", src[0], dest[0]);
				
				if(src.Length != dest.Length) throw new ArgumentException();
				for(int i = 0; i < src.Length; ++i) {
					foreach(FileSystemHandler handler in CommonUtil.fsHandlers) {
						if(handler.HandlesUriType(src[i])) {
							fsSrc = handler;
						}
						
						Uri parent = new Uri(dest[i], "..");
						if(handler.HandlesUriType(parent)) {
							fsDst = handler;
						}
					}
					
					if(fsSrc == null || fsDst == null) {
						throw new PluginNotFoundException();
					}
					
					if(i > 0) {
						if(fsSrc != fsSrc2 || fsDst != fsDst2) {
							throw new ArgumentException();
						}
					}
					else {
						fsSrc2 = fsSrc;
						fsDst2 = fsDst;
					}
				}
				
				if(fsSrc == fsDst) {
					progress.Start();
					Thread thread = new Thread(delegate() {
						fsSrc.CopyAsync(src, dest, progress);
					});
					thread.Start();
				}
				else {
					throw new NotImplementedException();
				}
			}
			
			public static void Move(Uri src, Uri dest) {
				Uri parent = new Uri(dest, "..");
				FileSystemHandler fsSrc = null, fsDst = null;
				
				foreach(FileSystemHandler handler in CommonUtil.fsHandlers) {
					if(handler.HandlesUriType(src)) {
						fsSrc = handler;
					}
					if(handler.HandlesUriType(parent)) {
						fsDst = handler;
					}
				}
				
				if(fsSrc == null || fsDst == null) {
					throw new PluginNotFoundException();
				}
				
				if(fsSrc == fsDst) {
					fsSrc.Move(src, dest);
				}
				else {
					throw new NotImplementedException();
				}
			}
			
			public static void Recycle(Uri path) {
				foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						handler.Recycle(path);
						return;
					}
				}
				throw new PluginNotFoundException();
			}
			
			public static void DeleteAsync(IEnumerable<Uri> paths) {
				/*foreach(FileSystemHandler handler in fsHandlers) {
					if(handler.HandlesUriType(path)) {
						handler.Delete(path);
						return;
					}
				}
				throw new PluginNotFoundException();*/
			}
			
			public static void OnUpdated(object sender, FileSystemHandler.FileSystemUpdateEventArgs e) {
				CommonUtil.LoadDirectory(e.Control.CurrentLocation, e.Control, false);
			}
			
			public static void UpdateWatchList(IEnumerable<IDisplayInterfaceControl> controls) {
				foreach(FileSystemHandler handler in fsHandlers) {
					try {
						handler.UpdateWatchList(from control in controls where handler.LoadsUriType(control.CurrentLocation) select control);
					}
					catch(NotImplementedException) { }
				}
			}
			
			public static Uri CopyOperationDestination(Uri src, Uri currentLocation) {
				if(ParentDirectoryFor(src).ToString().TrimEnd('\\', '/') == currentLocation.ToString().TrimEnd('\\', '/')) {
					string[] parts = FileNameFor(src);
					Uri dest = Combine(currentLocation, string.Format(Catalog.GetString("{0} - Copy{1}"), parts[1], parts[2]));
					for(int i = 1; Exists(dest); ++i) {
						dest = Combine(currentLocation, string.Format(Catalog.GetString("{0} - Copy ({2}){1}"), parts[1], parts[2], i));
					}
					return dest;
				}
				else {
					return new Uri(currentLocation, FileNameFor(src)[0]);
				}
			}
		}
	}
	
}