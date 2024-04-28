using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MajorConstruct
{
	// Token: 0x0200028A RID: 650
	public class Death : GenericCharacterDeath
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002FE28 File Offset: 0x0002E028
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002FE3C File Offset: 0x0002E03C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.beginEffect)
			{
				EffectManager.SimpleMuzzleFlash(this.beginEffect, base.gameObject, this.beginMuzzleName, false);
			}
			base.FindModelChild("Collision").gameObject.SetActive(false);
			MasterSpawnSlotController component = base.GetComponent<MasterSpawnSlotController>();
			if (NetworkServer.active)
			{
				component;
			}
		}

		// Token: 0x04000D84 RID: 3460
		[SerializeField]
		public float duration;

		// Token: 0x04000D85 RID: 3461
		[SerializeField]
		public GameObject beginEffect;

		// Token: 0x04000D86 RID: 3462
		[SerializeField]
		public string beginMuzzleName;

		// Token: 0x04000D87 RID: 3463
		[SerializeField]
		public GameObject padEffect;

		// Token: 0x04000D88 RID: 3464
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000D89 RID: 3465
		[SerializeField]
		public string animationStateName;
	}
}
