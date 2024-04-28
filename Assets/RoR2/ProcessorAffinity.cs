using System;
using System.Runtime.InteropServices;
using RoR2;
using RoR2.ConVar;
using UnityEngine;

// Token: 0x02000084 RID: 132
public static class ProcessorAffinity
{
	// Token: 0x0600022D RID: 557
	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetProcessAffinityMask(IntPtr hProcess, IntPtr dwProcessAffinityMask);

	// Token: 0x0600022E RID: 558
	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetProcessAffinityMask(IntPtr hProcess, ref IntPtr lpProcessAffinityMask, ref IntPtr lpSystemAffinityMask);

	// Token: 0x0600022F RID: 559
	[DllImport("kernel32.dll")]
	private static extern IntPtr GetCurrentProcess();

	// Token: 0x02000085 RID: 133
	private class ProcessorAffinityConVar : BaseConVar
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00009F73 File Offset: 0x00008173
		private ProcessorAffinityConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009F80 File Offset: 0x00008180
		public override void SetString(string newValue)
		{
			ulong num;
			if (TextSerialization.TryParseInvariant(newValue, out num) && num != 0UL)
			{
				ProcessorAffinity.SetProcessAffinityMask(ProcessorAffinity.GetCurrentProcess(), (IntPtr)((long)num));
				return;
			}
			Debug.LogFormat("Could not accept value \"{0}\"", new object[]
			{
				newValue
			});
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009FC0 File Offset: 0x000081C0
		public override string GetString()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			ProcessorAffinity.GetProcessAffinityMask(ProcessorAffinity.GetCurrentProcess(), ref zero, ref zero2);
			return zero.ToString();
		}

		// Token: 0x0400021D RID: 541
		public static ProcessorAffinity.ProcessorAffinityConVar instance = new ProcessorAffinity.ProcessorAffinityConVar("processor_affinity", ConVarFlags.Engine, null, "The processor affinity mask.");
	}
}
