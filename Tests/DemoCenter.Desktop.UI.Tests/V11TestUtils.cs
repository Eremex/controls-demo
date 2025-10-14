using System;
using System.Linq;
using System.Reflection;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;

using Xunit;

namespace Eremex.Avalonia.TestUtls;

public static class V11TestUtils
{
	public static RawTextInputEventArgs CreateRawTextInputEventArgs(IInputRoot root, string text)
	{
		// IKeyboardDevice device, ulong timestamp, IInputRoot root,string text
		var keyboardDevice = GetKeyboardDevice()!;
		
		ulong timestamp = (ulong)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
		object[] args =
		{
			keyboardDevice,
			timestamp,
			root,
			text
		};
		return Create<RawTextInputEventArgs>(args);
	}
	
	public static RawKeyEventArgs CreateRawKeyEventArgs(IInputRoot root, RawKeyEventType eventType, Key key, RawInputModifiers modifiers, string keySymbol = "")
	{
		var keyboardDevice = GetKeyboardDevice()!;
		ulong timestamp = (ulong)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
		var types = new[]
		{
			typeof(KeyboardDevice),
			typeof(ulong),
			typeof(IInputRoot),
			typeof(RawKeyEventType),
			typeof(Key),
			typeof(RawInputModifiers),
			typeof(PhysicalKey),
			typeof(string)
		};
		object[] args =
		{
			keyboardDevice,
			timestamp,
			root,
			eventType,
			key,
			modifiers,
			PhysicalKey.None,
			keySymbol
			
		};
		return Create<RawKeyEventArgs>(args, types);
	}
	
	public static RawPointerEventArgs CreateRawPointerEventArgs(IInputRoot root, RawPointerEventType eventType, Point position, RawInputModifiers modifiers)
	{
		/*
		 *  IInputDevice device,
            ulong timestamp,
            IInputRoot root,
            RawPointerEventType type,
            RawPointerPoint point,
            RawInputModifiers inputModifiers
		 */
		var w = (WindowBase)root;
		var mouseDevice = GetMouseDevice(w)!;
		ulong timestamp = (ulong)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
		var types = new[]
		{
			typeof(IInputDevice),
			typeof(ulong),
			typeof(IInputRoot),
			typeof(RawPointerEventType),
			typeof(Point),
			typeof(RawInputModifiers)
		};
		object[] args =
		{
			mouseDevice,
			timestamp,
			root,
			eventType,
			position,
			modifiers
		};
		return Create<RawPointerEventArgs>(args, types);
	}
	
	private static KeyboardDevice GetKeyboardDevice()
	{
		var keyboardDevice = GetPropertyValue<KeyboardDevice>(typeof(KeyboardDevice), "Instance");
		Assert.NotNull(keyboardDevice);
		return keyboardDevice;
	}
	
	private static IInputDevice GetMouseDevice(WindowBase window)
	{
		var mouseDevice = GetPropertyValue<IInputDevice>(window.PlatformImpl!, "MouseDevice");
		Assert.NotNull(mouseDevice);
		return mouseDevice;
	}
	
	private static T Create<T>(object[] args, Type[] argTypes = null) where T : class
	{
		var types = argTypes ?? args.Select(x => x.GetType()).ToArray();
		var ctor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
			types);
		return ctor?.Invoke(args) as T;
	}
	
	private static T GetPropertyValue<T>(object obj, string propertyName)
	{
		object value = GetPropertyInfo(obj, propertyName)?.GetValue(obj);
		return value is T ? (T)value : default;
	}

	private static T GetPropertyValue<T>(Type ownerType, string propertyName)
	{
		object value = GetPropertyInfo(ownerType, propertyName)?.GetValue(null);
		return value is T ? (T)value : default;
	}
	
	private static PropertyInfo GetPropertyInfo(object obj, string propertyName)
	{
		return GetPropertyInfo(obj.GetType(), propertyName);
	}

	private static PropertyInfo GetPropertyInfo(Type ownerType, string propertyName)
	{
		return ownerType.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
	}
}