using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E2 RID: 738
	public class ChargeTrackingBomb : BaseState
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00037680 File Offset: 0x00035880
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = ChargeTrackingBomb.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.PlayCrossfade("Gesture, Override", "ChargeTrackingBomb", "ChargeTrackingBomb.playbackRate", this.duration, 0.3f);
			this.soundID = Util.PlayAttackSpeedSound(ChargeTrackingBomb.chargingSoundString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("TrackingBombMuzzle");
					if (transform && ChargeTrackingBomb.chargingEffectPrefab)
					{
						this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeTrackingBomb.chargingEffectPrefab, transform.position, transform.rotation);
						this.chargeEffectInstance.transform.parent = transform;
						this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.duration;
					}
				}
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003776D File Offset: 0x0003596D
		public override void OnExit()
		{
			base.OnExit();
			AkSoundEngine.StopPlayingID(this.soundID);
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00037798 File Offset: 0x00035998
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireTrackingBomb());
				return;
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x0400100D RID: 4109
		public static float baseDuration = 3f;

		// Token: 0x0400100E RID: 4110
		public static GameObject chargingEffectPrefab;

		// Token: 0x0400100F RID: 4111
		public static string chargingSoundString;

		// Token: 0x04001010 RID: 4112
		private float duration;

		// Token: 0x04001011 RID: 4113
		private float stopwatch;

		// Token: 0x04001012 RID: 4114
		private GameObject chargeEffectInstance;

		// Token: 0x04001013 RID: 4115
		private uint soundID;
	}
}
