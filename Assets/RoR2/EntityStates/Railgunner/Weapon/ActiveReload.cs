using System;
using EntityStates.Railgunner.Reload;
using RoR2;
using UnityEngine;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001EE RID: 494
	public class ActiveReload : BaseState
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x00025098 File Offset: 0x00023298
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration;
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Reload");
			if (entityStateMachine)
			{
				Reloading reloading = entityStateMachine.state as Reloading;
				if (reloading != null)
				{
					reloading.AttemptBoost();
				}
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00025116 File Offset: 0x00023316
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000A51 RID: 2641
		private const string reloadStateMachineName = "Reload";

		// Token: 0x04000A52 RID: 2642
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000A53 RID: 2643
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000A54 RID: 2644
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000A55 RID: 2645
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000A56 RID: 2646
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000A57 RID: 2647
		private float duration;
	}
}
