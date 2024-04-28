using System;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000137 RID: 311
	public class FireMultiBeamSmall : BaseFireMultiBeam
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x00017D95 File Offset: 0x00015F95
		protected override EntityState InstantiateNextState()
		{
			if (this.fireIndex < this.numFireBeforeFinale - 1)
			{
				return new FireMultiBeamSmall
				{
					fireIndex = this.fireIndex + 1
				};
			}
			return new FireMultiBeamFinale();
		}

		// Token: 0x040006A3 RID: 1699
		[SerializeField]
		public int numFireBeforeFinale;

		// Token: 0x040006A4 RID: 1700
		private int fireIndex;
	}
}
