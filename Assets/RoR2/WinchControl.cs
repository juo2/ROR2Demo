using System;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000903 RID: 2307
	public class WinchControl : MonoBehaviour
	{
		// Token: 0x06003420 RID: 13344 RVA: 0x000DB9CA File Offset: 0x000D9BCA
		private void Start()
		{
			this.attachmentTransform = this.FindAttachmentTransform();
			if (this.attachmentTransform)
			{
				this.tailTransform.position = this.attachmentTransform.position;
			}
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000DB9FB File Offset: 0x000D9BFB
		private void Update()
		{
			if (!this.attachmentTransform)
			{
				this.attachmentTransform = this.FindAttachmentTransform();
			}
			if (this.attachmentTransform)
			{
				this.tailTransform.position = this.attachmentTransform.position;
			}
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000DBA3C File Offset: 0x000D9C3C
		private Transform FindAttachmentTransform()
		{
			this.projectileGhostController = base.GetComponent<ProjectileGhostController>();
			if (this.projectileGhostController)
			{
				Transform authorityTransform = this.projectileGhostController.authorityTransform;
				if (authorityTransform)
				{
					ProjectileController component = authorityTransform.GetComponent<ProjectileController>();
					if (component)
					{
						GameObject owner = component.owner;
						if (owner)
						{
							ModelLocator component2 = owner.GetComponent<ModelLocator>();
							if (component2)
							{
								Transform modelTransform = component2.modelTransform;
								if (modelTransform)
								{
									ChildLocator component3 = modelTransform.GetComponent<ChildLocator>();
									if (component3)
									{
										return component3.FindChild(this.attachmentString);
									}
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04003517 RID: 13591
		public Transform tailTransform;

		// Token: 0x04003518 RID: 13592
		public string attachmentString;

		// Token: 0x04003519 RID: 13593
		private ProjectileGhostController projectileGhostController;

		// Token: 0x0400351A RID: 13594
		private Transform attachmentTransform;
	}
}
