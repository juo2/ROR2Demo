using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200080E RID: 2062
	public class PlayerSpawnInhibitor : MonoBehaviour
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000BF2C6 File Offset: 0x000BD4C6
		public static ReadOnlyCollection<PlayerSpawnInhibitor> readOnlyInstancesList
		{
			get
			{
				return PlayerSpawnInhibitor._readOnlyInstancesList;
			}
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000BF2CD File Offset: 0x000BD4CD
		private void OnEnable()
		{
			PlayerSpawnInhibitor.instancesList.Add(this);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000BF2DA File Offset: 0x000BD4DA
		private void OnDisable()
		{
			PlayerSpawnInhibitor.instancesList.Remove(this);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000BF2E8 File Offset: 0x000BD4E8
		public bool IsInhibiting(NodeGraph nodeGraph, NodeGraph.NodeIndex nodeIndex)
		{
			Vector3 b;
			return nodeGraph && nodeGraph.GetNodePosition(nodeIndex, out b) && (this.center.position - b).sqrMagnitude < this.radius * this.radius;
		}

		// Token: 0x04002EF4 RID: 12020
		private static List<PlayerSpawnInhibitor> instancesList = new List<PlayerSpawnInhibitor>();

		// Token: 0x04002EF5 RID: 12021
		private static ReadOnlyCollection<PlayerSpawnInhibitor> _readOnlyInstancesList = new ReadOnlyCollection<PlayerSpawnInhibitor>(PlayerSpawnInhibitor.instancesList);

		// Token: 0x04002EF6 RID: 12022
		[SerializeField]
		private float radius;

		// Token: 0x04002EF7 RID: 12023
		[SerializeField]
		private Transform center;
	}
}
