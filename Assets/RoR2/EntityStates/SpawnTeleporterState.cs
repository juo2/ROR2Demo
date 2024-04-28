using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000D2 RID: 210
	public class SpawnTeleporterState : BaseState
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
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

		// Token: 0x060003D1 RID: 977 RVA: 0x0000FA30 File Offset: 0x0000DC30
		public override void OnExit()
		{
			base.OnExit();
			if (!this.hasTeleported)
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

		// Token: 0x060003D2 RID: 978 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnTeleporterState.initialDelay && !this.hasTeleported)
			{
				this.hasTeleported = true;
				this.characterModel.invisibilityCount--;
				this.duration = SpawnTeleporterState.initialDelay;
				TeleportOutController.AddTPOutEffect(this.characterModel, 1f, 0f, this.duration);
				GameObject teleportEffectPrefab = Run.instance.GetTeleportEffectPrefab(base.gameObject);
				if (teleportEffectPrefab)
				{
					EffectManager.SimpleEffect(teleportEffectPrefab, base.transform.position, Quaternion.identity, false);
				}
				Util.PlaySound(SpawnTeleporterState.soundString, base.gameObject);
			}
			if (base.fixedAge >= this.duration && this.hasTeleported && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040003CB RID: 971
		private float duration = 4f;

		// Token: 0x040003CC RID: 972
		public static string soundString;

		// Token: 0x040003CD RID: 973
		public static float initialDelay;

		// Token: 0x040003CE RID: 974
		private bool hasTeleported;

		// Token: 0x040003CF RID: 975
		private Animator modelAnimator;

		// Token: 0x040003D0 RID: 976
		private PrintController printController;

		// Token: 0x040003D1 RID: 977
		private CharacterModel characterModel;

		// Token: 0x040003D2 RID: 978
		private CameraTargetParams.AimRequest aimRequest;
	}
}
