using System;
using UnityEngine;

namespace EntityStates.ImpMonster
{
	// Token: 0x0200030D RID: 781
	public class ChargeSpines : BaseState
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003B954 File Offset: 0x00039B54
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeSpines.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleMouth");
					if (transform && ChargeSpines.effectPrefab)
					{
						this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(ChargeSpines.effectPrefab, transform.position, transform.rotation);
						this.chargeEffect.transform.parent = transform;
					}
				}
			}
			base.PlayAnimation("Gesture", "ChargeSpines", "ChargeSpines.playbackRate", this.duration);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003B9FF File Offset: 0x00039BFF
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003BA20 File Offset: 0x00039C20
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireSpines nextState = new FireSpines();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001151 RID: 4433
		public static float baseDuration = 1f;

		// Token: 0x04001152 RID: 4434
		public static GameObject effectPrefab;

		// Token: 0x04001153 RID: 4435
		private float duration;

		// Token: 0x04001154 RID: 4436
		private GameObject chargeEffect;
	}
}
