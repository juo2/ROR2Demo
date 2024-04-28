using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E61 RID: 3681
	public static class BombArtifactManager
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06005446 RID: 21574 RVA: 0x0015B9A9 File Offset: 0x00159BA9
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.bombArtifactDef;
			}
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0015B9B0 File Offset: 0x00159BB0
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.onArtifactEnabledGlobal += BombArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += BombArtifactManager.OnArtifactDisabled;
			BombArtifactManager.bombPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SpiteBomb");
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0015B9E3 File Offset: 0x00159BE3
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (artifactDef != BombArtifactManager.myArtifact)
			{
				return;
			}
			GlobalEventManager.onCharacterDeathGlobal += BombArtifactManager.OnServerCharacterDeath;
			RoR2Application.onFixedUpdate += BombArtifactManager.ProcessBombQueue;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0015BA1D File Offset: 0x00159C1D
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != BombArtifactManager.myArtifact)
			{
				return;
			}
			BombArtifactManager.bombRequestQueue.Clear();
			RoR2Application.onFixedUpdate -= BombArtifactManager.ProcessBombQueue;
			GlobalEventManager.onCharacterDeathGlobal -= BombArtifactManager.OnServerCharacterDeath;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0015BA5C File Offset: 0x00159C5C
		private static void SpawnBomb(BombArtifactManager.BombRequest bombRequest, float groundY)
		{
			Vector3 spawnPosition = bombRequest.spawnPosition;
			if (spawnPosition.y < groundY + 4f)
			{
				spawnPosition.y = groundY + 4f;
			}
			Vector3 raycastOrigin = bombRequest.raycastOrigin;
			raycastOrigin.y = groundY;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(BombArtifactManager.bombPrefab, spawnPosition, UnityEngine.Random.rotation);
			SpiteBombController component = gameObject.GetComponent<SpiteBombController>();
			DelayBlast delayBlast = component.delayBlast;
			TeamFilter component2 = gameObject.GetComponent<TeamFilter>();
			component.bouncePosition = raycastOrigin;
			component.initialVelocityY = bombRequest.velocityY;
			delayBlast.position = spawnPosition;
			delayBlast.baseDamage = bombRequest.bombBaseDamage;
			delayBlast.baseForce = 2300f;
			delayBlast.attacker = bombRequest.attacker;
			delayBlast.radius = BombArtifactManager.bombBlastRadius;
			delayBlast.crit = false;
			delayBlast.procCoefficient = 0.75f;
			delayBlast.maxTimer = BombArtifactManager.bombFuseTimeout;
			delayBlast.timerStagger = 0f;
			delayBlast.falloffModel = BlastAttack.FalloffModel.None;
			component2.teamIndex = bombRequest.teamIndex;
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0015BB4C File Offset: 0x00159D4C
		private static void OnServerCharacterDeath(DamageReport damageReport)
		{
			if (damageReport.victimTeamIndex != TeamIndex.Monster)
			{
				return;
			}
			CharacterBody victimBody = damageReport.victimBody;
			Vector3 corePosition = victimBody.corePosition;
			int num = Mathf.Min(BombArtifactManager.maxBombCount, Mathf.CeilToInt(victimBody.bestFitRadius * BombArtifactManager.extraBombPerRadius * BombArtifactManager.cvSpiteBombCoefficient.value));
			for (int i = 0; i < num; i++)
			{
				Vector3 b = UnityEngine.Random.insideUnitSphere * (BombArtifactManager.bombSpawnBaseRadius + victimBody.bestFitRadius * BombArtifactManager.bombSpawnRadiusCoefficient);
				BombArtifactManager.BombRequest item = new BombArtifactManager.BombRequest
				{
					spawnPosition = corePosition,
					raycastOrigin = corePosition + b,
					bombBaseDamage = victimBody.damage * BombArtifactManager.bombDamageCoefficient,
					attacker = victimBody.gameObject,
					teamIndex = damageReport.victimTeamIndex,
					velocityY = UnityEngine.Random.Range(5f, 25f)
				};
				BombArtifactManager.bombRequestQueue.Enqueue(item);
			}
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0015BC3C File Offset: 0x00159E3C
		private static void ProcessBombQueue()
		{
			if (BombArtifactManager.bombRequestQueue.Count > 0)
			{
				BombArtifactManager.BombRequest bombRequest = BombArtifactManager.bombRequestQueue.Dequeue();
				Ray ray = new Ray(bombRequest.raycastOrigin + new Vector3(0f, BombArtifactManager.maxBombStepUpDistance, 0f), Vector3.down);
				float maxDistance = BombArtifactManager.maxBombStepUpDistance + BombArtifactManager.maxBombFallDistance;
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, maxDistance, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					BombArtifactManager.SpawnBomb(bombRequest, raycastHit.point.y);
				}
			}
		}

		// Token: 0x04005009 RID: 20489
		private static FloatConVar cvSpiteBombCoefficient = new FloatConVar("spite_bomb_coefficient", ConVarFlags.Cheat, "0.5", "Multiplier for number of spite bombs.");

		// Token: 0x0400500A RID: 20490
		private static GameObject bombPrefab;

		// Token: 0x0400500B RID: 20491
		private static readonly int maxBombCount = 30;

		// Token: 0x0400500C RID: 20492
		private static readonly float extraBombPerRadius = 4f;

		// Token: 0x0400500D RID: 20493
		private static readonly float bombSpawnBaseRadius = 3f;

		// Token: 0x0400500E RID: 20494
		private static readonly float bombSpawnRadiusCoefficient = 4f;

		// Token: 0x0400500F RID: 20495
		private static readonly float bombDamageCoefficient = 1.5f;

		// Token: 0x04005010 RID: 20496
		private static readonly Queue<BombArtifactManager.BombRequest> bombRequestQueue = new Queue<BombArtifactManager.BombRequest>();

		// Token: 0x04005011 RID: 20497
		private static readonly float bombBlastRadius = 7f;

		// Token: 0x04005012 RID: 20498
		private static readonly float bombFuseTimeout = 8f;

		// Token: 0x04005013 RID: 20499
		private static readonly float maxBombStepUpDistance = 8f;

		// Token: 0x04005014 RID: 20500
		private static readonly float maxBombFallDistance = 60f;

		// Token: 0x02000E62 RID: 3682
		private struct BombRequest
		{
			// Token: 0x04005015 RID: 20501
			public Vector3 spawnPosition;

			// Token: 0x04005016 RID: 20502
			public Vector3 raycastOrigin;

			// Token: 0x04005017 RID: 20503
			public float bombBaseDamage;

			// Token: 0x04005018 RID: 20504
			public GameObject attacker;

			// Token: 0x04005019 RID: 20505
			public TeamIndex teamIndex;

			// Token: 0x0400501A RID: 20506
			public float velocityY;
		}
	}
}
