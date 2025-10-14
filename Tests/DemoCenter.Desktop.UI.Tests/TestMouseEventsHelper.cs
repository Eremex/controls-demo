using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.Threading;
using Avalonia.VisualTree;

using Xunit;
using Xunit.Abstractions;

namespace Eremex.Avalonia.TestUtls;

public class TestMouseEventsHelper
{
	private readonly ITestOutputHelper testOutputHelper;
	public TestMouseEventsHelper() : this(null) { }
	public TestMouseEventsHelper(ITestOutputHelper testOutputHelper = null)
	{
		this.testOutputHelper = testOutputHelper;
	}
		
	private void CheckVisualTree(TopLevel win, Control target, Point position)
	{
		// если контрол задизаблен, то GetInputElementsAt не вернет его, так как он не пройдет HitTestFilter
		if (target == null || win == target || !target.IsEnabled)
			return;
		bool found = false;
		// RawPointerEventArgs e = V11TestUtils.CreateRawPointerEventArgs(win, RawPointerEventType.Move, position, RawInputModifiers.None);
		// PropertyInfo pInfo = e.GetType().GetProperty("Position", BindingFlags.Public | BindingFlags.Instance);
		// var hitPoint = (Point)pInfo!.GetValue(e)!;
		var hitPoint = position;
		
		for(int i = 0; i < 30; i++) 
		{
			if(i > 0) 
			{
				Debug.WriteLine("Wait For Actualize RenderTree");
			}

			IInputElement item = win.InputHitTest(hitPoint);

			Visual vis = item as Visual;
			while(vis != null) 
			{
				if(vis == target) 
				{
					found = true;
					break;
				}

				vis = vis.GetVisualParent();
			}

			if(found)
				break;
			win.UpdateLayout();
			Dispatcher.UIThread.RunJobs();
			Thread.Sleep(50);
		}

		if (found)
			return;
		testOutputHelper?.WriteLine("CheckVisualTree: " + target.ToString() + " -> " + win.ToString() + " Fails");
		Assert.True(found);
	}

	public void SendMouseEvent(TopLevel win, Control target, RawPointerEventType eventType, Point position, RawInputModifiers modifiers)
	{
		CheckVisualTree(win, target, position);
		
		var e = V11TestUtils.CreateRawPointerEventArgs(win, eventType, position, modifiers);
		MethodInfo mi = GetHandleInput(win);
		mi.Invoke(win, new object[]{ e});
	}

	public void SendLeftMouseDown(TopLevel win, Control target, Point position, KeyModifiers modifiers)
	{
		SendMouseEvent(win, target, RawPointerEventType.LeftButtonDown, position, ToRawInputModifiers(modifiers));
	}

	private RawInputModifiers ToRawInputModifiers(KeyModifiers modifiers)
	{
		var res = RawInputModifiers.None;
		if (modifiers.HasFlag(KeyModifiers.Control))
		{
			res |= RawInputModifiers.Control;
		}
		
		if (modifiers.HasFlag(KeyModifiers.Shift))
		{
			res |= RawInputModifiers.Shift;
		}
		
		if (modifiers.HasFlag(KeyModifiers.Alt))
		{
			res |= RawInputModifiers.Alt;
		}

		return res;
	}

	public void SendLeftMouseDown(TopLevel win, Control target, Point position)
	{
		SendMouseEvent(win, target, RawPointerEventType.LeftButtonDown, position, RawInputModifiers.None);
	}

	public void SendRightMouseDown(TopLevel win, Control target, Point position, ITestOutputHelper testOutputHelper)
	{
		SendMouseEvent(win, target, RawPointerEventType.RightButtonDown, position, RawInputModifiers.None);
	}
	
