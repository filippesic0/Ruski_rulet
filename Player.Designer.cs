namespace Ruski_rulet
{
	partial class Player
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
			this.player1 = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this.player1)).BeginInit();
			this.SuspendLayout();
			// 
			// player1
			// 
			this.player1.Enabled = true;
			this.player1.Location = new System.Drawing.Point(12, 12);
			this.player1.Name = "player1";
			this.player1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player1.OcxState")));
			this.player1.Size = new System.Drawing.Size(436, 426);
			this.player1.TabIndex = 0;
			this.player1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.player1_PlayStateChange);
			// 
			// Player
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(460, 450);
			this.Controls.Add(this.player1);
			this.Name = "Player";
			this.Text = "Player";
			((System.ComponentModel.ISupportInitialize)(this.player1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer player1;
	}
}