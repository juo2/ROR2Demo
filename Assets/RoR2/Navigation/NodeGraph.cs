using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using HG;
using Unity.Collections;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B44 RID: 2884
	[CreateAssetMenu(menuName = "RoR2/NodeGraph")]
	[PreferBinarySerialization]
	public class NodeGraph : ScriptableObject
	{
		// Token: 0x060041A3 RID: 16803 RVA: 0x0010FF38 File Offset: 0x0010E138
		private void OnNodeCountChanged()
		{
			this.boolPerNodePool.lengthOfArrays = this.nodes.Length;
			this.floatPerNodePool.lengthOfArrays = this.nodes.Length;
			this.nodePerNodePool.lengthOfArrays = this.nodes.Length;
			this.linkPerNodePool.lengthOfArrays = this.nodes.Length;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x0010FF91 File Offset: 0x0010E191
		private void Awake()
		{
			this.OnNodeCountChanged();
			this.RebuildBlockMap();
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x0010FFA0 File Offset: 0x0010E1A0
		private void RebuildBlockMap()
		{
			NativeArray<NodeGraph.NodeIndex> nativeArray = new NativeArray<NodeGraph.NodeIndex>(this.nodes.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < this.nodes.Length; i++)
			{
				nativeArray[i] = new NodeGraph.NodeIndex(i);
			}
			try
			{
				this.blockMap.Set<NativeArrayIListWrapper<NodeGraph.NodeIndex>>(new NativeArrayIListWrapper<NodeGraph.NodeIndex>(nativeArray), nativeArray.Length, new NodeGraph.NodePositionGetter(this));
			}
			finally
			{
				nativeArray.Dispose();
			}
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x00110018 File Offset: 0x0010E218
		public void Clear()
		{
			this.nodes = Array.Empty<NodeGraph.Node>();
			this.links = Array.Empty<NodeGraph.Link>();
			this.gateNames.Clear();
			this.gateNames.Add("");
			this.blockMap.Reset();
			this.OnNodeCountChanged();
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00110068 File Offset: 0x0010E268
		public void SetNodes(ReadOnlyCollection<MapNode> mapNodes, ReadOnlyCollection<SerializableBitArray> lineOfSightMasks)
		{
			this.Clear();
			Dictionary<MapNode, NodeGraph.NodeIndex> dictionary = new Dictionary<MapNode, NodeGraph.NodeIndex>();
			List<NodeGraph.Node> list = new List<NodeGraph.Node>();
			List<NodeGraph.Link> list2 = new List<NodeGraph.Link>();
			for (int i = 0; i < mapNodes.Count; i++)
			{
				MapNode key = mapNodes[i];
				dictionary[key] = new NodeGraph.NodeIndex(i);
			}
			for (int j = 0; j < mapNodes.Count; j++)
			{
				MapNode mapNode = mapNodes[j];
				NodeGraph.NodeIndex nodeIndexA = dictionary[mapNode];
				int count = list2.Count;
				for (int k = 0; k < mapNode.links.Count; k++)
				{
					MapNode.Link link = mapNode.links[k];
					if (!dictionary.ContainsKey(link.nodeB))
					{
						Debug.LogErrorFormat(link.nodeB, "[{0}] Node {1} was not registered.", new object[]
						{
							k,
							link.nodeB
						});
					}
					list2.Add(new NodeGraph.Link
					{
						nodeIndexA = nodeIndexA,
						nodeIndexB = dictionary[link.nodeB],
						distanceScore = link.distanceScore,
						minJumpHeight = link.minJumpHeight,
						hullMask = link.hullMask,
						jumpHullMask = link.jumpHullMask,
						gateIndex = this.RegisterGateName(link.gateName)
					});
				}
				HullMask hullMask = mapNode.forbiddenHulls;
				for (HullClassification hullClassification = HullClassification.Human; hullClassification < HullClassification.Count; hullClassification++)
				{
					bool flag = false;
					int num = 1 << (int)hullClassification;
					List<MapNode.Link> list3 = mapNode.links;
					for (int l = 0; l < list3.Count; l++)
					{
						if ((list3[l].hullMask & num) != 0)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						hullMask |= (HullMask)num;
					}
				}
				list.Add(new NodeGraph.Node
				{
					position = mapNode.transform.position,
					linkListIndex = new NodeGraph.LinkListIndex
					{
						index = count,
						size = (uint)mapNode.links.Count
					},
					forbiddenHulls = hullMask,
					flags = mapNode.flags,
					lineOfSightMask = new SerializableBitArray(lineOfSightMasks[j]),
					gateIndex = this.RegisterGateName(mapNode.gateName)
				});
			}
			this.nodes = list.ToArray();
			this.links = list2.ToArray();
			this.OnNodeCountChanged();
			this.RebuildBlockMap();
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x001102EC File Offset: 0x0010E4EC
		public Vector3 GetQuadraticCoordinates(float t, Vector3 startPos, Vector3 apexPos, Vector3 endPos)
		{
			return Mathf.Pow(1f - t, 2f) * startPos + 2f * t * (1f - t) * apexPos + Mathf.Pow(t, 2f) * endPos;
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x00110344 File Offset: 0x0010E544
		public Mesh GenerateLinkDebugMesh(HullMask hullMask)
		{
			Mesh result;
			using (WireMeshBuilder wireMeshBuilder = new WireMeshBuilder())
			{
				foreach (NodeGraph.Link link in this.links)
				{
					if ((link.hullMask & (int)hullMask) != 0)
					{
						Vector3 position = this.nodes[link.nodeIndexA.nodeIndex].position;
						Vector3 position2 = this.nodes[link.nodeIndexB.nodeIndex].position;
						Vector3 vector = (position + position2) * 0.5f;
						bool flag = (link.jumpHullMask & (int)hullMask) != 0;
						Color color = flag ? Color.cyan : Color.green;
						if (flag)
						{
							Vector3 apexPos = vector;
							apexPos.y = position.y + link.minJumpHeight;
							int num = 8;
							Vector3 p = position;
							for (int j = 1; j <= num; j++)
							{
								if (j > num / 2)
								{
									color.a = 0.1f;
								}
								Vector3 quadraticCoordinates = this.GetQuadraticCoordinates((float)j / (float)num, position, apexPos, position2);
								wireMeshBuilder.AddLine(p, color, quadraticCoordinates, color);
								p = quadraticCoordinates;
							}
						}
						else
						{
							Color c = color;
							c.a = 0.1f;
							wireMeshBuilder.AddLine(position, color, (position + position2) * 0.5f, c);
						}
					}
				}
				result = wireMeshBuilder.GenerateMesh();
			}
			return result;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x001104C4 File Offset: 0x0010E6C4
		public void DebugDrawLinks(HullClassification hull)
		{
			int num = 1 << (int)hull;
			foreach (NodeGraph.Link link in this.links)
			{
				if ((link.hullMask & num) != 0)
				{
					Vector3 position = this.nodes[link.nodeIndexA.nodeIndex].position;
					Vector3 position2 = this.nodes[link.nodeIndexB.nodeIndex].position;
					Vector3 vector = (position + position2) * 0.5f;
					bool flag = (link.jumpHullMask & num) != 0;
					Color color = flag ? Color.cyan : Color.green;
					if (flag)
					{
						Vector3 apexPos = vector;
						apexPos.y = position.y + link.minJumpHeight;
						int num2 = 8;
						Vector3 start = position;
						for (int j = 1; j <= num2; j++)
						{
							if (j > num2 / 2)
							{
								color.a = 0.1f;
							}
							Vector3 quadraticCoordinates = this.GetQuadraticCoordinates((float)j / (float)num2, position, apexPos, position2);
							Debug.DrawLine(start, quadraticCoordinates, color, 10f);
							start = quadraticCoordinates;
						}
					}
					else
					{
						Debug.DrawLine(position, vector, color, 10f, false);
						Color color2 = color;
						color2.a = 0.1f;
						Debug.DrawLine(vector, position2, color2, 10f, false);
					}
				}
			}
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x00110618 File Offset: 0x0010E818
		public void DebugDrawPath(Vector3 startPos, Vector3 endPos)
		{
			Path path = new Path(this);
			this.ComputePath(new NodeGraph.PathRequest
			{
				startPos = startPos,
				endPos = endPos,
				path = path,
				hullClassification = HullClassification.Human
			}).Wait();
			if (path.status == PathStatus.Valid)
			{
				for (int i = 1; i < path.waypointsCount; i++)
				{
					Debug.DrawLine(this.nodes[path[i - 1].nodeIndex.nodeIndex].position, this.nodes[path[i].nodeIndex.nodeIndex].position, Color.red, 10f);
				}
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x001106D0 File Offset: 0x0010E8D0
		public void DebugHighlightNodesWithNoLinks()
		{
			foreach (NodeGraph.Node node in this.nodes)
			{
				if (node.linkListIndex.size <= 0U)
				{
					Debug.DrawRay(node.position, Vector3.up * 100f, Color.cyan, 60f);
				}
			}
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0011072C File Offset: 0x0010E92C
		public int GetNodeCount()
		{
			return this.nodes.Length;
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x00110738 File Offset: 0x0010E938
		public List<NodeGraph.NodeIndex> GetActiveNodesForHullMask(HullMask hullMask)
		{
			List<NodeGraph.NodeIndex> list = new List<NodeGraph.NodeIndex>(this.nodes.Length);
			this.GetActiveNodesForHullMask(hullMask, list);
			return list;
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x0011075C File Offset: 0x0010E95C
		public void GetActiveNodesForHullMask(HullMask hullMask, List<NodeGraph.NodeIndex> dest)
		{
			dest.Capacity = Math.Max(dest.Capacity, this.nodes.Length);
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if ((this.nodes[i].forbiddenHulls & hullMask) == HullMask.None && (this.nodes[i].gateIndex == 0 || this.openGates[(int)this.nodes[i].gateIndex]))
				{
					dest.Add(new NodeGraph.NodeIndex(i));
				}
			}
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x001107E4 File Offset: 0x0010E9E4
		public List<NodeGraph.NodeIndex> GetActiveNodesForHullMaskWithFlagConditions(HullMask hullMask, NodeFlags requiredFlags, NodeFlags forbiddenFlags)
		{
			List<NodeGraph.NodeIndex> list = new List<NodeGraph.NodeIndex>(this.nodes.Length);
			this.GetActiveNodesForHullMaskWithFlagConditions(hullMask, requiredFlags, forbiddenFlags, list);
			return list;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0011080C File Offset: 0x0010EA0C
		public void GetActiveNodesForHullMaskWithFlagConditions(HullMask hullMask, NodeFlags requiredFlags, NodeFlags forbiddenFlags, List<NodeGraph.NodeIndex> dest)
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				NodeFlags flags = this.nodes[i].flags;
				if ((flags & forbiddenFlags) == NodeFlags.None && (flags & requiredFlags) == requiredFlags && (this.nodes[i].forbiddenHulls & hullMask) == HullMask.None && (this.nodes[i].gateIndex == 0 || this.openGates[(int)this.nodes[i].gateIndex]))
				{
					dest.Add(new NodeGraph.NodeIndex(i));
				}
			}
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x001108A0 File Offset: 0x0010EAA0
		public List<NodeGraph.NodeIndex> FindNodesInRange(Vector3 position, float minRange, float maxRange, HullMask hullMask)
		{
			List<NodeGraph.NodeIndex> list = new List<NodeGraph.NodeIndex>();
			this.FindNodesInRange(position, minRange, maxRange, hullMask, list);
			return list;
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x001108C0 File Offset: 0x0010EAC0
		public void FindNodesInRange(Vector3 position, float minRange, float maxRange, HullMask hullMask, List<NodeGraph.NodeIndex> dest)
		{
			NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>>> nodeSearchFilter = NodeGraph.NodeFilters.Create<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>>>(this, NodeGraph.NodeFilters.And<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>(new NodeGraph.NodeFilters.NodeHullFilter(hullMask), default(NodeGraph.NodeFilters.NodeAvailableFilter), new NodeGraph.NodeFilters.NodeMinDistanceFilter(position, minRange)));
			this.blockMap.GetNearestItemsWhichPassFilter<NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>>>>(position, maxRange, ref nodeSearchFilter, dest);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00110904 File Offset: 0x0010EB04
		public List<NodeGraph.NodeIndex> FindNodesInRangeWithFlagConditions(Vector3 position, float minRange, float maxRange, HullMask hullMask, NodeFlags requiredFlags, NodeFlags forbiddenFlags, bool preventOverhead)
		{
			List<NodeGraph.NodeIndex> list = new List<NodeGraph.NodeIndex>();
			this.FindNodesInRangeWithFlagConditions(position, minRange, maxRange, hullMask, requiredFlags, forbiddenFlags, preventOverhead, list);
			return list;
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x0011092C File Offset: 0x0010EB2C
		public void FindNodesInRangeWithFlagConditions(Vector3 position, float minRange, float maxRange, HullMask hullMask, NodeFlags requiredFlags, NodeFlags forbiddenFlags, bool preventOverhead, List<NodeGraph.NodeIndex> dest)
		{
			NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeOverheadFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>>>>> nodeSearchFilter = NodeGraph.NodeFilters.Create<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeOverheadFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>>>>>(this, NodeGraph.NodeFilters.And<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeOverheadFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>(new NodeGraph.NodeFilters.NodeFlagsFilter(requiredFlags, forbiddenFlags), new NodeGraph.NodeFilters.NodeHullFilter(hullMask), default(NodeGraph.NodeFilters.NodeAvailableFilter), new NodeGraph.NodeFilters.NodeOverheadFilter(position, preventOverhead), new NodeGraph.NodeFilters.NodeMinDistanceFilter(position, minRange)));
			this.blockMap.GetNearestItemsWhichPassFilter<NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeOverheadFilter, NodeGraph.NodeFilters.NodeMinDistanceFilter>>>>>>(position, maxRange, ref nodeSearchFilter, dest);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x00110980 File Offset: 0x0010EB80
		public bool GetNodePosition(NodeGraph.NodeIndex nodeIndex, out Vector3 position)
		{
			if (nodeIndex != NodeGraph.NodeIndex.invalid && nodeIndex.nodeIndex < this.nodes.Length)
			{
				position = this.nodes[nodeIndex.nodeIndex].position;
				return true;
			}
			position = Vector3.zero;
			return false;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x001109D4 File Offset: 0x0010EBD4
		public bool GetNodeFlags(NodeGraph.NodeIndex nodeIndex, out NodeFlags flags)
		{
			if (nodeIndex != NodeGraph.NodeIndex.invalid && nodeIndex.nodeIndex < this.nodes.Length)
			{
				flags = this.nodes[nodeIndex.nodeIndex].flags;
				return true;
			}
			flags = NodeFlags.None;
			return false;
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x00110A14 File Offset: 0x0010EC14
		public NodeGraph.LinkIndex[] GetActiveNodeLinks(NodeGraph.NodeIndex nodeIndex)
		{
			if (nodeIndex != NodeGraph.NodeIndex.invalid && nodeIndex.nodeIndex < this.nodes.Length)
			{
				NodeGraph.LinkListIndex linkListIndex = this.nodes[nodeIndex.nodeIndex].linkListIndex;
				NodeGraph.LinkIndex[] array = new NodeGraph.LinkIndex[linkListIndex.size];
				int index = linkListIndex.index;
				int num = 0;
				while ((long)num < (long)((ulong)linkListIndex.size))
				{
					array[num] = new NodeGraph.LinkIndex
					{
						linkIndex = index++
					};
					num++;
				}
				return array;
			}
			return null;
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x00110A9C File Offset: 0x0010EC9C
		public void GetActiveNodeLinks(NodeGraph.NodeIndex nodeIndex, List<NodeGraph.LinkIndex> results)
		{
			if (nodeIndex != NodeGraph.NodeIndex.invalid && nodeIndex.nodeIndex < this.nodes.Length)
			{
				NodeGraph.LinkListIndex linkListIndex = this.nodes[nodeIndex.nodeIndex].linkListIndex;
				int index = linkListIndex.index;
				int num = 0;
				while ((long)num < (long)((ulong)linkListIndex.size))
				{
					results.Add(new NodeGraph.LinkIndex
					{
						linkIndex = index++
					});
					num++;
				}
			}
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x00110B14 File Offset: 0x0010ED14
		public bool TestNodeLineOfSight(NodeGraph.NodeIndex nodeIndexA, NodeGraph.NodeIndex nodeIndexB)
		{
			return nodeIndexA != NodeGraph.NodeIndex.invalid && nodeIndexA.nodeIndex < this.nodes.Length && nodeIndexB != NodeGraph.NodeIndex.invalid && nodeIndexB.nodeIndex < this.nodes.Length && this.nodes[nodeIndexA.nodeIndex].lineOfSightMask[nodeIndexB.nodeIndex];
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x00110B80 File Offset: 0x0010ED80
		public bool GetPositionAlongLink(NodeGraph.LinkIndex linkIndex, float t, out Vector3 position)
		{
			if (linkIndex != NodeGraph.LinkIndex.invalid && linkIndex.linkIndex < this.links.Length)
			{
				position = Vector3.LerpUnclamped(this.nodes[this.links[linkIndex.linkIndex].nodeIndexA.nodeIndex].position, this.nodes[this.links[linkIndex.linkIndex].nodeIndexB.nodeIndex].position, t);
				return true;
			}
			position = Vector3.zero;
			return false;
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x00110C1C File Offset: 0x0010EE1C
		public bool IsLinkSuitableForHull(NodeGraph.LinkIndex linkIndex, HullClassification hullClassification)
		{
			return linkIndex != NodeGraph.LinkIndex.invalid && linkIndex.linkIndex < this.links.Length && (this.links[linkIndex.linkIndex].hullMask & 1 << (int)hullClassification) != 0 && (this.links[linkIndex.linkIndex].gateIndex == 0 || this.openGates[(int)this.links[linkIndex.linkIndex].gateIndex]);
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x00110CA0 File Offset: 0x0010EEA0
		public bool IsLinkSuitableForHull(NodeGraph.LinkIndex linkIndex, HullMask hullMask)
		{
			return linkIndex != NodeGraph.LinkIndex.invalid && linkIndex.linkIndex < this.links.Length && (this.links[linkIndex.linkIndex].hullMask & (int)hullMask) != 0 && (this.links[linkIndex.linkIndex].gateIndex == 0 || this.openGates[(int)this.links[linkIndex.linkIndex].gateIndex]);
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x00110D1F File Offset: 0x0010EF1F
		public NodeGraph.NodeIndex GetLinkStartNode(NodeGraph.LinkIndex linkIndex)
		{
			if (linkIndex != NodeGraph.LinkIndex.invalid && linkIndex.linkIndex < this.links.Length)
			{
				return this.links[linkIndex.linkIndex].nodeIndexA;
			}
			return NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x00110D5A File Offset: 0x0010EF5A
		public NodeGraph.NodeIndex GetLinkEndNode(NodeGraph.LinkIndex linkIndex)
		{
			if (linkIndex != NodeGraph.LinkIndex.invalid && linkIndex.linkIndex < this.links.Length)
			{
				return this.links[linkIndex.linkIndex].nodeIndexB;
			}
			return NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x00110D98 File Offset: 0x0010EF98
		public NodeGraph.NodeIndex FindClosestNode(Vector3 position, HullClassification hullClassification, float maxDistance = float.PositiveInfinity)
		{
			NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter>> nodeSearchFilter = NodeGraph.NodeFilters.Create<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter>>(this, NodeGraph.NodeFilters.And<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter>(new NodeGraph.NodeFilters.NodeHullFilter(hullClassification), default(NodeGraph.NodeFilters.NodeAvailableFilter)));
			NodeGraph.NodeIndex result;
			if (this.blockMap.GetNearestItemWhichPassesFilter<NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter>>>(position, maxDistance, ref nodeSearchFilter, out result))
			{
				return result;
			}
			return NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x00110DDC File Offset: 0x0010EFDC
		public NodeGraph.NodeIndex FindClosestNodeWithRaycast(Vector3 position, HullClassification hullClassification, float maxDistance, int maxRaycasts)
		{
			NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeRaycastFilter>>> nodeSearchFilter = NodeGraph.NodeFilters.Create<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeRaycastFilter>>>(this, NodeGraph.NodeFilters.And<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeRaycastFilter>(new NodeGraph.NodeFilters.NodeHullFilter(hullClassification), default(NodeGraph.NodeFilters.NodeAvailableFilter), new NodeGraph.NodeFilters.NodeRaycastFilter(position, new Vector3(0f, 0.2f, 0f), 3)));
			NodeGraph.NodeIndex result;
			if (this.blockMap.GetNearestItemWhichPassesFilter<NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeRaycastFilter>>>>(position, maxDistance, ref nodeSearchFilter, out result))
			{
				return result;
			}
			return NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x00110E3C File Offset: 0x0010F03C
		public NodeGraph.NodeIndex FindClosestNodeWithFlagConditions(Vector3 position, HullClassification hullClassification, NodeFlags requiredFlags, NodeFlags forbiddenFlags, bool preventOverhead)
		{
			NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeOverheadFilter>>>> nodeSearchFilter = NodeGraph.NodeFilters.Create<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeOverheadFilter>>>>(this, NodeGraph.NodeFilters.And<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeOverheadFilter>(new NodeGraph.NodeFilters.NodeFlagsFilter(requiredFlags, forbiddenFlags), new NodeGraph.NodeFilters.NodeHullFilter(hullClassification), default(NodeGraph.NodeFilters.NodeAvailableFilter), new NodeGraph.NodeFilters.NodeOverheadFilter(position, preventOverhead)));
			NodeGraph.NodeIndex result;
			if (this.blockMap.GetNearestItemWhichPassesFilter<NodeGraph.NodeFilters.NodeSearchFilter<NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeFlagsFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeHullFilter, NodeGraph.NodeFilters.NodeCheckFilterAnd<NodeGraph.NodeFilters.NodeAvailableFilter, NodeGraph.NodeFilters.NodeOverheadFilter>>>>>(position, float.PositiveInfinity, ref nodeSearchFilter, out result))
			{
				return result;
			}
			return NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x00110E92 File Offset: 0x0010F092
		private float HeuristicCostEstimate(Vector3 startPos, Vector3 endPos)
		{
			return Vector3.Distance(startPos, endPos);
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000E8EE5 File Offset: 0x000E70E5
		private static float DistanceXZ(Vector3 a, Vector3 b)
		{
			a.y = 0f;
			b.y = 0f;
			return Vector3.Distance(a, b);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00110E9C File Offset: 0x0010F09C
		private static void ArrayRemoveNodeIndex(NodeGraph.NodeIndex[] array, NodeGraph.NodeIndex value, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (array[i] == value)
				{
					array[i] = array[count - 1];
					return;
				}
			}
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x00110ED8 File Offset: 0x0010F0D8
		public PathTask ComputePath(NodeGraph.PathRequest pathRequest)
		{
			NodeGraph.<>c__DisplayClass54_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.pathRequest = pathRequest;
			bool[] array = this.boolPerNodePool.Request();
			bool[] array2 = this.boolPerNodePool.Request();
			NodeGraph.NodeIndex[] array3 = this.nodePerNodePool.Request();
			NodeGraph.LinkIndex[] array4 = this.linkPerNodePool.Request();
			ArrayUtils.SetAll<NodeGraph.LinkIndex>(array4, NodeGraph.LinkIndex.invalid);
			float[] array5 = this.floatPerNodePool.Request();
			float[] array6 = array5;
			float positiveInfinity = float.PositiveInfinity;
			ArrayUtils.SetAll<float>(array6, positiveInfinity);
			float[] array7 = this.floatPerNodePool.Request();
			float[] array8 = array7;
			positiveInfinity = float.PositiveInfinity;
			ArrayUtils.SetAll<float>(array8, positiveInfinity);
			try
			{
				NodeGraph.NodeIndex nodeIndex;
				this.<ComputePath>g__ResolveNodePosition|54_0(CS$<>8__locals1.pathRequest.startPos, out nodeIndex, 100f, 2, ref CS$<>8__locals1);
				NodeGraph.NodeIndex nodeIndex2;
				this.<ComputePath>g__ResolveNodePosition|54_0(CS$<>8__locals1.pathRequest.endPos, out nodeIndex2, 500f, 0, ref CS$<>8__locals1);
				PathTask pathTask = new PathTask(CS$<>8__locals1.pathRequest.path);
				pathTask.status = PathTask.TaskStatus.Running;
				if (nodeIndex.nodeIndex == NodeGraph.NodeIndex.invalid.nodeIndex || nodeIndex2.nodeIndex == NodeGraph.NodeIndex.invalid.nodeIndex)
				{
					CS$<>8__locals1.pathRequest.path.Clear();
					pathTask.status = PathTask.TaskStatus.Complete;
					pathTask.wasReachable = false;
					return pathTask;
				}
				int num = 1 << (int)CS$<>8__locals1.pathRequest.hullClassification;
				array2[nodeIndex.nodeIndex] = true;
				int i = 1;
				array3[0] = nodeIndex;
				array5[nodeIndex.nodeIndex] = 0f;
				array7[nodeIndex.nodeIndex] = this.HeuristicCostEstimate(this.nodes[nodeIndex.nodeIndex].position, this.nodes[nodeIndex2.nodeIndex].position);
				while (i > 0)
				{
					NodeGraph.NodeIndex invalid = NodeGraph.NodeIndex.invalid;
					float num2 = float.PositiveInfinity;
					for (int j = 0; j < i; j++)
					{
						int nodeIndex3 = array3[j].nodeIndex;
						if (array7[nodeIndex3] <= num2)
						{
							num2 = array7[nodeIndex3];
							invalid = new NodeGraph.NodeIndex(nodeIndex3);
						}
					}
					if (invalid.nodeIndex == nodeIndex2.nodeIndex)
					{
						this.ReconstructPath(CS$<>8__locals1.pathRequest.path, array4, array4[invalid.nodeIndex], CS$<>8__locals1.pathRequest);
						pathTask.status = PathTask.TaskStatus.Complete;
						pathTask.wasReachable = true;
						return pathTask;
					}
					array2[invalid.nodeIndex] = false;
					NodeGraph.ArrayRemoveNodeIndex(array3, invalid, i);
					i--;
					array[invalid.nodeIndex] = true;
					NodeGraph.LinkListIndex linkListIndex = this.nodes[invalid.nodeIndex].linkListIndex;
					NodeGraph.LinkIndex linkIndex = new NodeGraph.LinkIndex
					{
						linkIndex = linkListIndex.index
					};
					NodeGraph.LinkIndex linkIndex2 = new NodeGraph.LinkIndex
					{
						linkIndex = linkListIndex.index + (int)linkListIndex.size
					};
					while (linkIndex.linkIndex < linkIndex2.linkIndex)
					{
						NodeGraph.Link link = this.links[linkIndex.linkIndex];
						NodeGraph.NodeIndex nodeIndexB = link.nodeIndexB;
						if (!array[nodeIndexB.nodeIndex])
						{
							if ((num & link.jumpHullMask) != 0 && this.links[linkIndex.linkIndex].minJumpHeight > 0f)
							{
								Vector3 position = this.nodes[link.nodeIndexA.nodeIndex].position;
								Vector3 position2 = this.nodes[link.nodeIndexB.nodeIndex].position;
								if (Trajectory.CalculateApex(Trajectory.CalculateInitialYSpeed(Trajectory.CalculateGroundTravelTime(CS$<>8__locals1.pathRequest.maxSpeed, NodeGraph.DistanceXZ(position, position2)), position2.y - position.y)) > CS$<>8__locals1.pathRequest.maxJumpHeight)
								{
									goto IL_460;
								}
							}
							if ((link.hullMask & num) != 0 && (link.gateIndex == 0 || this.openGates[(int)link.gateIndex]))
							{
								float distanceScore = link.distanceScore;
								float num3 = array5[invalid.nodeIndex] + distanceScore;
								if (!array2[nodeIndexB.nodeIndex])
								{
									array2[nodeIndexB.nodeIndex] = true;
									array3[i] = nodeIndexB;
									i++;
								}
								else if (num3 >= array5[nodeIndexB.nodeIndex])
								{
									goto IL_460;
								}
								array4[nodeIndexB.nodeIndex] = linkIndex;
								array5[nodeIndexB.nodeIndex] = num3;
								array7[nodeIndexB.nodeIndex] = num3 + this.HeuristicCostEstimate(this.nodes[nodeIndexB.nodeIndex].position, this.nodes[nodeIndex2.nodeIndex].position);
							}
						}
						IL_460:
						linkIndex.linkIndex++;
					}
				}
				CS$<>8__locals1.pathRequest.path.Clear();
				pathTask.status = PathTask.TaskStatus.Complete;
				return pathTask;
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("ComputePath exception:  {0}\n\n{1}", ex, ex.StackTrace));
			}
			finally
			{
				this.floatPerNodePool.Return(array7);
				this.floatPerNodePool.Return(array5);
				this.linkPerNodePool.Return(array4);
				this.nodePerNodePool.Return(array3);
				this.boolPerNodePool.Return(array2);
				this.boolPerNodePool.Return(array);
			}
			return null;
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x0011142C File Offset: 0x0010F62C
		private NodeGraph.LinkIndex Resolve(NodeGraph.LinkIndex[] cameFrom, NodeGraph.LinkIndex current)
		{
			if (current.linkIndex < 0 || current.linkIndex > this.links.Length)
			{
				Debug.LogFormat("Link {0} is out of range [0,{1})", new object[]
				{
					current.linkIndex,
					this.links.Length
				});
			}
			NodeGraph.NodeIndex nodeIndexA = this.links[current.linkIndex].nodeIndexA;
			return cameFrom[nodeIndexA.nodeIndex];
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x001114A4 File Offset: 0x0010F6A4
		private void ReconstructPath(Path path, NodeGraph.LinkIndex[] cameFrom, NodeGraph.LinkIndex current, NodeGraph.PathRequest pathRequest)
		{
			int num = 1 << (int)pathRequest.hullClassification;
			path.Clear();
			if (current != NodeGraph.LinkIndex.invalid)
			{
				path.PushWaypointToFront(this.links[current.linkIndex].nodeIndexB, 0f);
			}
			while (current != NodeGraph.LinkIndex.invalid)
			{
				NodeGraph.NodeIndex nodeIndexB = this.links[current.linkIndex].nodeIndexB;
				float minJumpHeight = 0f;
				if ((num & this.links[current.linkIndex].jumpHullMask) != 0 && this.links[current.linkIndex].minJumpHeight > 0f)
				{
					Vector3 position = this.nodes[this.links[current.linkIndex].nodeIndexA.nodeIndex].position;
					Vector3 position2 = this.nodes[this.links[current.linkIndex].nodeIndexB.nodeIndex].position;
					minJumpHeight = Trajectory.CalculateApex(Trajectory.CalculateInitialYSpeed(Trajectory.CalculateGroundTravelTime(pathRequest.maxSpeed, NodeGraph.DistanceXZ(position, position2)), position2.y - position.y));
				}
				path.PushWaypointToFront(nodeIndexB, minJumpHeight);
				if (cameFrom[this.links[current.linkIndex].nodeIndexA.nodeIndex] == NodeGraph.LinkIndex.invalid)
				{
					path.PushWaypointToFront(this.links[current.linkIndex].nodeIndexA, 0f);
				}
				current = cameFrom[this.links[current.linkIndex].nodeIndexA.nodeIndex];
			}
			path.status = PathStatus.Valid;
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x0011166C File Offset: 0x0010F86C
		private byte RegisterGateName(string gateName)
		{
			if (string.IsNullOrEmpty(gateName))
			{
				return 0;
			}
			int num = this.gateNames.IndexOf(gateName);
			if (num == -1)
			{
				num = this.gateNames.Count;
				if (num >= 256)
				{
					Debug.LogErrorFormat(this, "Nodegraph cannot have more than 255 gate names. Nodegraph={0} gateName={1}", new object[]
					{
						this,
						gateName
					});
					num = 0;
				}
				else
				{
					this.gateNames.Add(gateName);
				}
			}
			return (byte)num;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x001116D4 File Offset: 0x0010F8D4
		public bool IsGateOpen(string gateName)
		{
			int num = this.gateNames.IndexOf(gateName);
			return num != -1 && this.openGates[num];
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x001116FC File Offset: 0x0010F8FC
		public void SetGateState(string gateName, bool open)
		{
			this.TrySetGateState(gateName, open);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00111708 File Offset: 0x0010F908
		public bool TrySetGateState(string gateName, bool open)
		{
			int num = this.gateNames.IndexOf(gateName);
			if (num == -1)
			{
				return false;
			}
			this.openGates[num] = open;
			return true;
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x001117C0 File Offset: 0x0010F9C0
		[CompilerGenerated]
		private void <ComputePath>g__ResolveNodePosition|54_0(in NodeGraph.PathRequestPosition pathRequestPosition, out NodeGraph.NodeIndex dest, float maxSearchDistance, int maxRaycasts, ref NodeGraph.<>c__DisplayClass54_0 A_5)
		{
			if (pathRequestPosition.nodeIndex != null)
			{
				dest = pathRequestPosition.nodeIndex.Value;
				return;
			}
			if (pathRequestPosition.position != null)
			{
				dest = this.FindClosestNodeWithRaycast(pathRequestPosition.position.Value, A_5.pathRequest.hullClassification, maxSearchDistance, maxRaycasts);
				return;
			}
			dest = NodeGraph.NodeIndex.invalid;
		}

		// Token: 0x04003FF4 RID: 16372
		[SerializeField]
		private NodeGraph.Node[] nodes = Array.Empty<NodeGraph.Node>();

		// Token: 0x04003FF5 RID: 16373
		[SerializeField]
		private NodeGraph.Link[] links = Array.Empty<NodeGraph.Link>();

		// Token: 0x04003FF6 RID: 16374
		[SerializeField]
		private List<string> gateNames = new List<string>
		{
			""
		};

		// Token: 0x04003FF7 RID: 16375
		private bool[] openGates = new bool[256];

		// Token: 0x04003FF8 RID: 16376
		private BlockMap<NodeGraph.NodeIndex, NodeGraph.NodePositionGetter> blockMap = new BlockMap<NodeGraph.NodeIndex, NodeGraph.NodePositionGetter>();

		// Token: 0x04003FF9 RID: 16377
		private FixedSizeArrayPool<bool> boolPerNodePool = new FixedSizeArrayPool<bool>(0);

		// Token: 0x04003FFA RID: 16378
		private FixedSizeArrayPool<float> floatPerNodePool = new FixedSizeArrayPool<float>(0);

		// Token: 0x04003FFB RID: 16379
		private FixedSizeArrayPool<NodeGraph.NodeIndex> nodePerNodePool = new FixedSizeArrayPool<NodeGraph.NodeIndex>(0);

		// Token: 0x04003FFC RID: 16380
		private FixedSizeArrayPool<NodeGraph.LinkIndex> linkPerNodePool = new FixedSizeArrayPool<NodeGraph.LinkIndex>(0);

		// Token: 0x04003FFD RID: 16381
		private const float overheadDotLimit = 0.70710677f;

		// Token: 0x02000B45 RID: 2885
		[Serializable]
		public struct NodeIndex : IEquatable<NodeGraph.NodeIndex>
		{
			// Token: 0x060041CF RID: 16847 RVA: 0x0011182B File Offset: 0x0010FA2B
			public NodeIndex(int nodeIndex)
			{
				this.nodeIndex = nodeIndex;
			}

			// Token: 0x060041D0 RID: 16848 RVA: 0x00111834 File Offset: 0x0010FA34
			public static bool operator ==(NodeGraph.NodeIndex lhs, NodeGraph.NodeIndex rhs)
			{
				return lhs.nodeIndex == rhs.nodeIndex;
			}

			// Token: 0x060041D1 RID: 16849 RVA: 0x00111844 File Offset: 0x0010FA44
			public static bool operator !=(NodeGraph.NodeIndex lhs, NodeGraph.NodeIndex rhs)
			{
				return lhs.nodeIndex != rhs.nodeIndex;
			}

			// Token: 0x060041D2 RID: 16850 RVA: 0x00111857 File Offset: 0x0010FA57
			public override bool Equals(object other)
			{
				return other is NodeGraph.NodeIndex && ((NodeGraph.NodeIndex)other).nodeIndex == this.nodeIndex;
			}

			// Token: 0x060041D3 RID: 16851 RVA: 0x00111876 File Offset: 0x0010FA76
			public override int GetHashCode()
			{
				return this.nodeIndex;
			}

			// Token: 0x060041D4 RID: 16852 RVA: 0x00111834 File Offset: 0x0010FA34
			public bool Equals(NodeGraph.NodeIndex other)
			{
				return this.nodeIndex == other.nodeIndex;
			}

			// Token: 0x04003FFE RID: 16382
			public int nodeIndex;

			// Token: 0x04003FFF RID: 16383
			public static readonly NodeGraph.NodeIndex invalid = new NodeGraph.NodeIndex(-1);
		}

		// Token: 0x02000B46 RID: 2886
		[Serializable]
		public struct LinkIndex
		{
			// Token: 0x060041D6 RID: 16854 RVA: 0x0011188B File Offset: 0x0010FA8B
			public static bool operator ==(NodeGraph.LinkIndex lhs, NodeGraph.LinkIndex rhs)
			{
				return lhs.linkIndex == rhs.linkIndex;
			}

			// Token: 0x060041D7 RID: 16855 RVA: 0x0011189B File Offset: 0x0010FA9B
			public static bool operator !=(NodeGraph.LinkIndex lhs, NodeGraph.LinkIndex rhs)
			{
				return lhs.linkIndex != rhs.linkIndex;
			}

			// Token: 0x060041D8 RID: 16856 RVA: 0x001118AE File Offset: 0x0010FAAE
			public override bool Equals(object other)
			{
				return other is NodeGraph.LinkIndex && ((NodeGraph.LinkIndex)other).linkIndex == this.linkIndex;
			}

			// Token: 0x060041D9 RID: 16857 RVA: 0x001118CD File Offset: 0x0010FACD
			public override int GetHashCode()
			{
				return this.linkIndex;
			}

			// Token: 0x04004000 RID: 16384
			public int linkIndex;

			// Token: 0x04004001 RID: 16385
			public static readonly NodeGraph.LinkIndex invalid = new NodeGraph.LinkIndex
			{
				linkIndex = -1
			};
		}

		// Token: 0x02000B47 RID: 2887
		[Serializable]
		public struct LinkListIndex
		{
			// Token: 0x04004002 RID: 16386
			public int index;

			// Token: 0x04004003 RID: 16387
			public uint size;
		}

		// Token: 0x02000B48 RID: 2888
		[Serializable]
		public struct Node
		{
			// Token: 0x04004004 RID: 16388
			public Vector3 position;

			// Token: 0x04004005 RID: 16389
			public NodeGraph.LinkListIndex linkListIndex;

			// Token: 0x04004006 RID: 16390
			public HullMask forbiddenHulls;

			// Token: 0x04004007 RID: 16391
			public SerializableBitArray lineOfSightMask;

			// Token: 0x04004008 RID: 16392
			public byte gateIndex;

			// Token: 0x04004009 RID: 16393
			public NodeFlags flags;
		}

		// Token: 0x02000B49 RID: 2889
		[Serializable]
		public struct Link
		{
			// Token: 0x0400400A RID: 16394
			public NodeGraph.NodeIndex nodeIndexA;

			// Token: 0x0400400B RID: 16395
			public NodeGraph.NodeIndex nodeIndexB;

			// Token: 0x0400400C RID: 16396
			public float distanceScore;

			// Token: 0x0400400D RID: 16397
			public float maxSlope;

			// Token: 0x0400400E RID: 16398
			public float minJumpHeight;

			// Token: 0x0400400F RID: 16399
			public int hullMask;

			// Token: 0x04004010 RID: 16400
			public int jumpHullMask;

			// Token: 0x04004011 RID: 16401
			public byte gateIndex;
		}

		// Token: 0x02000B4A RID: 2890
		private struct NodePositionGetter : IPosition3Getter<NodeGraph.NodeIndex>
		{
			// Token: 0x060041DB RID: 16859 RVA: 0x001118FB File Offset: 0x0010FAFB
			public NodePositionGetter(NodeGraph nodeGraph)
			{
				this.nodeGraph = nodeGraph;
			}

			// Token: 0x060041DC RID: 16860 RVA: 0x00111904 File Offset: 0x0010FB04
			public Vector3 GetPosition3(NodeGraph.NodeIndex item)
			{
				return this.nodeGraph.nodes[item.nodeIndex].position;
			}

			// Token: 0x04004012 RID: 16402
			private readonly NodeGraph nodeGraph;
		}

		// Token: 0x02000B4B RID: 2891
		public readonly struct PathRequestPosition
		{
			// Token: 0x060041DD RID: 16861 RVA: 0x00111921 File Offset: 0x0010FB21
			private PathRequestPosition(NodeGraph.NodeIndex nodeIndex)
			{
				this.nodeIndex = new NodeGraph.NodeIndex?(nodeIndex);
				this.position = null;
			}

			// Token: 0x060041DE RID: 16862 RVA: 0x0011193B File Offset: 0x0010FB3B
			private PathRequestPosition(Vector3 position)
			{
				this.nodeIndex = null;
				this.position = new Vector3?(position);
			}

			// Token: 0x060041DF RID: 16863 RVA: 0x00111955 File Offset: 0x0010FB55
			public static implicit operator NodeGraph.PathRequestPosition(NodeGraph.NodeIndex nodeIndex)
			{
				return new NodeGraph.PathRequestPosition(nodeIndex);
			}

			// Token: 0x060041E0 RID: 16864 RVA: 0x0011195D File Offset: 0x0010FB5D
			public static implicit operator NodeGraph.PathRequestPosition(Vector3 position)
			{
				return new NodeGraph.PathRequestPosition(position);
			}

			// Token: 0x04004013 RID: 16403
			public readonly NodeGraph.NodeIndex? nodeIndex;

			// Token: 0x04004014 RID: 16404
			public readonly Vector3? position;
		}

		// Token: 0x02000B4C RID: 2892
		public class PathRequest
		{
			// Token: 0x060041E1 RID: 16865 RVA: 0x00111965 File Offset: 0x0010FB65
			public PathRequest()
			{
				this.Init();
			}

			// Token: 0x060041E2 RID: 16866 RVA: 0x00111973 File Offset: 0x0010FB73
			public void Reset()
			{
				this.Init();
			}

			// Token: 0x060041E3 RID: 16867 RVA: 0x0011197C File Offset: 0x0010FB7C
			private void Init()
			{
				this.path = null;
				this.startPos = NodeGraph.NodeIndex.invalid;
				this.endPos = NodeGraph.NodeIndex.invalid;
				this.hullClassification = HullClassification.Human;
				this.maxSlope = 0f;
				this.maxJumpHeight = 0f;
				this.maxSpeed = 0f;
			}

			// Token: 0x04004015 RID: 16405
			public Path path;

			// Token: 0x04004016 RID: 16406
			public NodeGraph.PathRequestPosition startPos;

			// Token: 0x04004017 RID: 16407
			public NodeGraph.PathRequestPosition endPos;

			// Token: 0x04004018 RID: 16408
			public HullClassification hullClassification;

			// Token: 0x04004019 RID: 16409
			public float maxSlope;

			// Token: 0x0400401A RID: 16410
			public float maxJumpHeight;

			// Token: 0x0400401B RID: 16411
			public float maxSpeed;
		}

		// Token: 0x02000B4D RID: 2893
		private static class NodeFilters
		{
			// Token: 0x060041E4 RID: 16868 RVA: 0x001119D8 File Offset: 0x0010FBD8
			public static NodeGraph.NodeFilters.NodeSearchFilter<TFilter> Create<TFilter>(NodeGraph nodeGraph, TFilter nodeCheckFilter) where TFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				return new NodeGraph.NodeFilters.NodeSearchFilter<TFilter>(nodeGraph, nodeCheckFilter);
			}

			// Token: 0x060041E5 RID: 16869 RVA: 0x001119E1 File Offset: 0x0010FBE1
			public static NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterA, TFilterB> And<TFilterA, TFilterB>(TFilterA filterA, TFilterB filterB) where TFilterA : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterB : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				return new NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterA, TFilterB>(filterA, filterB);
			}

			// Token: 0x060041E6 RID: 16870 RVA: 0x001119EA File Offset: 0x0010FBEA
			public static NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterA, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterB, TFilterC>> And<TFilterA, TFilterB, TFilterC>(TFilterA filterA, TFilterB filterB, TFilterC filterC) where TFilterA : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterB : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterC : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				return NodeGraph.NodeFilters.And<TFilterA, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterB, TFilterC>>(filterA, NodeGraph.NodeFilters.And<TFilterB, TFilterC>(filterB, filterC));
			}

			// Token: 0x060041E7 RID: 16871 RVA: 0x001119F9 File Offset: 0x0010FBF9
			public static NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterA, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterB, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterC, TFilterD>>> And<TFilterA, TFilterB, TFilterC, TFilterD>(TFilterA filterA, TFilterB filterB, TFilterC filterC, TFilterD filterD) where TFilterA : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterB : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterC : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterD : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				return NodeGraph.NodeFilters.And<TFilterA, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterB, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterC, TFilterD>>>(filterA, NodeGraph.NodeFilters.And<TFilterB, TFilterC, TFilterD>(filterB, filterC, filterD));
			}

			// Token: 0x060041E8 RID: 16872 RVA: 0x00111A09 File Offset: 0x0010FC09
			public static NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterA, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterB, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterC, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterD, TFilterE>>>> And<TFilterA, TFilterB, TFilterC, TFilterD, TFilterE>(TFilterA filterA, TFilterB filterB, TFilterC filterC, TFilterD filterD, TFilterE filterE) where TFilterA : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterB : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterC : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterD : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterE : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				return NodeGraph.NodeFilters.And<TFilterA, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterB, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterC, NodeGraph.NodeFilters.NodeCheckFilterAnd<TFilterD, TFilterE>>>>(filterA, NodeGraph.NodeFilters.And<TFilterB, TFilterC, TFilterD, TFilterE>(filterB, filterC, filterD, filterE));
			}

			// Token: 0x02000B4E RID: 2894
			public interface INodeCheckFilterComponent
			{
				// Token: 0x060041E9 RID: 16873
				bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node);
			}

			// Token: 0x02000B4F RID: 2895
			public struct NodeCheckFilterAnd<TFilterA, TFilterB> : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterA : NodeGraph.NodeFilters.INodeCheckFilterComponent where TFilterB : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041EA RID: 16874 RVA: 0x00111A1B File Offset: 0x0010FC1B
				public NodeCheckFilterAnd(TFilterA filterA, TFilterB filterB)
				{
					this.filterA = filterA;
					this.filterB = filterB;
				}

				// Token: 0x060041EB RID: 16875 RVA: 0x00111A2B File Offset: 0x0010FC2B
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					return this.filterA.CheckNode(nodeGraph, ref node) && this.filterB.CheckNode(nodeGraph, ref node);
				}

				// Token: 0x0400401C RID: 16412
				private TFilterA filterA;

				// Token: 0x0400401D RID: 16413
				private TFilterB filterB;
			}

			// Token: 0x02000B50 RID: 2896
			public struct NodeSearchFilter<TNodeCheckFilter> : IBlockMapSearchFilter<NodeGraph.NodeIndex> where TNodeCheckFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041EC RID: 16876 RVA: 0x00111A57 File Offset: 0x0010FC57
				public NodeSearchFilter(NodeGraph nodeGraph, TNodeCheckFilter nodeCheckFilter)
				{
					this.nodeGraph = nodeGraph;
					this.nodeCheckFilter = nodeCheckFilter;
				}

				// Token: 0x060041ED RID: 16877 RVA: 0x00111A67 File Offset: 0x0010FC67
				public bool CheckItem(NodeGraph.NodeIndex item, ref bool shouldFinish)
				{
					return this.nodeCheckFilter.CheckNode(this.nodeGraph, ref this.nodeGraph.nodes[item.nodeIndex]);
				}

				// Token: 0x0400401E RID: 16414
				private readonly NodeGraph nodeGraph;

				// Token: 0x0400401F RID: 16415
				private TNodeCheckFilter nodeCheckFilter;
			}

			// Token: 0x02000B51 RID: 2897
			public readonly struct NodeHullFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041EE RID: 16878 RVA: 0x00111A96 File Offset: 0x0010FC96
				public NodeHullFilter(HullMask hullMask)
				{
					this.hullMask = hullMask;
				}

				// Token: 0x060041EF RID: 16879 RVA: 0x00111A9F File Offset: 0x0010FC9F
				public NodeHullFilter(HullClassification hullClassification)
				{
					this.hullMask = (HullMask)(1 << (int)hullClassification);
				}

				// Token: 0x060041F0 RID: 16880 RVA: 0x00111AAD File Offset: 0x0010FCAD
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					return (node.forbiddenHulls & this.hullMask) == HullMask.None;
				}

				// Token: 0x04004020 RID: 16416
				private readonly HullMask hullMask;
			}

			// Token: 0x02000B52 RID: 2898
			public readonly struct NodeFlagsFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041F1 RID: 16881 RVA: 0x00111ABF File Offset: 0x0010FCBF
				public NodeFlagsFilter(NodeFlags requiredFlags, NodeFlags forbiddenFlags)
				{
					this.requiredFlags = requiredFlags;
					this.forbiddenFlags = forbiddenFlags;
				}

				// Token: 0x060041F2 RID: 16882 RVA: 0x00111AD0 File Offset: 0x0010FCD0
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					NodeFlags flags = node.flags;
					return (flags & this.forbiddenFlags) == NodeFlags.None && (flags & this.requiredFlags) == this.requiredFlags;
				}

				// Token: 0x04004021 RID: 16417
				private readonly NodeFlags requiredFlags;

				// Token: 0x04004022 RID: 16418
				private readonly NodeFlags forbiddenFlags;
			}

			// Token: 0x02000B53 RID: 2899
			public readonly struct NodeAvailableFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041F3 RID: 16883 RVA: 0x00111B00 File Offset: 0x0010FD00
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					return node.gateIndex == 0 || nodeGraph.openGates[(int)node.gateIndex];
				}
			}

			// Token: 0x02000B54 RID: 2900
			public struct NodeMinDistanceFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041F4 RID: 16884 RVA: 0x00111B19 File Offset: 0x0010FD19
				public NodeMinDistanceFilter(Vector3 position, float minDistance)
				{
					this.position = position;
					this.minDistanceSqr = minDistance * minDistance;
				}

				// Token: 0x060041F5 RID: 16885 RVA: 0x00111B2C File Offset: 0x0010FD2C
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					return (node.position - this.position).sqrMagnitude >= this.minDistanceSqr;
				}

				// Token: 0x04004023 RID: 16419
				private readonly Vector3 position;

				// Token: 0x04004024 RID: 16420
				private readonly float minDistanceSqr;
			}

			// Token: 0x02000B55 RID: 2901
			public struct NodeRaycastFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041F6 RID: 16886 RVA: 0x00111B5D File Offset: 0x0010FD5D
				public NodeRaycastFilter(Vector3 raycastOrigin, Vector3 raycastOffset, int maxRaycasts)
				{
					this.raycastOrigin = raycastOrigin + raycastOffset;
					this.raycastOffset = raycastOffset;
					this.maxRaycasts = maxRaycasts;
					this.raycastsPerformed = 0;
				}

				// Token: 0x060041F7 RID: 16887 RVA: 0x00111B84 File Offset: 0x0010FD84
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					if (this.raycastsPerformed < this.maxRaycasts)
					{
						this.raycastsPerformed++;
						RaycastHit raycastHit;
						if (Physics.Linecast(this.raycastOrigin, node.position + this.raycastOffset, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x04004025 RID: 16421
				private readonly Vector3 raycastOrigin;

				// Token: 0x04004026 RID: 16422
				private readonly Vector3 raycastOffset;

				// Token: 0x04004027 RID: 16423
				private readonly int maxRaycasts;

				// Token: 0x04004028 RID: 16424
				private int raycastsPerformed;
			}

			// Token: 0x02000B56 RID: 2902
			public struct NodeOverheadFilter : NodeGraph.NodeFilters.INodeCheckFilterComponent
			{
				// Token: 0x060041F8 RID: 16888 RVA: 0x00111BE3 File Offset: 0x0010FDE3
				public NodeOverheadFilter(Vector3 position, bool enabled)
				{
					this.position = position;
					this.enabled = enabled;
				}

				// Token: 0x060041F9 RID: 16889 RVA: 0x00111BF4 File Offset: 0x0010FDF4
				public bool CheckNode(NodeGraph nodeGraph, ref NodeGraph.Node node)
				{
					return !this.enabled || Vector3.Dot((node.position - this.position).normalized, Vector3.up) <= 0.70710677f;
				}

				// Token: 0x04004029 RID: 16425
				private Vector3 position;

				// Token: 0x0400402A RID: 16426
				private bool enabled;
			}
		}
	}
}
