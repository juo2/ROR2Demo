using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace RoR2
{
	// Token: 0x02000A9F RID: 2719
	public class VFXHelper : IDisposable
	{
		// Token: 0x06003E8C RID: 16012 RVA: 0x00102656 File Offset: 0x00100856
		private VFXHelper()
		{
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x00102673 File Offset: 0x00100873
		public static VFXHelper Rent()
		{
			if (VFXHelper.pool.Count <= 0)
			{
				return new VFXHelper();
			}
			return VFXHelper.pool.Pop();
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x00102692 File Offset: 0x00100892
		public static VFXHelper Return(VFXHelper instance)
		{
			instance.Dispose();
			VFXHelper.pool.Push(instance);
			return null;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x001026A6 File Offset: 0x001008A6
		// (set) Token: 0x06003E90 RID: 16016 RVA: 0x001026AE File Offset: 0x001008AE
		public GameObject vfxPrefabReference
		{
			get
			{
				return this._vfxPrefabReference;
			}
			set
			{
				this._vfxPrefabReference = value;
				this._vfxPrefabAddress = null;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x001026BE File Offset: 0x001008BE
		// (set) Token: 0x06003E92 RID: 16018 RVA: 0x001026C6 File Offset: 0x001008C6
		public object vfxPrefabAddress
		{
			get
			{
				return this._vfxPrefabAddress;
			}
			set
			{
				this._vfxPrefabReference = null;
				this._vfxPrefabAddress = null;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x001026D6 File Offset: 0x001008D6
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x001026DE File Offset: 0x001008DE
		public GameObject vfxInstance { get; private set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x001026E7 File Offset: 0x001008E7
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x001026EF File Offset: 0x001008EF
		public Transform vfxInstanceTransform { get; private set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x001026F8 File Offset: 0x001008F8
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x00102700 File Offset: 0x00100900
		private VFXHelper.VFXTransformController vfxInstanceTransformController { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x00102709 File Offset: 0x00100909
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x00102718 File Offset: 0x00100918
		public bool enabled
		{
			get
			{
				return this.vfxInstance;
			}
			set
			{
				if (this.vfxInstance == value)
				{
					return;
				}
				if (value)
				{
					this.vfxInstance = null;
					this.vfxInstanceTransform = null;
					Vector3 vector = Vector3.zero;
					Quaternion quaternion = Quaternion.identity;
					Vector3 vector2 = Vector3.one;
					if (this.followedTransform)
					{
						vector = (this.useFollowedTransformPosition ? this.followedTransform.position : vector);
						quaternion = (this.useFollowedTransformRotation ? this.followedTransform.rotation : quaternion);
						vector2 = (this.useFollowedTransformScale ? this.followedTransform.lossyScale : vector2);
					}
					if (this.vfxPrefabAddress != null)
					{
						InstantiationParameters instantiateParameters = new InstantiationParameters(vector, quaternion, null);
						this.vfxInstance = Addressables.InstantiateAsync(this.vfxPrefabAddress, instantiateParameters, true).WaitForCompletion();
					}
					else if (this.vfxPrefabReference)
					{
						this.vfxInstance = UnityEngine.Object.Instantiate<GameObject>(this.vfxPrefabReference, vector, quaternion);
					}
					if (this.vfxInstance)
					{
						this.vfxInstanceTransform = this.vfxInstance.transform;
						this.vfxInstanceTransform.localScale = vector2;
						this.vfxInstanceTransformController = this.vfxInstance.AddComponent<VFXHelper.VFXTransformController>();
						this.vfxInstanceTransformController.usePosition = this.useFollowedTransformPosition;
						this.vfxInstanceTransformController.useRotation = this.useFollowedTransformRotation;
						this.vfxInstanceTransformController.useScale = this.useFollowedTransformScale;
						return;
					}
				}
				else
				{
					VfxKillBehavior.KillVfxObject(this.vfxInstance);
					this.vfxInstance = null;
					this.vfxInstanceTransform = null;
					this.vfxInstanceTransformController = null;
				}
			}
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x0010288E File Offset: 0x00100A8E
		public void Dispose()
		{
			this.enabled = false;
			this.vfxPrefabAddress = null;
			this.vfxPrefabReference = null;
			this.followedTransform = null;
			this.useFollowedTransformPosition = true;
			this.useFollowedTransformRotation = true;
			this.useFollowedTransformScale = true;
		}

		// Token: 0x04003CDF RID: 15583
		private static readonly Stack<VFXHelper> pool = new Stack<VFXHelper>();

		// Token: 0x04003CE0 RID: 15584
		private GameObject _vfxPrefabReference;

		// Token: 0x04003CE1 RID: 15585
		private object _vfxPrefabAddress;

		// Token: 0x04003CE5 RID: 15589
		public Transform followedTransform;

		// Token: 0x04003CE6 RID: 15590
		public bool useFollowedTransformPosition = true;

		// Token: 0x04003CE7 RID: 15591
		public bool useFollowedTransformRotation = true;

		// Token: 0x04003CE8 RID: 15592
		public bool useFollowedTransformScale = true;

		// Token: 0x02000AA0 RID: 2720
		private class VFXTransformController : MonoBehaviour
		{
			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06003E9D RID: 16029 RVA: 0x001028CD File Offset: 0x00100ACD
			// (set) Token: 0x06003E9E RID: 16030 RVA: 0x001028D5 File Offset: 0x00100AD5
			private protected new Transform transform { protected get; private set; }

			// Token: 0x06003E9F RID: 16031 RVA: 0x001028DE File Offset: 0x00100ADE
			private void Awake()
			{
				this.transform = base.transform;
			}

			// Token: 0x06003EA0 RID: 16032 RVA: 0x001028EC File Offset: 0x00100AEC
			private void LateUpdate()
			{
				if (this.followedTransform)
				{
					if (this.usePosition)
					{
						this.transform.position = this.followedTransform.position;
					}
					if (this.useRotation)
					{
						this.transform.rotation = this.followedTransform.rotation;
					}
					if (this.useScale)
					{
						this.transform.localScale = this.followedTransform.lossyScale;
					}
				}
			}

			// Token: 0x04003CE9 RID: 15593
			public Transform followedTransform;

			// Token: 0x04003CEA RID: 15594
			public bool usePosition;

			// Token: 0x04003CEB RID: 15595
			public bool useRotation;

			// Token: 0x04003CEC RID: 15596
			public bool useScale;
		}
	}
}
