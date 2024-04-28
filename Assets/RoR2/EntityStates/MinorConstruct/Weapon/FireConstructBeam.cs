using System;
using UnityEngine;

namespace EntityStates.MinorConstruct.Weapon
{
	// Token: 0x0200026E RID: 622
	public class FireConstructBeam : GenericProjectileBaseState
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002C602 File Offset: 0x0002A802
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000C65 RID: 3173
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000C66 RID: 3174
		[SerializeField]
		public string animationStateName;
	}
}
