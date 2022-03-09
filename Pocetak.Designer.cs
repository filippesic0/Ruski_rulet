namespace Ruski_rulet
{
	partial class Pocetak
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ok = new System.Windows.Forms.Button();
			this.ime6 = new System.Windows.Forms.TextBox();
			this.ime5 = new System.Windows.Forms.TextBox();
			this.ime4 = new System.Windows.Forms.TextBox();
			this.ime3 = new System.Windows.Forms.TextBox();
			this.ime2 = new System.Windows.Forms.TextBox();
			this.ime1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ok
			// 
			this.ok.Location = new System.Drawing.Point(12, 163);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(172, 38);
			this.ok.TabIndex = 13;
			this.ok.Text = "OK";
			this.ok.UseVisualStyleBackColor = true;
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// ime6
			// 
			this.ime6.Location = new System.Drawing.Point(12, 138);
			this.ime6.Name = "ime6";
			this.ime6.Size = new System.Drawing.Size(173, 20);
			this.ime6.TabIndex = 12;
			this.ime6.Text = "6";
			// 
			// ime5
			// 
			this.ime5.Location = new System.Drawing.Point(12, 113);
			this.ime5.Name = "ime5";
			this.ime5.Size = new System.Drawing.Size(173, 20);
			this.ime5.TabIndex = 11;
			this.ime5.Text = "5";
			// 
			// ime4
			// 
			this.ime4.Location = new System.Drawing.Point(12, 88);
			this.ime4.Name = "ime4";
			this.ime4.Size = new System.Drawing.Size(173, 20);
			this.ime4.TabIndex = 10;
			this.ime4.Text = "4";
			// 
			// ime3
			// 
			this.ime3.Location = new System.Drawing.Point(12, 63);
			this.ime3.Name = "ime3";
			this.ime3.Size = new System.Drawing.Size(173, 20);
			this.ime3.TabIndex = 9;
			this.ime3.Text = "3";
			// 
			// ime2
			// 
			this.ime2.Location = new System.Drawing.Point(12, 38);
			this.ime2.Name = "ime2";
			this.ime2.Size = new System.Drawing.Size(173, 20);
			this.ime2.TabIndex = 8;
			this.ime2.Text = "2";
			// 
			// ime1
			// 
			this.ime1.Location = new System.Drawing.Point(12, 12);
			this.ime1.Name = "ime1";
			this.ime1.Size = new System.Drawing.Size(173, 20);
			this.ime1.TabIndex = 7;
			this.ime1.Text = "1";
			// 
			// Pocetak
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(196, 213);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.ime6);
			this.Controls.Add(this.ime5);
			this.Controls.Add(this.ime4);
			this.Controls.Add(this.ime3);
			this.Controls.Add(this.ime2);
			this.Controls.Add(this.ime1);
			this.MaximumSize = new System.Drawing.Size(212, 252);
			this.MinimumSize = new System.Drawing.Size(212, 252);
			this.Name = "Pocetak";
			this.Text = "Pocetak";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.TextBox ime6;
		private System.Windows.Forms.TextBox ime5;
		private System.Windows.Forms.TextBox ime4;
		private System.Windows.Forms.TextBox ime3;
		private System.Windows.Forms.TextBox ime2;
		private System.Windows.Forms.TextBox ime1;
	}
}