using System;
using EntityStates.SurvivorPod;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivorPod
{
	// Token: 0x020000E6 RID: 230
	public class Descent : SurvivorPodBaseState
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x000111B3 File Offset: 0x0000F3B3
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000111DF File Offset: 0x0000F3DF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.duration < base.fixedAge)
			{
				this.TransitionIntoNextState();
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00011203 File Offset: 0x0000F403
		protected virtual void TransitionIntoNextState()
		{
			this.outer.SetNextState(new Landed());
		}

		// Token: 0x04000428 RID: 1064
		[SerializeField]
		public float duration;

		// Token: 0x04000429 RID: 1065
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400042A RID: 1066
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400042B RID: 1067
		[SerializeField]
		public string enterSoundString;
	}
}
