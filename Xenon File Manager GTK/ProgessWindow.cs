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

namespace Xenon.FileManager.GtkUI {
	public class ProgessWindow : Window {
		List<Widget> childList = new List<Widget>();
		List<ProgressBar> barList = new List<ProgressBar>();
		
		public ProgessWindow() : base(WindowType.Popup) {
			Title = "File Operation Progress";
			
			VBox vbox = new VBox();
			
			
		}
		
		public void AddOperation(object val) {
			
		}
	}
}

