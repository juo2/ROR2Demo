using System;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000560 RID: 1376
	[CreateAssetMenu(menuName = "RoR2/SpawnCards/SpawnCard")]
	public class SpawnCard : ScriptableObject
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x0006C000 File Offset: 0x0006A200
		protected virtual void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest spawnRequest, ref SpawnCard.SpawnResult spawnResult)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation);
			if (this.sendOverNetwork)
			{
				NetworkServer.Spawn(gameObject);
			}
			spawnResult.spawnedInstance = gameObject;
			spawnResult.success = true;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0006C03C File Offset: 0x0006A23C
		public SpawnCard.SpawnResult DoSpawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest spawnRequest)
		{
			SpawnCard.SpawnResult spawnResult = new SpawnCard.SpawnResult
			{
				spawnRequest = spawnRequest,
				position = position,
				rotation = rotation
			};
			this.Spawn(position, rotation, spawnRequest, ref spawnResult);
			Action<SpawnCard.SpawnResult> onSpawnedServer = spawnRequest.onSpawnedServer;
			if (onSpawnedServer != null)
			{
				onSpawnedServer(spawnResult);
			}
			Action<SpawnCard.SpawnResult> action = SpawnCard.onSpawnedServerGlobal;
			if (action != null)
			{
				action(spawnResult);
			}
			return spawnResult;
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060018E8 RID: 6376 RVA: 0x0006C09C File Offset: 0x0006A29C
		// (remove) Token: 0x060018E9 RID: 6377 RVA: 0x0006C0D0 File Offset: 0x0006A2D0
		public static event Action<SpawnCard.SpawnResult> onSpawnedServerGlobal;

		// Token: 0x04001EA3 RID: 7843
		public GameObject prefab;

		// Token: 0x04001EA4 RID: 7844
		public bool sendOverNetwork;

		// Token: 0x04001EA5 RID: 7845
		public HullClassification hullSize;

		// Token: 0x04001EA6 RID: 7846
		public MapNodeGroup.GraphType nodeGraphType;

		// Token: 0x04001EA7 RID: 7847
		[EnumMask(typeof(NodeFlags))]
		public NodeFlags requiredFlags;

		// Token: 0x04001EA8 RID: 7848
		[EnumMask(typeof(NodeFlags))]
		public NodeFlags forbiddenFlags;

		// Token: 0x04001EA9 RID: 7849
		public int directorCreditCost;

		// Token: 0x04001EAA RID: 7850
		public bool occupyPosition;

		// Token: 0x04001EAB RID: 7851
		[Tooltip("Default = default rules, ArtifactOnly = only elite when forced by the elite-only artifact, Lunar = special lunar elites only (+ regular w/ elite-only artifact)")]
		public SpawnCard.EliteRules eliteRules;

		// Token: 0x02000561 RID: 1377
		public struct SpawnResult
		{
			// Token: 0x04001EAD RID: 7853
			public GameObject spawnedInstance;

			// Token: 0x04001EAE RID: 7854
			public DirectorSpawnRequest spawnRequest;

			// Token: 0x04001EAF RID: 7855
			public Vector3 position;

			// Token: 0x04001EB0 RID: 7856
			public Quaternion rotation;

			// Token: 0x04001EB1 RID: 7857
			public bool success;
		}

		// Token: 0x02000562 RID: 1378
		public enum EliteRules
		{
			// Token: 0x04001EB3 RID: 7859
			Default,
			// Token: 0x04001EB4 RID: 7860
			ArtifactOnly,
			// Token: 0x04001EB5 RID: 7861
			Lunar
		}
	}
}
