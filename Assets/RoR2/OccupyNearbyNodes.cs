using System;
using System.Collections.Generic;
using System.Linq;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007E2 RID: 2018
	public class OccupyNearbyNodes : MonoBehaviour
	{
		// Token: 0x06002BA4 RID: 11172 RVA: 0x000BB063 File Offset: 0x000B9263
		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
			Gizmos.DrawWireSphere(base.transform.position, this.radius);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000BB099 File Offset: 0x000B9299
		private void OnEnable()
		{
			OccupyNearbyNodes.instancesList.Add(this);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000BB0A6 File Offset: 0x000B92A6
		private void OnDisable()
		{
			OccupyNearbyNodes.instancesList.Remove(this);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000BB0B4 File Offset: 0x000B92B4
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			SceneDirector.onPrePopulateSceneServer += OccupyNearbyNodes.OnSceneDirectorPrePopulateSceneServer;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000BB0C8 File Offset: 0x000B92C8
		private static void OnSceneDirectorPrePopulateSceneServer(SceneDirector sceneDirector)
		{
			DirectorCore instance = DirectorCore.instance;
			NodeGraph groundNodeGraph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Ground);
			foreach (NodeGraph.NodeIndex nodeIndex in OccupyNearbyNodes.instancesList.SelectMany((OccupyNearbyNodes v) => groundNodeGraph.FindNodesInRange(v.transform.position, 0f, v.radius, HullMask.None)).Distinct<NodeGraph.NodeIndex>().ToArray<NodeGraph.NodeIndex>())
			{
				instance.AddOccupiedNode(groundNodeGraph, nodeIndex);
			}
		}

		// Token: 0x04002E15 RID: 11797
		public float radius = 5f;

		// Token: 0x04002E16 RID: 11798
		private static readonly List<OccupyNearbyNodes> instancesList = new List<OccupyNearbyNodes>();
	}
}
