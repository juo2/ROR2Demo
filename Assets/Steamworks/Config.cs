using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000176 RID: 374
	public static class Config
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x00038614 File Offset: 0x00036814
		public static void ForUnity(string platform)
		{
			if (platform == "WindowsEditor" || platform == "WindowsPlayer")
			{
				if (IntPtr.Size == 4)
				{
					Config.UseThisCall = false;
				}
				Config.ForcePlatform(OperatingSystem.Windows, (IntPtr.Size == 4) ? Architecture.x86 : Architecture.x64);
			}
			if (platform == "OSXEditor" || platform == "OSXPlayer" || platform == "OSXDashboardPlayer")
			{
				Config.ForcePlatform(OperatingSystem.macOS, (IntPtr.Size == 4) ? Architecture.x86 : Architecture.x64);
			}
			if (platform == "LinuxPlayer" || platform == "LinuxEditor")
			{
				Config.ForcePlatform(OperatingSystem.Linux, (IntPtr.Size == 4) ? Architecture.x86 : Architecture.x64);
			}
			Console.WriteLine("Facepunch.Steamworks Unity: " + platform);
			Console.WriteLine("Facepunch.Steamworks Os: " + Platform.Os);
			Console.WriteLine("Facepunch.Steamworks Arch: " + Platform.Arch);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00038702 File Offset: 0x00036902
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00038709 File Offset: 0x00036909
		public static bool UseThisCall { get; set; } = true;

		// Token: 0x06000B99 RID: 2969 RVA: 0x00038711 File Offset: 0x00036911
		public static void ForcePlatform(OperatingSystem os, Architecture arch)
		{
			Platform.Os = os;
			Platform.Arch = arch;
		}
	}
}
