using System;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005A0 RID: 1440
	public static class EngineConVars
	{
		// Token: 0x020005A1 RID: 1441
		private class SyncPhysicsConVar : BaseConVar
		{
			// Token: 0x060019FF RID: 6655 RVA: 0x00009F73 File Offset: 0x00008173
			private SyncPhysicsConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06001A00 RID: 6656 RVA: 0x000707E6 File Offset: 0x0006E9E6
			public override void SetString(string newValue)
			{
				Physics.autoSyncTransforms = BaseConVar.ParseBoolInvariant(newValue);
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x000707F3 File Offset: 0x0006E9F3
			public override string GetString()
			{
				if (!Physics.autoSyncTransforms)
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x0400204C RID: 8268
			public static EngineConVars.SyncPhysicsConVar instance = new EngineConVars.SyncPhysicsConVar("sync_physics", ConVarFlags.None, "0", "Enable/disables Physics 'autosyncing' between moves.");
		}

		// Token: 0x020005A2 RID: 1442
		private class AutoSimulatePhysicsConVar : BaseConVar
		{
			// Token: 0x06001A03 RID: 6659 RVA: 0x00009F73 File Offset: 0x00008173
			private AutoSimulatePhysicsConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06001A04 RID: 6660 RVA: 0x00070823 File Offset: 0x0006EA23
			public override void SetString(string newValue)
			{
				Physics.autoSimulation = BaseConVar.ParseBoolInvariant(newValue);
			}

			// Token: 0x06001A05 RID: 6661 RVA: 0x00070830 File Offset: 0x0006EA30
			public override string GetString()
			{
				if (!Physics.autoSimulation)
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x0400204D RID: 8269
			public static EngineConVars.AutoSimulatePhysicsConVar instance = new EngineConVars.AutoSimulatePhysicsConVar("auto_simulate_physics", ConVarFlags.None, "1", "Enable/disables Physics autosimulate.");
		}

		// Token: 0x020005A3 RID: 1443
		private class TimeScaleConVar : BaseConVar
		{
			// Token: 0x06001A07 RID: 6663 RVA: 0x00009F73 File Offset: 0x00008173
			public TimeScaleConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06001A08 RID: 6664 RVA: 0x00070860 File Offset: 0x0006EA60
			public override void SetString(string newValue)
			{
				Time.timeScale = BaseConVar.ParseFloatInvariant(newValue);
			}

			// Token: 0x06001A09 RID: 6665 RVA: 0x0007086D File Offset: 0x0006EA6D
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(Time.timeScale);
			}

			// Token: 0x0400204E RID: 8270
			private static readonly EngineConVars.TimeScaleConVar instance = new EngineConVars.TimeScaleConVar("timescale", ConVarFlags.ExecuteOnServer | ConVarFlags.Cheat | ConVarFlags.Engine, null, "The timescale of the game.");
		}

		// Token: 0x020005A4 RID: 1444
		private class TimeStepConVar : BaseConVar
		{
			// Token: 0x06001A0B RID: 6667 RVA: 0x00009F73 File Offset: 0x00008173
			public TimeStepConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06001A0C RID: 6668 RVA: 0x00070892 File Offset: 0x0006EA92
			public override void SetString(string newValue)
			{
				Time.fixedDeltaTime = BaseConVar.ParseFloatInvariant(newValue);
			}

			// Token: 0x06001A0D RID: 6669 RVA: 0x0007089F File Offset: 0x0006EA9F
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(Time.fixedDeltaTime);
			}

			// Token: 0x0400204F RID: 8271
			private static readonly EngineConVars.TimeStepConVar instance = new EngineConVars.TimeStepConVar("timestep", ConVarFlags.ExecuteOnServer | ConVarFlags.Cheat | ConVarFlags.Engine, null, "The timestep of the game.");
		}
	}
}
