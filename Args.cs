using System;
using System.Collections.Generic;
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
		/// Unused / same as None start type.
		/// </summary>
		Custom = 2
	}
	public static class Arg
	{
		static string[] Args;
		static StartType Type;
		public static void LaunchProperties(string[] args, StartType type)
		{
			Args = args;
			Type = type;
			switch (type)
			{
				case StartType.Custom:
				case StartType.None:
				default:
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Overlay());
					break;
				case StartType.Debug:
					using (Production.Instance = new Production())
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
