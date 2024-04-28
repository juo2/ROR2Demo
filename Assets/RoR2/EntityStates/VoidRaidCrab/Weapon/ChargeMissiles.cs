using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000132 RID: 306
	public class ChargeMissiles : BaseState
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x00017460 File Offset: 0x00015660
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator && this.chargeEffectPrefab)
			{
				Transform transform = modelChildLocator.FindChild(this.muzzleName) ?? base.characterBody.coreTransform;
				if (transform)
				{
					this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
					this.chargeEffectInstance.transform.parent = transform;
				}
			}
			if (!string.IsNullOrEmpty(this.enterSoundString))
			{
				if (this.isSoundScaledByAttackSpeed)
				{
					Util.PlayAttackSpeedSound(this.enterSoundString, base.gameObject, this.attackSpeedStat);
					return;
				}
				Util.PlaySound(this.enterSoundString, base.gameObject);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00017552 File Offset: 0x00015752
		public override void OnExit()
		{
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00017565 File Offset: 0x00015765
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new FireMissiles());
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400065E RID: 1630
		[SerializeField]
		public float baseDuration;

		// Token: 0x0400065F RID: 1631
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04000660 RID: 1632
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000661 RID: 1633
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000662 RID: 1634
		[SerializeField]
		public bool isSoundScaledByAttackSpeed;

		// Token: 0x04000663 RID: 1635
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000664 RID: 1636
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000665 RID: 1637
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000666 RID: 1638
		private float duration;

		// Token: 0x04000667 RID: 1639
		private GameObject chargeEffectInstance;
	}
}
