using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x0200042E RID: 1070
	public class ChargeCaptainShotgun : BaseState
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x00055494 File Offset: 0x00053694
		public override void OnEnter()
		{
			base.OnEnter();
			this.minChargeDuration = ChargeCaptainShotgun.baseMinChargeDuration / this.attackSpeedStat;
			this.chargeDuration = ChargeCaptainShotgun.baseChargeDuration / this.attackSpeedStat;
			base.PlayCrossfade("Gesture, Override", "ChargeCaptainShotgun", "ChargeCaptainShotgun.playbackRate", this.chargeDuration, 0.1f);
			base.PlayCrossfade("Gesture, Additive", "ChargeCaptainShotgun", "ChargeCaptainShotgun.playbackRate", this.chargeDuration, 0.1f);
			this.muzzleTransform = base.FindModelChild(ChargeCaptainShotgun.muzzleName);
			if (this.muzzleTransform)
			{
				this.chargeupVfxGameObject = UnityEngine.Object.Instantiate<GameObject>(ChargeCaptainShotgun.chargeupVfxPrefab, this.muzzleTransform);
				this.chargeupVfxGameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = this.chargeDuration;
			}
			this.enterSoundID = Util.PlayAttackSpeedSound(ChargeCaptainShotgun.enterSoundString, base.gameObject, this.attackSpeedStat);
			Util.PlaySound(ChargeCaptainShotgun.playChargeSoundString, base.gameObject);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00055584 File Offset: 0x00053784
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
			AkSoundEngine.StopPlayingID(this.enterSoundID);
			Util.PlaySound(ChargeCaptainShotgun.stopChargeSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000555F1 File Offset: 0x000537F1
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(base.age / this.chargeDuration, true);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00055614 File Offset: 0x00053814
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(1f);
			Mathf.Clamp01(base.fixedAge / this.chargeDuration);
			if (base.fixedAge >= this.chargeDuration)
			{
				if (this.chargeupVfxGameObject)
				{
					EntityState.Destroy(this.chargeupVfxGameObject);
					this.chargeupVfxGameObject = null;
				}
				if (!this.holdChargeVfxGameObject && this.muzzleTransform)
				{
					this.holdChargeVfxGameObject = UnityEngine.Object.Instantiate<GameObject>(ChargeCaptainShotgun.holdChargeVfxPrefab, this.muzzleTransform);
				}
			}
			if (base.isAuthority)
			{
				if (!this.released && (!base.inputBank || !base.inputBank.skill1.down))
				{
					this.released = true;
				}
				if (this.released)
				{
					this.outer.SetNextState(new FireCaptainShotgun());
				}
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001890 RID: 6288
		public static float baseMinChargeDuration;

		// Token: 0x04001891 RID: 6289
		public static float baseChargeDuration;

		// Token: 0x04001892 RID: 6290
		public static string muzzleName;

		// Token: 0x04001893 RID: 6291
		public static GameObject chargeupVfxPrefab;

		// Token: 0x04001894 RID: 6292
		public static GameObject holdChargeVfxPrefab;

		// Token: 0x04001895 RID: 6293
		public static string enterSoundString;

		// Token: 0x04001896 RID: 6294
		public static string playChargeSoundString;

		// Token: 0x04001897 RID: 6295
		public static string stopChargeSoundString;

		// Token: 0x04001898 RID: 6296
		private float minChargeDuration;

		// Token: 0x04001899 RID: 6297
		private float chargeDuration;

		// Token: 0x0400189A RID: 6298
		private bool released;

		// Token: 0x0400189B RID: 6299
		private GameObject chargeupVfxGameObject;

		// Token: 0x0400189C RID: 6300
		private GameObject holdChargeVfxGameObject;

		// Token: 0x0400189D RID: 6301
		private Transform muzzleTransform;

		// Token: 0x0400189E RID: 6302
		private uint enterSoundID;
	}
}
