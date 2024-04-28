using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008E1 RID: 2273
	public class VFXAttributes : MonoBehaviour
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000D6C83 File Offset: 0x000D4E83
		public static ReadOnlyCollection<VFXAttributes> readonlyVFXList
		{
			get
			{
				return VFXAttributes._readonlyVFXList;
			}
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000D6C8C File Offset: 0x000D4E8C
		public int GetIntensityScore()
		{
			switch (this.vfxIntensity)
			{
			case VFXAttributes.VFXIntensity.Low:
				return 1;
			case VFXAttributes.VFXIntensity.Medium:
				return 5;
			case VFXAttributes.VFXIntensity.High:
				return 25;
			default:
				return 0;
			}
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000D6CBC File Offset: 0x000D4EBC
		public void OnEnable()
		{
			VFXAttributes.vfxList.Add(this);
			VFXBudget.totalCost += this.GetIntensityScore();
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000D6CDA File Offset: 0x000D4EDA
		public void OnDisable()
		{
			VFXAttributes.vfxList.Remove(this);
			VFXBudget.totalCost -= this.GetIntensityScore();
		}

		// Token: 0x0400340B RID: 13323
		private static List<VFXAttributes> vfxList = new List<VFXAttributes>();

		// Token: 0x0400340C RID: 13324
		private static ReadOnlyCollection<VFXAttributes> _readonlyVFXList = new ReadOnlyCollection<VFXAttributes>(VFXAttributes.vfxList);

		// Token: 0x0400340D RID: 13325
		[Tooltip("Controls whether or not a VFX appears at all - consider if you would notice if this entire VFX never appeared. Also means it has a networking consequence.")]
		public VFXAttributes.VFXPriority vfxPriority;

		// Token: 0x0400340E RID: 13326
		[Tooltip("Define how expensive a particle system is IF it appears.")]
		public VFXAttributes.VFXIntensity vfxIntensity;

		// Token: 0x0400340F RID: 13327
		public Light[] optionalLights;

		// Token: 0x04003410 RID: 13328
		[Tooltip("Particle systems that may be deactivated without impacting gameplay.")]
		public ParticleSystem[] secondaryParticleSystem;

		// Token: 0x020008E2 RID: 2274
		public enum VFXPriority
		{
			// Token: 0x04003412 RID: 13330
			Low,
			// Token: 0x04003413 RID: 13331
			Medium,
			// Token: 0x04003414 RID: 13332
			Always
		}

		// Token: 0x020008E3 RID: 2275
		public enum VFXIntensity
		{
			// Token: 0x04003416 RID: 13334
			Low,
			// Token: 0x04003417 RID: 13335
			Medium,
			// Token: 0x04003418 RID: 13336
			High
		}
	}
}
