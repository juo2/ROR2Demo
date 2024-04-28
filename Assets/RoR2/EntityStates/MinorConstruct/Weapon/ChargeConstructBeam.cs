using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MinorConstruct.Weapon
{
	// Token: 0x0200026D RID: 621
	public class ChargeConstructBeam : BaseState
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x0002C49C File Offset: 0x0002A69C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			Transform transform = base.FindModelChild(this.chargeEffectMuzzle);
			if (transform && this.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeInstance.transform.parent = base.gameObject.transform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002C56C File Offset: 0x0002A76C
		public override void Update()
		{
			base.Update();
			if (this.chargeInstance)
			{
				Ray aimRay = base.GetAimRay();
				this.chargeInstance.transform.forward = aimRay.direction;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002C5AA File Offset: 0x0002A7AA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new FireConstructBeam());
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
		public override void OnExit()
		{
			Util.PlaySound(this.exitSoundString, base.gameObject);
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000C5B RID: 3163
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000C5C RID: 3164
		[SerializeField]
		public string exitSoundString;

		// Token: 0x04000C5D RID: 3165
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000C5E RID: 3166
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000C5F RID: 3167
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000C60 RID: 3168
		[SerializeField]
		public string chargeEffectMuzzle;

		// Token: 0x04000C61 RID: 3169
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04000C62 RID: 3170
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000C63 RID: 3171
		private float duration;

		// Token: 0x04000C64 RID: 3172
		private GameObject chargeInstance;
	}
}
