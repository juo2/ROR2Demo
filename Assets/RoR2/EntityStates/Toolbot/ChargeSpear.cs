using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000193 RID: 403
	public class ChargeSpear : BaseToolbotPrimarySkillState
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0001E74C File Offset: 0x0001C94C
		public override void OnEnter()
		{
			base.OnEnter();
			this.minChargeDuration = ChargeSpear.baseMinChargeDuration / this.attackSpeedStat;
			this.chargeDuration = ChargeSpear.baseChargeDuration / this.attackSpeedStat;
			if (!base.isInDualWield)
			{
				base.PlayAnimation("Gesture, Additive", "ChargeSpear", "ChargeSpear.playbackRate", this.chargeDuration);
			}
			if (base.muzzleTransform)
			{
				this.chargeupVfxGameObject = UnityEngine.Object.Instantiate<GameObject>(ChargeSpear.chargeupVfxPrefab, base.muzzleTransform);
				this.chargeupVfxGameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = this.chargeDuration;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		public override void OnExit()
		{
			if (this.chargeupVfxGameObject)
			{
				EntityState.Destroy(this.chargeupVfxGameObject);
				this.chargeupVfxGameObject = null;
			}
			if (this.holdChargeVfxGameObject)
			{
				EntityState.Destroy(this.holdChargeVfxGameObject);
				this.holdChargeVfxGameObject = null;
			}
			base.OnExit();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001E831 File Offset: 0x0001CA31
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(base.age / this.chargeDuration, true);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001E854 File Offset: 0x0001CA54
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			float num = base.fixedAge - this.chargeDuration;
			if (num >= 0f)
			{
				float num2 = ChargeSpear.perfectChargeWindow;
			}
			float charge = Mathf.Clamp01(base.fixedAge / this.chargeDuration);
			if (base.fixedAge >= this.chargeDuration)
			{
				if (this.chargeupVfxGameObject)
				{
					EntityState.Destroy(this.chargeupVfxGameObject);
					this.chargeupVfxGameObject = null;
				}
				if (!this.holdChargeVfxGameObject && base.muzzleTransform)
				{
					this.holdChargeVfxGameObject = UnityEngine.Object.Instantiate<GameObject>(ChargeSpear.holdChargeVfxPrefab, base.muzzleTransform);
				}
			}
			if (base.isAuthority)
			{
				if (!this.released && !base.IsKeyDownAuthority())
				{
					this.released = true;
				}
				if (this.released && base.fixedAge >= this.minChargeDuration)
				{
					this.outer.SetNextState(new FireSpear
					{
						charge = charge,
						activatorSkillSlot = base.activatorSkillSlot
					});
				}
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040008A4 RID: 2212
		public static float baseMinChargeDuration;

		// Token: 0x040008A5 RID: 2213
		public static float baseChargeDuration;

		// Token: 0x040008A6 RID: 2214
		public static float perfectChargeWindow;

		// Token: 0x040008A7 RID: 2215
		public new static string muzzleName;

		// Token: 0x040008A8 RID: 2216
		public static GameObject chargeupVfxPrefab;

		// Token: 0x040008A9 RID: 2217
		public static GameObject holdChargeVfxPrefab;

		// Token: 0x040008AA RID: 2218
		private float minChargeDuration;

		// Token: 0x040008AB RID: 2219
		private float chargeDuration;

		// Token: 0x040008AC RID: 2220
		private bool released;

		// Token: 0x040008AD RID: 2221
		private GameObject chargeupVfxGameObject;

		// Token: 0x040008AE RID: 2222
		private GameObject holdChargeVfxGameObject;
	}
}
