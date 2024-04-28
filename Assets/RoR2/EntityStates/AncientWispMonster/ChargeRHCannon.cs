using System;
using UnityEngine;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x02000498 RID: 1176
	public class ChargeRHCannon : BaseState
	{
		// Token: 0x06001513 RID: 5395 RVA: 0x0005D9F0 File Offset: 0x0005BBF0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeRHCannon.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.PlayAnimation("Gesture", "ChargeRHCannon", "ChargeRHCannon.playbackRate", this.duration);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component && ChargeRHCannon.effectPrefab)
				{
					Transform transform = component.FindChild("MuzzleRight");
					if (transform)
					{
						this.chargeEffectRight = UnityEngine.Object.Instantiate<GameObject>(ChargeRHCannon.effectPrefab, transform.position, transform.rotation);
						this.chargeEffectRight.transform.parent = transform;
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0005DAB9 File Offset: 0x0005BCB9
		public override void OnExit()
		{
			base.OnExit();
			EntityState.Destroy(this.chargeEffectLeft);
			EntityState.Destroy(this.chargeEffectRight);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0005DAD8 File Offset: 0x0005BCD8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireRHCannon nextState = new FireRHCannon();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001AEA RID: 6890
		public static float baseDuration = 3f;

		// Token: 0x04001AEB RID: 6891
		public static GameObject effectPrefab;

		// Token: 0x04001AEC RID: 6892
		private float duration;

		// Token: 0x04001AED RID: 6893
		private GameObject chargeEffectLeft;

		// Token: 0x04001AEE RID: 6894
		private GameObject chargeEffectRight;
	}
}
