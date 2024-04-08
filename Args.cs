using Microsoft.Xna.Framework;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchaeaMod
{
	public enum StartType : int
	{
		/// <summary>
		/// Start form control only.
		/// </summary>
		None = -1,
		/// <summary>
		/// Start the form and surface controls.
		/// </summary>
		Normal = 0,
		/// <summary>
		/// Start only the surface control.
		/// </summary>
		Debug = 1,
		/// <summary>
		/// Used for custom start settings, otherwise same as Debug start type.
		/// </summary>
		Custom = 2,
		/// <summary>
		/// Don't use -- this is a case container for starting Debug and Custom types.
		/// </summary>
		RESERVED = 3
	}
	public struct LaunchConfig
	{
		public bool IsBorderless;
		public int Width;
		public int Height;
		public string Title;
		public Point StartPosition;
		/// <summary>
		/// Arguments: -width, -height, -startx, -starty, -borderless [true/false]
		/// </summary>
		/// <param name="args">Program args input.</param>
		/// <returns></returns>
		public static LaunchConfig ParseArgs(string[] args)
		{
			LaunchConfig config = new LaunchConfig();
			config.StartPosition = Point.Zero;
			if (args.Length > 1)
			{ 
				for (int i = 1; i < args.Length; i++)
				{ 
					switch (args[i - 1])
					{
						case "-width":
							int.TryParse(args[i], out config.Width);
							break;
						case "-height":
							int.TryParse(args[i], out config.Height);
							break;
						case "-startx":
							int.TryParse(args[i], out config.StartPosition.X);
							break;
						case "-starty":
							int.TryParse(args[i], out config.StartPosition.Y);
							break;
						case "-borderless":
							bool.TryParse(args[i], out config.IsBorderless);
							break;
					}
				}
			}
			return config;
		}	
		public static bool operator !=(LaunchConfig a, LaunchConfig b)
		{
			return a.Height != b.Height || a.Width != b.Width || a.StartPosition != b.StartPosition || a.Title != b.Title;
		}
		public static bool operator ==(LaunchConfig a, LaunchConfig b)
		{
			return a.Height == b.Height && a.Width == b.Width && a.StartPosition == b.StartPosition && a.Title == b.Title;
		}
		public override string ToString()
		{
			return $"\"Title: {Title}, Width: {Width}, Height: {Height}\"";
		}
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
	public static class Arg
	{
		static string[] Args;
		static StartType Type;
		public static void LaunchProperties(in string[] args, StartType type, LaunchConfig config = default)
		{
			Args = args;
			Type = type;
			if (Args.Length > 1 && config == default)
			{
				config = LaunchConfig.ParseArgs(Args);
			}
			switch (type)
			{
				case StartType.Debug:
					Production.Instance = new Production();
					Production.Instance.Window.IsBorderless = false;
					Production.Instance.Window.Title = "Archaea Mod";
					Production.Instance.Window.Position = Point.Zero;
					Production.Instance._graphics.PreferredBackBufferWidth = 640;
					Production.Instance._graphics.PreferredBackBufferHeight = 480;
					Production.Instance._graphics.ApplyChanges();
					goto case StartType.RESERVED;
				case StartType.Custom:
					Production.Instance = new Production();
					Production.Instance.Window.IsBorderless = config.IsBorderless;
					Production.Instance.Window.Title = config.Title;
					Production.Instance.Window.Position = config.StartPosition;
					Production.Instance._graphics.PreferredBackBufferWidth = config.Width;
					Production.Instance._graphics.PreferredBackBufferHeight = config.Height;
					Production.Instance._graphics.ApplyChanges();
					goto case StartType.RESERVED;
				case StartType.None:
				default:
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Overlay());
					break;
				case StartType.RESERVED:
					using (Production.Instance)
					{ 
						Production.Instance.Window.IsBorderless = true;
						Production.Instance.Window.AllowAltF4 = true;
						Production.Instance.Window.AllowUserResizing = false;
						Production.Instance.Run();
					}
					break;
				case StartType.Normal:
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Overlay());
					using (Production.Instance = new Production())
					{ 
						Production.Instance.Exiting += new EventHandler<EventArgs>(Overlay.OnExiting);
						Production.Instance.Window.IsBorderless = true;
						Production.Instance.Window.AllowAltF4 = true;
						Production.Instance.Window.AllowUserResizing = false;
						Production.Instance.Run();
					}
					break;
			}
		}
	}
}
