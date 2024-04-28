using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GummyClone
{
	// Token: 0x02000340 RID: 832
	public class GummyCloneSpawnState : BaseState
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00040518 File Offset: 0x0003E718
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			if (this.modelAnimator)
			{
				GameObject gameObject = this.modelAnimator.gameObject;
				this.characterModel = gameObject.GetComponent<CharacterModel>();
				this.characterModel.invisibilityCount++;
			}
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000405A8 File Offset: 0x0003E7A8
		public override void OnExit()
		{
			base.OnExit();
			if (!this.hasFinished)
			{
				this.characterModel.invisibilityCount--;
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 3f);
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00040614 File Offset: 0x0003E814
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.initialDelay && !this.hasFinished)
			{
				this.hasFinished = true;
				this.characterModel.invisibilityCount--;
				this.duration = this.initialDelay;
				if (this.effectPrefab)
				{
					EffectManager.SimpleEffect(this.effectPrefab, base.transform.position, Quaternion.identity, false);
				}
				Util.PlaySound(this.soundString, base.gameObject);
			}
			if (base.fixedAge >= this.duration && this.hasFinished && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040012A9 RID: 4777
		[SerializeField]
		public float duration = 4f;

		// Token: 0x040012AA RID: 4778
		[SerializeField]
		public string soundString;

		// Token: 0x040012AB RID: 4779
		[SerializeField]
		public float initialDelay;

		// Token: 0x040012AC RID: 4780
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x040012AD RID: 4781
		private bool hasFinished;

		// Token: 0x040012AE RID: 4782
		private Animator modelAnimator;

		// Token: 0x040012AF RID: 4783
		private CharacterModel characterModel;

		// Token: 0x040012B0 RID: 4784
		private CameraTargetParams.AimRequest aimRequest;
	}
}
