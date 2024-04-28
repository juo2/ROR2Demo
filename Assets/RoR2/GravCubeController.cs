using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000715 RID: 1813
	[RequireComponent(typeof(ItemFollower))]
	public class GravCubeController : MonoBehaviour
	{
		// Token: 0x0600256B RID: 9579 RVA: 0x000A2057 File Offset: 0x000A0257
		private void Start()
		{
			this.itemFollower = base.GetComponent<ItemFollower>();
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000A2065 File Offset: 0x000A0265
		public void ActivateCube(float duration)
		{
			this.activeTimer = duration;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000A2070 File Offset: 0x000A0270
		private void Update()
		{
			if (this.itemFollower && this.itemFollower.followerInstance)
			{
				if (!this.itemFollowerAnimator)
				{
					this.itemFollowerAnimator = this.itemFollower.followerInstance.GetComponentInChildren<Animator>();
				}
				this.activeTimer -= Time.deltaTime;
				this.itemFollowerAnimator.SetBool("active", this.activeTimer > 0f);
			}
		}

		// Token: 0x04002949 RID: 10569
		private ItemFollower itemFollower;

		// Token: 0x0400294A RID: 10570
		private float activeTimer;

		// Token: 0x0400294B RID: 10571
		private Animator itemFollowerAnimator;
	}
}
