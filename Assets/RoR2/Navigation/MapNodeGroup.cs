using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B3E RID: 2878
	public class MapNodeGroup : MonoBehaviour
	{
		// Token: 0x06004195 RID: 16789 RVA: 0x0010FBB8 File Offset: 0x0010DDB8
		public void Clear()
		{
			for (int i = base.transform.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.DestroyImmediate(base.transform.GetChild(i).gameObject);
			}
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x0010FBF3 File Offset: 0x0010DDF3
		public GameObject AddNode(Vector3 position)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = position;
			gameObject.transform.parent = base.transform;
			gameObject.AddComponent<MapNode>();
			gameObject.name = "MapNode";
			return gameObject;
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x0010FC2C File Offset: 0x0010DE2C
		public List<MapNode> GetNodes()
		{
			List<MapNode> result = new List<MapNode>();
			base.GetComponentsInChildren<MapNode>(false, result);
			return result;
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x0010FC48 File Offset: 0x0010DE48
		public void UpdateNoCeilingMasks()
		{
			int num = 0;
			foreach (MapNode mapNode in this.GetNodes())
			{
				mapNode.flags &= ~NodeFlags.NoCeiling;
				if (mapNode.TestNoCeiling())
				{
					num++;
					mapNode.flags |= NodeFlags.NoCeiling;
				}
			}
			Debug.LogFormat("{0} successful ceiling masks baked.", new object[]
			{
				num
			});
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0010FCDC File Offset: 0x0010DEDC
		public void UpdateTeleporterMasks()
		{
			int num = 0;
			foreach (MapNode mapNode in this.GetNodes())
			{
				mapNode.flags &= ~NodeFlags.TeleporterOK;
				if (mapNode.TestTeleporterOK())
				{
					num++;
					mapNode.flags |= NodeFlags.TeleporterOK;
				}
			}
			Debug.LogFormat("{0} successful teleporter masks baked.", new object[]
			{
				num
			});
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x0010FD70 File Offset: 0x0010DF70
		public void Bake(NodeGraph nodeGraph)
		{
			List<MapNode> nodes = this.GetNodes();
			ReadOnlyCollection<MapNode> readOnlyCollection = nodes.AsReadOnly();
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes[i].BuildLinks(readOnlyCollection, this.graphType);
			}
			List<SerializableBitArray> list = new List<SerializableBitArray>();
			for (int j = 0; j < nodes.Count; j++)
			{
				MapNode mapNode = nodes[j];
				SerializableBitArray serializableBitArray = new SerializableBitArray(nodes.Count);
				for (int k = 0; k < nodes.Count; k++)
				{
					MapNode other = nodes[k];
					serializableBitArray[k] = mapNode.TestLineOfSight(other);
				}
				list.Add(serializableBitArray);
			}
			nodeGraph.SetNodes(readOnlyCollection, list.AsReadOnly());
		}

		// Token: 0x04003FD8 RID: 16344
		public NodeGraph nodeGraph;

		// Token: 0x04003FD9 RID: 16345
		public Transform testPointA;

		// Token: 0x04003FDA RID: 16346
		public Transform testPointB;

		// Token: 0x04003FDB RID: 16347
		public HullClassification debugHullDef;

		// Token: 0x04003FDC RID: 16348
		public MapNodeGroup.GraphType graphType;

		// Token: 0x02000B3F RID: 2879
		public enum GraphType
		{
			// Token: 0x04003FDE RID: 16350
			Ground,
			// Token: 0x04003FDF RID: 16351
			Air,
			// Token: 0x04003FE0 RID: 16352
			Rail
		}
	}
}
