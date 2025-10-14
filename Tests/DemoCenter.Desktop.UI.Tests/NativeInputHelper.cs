using System;
using System.Runtime.InteropServices;

using Avalonia;
using Avalonia.Input;

using DisplayPtr = System.IntPtr;

namespace Eremex.AvaloniaUI.Controls.Tests.Editors;

public class TestInputHelper
{
	private readonly INativeInputHelper inputHelper;

	public TestInputHelper()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			inputHelper = new WindowsNativeInputHelper();
		}
		else
		{
			inputHelper = new LinuxNativeInputHelper();
		}
	}

	public void MouseMove(Point position)
	{
		inputHelper.MouseMove(position);
	}

	public void MouseDown(MouseButton mouseButton/*, Point position*/)
	{
		inputHelper.MouseDown(mouseButton, default);
	}

	public void MouseUp(MouseButton mouseButton/*, Point position*/)
	{
		inputHelper.MouseUp(mouseButton, default);
	}

	public void MouseClick(MouseButton mouseButton/*, Point position*/)
	{
		MouseDown(mouseButton);
		MouseUp(mouseButton);
	}

	public void KeyDown(Key key)
	{
		inputHelper.KeyDown(key);
	}

	public void KeyUp(Key key)
	{
		inputHelper.KeyUp(key);
	}

	public void KeyPress(Key key)
	{
		KeyDown(key);
		KeyUp(key);
	}

	//public void Delay(uint ms)
	//{
	//	Thread.Sleep((int)ms);
	//}

	//protected void ApplyAutoDelay()
	//{
	//	if (AutoDelay > 0)
	//	{
	//		Thread.Sleep((int)AutoDelay);
	//	}
	//}
}

public interface INativeInputHelper
{
	void MouseMove(Point position);
	void MouseUp(MouseButton mouseButton, Point position);
	void MouseDown(MouseButton mouseButton, Point position);
	void KeyDown(Key key);
	void KeyUp(Key key);
}

internal class WindowsNativeInputHelper : INativeInputHelper
{
	public void KeyDown(Key key)
	{
		InvokeKeyboardEvent(key, false);
	}

	public void KeyUp(Key key)
	{
		InvokeKeyboardEvent(key, true);
	}

	public void MouseDown(MouseButton mouseButton, Point position)
	{
		InvokeMouseEvent(mouseButton, false);
	}

	public void MouseMove(Point position)
	{
		SetCursorPos((uint)position.X, (uint)position.Y);
	}

	public void MouseUp(MouseButton mouseButton, Point position)
	{
		InvokeMouseEvent(mouseButton, true);
	}

	private void InvokeKeyboardEvent(Key key, bool up)
	{
		var input = new INPUT(new KEYBDINPUT(ConvertKey(key), up ? KEYBDINPUT.KEYEVENTF_KEYUP : 0));
		SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
	}

	private void InvokeMouseEvent(MouseButton mouseButton, bool up)
	{
		uint mouseData = 0;
		uint dwFlags = 0;
		if (up)
		{
			switch (mouseButton)
			{
				case MouseButton.Left:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_LEFTUP;
					break;
				case MouseButton.Right:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_RIGHTUP;
					break;
				case MouseButton.Middle:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_MIDDLEUP;
					break;
				case MouseButton.XButton1:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_XUP;
					mouseData = MOUSEINPUT.XBUTTON1;
					break;
				case MouseButton.XButton2:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_XUP;
					mouseData = MOUSEINPUT.XBUTTON2;
					break;
				default:
					break;
			}
		}
		else
		{
			switch (mouseButton)
			{
				case MouseButton.Left:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_LEFTDOWN;
					break;
				case MouseButton.Right:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_RIGHTDOWN;
					break;
				case MouseButton.Middle:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_MIDDLEDOWN;
					break;
				case MouseButton.XButton1:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_XDOWN;
					mouseData = MOUSEINPUT.XBUTTON1;
					break;
				case MouseButton.XButton2:
					dwFlags = MOUSEINPUT.MOUSEEVENTF_XDOWN;
					mouseData = MOUSEINPUT.XBUTTON2;
					break;
				default:
					break;
			}
		}
		var input = new INPUT(new MOUSEINPUT(mouseData, dwFlags));
		var result = SendInput(1, new[] { input }, Marshal.SizeOf<INPUT>());
		if (result == 0)
		{
			throw new ExternalException($"SendInput return error: {Marshal.GetLastWin32Error()}");
		}

	}

	//public override void KeyDown(Key key)
	//{
	//	ApplyAutoDelay();
	//	var metadata = key.GetKeycode();
	//	var keycode = (byte)metadata.Keycode;
	//	var scancode = (byte)metadata.ScanCode;
	//	keybd_event(keycode, scancode, 0, 0);
	//}

	//public override void KeyDown(char key)
	//{
	//	ApplyAutoDelay();
	//	var keycode = (byte)VkKeyScan(key);
	//	keybd_event(keycode, 0, 0, 0);
	//}

	//public override void KeyPress(Key key)
	//{
	//	ApplyAutoDelay();
	//	var metadata = key.GetKeycode();
	//	var keycode = (byte)metadata.Keycode;
	//	var scancode = (byte)metadata.ScanCode;
	//	keybd_event(keycode, scancode, 0, 0);
	//	keybd_event(keycode, scancode, 2, 0);
	//}

