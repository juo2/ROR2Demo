using System;
using EntityStates.Mage.Weapon;
using UnityEngine;

namespace EntityStates.GlobalSkills.LunarNeedle
{
	// Token: 0x02000372 RID: 882
	public class ChargeLunarSecondary : BaseChargeBombState
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x000465D5 File Offset: 0x000447D5
		protected override BaseThrowBombState GetNextState()
		{
			return new ThrowLunarSecondary();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000465DC File Offset: 0x000447DC
		protected override void PlayChargeAnimation()
		{
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.playbackRateParam, base.duration);
		}

		// Token: 0x0400145A RID: 5210
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400145B RID: 5211
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400145C RID: 5212
		[SerializeField]
		public string playbackRateParam;
	}
}
