using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Ruski_rulet
{
	public partial class Ruski_rulet : Form
	{
		int ind = 0;
		Label[] novac = new Label[12];
		Label[] ime = new Label[12];
		Button[] izazovi = new Button[6];
		List<int> uzasotpno = new List<int>();
		string[] pitanja;
		string[] odgovori;
		int[] povezana_pitanja;
		int[] pitanja_po_rundama;
		int runda;
		int izazivac = 0;
		int izazvao = 0;
		int p = 100;
		int[] pitanja_5 = new int[10];
		int[] odgovorena_pitanja_5 = new int[10];
		int broj_odgovorenih_pitanja = 0;
		int pitanje_po_redu = 0;
		int RR_potez = 0;
		int RR_tick = 0;
		int RR_potez1 = 0;
		int RR_brzina = 0;
		int cekanje_tick = 0;
		int prazna_imena = 0;
		int[] ticks = new int[9];
		string prethodno_pitanje = null;
		bool player_is_free = true;
		string game_stage = "spica";
		int opasno_polje;
		int pocetna_runda;

		public Ruski_rulet()
		{
			InitializeComponent();
			System.IO.Directory.SetCurrentDirectory(@"..\..\Resources");
			string url = "Špica";
			play_video(url);
			//	Sledeca 3 reda su u svrhu testiranja, da se zaobidje uvodna spica i pisanje imena:
			//	string[] imena = new string[] { "1", "2", "3", "4", "5", "6" };
			//	string[] imena = new string[] { "1", "2", "3", "", "", "" };
			//	Pocetna_podesavanja(imena);
		}

		public void Pocetna_podesavanja(string[] imena)
		{
			novac[0] = novac1;
			novac[1] = novac2;
			novac[2] = novac3;
			novac[3] = novac4;
			novac[4] = novac5;
			novac[5] = novac6;
			novac[6] = novac1;
			novac[7] = novac2;
			novac[8] = novac3;
			novac[9] = novac4;
			novac[10] = novac5;
			novac[11] = novac6;

			ime[0] = ime1;
			ime[1] = ime2;
			ime[2] = ime3;
			ime[3] = ime4;
			ime[4] = ime5;
			ime[5] = ime6;
			ime[6] = ime1;
			ime[7] = ime2;
			ime[8] = ime3;
			ime[9] = ime4;
			ime[10] = ime5;
			ime[11] = ime6;

			izazovi[0] = izazovi1;
			izazovi[1] = izazovi2;
			izazovi[2] = izazovi3;
			izazovi[3] = izazovi4;
			izazovi[4] = izazovi5;
			izazovi[5] = izazovi6;

			biranje_izazivaca.Interval = 100;//desetina sekunde
			ticks[0] = 2;
			ticks[1] = 2;
			ticks[2] = 2;
			ticks[3] = 2;
			ticks[4] = 3;
			ticks[5] = 4;
			ticks[6] = 6;
			ticks[7] = 8;
			ticks[8] = 10;

			string file = System.IO.Directory.GetCurrentDirectory() + @"\Da li su pitanja povezana.txt";
			pitanja = File.ReadAllLines(file);
			povezana_pitanja = new int[pitanja.Length];
			for (int i = 0; i < pitanja.Length; i++)
				povezana_pitanja[i] = int.Parse(pitanja[i]);
			file = System.IO.Directory.GetCurrentDirectory() + @"\Pitanja po rundama.txt";
			pitanja = File.ReadAllLines(file);
			pitanja_po_rundama = new int[pitanja.Length];
			for (int i = 0; i < pitanja.Length; i++)
				pitanja_po_rundama[i] = int.Parse(pitanja[i]);
			file = System.IO.Directory.GetCurrentDirectory() + @"\Pitanja.txt";
			pitanja = File.ReadAllLines(file);
			file = System.IO.Directory.GetCurrentDirectory() + @"\Odgovori.txt";
			odgovori = File.ReadAllLines(file);

			var random = new Random();
			imena = imena.OrderBy(x => random.Next()).ToArray();
			for (int i = 0; i < 6; i++)
			{
				ime[i].Text = imena[i];
				if (ime[i].Text.Equals(""))
				{
					prazna_imena++;
					novac[i].Text = "";
				}
			}

			runda = prazna_imena;
			pocetna_runda = prazna_imena;
			pitanje.Text = "Želim vam srećan prelazak preko minskog polja uz pesmu: \"Učini jedan pogrešan korak\"!!!";
			play_video("Runda " + runda);
			game_stage = "biranje izazivaca";
		}

		private void Ruski_rulet_FormClosed(object sender, FormClosedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void initialize_timer()
		{
			RR_potez = 0;
			RR_tick = 0;
			var random = new Random();
			RR_brzina = random.Next(3) + 3;
		}

		private void biranje_izazivaca_Tick(object sender, EventArgs e)
		{
			//	if (RR_potez < 9)
			//		RR_potez = 9;//Za potrebe testiranja preskace se pocetni ruski rulet.

			RR_tick++;

			if (RR_potez >= 9)//otkljucavanje opasnog polja i prelazak na pitanje
			{
				if (RR_tick != 10)
					return;
				for (int i = 0; i < 6; i++)
				{
					novac[i].BackColor = Color.White;
					ime[i].BackColor = Color.White;
				}
				otkljucavanje_opasnih_polja(1);
				pitanje_po_redu = 0;
				return;
			}

			//odredjivanje prvog izazivaca
			var random = new Random();
			if (ticks[RR_potez] == RR_tick)//Sto vise polja je proslo, ruski rulet se sporije vrti.
			{
				RR_potez1++;
				RR_tick = 0;
				izazivac++;
				if (izazivac > 6)
					izazivac -= 6;
				ime[izazivac].BackColor = Color.LightBlue;
				novac[izazivac].BackColor = Color.LightBlue;
				ime[izazivac - 1].BackColor = Color.White;
				novac[izazivac - 1].BackColor = Color.White;
				play_sound("Ruski rulet");
				if (ime[izazivac].Text.Equals(""))
					RR_potez1--;
				if (RR_potez1 == RR_brzina)//promena brzine
				{
					RR_potez1 = 0;
					RR_potez++;
					RR_brzina = random.Next(1) + 2;//broj poteza ruskog ruleta pre sledece promene brzine
				}
			}
		}

		private void spica_Tick(object sender, EventArgs e)
		{
			if (player_is_free)
			{
				spica.Enabled = false;
				runda++;
				play_video("Runda " + runda);
				game_stage = "sledeca runda 2";
			}
		}

		private void runda1234()
		{
			if (pitanje_po_redu >= 10)//kraj runde
			{
				int indmax = najvise_novca();
				if (runda < 4 || indmax > -1)
				{
					pitanje.Text = "Kraj runde";
					if (indmax > -1)
						pitanje.Text += Environment.NewLine + ime[indmax].Text + " ima najviše novca i sigurno neće ispasti.";
					game_stage = "kraj runde";
					rucica.Value = 0;
					rucica.Visible = true;
					return;
				}
			}

			if (runda == 4)
				for (int i = 0; i < 6; i++)
					if (i != izazivac && !ime[i].Text.Equals(""))
					{
						izazvao = i;
						break;
					}

			for (int i = 0; i < 6; i++)
			{
				novac[i].BackColor = Color.White;
				ime[i].BackColor = Color.White;
			}
			novac[izazivac].BackColor = Color.LightBlue;
			ime[izazivac].BackColor = Color.LightBlue;
			biranje_pitanja(1);
			while (izazivac >= 6)
				izazivac -= 6;
			for (int i = 0; i < 6; i++)
				if (!novac[i].Text.Equals(""))
					izazovi[i].Visible = true;
			if (runda < 4)
				izazovi[izazivac].Visible = false;
			else
				izazovi[izazivac].Text = "Zadrži";

			cekanje_tick = Math.Max(20, pitanje.Text.Length / 15 + 15);
			cekanje.Enabled = true;

			if ((runda == 4 && pitanje_po_redu == 20) || pitanje_po_redu == 10)
			{
				return;
			}
		}

		private void kraj_runde_Tick(object sender, EventArgs e)
		{
			//	if (RR_potez < 8)
			//		RR_potez = 8;//Za potrebe testiranja preskace se pocetni ruski rulet.

			RR_tick++;

			if (RR_potez >= 9 || runda == 4)//otkljucavanje opasnog polja i prelazak na pitanje
			{
				if (RR_tick == 10 || runda == 4)
				{
					if (runda == 4)
						izazivac = najmanje_novca();
					ime[izazivac].BackColor = Color.Black;
					novac[izazivac].BackColor = Color.Black;
					pitanje.Text = ime[izazivac].Text + " ispade iz igre i ode sa " + novac[izazivac].Text + " evra.";
					ime[izazivac].Text = "";
					novac[izazivac].Text = "";
					play_sound(new string[] { "Propadanje", "Padanje " + runda });
					//	play_sound(new string[] { "Propadanje", "Kua kua kua" });
					kraj_runde.Enabled = false;
					game_stage = "sledeca runda";
				}
				return;
			}

			var random = new Random();
			if (ticks[RR_potez] == RR_tick)//Sto vise polja je proslo, ruski rulet se sporije vrti.
			{
				RR_potez1++;
				RR_tick = 0;
				izazivac++;
				if (izazivac > 6)
					izazivac -= 6;
				ime[izazivac].BackColor = Color.Red;
				novac[izazivac].BackColor = Color.Red;
				ime[izazivac - 1].BackColor = Color.White;
				novac[izazivac - 1].BackColor = Color.White;
				play_sound("Ruski rulet");
				if (ime[izazivac].Text.Equals("") || izazivac == najvise_novca())
					RR_potez1--;
				if (RR_potez1 == RR_brzina)//promena brzine
				{
					RR_potez1 = 0;
					RR_potez++;
					RR_brzina = random.Next(3) + 3;//broj poteza ruskog ruleta pre sledece promene brzine
				}
			}
		}

		private int[] biranje_pitanja(int n)
		{
			pitanje_po_redu++;
			int[] pp = new int[n];
			var random = new Random();
			for (int i = 0; i < n; i++)
			{
				if (povezana_pitanja[p + 1] > 0)
				{
					p++;
					pitanje.Text = pitanja[p];
					pp[i] = p;
					continue;
				}
				p = random.Next(20);
				if (prethodno_pitanje != null)
				{
					if (p == 0)
					{
						prethodno_pitanje = pitanje.Text;
						pitanje.Text = "Koje je bilo prethodno pitanje?";
						pp[i] = -2;
						continue;
					}
					if (p == 1)
					{
						pitanje.Text = "Koje je ovo pitanje po redu u rundi?";
						pp[i] = -1;
						continue;
					}
				}
				//provera runde
				while (true)
				{
					p = random.Next(pitanja.Length);
					if (runda == 5)
					{
						if (pitanja_po_rundama[p] >= 2)
						{
							if (pitanja[p].Length / 15 + 15 <= 20)
								break;
						}
					}
					else if (runda == 3 || runda == 4)
					{
						if (pitanja_po_rundama[p] == runda)
							break;
					}
					else if (runda == 2)
					{
						if (pocetna_runda == 2)
						{
							if (pitanja_po_rundama[p] <= 2)
								break;
						}
						else
							if (pitanja_po_rundama[p] == 2)
							break;
					}
					else if (runda == 1)
					{
						if (pocetna_runda == 1)
						{
							if (pitanja_po_rundama[p] == 1)
								break;
						}
						else
							if (pitanja_po_rundama[p] <= 2)
							break;
					}
					else
						if (pitanja_po_rundama[p] == 1)
						break;
				}

				if (povezana_pitanja[p] > 0)
					p = p - povezana_pitanja[p];
				if (povezana_pitanja[p] == 0 && n > 1)
				{
					while (povezana_pitanja[p + 1] > 0)
						p++;
					if (i + povezana_pitanja[p] >= n)
						i = n - povezana_pitanja[p] - 1;
					p = p - povezana_pitanja[p];
				}
				pitanja[p] = pitanja[p].Replace("NEWLINE", Environment.NewLine);
				odgovori[p] = odgovori[p].Replace("NEWLINE", Environment.NewLine);
				if (runda < 5)
					pitanje.Text = pitanja[p];
				pp[i] = p;
			}
			return pp;
		}

		private void cekanje_Tick(object sender, EventArgs e)
		{
			game_stage = "cekanje izazivanja";
			cekanje_tick--;
			if (player_is_free)
				play_sound("Izazivanje");
			if (cekanje_tick == 0)
			{
				int igrac = 0;
				if (runda != 4)
				{
					do
					{
						var random = new Random();
						igrac = random.Next(6);
					} while (ime[igrac].Equals("") || izazovi[igrac].Visible == false);
					izazivanje(igrac);
				}
				else
					izazivanje(izazivac);
			}
		}

		private void izazivanje(int igrac)
		{
			game_stage = "izazivanje";
			cekanje.Enabled = false;
			play_sound("Otkucavanje vremena");
			novac[izazivac].BackColor = Color.White;
			ime[izazivac].BackColor = Color.White;
			novac[igrac].BackColor = Color.LightBlue;
			ime[igrac].BackColor = Color.LightBlue;
			for (int i = 0; i < 6; i++)
			{
				izazovi[i].Visible = false;
				izazovi[i].Text = "Izazovi";
			}
			tacno.Visible = true;
			vreme.Text = "20";//20
			vreme.Visible = true;
			cekanje_tick = 3;
			odgovaranje.Enabled = true;
			if (runda < 4)
				izazvao = izazivac;
			else
				izazvao = izazivac + izazvao - igrac;
			izazivac = igrac;
		}

		private void odgovaranje_Tick(object sender, EventArgs e)
		{
			game_stage = "odgovaranje";
			vreme.Text = (int.Parse(vreme.Text) - 1).ToString();
			if (runda < 5)
			{
				if (vreme.Text.Equals("-1"))
				{
					vreme.Text = "0";
					stop_sound();
					if (cekanje_tick >= 0)
						cekanje_tick--;
					else
					{
						odgovaranje.Enabled = false;
						netacno.Visible = true;
						pitanje.Text = odgovori[p];
						netacno.PerformClick();
					}
				}
			}
			else
			{

			}
		}

		private void tacno_Click(object sender, EventArgs e)
		{
			int opasna_polja = 0;
			if (tacno.Text.Contains("odgovor"))
			{
				runda_5.Enabled = false;
				tacno.Text = "Tačno";
				netacno.Visible = true;
				netacno.Text = "Netačno";
				odgovaranje.Enabled = false;
				stop_sound();
				if (runda < 5)
				{
					vreme.Visible = false;
					pitanje.Text = odgovori[p];
				}
				else
					pitanje.Text = odgovori[pitanja_5[pitanje_po_redu - 1]];
			}
			else if (tacno.Text.Contains("DA"))
			{
				pitanje.Text = "";
				pitanje_po_redu = Math.Max(opasna_polja, 1) * 2;
				tacno.Visible = false;
				netacno.Visible = false;
				rucica.Value = 0;
				rucica.Visible = true;
			}
			else if (tacno.Text.Contains("Tačno"))
			{
				tacno.Text = "Pokaži odgovor";
				if (runda < 5)
				{
					game_stage = "tacan odgovor";
					cekanje_tick = 0;
					play_sound("Tačan odgovor");
					tacno.Visible = false;
					netacno.Visible = false;
					vreme.Visible = false;
				}
				else
				{
					runda_5.Enabled = true;
					odgovorena_pitanja_5[pitanje_po_redu - 1] = 1;
					broj_odgovorenih_pitanja++;
					netacno.Text = "Dalje";
					novac[izazivac].Text = (int.Parse(novac[izazivac].Text) + 500).ToString();
					if (broj_odgovorenih_pitanja == 5)
					{
						game_stage = "poslednja odluka";
						runda_5.Enabled = false;
						vreme.Visible = false;
						tacno.Text = "DA";
						netacno.Text = "NE";
						netacno.Height = 200;
						netacno.Location = new System.Drawing.Point(netacno.Location.X, 534);
						opasna_polja = Math.Max(1, 5 - int.Parse(vreme.Text) / 20);
						if (opasna_polja == 1)
							pitanje.Text = "Da li želite da igrate ruski rulet sa jednim opasnim poljem u upravo zarađenih 2500 evra?";
						else if (opasna_polja == 2)
							pitanje.Text = "Da li želite da igrate ruski rulet sa 2 opasna polja u upravo zarađenih 2500 evra?";
						else
							pitanje.Text = "Da li želite da igrate ruski rulet sa " +
								 opasna_polja + " opasnih polja u upravo zarađenih 2500 evra?";
					}
					else
						netacno.PerformClick();
				}
			}
		}

		private void netacno_Click(object sender, EventArgs e)
		{
			if (runda < 5)
			{
				game_stage = "pogresan odgovor";
				odgovaranje.Enabled = false;
				cekanje_tick = 0;
				play_sound("Pogrešan odgovor");
				tacno.Text = "Pokaži odgovor";
				tacno.Visible = false;
				netacno.Visible = false;
				vreme.Visible = false;
			}
			else if (netacno.Text.Contains("Dalje"))
			{
				runda_5.Enabled = true;
				do
				{
					pitanje.Text = pitanja[pitanja_5[pitanje_po_redu % 10]];
					pitanje_po_redu++;
					if (pitanje_po_redu == 11)
						pitanje_po_redu = 1;
				}
				while (odgovorena_pitanja_5[pitanje_po_redu - 1] > 0);
			}
			else if (netacno.Text.Contains("NE"))
			{
				kraj_igre();
			}
			else if (netacno.Text.Contains("KRAJ"))
			{
				netacno.Visible = false;
				game_stage = "oduzimanje para";
				play_sound("Oduzimanje para");
			}
			else
			{
				pitanje.Text = "";
				ime[izazivac].BackColor = Color.Black;
				novac[izazivac].BackColor = Color.Black;
				play_sound(new string[] { "Propadanje", "Padanje 5" });
				game_stage = "poslednja odluka";//nema veze sto nije
			}
		}

		private void rulet_Tick(object sender, EventArgs e)
		{
			var random = new Random();
			int broj_opasnih_polja = (pitanje_po_redu + 1) / 2;

			//	if (RR_potez < 7)
			//	RR_potez = 7;//Za potrebe testiranja preskace se ruski rulet.
			//	broj_opasnih_polja = 5;

			RR_tick++;

			if (RR_potez >= 9)//prezivljavanje ili propadanje
			{
				if (RR_tick == 10)
					if (ime[izazivac].BackColor != Color.White)
					{
						rulet.Enabled = false;
						if (game_stage == "poslednja odluka")
							novac[izazivac].Text = (int.Parse(novac[izazivac].Text) - 2500).ToString();
						else
						{
							game_stage = "sledeca runda";
							ime[izazivac].Text = "";
							novac[izazivac].Text = "";
						}
						ime[izazivac].BackColor = Color.Black;
						novac[izazivac].BackColor = Color.Black;
						play_sound(new string[] { "Propadanje", "Padanje " + runda });
					}
					else if (player_is_free)
					{
						play_sound("Preživeo");
						rulet.Enabled = false;
						if (game_stage == "poslednja odluka")
						{
							novac[izazivac].Text = (int.Parse(novac[izazivac].Text) + 2500).ToString();
							kraj_igre();
							kraj_igre_1();
						}
						else
							game_stage = "preziveo";
					}
				return;
			}

			//ruski rulet
			if (ticks[RR_potez] == RR_tick)//Sto vise polja je proslo, ruski rulet se sporije vrti.
			{
				RR_potez1++;
				RR_tick = 0;
				opasno_polje++;
				if (opasno_polje > 11)
					opasno_polje -= 6;
				ime[opasno_polje].BackColor = Color.Red;
				novac[opasno_polje].BackColor = Color.Red;
				ime[opasno_polje - broj_opasnih_polja].BackColor = Color.White;
				novac[opasno_polje - broj_opasnih_polja].BackColor = Color.White;
				play_sound("Ruski rulet");
				if (RR_potez1 == RR_brzina)//promena brzine
				{
					RR_potez1 = 0;
					RR_potez++;
					RR_brzina = random.Next(3) + 3;//broj poteza ruskog ruleta pre sledece promene brzine
				}
			}
		}

		private void otkljucavanje_opasnih_polja(int broj_polja)
		{
			if ((broj_polja & 1) == 0)
			{
				game_stage = "sledece pitanje";
				return;
			}

			otkljucavanje_polja.Enabled = true;
			broj_polja = broj_polja / 2 + 1;
			for (int i = 0; i < 6; i++)
			{
				novac[i].BackColor = Color.White;
				ime[i].BackColor = Color.White;
			}
			switch (broj_polja)
			{
				case 1:
					novac[0].BackColor = Color.Red;
					ime[0].BackColor = Color.Red;
					pitanje.Text = "Otključavamo prvo opasno polje.";
					break;
				case 2:
					novac[0].BackColor = Color.Red;
					ime[0].BackColor = Color.Red;
					novac[3].BackColor = Color.Red;
					ime[3].BackColor = Color.Red;
					pitanje.Text = "Otključavamo drugo opasno polje.";
					break;
				case 3:
					novac[0].BackColor = Color.Red;
					ime[0].BackColor = Color.Red;
					novac[2].BackColor = Color.Red;
					ime[2].BackColor = Color.Red;
					novac[4].BackColor = Color.Red;
					ime[4].BackColor = Color.Red;
					pitanje.Text = "Otključavamo treće opasno polje.";
					break;
				case 4:
					novac[0].BackColor = Color.Red;
					ime[0].BackColor = Color.Red;
					for (int i = 2; i < 5; i++)
					{
						novac[i].BackColor = Color.Red;
						ime[i].BackColor = Color.Red;
					}
					pitanje.Text = "Otključavamo četvrto opasno polje.";
					break;
				case 5:
					for (int i = 0; i < 5; i++)
					{
						novac[i].BackColor = Color.Red;
						ime[i].BackColor = Color.Red;
					}
					pitanje.Text = "Otključavamo peto opasno polje.";
					break;
				case 6:
					for (int i = 0; i < 6; i++)
					{
						novac[i].BackColor = Color.Red;
						ime[i].BackColor = Color.Red;
					}
					pitanje.Text = "Otključavamo sva opasna polja.";
					break;
			}
		}

		private void otkljucavanje_polja_Tick(object sender, EventArgs e)
		{
			if (player_is_free)
			{
				play_sound("Otključavanje opasnog polja");
				otkljucavanje_polja.Enabled = false;
				if (runda < 5)
					game_stage = "sledece pitanje";
			}
		}

		private void runda5()
		{
			game_stage = "runda 5";
			for (int i = 0; i < 6; i++)
			{
				novac[i].BackColor = Color.White;
				ime[i].BackColor = Color.White;
				if (!ime[i].Text.Equals(""))
					izazivac = i;
			}
			pitanja_5 = biranje_pitanja(10);
			pitanje_po_redu = 1;
			otkljucavanje_opasnih_polja(11);
			vreme.Text = "120";
			pitanje.Text = pitanja[pitanja_5[0]];
			vreme.Font = new Font(vreme.Font.FontFamily, 60);
			vreme.Height = 95;
			netacno.Height = 97;
			netacno.Text = "Dalje";
			netacno.Location = new System.Drawing.Point(netacno.Location.X, 534 + 103);
		}

		private void runda_5_Tick(object sender, EventArgs e)
		{
			if (player_is_free)
				play_sound("Otkucavanje vremena");
			int Vreme = int.Parse(vreme.Text);
			if (Vreme == 0)
			{
				netacno.Text = "Netačno";
				netacno.PerformClick();
			}
			vreme.Text = (Vreme - 1).ToString();
			Vreme--;
			if (Vreme % 20 == 0)
			{
				ime[izazivac + Vreme / 20].BackColor = Color.Black;
				novac[izazivac + Vreme / 20].BackColor = Color.Black;
				play_sound("Propadanje");
			}
		}

		private void kraj_igre()
		{
			game_stage = "kraj igre";
			runda_5.Enabled = false;
			vreme.Visible = false;
			tacno.Visible = false;
			netacno.Visible = false;
			netacno.Height = 200;
			netacno.Location = new System.Drawing.Point(netacno.Location.X, 534);
			if (player_is_free)
				kraj_igre_1();
		}

		private void kraj_igre_1()
		{
			if (ime[izazivac].BackColor == Color.White)
				pitanje.Text = "Pobednik je " + ime[izazivac].Text + " sa osvojenih " + int.Parse(novac[izazivac].Text) + " evra.";
			else
				pitanje.Text = ime[izazivac].Text + " ispade iz igre i ode sa " + novac[izazivac].Text + " evra.";
			netacno.Text = "KRAJ";
			netacno.Visible = true;
		}

		private void play_video(string filename)
		{
			string url = System.IO.Directory.GetCurrentDirectory() + @"\" + filename + ".wmv";
			player.Visible = true;
			player.URL = url;
			player.Ctlcontrols.play();
		}

		private void play_sound(string filename)
		{
			string url = System.IO.Directory.GetCurrentDirectory() + @"\" + filename + ".wav";
			player.URL = url;
			player.Ctlcontrols.play();
		}

		private void play_sound(string[] filenames)
		{
			player.currentPlaylist.clear();
			foreach (string filename in filenames)
			{
				string url = System.IO.Directory.GetCurrentDirectory() + @"\" + filename + ".wav";
				player.currentPlaylist.appendItem(player.newMedia(url));
			}
			player.currentPlaylist.appendItem(player.newMedia(""));
			player.Ctlcontrols.play();
		}

		private void stop_sound()
		{
			player.Ctlcontrols.stop();
		}

		private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
		{
			if (e.newState == 3)
				player_is_free = false;
			if (e.newState == 8)
			{
				if (ime[0] == null)
				{
					Pocetak pocetak = new Pocetak(this);
					this.Hide();
					pocetak.Show();
				}
				else if (game_stage.Equals("pogresan odgovor"))
				{
					rucica.Value = 0;
					rucica.Visible = true;
					novac[izazvao].Text = (int.Parse(novac[izazvao].Text) + int.Parse(novac[izazivac].Text)).ToString();
					novac[izazivac].Text = "0";
				}
				else if (game_stage.Equals("sledeca runda"))
					game_stage = "sledeca runda 1";
				else if (game_stage.Equals("sledeca runda 1"))
					spica.Enabled = true;
				else if (game_stage.Equals("sledeca runda 2"))
				{
					pitanje_po_redu = 0;
					ime[izazivac].BackColor = Color.White;
					novac[izazivac].BackColor = Color.White;

					if (runda == 5)
						runda5();
					else
					{
						int m = najvise_novca();
						if (m >= 0)
						{
							izazivac = m;
							runda1234();
						}
						else
						{
							rucica.Value = 0;
							rucica.Visible = true;
							game_stage = "biranje izazivaca";
						}
					}
				}
				else if (game_stage.Equals("runda 5"))
				{
					for (int i = 0; i < 6; i++)
					{
						if (int.Parse(vreme.Text) > 100)
						{
							ime[i].BackColor = Color.White;
							novac[i].BackColor = Color.White;
						}
						vreme.Visible = true;
						tacno.Visible = true;
						netacno.Visible = true;
					}
					pitanje.Text = pitanja[pitanja_5[0]];
				}
				else if (game_stage.Equals("poslednja odluka"))
				{
					if (rulet.Enabled == false)
						kraj_igre();
				}
				else if (game_stage.Equals("kraj igre"))
				{
					if (rulet.Enabled == false)
						kraj_igre_1();
				}
				else if (game_stage.Equals("oduzimanje para"))
					Environment.Exit(0);
				else
				{
					if (game_stage.Equals("tacan odgovor"))
					{
						novac[izazivac].Text = (int.Parse(novac[izazivac].Text) + 100 * runda).ToString();
						if (runda == 0)
							novac[izazivac].Text = (int.Parse(novac[izazivac].Text) + 50).ToString();
						if (pitanje_po_redu < 10)
							otkljucavanje_opasnih_polja(pitanje_po_redu + 1);
						else
							runda1234();
					}
					else if (game_stage.Equals("preziveo"))
						otkljucavanje_opasnih_polja(pitanje_po_redu + 1);
					if (game_stage.Equals("sledece pitanje"))
					{
						biranje_izazivaca.Enabled = false;
						runda1234();
					}
				}
				player_is_free = true;
				player.Visible = false;
			}
		}

		private int najvise_novca()
		{
			int max = -1;
			int maxcount = 1;
			int indmax = -1;
			for (int i = 0; i < 6; i++)
			{
				if (novac[i].Text.Equals(""))
					continue;
				int Novac = int.Parse(novac[i].Text);
				if (Novac > max)
				{
					max = Novac;
					maxcount = 1;
					indmax = i;
				}
				else if (Novac == max)
					maxcount++;
			}
			if (maxcount > 1)
				return -1;
			return indmax;
		}

		private int najmanje_novca()
		{
			int min = int.MaxValue;
			int indmin = -1;
			for (int i = 0; i < 6; i++)
			{
				if (novac[i].Text.Equals(""))
					continue;
				int Novac = int.Parse(novac[i].Text);
				if (Novac < min)
				{
					min = Novac;
					indmin = i;
				}
			}
			return indmin;
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			var random = new Random();
			if (rucica.Value > 90)
			{
				rucica.Value = 0;
				rucica.Visible = false;
				if (game_stage.Equals("biranje izazivaca"))
				{
					biranje_izazivaca.Enabled = true;
					game_stage = "biranje izazivaca";
					izazivac = random.Next(6) + 1;
				}
				else if (game_stage.Equals("pogresan odgovor"))
				{
					rulet.Enabled = true;
					game_stage = "ruski rulet";
					ime[izazivac].BackColor = Color.White;
					novac[izazivac].BackColor = Color.White;
					opasno_polje = random.Next(6) + 6;
				}
				else if (game_stage.Equals("kraj runde"))
				{
					kraj_runde.Enabled = true;
					ime[izazivac].BackColor = Color.White;
					novac[izazivac].BackColor = Color.White;
					opasno_polje = random.Next(6) + 6;
				}
				else if (game_stage.Equals("poslednja odluka"))
				{
					rulet.Enabled = true;
					opasno_polje = random.Next(6) + 6;
				}
				initialize_timer();
			}
		}

		private void izazovi1_Click(object sender, EventArgs e)
		{
			izazivanje(0);
		}

		private void izazovi2_Click(object sender, EventArgs e)
		{
			izazivanje(1);
		}

		private void izazovi3_Click(object sender, EventArgs e)
		{
			izazivanje(2);
		}

		private void izazovi4_Click(object sender, EventArgs e)
		{
			izazivanje(3);
		}

		private void izazovi5_Click(object sender, EventArgs e)
		{
			izazivanje(4);
		}

		private void izazovi6_Click(object sender, EventArgs e)
		{
			izazivanje(5);
		}
	}
}
