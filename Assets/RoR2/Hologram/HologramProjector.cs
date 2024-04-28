using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2.Hologram
{
	// Token: 0x02000BF5 RID: 3061
	public class HologramProjector : MonoBehaviour
	{
		// Token: 0x0600457E RID: 17790 RVA: 0x00121105 File Offset: 0x0011F305
		private void Awake()
		{
			this.contentProvider = base.GetComponent<IHologramContentProvider>();
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x00121114 File Offset: 0x0011F314
		private Transform FindViewer(Vector3 position)
		{
			if (this.viewerReselectTimer > 0f)
			{
				return this.cachedViewer;
			}
			this.viewerReselectTimer = this.viewerReselectInterval;
			this.cachedViewer = null;
			float num = float.PositiveInfinity;
			ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
			int i = 0;
			int count = instances.Count;
			while (i < count)
			{
				GameObject bodyObject = instances[i].master.GetBodyObject();
				if (bodyObject)
				{
					float sqrMagnitude = (bodyObject.transform.position - position).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						num = sqrMagnitude;
						this.cachedViewer = bodyObject.transform;
					}
				}
				i++;
			}
			return this.cachedViewer;
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x001211BC File Offset: 0x0011F3BC
		private void Update()
		{
			this.viewerReselectTimer -= Time.deltaTime;
			Vector3 vector = this.hologramPivot ? this.hologramPivot.position : base.transform.position;
			this.viewer = this.FindViewer(vector);
			Vector3 b = this.viewer ? this.viewer.position : base.transform.position;
			bool flag = false;
			Vector3 forward = Vector3.zero;
			if (this.viewer)
			{
				forward = vector - b;
				if (forward.sqrMagnitude <= this.displayDistance * this.displayDistance)
				{
					flag = true;
				}
			}
			if (flag)
			{
				flag = this.contentProvider.ShouldDisplayHologram(this.viewer.gameObject);
			}
			if (flag)
			{
				if (!this.hologramContentInstance)
				{
					this.BuildHologram();
				}
				if (this.hologramContentInstance && this.contentProvider != null)
				{
					this.contentProvider.UpdateHologramContent(this.hologramContentInstance);
					if (!this.disableHologramRotation)
					{
						this.hologramContentInstance.transform.rotation = Util.SmoothDampQuaternion(this.hologramContentInstance.transform.rotation, Util.QuaternionSafeLookRotation(forward), ref this.transformDampVelocity, 0.2f);
						return;
					}
				}
			}
			else
			{
				this.DestroyHologram();
			}
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x00121304 File Offset: 0x0011F504
		private void BuildHologram()
		{
			this.DestroyHologram();
			if (this.contentProvider != null)
			{
				GameObject hologramContentPrefab = this.contentProvider.GetHologramContentPrefab();
				if (hologramContentPrefab)
				{
					this.hologramContentInstance = UnityEngine.Object.Instantiate<GameObject>(hologramContentPrefab);
					this.hologramContentInstance.transform.parent = (this.hologramPivot ? this.hologramPivot : base.transform);
					this.hologramContentInstance.transform.localPosition = Vector3.zero;
					this.hologramContentInstance.transform.localRotation = Quaternion.identity;
					this.hologramContentInstance.transform.localScale = Vector3.one;
					if (this.viewer && !this.disableHologramRotation)
					{
						Vector3 a = this.hologramPivot ? this.hologramPivot.position : base.transform.position;
						Vector3 position = this.viewer.position;
						Vector3 forward = a - this.viewer.position;
						this.hologramContentInstance.transform.rotation = Util.QuaternionSafeLookRotation(forward);
					}
					this.contentProvider.UpdateHologramContent(this.hologramContentInstance);
				}
			}
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x0012142D File Offset: 0x0011F62D
		private void DestroyHologram()
		{
			if (this.hologramContentInstance)
			{
				UnityEngine.Object.Destroy(this.hologramContentInstance);
			}
			this.hologramContentInstance = null;
		}

		// Token: 0x040043AC RID: 17324
		[Tooltip("The range in meters at which the hologram begins to display.")]
		public float displayDistance = 15f;

		// Token: 0x040043AD RID: 17325
		[Tooltip("The position at which to display the hologram.")]
		public Transform hologramPivot;

		// Token: 0x040043AE RID: 17326
		[Tooltip("Whether or not the hologram will pivot to the player")]
		public bool disableHologramRotation;

		// Token: 0x040043AF RID: 17327
		private float transformDampVelocity;

		// Token: 0x040043B0 RID: 17328
		private IHologramContentProvider contentProvider;

		// Token: 0x040043B1 RID: 17329
		private float viewerReselectTimer;

		// Token: 0x040043B2 RID: 17330
		private float viewerReselectInterval = 0.25f;

		// Token: 0x040043B3 RID: 17331
		private Transform cachedViewer;

		// Token: 0x040043B4 RID: 17332
		private Transform viewer;

		// Token: 0x040043B5 RID: 17333
		private GameObject hologramContentInstance;
	}
}
