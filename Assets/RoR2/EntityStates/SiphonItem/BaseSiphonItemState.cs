using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.SiphonItem
{
	// Token: 0x020001C8 RID: 456
	public class BaseSiphonItemState : BaseBodyAttachmentState
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00022B35 File Offset: 0x00020D35
		protected int GetItemStack()
		{
			if (!base.attachedBody || !base.attachedBody.inventory)
			{
				return 1;
			}
			return base.attachedBody.inventory.GetItemCount(RoR2Content.Items.SiphonOnLowHealth);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00022B6D File Offset: 0x00020D6D
		public override void OnEnter()
		{
			base.OnEnter();
			this.FXParticles = base.gameObject.GetComponentsInChildren<ParticleSystem>().ToList<ParticleSystem>();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00022B8C File Offset: 0x00020D8C
		public void TurnOffHealingFX()
		{
			for (int i = 0; i < this.FXParticles.Count; i++)
			{
				this.FXParticles[i].emission.enabled = false;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00022BCC File Offset: 0x00020DCC
		public void TurnOnHealingFX()
		{
			for (int i = 0; i < this.FXParticles.Count; i++)
			{
				this.FXParticles[i].emission.enabled = true;
			}
		}

		// Token: 0x040009A4 RID: 2468
		private List<ParticleSystem> FXParticles = new List<ParticleSystem>();
	}
}
