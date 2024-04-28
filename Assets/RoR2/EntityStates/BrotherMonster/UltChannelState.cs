using System;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200044D RID: 1101
	public class UltChannelState : BaseState
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x000576B8 File Offset: 0x000558B8
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(UltChannelState.enterSoundString, base.gameObject);
			Transform transform = base.FindModelChild("MuzzleUlt");
			if (transform && UltChannelState.channelEffectPrefab)
			{
				this.channelEffectInstance = UnityEngine.Object.Instantiate<GameObject>(UltChannelState.channelEffectPrefab, transform.position, Quaternion.identity, transform);
			}
			if (UltChannelState.channelBeginMuzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(UltChannelState.channelBeginMuzzleflashEffectPrefab, base.gameObject, "MuzzleUlt", false);
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0005773C File Offset: 0x0005593C
		private void FireWave()
		{
			this.wavesFired++;
			float num = 360f / (float)UltChannelState.waveProjectileCount;
			Vector3 normalized = Vector3.ProjectOnPlane(UnityEngine.Random.onUnitSphere, Vector3.up).normalized;
			Vector3 footPosition = base.characterBody.footPosition;
			GameObject prefab = UltChannelState.waveProjectileLeftPrefab;
			if (UnityEngine.Random.value <= 0.5f)
			{
				prefab = UltChannelState.waveProjectileRightPrefab;
			}
			for (int i = 0; i < UltChannelState.waveProjectileCount; i++)
			{
				Vector3 forward = Quaternion.AngleAxis(num * (float)i, Vector3.up) * normalized;
				ProjectileManager.instance.FireProjectile(prefab, footPosition, Util.QuaternionSafeLookRotation(forward), base.gameObject, base.characterBody.damage * UltChannelState.waveProjectileDamageCoefficient, UltChannelState.waveProjectileForce, Util.CheckRoll(base.characterBody.crit, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00057820 File Offset: 0x00055A20
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (Mathf.CeilToInt(base.fixedAge / UltChannelState.maxDuration * (float)UltChannelState.totalWaves) > this.wavesFired)
				{
					this.FireWave();
				}
				if (base.fixedAge > UltChannelState.maxDuration)
				{
					this.outer.SetNextState(new UltExitState());
				}
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0005787E File Offset: 0x00055A7E
		public override void OnExit()
		{
			Util.PlaySound(UltChannelState.exitSoundString, base.gameObject);
			if (this.channelEffectInstance)
			{
				EntityState.Destroy(this.channelEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x0400190F RID: 6415
		public static GameObject waveProjectileLeftPrefab;

		// Token: 0x04001910 RID: 6416
		public static GameObject waveProjectileRightPrefab;

		// Token: 0x04001911 RID: 6417
		public static int waveProjectileCount;

		// Token: 0x04001912 RID: 6418
		public static float waveProjectileDamageCoefficient;

		// Token: 0x04001913 RID: 6419
		public static float waveProjectileForce;

		// Token: 0x04001914 RID: 6420
		public static int totalWaves;

		// Token: 0x04001915 RID: 6421
		public static float maxDuration;

		// Token: 0x04001916 RID: 6422
		public static GameObject channelBeginMuzzleflashEffectPrefab;

		// Token: 0x04001917 RID: 6423
		public static GameObject channelEffectPrefab;

		// Token: 0x04001918 RID: 6424
		public static string enterSoundString;

		// Token: 0x04001919 RID: 6425
		public static string exitSoundString;

		// Token: 0x0400191A RID: 6426
		private GameObject channelEffectInstance;

		// Token: 0x0400191B RID: 6427
		public static SkillDef replacementSkillDef;

		// Token: 0x0400191C RID: 6428
		private int wavesFired;
	}
}
