using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D18 RID: 3352
	[RequireComponent(typeof(Canvas))]
	public class HighlightRect : MonoBehaviour
	{
		// Token: 0x06004C5C RID: 19548 RVA: 0x0013B2A6 File Offset: 0x001394A6
		static HighlightRect()
		{
			RoR2Application.onLateUpdate += HighlightRect.UpdateAll;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0013B2CE File Offset: 0x001394CE
		private void Awake()
		{
			this.canvas = base.GetComponent<Canvas>();
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0013B2DC File Offset: 0x001394DC
		private void OnEnable()
		{
			HighlightRect.instancesList.Add(this);
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0013B2E9 File Offset: 0x001394E9
		private void OnDisable()
		{
			HighlightRect.instancesList.Remove(this);
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0013B2F8 File Offset: 0x001394F8
		private void Start()
		{
			this.highlightState = HighlightRect.HighlightState.Expanding;
			this.bottomLeftImage = this.bottomLeftRectTransform.GetComponent<Image>();
			this.bottomRightImage = this.bottomRightRectTransform.GetComponent<Image>();
			this.topLeftImage = this.topLeftRectTransform.GetComponent<Image>();
			this.topRightImage = this.topRightRectTransform.GetComponent<Image>();
			this.bottomLeftImage.sprite = this.cornerImage;
			this.bottomRightImage.sprite = this.cornerImage;
			this.topLeftImage.sprite = this.cornerImage;
			this.topRightImage.sprite = this.cornerImage;
			this.bottomLeftImage.color = this.highlightColor;
			this.bottomRightImage.color = this.highlightColor;
			this.topLeftImage.color = this.highlightColor;
			this.topRightImage.color = this.highlightColor;
			if (this.nametagRectTransform)
			{
				this.nametagText = this.nametagRectTransform.GetComponent<TextMeshProUGUI>();
				this.nametagText.color = this.highlightColor;
				this.nametagText.text = this.nametagString;
			}
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0013B418 File Offset: 0x00139618
		private static void UpdateAll()
		{
			for (int i = HighlightRect.instancesList.Count - 1; i >= 0; i--)
			{
				HighlightRect.instancesList[i].DoUpdate();
			}
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0013B44C File Offset: 0x0013964C
		private void DoUpdate()
		{
			if (!this.targetRenderer)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			switch (this.highlightState)
			{
			case HighlightRect.HighlightState.Expanding:
				this.time += Time.deltaTime;
				if (this.time >= this.expandTime)
				{
					this.time = this.expandTime;
					this.highlightState = HighlightRect.HighlightState.Holding;
				}
				break;
			case HighlightRect.HighlightState.Holding:
				if (this.destroyOnLifeEnd)
				{
					this.time += Time.deltaTime;
					if (this.time > this.maxLifeTime)
					{
						this.highlightState = HighlightRect.HighlightState.Contracting;
						this.time = this.expandTime;
					}
				}
				break;
			case HighlightRect.HighlightState.Contracting:
				this.time -= Time.deltaTime;
				if (this.time <= 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
				break;
			}
			Rect rect = HighlightRect.GUIRectWithObject(this.sceneCam, this.targetRenderer);
			Vector2 a = new Vector2(Mathf.Lerp(rect.xMin, rect.xMax, 0.5f), Mathf.Lerp(rect.yMin, rect.yMax, 0.5f));
			float t = this.curve.Evaluate(this.time / this.expandTime);
			this.bottomLeftRectTransform.anchoredPosition = Vector2.LerpUnclamped(a, new Vector2(rect.xMin, rect.yMin), t);
			this.bottomRightRectTransform.anchoredPosition = Vector2.LerpUnclamped(a, new Vector2(rect.xMax, rect.yMin), t);
			this.topLeftRectTransform.anchoredPosition = Vector2.LerpUnclamped(a, new Vector2(rect.xMin, rect.yMax), t);
			this.topRightRectTransform.anchoredPosition = Vector2.LerpUnclamped(a, new Vector2(rect.xMax, rect.yMax), t);
			if (this.nametagRectTransform)
			{
				this.nametagRectTransform.anchoredPosition = Vector2.LerpUnclamped(a, new Vector2(rect.xMin, rect.yMax), t);
			}
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0013B658 File Offset: 0x00139858
		public static Rect GUIRectWithObject(Camera cam, Renderer rend)
		{
			Vector3 center = rend.bounds.center;
			Vector3 extents = rend.bounds.extents;
			HighlightRect.extentPoints[0] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z));
			HighlightRect.extentPoints[1] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z));
			HighlightRect.extentPoints[2] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z));
			HighlightRect.extentPoints[3] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z));
			HighlightRect.extentPoints[4] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z));
			HighlightRect.extentPoints[5] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z));
			HighlightRect.extentPoints[6] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z));
			HighlightRect.extentPoints[7] = HighlightRect.WorldToGUIPoint(cam, new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z));
			Vector2 vector = HighlightRect.extentPoints[0];
			Vector2 vector2 = HighlightRect.extentPoints[0];
			foreach (Vector2 rhs in HighlightRect.extentPoints)
			{
				vector = Vector2.Min(vector, rhs);
				vector2 = Vector2.Max(vector2, rhs);
			}
			return new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x0013B8E7 File Offset: 0x00139AE7
		public static Vector2 WorldToGUIPoint(Camera cam, Vector3 world)
		{
			return cam.WorldToScreenPoint(world);
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0013B8F8 File Offset: 0x00139AF8
		public static void CreateHighlight(GameObject viewerBodyObject, Renderer targetRenderer, GameObject highlightPrefab, float overrideDuration = -1f, bool visibleToAll = false)
		{
			ReadOnlyCollection<CameraRigController> readOnlyInstancesList = CameraRigController.readOnlyInstancesList;
			int i = 0;
			int count = readOnlyInstancesList.Count;
			while (i < count)
			{
				CameraRigController cameraRigController = readOnlyInstancesList[i];
				if (!(cameraRigController.target != viewerBodyObject) || visibleToAll)
				{
					HighlightRect component = UnityEngine.Object.Instantiate<GameObject>(highlightPrefab).GetComponent<HighlightRect>();
					component.targetRenderer = targetRenderer;
					component.canvas.worldCamera = cameraRigController.uiCam;
					component.uiCam = cameraRigController.uiCam;
					component.sceneCam = cameraRigController.sceneCam;
					if (overrideDuration > 0f)
					{
						component.maxLifeTime = overrideDuration;
					}
				}
				i++;
			}
		}

		// Token: 0x0400495D RID: 18781
		public AnimationCurve curve;

		// Token: 0x0400495E RID: 18782
		public Color highlightColor;

		// Token: 0x0400495F RID: 18783
		public Sprite cornerImage;

		// Token: 0x04004960 RID: 18784
		public string nametagString;

		// Token: 0x04004961 RID: 18785
		private Image bottomLeftImage;

		// Token: 0x04004962 RID: 18786
		private Image bottomRightImage;

		// Token: 0x04004963 RID: 18787
		private Image topLeftImage;

		// Token: 0x04004964 RID: 18788
		private Image topRightImage;

		// Token: 0x04004965 RID: 18789
		private TextMeshProUGUI nametagText;

		// Token: 0x04004966 RID: 18790
		public Renderer targetRenderer;

		// Token: 0x04004967 RID: 18791
		public GameObject cameraTarget;

		// Token: 0x04004968 RID: 18792
		public RectTransform nametagRectTransform;

		// Token: 0x04004969 RID: 18793
		public RectTransform bottomLeftRectTransform;

		// Token: 0x0400496A RID: 18794
		public RectTransform bottomRightRectTransform;

		// Token: 0x0400496B RID: 18795
		public RectTransform topLeftRectTransform;

		// Token: 0x0400496C RID: 18796
		public RectTransform topRightRectTransform;

		// Token: 0x0400496D RID: 18797
		public float expandTime = 1f;

		// Token: 0x0400496E RID: 18798
		public float maxLifeTime;

		// Token: 0x0400496F RID: 18799
		public bool destroyOnLifeEnd;

		// Token: 0x04004970 RID: 18800
		private float time;

		// Token: 0x04004971 RID: 18801
		public HighlightRect.HighlightState highlightState;

		// Token: 0x04004972 RID: 18802
		private static List<HighlightRect> instancesList = new List<HighlightRect>();

		// Token: 0x04004973 RID: 18803
		private Canvas canvas;

		// Token: 0x04004974 RID: 18804
		private Camera uiCam;

		// Token: 0x04004975 RID: 18805
		private Camera sceneCam;

		// Token: 0x04004976 RID: 18806
		private static readonly Vector2[] extentPoints = new Vector2[8];

		// Token: 0x02000D19 RID: 3353
		public enum HighlightState
		{
			// Token: 0x04004978 RID: 18808
			Expanding,
			// Token: 0x04004979 RID: 18809
			Holding,
			// Token: 0x0400497A RID: 18810
			Contracting
		}
	}
}
