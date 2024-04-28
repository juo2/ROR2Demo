using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x02000176 RID: 374
	public class BurrowIn : BaseState
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0001C308 File Offset: 0x0001A508
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = BurrowIn.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "BurrowIn", "BurrowIn.playbackRate", this.duration, 0.1f);
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			Util.PlaySound(BurrowIn.burrowInSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(BurrowIn.burrowPrefab, base.gameObject, "BurrowCenter", false);
			if (base.characterBody)
			{
				base.characterBody.hideCrosshair = true;
			}
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001C3C9 File Offset: 0x0001A5C9
		public override void OnExit()
		{
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				Burrowed nextState = new Burrowed();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040007FD RID: 2045
		public static GameObject burrowPrefab;

		// Token: 0x040007FE RID: 2046
		public static float baseDuration;

		// Token: 0x040007FF RID: 2047
		public static string burrowInSoundString;

		// Token: 0x04000800 RID: 2048
		private float stopwatch;

		// Token: 0x04000801 RID: 2049
		private float duration;

		// Token: 0x04000802 RID: 2050
		private Transform modelTransform;

		// Token: 0x04000803 RID: 2051
		private ChildLocator childLocator;

		// Token: 0x04000804 RID: 2052
		private CameraTargetParams.AimRequest aimRequest;
	}
}
