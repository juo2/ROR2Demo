using System;
using UnityEngine;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000464 RID: 1124
	public class BeginBurrow : BaseState
	{
		// Token: 0x0600140F RID: 5135 RVA: 0x00059684 File Offset: 0x00057884
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			base.PlayCrossfade("Body", "BeginBurrow", "BeginBurrow.playbackRate", BeginBurrow.duration, 0.5f);
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000596E0 File Offset: 0x000578E0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			bool flag = this.animator.GetFloat("BeginBurrow.active") > 0.9f;
			if (flag && !this.isBurrowing)
			{
				string childName = "BurrowCenter";
				Transform transform = this.childLocator.FindChild(childName);
				if (transform)
				{
					UnityEngine.Object.Instantiate<GameObject>(BeginBurrow.burrowPrefab, transform.position, Quaternion.identity);
				}
			}
			this.isBurrowing = flag;
			if (base.fixedAge >= BeginBurrow.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040019BA RID: 6586
		public static GameObject burrowPrefab;

		// Token: 0x040019BB RID: 6587
		public static float duration;

		// Token: 0x040019BC RID: 6588
		private bool isBurrowing;

		// Token: 0x040019BD RID: 6589
		private Animator animator;

		// Token: 0x040019BE RID: 6590
		private Transform modelTransform;

		// Token: 0x040019BF RID: 6591
		private ChildLocator childLocator;
	}
}
