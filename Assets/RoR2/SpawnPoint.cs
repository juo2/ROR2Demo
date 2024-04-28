using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HG;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008A1 RID: 2209
	public class SpawnPoint : MonoBehaviour
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000CFA89 File Offset: 0x000CDC89
		public static ReadOnlyCollection<SpawnPoint> readOnlyInstancesList
		{
			get
			{
				return SpawnPoint._readOnlyInstancesList;
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000CFA90 File Offset: 0x000CDC90
		private void OnEnable()
		{
			SpawnPoint.instancesList.Add(this);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000CFAA0 File Offset: 0x000CDCA0
		public static SpawnPoint ConsumeSpawnPoint()
		{
			if (SpawnPoint.instancesList.Count == 0)
			{
				return null;
			}
			SpawnPoint spawnPoint = null;
			for (int i = 0; i < SpawnPoint.readOnlyInstancesList.Count; i++)
			{
				if (!SpawnPoint.readOnlyInstancesList[i].consumed)
				{
					spawnPoint = SpawnPoint.readOnlyInstancesList[i];
					SpawnPoint.readOnlyInstancesList[i].consumed = true;
					break;
				}
			}
			if (!spawnPoint)
			{
				for (int j = 0; j < SpawnPoint.readOnlyInstancesList.Count; j++)
				{
					SpawnPoint.readOnlyInstancesList[j].consumed = false;
				}
				spawnPoint = SpawnPoint.readOnlyInstancesList[0];
			}
			return spawnPoint;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000CFB3E File Offset: 0x000CDD3E
		private void OnDisable()
		{
			SpawnPoint.instancesList.Remove(this);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000CFB4C File Offset: 0x000CDD4C
		private static Mesh GetCommandoMesh()
		{
			if (!SpawnPoint.commandoMesh)
			{
				SpawnPoint.LoadCommandoMesh();
			}
			return SpawnPoint.commandoMesh;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000CFB64 File Offset: 0x000CDD64
		private static void LoadCommandoMesh()
		{
			if (SpawnPoint.attemptedCommandoMeshLoad)
			{
				return;
			}
			SpawnPoint.attemptedCommandoMeshLoad = true;
			GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody");
			if (!gameObject)
			{
				return;
			}
			CharacterModel component = gameObject.GetComponent<ModelLocator>().modelTransform.GetComponent<CharacterModel>();
			SkinnedMeshRenderer skinnedMeshRenderer = null;
			for (int i = 0; i < component.baseRendererInfos.Length; i++)
			{
				skinnedMeshRenderer = (component.baseRendererInfos[i].renderer as SkinnedMeshRenderer);
				if (skinnedMeshRenderer)
				{
					break;
				}
			}
			SpawnPoint.commandoMesh = skinnedMeshRenderer.sharedMesh;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000CFBE5 File Offset: 0x000CDDE5
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			SpawnPoint.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/SpawnPoint");
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000CFBF8 File Offset: 0x000CDDF8
		public static void AddSpawnPoint(NodeGraph nodeGraph, NodeGraph.NodeIndex nodeIndex, Xoroshiro128Plus rng)
		{
			Vector3 vector;
			nodeGraph.GetNodePosition(nodeIndex, out vector);
			List<NodeGraph.LinkIndex> list = CollectionPool<NodeGraph.LinkIndex, List<NodeGraph.LinkIndex>>.RentCollection();
			nodeGraph.GetActiveNodeLinks(nodeIndex, list);
			Quaternion rotation;
			if (list.Count > 0)
			{
				NodeGraph.LinkIndex linkIndex = rng.NextElementUniform<NodeGraph.LinkIndex>(list);
				Vector3 a;
				nodeGraph.GetNodePosition(nodeGraph.GetLinkEndNode(linkIndex), out a);
				rotation = Util.QuaternionSafeLookRotation(a - vector);
			}
			else
			{
				rotation = Quaternion.Euler(0f, rng.nextNormalizedFloat * 360f, 0f);
			}
			SpawnPoint.AddSpawnPoint(vector, rotation);
			list = CollectionPool<NodeGraph.LinkIndex, List<NodeGraph.LinkIndex>>.ReturnCollection(list);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000CFC78 File Offset: 0x000CDE78
		public static void AddSpawnPoint(Vector3 position, Quaternion rotation)
		{
			UnityEngine.Object.Instantiate<GameObject>(SpawnPoint.prefab, position, rotation);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000CFC87 File Offset: 0x000CDE87
		public void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = Color.green;
			Gizmos.DrawWireMesh(SpawnPoint.GetCommandoMesh());
		}

		// Token: 0x04003287 RID: 12935
		private static List<SpawnPoint> instancesList = new List<SpawnPoint>();

		// Token: 0x04003288 RID: 12936
		private static ReadOnlyCollection<SpawnPoint> _readOnlyInstancesList = new ReadOnlyCollection<SpawnPoint>(SpawnPoint.instancesList);

		// Token: 0x04003289 RID: 12937
		[Tooltip("Flagged when a player spawns on this position, to stop overlapping spawn positions")]
		public bool consumed;

		// Token: 0x0400328A RID: 12938
		private static GameObject prefab;

		// Token: 0x0400328B RID: 12939
		private static Mesh commandoMesh;

		// Token: 0x0400328C RID: 12940
		private static bool attemptedCommandoMeshLoad = false;
	}
}
