using System;
using EntityStates.Engi.Mine;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B77 RID: 2935
	[RequireComponent(typeof(ProjectileGhostController))]
	public class EngiMineGhostController : MonoBehaviour
	{
		// Token: 0x060042DE RID: 17118 RVA: 0x0011524A File Offset: 0x0011344A
		private void Awake()
		{
			this.projectileGhostController = base.GetComponent<ProjectileGhostController>();
			this.stickIndicator.SetActive(false);
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00115264 File Offset: 0x00113464
		private void Start()
		{
			Transform authorityTransform = this.projectileGhostController.authorityTransform;
			if (authorityTransform)
			{
				this.armingStateMachine = EntityStateMachine.FindByCustomName(authorityTransform.gameObject, "Arming");
			}
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0011529B File Offset: 0x0011349B
		private bool IsArmed()
		{
			EntityStateMachine entityStateMachine = this.armingStateMachine;
			BaseMineArmingState baseMineArmingState = ((entityStateMachine != null) ? entityStateMachine.state : null) as BaseMineArmingState;
			return ((baseMineArmingState != null) ? baseMineArmingState.damageScale : 0f) > 1f;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x001152CC File Offset: 0x001134CC
		private void FixedUpdate()
		{
			bool flag = this.IsArmed();
			if (flag != this.cachedArmed)
			{
				this.cachedArmed = flag;
				this.stickIndicator.SetActive(flag);
			}
		}

		// Token: 0x040040E0 RID: 16608
		private ProjectileGhostController projectileGhostController;

		// Token: 0x040040E1 RID: 16609
		private EntityStateMachine armingStateMachine;

		// Token: 0x040040E2 RID: 16610
		[Tooltip("Child object which will be enabled if the projectile is armed.")]
		public GameObject stickIndicator;

		// Token: 0x040040E3 RID: 16611
		private bool cachedArmed;
	}
}