	public void SendRightMouseDown(Control control)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendRightMouseDown(root, GetMousePoint(root, control));
	}

	public void SendRightMouseUp(TopLevel win, Control target, Point position)
	{
		SendMouseEvent(win, target, RawPointerEventType.RightButtonUp, position, RawInputModifiers.None);
	}
	
	public void SendRightMouseUp(Control control)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendRightMouseUp(root, control, GetMousePoint(root, control));
	}

	public void SendNcLeftMouseDown(TopLevel win, Control target, Point position)
	{
		SendMouseEvent(win, target, RawPointerEventType.NonClientLeftButtonDown, position, RawInputModifiers.None);
	}

	public void SendLeftMouseUp(TopLevel win, Control target, Point position)
	{
		SendMouseEvent(win, target, RawPointerEventType.LeftButtonUp, position, RawInputModifiers.None);
	}
	
	public void SendLeftMouseUp(Control target, Point position, KeyModifiers modifiers = KeyModifiers.None)
	{
		TopLevel root = CheckVisualRoot(target);
		SendMouseEvent(root, target, RawPointerEventType.LeftButtonUp, GetMousePoint(root, target, position), ToRawInputModifiers(modifiers));
	}
	
	public void SendLeftMouseUp(TopLevel win, Control target, Point position, KeyModifiers modifiers)
	{
		SendMouseEvent(win, target, RawPointerEventType.LeftButtonUp, position, ToRawInputModifiers(modifiers));
	}

	public void SendLeftMouseDown(TopLevel win, Point position)
	{
		SendMouseEvent(win, null, RawPointerEventType.LeftButtonDown, position, RawInputModifiers.None);
	}

	public void SendRightMouseDown(TopLevel win, Point position)
	{
		SendMouseEvent(win, null, RawPointerEventType.RightButtonDown, position, RawInputModifiers.None);
	}

	public void SendRightMouseUp(TopLevel win, Point position)
	{
		SendMouseEvent(win, null, RawPointerEventType.RightButtonUp, position, RawInputModifiers.None);
	}

	public void SendNcLeftMouseDown(TopLevel win, Point position)
	{
		SendMouseEvent(win, null, RawPointerEventType.NonClientLeftButtonDown, position, RawInputModifiers.None);
	}

	public void SendLeftMouseUp(TopLevel win, Point position)
	{
		SendMouseEvent(win, null, RawPointerEventType.LeftButtonUp, position, RawInputModifiers.None);
	}

	MethodInfo GetHandleInput(TopLevel win) 
	{
		Type type = typeof(TopLevel);
		var mi = type.GetMethod("HandleInput", BindingFlags.Instance | BindingFlags.NonPublic);
		return mi;
	}
	
	public void SendMouseMove(TopLevel win, Control target, Point position, RawInputModifiers modifiers = RawInputModifiers.None)
	{
		CheckVisualTree(win, target, position);
		
		var e = V11TestUtils.CreateRawPointerEventArgs(win, RawPointerEventType.Move, position, modifiers);
		MethodInfo mi = GetHandleInput(win);
		mi.Invoke(win, new object[]{ e});
	}
	
	public void SendMouseOut(TopLevel win, Point position)
	{
		var e = V11TestUtils.CreateRawPointerEventArgs(win, RawPointerEventType.Move, position, RawInputModifiers.None);
		MethodInfo mi = GetHandleInput(win);
		mi.Invoke(win, new object[]{ e});
	}

	public void SendMouseMove(Control control, Point localPosition, RawInputModifiers modifiers = RawInputModifiers.None, bool translatePositionToRoot = true)
	{
		TopLevel root = CheckVisualRoot(control);
		SendMouseMove(root, control, translatePositionToRoot ? GetMousePoint(root, control, localPosition) : localPosition, modifiers);
	}

	private TopLevel CheckVisualRoot(Control control)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		for (int i = 0; i < 20; i++)
		{
			if (root != null)
				break;
			Dispatcher.UIThread.RunJobs();
			Thread.Sleep(20);
			root = (TopLevel)control.GetVisualRoot();
		}

		Assert.NotNull(root);
		return root;
	}

	public void SendMouseMove(Control control, RawInputModifiers modifiers = RawInputModifiers.None)
	{
		TopLevel root = CheckVisualRoot(control);
		SendMouseMove(root, control, GetMousePoint(root, control), modifiers);
	}

	public void SendMouseOut(Control control)
	{
		TopLevel root = CheckVisualRoot(control);
		SendMouseOut(root, GetMousePoint(root, control, new Point(-1, 0)));
	}

	public void SendLeftMouseDown(Control control)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendLeftMouseDown(root, control, GetMousePoint(root, control));
	}
	
	public void SendLeftMouseDown(Control control, KeyModifiers modifiers)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendLeftMouseDown(root, control, GetMousePoint(root, control), modifiers);
	}
	
	public void SendLeftMouseDown(Control control, Point pt, KeyModifiers modifiers)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendLeftMouseDown(root, control, GetMousePoint(root, control, pt), modifiers);
	}

	public void SendLeftMouseUp(Control control)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendLeftMouseUp(root, control, GetMousePoint(root, control));
	}
	
	public void SendLeftMouseUp(Control control, KeyModifiers modifiers)
	{
		TopLevel root = (TopLevel)control.GetVisualRoot();
		SendLeftMouseUp(root, control, GetMousePoint(root, control), modifiers);
	}

	public Point GetMousePoint(TopLevel root, Control control)
	{
		if (control.Bounds.Width == 0 || control.Bounds.Height == 0)
		{
			Dispatcher.UIThread.RunJobs();
			Thread.Sleep(50);
		}
		
		Point pt = new Point(control.Bounds.Width / 2, control.Bounds.Height / 2);
		return GetMousePoint(root, control, pt);
	}

	public Point GetMousePoint(TopLevel root, Control control, Point pt)
	{
		var p = control.TranslatePoint(pt, root);
		for (int i = 0; i < 50 && p == null; i++)
		{
			Dispatcher.UIThread.RunJobs();
			Thread.Sleep(20);
			p = control.TranslatePoint(pt, root);
		}

		Assert.NotNull(p);
		PixelPoint screen = root.PointToScreen(p.Value);
		Point winPoint = root.PointToClient(screen);
		return winPoint;
	}
	
	public void SendMiddleMouseUp(TopLevel win, Control target, Point position)
	{
		SendMouseEvent(win, target, RawPointerEventType.MiddleButtonUp, position, RawInputModifiers.None);
	}
	
	public void SendMiddleMouseDown(TopLevel win, Control target, Point position)
	{
		SendMouseEvent(win, target, RawPointerEventType.MiddleButtonDown, position, RawInputModifiers.None);
	}
}