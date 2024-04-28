using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Joint
{
	// Token: 0x02000145 RID: 325
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x0001854C File Offset: 0x0001674C
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleMuzzleFlash(this.joint1EffectPrefab, base.gameObject, this.joint1Name, false);
			EffectManager.SimpleMuzzleFlash(this.joint2EffectPrefab, base.gameObject, this.joint2Name, false);
			EffectManager.SimpleMuzzleFlash(this.joint3EffectPrefab, base.gameObject, this.joint3Name, false);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.characterModel = modelTransform.GetComponent<CharacterModel>();
			}
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount++;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000185E2 File Offset: 0x000167E2
		public override void OnExit()
		{
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount--;
			}
			base.OnExit();
		}

		// Token: 0x040006B5 RID: 1717
		[SerializeField]
		public string joint1Name;

		// Token: 0x040006B6 RID: 1718
		[SerializeField]
		public string joint2Name;

		// Token: 0x040006B7 RID: 1719
		[SerializeField]
		public string joint3Name;

		// Token: 0x040006B8 RID: 1720
		[SerializeField]
		public GameObject joint1EffectPrefab;

		// Token: 0x040006B9 RID: 1721
		[SerializeField]
		public GameObject joint2EffectPrefab;

		// Token: 0x040006BA RID: 1722
		[SerializeField]
		public GameObject joint3EffectPrefab;

		// Token: 0x040006BB RID: 1723
		private CharacterModel characterModel;
	}
}
