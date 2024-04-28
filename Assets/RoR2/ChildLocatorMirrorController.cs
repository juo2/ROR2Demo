using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000656 RID: 1622
	public class ChildLocatorMirrorController : MonoBehaviour
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0008713D File Offset: 0x0008533D
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x00087145 File Offset: 0x00085345
		public ChildLocator referenceLocator
		{
			get
			{
				return this._referenceLocator;
			}
			set
			{
				this._referenceLocator = value;
				this.mirrorPairs.Clear();
				if (this._referenceLocator && this._targetLocator)
				{
					this.RebuildMirrorPairs();
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00087179 File Offset: 0x00085379
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x00087181 File Offset: 0x00085381
		public ChildLocator targetLocator
		{
			get
			{
				return this._targetLocator;
			}
			set
			{
				this._targetLocator = value;
				this.mirrorPairs.Clear();
				if (this._referenceLocator && this._targetLocator)
				{
					this.RebuildMirrorPairs();
				}
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000871B5 File Offset: 0x000853B5
		private void Start()
		{
			if (this._referenceLocator && this._targetLocator)
			{
				this.RebuildMirrorPairs();
			}
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000871D8 File Offset: 0x000853D8
		private void FixedUpdate()
		{
			foreach (ChildLocatorMirrorController.MirrorPair mirrorPair in this.mirrorPairs)
			{
				if (mirrorPair.referenceTransform && mirrorPair.targetTransform)
				{
					mirrorPair.targetTransform.position = mirrorPair.referenceTransform.position;
					mirrorPair.targetTransform.rotation = mirrorPair.referenceTransform.rotation;
				}
			}
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0008726C File Offset: 0x0008546C
		private void RebuildMirrorPairs()
		{
			this.mirrorPairs.Clear();
			for (int i = 0; i < this._targetLocator.Count; i++)
			{
				string childName = this._targetLocator.FindChildName(i);
				Transform transform = this._targetLocator.FindChild(i);
				Transform transform2 = this._referenceLocator.FindChild(childName);
				if (transform && transform2)
				{
					this.mirrorPairs.Add(new ChildLocatorMirrorController.MirrorPair
					{
						targetTransform = transform,
						referenceTransform = transform2
					});
				}
			}
		}

		// Token: 0x040024FA RID: 9466
		[SerializeField]
		[Tooltip("The ChildLocator we are using are a reference to GET the transform information")]
		private ChildLocator _referenceLocator;

		// Token: 0x040024FB RID: 9467
		[SerializeField]
		[Tooltip("The ChildLocator we are targeting to SET the transform information")]
		private ChildLocator _targetLocator;

		// Token: 0x040024FC RID: 9468
		private List<ChildLocatorMirrorController.MirrorPair> mirrorPairs = new List<ChildLocatorMirrorController.MirrorPair>();

		// Token: 0x02000657 RID: 1623
		private class MirrorPair
		{
			// Token: 0x040024FD RID: 9469
			public Transform referenceTransform;

			// Token: 0x040024FE RID: 9470
			public Transform targetTransform;
		}
	}
}
