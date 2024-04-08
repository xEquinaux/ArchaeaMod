using ArchaeaMod;
using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using System.Windows.Forms;

internal partial class Overlay : Form
{
	[STAThread]
	static void Main(string[] args)
	{
		Arg.LaunchProperties(args, StartType.Debug);
	}

	static Overlay Instance;
	Production instance => ArchaeaMod.Production.Instance;

	public Overlay()
	{
		this.FormBorderStyle = FormBorderStyle.None;
		this.ShowInTaskbar = false;
		this.Load		+= new EventHandler(Overlay_Load);
		this.Paint		+= new PaintEventHandler(Overlay_Paint);
		this.Move		+= new EventHandler(Overlay_Move);
		this.Resize		+= new EventHandler(Overlay_Resize);
	}

	private static void Game_Exiting(object sender, EventArgs e)
	{
		Instance.Close();
	}
	 
	private void Overlay_Resize(object sender, EventArgs e)
	{
		instance._graphics.PreferredBackBufferWidth = this.Width;
		instance._graphics.PreferredBackBufferHeight = this.Height;
		instance._graphics.ApplyChanges();
	}

	private void Overlay_Move(object sender, EventArgs e)
	{
		instance.Window.Position = new Microsoft.Xna.Framework.Point(this.Location.X, this.Location.Y);
	}

	private void Overlay_Load(object sender, EventArgs e)
	{
		this.TopMost = true;
		this.Location = new System.Drawing.Point(0, 0);
		this.Size = new Size(800, 600);
		this.TransparencyKey = this.BackColor = System.Drawing.Color.Red;
	}

	private void Overlay_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.DrawString("Hello, Overlay!", new Font("Arial", 16), Brushes.Black, new PointF(10, 10));
	}

	private void InitializeComponent()
	{
	}
}