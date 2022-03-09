using System.Windows.Forms;

namespace Ruski_rulet
{
	public partial class Player : Form
	{
		Pocetak pocetak;
		Ruski_rulet rulet;
		string ind = "";

		public Player()
		{
			InitializeComponent();
		}

		public void play(Pocetak pocetak1, string url)
		{
			pocetak = pocetak1;
			this.Show();
			player1.URL = url;
			player1.Ctlcontrols.play();
			ind = "pocetak";
		}

		public void play(Ruski_rulet rulet1, string url)
		{
			rulet = rulet1;
			this.Show();
			player1.URL = url;
			player1.Ctlcontrols.play();
			ind = "rulet";
		}

		private void player1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
		{
			if (e.newState == 8)
			{
				if (pocetak != null)
					pocetak.Show();
				else
					rulet.Show();
				this.Close();
			}
			else
			{
				if (pocetak != null)
					pocetak.Hide();
				else
					rulet.Hide();
			}
		}
	}
}
