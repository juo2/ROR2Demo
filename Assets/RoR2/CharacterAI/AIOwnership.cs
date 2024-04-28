using System;
using UnityEngine;

namespace RoR2.CharacterAI
{
	// Token: 0x02000C71 RID: 3185
	[RequireComponent(typeof(BaseAI))]
	[DisallowMultipleComponent]
	public class AIOwnership : MonoBehaviour
	{
		// Token: 0x060048D8 RID: 18648 RVA: 0x0012C08C File Offset: 0x0012A28C
		private void Awake()
		{
			this.baseAI = base.GetComponent<BaseAI>();
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0012C09A File Offset: 0x0012A29A
		private void FixedUpdate()
		{
			if (this.ownerMaster)
			{
				this.baseAI.leader.gameObject = this.ownerMaster.GetBodyObject();
			}
		}

		// Token: 0x04004589 RID: 17801
		public CharacterMaster ownerMaster;

		// Token: 0x0400458A RID: 17802
		private BaseAI baseAI;
	}
}
