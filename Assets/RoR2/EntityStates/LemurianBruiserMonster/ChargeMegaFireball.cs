using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LemurianBruiserMonster
{
	// Token: 0x020002CE RID: 718
	public class ChargeMegaFireball : BaseState
	{
		// Token: 0x06000CBF RID: 3263 RVA: 0x00035A28 File Offset: 0x00033C28
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeMegaFireball.baseDuration / this.attackSpeedStat;
			UnityEngine.Object modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(ChargeMegaFireball.attackString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleMouth");
					if (transform && ChargeMegaFireball.chargeEffectPrefab)
					{
						this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMegaFireball.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeInstance.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
				}
			}
			if (modelAnimator)
			{
				base.PlayCrossfade("Gesture, Additive", "ChargeMegaFireball", "ChargeMegaFireball.playbackRate", this.duration, 0.1f);
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00035B1C File Offset: 0x00033D1C
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00035B3C File Offset: 0x00033D3C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireMegaFireball nextState = new FireMegaFireball();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000F8A RID: 3978
		public static float baseDuration = 1f;

		// Token: 0x04000F8B RID: 3979
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000F8C RID: 3980
		public static string attackString;

		// Token: 0x04000F8D RID: 3981
		private float duration;

		// Token: 0x04000F8E RID: 3982
		private GameObject chargeInstance;
	}
}
