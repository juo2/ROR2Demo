using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GoldGat
{
	// Token: 0x0200036E RID: 878
	public class BaseGoldGatState : EntityState
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00045E4C File Offset: 0x0004404C
		public override void OnEnter()
		{
			base.OnEnter();
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			if (this.networkedBodyAttachment)
			{
				this.bodyGameObject = this.networkedBodyAttachment.attachedBodyObject;
				this.body = this.networkedBodyAttachment.attachedBody;
				if (this.bodyGameObject)
				{
					this.bodyMaster = this.body.master;
					this.bodyInputBank = this.bodyGameObject.GetComponent<InputBankTest>();
					this.bodyEquipmentSlot = this.body.equipmentSlot;
					ModelLocator component = this.body.GetComponent<ModelLocator>();
					if (component)
					{
						this.bodyAimAnimator = component.modelTransform.GetComponent<AimAnimator>();
					}
					this.LinkToDisplay();
				}
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00045F08 File Offset: 0x00044108
		private void LinkToDisplay()
		{
			if (this.linkedToDisplay)
			{
				return;
			}
			if (this.bodyEquipmentSlot)
			{
				this.gunTransform = this.bodyEquipmentSlot.FindActiveEquipmentDisplay();
				if (this.gunTransform)
				{
					this.gunChildLocator = this.gunTransform.GetComponentInChildren<ChildLocator>();
					if (this.gunChildLocator && base.modelLocator)
					{
						base.modelLocator.modelTransform = this.gunChildLocator.transform;
						this.gunAnimator = base.GetModelAnimator();
						this.linkedToDisplay = true;
					}
				}
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00045FA0 File Offset: 0x000441A0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.bodyInputBank)
			{
				if (this.bodyInputBank.activateEquipment.justPressed)
				{
					this.shouldFire = !this.shouldFire;
				}
				if (this.body.inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) > 0)
				{
					this.shouldFire = true;
				}
			}
			this.LinkToDisplay();
			if (this.bodyAimAnimator && this.gunAnimator)
			{
				this.bodyAimAnimator.UpdateAnimatorParameters(this.gunAnimator, -45f, 45f, 0f, 0f);
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00046050 File Offset: 0x00044250
		protected bool CheckReturnToIdle()
		{
			if (!base.isAuthority)
			{
				return false;
			}
			if ((this.bodyMaster && this.bodyMaster.money <= 0U) || !this.shouldFire)
			{
				this.outer.SetNextState(new GoldGatIdle
				{
					shouldFire = this.shouldFire
				});
				return true;
			}
			return false;
		}

		// Token: 0x04001430 RID: 5168
		protected NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x04001431 RID: 5169
		protected GameObject bodyGameObject;

		// Token: 0x04001432 RID: 5170
		protected CharacterBody body;

		// Token: 0x04001433 RID: 5171
		protected ChildLocator gunChildLocator;

		// Token: 0x04001434 RID: 5172
		protected Animator gunAnimator;

		// Token: 0x04001435 RID: 5173
		protected Transform gunTransform;

		// Token: 0x04001436 RID: 5174
		protected CharacterMaster bodyMaster;

		// Token: 0x04001437 RID: 5175
		protected EquipmentSlot bodyEquipmentSlot;

		// Token: 0x04001438 RID: 5176
		protected InputBankTest bodyInputBank;

		// Token: 0x04001439 RID: 5177
		protected AimAnimator bodyAimAnimator;

		// Token: 0x0400143A RID: 5178
		public bool shouldFire;

		// Token: 0x0400143B RID: 5179
		private bool linkedToDisplay;
	}
}
