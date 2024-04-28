using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000468 RID: 1128
	public class SpawnWards : BaseState
	{
		// Token: 0x06001427 RID: 5159 RVA: 0x00059C38 File Offset: 0x00057E38
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.childLocator = this.animator.GetComponent<ChildLocator>();
			this.duration = SpawnWards.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(SpawnWards.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("Gesture", "SpawnWards", "SpawnWards.playbackRate", this.duration, 0.5f);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00059CB4 File Offset: 0x00057EB4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (!this.hasFiredOrbs && this.animator.GetFloat("SpawnWards.active") > 0.5f)
			{
				this.hasFiredOrbs = true;
				this.FireOrbs();
			}
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00059D28 File Offset: 0x00057F28
		private void FireOrbs()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			Transform transform = this.childLocator.FindChild(SpawnWards.muzzleString).transform;
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = transform.position;
			bullseyeSearch.searchDirection = transform.forward;
			bullseyeSearch.maxDistanceFilter = SpawnWards.orbRange;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(TeamComponent.GetObjectTeam(base.gameObject));
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.RefreshCandidates();
			EffectManager.SimpleMuzzleFlash(SpawnWards.muzzleflashEffectPrefab, base.gameObject, SpawnWards.muzzleString, true);
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			for (int i = 0; i < list.Count; i++)
			{
				HurtBox target = list[i];
				BeetleWardOrb beetleWardOrb = new BeetleWardOrb();
				beetleWardOrb.origin = transform.position;
				beetleWardOrb.target = target;
				beetleWardOrb.speed = SpawnWards.orbTravelSpeed;
				OrbManager.instance.AddOrb(beetleWardOrb);
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00059E18 File Offset: 0x00058018
		public override void OnExit()
		{
			base.OnExit();
			if (base.cameraTargetParams)
			{
				base.cameraTargetParams.fovOverride = -1f;
			}
			int layerIndex = this.animator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, 1.5f);
				this.animator.PlayInFixedTime("LightImpact", layerIndex, 0f);
			}
		}

		// Token: 0x040019D5 RID: 6613
		public static string muzzleString;

		// Token: 0x040019D6 RID: 6614
		public static string attackSoundString;

		// Token: 0x040019D7 RID: 6615
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x040019D8 RID: 6616
		public static float baseDuration = 0.9f;

		// Token: 0x040019D9 RID: 6617
		public static float orbRange;

		// Token: 0x040019DA RID: 6618
		public static float orbTravelSpeed;

		// Token: 0x040019DB RID: 6619
		public static int orbCountMax;

		// Token: 0x040019DC RID: 6620
		private float stopwatch;

		// Token: 0x040019DD RID: 6621
		private int orbCount;

		// Token: 0x040019DE RID: 6622
		private float duration;

		// Token: 0x040019DF RID: 6623
		private bool hasFiredOrbs;

		// Token: 0x040019E0 RID: 6624
		private Animator animator;

		// Token: 0x040019E1 RID: 6625
		private ChildLocator childLocator;
	}
}
