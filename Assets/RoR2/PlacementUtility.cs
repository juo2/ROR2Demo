using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200080A RID: 2058
	public class PlacementUtility : MonoBehaviour
	{
		// Token: 0x06002C81 RID: 11393 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000BE261 File Offset: 0x000BC461
		public void PlacePrefab(Vector3 targetPosition, Quaternion rotation)
		{
			this.prefabPlacement;
		}

		// Token: 0x04002EC8 RID: 11976
		public Transform targetParent;

		// Token: 0x04002EC9 RID: 11977
		public GameObject prefabPlacement;

		// Token: 0x04002ECA RID: 11978
		public bool normalToSurface;

		// Token: 0x04002ECB RID: 11979
		public bool flipForwardDirection;

		// Token: 0x04002ECC RID: 11980
		public float minScale = 1f;

		// Token: 0x04002ECD RID: 11981
		public float maxScale = 2f;

		// Token: 0x04002ECE RID: 11982
		public float minXRotation;

		// Token: 0x04002ECF RID: 11983
		public float maxXRotation;

		// Token: 0x04002ED0 RID: 11984
		public float minYRotation;

		// Token: 0x04002ED1 RID: 11985
		public float maxYRotation;

		// Token: 0x04002ED2 RID: 11986
		public float minZRotation;

		// Token: 0x04002ED3 RID: 11987
		public float maxZRotation;
	}
}
