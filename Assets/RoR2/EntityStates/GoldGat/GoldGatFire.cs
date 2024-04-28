using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GoldGat
{
	// Token: 0x02000370 RID: 880
	public class GoldGatFire : BaseGoldGatState
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00046154 File Offset: 0x00044354
		public override void OnEnter()
		{
			base.OnEnter();
			this.loopSoundID = Util.PlaySound(GoldGatFire.windUpSoundString, base.gameObject);
			this.FireBullet();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00046178 File Offset: 0x00044378
		private void FireBullet()
		{
			this.body.SetAimTimer(2f);
			float t = Mathf.Clamp01(this.totalStopwatch / GoldGatFire.windUpDuration);
			this.fireFrequency = Mathf.Lerp(GoldGatFire.minFireFrequency, GoldGatFire.maxFireFrequency, t);
			float num = Mathf.Lerp(GoldGatFire.minSpread, GoldGatFire.maxSpread, t);
			Util.PlaySound(GoldGatFire.attackSoundString, base.gameObject);
			int num2 = (int)((float)GoldGatFire.baseMoneyCostPerBullet * (1f + (TeamManager.instance.GetTeamLevel(this.bodyMaster.teamIndex) - 1f) * 0.25f));
			if (base.isAuthority)
			{
				UnityEngine.Object aimOriginTransform = this.body.aimOriginTransform;
				if (this.gunChildLocator)
				{
					this.gunChildLocator.FindChild("Muzzle");
				}
				if (aimOriginTransform)
				{
					new BulletAttack
					{
						owner = this.networkedBodyAttachment.attachedBodyObject,
						aimVector = this.bodyInputBank.aimDirection,
						origin = this.bodyInputBank.aimOrigin,
						falloffModel = BulletAttack.FalloffModel.DefaultBullet,
						force = GoldGatFire.force,
						damage = this.body.damage * GoldGatFire.damageCoefficient,
						damageColorIndex = DamageColorIndex.Item,
						bulletCount = 1U,
						minSpread = 0f,
						maxSpread = num,
						tracerEffectPrefab = GoldGatFire.tracerEffectPrefab,
						isCrit = Util.CheckRoll(this.body.crit, this.bodyMaster),
						procCoefficient = GoldGatFire.procCoefficient,
						muzzleName = "Muzzle",
						weapon = base.gameObject
					}.Fire();
					Animator gunAnimator = this.gunAnimator;
					if (gunAnimator != null)
					{
						gunAnimator.Play("Fire");
					}
				}
			}
			if (NetworkServer.active)
			{
				this.bodyMaster.money = (uint)Mathf.Max(0f, (float)((ulong)this.bodyMaster.money - (ulong)((long)num2)));
			}
			Animator gunAnimator2 = this.gunAnimator;
			if (gunAnimator2 != null)
			{
				gunAnimator2.SetFloat("Crank.playbackRate", this.fireFrequency);
			}
			EffectManager.SimpleMuzzleFlash(GoldGatFire.muzzleFlashEffectPrefab, base.gameObject, "Muzzle", false);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00046394 File Offset: 0x00044594
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.totalStopwatch += Time.fixedDeltaTime;
			this.stopwatch += Time.fixedDeltaTime;
			AkSoundEngine.SetRTPCValueByPlayingID(GoldGatFire.windUpRTPC, Mathf.InverseLerp(GoldGatFire.minFireFrequency, GoldGatFire.maxFireFrequency, this.fireFrequency) * 100f, this.loopSoundID);
			if (base.CheckReturnToIdle())
			{
				return;
			}
			if (this.stopwatch > 1f / this.fireFrequency)
			{
				this.stopwatch = 0f;
				this.FireBullet();
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00046425 File Offset: 0x00044625
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.loopSoundID);
			base.OnExit();
		}

		// Token: 0x0400143D RID: 5181
		public static float minFireFrequency;

		// Token: 0x0400143E RID: 5182
		public static float maxFireFrequency;

		// Token: 0x0400143F RID: 5183
		public static float minSpread;

		// Token: 0x04001440 RID: 5184
		public static float maxSpread;

		// Token: 0x04001441 RID: 5185
		public static float windUpDuration;

		// Token: 0x04001442 RID: 5186
		public static float force;

		// Token: 0x04001443 RID: 5187
		public static float damageCoefficient;

		// Token: 0x04001444 RID: 5188
		public static float procCoefficient;

		// Token: 0x04001445 RID: 5189
		public static string attackSoundString;

		// Token: 0x04001446 RID: 5190
		public static GameObject tracerEffectPrefab;

		// Token: 0x04001447 RID: 5191
		public static GameObject impactEffectPrefab;

		// Token: 0x04001448 RID: 5192
		public static GameObject muzzleFlashEffectPrefab;

		// Token: 0x04001449 RID: 5193
		public static int baseMoneyCostPerBullet;

		// Token: 0x0400144A RID: 5194
		public static string windUpSoundString;

		// Token: 0x0400144B RID: 5195
		public static string windUpRTPC;

		// Token: 0x0400144C RID: 5196
		public float totalStopwatch;

		// Token: 0x0400144D RID: 5197
		private float stopwatch;

		// Token: 0x0400144E RID: 5198
		private float fireFrequency;

		// Token: 0x0400144F RID: 5199
		private uint loopSoundID;
	}
}
