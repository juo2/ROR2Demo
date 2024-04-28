using System;
using UnityEngine;

namespace EntityStates.GrandParent
{
	// Token: 0x02000359 RID: 857
	public abstract class ChannelSunBase : BaseSkillState
	{
		// Token: 0x06000F6C RID: 3948 RVA: 0x000438A4 File Offset: 0x00041AA4
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.handVfxPrefab)
			{
				ChildLocator modelChildLocator = base.GetModelChildLocator();
				if (modelChildLocator)
				{
					this.CreateVfxInstanceForHand(modelChildLocator, ChannelSunBase.leftHandVfxTargetNameInChildLocator, ref this.leftHandVfxInstance);
					this.CreateVfxInstanceForHand(modelChildLocator, ChannelSunBase.rightHandVfxTargetNameInChildLocator, ref this.rightHandVfxInstance);
				}
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000438F7 File Offset: 0x00041AF7
		public override void OnExit()
		{
			this.DestroyVfxInstance(ref this.leftHandVfxInstance);
			this.DestroyVfxInstance(ref this.rightHandVfxInstance);
			base.OnExit();
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00043918 File Offset: 0x00041B18
		protected void CreateVfxInstanceForHand(ChildLocator childLocator, string nameInChildLocator, ref GameObject dest)
		{
			Transform transform = childLocator.FindChild(nameInChildLocator);
			if (transform)
			{
				dest = UnityEngine.Object.Instantiate<GameObject>(this.handVfxPrefab, transform);
				return;
			}
			dest = null;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00043947 File Offset: 0x00041B47
		protected void DestroyVfxInstance(ref GameObject vfxInstance)
		{
			EntityState.Destroy(vfxInstance);
			vfxInstance = null;
		}

		// Token: 0x04001386 RID: 4998
		[SerializeField]
		public GameObject handVfxPrefab;

		// Token: 0x04001387 RID: 4999
		public static string leftHandVfxTargetNameInChildLocator;

		// Token: 0x04001388 RID: 5000
		public static string rightHandVfxTargetNameInChildLocator;

		// Token: 0x04001389 RID: 5001
		private GameObject leftHandVfxInstance;

		// Token: 0x0400138A RID: 5002
		private GameObject rightHandVfxInstance;
	}
}
