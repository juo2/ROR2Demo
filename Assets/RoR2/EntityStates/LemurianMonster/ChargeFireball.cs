using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LemurianMonster
{
	// Token: 0x020002D4 RID: 724
	public class ChargeFireball : BaseState
	{
		// Token: 0x06000CE0 RID: 3296 RVA: 0x00036510 File Offset: 0x00034710
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeFireball.baseDuration / this.attackSpeedStat;
			base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(ChargeFireball.attackString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleMouth");
					if (transform && ChargeFireball.chargeVfxPrefab)
					{
						this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFireball.chargeVfxPrefab, transform.position, transform.rotation);
						this.chargeVfxInstance.transform.parent = transform;
					}
				}
			}
			base.PlayAnimation("Gesture", "ChargeFireball", "ChargeFireball.playbackRate", this.duration);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000365D9 File Offset: 0x000347D9
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeVfxInstance)
			{
				EntityState.Destroy(this.chargeVfxInstance);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000365FC File Offset: 0x000347FC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireFireball nextState = new FireFireball();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000FC8 RID: 4040
		public static float baseDuration = 1f;

		// Token: 0x04000FC9 RID: 4041
		public static GameObject chargeVfxPrefab;

		// Token: 0x04000FCA RID: 4042
		public static string attackString;

		// Token: 0x04000FCB RID: 4043
		private float duration;

		// Token: 0x04000FCC RID: 4044
		private GameObject chargeVfxInstance;
	}
}
