using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.CaptainDefenseMatrixItem
{
	// Token: 0x02000431 RID: 1073
	public class DefenseMatrixOn : BaseBodyAttachmentState
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x000559F4 File Offset: 0x00053BF4
		private float rechargeFrequency
		{
			get
			{
				return DefenseMatrixOn.baseRechargeFrequency * (base.attachedBody ? base.attachedBody.attackSpeed : 1f);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00055A1B File Offset: 0x00053C1B
		private float fireFrequency
		{
			get
			{
				return Mathf.Max(DefenseMatrixOn.minimumFireFrequency, this.rechargeFrequency);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00055A2D File Offset: 0x00053C2D
		private float timeBetweenFiring
		{
			get
			{
				return 1f / this.fireFrequency;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00055A3B File Offset: 0x00053C3B
		private bool isReadyToFire
		{
			get
			{
				return this.rechargeTimer <= 0f;
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00055A4D File Offset: 0x00053C4D
		protected int GetItemStack()
		{
			if (!base.attachedBody || !base.attachedBody.inventory)
			{
				return 1;
			}
			return base.attachedBody.inventory.GetItemCount(RoR2Content.Items.CaptainDefenseMatrix);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00055A88 File Offset: 0x00053C88
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.attachedBody)
			{
				PlayerCharacterMasterController component = base.attachedBody.master.GetComponent<PlayerCharacterMasterController>();
				if (component)
				{
					NetworkUser networkUser = component.networkUser;
					if (networkUser)
					{
						PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(RoR2Content.Items.CaptainDefenseMatrix.itemIndex);
						LocalUser localUser = networkUser.localUser;
						if (localUser == null)
						{
							return;
						}
						localUser.userProfile.DiscoverPickup(pickupIndex);
					}
				}
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00055AF8 File Offset: 0x00053CF8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!NetworkServer.active)
			{
				return;
			}
			this.rechargeTimer -= Time.fixedDeltaTime;
			if (base.fixedAge > this.timeBetweenFiring)
			{
				base.fixedAge -= this.timeBetweenFiring;
				if (this.isReadyToFire && this.DeleteNearbyProjectile())
				{
					this.rechargeTimer = 1f / this.rechargeFrequency;
				}
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00055B68 File Offset: 0x00053D68
		private bool DeleteNearbyProjectile()
		{
			Vector3 vector = base.attachedBody ? base.attachedBody.corePosition : Vector3.zero;
			TeamIndex teamIndex = base.attachedBody ? base.attachedBody.teamComponent.teamIndex : TeamIndex.None;
			float num = DefenseMatrixOn.projectileEraserRadius * DefenseMatrixOn.projectileEraserRadius;
			int num2 = 0;
			int itemStack = this.GetItemStack();
			bool result = false;
			List<ProjectileController> instancesList = InstanceTracker.GetInstancesList<ProjectileController>();
			List<ProjectileController> list = new List<ProjectileController>();
			int num3 = 0;
			int count = instancesList.Count;
			while (num3 < count && num2 < itemStack)
			{
				ProjectileController projectileController = instancesList[num3];
				if (!projectileController.cannotBeDeleted && projectileController.teamFilter.teamIndex != teamIndex && (projectileController.transform.position - vector).sqrMagnitude < num)
				{
					list.Add(projectileController);
					num2++;
				}
				num3++;
			}
			int i = 0;
			int count2 = list.Count;
			while (i < count2)
			{
				ProjectileController projectileController2 = list[i];
				if (projectileController2)
				{
					result = true;
					Vector3 position = projectileController2.transform.position;
					Vector3 start = vector;
					if (DefenseMatrixOn.tracerEffectPrefab)
					{
						EffectData effectData = new EffectData
						{
							origin = position,
							start = start
						};
						EffectManager.SpawnEffect(DefenseMatrixOn.tracerEffectPrefab, effectData, true);
					}
					EntityState.Destroy(projectileController2.gameObject);
				}
				i++;
			}
			return result;
		}

		// Token: 0x040018B3 RID: 6323
		public static float projectileEraserRadius;

		// Token: 0x040018B4 RID: 6324
		public static float minimumFireFrequency;

		// Token: 0x040018B5 RID: 6325
		public static float baseRechargeFrequency;

		// Token: 0x040018B6 RID: 6326
		public static GameObject tracerEffectPrefab;

		// Token: 0x040018B7 RID: 6327
		private float rechargeTimer;
	}
}
