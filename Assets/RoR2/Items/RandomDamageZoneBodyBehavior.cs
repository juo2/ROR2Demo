using System;
using System.Collections.Generic;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BE1 RID: 3041
	public class RandomDamageZoneBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x06004504 RID: 17668 RVA: 0x0011F516 File Offset: 0x0011D716
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.RandomDamageZone;
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x0011F520 File Offset: 0x0011D720
		private void FixedUpdate()
		{
			CharacterMaster master = base.body.master;
			if (!master)
			{
				return;
			}
			if (master.IsDeployableSlotAvailable(DeployableSlot.PowerWard))
			{
				this.wardResummonCooldown -= Time.fixedDeltaTime;
				if (this.wardResummonCooldown <= 0f)
				{
					this.wardResummonCooldown = RandomDamageZoneBodyBehavior.wardRespawnRetryTime;
					if (master.IsDeployableSlotAvailable(DeployableSlot.PowerWard))
					{
						Vector3? vector = this.FindWardSpawnPosition(base.body.corePosition);
						if (vector != null)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/DamageZoneWard"), vector.Value, Quaternion.identity);
							Util.PlaySound("Play_randomDamageZone_appear", gameObject.gameObject);
							gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.None;
							BuffWard component = gameObject.GetComponent<BuffWard>();
							component.Networkradius = RandomDamageZoneBodyBehavior.baseWardRadius * Mathf.Pow(RandomDamageZoneBodyBehavior.wardRadiusMultiplierPerStack, (float)(this.stack - 1));
							component.expireDuration = RandomDamageZoneBodyBehavior.wardDuration;
							NetworkServer.Spawn(gameObject);
							Deployable component2 = gameObject.GetComponent<Deployable>();
							master.AddDeployable(component2, DeployableSlot.PowerWard);
						}
					}
				}
			}
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x0011F624 File Offset: 0x0011D824
		private Vector3? FindWardSpawnPosition(Vector3 ownerPosition)
		{
			if (!SceneInfo.instance)
			{
				return null;
			}
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			if (!groundNodes)
			{
				return null;
			}
			List<NodeGraph.NodeIndex> list = groundNodes.FindNodesInRange(ownerPosition, 0f, 50f, HullMask.None);
			Vector3 value;
			if (list.Count > 0 && groundNodes.GetNodePosition(list[(int)UnityEngine.Random.Range(0f, (float)list.Count)], out value))
			{
				return new Vector3?(value);
			}
			return null;
		}

		// Token: 0x0400436C RID: 17260
		private static readonly float wardDuration = 25f;

		// Token: 0x0400436D RID: 17261
		private static readonly float wardRespawnRetryTime = 1f;

		// Token: 0x0400436E RID: 17262
		private static readonly float baseWardRadius = 16f;

		// Token: 0x0400436F RID: 17263
		private static readonly float wardRadiusMultiplierPerStack = 1.5f;

		// Token: 0x04004370 RID: 17264
		private float wardResummonCooldown;
	}
}
