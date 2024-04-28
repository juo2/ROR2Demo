using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000772 RID: 1906
	public class InstantiatePrefabBehavior : MonoBehaviour
	{
		// Token: 0x06002798 RID: 10136 RVA: 0x000ABF90 File Offset: 0x000AA190
		public void Start()
		{
			if (this.instantiateOnStart)
			{
				this.InstantiatePrefab();
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000ABFA0 File Offset: 0x000AA1A0
		public void InstantiatePrefab()
		{
			if (!this.networkedPrefab || NetworkServer.active)
			{
				Vector3 position = this.targetTransform ? this.targetTransform.position : Vector3.zero;
				Quaternion rotation = this.copyTargetRotation ? this.targetTransform.rotation : Quaternion.identity;
				Transform parent = this.parentToTarget ? this.targetTransform : null;
				GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation, parent);
				if (this.networkedPrefab)
				{
					NetworkServer.Spawn(obj);
				}
			}
		}

		// Token: 0x04002B8A RID: 11146
		[Tooltip("The prefab to instantiate.")]
		public GameObject prefab;

		// Token: 0x04002B8B RID: 11147
		[Tooltip("The object upon which the prefab will be positioned.")]
		public Transform targetTransform;

		// Token: 0x04002B8C RID: 11148
		[Tooltip("The transform upon which to instantiate the prefab.")]
		public bool copyTargetRotation;

		// Token: 0x04002B8D RID: 11149
		[Tooltip("Whether or not to parent the instantiated prefab to the specified transform.")]
		public bool parentToTarget;

		// Token: 0x04002B8E RID: 11150
		[Tooltip("Whether or not this is a networked prefab. If so, this will only run on the server, and will be spawned over the network.")]
		public bool networkedPrefab;

		// Token: 0x04002B8F RID: 11151
		[Tooltip("Whether or not to instantiate this prefab. If so, this will only run on the server, and will be spawned over the network.")]
		public bool instantiateOnStart = true;
	}
}
