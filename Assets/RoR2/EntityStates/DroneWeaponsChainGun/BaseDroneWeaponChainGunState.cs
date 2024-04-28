using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates.DroneWeaponsChainGun
{
	// Token: 0x020003BF RID: 959
	public abstract class BaseDroneWeaponChainGunState : EntityState
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x0004B3CC File Offset: 0x000495CC
		public override void OnEnter()
		{
			base.OnEnter();
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			if (this.networkedBodyAttachment)
			{
				this.bodyGameObject = this.networkedBodyAttachment.attachedBodyObject;
				this.body = this.networkedBodyAttachment.attachedBody;
				if (this.bodyGameObject && this.body)
				{
					ModelLocator component = this.body.GetComponent<ModelLocator>();
					if (component)
					{
						this.bodyAimAnimator = component.modelTransform.GetComponent<AimAnimator>();
						this.bodyTeamComponent = this.body.GetComponent<TeamComponent>();
					}
				}
			}
			this.LinkToDisplay();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0004B470 File Offset: 0x00049670
		private void LinkToDisplay()
		{
			if (this.linkedToDisplay)
			{
				return;
			}
			this.gunAnimators = new List<Animator>();
			this.gunChildLocators = new List<ChildLocator>();
			if (this.networkedBodyAttachment)
			{
				this.bodyGameObject = this.networkedBodyAttachment.attachedBodyObject;
				this.body = this.networkedBodyAttachment.attachedBody;
				if (this.bodyGameObject && this.body)
				{
					ModelLocator component = this.body.GetComponent<ModelLocator>();
					if (component && component.modelTransform)
					{
						this.bodyAimAnimator = component.modelTransform.GetComponent<AimAnimator>();
						this.bodyTeamComponent = this.body.GetComponent<TeamComponent>();
						CharacterModel component2 = component.modelTransform.GetComponent<CharacterModel>();
						if (component2)
						{
							List<GameObject> itemDisplayObjects = component2.GetItemDisplayObjects(DLC1Content.Items.DroneWeaponsDisplay1.itemIndex);
							itemDisplayObjects.AddRange(component2.GetItemDisplayObjects(DLC1Content.Items.DroneWeaponsDisplay2.itemIndex));
							foreach (GameObject gameObject in itemDisplayObjects)
							{
								ChildLocator component3 = gameObject.GetComponent<ChildLocator>();
								if (component3)
								{
									this.gunChildLocators.Add(component3);
									Animator animator = component3.FindChildComponent<Animator>("AimAnimator");
									if (animator)
									{
										this.gunAnimators.Add(animator);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0004B5EC File Offset: 0x000497EC
		public void PassDisplayLinks(List<ChildLocator> gunChildLocators, List<Animator> gunAnimators)
		{
			if (this.linkedToDisplay)
			{
				return;
			}
			this.linkedToDisplay = true;
			this.gunAnimators = gunAnimators;
			this.gunChildLocators = gunChildLocators;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0004B60C File Offset: 0x0004980C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.LinkToDisplay();
			if (this.bodyAimAnimator)
			{
				foreach (Animator animator in this.gunAnimators)
				{
					this.bodyAimAnimator.UpdateAnimatorParameters(animator, this.pitchRangeMin, this.pitchRangeMax, this.yawRangeMin, this.yawRangeMax);
				}
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0004B698 File Offset: 0x00049898
		protected Transform FindChild(string childName)
		{
			foreach (ChildLocator childLocator in this.gunChildLocators)
			{
				Transform transform = childLocator.FindChild(childName);
				if (transform)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0004B6FC File Offset: 0x000498FC
		protected Ray GetAimRay()
		{
			if (this.body.inputBank)
			{
				return new Ray(this.body.inputBank.aimOrigin, this.body.inputBank.aimDirection);
			}
			return new Ray(base.transform.position, base.transform.forward);
		}

		// Token: 0x0400159C RID: 5532
		private const string aimAnimatorChildName = "AimAnimator";

		// Token: 0x0400159D RID: 5533
		[SerializeField]
		public float pitchRangeMin;

		// Token: 0x0400159E RID: 5534
		[SerializeField]
		public float pitchRangeMax;

		// Token: 0x0400159F RID: 5535
		[SerializeField]
		public float yawRangeMin;

		// Token: 0x040015A0 RID: 5536
		[SerializeField]
		public float yawRangeMax;

		// Token: 0x040015A1 RID: 5537
		protected NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x040015A2 RID: 5538
		protected GameObject bodyGameObject;

		// Token: 0x040015A3 RID: 5539
		protected CharacterBody body;

		// Token: 0x040015A4 RID: 5540
		protected List<ChildLocator> gunChildLocators;

		// Token: 0x040015A5 RID: 5541
		protected List<Animator> gunAnimators;

		// Token: 0x040015A6 RID: 5542
		protected AimAnimator bodyAimAnimator;

		// Token: 0x040015A7 RID: 5543
		protected TeamComponent bodyTeamComponent;

		// Token: 0x040015A8 RID: 5544
		private bool linkedToDisplay;
	}
}
