using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000449 RID: 1097
	public class StaggerEnter : StaggerBaseState
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000575A1 File Offset: 0x000557A1
		public override EntityState nextState
		{
			get
			{
				return new StaggerLoop();
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000575A8 File Offset: 0x000557A8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "StaggerEnter", "Stagger.playbackRate", this.duration);
			if (StaggerEnter.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(StaggerEnter.effectPrefab, base.gameObject, StaggerEnter.effectMuzzleString, false);
			}
		}

		// Token: 0x0400190B RID: 6411
		public static GameObject effectPrefab;

		// Token: 0x0400190C RID: 6412
		public static string effectMuzzleString;
	}
}