	//public override void KeyPress(char key)
	//{
	//	ApplyAutoDelay();
	//	var keycode = (byte)VkKeyScan(key);
	//	keybd_event(keycode, 0, 0, 0);
	//	keybd_event(keycode, 0, 2, 0);
	//}

	//public override void KeyUp(Key key)
	//{
	//	ApplyAutoDelay();
	//	var metadata = key.GetKeycode();
	//	var keycode = (byte)metadata.Keycode;
	//	var scancode = (byte)metadata.ScanCode;
	//	keybd_event(keycode, scancode, 2, 0);
	//}

	//public override void KeyUp(char key)
	//{
	//	ApplyAutoDelay();
	//	var keycode = (byte)VkKeyScan(key);
	//	keybd_event(keycode, 0, 2, 0);
	//}

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetCursorPos(uint x, uint y);

	//[StructLayout(LayoutKind.Sequential)]
	//public struct PointInter
	//{
	//	public int X;
	//	public int Y;
	//	public static explicit operator Point(PointInter point) => new Point(point.X, point.Y);
	//}

	//[DllImport("user32.dll")]
	//public static extern bool GetCursorPos(out PointInter lpPoint);

	[DllImport("user32.dll", SetLastError = true)]
	static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

	[DllImport("user32.dll")]
	private static extern short VkKeyScan(char ch);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint SendInput(uint cInputs, [MarshalAs(UnmanagedType.LPArray)] INPUT[] pInputs, int cbSize);

	[StructLayout(LayoutKind.Sequential)]
	public struct INPUT
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct INPUTUnion
		{
			[FieldOffset(0)]
			public MOUSEINPUT mi;
			[FieldOffset(0)]
			public KEYBDINPUT ki;
			//[FieldOffset(0)]
			//public MOUSEINPUT mi;
		}

		private const int INPUT_MOUSE = 0;
		private const int INPUT_KEYBOARD = 1;
		private const int INPUT_HARDWARE = 2;

		private readonly uint type;
		private readonly INPUTUnion union;

		public INPUT(KEYBDINPUT ki)
		{
			type = INPUT_KEYBOARD;
			union = new INPUTUnion() { ki = ki };
		}

