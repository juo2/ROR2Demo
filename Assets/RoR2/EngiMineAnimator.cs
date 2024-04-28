using System;
using EntityStates.Engi.Mine;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006BE RID: 1726
	public class EngiMineAnimator : MonoBehaviour
	{
		// Token: 0x0600219A RID: 8602 RVA: 0x00090968 File Offset: 0x0008EB68
		private void Start()
		{
			ProjectileGhostController component = base.GetComponent<ProjectileGhostController>();
			if (component)
			{
				this.projectileTransform = component.authorityTransform;
				if (this.projectileTransform)
				{
					this.armingStateMachine = EntityStateMachine.FindByCustomName(this.projectileTransform.gameObject, "Arming");
				}
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000909B8 File Offset: 0x0008EBB8
		private bool IsArmed()
		{
			EntityStateMachine entityStateMachine = this.armingStateMachine;
			BaseMineArmingState baseMineArmingState = ((entityStateMachine != null) ? entityStateMachine.state : null) as BaseMineArmingState;
			return ((baseMineArmingState != null) ? baseMineArmingState.damageScale : 0f) > 1f;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000909E8 File Offset: 0x0008EBE8
		private void Update()
		{
			if (this.IsArmed())
			{
				this.animator.SetTrigger("Arming");
			}
		}

		// Token: 0x040026FE RID: 9982
		private Transform projectileTransform;

		// Token: 0x040026FF RID: 9983
		public Animator animator;

		// Token: 0x04002700 RID: 9984
		private EntityStateMachine armingStateMachine;
	}
}
