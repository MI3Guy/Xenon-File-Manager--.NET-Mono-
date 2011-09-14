//  
//  SettingsDialog.cs
//  
//  Author:
//       john <${AuthorEmail}>
// 
//  Copyright (c) 2011 john
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using Xenon.PluginUtil;

namespace Xenon.FileManager.GtkUI {
	public class SettingsDialog : Window {
		public SettingsDialog() : base("Settings") {
			Modal = true;
			//Title = "Settings";
			
			this.SetSizeRequest(400, 400);
			
			VBox vbox1 = new VBox(false, 0);
			Notebook nb =  new Notebook();
			ScrolledWindow sw = new ScrolledWindow();
			VBox vbox2 = new VBox(false, 0);
			settingNames = new SettingsUtil.ConstSettingEntry[SettingsUtil.MainSettings.Count];
			settingCtrls = new Widget[SettingsUtil.MainSettings.Count];
			int i = 0;
			foreach(KeyValuePair<string, SettingsUtil.SettingEntry> entry in SettingsUtil.MainSettings) {
				//try {
					SettingsUtil.ConstSettingEntry constEntry = SettingsUtil.ValidSettings.Where(x => entry.Key == x.name).First();
					string dispName = constEntry.displayName;
					Console.WriteLine(dispName);
					settingNames[i] = constEntry;
					
					switch(constEntry.type) {
						case SettingsUtil.SettingType.Text:
						case SettingsUtil.SettingType.Path:
							HBox hbox = new HBox(false, 0);
							Label lbl = new Label(dispName);
							hbox.PackStart(lbl, false, false, 0);
							vbox2.PackStart(hbox, false, false, 0);
							Entry textbox = new Entry();
							textbox.Text = entry.Value.writetofile ? entry.Value.data.ToString() : "";
							vbox2.PackStart(textbox, false, false, 0);
							settingCtrls[i] = textbox;
							break;
						case SettingsUtil.SettingType.Bool:
							CheckButton checkBtn = new CheckButton(dispName);
							checkBtn.Inconsistent = !entry.Value.writetofile;
							checkBtn.Active = (bool)entry.Value.data;
							checkBtn.Toggled += new EventHandler(OnToggle);
							vbox2.PackStart(checkBtn, false, false, 0);
							settingCtrls[i] = checkBtn;
							break;
					}
				//}
				//catch {}
				++i;
			}
			sw.Add(vbox2);
			nb.AppendPage(sw, new Label("Main Settings"));
			
			sw = new ScrolledWindow();
			vbox2 = new VBox(false, 0);
			foreach(PluginInfo pi in from p in PluginInfo.AllPlugins orderby p.Name select p) {
				HBox hbox = new HBox(false, 0);
				Label lbl = new Label(pi.Name);
				hbox.PackStart(lbl, false, false, 0);
				vbox2.PackStart(hbox, false, false, 0);
				
				hbox = new HBox(false, 0);
				lbl = new Label(string.Format("Version: {0}", pi.Revision));
				hbox.PackStart(lbl, false, false, 0);
				vbox2.PackStart(hbox, false, false, 0);
				
				hbox = new HBox(false, 0);
				lbl = new Label(pi.Author);
				hbox.PackStart(lbl, false, false, 0);
				vbox2.PackStart(hbox, false, false, 0);
				
				hbox = new HBox(false, 0);
				lbl = new Label(pi.Description);
				hbox.PackStart(lbl, false, false, 0);
				vbox2.PackStart(hbox, false, false, 0);
				
				CheckButton checkBtn = new CheckButton("Enabled");
				//checkBtn.Active = (bool)entry.Value.data;
				vbox2.PackStart(checkBtn, false, false, 0);
				
				vbox2.PackStart(new Label(""), false, false, 0);
			}
			sw.Add(vbox2);
			nb.AppendPage(sw, new Label("Plugins"));
			
			
			vbox1.Add(nb);
			HButtonBox hbtnbox = new HButtonBox();
			Button cancelButton = new Button(Stock.Cancel);
			Button okButton = new Button(Stock.Ok);
			hbtnbox.Add(cancelButton);
			hbtnbox.Add(okButton);
			vbox1.PackStart(hbtnbox, false, false, 0);
			this.Add(vbox1);
			//this.Add(vbox1);
			
			cancelButton.Clicked += OnCancelClicked;
			okButton.Clicked += OnOkClicked;
		}
		
		SettingsUtil.ConstSettingEntry[] settingNames;
		Widget[] settingCtrls;
		
		void OnToggle(object sender, EventArgs e)  {
	        CheckButton cb = (CheckButton)sender;
			cb.Inconsistent = false;
	    }
		
		void OnCancelClicked(object sender, EventArgs e) {
			this.Destroy();
		}
		
		void OnOkClicked(object sender, EventArgs e) {
			Save();
			this.Destroy();
		}
		
		void Save() {
			for(int i = 0; i < settingNames.Length; ++i) {
				if(settingCtrls[i] is Entry) {
					SettingsUtil.MainSettings[settingNames[i].name].writetofile = ((Entry)settingCtrls[i]).Text != "";
					if(SettingsUtil.MainSettings[settingNames[i].name].writetofile) {
						if(settingNames[i].type == SettingsUtil.SettingType.Path) {
							string text = ((Entry)settingCtrls[i]).Text;
							if(text == "/") text = "file:///";
							SettingsUtil.MainSettings[settingNames[i].name].data = new Uri(text);
						}
						else {
							SettingsUtil.MainSettings[settingNames[i].name].data = new Uri(((Entry)settingCtrls[i]).Text);
						}
					}
					else {
						SettingsUtil.MainSettings[settingNames[i].name] = SettingsUtil.DefaultSetting(settingNames[i].name);
					}
				}
				else if(settingCtrls[i] is CheckButton) {
					SettingsUtil.MainSettings[settingNames[i].name].writetofile = !((CheckButton)settingCtrls[i]).Inconsistent;
					if(SettingsUtil.MainSettings[settingNames[i].name].writetofile) {
						SettingsUtil.MainSettings[settingNames[i].name].data = ((CheckButton)settingCtrls[i]).Active;
					}
					else {
						SettingsUtil.MainSettings[settingNames[i].name] = SettingsUtil.DefaultSetting(settingNames[i].name);
					}
				}
			}
		}
	}
}

