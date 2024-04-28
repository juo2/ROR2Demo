using System;
using System.Runtime.InteropServices;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// Token: 0x0200002D RID: 45
public static class FlashWindow
{
	// Token: 0x060000C9 RID: 201
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool FlashWindowEx(ref FlashWindow.FLASHWINFO pwfi);

	// Token: 0x060000CA RID: 202
	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	// Token: 0x060000CB RID: 203
	[DllImport("user32.dll")]
	private static extern bool EnumWindows(FlashWindow.EnumWindowsProc enumProc, IntPtr lParam);

	// Token: 0x060000CC RID: 204
	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

	// Token: 0x060000CD RID: 205
	[DllImport("kernel32.dll")]
	private static extern IntPtr GetCurrentProcessId();

	// Token: 0x060000CE RID: 206 RVA: 0x00004E64 File Offset: 0x00003064
	private static bool GetWindowEnum(IntPtr hWnd, IntPtr lParam)
	{
		IntPtr value;
		FlashWindow.GetWindowThreadProcessId(hWnd, out value);
		if (value == FlashWindow.myProcessId)
		{
			FlashWindow.myWindow = hWnd;
			return false;
		}
		return true;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00004E90 File Offset: 0x00003090
	private static void UpdateCurrentWindow()
	{
		FlashWindow.EnumWindows(new FlashWindow.EnumWindowsProc(FlashWindow.GetWindowEnum), IntPtr.Zero);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00004EAC File Offset: 0x000030AC
	private static bool IsCurrentWindowValid()
	{
		IntPtr value;
		FlashWindow.GetWindowThreadProcessId(FlashWindow.myWindow, out value);
		if (value != FlashWindow.myProcessId)
		{
			FlashWindow.myWindow = IntPtr.Zero;
		}
		return FlashWindow.myWindow != IntPtr.Zero;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00004EEC File Offset: 0x000030EC
	public static IntPtr GetWindowHandle()
	{
		if (!FlashWindow.IsCurrentWindowValid())
		{
			FlashWindow.UpdateCurrentWindow();
		}
		return FlashWindow.myWindow;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00004F00 File Offset: 0x00003100
	public static bool Flash(IntPtr formHandle)
	{
		if (FlashWindow.Win2000OrLater)
		{
			FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(formHandle, 15U, uint.MaxValue, 0U);
			return FlashWindow.FlashWindowEx(ref flashwinfo);
		}
		return false;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00004F28 File Offset: 0x00003128
	private static FlashWindow.FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
	{
		FlashWindow.FLASHWINFO flashwinfo = default(FlashWindow.FLASHWINFO);
		flashwinfo.cbSize = Convert.ToUInt32(Marshal.SizeOf<FlashWindow.FLASHWINFO>(flashwinfo));
		flashwinfo.hwnd = handle;
		flashwinfo.dwFlags = flags;
		flashwinfo.uCount = count;
		flashwinfo.dwTimeout = timeout;
		return flashwinfo;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00004F70 File Offset: 0x00003170
	public static bool Flash(IntPtr formHandle, uint count)
	{
		if (FlashWindow.Win2000OrLater)
		{
			FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(formHandle, 3U, count, 0U);
			return FlashWindow.FlashWindowEx(ref flashwinfo);
		}
		return false;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00004F97 File Offset: 0x00003197
	public static bool Flash()
	{
		return FlashWindow.Flash(FlashWindow.GetWindowHandle());
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00004FA4 File Offset: 0x000031A4
	public static bool Start(IntPtr formHandle)
	{
		if (FlashWindow.Win2000OrLater)
		{
			FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(formHandle, 3U, uint.MaxValue, 0U);
			return FlashWindow.FlashWindowEx(ref flashwinfo);
		}
		return false;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00004FCC File Offset: 0x000031CC
	public static bool Stop(IntPtr formHandle)
	{
		if (FlashWindow.Win2000OrLater)
		{
			FlashWindow.FLASHWINFO flashwinfo = FlashWindow.Create_FLASHWINFO(formHandle, 0U, uint.MaxValue, 0U);
			return FlashWindow.FlashWindowEx(ref flashwinfo);
		}
		return false;
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004FF3 File Offset: 0x000031F3
	private static bool Win2000OrLater
	{
		get
		{
			return Environment.OSVersion.Version.Major >= 5;
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000500C File Offset: 0x0000320C
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		SceneManager.activeSceneChanged += delegate(Scene previousScene, Scene newScene)
		{
			if (newScene.name == "lobby")
			{
				FlashWindow.Flash();
			}
		};
		NetworkManagerSystem.onClientConnectGlobal += delegate(NetworkConnection conn)
		{
			FlashWindow.Flash();
		};
	}

	// Token: 0x040000C4 RID: 196
	private static IntPtr myWindow;

	// Token: 0x040000C5 RID: 197
	private static readonly IntPtr myProcessId = FlashWindow.GetCurrentProcessId();

	// Token: 0x040000C6 RID: 198
	public const uint FLASHW_STOP = 0U;

	// Token: 0x040000C7 RID: 199
	public const uint FLASHW_CAPTION = 1U;

	// Token: 0x040000C8 RID: 200
	public const uint FLASHW_TRAY = 2U;

	// Token: 0x040000C9 RID: 201
	public const uint FLASHW_ALL = 3U;

	// Token: 0x040000CA RID: 202
	public const uint FLASHW_TIMER = 4U;

	// Token: 0x040000CB RID: 203
	public const uint FLASHW_TIMERNOFG = 12U;

	// Token: 0x0200002E RID: 46
	// (Invoke) Token: 0x060000DC RID: 220
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	// Token: 0x0200002F RID: 47
	private struct FLASHWINFO
	{
		// Token: 0x040000CC RID: 204
		public uint cbSize;

		// Token: 0x040000CD RID: 205
		public IntPtr hwnd;

		// Token: 0x040000CE RID: 206
		public uint dwFlags;

		// Token: 0x040000CF RID: 207
		public uint uCount;

		// Token: 0x040000D0 RID: 208
		public uint dwTimeout;
	}
}
