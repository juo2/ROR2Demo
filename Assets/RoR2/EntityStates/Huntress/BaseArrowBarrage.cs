using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Huntress
{
	// Token: 0x02000317 RID: 791
	public class BaseArrowBarrage : BaseState
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x0003C4BC File Offset: 0x0003A6BC
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.beginLoopSoundString, base.gameObject);
			this.huntressTracker = base.GetComponent<HuntressTracker>();
			if (this.huntressTracker)
			{
				this.huntressTracker.enabled = false;
			}
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003C528 File Offset: 0x0003A728
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterMotor)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			if (base.isAuthority && base.inputBank)
			{
				if (base.skillLocator && base.skillLocator.utility.IsReady() && base.inputBank.skill3.justPressed)
				{
					this.outer.SetNextStateToMain();
					return;
				}
				if (base.fixedAge >= this.maxDuration || base.inputBank.skill1.justPressed || base.inputBank.skill4.justPressed)
				{
					this.HandlePrimaryAttack();
				}
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void HandlePrimaryAttack()
		{
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
		public override void OnExit()
		{
			this.PlayAnimation("FullBody, Override", "FireArrowRain");
			Util.PlaySound(this.endLoopSoundString, base.gameObject);
			Util.PlaySound(this.fireSoundString, base.gameObject);
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			if (this.huntressTracker)
			{
				this.huntressTracker.enabled = true;
			}
			base.OnExit();
		}

		// Token: 0x04001185 RID: 4485
		[SerializeField]
		public float maxDuration;

		// Token: 0x04001186 RID: 4486
		[SerializeField]
		public string beginLoopSoundString;

		// Token: 0x04001187 RID: 4487
		[SerializeField]
		public string endLoopSoundString;

		// Token: 0x04001188 RID: 4488
		[SerializeField]
		public string fireSoundString;

		// Token: 0x04001189 RID: 4489
		private HuntressTracker huntressTracker;

		// Token: 0x0400118A RID: 4490
		private CameraTargetParams.AimRequest aimRequest;
	}
}
