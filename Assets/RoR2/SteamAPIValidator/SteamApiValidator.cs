using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SteamAPIValidator
{
	// Token: 0x020000A2 RID: 162
	public static class SteamApiValidator
	{
		// Token: 0x060002B5 RID: 693
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x060002B6 RID: 694
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x060002B7 RID: 695
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [MarshalAs(UnmanagedType.U4)] [In] int nSize);

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AFDC File Offset: 0x000091DC
		public static bool IsValidSteamApiDll()
		{
			string text = Environment.Is64BitProcess ? "steam_api64.dll" : "steam_api.dll";
			IntPtr intPtr = SteamApiValidator.GetModuleHandle(text);
			if (intPtr == IntPtr.Zero)
			{
				intPtr = SteamApiValidator.LoadLibrary(text);
			}
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			if (intPtr != IntPtr.Zero)
			{
				StringBuilder stringBuilder = new StringBuilder(32767);
				if (SteamApiValidator.GetModuleFileName(intPtr, stringBuilder, 32767) > 0U)
				{
					return SteamApiValidator.CheckIfValveSigned(stringBuilder.ToString());
				}
			}
			return false;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B05C File Offset: 0x0000925C
		public static bool IsValidSteamClientDll()
		{
			IntPtr moduleHandle = SteamApiValidator.GetModuleHandle(Environment.Is64BitProcess ? "steamclient64.dll" : "steamclient.dll");
			if (moduleHandle != IntPtr.Zero)
			{
				StringBuilder stringBuilder = new StringBuilder(32767);
				if (SteamApiValidator.GetModuleFileName(moduleHandle, stringBuilder, 32767) > 0U)
				{
					return SteamApiValidator.CheckIfValveSigned(stringBuilder.ToString());
				}
			}
			return false;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B0B8 File Offset: 0x000092B8
		private static bool CheckIfValveSigned(string filePath)
		{
			bool result;
			try
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr zero2 = IntPtr.Zero;
				IntPtr zero3 = IntPtr.Zero;
				int num;
				int num2;
				int num3;
				if (!WinCrypt.CryptQueryObject(1, Marshal.StringToHGlobalUni(filePath), 16382, 14, 0, out num, out num2, out num3, ref zero, ref zero2, ref zero3))
				{
					result = false;
				}
				else
				{
					result = (num2 == 10);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040002BC RID: 700
		private const int MAX_PATH_SIZE = 32767;
	}
}
