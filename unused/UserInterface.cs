using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchaeaMod.unused
{
	public enum ControlType : byte
	{
		None = 0,
		Button = 1,
		Textbox = 2,
		Checkbox = 3,
		Listbox = 4,
		Menubox = 5,
		Thumbnail = 6,
		Dialog = 7
	}
	public interface IUserInterface
	{
		public abstract bool LeftClick();
		public abstract bool RightClick();
		public abstract bool MouseHover();
		public abstract bool AcceptKey(Keys k);
		public abstract bool Input(string text);
	}
	public abstract class Control : IUserInterface
	{
		public bool active;
		public int Width => bounds.Width;
		public int Height => bounds.Height;
		public int X => bounds.X;
		public int Y => bounds.Y;
		public int Right => bounds.X + bounds.Width;
		public int Bottom => bounds.Y + bounds.Height;
		public ControlType type;
		public Rectangle bounds;
		public Microsoft.Xna.Framework.Color color;
		public abstract bool AcceptKey(Keys k);
		public abstract bool Input(string text);
		public abstract bool LeftClick();
		public abstract bool MouseHover();
		public abstract bool RightClick();
		public void UpdateControl()
		{
			switch (type)
			{
				case ControlType.None:
				default:
					break;
				case ControlType.Button:
				case ControlType.Checkbox:
				case ControlType.Listbox:
				case ControlType.Menubox:
				case ControlType.Textbox:
				case ControlType.Thumbnail:
				case ControlType.Dialog:
					throw new NotImplementedException();
			}
		}
		public void UpdateDraw(SpriteBatch sb)
		{
			switch (type)
			{
				case ControlType.None:
				default:
					break;
				case ControlType.Button:
				case ControlType.Checkbox:
				case ControlType.Listbox:
				case ControlType.Menubox:
				case ControlType.Textbox:
				case ControlType.Thumbnail:
				case ControlType.Dialog:
					throw new NotImplementedException();
			}
		}
	}
}
