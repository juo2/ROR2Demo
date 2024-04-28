using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000788 RID: 1928
	[RequireComponent(typeof(ItemDisplay))]
	public class ItemFollower : MonoBehaviour
	{
		// Token: 0x06002864 RID: 10340 RVA: 0x000AF66C File Offset: 0x000AD86C
		private void Start()
		{
			this.itemDisplay = base.GetComponent<ItemDisplay>();
			this.followerLineRenderer = base.GetComponent<LineRenderer>();
			this.Rebuild();
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000AF68C File Offset: 0x000AD88C
		private void Rebuild()
		{
			if (this.itemDisplay.GetVisibilityLevel() == VisibilityLevel.Invisible)
			{
				if (this.followerInstance)
				{
					UnityEngine.Object.Destroy(this.followerInstance);
				}
				if (this.followerLineRenderer)
				{
					this.followerLineRenderer.enabled = false;
					return;
				}
			}
			else
			{
				if (!this.followerInstance)
				{
					this.followerInstance = UnityEngine.Object.Instantiate<GameObject>(this.followerPrefab, this.targetObject.transform.position, Quaternion.identity);
					this.followerInstance.transform.localScale = base.transform.localScale;
					if (this.followerCurve)
					{
						this.v0 = this.followerCurve.v0;
						this.v1 = this.followerCurve.v1;
					}
				}
				if (this.followerLineRenderer)
				{
					this.followerLineRenderer.enabled = true;
				}
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000AF774 File Offset: 0x000AD974
		private void Update()
		{
			this.Rebuild();
			if (this.followerInstance)
			{
				Transform transform = this.followerInstance.transform;
				Transform transform2 = this.targetObject.transform;
				transform.position = Vector3.SmoothDamp(transform.position, transform2.position, ref this.velocityDistance, this.distanceDampTime);
				transform.rotation = transform2.rotation;
				if (this.followerCurve)
				{
					this.followerCurve.v0 = base.transform.TransformVector(this.v0);
					this.followerCurve.v1 = transform.TransformVector(this.v1);
					this.followerCurve.p1 = transform.position;
				}
			}
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000AF82F File Offset: 0x000ADA2F
		private void OnDestroy()
		{
			if (this.followerInstance)
			{
				UnityEngine.Object.Destroy(this.followerInstance);
			}
		}

		// Token: 0x04002C0E RID: 11278
		public GameObject followerPrefab;

		// Token: 0x04002C0F RID: 11279
		public GameObject targetObject;

		// Token: 0x04002C10 RID: 11280
		public BezierCurveLine followerCurve;

		// Token: 0x04002C11 RID: 11281
		public LineRenderer followerLineRenderer;

		// Token: 0x04002C12 RID: 11282
		public float distanceDampTime;

		// Token: 0x04002C13 RID: 11283
		public float distanceMaxSpeed;

		// Token: 0x04002C14 RID: 11284
		private ItemDisplay itemDisplay;

		// Token: 0x04002C15 RID: 11285
		private Vector3 velocityDistance;

		// Token: 0x04002C16 RID: 11286
		private Vector3 v0;

		// Token: 0x04002C17 RID: 11287
		private Vector3 v1;

		// Token: 0x04002C18 RID: 11288
		[HideInInspector]
		public GameObject followerInstance;
	}
}
