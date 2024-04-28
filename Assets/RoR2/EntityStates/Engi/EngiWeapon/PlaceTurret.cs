using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003AE RID: 942
	public class PlaceTurret : BaseState
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x00049FF8 File Offset: 0x000481F8
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.currentPlacementInfo = this.GetPlacementInfo();
				this.blueprints = UnityEngine.Object.Instantiate<GameObject>(this.blueprintPrefab, this.currentPlacementInfo.position, this.currentPlacementInfo.rotation).GetComponent<BlueprintController>();
			}
			this.PlayAnimation("Gesture", "PrepTurret");
			this.entryCountdown = 0.1f;
			this.exitCountdown = 0.25f;
			this.exitPending = false;
			if (base.modelLocator)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("WristDisplay");
					if (transform)
					{
						this.wristDisplayObject = UnityEngine.Object.Instantiate<GameObject>(this.wristDisplayPrefab, transform);
					}
				}
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0004A0C4 File Offset: 0x000482C4
		private PlaceTurret.PlacementInfo GetPlacementInfo()
		{
			Ray aimRay = base.GetAimRay();
			Vector3 direction = aimRay.direction;
			direction.y = 0f;
			direction.Normalize();
			aimRay.direction = direction;
			PlaceTurret.PlacementInfo placementInfo = default(PlaceTurret.PlacementInfo);
			placementInfo.ok = false;
			placementInfo.rotation = Util.QuaternionSafeLookRotation(-direction);
			Ray ray = new Ray(aimRay.GetPoint(2f) + Vector3.up * 1f, Vector3.down);
			float num = 4f;
			float num2 = num;
			RaycastHit raycastHit;
			if (Physics.SphereCast(ray, 0.5f, out raycastHit, num, LayerIndex.world.mask) && raycastHit.normal.y > 0.5f)
			{
				num2 = raycastHit.distance;
				placementInfo.ok = true;
			}
			Vector3 point = ray.GetPoint(num2 + 0.5f);
			placementInfo.position = point;
			if (placementInfo.ok)
			{
				float num3 = Mathf.Max(1.82f, 0f);
				if (Physics.CheckCapsule(placementInfo.position + Vector3.up * (num3 - 0.5f), placementInfo.position + Vector3.up * 0.5f, 0.45f, LayerIndex.world.mask | LayerIndex.defaultLayer.mask))
				{
					placementInfo.ok = false;
				}
			}
			return placementInfo;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004A246 File Offset: 0x00048446
		private void DestroyBlueprints()
		{
			if (this.blueprints)
			{
				EntityState.Destroy(this.blueprints.gameObject);
				this.blueprints = null;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004A26C File Offset: 0x0004846C
		public override void OnExit()
		{
			base.OnExit();
			this.PlayAnimation("Gesture", "PlaceTurret");
			if (this.wristDisplayObject)
			{
				EntityState.Destroy(this.wristDisplayObject);
			}
			this.DestroyBlueprints();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004A2A4 File Offset: 0x000484A4
		public override void Update()
		{
			base.Update();
			this.currentPlacementInfo = this.GetPlacementInfo();
			if (this.blueprints)
			{
				this.blueprints.PushState(this.currentPlacementInfo.position, this.currentPlacementInfo.rotation, this.currentPlacementInfo.ok);
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004A2FC File Offset: 0x000484FC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.entryCountdown -= Time.fixedDeltaTime;
				if (this.exitPending)
				{
					this.exitCountdown -= Time.fixedDeltaTime;
					if (this.exitCountdown <= 0f)
					{
						this.outer.SetNextStateToMain();
						return;
					}
				}
				else if (base.inputBank && this.entryCountdown <= 0f)
				{
					if ((base.inputBank.skill1.down || base.inputBank.skill4.justPressed) && this.currentPlacementInfo.ok)
					{
						if (base.characterBody)
						{
							base.characterBody.SendConstructTurret(base.characterBody, this.currentPlacementInfo.position, this.currentPlacementInfo.rotation, MasterCatalog.FindMasterIndex(this.turretMasterPrefab));
							if (base.skillLocator)
							{
								GenericSkill skill = base.skillLocator.GetSkill(SkillSlot.Special);
								if (skill)
								{
									skill.DeductStock(1);
								}
							}
						}
						Util.PlaySound(this.placeSoundString, base.gameObject);
						this.DestroyBlueprints();
						this.exitPending = true;
					}
					if (base.inputBank.skill2.justPressed)
					{
						this.DestroyBlueprints();
						this.exitPending = true;
					}
				}
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400154B RID: 5451
		[SerializeField]
		public GameObject wristDisplayPrefab;

		// Token: 0x0400154C RID: 5452
		[SerializeField]
		public string placeSoundString;

		// Token: 0x0400154D RID: 5453
		[SerializeField]
		public GameObject blueprintPrefab;

		// Token: 0x0400154E RID: 5454
		[SerializeField]
		public GameObject turretMasterPrefab;

		// Token: 0x0400154F RID: 5455
		private const float placementMaxUp = 1f;

		// Token: 0x04001550 RID: 5456
		private const float placementMaxDown = 3f;

		// Token: 0x04001551 RID: 5457
		private const float placementForwardDistance = 2f;

		// Token: 0x04001552 RID: 5458
		private const float entryDelay = 0.1f;

		// Token: 0x04001553 RID: 5459
		private const float exitDelay = 0.25f;

		// Token: 0x04001554 RID: 5460
		private const float turretRadius = 0.5f;

		// Token: 0x04001555 RID: 5461
		private const float turretHeight = 1.82f;

		// Token: 0x04001556 RID: 5462
		private const float turretCenter = 0f;

		// Token: 0x04001557 RID: 5463
		private const float turretModelYOffset = -0.75f;

		// Token: 0x04001558 RID: 5464
		private GameObject wristDisplayObject;

		// Token: 0x04001559 RID: 5465
		private BlueprintController blueprints;

		// Token: 0x0400155A RID: 5466
		private float exitCountdown;

		// Token: 0x0400155B RID: 5467
		private bool exitPending;

		// Token: 0x0400155C RID: 5468
		private float entryCountdown;

		// Token: 0x0400155D RID: 5469
		private PlaceTurret.PlacementInfo currentPlacementInfo;

		// Token: 0x020003AF RID: 943
		private struct PlacementInfo
		{
			// Token: 0x0400155E RID: 5470
			public bool ok;

			// Token: 0x0400155F RID: 5471
			public Vector3 position;

			// Token: 0x04001560 RID: 5472
			public Quaternion rotation;
		}
	}
}
