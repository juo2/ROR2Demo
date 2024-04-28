using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MinorConstruct
{
	// Token: 0x02000269 RID: 617
	public class NoCastSpawn : BaseState
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002C31E File Offset: 0x0002A51E
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			this.outer.SetNextStateToMain();
		}

		// Token: 0x04000C53 RID: 3155
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000C54 RID: 3156
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000C55 RID: 3157
		[SerializeField]
		public string enterSoundString;
	}
}
