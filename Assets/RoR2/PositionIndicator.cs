using System;
using System.Collections.Generic;
using RoR2.ConVar;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008D7 RID: 2263
	[DisallowMultipleComponent]
	public class PositionIndicator : MonoBehaviour
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000D5EF6 File Offset: 0x000D40F6
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x000D5EFE File Offset: 0x000D40FE
		public Vector3 defaultPosition { get; set; }

		// Token: 0x060032B5 RID: 12981 RVA: 0x000D5F07 File Offset: 0x000D4107
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000D5F18 File Offset: 0x000D4118
		private void Start()
		{
			if (!this.generateDefaultPosition)
			{
				this.generateDefaultPosition = true;
				this.defaultPosition = base.transform.position;
			}
			if (this.targetTransform)
			{
				this.yOffset = PositionIndicator.CalcHeadOffset(this.targetTransform) + 1f;
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000D5F6C File Offset: 0x000D416C
		private static float CalcHeadOffset(Transform transform)
		{
			Collider component = transform.GetComponent<Collider>();
			if (component)
			{
				return component.bounds.extents.y;
			}
			return 0f;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000D5FA1 File Offset: 0x000D41A1
		private void OnEnable()
		{
			PositionIndicator.instancesList.Add(this);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000D5FAE File Offset: 0x000D41AE
		private void OnDisable()
		{
			PositionIndicator.instancesList.Remove(this);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000D5FBC File Offset: 0x000D41BC
		private void OnValidate()
		{
			if (this.insideViewObject && this.insideViewObject.GetComponentInChildren<PositionIndicator>())
			{
				Debug.LogError("insideViewObject may not be assigned another object with another PositionIndicator in its heirarchy!");
				this.insideViewObject = null;
			}
			if (this.outsideViewObject && this.outsideViewObject.GetComponentInChildren<PositionIndicator>())
			{
				Debug.LogError("outsideViewObject may not be assigned another object with another PositionIndicator in its heirarchy!");
				this.outsideViewObject = null;
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000D6029 File Offset: 0x000D4229
		static PositionIndicator()
		{
			UICamera.onUICameraPreCull += PositionIndicator.UpdatePositions;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000D6060 File Offset: 0x000D4260
		private static void UpdatePositions(UICamera uiCamera)
		{
			Camera sceneCam = uiCamera.cameraRigController.sceneCam;
			Camera camera = uiCamera.camera;
			Rect pixelRect = camera.pixelRect;
			Vector2 center = pixelRect.center;
			pixelRect.size *= 0.95f;
			pixelRect.center = center;
			Vector2 center2 = pixelRect.center;
			float num = 1f / (pixelRect.width * 0.5f);
			float num2 = 1f / (pixelRect.height * 0.5f);
			Quaternion rotation = uiCamera.transform.rotation;
			CameraRigController cameraRigController = uiCamera.cameraRigController;
			Transform y = null;
			if (cameraRigController && cameraRigController.target)
			{
				CharacterBody component = cameraRigController.target.GetComponent<CharacterBody>();
				if (component)
				{
					y = component.coreTransform;
				}
				else
				{
					y = cameraRigController.target.transform;
				}
			}
			for (int i = 0; i < PositionIndicator.instancesList.Count; i++)
			{
				PositionIndicator positionIndicator = PositionIndicator.instancesList[i];
				bool flag = false;
				bool flag2 = false;
				bool active = false;
				if (!HUD.cvHudEnable.value)
				{
					positionIndicator.insideViewObject.SetActive(false);
					positionIndicator.outsideViewObject.SetActive(false);
					positionIndicator.alwaysVisibleObject.SetActive(false);
				}
				else
				{
					float num3 = 0f;
					if (PositionIndicator.cvPositionIndicatorsEnable.value)
					{
						Vector3 a = positionIndicator.targetTransform ? positionIndicator.targetTransform.position : positionIndicator.defaultPosition;
						if (!positionIndicator.targetTransform || (positionIndicator.targetTransform && positionIndicator.targetTransform != y))
						{
							active = true;
							Vector3 vector = sceneCam.WorldToScreenPoint(a + new Vector3(0f, positionIndicator.yOffset, 0f));
							bool flag3 = vector.z <= 0f;
							bool flag4 = !flag3 && pixelRect.Contains(vector);
							if (!flag4)
							{
								Vector2 vector2 = vector - center2;
								float a2 = Mathf.Abs(vector2.x * num);
								float b = Mathf.Abs(vector2.y * num2);
								float d = Mathf.Max(a2, b);
								vector2 /= d;
								vector2 *= (flag3 ? -1f : 1f);
								vector = vector2 + center2;
								if (positionIndicator.shouldRotateOutsideViewObject)
								{
									num3 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
								}
							}
							flag = flag4;
							flag2 = !flag4;
							vector.z = 1f;
							Vector3 position = camera.ScreenToWorldPoint(vector);
							positionIndicator.transform.SetPositionAndRotation(position, rotation);
						}
					}
					if (positionIndicator.alwaysVisibleObject)
					{
						positionIndicator.alwaysVisibleObject.SetActive(active);
					}
					if (positionIndicator.insideViewObject == positionIndicator.outsideViewObject)
					{
						if (positionIndicator.insideViewObject)
						{
							positionIndicator.insideViewObject.SetActive(flag || flag2);
						}
					}
					else
					{
						if (positionIndicator.insideViewObject)
						{
							positionIndicator.insideViewObject.SetActive(flag);
						}
						if (positionIndicator.outsideViewObject)
						{
							positionIndicator.outsideViewObject.SetActive(flag2);
							if (flag2 && positionIndicator.shouldRotateOutsideViewObject)
							{
								positionIndicator.outsideViewObject.transform.localEulerAngles = new Vector3(0f, 0f, num3 + positionIndicator.outsideViewRotationOffset);
							}
						}
					}
				}
			}
		}

		// Token: 0x040033DC RID: 13276
		public Transform targetTransform;

		// Token: 0x040033DD RID: 13277
		private new Transform transform;

		// Token: 0x040033DE RID: 13278
		private static readonly List<PositionIndicator> instancesList = new List<PositionIndicator>();

		// Token: 0x040033DF RID: 13279
		[Tooltip("The child object to enable when the target is within the frame.")]
		public GameObject insideViewObject;

		// Token: 0x040033E0 RID: 13280
		[Tooltip("The child object to enable when the target is outside the frame.")]
		public GameObject outsideViewObject;

		// Token: 0x040033E1 RID: 13281
		[Tooltip("The child object to ALWAYS enable, IF its not my own position indicator.")]
		public GameObject alwaysVisibleObject;

		// Token: 0x040033E2 RID: 13282
		[Tooltip("Whether or not outsideViewObject should be rotated to point to the target.")]
		public bool shouldRotateOutsideViewObject;

		// Token: 0x040033E3 RID: 13283
		[Tooltip("The offset to apply to the rotation of the outside view object when shouldRotateOutsideViewObject is set.")]
		public float outsideViewRotationOffset;

		// Token: 0x040033E4 RID: 13284
		private float yOffset;

		// Token: 0x040033E5 RID: 13285
		private bool generateDefaultPosition;

		// Token: 0x040033E7 RID: 13287
		private static BoolConVar cvPositionIndicatorsEnable = new BoolConVar("position_indicators_enable", ConVarFlags.None, "1", "Enables/Disables position indicators for allies, bosses, pings, etc.");
	}
}
