using System;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BDB RID: 3035
	public class MinionLeashBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044E9 RID: 17641 RVA: 0x0011ED83 File Offset: 0x0011CF83
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = true)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.MinionLeash;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x0011ED8A File Offset: 0x0011CF8A
		public void Start()
		{
			if (base.body.hasEffectiveAuthority)
			{
				this.helperPrefab = LegacyResourcesAPI.Load<GameObject>("SpawnCards/HelperPrefab");
				this.rigidbodyMotor = base.GetComponent<RigidbodyMotor>();
			}
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x0011EDB8 File Offset: 0x0011CFB8
		private void FixedUpdate()
		{
			if (!base.body.hasEffectiveAuthority)
			{
				return;
			}
			CharacterMaster master = base.body.master;
			CharacterMaster characterMaster = master ? master.minionOwnership.ownerMaster : null;
			CharacterBody characterBody = characterMaster ? characterMaster.GetBody() : null;
			if (!characterBody)
			{
				return;
			}
			Vector3 corePosition = characterBody.corePosition;
			Vector3 corePosition2 = base.body.corePosition;
			if (((base.body.characterMotor && base.body.characterMotor.walkSpeed > 0f) || (this.rigidbodyMotor && base.body.moveSpeed > 0f)) && (corePosition2 - corePosition).sqrMagnitude > 160000f)
			{
				this.teleportAttemptTimer -= Time.fixedDeltaTime;
				if (this.teleportAttemptTimer <= 0f)
				{
					this.teleportAttemptTimer = 10f;
					SpawnCard spawnCard = ScriptableObject.CreateInstance<SpawnCard>();
					spawnCard.hullSize = base.body.hullClassification;
					spawnCard.nodeGraphType = (base.body.isFlying ? MapNodeGroup.GraphType.Air : MapNodeGroup.GraphType.Ground);
					spawnCard.prefab = this.helperPrefab;
					GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Approximate,
						position = corePosition,
						minDistance = 10f,
						maxDistance = 40f
					}, RoR2Application.rng));
					if (gameObject)
					{
						Vector3 position = gameObject.transform.position;
						if ((position - corePosition).sqrMagnitude < 160000f)
						{
							Debug.Log("MinionLeash teleport for " + base.body.name);
							TeleportHelper.TeleportBody(base.body, position);
							GameObject teleportEffectPrefab = Run.instance.GetTeleportEffectPrefab(base.body.gameObject);
							if (teleportEffectPrefab)
							{
								EffectManager.SimpleEffect(teleportEffectPrefab, position, Quaternion.identity, true);
							}
							UnityEngine.Object.Destroy(gameObject);
						}
					}
					UnityEngine.Object.Destroy(spawnCard);
				}
			}
		}

		// Token: 0x04004357 RID: 17239
		public const float leashDistSq = 160000f;

		// Token: 0x04004358 RID: 17240
		public const float teleportDelayTime = 10f;

		// Token: 0x04004359 RID: 17241
		public const float minTeleportDistance = 10f;

		// Token: 0x0400435A RID: 17242
		public const float maxTeleportDistance = 40f;

		// Token: 0x0400435B RID: 17243
		private GameObject helperPrefab;

		// Token: 0x0400435C RID: 17244
		private RigidbodyMotor rigidbodyMotor;

		// Token: 0x0400435D RID: 17245
		private float teleportAttemptTimer = 10f;
	}
}
