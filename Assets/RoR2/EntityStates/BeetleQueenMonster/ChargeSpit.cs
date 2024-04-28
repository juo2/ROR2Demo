using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000465 RID: 1125
	public class ChargeSpit : BaseState
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x00059770 File Offset: 0x00057970
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeSpit.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.PlayCrossfade("Gesture", "ChargeSpit", "ChargeSpit.playbackRate", this.duration, 0.2f);
			Util.PlaySound(ChargeSpit.attackSoundString, base.gameObject);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component && ChargeSpit.effectPrefab)
				{
					Transform transform = component.FindChild("Mouth");
					if (transform)
					{
						this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(ChargeSpit.effectPrefab, transform.position, transform.rotation);
						this.chargeEffect.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeEffect.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
				}
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00059851 File Offset: 0x00057A51
		public override void OnExit()
		{
			base.OnExit();
			EntityState.Destroy(this.chargeEffect);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00059864 File Offset: 0x00057A64
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireSpit nextState = new FireSpit();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040019C0 RID: 6592
		public static float baseDuration = 3f;

		// Token: 0x040019C1 RID: 6593
		public static GameObject effectPrefab;

		// Token: 0x040019C2 RID: 6594
		public static string attackSoundString;

		// Token: 0x040019C3 RID: 6595
		private float duration;

		// Token: 0x040019C4 RID: 6596
		private GameObject chargeEffect;
	}
}
