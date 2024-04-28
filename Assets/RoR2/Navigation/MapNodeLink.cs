using System;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B40 RID: 2880
	[RequireComponent(typeof(MapNode))]
	public class MapNodeLink : MonoBehaviour
	{
		// Token: 0x0600419C RID: 16796 RVA: 0x0010FE28 File Offset: 0x0010E028
		private void OnValidate()
		{
			if (this.other == this)
			{
				Debug.LogWarning("Map node link cannot link a node to itself.");
				this.other = null;
			}
			if (this.other && this.other.GetComponentInParent<MapNodeGroup>() != base.GetComponentInParent<MapNodeGroup>())
			{
				Debug.LogWarning("Map node link cannot link to a node in a separate node group.");
				this.other = null;
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x0010FE8C File Offset: 0x0010E08C
		private void OnDrawGizmos()
		{
			if (this.other)
			{
				Vector3 position = base.transform.position;
				Vector3 position2 = this.other.transform.position;
				Vector3 vector = (position + position2) * 0.5f;
				Color yellow = Color.yellow;
				yellow.a = 0.5f;
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(position, vector);
				Gizmos.color = yellow;
				Gizmos.DrawLine(vector, position2);
			}
		}

		// Token: 0x04003FE1 RID: 16353
		public MapNode other;

		// Token: 0x04003FE2 RID: 16354
		public float minJumpHeight;

		// Token: 0x04003FE3 RID: 16355
		[Tooltip("The gate name associated with this link. If the named gate is closed, this link will not be used in pathfinding.")]
		public string gateName = "";

		// Token: 0x04003FE4 RID: 16356
		public GameObject[] objectsToEnableDuringTest;

		// Token: 0x04003FE5 RID: 16357
		public GameObject[] objectsToDisableDuringTest;
	}
}
