using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E1 RID: 737
	public class ChargeMegaNova : BaseState
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x0003742C File Offset: 0x0003562C
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = ChargeMegaNova.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.PlayCrossfade("Gesture, Override", "ChargeMegaNova", "ChargeMegaNova.playbackRate", this.duration, 0.3f);
			this.soundID = Util.PlayAttackSpeedSound(ChargeMegaNova.chargingSoundString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("HullCenter");
					Transform transform2 = component.FindChild("NovaCenter");
					if (transform && ChargeMegaNova.chargingEffectPrefab)
					{
						this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMegaNova.chargingEffectPrefab, transform.position, transform.rotation);
						this.chargeEffectInstance.transform.localScale = new Vector3(ChargeMegaNova.novaRadius, ChargeMegaNova.novaRadius, ChargeMegaNova.novaRadius);
						this.chargeEffectInstance.transform.parent = transform;
						this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.duration;
					}
					if (transform2 && ChargeMegaNova.areaIndicatorPrefab)
					{
						this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMegaNova.areaIndicatorPrefab, transform2.position, transform2.rotation);
						this.areaIndicatorInstance.transform.localScale = new Vector3(ChargeMegaNova.novaRadius * 2f, ChargeMegaNova.novaRadius * 2f, ChargeMegaNova.novaRadius * 2f);
						this.areaIndicatorInstance.transform.parent = transform2;
					}
				}
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000375C8 File Offset: 0x000357C8
		public override void OnExit()
		{
			base.OnExit();
			AkSoundEngine.StopPlayingID(this.soundID);
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			if (this.areaIndicatorInstance)
			{
				EntityState.Destroy(this.areaIndicatorInstance);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00037618 File Offset: 0x00035818
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				FireMegaNova fireMegaNova = new FireMegaNova();
				fireMegaNova.novaRadius = ChargeMegaNova.novaRadius;
				this.outer.SetNextState(fireMegaNova);
				return;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04001003 RID: 4099
		public static float baseDuration = 3f;

		// Token: 0x04001004 RID: 4100
		public static GameObject chargingEffectPrefab;

		// Token: 0x04001005 RID: 4101
		public static GameObject areaIndicatorPrefab;

		// Token: 0x04001006 RID: 4102
		public static string chargingSoundString;

		// Token: 0x04001007 RID: 4103
		public static float novaRadius;

		// Token: 0x04001008 RID: 4104
		private float duration;

		// Token: 0x04001009 RID: 4105
		private float stopwatch;

		// Token: 0x0400100A RID: 4106
		private GameObject chargeEffectInstance;

		// Token: 0x0400100B RID: 4107
		private GameObject areaIndicatorInstance;

		// Token: 0x0400100C RID: 4108
		private uint soundID;
	}
}
