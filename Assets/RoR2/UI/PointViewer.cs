using System;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D69 RID: 3433
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(RectTransform))]
	public class PointViewer : MonoBehaviour, ILayoutGroup, ILayoutController
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x001451F5 File Offset: 0x001433F5
		// (set) Token: 0x06004EB3 RID: 20147 RVA: 0x001451FD File Offset: 0x001433FD
		private protected RectTransform rectTransform { protected get; private set; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x00145206 File Offset: 0x00143406
		// (set) Token: 0x06004EB5 RID: 20149 RVA: 0x0014520E File Offset: 0x0014340E
		private protected Canvas canvas { protected get; private set; }

		// Token: 0x06004EB6 RID: 20150 RVA: 0x00145217 File Offset: 0x00143417
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.canvas = base.GetComponent<Canvas>();
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x00145236 File Offset: 0x00143436
		private void Start()
		{
			this.FindCamera();
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0014523E File Offset: 0x0014343E
		private void Update()
		{
			this.SetDirty();
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00145248 File Offset: 0x00143448
		public GameObject AddElement(PointViewer.AddElementRequest request)
		{
			if (!request.target || !request.elementPrefab)
			{
				return null;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(request.elementPrefab, this.rectTransform);
			StructAllocator<PointViewer.ElementInfo>.Ptr ptr = this.elementInfoAllocator.Alloc();
			ref PointViewer.ElementInfo @ref = ref this.elementInfoAllocator.GetRef(ptr);
			@ref.targetTransform = request.target;
			@ref.targetWorldVerticalOffset = request.targetWorldVerticalOffset;
			@ref.targetWorldDiameter = request.targetWorldDiameter;
			@ref.targetLastKnownPosition = request.target.position;
			@ref.scaleWithDistance = request.scaleWithDistance;
			@ref.elementInstance = gameObject;
			@ref.elementRectTransform = (RectTransform)gameObject.transform;
			@ref.elementCanvasGroup = gameObject.GetComponent<CanvasGroup>();
			this.elementToElementInfo.Add(gameObject, ptr);
			return gameObject;
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00145314 File Offset: 0x00143514
		public void RemoveElement(GameObject elementInstance)
		{
			StructAllocator<PointViewer.ElementInfo>.Ptr ptr;
			if (this.elementToElementInfo.TryGetValue(elementInstance, out ptr))
			{
				this.elementToElementInfo.Remove(elementInstance);
				this.elementInfoAllocator.Free(ptr);
				UnityEngine.Object.Destroy(elementInstance);
			}
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0014535C File Offset: 0x0014355C
		public void RemoveAll()
		{
			foreach (KeyValuePair<UnityObjectWrapperKey<GameObject>, StructAllocator<PointViewer.ElementInfo>.Ptr> keyValuePair in this.elementToElementInfo)
			{
				UnityEngine.Object obj = keyValuePair.Key;
				StructAllocator<PointViewer.ElementInfo>.Ptr value = keyValuePair.Value;
				this.elementInfoAllocator.Free(value);
				UnityEngine.Object.Destroy(obj);
			}
			this.elementToElementInfo.Clear();
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x001453DC File Offset: 0x001435DC
		private void FindCamera()
		{
			this.uiCamController = this.canvas.rootCanvas.worldCamera.GetComponent<UICamera>();
			this.sceneCam = (this.uiCamController ? this.uiCamController.cameraRigController.sceneCam : null);
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0014542A File Offset: 0x0014362A
		private void SetDirty()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (!CanvasUpdateRegistry.IsRebuildingLayout())
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			}
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x00145448 File Offset: 0x00143648
		protected void UpdateAllElementPositions()
		{
			if (!this.uiCamController || !this.sceneCam)
			{
				return;
			}
			Camera camera = this.uiCamController.camera;
			Vector2 size = this.rectTransform.rect.size;
			float num = this.sceneCam.fieldOfView * 0.017453292f;
			float num2 = 1f / num;
			foreach (KeyValuePair<UnityObjectWrapperKey<GameObject>, StructAllocator<PointViewer.ElementInfo>.Ptr> keyValuePair in this.elementToElementInfo)
			{
				keyValuePair.Key;
				StructAllocator<PointViewer.ElementInfo>.Ptr value = keyValuePair.Value;
				ref PointViewer.ElementInfo @ref = ref this.elementInfoAllocator.GetRef(value);
				if (@ref.targetTransform)
				{
					@ref.targetLastKnownPosition = @ref.targetTransform.position;
				}
				Vector3 targetLastKnownPosition = @ref.targetLastKnownPosition;
				targetLastKnownPosition.y += @ref.targetWorldVerticalOffset;
				Vector3 vector = this.sceneCam.WorldToViewportPoint(targetLastKnownPosition);
				float z = vector.z;
				Vector3 v = camera.ViewportToScreenPoint(vector);
				Vector2 v2;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, v, camera, out v2);
				Vector3 localPosition = v2;
				localPosition.z = ((z >= 0f) ? 0f : -1f);
				@ref.elementRectTransform.localPosition = localPosition;
				if (@ref.scaleWithDistance)
				{
					float d = @ref.targetWorldDiameter * num2 / z;
					@ref.elementRectTransform.sizeDelta = d * size;
				}
			}
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x001455F8 File Offset: 0x001437F8
		public void SetLayoutHorizontal()
		{
			this.UpdateAllElementPositions();
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x000026ED File Offset: 0x000008ED
		public void SetLayoutVertical()
		{
		}

		// Token: 0x04004B66 RID: 19302
		private UICamera uiCamController;

		// Token: 0x04004B67 RID: 19303
		private Camera sceneCam;

		// Token: 0x04004B68 RID: 19304
		private StructAllocator<PointViewer.ElementInfo> elementInfoAllocator = new StructAllocator<PointViewer.ElementInfo>(16U);

		// Token: 0x04004B69 RID: 19305
		private Dictionary<UnityObjectWrapperKey<GameObject>, StructAllocator<PointViewer.ElementInfo>.Ptr> elementToElementInfo = new Dictionary<UnityObjectWrapperKey<GameObject>, StructAllocator<PointViewer.ElementInfo>.Ptr>();

		// Token: 0x02000D6A RID: 3434
		private struct ElementInfo
		{
			// Token: 0x04004B6A RID: 19306
			public Transform targetTransform;

			// Token: 0x04004B6B RID: 19307
			public Vector3 targetLastKnownPosition;

			// Token: 0x04004B6C RID: 19308
			public float targetWorldDiameter;

			// Token: 0x04004B6D RID: 19309
			public float targetWorldVerticalOffset;

			// Token: 0x04004B6E RID: 19310
			public bool scaleWithDistance;

			// Token: 0x04004B6F RID: 19311
			public GameObject elementInstance;

			// Token: 0x04004B70 RID: 19312
			public RectTransform elementRectTransform;

			// Token: 0x04004B71 RID: 19313
			public CanvasGroup elementCanvasGroup;
		}

		// Token: 0x02000D6B RID: 3435
		public struct AddElementRequest
		{
			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x00145620 File Offset: 0x00143820
			// (set) Token: 0x06004EC3 RID: 20163 RVA: 0x0014562E File Offset: 0x0014382E
			public float targetWorldRadius
			{
				get
				{
					return this.targetWorldDiameter * 0.5f;
				}
				set
				{
					this.targetWorldDiameter = value * 2f;
				}
			}

			// Token: 0x04004B72 RID: 19314
			public GameObject elementPrefab;

			// Token: 0x04004B73 RID: 19315
			public Transform target;

			// Token: 0x04004B74 RID: 19316
			public float targetWorldVerticalOffset;

			// Token: 0x04004B75 RID: 19317
			public float targetWorldDiameter;

			// Token: 0x04004B76 RID: 19318
			public bool scaleWithDistance;
		}
	}
}
