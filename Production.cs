using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing.Drawing2D;
using System.Numerics;
using static ArchaeaMod.Production;
using Matrix = Microsoft.Xna.Framework.Matrix;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace ArchaeaMod
{
	public class Production : Game
	{
		internal static Production Instance;
		internal GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
      private Matrix matrix = Matrix.CreateTranslation(0, 0, 0);

		public Production()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
         RegisterHooks();
         InitializeEvent?.Invoke(new InitializeArgs());
			base.Initialize();
		}

		protected override bool BeginDraw()
		{
			_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, transformMatrix: matrix);
         return base.BeginDraw();
		}

		protected override void EndDraw()
		{
         _spriteBatch.End();
			base.EndDraw();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
         LoadResourcesEvent?.Invoke();
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
         ExitEvent?.Invoke(new ExitArgs());
			base.OnExiting(sender, args);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
         ViewportEvent?.Invoke(new ViewportArgs() { viewport = _graphics.GraphicsDevice.Viewport, matrix = matrix });
         UpdateEvent?.Invoke(new UpdateArgs());

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
         if (!(bool)MainMenuDrawEvent?.Invoke(new DrawingArgs() { spriteBatch = _spriteBatch }))
         {
            if ((bool)PreDrawEvent?.Invoke(new PreDrawArgs() { spriteBatch = _spriteBatch}))
            { 
               DrawEvent?.Invoke(new DrawingArgs() { spriteBatch = _spriteBatch });
            }
         }

			base.Draw(gameTime);
		}

      public virtual void RegisterHooks() { }
		public delegate void Event<T>(T e);
      public delegate void Event();
      public delegate bool _PrewDraw<T>(T e);
      public delegate bool _Resize<T>(T e);
      public delegate bool _Exit<T>(T e);
      public delegate bool _MainMenuDraw<T>(T e);
      public static event _Exit<ExitArgs> ExitEvent;
      public static event _Resize<ResizeArgs> ResizeEvent;
      public static event Event<InitializeArgs> InitializeEvent;
      public static event Event<InputArgs> InputEvent;
      public static event Event LoadResourcesEvent;
      public static event Event MainMenuEvent;
      public static event _MainMenuDraw<DrawingArgs> MainMenuDrawEvent;
      public static event _PrewDraw<PreDrawArgs> PreDrawEvent;
      public static event Event<DrawingArgs> DrawEvent;
      public static event Event<UpdateArgs> UpdateEvent;
      public static event Event<ViewportArgs> ViewportEvent;
      public interface IArgs
      {
      }
      public class ResizeArgs : IArgs
      {
      }
      public class DrawingArgs : IArgs
      {
         public SpriteBatch spriteBatch;
      }
      public class PreDrawArgs : IArgs
      {
         public SpriteBatch spriteBatch;
      }
      public class UpdateArgs : IArgs
      {
      }
      public class ViewportArgs : IArgs
      {
         public Viewport viewport;
			public Matrix matrix;
      }
      public class InitializeArgs : IArgs
      {
      }
      public class InputArgs : IArgs
      {
         public Rectangle windowBounds;
      }
      public class ExitArgs : IArgs
      {
      }
	}
	public partial class Main : Production
	{
      public override void RegisterHooks()
      {
         Production.UpdateEvent += Update;
         Production.ResizeEvent += Resize;
         Production.InputEvent += Input;
         Production.DrawEvent += Draw;
         Production.InitializeEvent += Initialize;
         Production.LoadResourcesEvent += LoadResources;
         Production.MainMenuEvent += MainMenu;
         Production.PreDrawEvent += PreDraw;
         Production.ViewportEvent += ViewportHook;
         Production.ExitEvent += ExitApp;
      }
	}
}
