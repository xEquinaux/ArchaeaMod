using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchaeaMod
{
   public partial class Main
	{
		private bool ExitApp(ExitArgs e)
		{
			throw new NotImplementedException();
		}

		public static bool		IsZoomed;
		public static int			ScreenWidth => Screen.Width;
		public static int			ScreenHeight => Screen.Height;
		public static float		MapX, MapY;
		public static float		ScrollSpeed = 0.3f;
		public static Vector2	ScreenPos => new Vector2(Screen.X, Screen.Y);
		public static Rectangle Screen;
		public static Player		myPlayer;

		private void ViewportHook(ViewportArgs e)
		{
         var offset = new Vector3(Main.ScreenWidth / 2, Main.ScreenHeight / 2, 0);
			var camera = new Vector3(-Main.myPlayer.position.X - Player.plrWidth / 2, -Main.myPlayer.position.Y - Player.plrHeight / 2, 0);
         e.matrix = Matrix.CreateTranslation(camera + offset + (Main.IsZoomed ? new Vector3(Main.ScreenWidth * 0.5f - Main.MapX * ScrollSpeed, Main.ScreenHeight * 0.5f - Main.MapY * ScrollSpeed, 0) : Vector3.Zero)) * Matrix.CreateScale(Main.IsZoomed ? 0.5f : 1f);
		}

		private bool PreDraw(PreDrawArgs e)
		{
			throw new NotImplementedException();
		}

		private void MainMenu()
		{
			throw new NotImplementedException();
		}

		private void LoadResources()
		{
			throw new NotImplementedException();
		}

		private void Initialize(InitializeArgs e)
		{
			throw new NotImplementedException();
		}

		private void Draw(DrawingArgs e)
		{
			throw new NotImplementedException();
		}

		private void Input(InputArgs e)
		{
			throw new NotImplementedException();
		}

		private bool Resize(ResizeArgs e)
		{
			throw new NotImplementedException();
		}

		private void Update(UpdateArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
