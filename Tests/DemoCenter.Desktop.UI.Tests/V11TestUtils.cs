using System.Reflection;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Rendering;
using Avalonia.VisualTree;

using KeyDeviceType = Avalonia.Input.KeyDeviceType;

namespace Eremex.Avalonia.TestUtls;

#nullable disable

public static class V11TestUtils
{
	public static RawPointerEventArgs CreateRawPointerEventArgs(Visual root, RawPointerEventType eventType, Point position, RawInputModifiers modifiers)
	{
		var w = (WindowBase)root;
		var inputRoot = w.GetInputRoot();
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
			inputRoot,
			eventType,
			position,
			modifiers
		};
		return Create<RawPointerEventArgs>(args, types);
	}
	
	private static IInputDevice GetMouseDevice(WindowBase window)
	{
		var mouseDevice = GetPropertyValue<IInputDevice>(window.PlatformImpl!, "MouseDevice");
		ArgumentNullException.ThrowIfNull(mouseDevice);
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
	
	private static PropertyInfo GetPropertyInfo(object obj, string propertyName)
	{
		return GetPropertyInfo(obj.GetType(), propertyName);
	}

	private static PropertyInfo GetPropertyInfo(Type ownerType, string propertyName)
	{
		return ownerType.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
	}
	
	public static IInputRoot GetInputRoot(this Visual element)
	{
		if (element is PopupRoot popupRoot)
		{
			return popupRoot.GetPresentationSource() as IInputRoot; 
		}
		
		return TopLevel.GetTopLevel(element)?.GetPresentationSource() as IInputRoot;
	}
	
	public static void HandleInput(this TopLevel topLevel, object argument)
	{
		var source = topLevel is PopupRoot popupRoot ? popupRoot.GetPresentationSource() : topLevel.GetPresentationSource();
		MethodInfo mi = GetHandleInput(source);
		mi.Invoke(source, new object[]{ argument});
		
		MethodInfo GetHandleInput(IPresentationSource ps)
		{
			var mi = ps.GetType().GetMethod("HandleInput", BindingFlags.Instance | BindingFlags.NonPublic);
			return mi;
		}
	}

	public static IPresentationSource GetPresentationSource(this PopupRoot popupRoot)
	{
		return GetPropertyValue<IPresentationSource>(popupRoot, "InputRoot");
	}
}