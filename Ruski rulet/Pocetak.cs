using System;
using System.Windows.Forms;

namespace Ruski_rulet
{
	public partial class Pocetak : Form
	{
		Ruski_rulet rulet;

		public Pocetak(Ruski_rulet rulet1)
		{
			InitializeComponent();
			rulet = rulet1;
		}

		private void ok_Click(object sender, EventArgs e)
		{
			string[] imena = new string[] { ime1.Text, ime2.Text, ime3.Text, ime4.Text, ime5.Text, ime6.Text };
			rulet.Pocetna_podesavanja(imena);
			rulet.Show();
			this.Close();
		}
	}
}
