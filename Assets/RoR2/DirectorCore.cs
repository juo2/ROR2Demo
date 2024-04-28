using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006A2 RID: 1698
	public class DirectorCore : MonoBehaviour
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0008DF74 File Offset: 0x0008C174
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x0008DF7B File Offset: 0x0008C17B
		public static DirectorCore instance { get; private set; }

		// Token: 0x06002120 RID: 8480 RVA: 0x0008DF84 File Offset: 0x0008C184
		public GameObject[] GetObjectsOfTeam(TeamIndex _teamIndex)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < DirectorCore.spawnedObjects.Count; i++)
			{
				CharacterMaster component = DirectorCore.spawnedObjects[i].GetComponent<CharacterMaster>();
				if (component && component.teamIndex == _teamIndex)
				{
					DirectorCore.spawnedObjects.Add(DirectorCore.spawnedObjects[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0008DFE9 File Offset: 0x0008C1E9
		private void OnEnable()
		{
			if (!DirectorCore.instance)
			{
				DirectorCore.instance = this;
				return;
			}
			Debug.LogErrorFormat(this, "Duplicate instance of singleton class {0}. Only one should exist at a time.", new object[]
			{
				base.GetType().Name
			});
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0008E01D File Offset: 0x0008C21D
		private void OnDisable()
		{
			if (DirectorCore.instance == this)
			{
				DirectorCore.instance = null;
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0008E032 File Offset: 0x0008C232
		public void AddOccupiedNode(NodeGraph nodeGraph, NodeGraph.NodeIndex nodeIndex)
		{
			Array.Resize<DirectorCore.NodeReference>(ref this.occupiedNodes, this.occupiedNodes.Length + 1);
			this.occupiedNodes[this.occupiedNodes.Length - 1] = new DirectorCore.NodeReference(nodeGraph, nodeIndex);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0008E068 File Offset: 0x0008C268
		private bool CheckPositionFree(NodeGraph nodeGraph, NodeGraph.NodeIndex nodeIndex, SpawnCard spawnCard)
		{
			DirectorCore.NodeReference value = new DirectorCore.NodeReference(nodeGraph, nodeIndex);
			if (Array.IndexOf<DirectorCore.NodeReference>(this.occupiedNodes, value) != -1)
			{
				return false;
			}
			float num = HullDef.Find(spawnCard.hullSize).radius * 0.7f;
			Vector3 vector;
			nodeGraph.GetNodePosition(nodeIndex, out vector);
			if (spawnCard.nodeGraphType == MapNodeGroup.GraphType.Ground)
			{
				vector += Vector3.up * (num + 0.25f);
			}
			return Physics.OverlapSphere(vector, num, LayerIndex.world.mask | LayerIndex.defaultLayer.mask | LayerIndex.fakeActor.mask).Length == 0;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0008E114 File Offset: 0x0008C314
		public GameObject TrySpawnObject([NotNull] DirectorSpawnRequest directorSpawnRequest)
		{
			SpawnCard spawnCard = directorSpawnRequest.spawnCard;
			DirectorCore.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.placementRule = directorSpawnRequest.placementRule;
			Xoroshiro128Plus rng = directorSpawnRequest.rng;
			NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(spawnCard.nodeGraphType);
			if (nodeGraph == null)
			{
				Debug.LogError(string.Format("Unable to find nodegraph for {0} of type {1}.", SceneInfo.instance.sceneDef.cachedName, spawnCard.nodeGraphType));
				return null;
			}
			GameObject result = null;
			switch (CS$<>8__locals1.placementRule.placementMode)
			{
			case DirectorPlacementRule.PlacementMode.Direct:
			{
				Quaternion quaternion = Quaternion.Euler(0f, rng.nextNormalizedFloat * 360f, 0f);
				result = spawnCard.DoSpawn(CS$<>8__locals1.placementRule.spawnOnTarget ? CS$<>8__locals1.placementRule.spawnOnTarget.position : directorSpawnRequest.placementRule.position, CS$<>8__locals1.placementRule.spawnOnTarget ? CS$<>8__locals1.placementRule.spawnOnTarget.rotation : quaternion, directorSpawnRequest).spawnedInstance;
				break;
			}
			case DirectorPlacementRule.PlacementMode.Approximate:
			{
				List<NodeGraph.NodeIndex> list = nodeGraph.FindNodesInRangeWithFlagConditions(CS$<>8__locals1.placementRule.targetPosition, CS$<>8__locals1.placementRule.minDistance, CS$<>8__locals1.placementRule.maxDistance, (HullMask)(1 << (int)spawnCard.hullSize), spawnCard.requiredFlags, spawnCard.forbiddenFlags, CS$<>8__locals1.placementRule.preventOverhead);
				if (list.Count == 0)
				{
					Debug.Log(string.Format("PlacementMode.Approximate:  could not find nodes satisfying conditions for {0}.  targetPosition={1}, minDistance={2}, maxDistance={3}, hullSize ={4}, requiredFlags={5}, forbiddenFlags={6}, preventOverhead={7}", new object[]
					{
						spawnCard.name,
						CS$<>8__locals1.placementRule.targetPosition,
						CS$<>8__locals1.placementRule.minDistance,
						CS$<>8__locals1.placementRule.maxDistance,
						spawnCard.hullSize,
						spawnCard.requiredFlags,
						spawnCard.forbiddenFlags,
						CS$<>8__locals1.placementRule.preventOverhead
					}));
				}
				while (list.Count > 0)
				{
					int index = rng.RangeInt(0, list.Count);
					NodeGraph.NodeIndex nodeIndex = list[index];
					Vector3 vector;
					if (nodeGraph.GetNodePosition(nodeIndex, out vector) && this.CheckPositionFree(nodeGraph, nodeIndex, spawnCard))
					{
						Quaternion rotation = DirectorCore.<TrySpawnObject>g__GetRotationFacingTargetPositionFromPoint|12_0(vector, ref CS$<>8__locals1);
						result = spawnCard.DoSpawn(vector, rotation, directorSpawnRequest).spawnedInstance;
						if (spawnCard.occupyPosition)
						{
							this.AddOccupiedNode(nodeGraph, nodeIndex);
							break;
						}
						break;
					}
					else
					{
						Debug.Log("Position not free or not found.");
						list.RemoveAt(index);
					}
				}
				break;
			}
			case DirectorPlacementRule.PlacementMode.ApproximateSimple:
			{
				NodeGraph.NodeIndex nodeIndex2 = nodeGraph.FindClosestNodeWithFlagConditions(CS$<>8__locals1.placementRule.targetPosition, spawnCard.hullSize, spawnCard.requiredFlags, spawnCard.forbiddenFlags, CS$<>8__locals1.placementRule.preventOverhead);
				Vector3 vector2;
				if (nodeGraph.GetNodePosition(nodeIndex2, out vector2))
				{
					if (this.CheckPositionFree(nodeGraph, nodeIndex2, spawnCard))
					{
						Quaternion rotation2 = DirectorCore.<TrySpawnObject>g__GetRotationFacingTargetPositionFromPoint|12_0(vector2, ref CS$<>8__locals1);
						result = spawnCard.DoSpawn(vector2, rotation2, directorSpawnRequest).spawnedInstance;
						if (spawnCard.occupyPosition)
						{
							this.AddOccupiedNode(nodeGraph, nodeIndex2);
						}
					}
					else
					{
						Debug.Log("Position not free.");
					}
				}
				else
				{
					Debug.Log(string.Format("PlacementMode.ApproximateSimple:  could not find nodes satisfying conditions for {0}.  targetPosition={1}, hullSize ={2}, requiredFlags={3}, forbiddenFlags={4}, preventOverhead={5}", new object[]
					{
						spawnCard.name,
						CS$<>8__locals1.placementRule.targetPosition,
						spawnCard.hullSize,
						spawnCard.requiredFlags,
						spawnCard.forbiddenFlags,
						CS$<>8__locals1.placementRule.preventOverhead
					}));
				}
				break;
			}
			case DirectorPlacementRule.PlacementMode.NearestNode:
			{
				NodeGraph.NodeIndex nodeIndex3 = nodeGraph.FindClosestNodeWithFlagConditions(CS$<>8__locals1.placementRule.targetPosition, spawnCard.hullSize, spawnCard.requiredFlags, spawnCard.forbiddenFlags, CS$<>8__locals1.placementRule.preventOverhead);
				Vector3 vector3;
				if (nodeGraph.GetNodePosition(nodeIndex3, out vector3))
				{
					Quaternion rotation3 = DirectorCore.<TrySpawnObject>g__GetRotationFacingTargetPositionFromPoint|12_0(vector3, ref CS$<>8__locals1);
					result = spawnCard.DoSpawn(vector3, rotation3, directorSpawnRequest).spawnedInstance;
					if (spawnCard.occupyPosition)
					{
						this.AddOccupiedNode(nodeGraph, nodeIndex3);
					}
				}
				else
				{
					Debug.Log(string.Format("PlacementMode.NearestNode:  could not find nodes satisfying conditions for {0}.  targetPosition={1}, hullSize ={2}, requiredFlags={3}, forbiddenFlags={4}, preventOverhead={5}", new object[]
					{
						spawnCard.name,
						CS$<>8__locals1.placementRule.targetPosition,
						spawnCard.hullSize,
						spawnCard.requiredFlags,
						spawnCard.forbiddenFlags,
						CS$<>8__locals1.placementRule.preventOverhead
					}));
				}
				break;
			}
			case DirectorPlacementRule.PlacementMode.Random:
			{
				List<NodeGraph.NodeIndex> activeNodesForHullMaskWithFlagConditions = nodeGraph.GetActiveNodesForHullMaskWithFlagConditions((HullMask)(1 << (int)spawnCard.hullSize), spawnCard.requiredFlags, spawnCard.forbiddenFlags);
				if (activeNodesForHullMaskWithFlagConditions.Count == 0)
				{
					Debug.Log(string.Format("PlacementMode.Random:  could not find nodes satisfying conditions for {0}.  hullSize={1}, requiredFlags={2}, forbiddenFlags={3}", new object[]
					{
						spawnCard.name,
						spawnCard.hullSize,
						spawnCard.requiredFlags,
						spawnCard.forbiddenFlags
					}));
				}
				while (activeNodesForHullMaskWithFlagConditions.Count > 0)
				{
					int index2 = rng.RangeInt(0, activeNodesForHullMaskWithFlagConditions.Count);
					NodeGraph.NodeIndex nodeIndex4 = activeNodesForHullMaskWithFlagConditions[index2];
					Vector3 position;
					if (nodeGraph.GetNodePosition(nodeIndex4, out position) && this.CheckPositionFree(nodeGraph, nodeIndex4, spawnCard))
					{
						Quaternion rotation4 = Quaternion.Euler(0f, rng.nextNormalizedFloat * 360f, 0f);
						result = spawnCard.DoSpawn(position, rotation4, directorSpawnRequest).spawnedInstance;
						if (spawnCard.occupyPosition)
						{
							this.AddOccupiedNode(nodeGraph, nodeIndex4);
							break;
						}
						break;
					}
					else
					{
						Debug.Log("Position not free or not found.");
						activeNodesForHullMaskWithFlagConditions.RemoveAt(index2);
					}
				}
				break;
			}
			case DirectorPlacementRule.PlacementMode.RandomNormalized:
				if (SceneInfo.instance.approximateMapBoundMesh == null)
				{
					Debug.Log("Approximate Map Bound is missing. Aborting.");
				}
				else
				{
					Bounds bounds = SceneInfo.instance.approximateMapBoundMesh.bounds;
					Vector3 vector4 = new Vector3(rng.RangeFloat(bounds.min.x, bounds.max.x), rng.RangeFloat(bounds.min.y, bounds.max.y), rng.RangeFloat(bounds.min.z, bounds.max.z));
					NodeGraph.NodeIndex nodeIndex5 = nodeGraph.FindClosestNodeWithFlagConditions(vector4, spawnCard.hullSize, spawnCard.requiredFlags, spawnCard.forbiddenFlags, CS$<>8__locals1.placementRule.preventOverhead);
					if (nodeGraph.GetNodePosition(nodeIndex5, out vector4))
					{
						if (this.CheckPositionFree(nodeGraph, nodeIndex5, spawnCard))
						{
							Quaternion rotation5 = DirectorCore.<TrySpawnObject>g__GetRotationFacingTargetPositionFromPoint|12_0(vector4, ref CS$<>8__locals1);
							result = spawnCard.DoSpawn(vector4, rotation5, directorSpawnRequest).spawnedInstance;
							if (spawnCard.occupyPosition)
							{
								this.AddOccupiedNode(nodeGraph, nodeIndex5);
							}
						}
						else
						{
							Debug.Log("Position not free.");
						}
					}
					else
					{
						Debug.Log(string.Format("PlacementMode.RandomNormalized:  could not find nodes satisfying conditions for {0}.  targetPosition={1}, hullSize ={2}, requiredFlags={3}, forbiddenFlags={4}, preventOverhead={5}", new object[]
						{
							spawnCard.name,
							vector4,
							spawnCard.hullSize,
							spawnCard.requiredFlags,
							spawnCard.forbiddenFlags,
							CS$<>8__locals1.placementRule.preventOverhead
						}));
					}
				}
				break;
			}
			return result;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0008E828 File Offset: 0x0008CA28
		public static void GetMonsterSpawnDistance(DirectorCore.MonsterSpawnDistance input, out float minimumDistance, out float maximumDistance)
		{
			minimumDistance = 0f;
			maximumDistance = 0f;
			switch (input)
			{
			case DirectorCore.MonsterSpawnDistance.Standard:
				minimumDistance = 25f;
				maximumDistance = 40f;
				return;
			case DirectorCore.MonsterSpawnDistance.Close:
				minimumDistance = 8f;
				maximumDistance = 20f;
				return;
			case DirectorCore.MonsterSpawnDistance.Far:
				minimumDistance = 70f;
				maximumDistance = 120f;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0008E8A4 File Offset: 0x0008CAA4
		[CompilerGenerated]
		internal static Quaternion <TrySpawnObject>g__GetRotationFacingTargetPositionFromPoint|12_0(Vector3 point, ref DirectorCore.<>c__DisplayClass12_0 A_1)
		{
			Vector3 targetPosition = A_1.placementRule.targetPosition;
			point.y = targetPosition.y;
			return Util.QuaternionSafeLookRotation(A_1.placementRule.targetPosition - point);
		}

		// Token: 0x04002677 RID: 9847
		public static List<GameObject> spawnedObjects = new List<GameObject>();

		// Token: 0x04002679 RID: 9849
		private DirectorCore.NodeReference[] occupiedNodes = Array.Empty<DirectorCore.NodeReference>();

		// Token: 0x020006A3 RID: 1699
		private struct NodeReference : IEquatable<DirectorCore.NodeReference>
		{
			// Token: 0x0600212A RID: 8490 RVA: 0x0008E8E0 File Offset: 0x0008CAE0
			public NodeReference(NodeGraph nodeGraph, NodeGraph.NodeIndex nodeIndex)
			{
				this.nodeGraph = nodeGraph;
				this.nodeIndex = nodeIndex;
			}

			// Token: 0x0600212B RID: 8491 RVA: 0x0008E8F0 File Offset: 0x0008CAF0
			public bool Equals(DirectorCore.NodeReference other)
			{
				return object.Equals(this.nodeGraph, other.nodeGraph) && this.nodeIndex.Equals(other.nodeIndex);
			}

			// Token: 0x0600212C RID: 8492 RVA: 0x0008E928 File Offset: 0x0008CB28
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is DirectorCore.NodeReference)
				{
					DirectorCore.NodeReference other = (DirectorCore.NodeReference)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x0600212D RID: 8493 RVA: 0x0008E954 File Offset: 0x0008CB54
			public override int GetHashCode()
			{
				return ((this.nodeGraph != null) ? this.nodeGraph.GetHashCode() : 0) * 397 ^ this.nodeIndex.GetHashCode();
			}

			// Token: 0x0400267A RID: 9850
			public readonly NodeGraph nodeGraph;

			// Token: 0x0400267B RID: 9851
			public readonly NodeGraph.NodeIndex nodeIndex;
		}

		// Token: 0x020006A4 RID: 1700
		public enum MonsterSpawnDistance
		{
			// Token: 0x0400267D RID: 9853
			Standard,
			// Token: 0x0400267E RID: 9854
			Close,
			// Token: 0x0400267F RID: 9855
			Far
		}
	}
}
