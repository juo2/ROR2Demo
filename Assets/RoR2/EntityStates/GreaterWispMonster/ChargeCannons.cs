using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GreaterWispMonster
{
	// Token: 0x02000344 RID: 836
	public class ChargeCannons : BaseState
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x00040730 File Offset: 0x0003E930
		public override void OnEnter()
		{
			base.OnEnter();
			this.soundID = Util.PlayAttackSpeedSound(this.attackString, base.gameObject, this.attackSpeedStat * (2f / this.baseDuration));
			this.duration = this.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.GetModelAnimator();
			base.PlayAnimation("Gesture", "ChargeCannons", "ChargeCannons.playbackRate", this.duration);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component && this.effectPrefab)
				{
					Transform transform = component.FindChild("MuzzleLeft");
					Transform transform2 = component.FindChild("MuzzleRight");
					if (transform)
					{
						this.chargeEffectLeft = UnityEngine.Object.Instantiate<GameObject>(this.effectPrefab, transform.position, transform.rotation);
						this.chargeEffectLeft.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeEffectLeft.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
					if (transform2)
					{
						this.chargeEffectRight = UnityEngine.Object.Instantiate<GameObject>(this.effectPrefab, transform2.position, transform2.rotation);
						this.chargeEffectRight.transform.parent = transform2;
						ScaleParticleSystemDuration component3 = this.chargeEffectRight.GetComponent<ScaleParticleSystemDuration>();
						if (component3)
						{
							component3.newDuration = this.duration;
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000408C0 File Offset: 0x0003EAC0
		public override void OnExit()
		{
			if (base.fixedAge < this.duration - 0.1f)
			{
				AkSoundEngine.StopPlayingID(this.soundID);
			}
			this.PlayAnimation("Gesture", "Empty");
			EntityState.Destroy(this.chargeEffectLeft);
			EntityState.Destroy(this.chargeEffectRight);
			base.OnExit();
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00040918 File Offset: 0x0003EB18
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireCannons nextState = new FireCannons();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040012B1 RID: 4785
		[SerializeField]
		public float baseDuration = 3f;

		// Token: 0x040012B2 RID: 4786
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x040012B3 RID: 4787
		[SerializeField]
		public string attackString;

		// Token: 0x040012B4 RID: 4788
		protected float duration;

		// Token: 0x040012B5 RID: 4789
		private GameObject chargeEffectLeft;

		// Token: 0x040012B6 RID: 4790
		private GameObject chargeEffectRight;

		// Token: 0x040012B7 RID: 4791
		private const float soundDuration = 2f;

		// Token: 0x040012B8 RID: 4792
		private uint soundID;
	}
}
