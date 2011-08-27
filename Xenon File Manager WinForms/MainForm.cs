using System;
using System.Windows.Forms;
using System.Drawing;

namespace XenonFileManagerWinForms {
	public class MainForm : Form {
		public MainForm() {
			this.Text = "Xenon File Manager";
			this.Size = new Size(600, 450);
			this.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("XenonFileManagerWinForms.xenon.ico"));
			
			
			ToolBar tbar = new ToolBar();
			ToolBarButton button = new ToolBarButton("Back");
			button.ImageIndex = 0;
			tbar.Buttons.Add(button);
			
			this.Controls.Add(tbar);
			
			//Controls.Add(toolContainer);
			
		}
	}
}