		public INPUT(MOUSEINPUT mi)
		{
			type = INPUT_MOUSE;
			union = new INPUTUnion() { mi = mi };
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct KEYBDINPUT
	{
		public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
		public const uint KEYEVENTF_KEYUP = 0x0002;
		public const uint KEYEVENTF_SCANCODE = 0x0008;
		public const uint KEYEVENTF_UNICODE = 0x0004;

		private readonly VirtualKey wVk;
		private readonly ushort wScan;
		private readonly uint dwFlags;
		private readonly uint time;
		private readonly IntPtr dwExtraInfo;

		public KEYBDINPUT(VirtualKey wVk, uint dwFlags)
		{
			this.wVk = wVk;
			this.dwFlags = dwFlags;
			wScan = 0;
			time = 0;
			dwExtraInfo = IntPtr.Zero;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSEINPUT
	{
		public const uint MOUSEEVENTF_MOVE = 0x0001;
		public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
		public const uint MOUSEEVENTF_LEFTUP = 0x0004;
		public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
		public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
		public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
		public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
		public const uint MOUSEEVENTF_XDOWN = 0x0080;
		public const uint MOUSEEVENTF_XUP = 0x0100;
		public const uint MOUSEEVENTF_WHEEL = 0x0800;
		public const uint MOUSEEVENTF_HWHEEL = 0x1000;
		public const uint MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000;
		public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
		public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
		public const uint XBUTTON1 = 0x0001;
		public const uint XBUTTON2 = 0x0002;

		private readonly int dx;
		private readonly int dy;
		private readonly uint mouseData;
		private readonly uint dwFlags;
		private readonly uint time;
		private readonly IntPtr dwExtraInfo;

		public MOUSEINPUT(uint mouseData, uint dwFlags) : this()
		{
			this.mouseData = mouseData;
			this.dwFlags = dwFlags;
		}
	}

	private static VirtualKey ConvertKey(Key key)
	{
		return key switch
		{
			Key.Cancel => VirtualKey.CANCEL,
			Key.Tab => VirtualKey.TAB,
			Key.Clear => VirtualKey.CLEAR,
			Key.Return => VirtualKey.RETURN,
			Key.Pause => VirtualKey.PAUSE,
			Key.Escape => VirtualKey.ESCAPE,
			Key.Space => VirtualKey.SPACE,
			Key.PageUp => VirtualKey.PRIOR,
			Key.PageDown => VirtualKey.NEXT,
			Key.End => VirtualKey.END,
			Key.Home => VirtualKey.HOME,

			Key.Left => VirtualKey.LEFT,
			Key.Up => VirtualKey.UP,
			Key.Right => VirtualKey.RIGHT,
			Key.Down => VirtualKey.DOWN,
			Key.Select => VirtualKey.SELECT,
			Key.Print => VirtualKey.PRINT,
			Key.Execute => VirtualKey.EXECUTE,
			Key.Insert => VirtualKey.INSERT,
			Key.Delete => VirtualKey.DELETE,

			Key.D0 => VirtualKey.D0,
			Key.D1 => VirtualKey.D1,
			Key.D2 => VirtualKey.D2,
			Key.D3 => VirtualKey.D3,
			Key.D4 => VirtualKey.D4,
			Key.D5 => VirtualKey.D5,
			Key.D6 => VirtualKey.D6,
			Key.D7 => VirtualKey.D7,
			Key.D8 => VirtualKey.D8,
			Key.D9 => VirtualKey.D9,

			Key.A => VirtualKey.A,
			Key.B => VirtualKey.B,
			Key.C => VirtualKey.C,
			Key.D => VirtualKey.D,
			Key.E => VirtualKey.E,
			Key.F => VirtualKey.F,
			Key.G => VirtualKey.G,
			Key.H => VirtualKey.H,
			Key.I => VirtualKey.I,
			Key.J => VirtualKey.J,
			Key.K => VirtualKey.K,
			Key.L => VirtualKey.L,
			Key.M => VirtualKey.M,
			Key.N => VirtualKey.N,
			Key.O => VirtualKey.O,
			Key.P => VirtualKey.P,
			Key.Q => VirtualKey.Q,
			Key.R => VirtualKey.R,
			Key.S => VirtualKey.S,
			Key.T => VirtualKey.T,
			Key.U => VirtualKey.U,
			Key.V => VirtualKey.V,
			Key.W => VirtualKey.W,
			Key.X => VirtualKey.X,
			Key.Y => VirtualKey.Y,
			Key.Z => VirtualKey.Z,

			Key.F1 => VirtualKey.F1,
			Key.F2 => VirtualKey.F2,
			Key.F3 => VirtualKey.F3,
			Key.F4 => VirtualKey.F4,
			Key.F5 => VirtualKey.F5,
			Key.F6 => VirtualKey.F6,
			Key.F7 => VirtualKey.F7,
			Key.F8 => VirtualKey.F8,
			Key.F9 => VirtualKey.F9,
			Key.F10 => VirtualKey.F10,
			Key.F11 => VirtualKey.F11,
			Key.F12 => VirtualKey.F12,
			Key.F13 => VirtualKey.F13,
			Key.F14 => VirtualKey.F14,
			Key.F15 => VirtualKey.F15,
			Key.F16 => VirtualKey.F16,
			Key.F17 => VirtualKey.F17,
			Key.F18 => VirtualKey.F18,
			Key.F19 => VirtualKey.F19,
			Key.F20 => VirtualKey.F20,
			Key.F21 => VirtualKey.F21,
			Key.F22 => VirtualKey.F22,
			Key.F23 => VirtualKey.F23,
			Key.F24 => VirtualKey.F24,

			Key.LeftShift => VirtualKey.LSHIFT,
			Key.RightShift => VirtualKey.RSHIFT,
			Key.LeftCtrl => VirtualKey.LCONTROL,
			Key.RightCtrl => VirtualKey.RCONTROL,
			Key.LeftAlt => VirtualKey.LMENU,
			Key.RightAlt => VirtualKey.RMENU,

			_ => throw new ArgumentException($"Cannot convert button: <{key}>", nameof(key))
		};
	}

	public enum VirtualKey : ushort
	{
		LBUTTON = 0x01,
		RBUTTON = 0x02,
		CANCEL = 0x03,
		MBUTTON = 0x04,   /* NOT contiguous with L & RBUTTON */
		XBUTTON1 = 0x05,    /* NOT contiguous with L & RBUTTON */
		XBUTTON2 = 0x06,    /* NOT contiguous with L & RBUTTON */

		BACK = 0x08,
		TAB = 0x09,

		/*
		 *= 0x0A -= 0x0B : reserved
		 */

		CLEAR = 0x0C,
		RETURN = 0x0D,

		/*
		 *= 0x0E -= 0x0F : unassigned
		 */

		SHIFT = 0x10,
		CONTROL = 0x11,
		MENU = 0x12,
		PAUSE = 0x13,
		CAPITAL = 0x14,

		KANA = 0x15,
		HANGEUL = 0x15,  /* old name - should be here for compatibility */
		HANGUL = 0x15,
		IME_ON = 0x16,
		JUNJA = 0x17,
		FINAL = 0x18,
		HANJA = 0x19,
		KANJI = 0x19,
		IME_OFF = 0x1A,

		ESCAPE = 0x1B,

		CONVERT = 0x1C,
		NONCONVERT = 0x1D,
		ACCEPT = 0x1E,
		MODECHANGE = 0x1F,

		SPACE = 0x20,
		PRIOR = 0x21,
		NEXT = 0x22,
		END = 0x23,
		HOME = 0x24,
		LEFT = 0x25,
		UP = 0x26,
		RIGHT = 0x27,
		DOWN = 0x28,
		SELECT = 0x29,
		PRINT = 0x2A,
		EXECUTE = 0x2B,
		SNAPSHOT = 0x2C,
		INSERT = 0x2D,
		DELETE = 0x2E,
		HELP = 0x2F,

		D0 = 0x30,
		D1 = 0x31,
		D2 = 0x32,
		D3 = 0x33,
		D4 = 0x34,
		D5 = 0x35,
		D6 = 0x36,
		D7 = 0x37,
		D8 = 0x38,
		D9 = 0x39,

		A = 0x41,
		B = 0x42,
		C = 0x43,
		D = 0x44,
		E = 0x45,
		F = 0x46,
		G = 0x47,
		H = 0x48,
		I = 0x49,
		J = 0x4a,
		K = 0x4b,
		L = 0x4c,
		M = 0x4d,
		N = 0x4e,
		O = 0x4f,
		P = 0x50,
		Q = 0x51,
		R = 0x52,
		S = 0x53,
		T = 0x54,
		U = 0x55,
		V = 0x56,
		W = 0x57,
		X = 0x58,
		Y = 0x59,
		Z = 0x5a,

		//LWIN = 0x5B,
		//RWIN = 0x5C,
		//APPS = 0x5D,

		NUMPAD0 = 0x60,
		NUMPAD1 = 0x61,
		NUMPAD2 = 0x62,
		NUMPAD3 = 0x63,
		NUMPAD4 = 0x64,
		NUMPAD5 = 0x65,
		NUMPAD6 = 0x66,
		NUMPAD7 = 0x67,
		NUMPAD8 = 0x68,
		NUMPAD9 = 0x69,
		MULTIPLY = 0x6A,
		ADD = 0x6B,
		SEPARATOR = 0x6C,
		SUBTRACT = 0x6D,
		DECIMAL = 0x6E,
		DIVIDE = 0x6F,
		F1 = 0x70,
		F2 = 0x71,
		F3 = 0x72,
		F4 = 0x73,
		F5 = 0x74,
		F6 = 0x75,
		F7 = 0x76,
		F8 = 0x77,
		F9 = 0x78,
		F10 = 0x79,
		F11 = 0x7A,
		F12 = 0x7B,
		F13 = 0x7C,
		F14 = 0x7D,
		F15 = 0x7E,
		F16 = 0x7F,
		F17 = 0x80,
		F18 = 0x81,
		F19 = 0x82,
		F20 = 0x83,
		F21 = 0x84,
		F22 = 0x85,
		F23 = 0x86,
		F24 = 0x87,

		//#define VK_NUMLOCK       = 0x90
		//#define VK_SCROLL        = 0x91

		//		/*
		//		 * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
		//		 * Used only as parameters to GetAsyncKeyState() and GetKeyState().
		//		 * No other API or message will distinguish left and right keys in this way.
		//		 */
		LSHIFT = 0xA0,
		RSHIFT = 0xA1,
		LCONTROL = 0xA2,
		RCONTROL = 0xA3,
		LMENU = 0xA4,
		RMENU = 0xA5,
	}
}

internal class LinuxNativeInputHelper : INativeInputHelper
{
	private static int ConvertBool(bool value)
	{
		return value ? 1 : 0;
	}

	private static Button ConvertMouseButton(MouseButton button)
	{
		return button switch
		{
			MouseButton.Left => Button.LEFT,
			MouseButton.Right => Button.RIGHT,
			MouseButton.Middle => Button.MIDDLE,
			MouseButton.XButton1 => Button.FOUR,
			MouseButton.XButton2 => Button.FIVE,
			_ => throw new ArgumentException($"Cannot convert button: <{button}>", nameof(button))
		};
	}

	private static KeySym ConvertKey(Key key)
	{
		return key switch
		{
			Key.Cancel => KeySym.Cancel,
			Key.Tab => KeySym.Tab,
			Key.LineFeed => KeySym.Linefeed,
			Key.Clear => KeySym.Clear,
			Key.Return => KeySym.Return,
			Key.Pause => KeySym.Pause,
			Key.CapsLock => KeySym.Caps_Lock,
			Key.Escape => KeySym.Escape,
			Key.Space => KeySym.space,
			Key.PageUp => KeySym.Page_Up,
			Key.PageDown => KeySym.Page_Down,
			Key.End => KeySym.End,
			Key.Home => KeySym.Home,

			Key.Left => KeySym.Left,
			Key.Up => KeySym.Up,
			Key.Right => KeySym.Right,
			Key.Down => KeySym.Down,
			Key.Select => KeySym.Select,
			Key.Print => KeySym.Print,
			Key.Execute => KeySym.Execute,
			Key.Insert => KeySym.Insert,
			Key.Delete => KeySym.Delete,

			Key.D0 => KeySym.D0,
			Key.D1 => KeySym.D1,
			Key.D2 => KeySym.D2,
			Key.D3 => KeySym.D3,
			Key.D4 => KeySym.D4,
			Key.D5 => KeySym.D5,
			Key.D6 => KeySym.D6,
			Key.D7 => KeySym.D7,
			Key.D8 => KeySym.D8,
			Key.D9 => KeySym.D9,

			Key.A => KeySym.A,
			Key.B => KeySym.B,
			Key.C => KeySym.C,
			Key.D => KeySym.D,
			Key.E => KeySym.E,
			Key.F => KeySym.F,
			Key.G => KeySym.G,
			Key.H => KeySym.H,
			Key.I => KeySym.I,
			Key.J => KeySym.J,
			Key.K => KeySym.K,
			Key.L => KeySym.L,
			Key.M => KeySym.M,
			Key.N => KeySym.N,
			Key.O => KeySym.O,
			Key.P => KeySym.P,
			Key.Q => KeySym.Q,
			Key.R => KeySym.R,
			Key.S => KeySym.S,
			Key.T => KeySym.T,
			Key.U => KeySym.U,
			Key.V => KeySym.V,
			Key.W => KeySym.W,
			Key.X => KeySym.X,
			Key.Y => KeySym.Y,
			Key.Z => KeySym.Z,

			Key.F1 => KeySym.F1,
			Key.F2 => KeySym.F2,
			Key.F3 => KeySym.F3,
			Key.F4 => KeySym.F4,
			Key.F5 => KeySym.F5,
			Key.F6 => KeySym.F6,
			Key.F7 => KeySym.F7,
			Key.F8 => KeySym.F8,
			Key.F9 => KeySym.F9,
			Key.F10 => KeySym.F10,
			Key.F11 => KeySym.F11,
			Key.F12 => KeySym.F12,
			Key.F13 => KeySym.F13,
			Key.F14 => KeySym.F14,
			Key.F15 => KeySym.F15,
			Key.F16 => KeySym.F16,
			Key.F17 => KeySym.F17,
			Key.F18 => KeySym.F18,
			Key.F19 => KeySym.F19,
			Key.F20 => KeySym.F20,
			Key.F21 => KeySym.F21,
			Key.F22 => KeySym.F22,
			Key.F23 => KeySym.F23,
			Key.F24 => KeySym.F24,

			Key.LeftShift => KeySym.Shift_L,
			Key.RightShift => KeySym.Shift_R,
			Key.LeftCtrl => KeySym.Control_L,
			Key.RightCtrl => KeySym.Control_R,
			Key.LeftAlt => KeySym.Alt_L,
			Key.RightAlt => KeySym.Alt_L,

			_ => throw new ArgumentException($"Cannot convert button: <{key}>", nameof(key))
		};
	}

	public void MouseMove(Point position)
	{
		InvokeMouseMove((int)position.X, (int)position.Y);
	}

	public void MouseUp(MouseButton mouseButton, Point position)
	{
		InvokeClick(false, ConvertMouseButton(mouseButton));
	}

	public void MouseDown(MouseButton mouseButton, Point position)
	{
		InvokeClick(false, ConvertMouseButton(mouseButton));
	}

	private void InvokeMouseMove(int x, int y)
	{
		var display = MainDisplay;
		XWarpPointer(display, Window.None, XDefaultRootWindow(display), 0, 0, 0, 0, x, y);
		XSync(display, ConvertBool(false));
	}

	private void InvokeClick(bool down, Button button)
	{
		var display = MainDisplay;
		XTestFakeButtonEvent(display, button, ConvertBool(down), CurrentTime);
		XSync(display, ConvertBool(false));
	}

	public void KeyDown(Key key)
	{
		InvokeKeyPress(ConvertKey(key), true, 0);
	}

	public void KeyUp(Key key)
	{
		InvokeKeyPress(ConvertKey(key), false, 0);
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Window
	{
		public static Window None { get; } = new Window(0);

		private readonly uint handle;

		private Window(uint handle)
		{
			this.handle = handle;
		}
	}

	//[StructLayout(LayoutKind.Sequential)]
	//public struct DisplayPtr
	//{
	//	//public static DisplayPtr None { get; } = new DisplayPtr(IntPtr.Zero);

	//	private readonly IntPtr pointer;

	//	private DisplayPtr(IntPtr pointer)
	//	{
	//		this.pointer = pointer;
	//	}
	//}

	[StructLayout(LayoutKind.Sequential)]
	public struct X11KeyCode
	{
		private readonly uint code;
	}

	public enum Button : uint
	{
		LEFT = 1,
		MIDDLE = 2,
		RIGHT = 3,
		FOUR = 4,
		FIVE = 5,
	}

	private const string LIBX11_NAME = "libX11.so.6";

	private const int CurrentTime = 0;

#pragma warning disable CA2101
	[DllImport(LIBX11_NAME)]
	public static extern DisplayPtr XOpenDisplay([MarshalAs(UnmanagedType.LPUTF8Str)] string display_name);
#pragma warning restore CA2101

	[DllImport(LIBX11_NAME)]
	public static extern int XCloseDisplay(DisplayPtr display);

	[DllImport(LIBX11_NAME)]
	public static extern Window XDefaultRootWindow(DisplayPtr display);

	[DllImport(LIBX11_NAME)]
	public static extern int XWarpPointer(DisplayPtr display, Window src_w, Window dest_w, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);

	[DllImport(LIBX11_NAME)]
	public static extern int XSync(DisplayPtr display, int discard);

	[DllImport("libXtst.so.6")]
	public static extern int XTestFakeButtonEvent(DisplayPtr display, Button button, int is_press, ulong delay);

	[DllImport("libXtst.so.6")]
	public static extern int XTestFakeKeyEvent(DisplayPtr display, X11KeyCode code, int is_press, ulong delay);

	[DllImport(LIBX11_NAME)]
	public static extern X11KeyCode XKeysymToKeycode(DisplayPtr display, KeySym keysym);

	private DisplayPtr mainDisplay;

	private DisplayPtr MainDisplay
	{
		get
		{
			///* Close the display if displayName has changed */
			//if (hasDisplayNameChanged)
			//{
			//	XCloseMainDisplay();
			//	hasDisplayNameChanged = 0;
			//}

			if (mainDisplay == IntPtr.Zero)
			{
				/* First try the user set displayName */
				mainDisplay = XOpenDisplay(null);
				if (mainDisplay == IntPtr.Zero)
				{
					mainDisplay = XOpenDisplay(":0.0");
				}

				if (mainDisplay == IntPtr.Zero)
				{
					throw new InvalidOperationException("Could not open main display");
				}
				//else if (!registered)
				//{
				//	atexit(&XCloseMainDisplay);
				//	registered = 1;
				//}
			}
			return mainDisplay;
		}
	}

	//char* getMousePosition()
	//{
	//	XInitThreads();

	//	int x, y; /* This is all we care about. Seriously. */
	//	Window garb1, garb2; /* Why you can't specify NULL as a parameter */
	//	int garb_x, garb_y;  /* is beyond me. */
	//	unsigned int more_garbage;

	//	Display* display = XGetMainDisplay();
	//	XQueryPointer(display, XDefaultRootWindow(display), &garb1, &garb2,
	//				&x, &y, &garb_x, &garb_y, &more_garbage);

	//	string res = to_string(x) + "x" + to_string(y);
	//	char* ret = strdup(res.c_str());

	//	return ret;
	//}

	//KeySym keyCodeForChar(char c)
	//{
	//	KeySym code;

	//	char buf[2];
	//	buf[0] = c;
	//	buf[1] = '\0';

	//	code = XStringToKeysym(buf);
	//	if (code == NoSymbol)
	//	{
	//		/* Some special keys are apparently not handled properly by
	//		* XStringToKeysym() on some systems, so search for them instead in our
	//		* mapping table. */
	//		size_t i;
	//		const size_t specialCharacterCount =
	//			sizeof(XSpecialCharacterTable) / sizeof(XSpecialCharacterTable[0]);
	//		for (i = 0; i < specialCharacterCount; ++i)
	//		{
	//			if (c == XSpecialCharacterTable[i].name)
	//			{
	//				code = XSpecialCharacterTable[i].code;
	//				break;
	//			}
	//		}
	//	}

	//	return code;
	//}

	private void InvokeXKeyEvent(KeySym key, bool down)
	{
		var display = MainDisplay;
		var keyCode = XKeysymToKeycode(display, key);
		XTestFakeKeyEvent(display, keyCode, ConvertBool(down), CurrentTime);
		XSync(display, ConvertBool(false));
	}

	private void InvokeKeyPress(KeySym code, bool down, int flags)
	{
		//if (flags & Mod4Mask) xKeyEvent(display, XK_Super_L, is_press);
		//if (flags & Mod1Mask) xKeyEvent(display, XK_Alt_L, is_press);
		//if (flags & ControlMask) xKeyEvent(display, XK_Control_L, is_press);
		//if (flags & ShiftMask) xKeyEvent(display, XK_Shift_L, is_press);

		InvokeXKeyEvent(code, down);
	}

	public enum KeySym : uint
	{
		/*
		 * TTY function keys, cleverly chosen to map to ASCII, for convenience of
		 * programming, but could have been arbitrary (at the cost of lookup
		 * tables in client code).
		 */

		BackSpace = 0xff08,  /* Back space, back char */
		Tab = 0xff09,
		Linefeed = 0xff0a,  /* Linefeed, LF */
		Clear = 0xff0b,
		Return = 0xff0d,  /* Return, enter */
		Pause = 0xff13,  /* Pause, hold */
		Scroll_Lock = 0xff14,
		Sys_Req = 0xff15,
		Escape = 0xff1b,
		Delete = 0xffff,  /* Delete, rubout */

		/* Cursor control & motion */

		Home = 0xff50,
		Left = 0xff51, /* Move left, left arrow */
		Up = 0xff52, /* Move up, up arrow */
		Right = 0xff53, /* Move right, right arrow */
		Down = 0xff54, /* Move down, down arrow */
		Prior = 0xff55, /* Prior, previous */
		Page_Up = 0xff55,
		Next = 0xff56, /* Next */
		Page_Down = 0xff56,
		End = 0xff57, /* EOL */
		Begin = 0xff58, /* BOL */

		/* Misc functions */

		Select = 0xff60,  /* Select, mark */
		Print = 0xff61,
		Execute = 0xff62,  /* Execute, run, do */
		Insert = 0xff63,  /* Insert, insert here */
		Undo = 0xff65,
		Redo = 0xff66,  /* Redo, again */
		Menu = 0xff67,
		Find = 0xff68,  /* Find, search */
		Cancel = 0xff69,  /* Cancel, stop, abort, exit */
		Help = 0xff6a,  /* Help */
		Break = 0xff6b,
		Mode_switch = 0xff7e,  /* Character set switch */
		script_switch = 0xff7e,  /* Alias for mode_switch */
		Num_Lock = 0xff7f,

		/* Keypad functions, keypad numbers cleverly chosen to map to ASCII */

		/*
		 * Auxiliary functions; note the duplicate definitions for left and right
		 * function keys;  Sun keyboards and a few other manufacturers have such
		 * function key groups on the left and/or right sides of the keyboard.
		 * We've not found a keyboard with more than 35 function keys total.
		 */

		F1 = 0xffbe,
		F2 = 0xffbf,
		F3 = 0xffc0,
		F4 = 0xffc1,
		F5 = 0xffc2,
		F6 = 0xffc3,
		F7 = 0xffc4,
		F8 = 0xffc5,
		F9 = 0xffc6,
		F10 = 0xffc7,
		F11 = 0xffc8,
		L1 = 0xffc8,
		F12 = 0xffc9,
		L2 = 0xffc9,
		F13 = 0xffca,
		L3 = 0xffca,
		F14 = 0xffcb,
		L4 = 0xffcb,
		F15 = 0xffcc,
		L5 = 0xffcc,
		F16 = 0xffcd,
		L6 = 0xffcd,
		F17 = 0xffce,
		L7 = 0xffce,
		F18 = 0xffcf,
		L8 = 0xffcf,
		F19 = 0xffd0,
		L9 = 0xffd0,
		F20 = 0xffd1,
		L10 = 0xffd1,
		F21 = 0xffd2,
		R1 = 0xffd2,
		F22 = 0xffd3,
		R2 = 0xffd3,
		F23 = 0xffd4,
		R3 = 0xffd4,
		F24 = 0xffd5,
		R4 = 0xffd5,
		F25 = 0xffd6,
		R5 = 0xffd6,
		F26 = 0xffd7,
		R6 = 0xffd7,
		F27 = 0xffd8,
		R7 = 0xffd8,
		F28 = 0xffd9,
		R8 = 0xffd9,
		F29 = 0xffda,
		R9 = 0xffda,
		F30 = 0xffdb,
		R10 = 0xffdb,
		F31 = 0xffdc,
		R11 = 0xffdc,
		F32 = 0xffdd,
		R12 = 0xffdd,
		F33 = 0xffde,
		R13 = 0xffde,
		F34 = 0xffdf,
		R14 = 0xffdf,
		F35 = 0xffe0,
		R15 = 0xffe0,

		/* Modifiers */

		Shift_L = 0xffe1,  /* Left shift */
		Shift_R = 0xffe2,  /* Right shift */
		Control_L = 0xffe3,  /* Left control */
		Control_R = 0xffe4,  /* Right control */
		Caps_Lock = 0xffe5,  /* Caps lock */
		Shift_Lock = 0xffe6,  /* Shift lock */

		Meta_L = 0xffe7, /* Left meta */
		Meta_R = 0xffe8, /* Right meta */
		Alt_L = 0xffe9, /* Left alt */
		Alt_R = 0xffea, /* Right alt */
		Super_L = 0xffeb, /* Left super */
		Super_R = 0xffec, /* Right super */
		Hyper_L = 0xffed, /* Left hyper */
		Hyper_R = 0xffee, /* Right hyper */

		/* XK_LATIN1 */
		space = 0x0020, /* U+0020 SPACE */
		exclam = 0x0021, /* U+0021 EXCLAMATION MARK */
		quotedbl = 0x0022, /* U+0022 QUOTATION MARK */
		numbersign = 0x0023, /* U+0023 NUMBER SIGN */
		dollar = 0x0024, /* U+0024 DOLLAR SIGN */
		percent = 0x0025, /* U+0025 PERCENT SIGN */
		ampersand = 0x0026, /* U+0026 AMPERSAND */
		apostrophe = 0x0027, /* U+0027 APOSTROPHE */
		quoteright = 0x0027, /* deprecated */
		parenleft = 0x0028, /* U+0028 LEFT PARENTHESIS */
		parenright = 0x0029, /* U+0029 RIGHT PARENTHESIS */
		asterisk = 0x002a, /* U+002A ASTERISK */
		plus = 0x002b, /* U+002B PLUS SIGN */
		comma = 0x002c, /* U+002C COMMA */
		minus = 0x002d, /* U+002D HYPHEN-MINUS */
		period = 0x002e, /* U+002E FULL STOP */
		slash = 0x002f, /* U+002F SOLIDUS */
		D0 = 0x0030, /* U+0030 DIGIT ZERO */
		D1 = 0x0031, /* U+0031 DIGIT ONE */
		D2 = 0x0032, /* U+0032 DIGIT TWO */
		D3 = 0x0033, /* U+0033 DIGIT THREE */
		D4 = 0x0034, /* U+0034 DIGIT FOUR */
		D5 = 0x0035, /* U+0035 DIGIT FIVE */
		D6 = 0x0036, /* U+0036 DIGIT SIX */
		D7 = 0x0037, /* U+0037 DIGIT SEVEN */
		D8 = 0x0038, /* U+0038 DIGIT EIGHT */
		D9 = 0x0039, /* U+0039 DIGIT NINE */
		colon = 0x003a, /* U+003A COLON */
		semicolon = 0x003b, /* U+003B SEMICOLON */
		less = 0x003c, /* U+003C LESS-THAN SIGN */
		equal = 0x003d, /* U+003D EQUALS SIGN */
		greater = 0x003e, /* U+003E GREATER-THAN SIGN */
		question = 0x003f, /* U+003F QUESTION MARK */
		at = 0x0040, /* U+0040 COMMERCIAL AT */
		A = 0x0041, /* U+0041 LATIN CAPITAL LETTER A */
		B = 0x0042, /* U+0042 LATIN CAPITAL LETTER B */
		C = 0x0043, /* U+0043 LATIN CAPITAL LETTER C */
		D = 0x0044, /* U+0044 LATIN CAPITAL LETTER D */
		E = 0x0045, /* U+0045 LATIN CAPITAL LETTER E */
		F = 0x0046, /* U+0046 LATIN CAPITAL LETTER F */
		G = 0x0047, /* U+0047 LATIN CAPITAL LETTER G */
		H = 0x0048, /* U+0048 LATIN CAPITAL LETTER H */
		I = 0x0049, /* U+0049 LATIN CAPITAL LETTER I */
		J = 0x004a, /* U+004A LATIN CAPITAL LETTER J */
		K = 0x004b, /* U+004B LATIN CAPITAL LETTER K */
		L = 0x004c, /* U+004C LATIN CAPITAL LETTER L */
		M = 0x004d, /* U+004D LATIN CAPITAL LETTER M */
		N = 0x004e, /* U+004E LATIN CAPITAL LETTER N */
		O = 0x004f, /* U+004F LATIN CAPITAL LETTER O */
		P = 0x0050, /* U+0050 LATIN CAPITAL LETTER P */
		Q = 0x0051, /* U+0051 LATIN CAPITAL LETTER Q */
		R = 0x0052, /* U+0052 LATIN CAPITAL LETTER R */
		S = 0x0053, /* U+0053 LATIN CAPITAL LETTER S */
		T = 0x0054, /* U+0054 LATIN CAPITAL LETTER T */
		U = 0x0055, /* U+0055 LATIN CAPITAL LETTER U */
		V = 0x0056, /* U+0056 LATIN CAPITAL LETTER V */
		W = 0x0057, /* U+0057 LATIN CAPITAL LETTER W */
		X = 0x0058, /* U+0058 LATIN CAPITAL LETTER X */
		Y = 0x0059, /* U+0059 LATIN CAPITAL LETTER Y */
		Z = 0x005a, /* U+005A LATIN CAPITAL LETTER Z */
		bracketleft = 0x005b, /* U+005B LEFT SQUARE BRACKET */
		backslash = 0x005c, /* U+005C REVERSE SOLIDUS */
		bracketright = 0x005d, /* U+005D RIGHT SQUARE BRACKET */
		asciicircum = 0x005e, /* U+005E CIRCUMFLEX ACCENT */
		underscore = 0x005f, /* U+005F LOW LINE */
		grave = 0x0060, /* U+0060 GRAVE ACCENT */
		quoteleft = 0x0060, /* deprecated */
		a = 0x0061, /* U+0061 LATIN SMALL LETTER A */
		b = 0x0062, /* U+0062 LATIN SMALL LETTER B */
		c = 0x0063, /* U+0063 LATIN SMALL LETTER C */
		d = 0x0064, /* U+0064 LATIN SMALL LETTER D */
		e = 0x0065, /* U+0065 LATIN SMALL LETTER E */
		f = 0x0066, /* U+0066 LATIN SMALL LETTER F */
		g = 0x0067, /* U+0067 LATIN SMALL LETTER G */
		h = 0x0068, /* U+0068 LATIN SMALL LETTER H */
		i = 0x0069, /* U+0069 LATIN SMALL LETTER I */
		j = 0x006a, /* U+006A LATIN SMALL LETTER J */
		k = 0x006b, /* U+006B LATIN SMALL LETTER K */
		l = 0x006c, /* U+006C LATIN SMALL LETTER L */
		m = 0x006d, /* U+006D LATIN SMALL LETTER M */
		n = 0x006e, /* U+006E LATIN SMALL LETTER N */
		o = 0x006f, /* U+006F LATIN SMALL LETTER O */
		p = 0x0070, /* U+0070 LATIN SMALL LETTER P */
		q = 0x0071, /* U+0071 LATIN SMALL LETTER Q */
		r = 0x0072, /* U+0072 LATIN SMALL LETTER R */
		s = 0x0073, /* U+0073 LATIN SMALL LETTER S */
		t = 0x0074, /* U+0074 LATIN SMALL LETTER T */
		u = 0x0075, /* U+0075 LATIN SMALL LETTER U */
		v = 0x0076, /* U+0076 LATIN SMALL LETTER V */
		w = 0x0077, /* U+0077 LATIN SMALL LETTER W */
		x = 0x0078, /* U+0078 LATIN SMALL LETTER X */
		y = 0x0079, /* U+0079 LATIN SMALL LETTER Y */
		z = 0x007a, /* U+007A LATIN SMALL LETTER Z */
		braceleft = 0x007b, /* U+007B LEFT CURLY BRACKET */
		bar = 0x007c, /* U+007C VERTICAL LINE */
		braceright = 0x007d, /* U+007D RIGHT CURLY BRACKET */
		asciitilde = 0x007e /* U+007E TILDE */
	}
}