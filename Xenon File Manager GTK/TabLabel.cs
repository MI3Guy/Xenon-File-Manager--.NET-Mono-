using System;
using Gtk;


namespace Xenon.FileManager.GtkUI {
	public class TabLabel : HBox {
		public TabLabel(string text, Notebook _nb) {
			Button btn = new Button();
			RcStyle rcStyle = new RcStyle ();
			rcStyle.Xthickness = 0;
			rcStyle.Ythickness = 0;
			btn.ModifyStyle (rcStyle);
			btn.Image = new Image(Stock.Close, IconSize.Menu);
			PackStart(TextLabel = new Label(text), true, true, 1);
			PackStart(btn, false, false, 0);
			CloseButton = btn;
			
			nb = _nb;
			//tabWidget = _tabWidget;
			
			CloseButton.Clicked += new EventHandler(OnCloseButtonClick);
			
			ShowAll();
		}
		
		public Label TextLabel { get; set; }
		public Button CloseButton { get; set; }
		//private Widget tabWidget;
		private Notebook nb;
		
		protected void OnCloseButtonClick(object sender, EventArgs e) {
			nb.RemovePageByLabel(this);
		}
	}
}

