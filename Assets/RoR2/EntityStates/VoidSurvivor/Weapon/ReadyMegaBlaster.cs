using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F0 RID: 240
	public class ReadyMegaBlaster : BaseSkillState
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00011D54 File Offset: 0x0000FF54
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.enterSoundString, base.gameObject);
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Transform transform = base.FindModelChild(this.muzzle);
			if (transform && this.chargeEffectPrefab)
			{
				this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeEffectInstance.transform.parent = transform;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011DDB File Offset: 0x0000FFDB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(3f);
			if (base.isAuthority && !base.IsKeyDownAuthority())
			{
				this.outer.SetNextState(new FireMegaBlasterBig());
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00011E13 File Offset: 0x00010013
		public override void OnExit()
		{
			Util.PlaySound(this.exitSoundString, base.gameObject);
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000461 RID: 1121
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04000462 RID: 1122
		[SerializeField]
		public string muzzle;

		// Token: 0x04000463 RID: 1123
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000464 RID: 1124
		[SerializeField]
		public string exitSoundString;

		// Token: 0x04000465 RID: 1125
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000466 RID: 1126
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000467 RID: 1127
		private GameObject chargeEffectInstance;
	}
}
