using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Vulture.Weapon
{
	// Token: 0x020000E4 RID: 228
	public class ChargeWindblade : BaseSkillState
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00010F6C File Offset: 0x0000F16C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeWindblade.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "ChargeWindblade", "ChargeWindblade.playbackRate", this.duration);
			Util.PlaySound(ChargeWindblade.soundString, base.gameObject);
			base.characterBody.SetAimTimer(3f);
			Transform transform = base.FindModelChild(ChargeWindblade.muzzleString);
			if (transform && ChargeWindblade.chargeEffectPrefab)
			{
				this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeWindblade.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeEffectInstance.transform.parent = transform;
				ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001103A File Offset: 0x0000F23A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new FireWindblade());
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011060 File Offset: 0x0000F260
		public override void OnExit()
		{
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x0400041A RID: 1050
		public static float baseDuration;

		// Token: 0x0400041B RID: 1051
		public static string muzzleString;

		// Token: 0x0400041C RID: 1052
		public static GameObject chargeEffectPrefab;

		// Token: 0x0400041D RID: 1053
		public static string soundString;

		// Token: 0x0400041E RID: 1054
		private float duration;

		// Token: 0x0400041F RID: 1055
		private GameObject chargeEffectInstance;
	}
}
