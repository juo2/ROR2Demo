using System;
using System.Runtime.InteropServices;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000AA7 RID: 2727
	public static class WindowsConVars
	{
		// Token: 0x06003EC0 RID: 16064
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtSetTimerResolution(int desiredResolution, bool setResolution, out int currentResolution);

		// Token: 0x06003EC1 RID: 16065
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtQueryTimerResolution(out int minimumResolution, out int maximumResolution, out int currentResolution);

		// Token: 0x02000AA8 RID: 2728
		private class TimerResolutionConVar : BaseConVar
		{
			// Token: 0x06003EC2 RID: 16066 RVA: 0x00009F73 File Offset: 0x00008173
			private TimerResolutionConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003EC3 RID: 16067 RVA: 0x00102F04 File Offset: 0x00101104
			public override void SetString(string newValue)
			{
				int num;
				WindowsConVars.NtSetTimerResolution(BaseConVar.ParseIntInvariant(newValue), true, out num);
				Debug.LogFormat("{0} set to {1}", new object[]
				{
					this.name,
					num
				});
			}

			// Token: 0x06003EC4 RID: 16068 RVA: 0x00102F44 File Offset: 0x00101144
			public override string GetString()
			{
				int num;
				int num2;
				int value;
				WindowsConVars.NtQueryTimerResolution(out num, out num2, out value);
				return TextSerialization.ToStringInvariant(value);
			}

			// Token: 0x04003D07 RID: 15623
			private static WindowsConVars.TimerResolutionConVar instance = new WindowsConVars.TimerResolutionConVar("timer_resolution", ConVarFlags.Engine, null, "The Windows timer resolution.");
		}
	}
}
