using System;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008E4 RID: 2276
	public static class VFXBudget
	{
		// Token: 0x06003301 RID: 13057 RVA: 0x000D6D14 File Offset: 0x000D4F14
		public static bool CanAffordSpawn(GameObject prefab)
		{
			return VFXBudget.CanAffordSpawn(prefab.GetComponent<VFXAttributes>());
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000D6D24 File Offset: 0x000D4F24
		public static bool CanAffordSpawn(VFXAttributes vfxAttributes)
		{
			if (vfxAttributes == null)
			{
				return true;
			}
			int intensityScore = vfxAttributes.GetIntensityScore();
			int num = VFXBudget.totalCost + intensityScore + VFXBudget.particleCostBias.value;
			switch (vfxAttributes.vfxPriority)
			{
			case VFXAttributes.VFXPriority.Low:
				return Mathf.Pow((float)VFXBudget.lowPriorityCostThreshold.value / (float)num, VFXBudget.chanceFailurePower) > UnityEngine.Random.value;
			case VFXAttributes.VFXPriority.Medium:
				return Mathf.Pow((float)VFXBudget.mediumPriorityCostThreshold.value / (float)num, VFXBudget.chanceFailurePower) > UnityEngine.Random.value;
			case VFXAttributes.VFXPriority.Always:
				return true;
			default:
				return true;
			}
		}

		// Token: 0x04003419 RID: 13337
		public static int totalCost = 0;

		// Token: 0x0400341A RID: 13338
		private static IntConVar lowPriorityCostThreshold = new IntConVar("vfxbudget_low_priority_cost_threshold", ConVarFlags.None, "50", "");

		// Token: 0x0400341B RID: 13339
		private static IntConVar mediumPriorityCostThreshold = new IntConVar("vfxbudget_medium_priority_cost_threshold", ConVarFlags.None, "200", "");

		// Token: 0x0400341C RID: 13340
		private static IntConVar particleCostBias = new IntConVar("vfxbudget_particle_cost_bias", ConVarFlags.Archive, "0", "");

		// Token: 0x0400341D RID: 13341
		private static float chanceFailurePower = 1f;
	}
}
