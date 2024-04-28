using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarWisp
{
	// Token: 0x020002AB RID: 683
	public class ChargeLunarGuns : BaseState
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x00032B70 File Offset: 0x00030D70
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = (ChargeLunarGuns.baseDuration + ChargeLunarGuns.spinUpDuration) / this.attackSpeedStat;
			this.muzzleTransformRoot = base.FindModelChild(ChargeLunarGuns.muzzleNameRoot);
			this.muzzleTransformOne = base.FindModelChild(ChargeLunarGuns.muzzleNameOne);
			this.muzzleTransformTwo = base.FindModelChild(ChargeLunarGuns.muzzleNameTwo);
			this.loopedSoundID = Util.PlaySound(ChargeLunarGuns.windUpSound, base.gameObject);
			base.PlayCrossfade("Gesture", "MinigunSpinUp", 0.2f);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00032C18 File Offset: 0x00030E18
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.StartAimMode(0.5f, false);
			if (base.fixedAge >= ChargeLunarGuns.chargeEffectDelay && !this.chargeEffectSpawned)
			{
				this.chargeEffectSpawned = true;
				if (this.muzzleTransformOne && this.muzzleTransformTwo && ChargeLunarGuns.chargeEffectPrefab)
				{
					this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeLunarGuns.chargeEffectPrefab, this.muzzleTransformOne.position, this.muzzleTransformOne.rotation);
					this.chargeInstance.transform.parent = this.muzzleTransformOne;
					this.chargeInstanceTwo = UnityEngine.Object.Instantiate<GameObject>(ChargeLunarGuns.chargeEffectPrefab, this.muzzleTransformTwo.position, this.muzzleTransformTwo.rotation);
					this.chargeInstanceTwo.transform.parent = this.muzzleTransformTwo;
					ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = this.duration;
					}
				}
			}
			if (base.fixedAge >= ChargeLunarGuns.spinUpDuration && !this.upToSpeed)
			{
				this.upToSpeed = true;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireLunarGuns fireLunarGuns = new FireLunarGuns();
				fireLunarGuns.muzzleTransformOne = this.muzzleTransformOne;
				fireLunarGuns.muzzleTransformTwo = this.muzzleTransformTwo;
				fireLunarGuns.muzzleNameOne = ChargeLunarGuns.muzzleNameOne;
				fireLunarGuns.muzzleNameTwo = ChargeLunarGuns.muzzleNameTwo;
				this.outer.SetNextState(fireLunarGuns);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00032D94 File Offset: 0x00030F94
		public override void OnExit()
		{
			base.OnExit();
			AkSoundEngine.StopPlayingID(this.loopedSoundID);
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
			if (this.chargeInstanceTwo)
			{
				EntityState.Destroy(this.chargeInstanceTwo);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000E99 RID: 3737
		public static string muzzleNameRoot;

		// Token: 0x04000E9A RID: 3738
		public static string muzzleNameOne;

		// Token: 0x04000E9B RID: 3739
		public static string muzzleNameTwo;

		// Token: 0x04000E9C RID: 3740
		public static float baseDuration;

		// Token: 0x04000E9D RID: 3741
		public static string windUpSound;

		// Token: 0x04000E9E RID: 3742
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000E9F RID: 3743
		private GameObject chargeInstance;

		// Token: 0x04000EA0 RID: 3744
		private GameObject chargeInstanceTwo;

		// Token: 0x04000EA1 RID: 3745
		private float duration;

		// Token: 0x04000EA2 RID: 3746
		public static float spinUpDuration;

		// Token: 0x04000EA3 RID: 3747
		public static float chargeEffectDelay;

		// Token: 0x04000EA4 RID: 3748
		private bool chargeEffectSpawned;

		// Token: 0x04000EA5 RID: 3749
		private bool upToSpeed;

		// Token: 0x04000EA6 RID: 3750
		private uint loopedSoundID;

		// Token: 0x04000EA7 RID: 3751
		protected Transform muzzleTransformRoot;

		// Token: 0x04000EA8 RID: 3752
		protected Transform muzzleTransformOne;

		// Token: 0x04000EA9 RID: 3753
		protected Transform muzzleTransformTwo;
	}
}
