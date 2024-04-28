using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004EB RID: 1259
	public class BodySplitter
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00064D89 File Offset: 0x00062F89
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x00064D91 File Offset: 0x00062F91
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("'value' cannot be non-positive.", "value");
				}
				this._count = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00064DAE File Offset: 0x00062FAE
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x00064DB6 File Offset: 0x00062FB6
		public Vector3 splinterInitialVelocityLocal { get; set; } = Vector3.zero;

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00064DBF File Offset: 0x00062FBF
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x00064DC7 File Offset: 0x00062FC7
		public float minSpawnCircleRadius { get; set; }

		// Token: 0x060016E1 RID: 5857 RVA: 0x00064DD0 File Offset: 0x00062FD0
		public BodySplitter()
		{
			this.masterSummon = new MasterSummon
			{
				masterPrefab = null,
				ignoreTeamMemberLimit = false,
				useAmbientLevel = null,
				teamIndexOverride = null
			};
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00064E26 File Offset: 0x00063026
		public void Perform()
		{
			this.PerformInternal(this.masterSummon);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00064E34 File Offset: 0x00063034
		private void PerformInternal(MasterSummon masterSummon)
		{
			if (this.body == null)
			{
				throw new InvalidOperationException("'body' is null.");
			}
			if (!this.body)
			{
				throw new InvalidOperationException("'body' is an invalid object.");
			}
			GameObject masterPrefab = masterSummon.masterPrefab;
			CharacterMaster component = masterPrefab.GetComponent<CharacterMaster>();
			if (component == null)
			{
				throw new InvalidOperationException("'splinterMasterPrefab' does not have a CharacterMaster component.");
			}
			if (!component.masterIndex.isValid)
			{
				throw new InvalidOperationException("'splinterMasterPrefab' is not registered with MasterCatalog.");
			}
			if (MasterCatalog.GetMasterPrefab(component.masterIndex) != masterPrefab)
			{
				throw new InvalidOperationException("'splinterMasterPrefab' is not a prefab.");
			}
			Vector3 position = this.body.transform.position;
			float y = Quaternion.LookRotation(this.body.inputBank.aimDirection).eulerAngles.y;
			GameObject bodyPrefab = component.bodyPrefab;
			bodyPrefab.GetComponent<CharacterBody>();
			float num = BodySplitter.CalcBodyXZRadius(bodyPrefab);
			float num2 = 0f;
			if (this.count > 1)
			{
				num2 = num / Mathf.Sin(3.1415927f / (float)this.count);
			}
			num2 = Mathf.Max(num2, this.minSpawnCircleRadius);
			masterSummon.summonerBodyObject = this.body.gameObject;
			masterSummon.inventoryToCopy = this.body.inventory;
			masterSummon.loadout = (this.body.master ? this.body.master.loadout : null);
			masterSummon.inventoryItemCopyFilter = new Func<ItemIndex, bool>(BodySplitter.CopyItemFilter);
			foreach (float num3 in new DegreeSlices(this.count, 0.5f))
			{
				Quaternion rotation = Quaternion.Euler(0f, y + num3 + 180f, 0f);
				Vector3 vector = rotation * Vector3.forward;
				float d = num2;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(position, vector), out raycastHit, num2 + num, LayerIndex.world.intVal, QueryTriggerInteraction.Ignore))
				{
					d = raycastHit.distance - num;
				}
				Vector3 position2 = position + vector * d;
				masterSummon.position = position2;
				masterSummon.rotation = rotation;
				try
				{
					CharacterMaster characterMaster = masterSummon.Perform();
					if (characterMaster)
					{
						CharacterBody exists = characterMaster.GetBody();
						if (exists)
						{
							Vector3 additionalVelocity = rotation * this.splinterInitialVelocityLocal;
							BodySplitter.AddBodyVelocity(exists, additionalVelocity);
							characterMaster.money = (uint)Mathf.FloorToInt(this.body.master.money * this.moneyMultiplier);
						}
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000650F8 File Offset: 0x000632F8
		private static float CalcBodyXZRadius(GameObject bodyPrefab)
		{
			Collider component = bodyPrefab.GetComponent<Collider>();
			if (!component)
			{
				return 0f;
			}
			Vector3 position = bodyPrefab.transform.position;
			Bounds bounds = component.bounds;
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Max(0f, position.x - min.x), position.z - min.z), max.x - position.x), max.z - position.z);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00065190 File Offset: 0x00063390
		private static void AddBodyVelocity(CharacterBody body, Vector3 additionalVelocity)
		{
			IPhysMotor component = body.GetComponent<IPhysMotor>();
			if (component == null)
			{
				return;
			}
			PhysForceInfo physForceInfo = PhysForceInfo.Create();
			physForceInfo.force = additionalVelocity;
			physForceInfo.massIsOne = true;
			physForceInfo.ignoreGroundStick = true;
			physForceInfo.disableAirControlUntilCollision = false;
			component.ApplyForceImpulse(physForceInfo);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000651D8 File Offset: 0x000633D8
		private static bool CopyItemFilter(ItemIndex itemIndex)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			return itemDef && itemDef.tier == ItemTier.NoTier;
		}

		// Token: 0x04001C8C RID: 7308
		public CharacterBody body;

		// Token: 0x04001C8D RID: 7309
		public float moneyMultiplier;

		// Token: 0x04001C8E RID: 7310
		private int _count = 1;

		// Token: 0x04001C91 RID: 7313
		public readonly MasterSummon masterSummon;
	}
}
