using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D4D RID: 3405
	[RequireComponent(typeof(RawImage))]
	[RequireComponent(typeof(RectTransform))]
	public class ModelPanel : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IScrollHandler, IEndDragHandler
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x00141256 File Offset: 0x0013F456
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x0014125E File Offset: 0x0013F45E
		public GameObject modelPrefab
		{
			get
			{
				return this._modelPrefab;
			}
			set
			{
				if (this._modelPrefab == value)
				{
					return;
				}
				this.DestroyModelInstance();
				this._modelPrefab = value;
				this.BuildModelInstance();
			}
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x00141282 File Offset: 0x0013F482
		private void DestroyModelInstance()
		{
			UnityEngine.Object.Destroy(this.modelInstance);
			this.modelInstance = null;
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x00141298 File Offset: 0x0013F498
		private void BuildModelInstance()
		{
			if (this._modelPrefab && base.enabled && !this.modelInstance)
			{
				this.modelInstance = UnityEngine.Object.Instantiate<GameObject>(this._modelPrefab, Vector3.zero, Quaternion.identity);
				ModelPanelParameters component = this._modelPrefab.GetComponent<ModelPanelParameters>();
				if (component)
				{
					this.modelInstance.transform.rotation = component.modelRotation;
				}
				Bounds bounds;
				Util.GuessRenderBoundsMeshOnly(this.modelInstance, out bounds);
				this.pivotPoint = bounds.center;
				this.minDistance = Mathf.Min(new float[]
				{
					bounds.size.x,
					bounds.size.y,
					bounds.size.z
				}) * 1f;
				this.maxDistance = Mathf.Max(new float[]
				{
					bounds.size.x,
					bounds.size.y,
					bounds.size.z
				}) * 2f;
				Renderer[] componentsInChildren = this.modelInstance.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gameObject.layer = LayerIndex.noDraw.intVal;
				}
				AimAnimator[] componentsInChildren2 = this.modelInstance.GetComponentsInChildren<AimAnimator>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					componentsInChildren2[j].inputBank = null;
					componentsInChildren2[j].directionComponent = null;
					componentsInChildren2[j].enabled = false;
				}
				foreach (Animator animator in this.modelInstance.GetComponentsInChildren<Animator>())
				{
					animator.SetBool("isGrounded", true);
					animator.SetFloat("aimPitchCycle", 0.5f);
					animator.SetFloat("aimYawCycle", 0.5f);
					animator.Play("Idle");
					animator.Update(0f);
				}
				IKSimpleChain[] componentsInChildren4 = this.modelInstance.GetComponentsInChildren<IKSimpleChain>();
				for (int l = 0; l < componentsInChildren4.Length; l++)
				{
					componentsInChildren4[l].enabled = false;
				}
				DitherModel[] componentsInChildren5 = this.modelInstance.GetComponentsInChildren<DitherModel>();
				for (int m = 0; m < componentsInChildren5.Length; m++)
				{
					componentsInChildren5[m].enabled = false;
				}
				PrintController[] componentsInChildren6 = this.modelInstance.GetComponentsInChildren<PrintController>();
				for (int m = 0; m < componentsInChildren6.Length; m++)
				{
					componentsInChildren6[m].enabled = false;
				}
				foreach (LightIntensityCurve lightIntensityCurve in this.modelInstance.GetComponentsInChildren<LightIntensityCurve>())
				{
					if (!lightIntensityCurve.loop)
					{
						lightIntensityCurve.enabled = false;
					}
				}
				AkEvent[] componentsInChildren8 = this.modelInstance.GetComponentsInChildren<AkEvent>();
				for (int m = 0; m < componentsInChildren8.Length; m++)
				{
					componentsInChildren8[m].enabled = false;
				}
				this.desiredZoom = 0.5f;
				this.zoom = this.desiredZoom;
				this.zoomVelocity = 0f;
				this.ResetOrbitAndPan();
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0014159C File Offset: 0x0013F79C
		// (set) Token: 0x06004E0A RID: 19978 RVA: 0x001415A4 File Offset: 0x0013F7A4
		public RenderTexture renderTexture { get; private set; }

		// Token: 0x06004E0B RID: 19979 RVA: 0x001415B0 File Offset: 0x0013F7B0
		private void ResetOrbitAndPan()
		{
			this.orbitPitch = 0f;
			this.orbitYaw = 0f;
			this.orbitalVelocity = Vector3.zero;
			this.orbitalVelocitySmoothDampVelocity = Vector3.zero;
			this.pan = Vector2.zero;
			this.panVelocity = Vector2.zero;
			this.panVelocitySmoothDampVelocity = Vector2.zero;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0014160C File Offset: 0x0013F80C
		private void Awake()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
			this.rawImage = base.GetComponent<RawImage>();
			this.mpEventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.cameraRigController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/Main Camera")).GetComponent<CameraRigController>();
			this.cameraRigController.gameObject.name = "ModelCamera";
			this.cameraRigController.uiCam.gameObject.SetActive(false);
			this.cameraRigController.createHud = false;
			this.cameraRigController.enableFading = false;
			GameObject gameObject = this.cameraRigController.sceneCam.gameObject;
			this.modelCamera = gameObject.AddComponent<ModelCamera>();
			this.cameraRigController.transform.position = -Vector3.forward * 10f;
			this.cameraRigController.transform.forward = Vector3.forward;
			CameraResolutionScaler component = gameObject.GetComponent<CameraResolutionScaler>();
			if (component)
			{
				component.enabled = false;
			}
			Camera sceneCam = this.cameraRigController.sceneCam;
			sceneCam.backgroundColor = Color.clear;
			sceneCam.clearFlags = CameraClearFlags.Color;
			if (this.disablePostProcessLayer)
			{
				PostProcessLayer component2 = sceneCam.GetComponent<PostProcessLayer>();
				if (component2)
				{
					component2.enabled = false;
				}
			}
			Vector3 eulerAngles = this.cameraRigController.transform.eulerAngles;
			this.orbitPitch = eulerAngles.x;
			this.orbitYaw = eulerAngles.y;
			this.modelCamera.attachedCamera.backgroundColor = this.camBackgroundColor;
			this.modelCamera.attachedCamera.clearFlags = CameraClearFlags.Color;
			this.modelCamera.attachedCamera.cullingMask = LayerIndex.manualRender.mask;
			if (this.headlightPrefab)
			{
				this.headlight = UnityEngine.Object.Instantiate<GameObject>(this.headlightPrefab, this.modelCamera.transform).GetComponent<Light>();
				if (this.headlight)
				{
					this.headlight.gameObject.SetActive(true);
					this.modelCamera.AddLight(this.headlight);
				}
			}
			for (int i = 0; i < this.lightPrefabs.Length; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.lightPrefabs[i]);
				Light component3 = gameObject2.GetComponent<Light>();
				gameObject2.SetActive(true);
				this.lights.Add(component3);
				this.modelCamera.AddLight(component3);
			}
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00141862 File Offset: 0x0013FA62
		public void Start()
		{
			this.BuildRenderTexture();
			this.desiredZoom = 0.5f;
			this.zoom = this.desiredZoom;
			this.zoomVelocity = 0f;
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0014188C File Offset: 0x0013FA8C
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.renderTexture);
			if (this.cameraRigController)
			{
				UnityEngine.Object.Destroy(this.cameraRigController.gameObject);
			}
			foreach (Light light in this.lights)
			{
				UnityEngine.Object.Destroy(light.gameObject);
			}
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0014190C File Offset: 0x0013FB0C
		private void OnDisable()
		{
			this.DestroyModelInstance();
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00141914 File Offset: 0x0013FB14
		private void OnEnable()
		{
			this.BuildModelInstance();
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0014191C File Offset: 0x0013FB1C
		public void Update()
		{
			this.UpdateForModelViewer(Time.unscaledDeltaTime);
			if (this.enableGamepadControls && (!this.requiredTopLayer || this.requiredTopLayer.representsTopLayer) && this.mpEventSystemLocator.eventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad && this.mpEventSystemLocator.eventSystem)
			{
				float axis = this.mpEventSystemLocator.eventSystem.player.GetAxis(16);
				float axis2 = this.mpEventSystemLocator.eventSystem.player.GetAxis(17);
				bool button = this.mpEventSystemLocator.eventSystem.player.GetButton(29);
				bool button2 = this.mpEventSystemLocator.eventSystem.player.GetButton(30);
				Vector3 zero = Vector3.zero;
				zero.y = -axis * this.gamepadRotateSensitivity;
				zero.x = axis2 * this.gamepadRotateSensitivity;
				this.orbitalVelocity = zero;
				if (button != button2)
				{
					this.desiredZoom = Mathf.Clamp01(this.desiredZoom + (button ? 0.1f : -0.1f) * Time.deltaTime * this.gamepadZoomSensitivity);
				}
			}
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x00141A48 File Offset: 0x0013FC48
		public void LateUpdate()
		{
			this.modelCamera.attachedCamera.aspect = (float)this.renderTexture.width / (float)this.renderTexture.height;
			this.cameraRigController.baseFov = this.fov;
			this.modelCamera.renderSettings = this.renderSettings;
			this.modelCamera.RenderItem(this.modelInstance, this.renderTexture);
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00141AB7 File Offset: 0x0013FCB7
		private void OnRectTransformDimensionsChange()
		{
			this.BuildRenderTexture();
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00141AC0 File Offset: 0x0013FCC0
		private void BuildRenderTexture()
		{
			if (!this.rectTransform)
			{
				return;
			}
			Vector3[] fourCornersArray = new Vector3[4];
			this.rectTransform.GetLocalCorners(fourCornersArray);
			Vector2 size = this.rectTransform.rect.size;
			int num = Mathf.FloorToInt(size.x);
			int num2 = Mathf.FloorToInt(size.y);
			if (this.renderTexture && this.renderTexture.width == num && this.renderTexture.height == num2)
			{
				return;
			}
			UnityEngine.Object.Destroy(this.renderTexture);
			this.renderTexture = null;
			if (num > 0 && num2 > 0)
			{
				this.renderTexture = new RenderTexture(new RenderTextureDescriptor(num, num2, RenderTextureFormat.ARGB32)
				{
					sRGB = true
				});
				this.renderTexture.useMipMap = false;
				this.renderTexture.filterMode = FilterMode.Bilinear;
			}
			this.rawImage.texture = this.renderTexture;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x00141BA4 File Offset: 0x0013FDA4
		private void UpdateForModelViewer(float deltaTime)
		{
			this.zoom = Mathf.SmoothDamp(this.zoom, this.desiredZoom, ref this.zoomVelocity, 0.1f);
			this.orbitPitch %= 360f;
			if (this.orbitPitch < -180f)
			{
				this.orbitPitch += 360f;
			}
			else if (this.orbitPitch > 180f)
			{
				this.orbitPitch -= 360f;
			}
			this.orbitPitch = Mathf.Clamp(this.orbitPitch + this.orbitalVelocity.x * deltaTime, -89f, 89f);
			this.orbitYaw += this.orbitalVelocity.y * deltaTime;
			this.orbitalVelocity = Vector3.SmoothDamp(this.orbitalVelocity, Vector3.zero, ref this.orbitalVelocitySmoothDampVelocity, 0.25f, 2880f, deltaTime);
			if (this.orbitDragCount > 0)
			{
				this.orbitalVelocity = Vector3.zero;
				this.orbitalVelocitySmoothDampVelocity = Vector3.zero;
			}
			this.pan += this.panVelocity * deltaTime;
			this.panVelocity = Vector2.SmoothDamp(this.panVelocity, Vector2.zero, ref this.panVelocitySmoothDampVelocity, 0.25f, 100f, deltaTime);
			if (this.panDragCount > 0)
			{
				this.panVelocity = Vector2.zero;
				this.panVelocitySmoothDampVelocity = Vector2.zero;
			}
			Quaternion rotation = Quaternion.Euler(this.orbitPitch, this.orbitYaw, 0f);
			this.cameraRigController.transform.forward = rotation * Vector3.forward;
			Vector3 forward = this.cameraRigController.transform.forward;
			Vector3 position = this.pivotPoint + forward * -Mathf.LerpUnclamped(this.minDistance, this.maxDistance, this.zoom) + this.cameraRigController.transform.up * this.pan.y + this.cameraRigController.transform.right * this.pan.x;
			this.cameraRigController.transform.position = position;
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00141DDC File Offset: 0x0013FFDC
		public void SetAnglesForCharacterThumbnailForSeconds(float time, bool setZoom = false)
		{
			this.SetAnglesForCharacterThumbnail(setZoom);
			float t = time;
			Action func = null;
			func = delegate()
			{
				t -= Time.deltaTime;
				if (this)
				{
					this.SetAnglesForCharacterThumbnail(setZoom);
				}
				if (t <= 0f)
				{
					RoR2Application.onUpdate -= func;
				}
			};
			RoR2Application.onUpdate += func;
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x00141E34 File Offset: 0x00140034
		public void SetAnglesForCharacterThumbnail(bool setZoom = false)
		{
			if (!this.modelInstance)
			{
				return;
			}
			ModelPanel.CameraFramingCalculator cameraFramingCalculator = new ModelPanel.CameraFramingCalculator(this.modelInstance);
			cameraFramingCalculator.GetCharacterThumbnailPosition(this.fov);
			this.pivotPoint = cameraFramingCalculator.outputPivotPoint;
			this.minDistance = cameraFramingCalculator.outputMinDistance;
			this.maxDistance = cameraFramingCalculator.outputMaxDistance;
			this.ResetOrbitAndPan();
			Vector3 eulerAngles = cameraFramingCalculator.outputCameraRotation.eulerAngles;
			this.orbitPitch = eulerAngles.x;
			this.orbitYaw = eulerAngles.y;
			if (setZoom)
			{
				this.zoom = Util.Remap(Vector3.Distance(cameraFramingCalculator.outputCameraPosition, cameraFramingCalculator.outputPivotPoint), this.minDistance, this.maxDistance, 0f, 1f);
				this.desiredZoom = this.zoom;
			}
			this.zoomVelocity = 0f;
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00141F04 File Offset: 0x00140104
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				this.orbitDragCount++;
				if (this.orbitDragCount == 1)
				{
					this.orbitDragPoint = eventData.pressPosition;
					return;
				}
			}
			else if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.panDragCount++;
				if (this.panDragCount == 1)
				{
					this.panDragPoint = eventData.pressPosition;
				}
			}
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x00141F69 File Offset: 0x00140169
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				this.orbitDragCount--;
			}
			else if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.panDragCount--;
			}
			this.OnDrag(eventData);
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00141FA4 File Offset: 0x001401A4
		public void OnDrag(PointerEventData eventData)
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				Vector2 vector = eventData.position - this.orbitDragPoint;
				this.orbitDragPoint = eventData.position;
				float num = 0.5f / unscaledDeltaTime;
				this.orbitalVelocity = new Vector3(-vector.y * num * 0.5f, vector.x * num, 0f);
				return;
			}
			Vector2 a = eventData.position - this.panDragPoint;
			this.panDragPoint = eventData.position;
			float d = -0.01f;
			this.panVelocity = a * d / unscaledDeltaTime;
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00142047 File Offset: 0x00140247
		public void OnScroll(PointerEventData eventData)
		{
			this.desiredZoom = Mathf.Clamp01(this.desiredZoom + eventData.scrollDelta.y * -0.05f);
		}

		// Token: 0x04004AA2 RID: 19106
		private GameObject _modelPrefab;

		// Token: 0x04004AA3 RID: 19107
		public RenderSettingsState renderSettings;

		// Token: 0x04004AA4 RID: 19108
		public Color camBackgroundColor = Color.clear;

		// Token: 0x04004AA5 RID: 19109
		public bool disablePostProcessLayer = true;

		// Token: 0x04004AA6 RID: 19110
		private RectTransform rectTransform;

		// Token: 0x04004AA7 RID: 19111
		private RawImage rawImage;

		// Token: 0x04004AA8 RID: 19112
		private GameObject modelInstance;

		// Token: 0x04004AA9 RID: 19113
		private CameraRigController cameraRigController;

		// Token: 0x04004AAA RID: 19114
		private ModelCamera modelCamera;

		// Token: 0x04004AAB RID: 19115
		public GameObject headlightPrefab;

		// Token: 0x04004AAC RID: 19116
		public GameObject[] lightPrefabs;

		// Token: 0x04004AAD RID: 19117
		private Light headlight;

		// Token: 0x04004AAF RID: 19119
		public float fov = 60f;

		// Token: 0x04004AB0 RID: 19120
		public bool enableGamepadControls;

		// Token: 0x04004AB1 RID: 19121
		public float gamepadZoomSensitivity;

		// Token: 0x04004AB2 RID: 19122
		public float gamepadRotateSensitivity;

		// Token: 0x04004AB3 RID: 19123
		public UILayerKey requiredTopLayer;

		// Token: 0x04004AB4 RID: 19124
		private MPEventSystemLocator mpEventSystemLocator;

		// Token: 0x04004AB5 RID: 19125
		private float zoom = 0.5f;

		// Token: 0x04004AB6 RID: 19126
		private float desiredZoom = 0.5f;

		// Token: 0x04004AB7 RID: 19127
		private float zoomVelocity;

		// Token: 0x04004AB8 RID: 19128
		private float minDistance = 0.5f;

		// Token: 0x04004AB9 RID: 19129
		private float maxDistance = 10f;

		// Token: 0x04004ABA RID: 19130
		private float orbitPitch;

		// Token: 0x04004ABB RID: 19131
		private float orbitYaw = 180f;

		// Token: 0x04004ABC RID: 19132
		private Vector3 orbitalVelocity = Vector3.zero;

		// Token: 0x04004ABD RID: 19133
		private Vector3 orbitalVelocitySmoothDampVelocity = Vector3.zero;

		// Token: 0x04004ABE RID: 19134
		private Vector2 pan;

		// Token: 0x04004ABF RID: 19135
		private Vector2 panVelocity;

		// Token: 0x04004AC0 RID: 19136
		private Vector2 panVelocitySmoothDampVelocity;

		// Token: 0x04004AC1 RID: 19137
		private Vector3 pivotPoint = Vector3.zero;

		// Token: 0x04004AC2 RID: 19138
		private List<Light> lights = new List<Light>();

		// Token: 0x04004AC3 RID: 19139
		private Vector2 orbitDragPoint;

		// Token: 0x04004AC4 RID: 19140
		private Vector2 panDragPoint;

		// Token: 0x04004AC5 RID: 19141
		private int orbitDragCount;

		// Token: 0x04004AC6 RID: 19142
		private int panDragCount;

		// Token: 0x02000D4E RID: 3406
		private class CameraFramingCalculator
		{
			// Token: 0x06004E1D RID: 19997 RVA: 0x001420FF File Offset: 0x001402FF
			private static void GenerateBoneList(Transform rootBone, List<Transform> boneList)
			{
				boneList.AddRange(rootBone.gameObject.GetComponentsInChildren<Transform>());
			}

			// Token: 0x06004E1E RID: 19998 RVA: 0x00142114 File Offset: 0x00140314
			public CameraFramingCalculator(GameObject modelInstance)
			{
				this.modelInstance = modelInstance;
				this.root = modelInstance.transform;
				ModelPanel.CameraFramingCalculator.GenerateBoneList(this.root, this.boneList);
				this.hurtBoxGroup = modelInstance.GetComponent<HurtBoxGroup>();
				if (this.hurtBoxGroup)
				{
					this.hurtBoxes = this.hurtBoxGroup.hurtBoxes;
				}
			}

			// Token: 0x06004E1F RID: 19999 RVA: 0x0014218C File Offset: 0x0014038C
			private bool FindBestEyePoint(out Vector3 result, out float approximateEyeRadius)
			{
				approximateEyeRadius = 1f;
				IEnumerable<Transform> source = this.boneList.Where(new Func<Transform, bool>(ModelPanel.CameraFramingCalculator.<>c.<>9.<FindBestEyePoint>g__FirstChoice|12_0));
				if (!source.Any<Transform>())
				{
					source = this.boneList.Where(new Func<Transform, bool>(ModelPanel.CameraFramingCalculator.<>c.<>9.<FindBestEyePoint>g__SecondChoice|12_1));
				}
				Vector3[] array = (from bone in source
				select bone.position).ToArray<Vector3>();
				result = Vector3Utils.AveragePrecise<Vector3[]>(array);
				for (int i = 0; i < array.Length; i++)
				{
					float magnitude = (array[i] - result).magnitude;
					if (magnitude > approximateEyeRadius)
					{
						approximateEyeRadius = magnitude;
					}
				}
				return array.Length != 0;
			}

			// Token: 0x06004E20 RID: 20000 RVA: 0x00142250 File Offset: 0x00140450
			private bool FindBestHeadPoint(string searchName, out Vector3 result, out float approximateRadius)
			{
				Transform[] array = (from bone in this.boneList
				where string.Equals(bone.name, searchName, StringComparison.OrdinalIgnoreCase)
				select bone).ToArray<Transform>();
				if (array.Length == 0)
				{
					array = (from bone in this.boneList
					where bone.name.ToLower(CultureInfo.InvariantCulture).Contains(searchName)
					select bone).ToArray<Transform>();
				}
				if (array.Length != 0)
				{
					foreach (Transform bone2 in array)
					{
						Bounds bounds;
						if (this.TryCalcBoneBounds(bone2, 0.2f, out bounds, out approximateRadius))
						{
							result = bounds.center;
							return true;
						}
					}
				}
				result = Vector3.zero;
				approximateRadius = 0f;
				return false;
			}

			// Token: 0x06004E21 RID: 20001 RVA: 0x001422F4 File Offset: 0x001404F4
			private static float CalcMagnitudeToFrameSphere(float sphereRadius, float fieldOfView)
			{
				float num = fieldOfView * 0.5f;
				float num2 = 90f;
				return Mathf.Tan((180f - num2 - num) * 0.017453292f) * sphereRadius;
			}

			// Token: 0x06004E22 RID: 20002 RVA: 0x00142328 File Offset: 0x00140528
			private bool FindBestCenterOfMass(out Vector3 result, out float approximateRadius)
			{
				from bone in this.boneList
				select bone.GetComponent<HurtBox>() into hurtBox
				where hurtBox
				select hurtBox;
				if (this.hurtBoxGroup && this.hurtBoxGroup.mainHurtBox)
				{
					result = this.hurtBoxGroup.mainHurtBox.transform.position;
					approximateRadius = Util.SphereVolumeToRadius(this.hurtBoxGroup.mainHurtBox.volume);
					return true;
				}
				result = Vector3.zero;
				approximateRadius = 1f;
				return false;
			}

			// Token: 0x06004E23 RID: 20003 RVA: 0x001423EC File Offset: 0x001405EC
			private static float GetWeightForBone(ref BoneWeight boneWeight, int boneIndex)
			{
				if (boneWeight.boneIndex0 == boneIndex)
				{
					return boneWeight.weight0;
				}
				if (boneWeight.boneIndex1 == boneIndex)
				{
					return boneWeight.weight1;
				}
				if (boneWeight.boneIndex2 == boneIndex)
				{
					return boneWeight.weight2;
				}
				if (boneWeight.boneIndex3 == boneIndex)
				{
					return boneWeight.weight3;
				}
				return 0f;
			}

			// Token: 0x06004E24 RID: 20004 RVA: 0x00142440 File Offset: 0x00140640
			private static int FindBoneIndex(SkinnedMeshRenderer _skinnedMeshRenderer, Transform _bone)
			{
				Transform[] bones = _skinnedMeshRenderer.bones;
				for (int i = 0; i < bones.Length; i++)
				{
					if (bones[i] == _bone)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06004E25 RID: 20005 RVA: 0x00142470 File Offset: 0x00140670
			private bool TryCalcBoneBounds(Transform bone, float weightThreshold, out Bounds bounds, out float approximateRadius)
			{
				SkinnedMeshRenderer[] componentsInChildren = this.modelInstance.GetComponentsInChildren<SkinnedMeshRenderer>();
				SkinnedMeshRenderer skinnedMeshRenderer = null;
				Mesh mesh = null;
				int num = -1;
				List<int> list = new List<int>();
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
				{
					mesh = skinnedMeshRenderer.sharedMesh;
					if (mesh)
					{
						num = ModelPanel.CameraFramingCalculator.FindBoneIndex(skinnedMeshRenderer, bone);
						if (num != -1)
						{
							BoneWeight[] boneWeights = mesh.boneWeights;
							for (int j = 0; j < boneWeights.Length; j++)
							{
								if (ModelPanel.CameraFramingCalculator.GetWeightForBone(ref boneWeights[j], num) > weightThreshold)
								{
									list.Add(j);
								}
							}
							if (list.Count == 0)
							{
								num = -1;
							}
						}
						if (num != -1)
						{
							break;
						}
					}
				}
				if (num == -1)
				{
					bounds = default(Bounds);
					approximateRadius = 0f;
					return false;
				}
				Mesh mesh2 = new Mesh();
				skinnedMeshRenderer.BakeMesh(mesh2);
				Vector3[] vertices = mesh2.vertices;
				UnityEngine.Object.Destroy(mesh2);
				if (mesh2.vertexCount != mesh.vertexCount)
				{
					Debug.LogWarningFormat("Baked mesh vertex count differs from the original mesh vertex count! baked={0} original={1}", new object[]
					{
						mesh2.vertexCount,
						mesh.vertexCount
					});
					vertices = mesh.vertices;
				}
				Vector3[] array = new Vector3[list.Count];
				Transform transform = skinnedMeshRenderer.transform;
				Vector3 position = transform.position;
				Quaternion rotation = transform.rotation;
				for (int k = 0; k < list.Count; k++)
				{
					int num2 = list[k];
					Vector3 point = vertices[num2];
					Vector3 vector = position + rotation * point;
					array[k] = vector;
				}
				bounds = new Bounds(Vector3Utils.AveragePrecise<Vector3[]>(array), Vector3.zero);
				float num3 = 0f;
				for (int l = 0; l < array.Length; l++)
				{
					bounds.Encapsulate(array[l]);
					float num4 = Vector3.Distance(bounds.center, array[l]);
					if (num4 > num3)
					{
						num3 = num4;
					}
				}
				approximateRadius = num3;
				return true;
			}

			// Token: 0x06004E26 RID: 20006 RVA: 0x00142658 File Offset: 0x00140858
			public void GetCharacterThumbnailPosition(float fov)
			{
				ModelPanelParameters component = this.modelInstance.GetComponent<ModelPanelParameters>();
				if (component)
				{
					if (component.focusPointTransform)
					{
						this.outputPivotPoint = component.focusPointTransform.position;
					}
					if (component.cameraPositionTransform)
					{
						this.outputCameraPosition = component.cameraPositionTransform.position;
					}
					this.outputCameraRotation = Util.QuaternionSafeLookRotation(component.cameraDirection);
					this.outputMinDistance = component.minDistance;
					this.outputMaxDistance = component.maxDistance;
					return;
				}
				Vector3 vector = Vector3.zero;
				float sphereRadius = 1f;
				bool flag = this.FindBestHeadPoint("head", out vector, out sphereRadius);
				if (!flag)
				{
					flag = this.FindBestHeadPoint("chest", out vector, out sphereRadius);
				}
				bool flag2 = false;
				float num = 1f;
				float num2 = 1f;
				Vector3 vector2;
				bool flag3 = this.FindBestEyePoint(out vector2, out num2);
				if (!flag)
				{
					sphereRadius = num2;
				}
				if (flag3)
				{
					vector = vector2;
				}
				if (!flag && !flag3)
				{
					flag2 = this.FindBestCenterOfMass(out vector, out sphereRadius);
				}
				float num3 = 1f;
				Bounds bounds;
				if (Util.GuessRenderBoundsMeshOnly(this.modelInstance, out bounds))
				{
					if (flag2)
					{
						sphereRadius = Util.SphereVolumeToRadius(bounds.size.x * bounds.size.y * bounds.size.z);
					}
					Mathf.Max((vector.y - bounds.min.y) / bounds.size.y - 0.5f - 0.2f, 0f);
					Vector3 center = bounds.center;
					num3 = bounds.size.z / bounds.size.x;
					this.outputMinDistance = Mathf.Min(new float[]
					{
						bounds.size.x,
						bounds.size.y,
						bounds.size.z
					}) * 1f;
					this.outputMaxDistance = Mathf.Max(new float[]
					{
						bounds.size.x,
						bounds.size.y,
						bounds.size.z
					}) * 2f;
				}
				Vector3 vector3 = -this.root.forward;
				for (int i = 0; i < this.boneList.Count; i++)
				{
					if (this.boneList[i].name.Equals("muzzle", StringComparison.OrdinalIgnoreCase))
					{
						Vector3 vector4 = this.root.position - this.boneList[i].position;
						vector4.y = 0f;
						float magnitude = vector4.magnitude;
						if (magnitude > 0.2f)
						{
							vector4 /= magnitude;
							vector3 = vector4;
							break;
						}
					}
				}
				vector3 = Quaternion.Euler(0f, 57.29578f * Mathf.Atan(num3 - 1f) * 1f, 0f) * vector3;
				Vector3 b = -vector3 * (ModelPanel.CameraFramingCalculator.CalcMagnitudeToFrameSphere(sphereRadius, fov) + num);
				Vector3 b2 = vector + b;
				this.outputPivotPoint = vector;
				this.outputCameraPosition = b2;
				this.outputCameraRotation = Util.QuaternionSafeLookRotation(vector - b2);
			}

			// Token: 0x04004AC7 RID: 19143
			private GameObject modelInstance;

			// Token: 0x04004AC8 RID: 19144
			private Transform root;

			// Token: 0x04004AC9 RID: 19145
			private readonly List<Transform> boneList = new List<Transform>();

			// Token: 0x04004ACA RID: 19146
			private HurtBoxGroup hurtBoxGroup;

			// Token: 0x04004ACB RID: 19147
			private HurtBox[] hurtBoxes = Array.Empty<HurtBox>();

			// Token: 0x04004ACC RID: 19148
			public Vector3 outputPivotPoint;

			// Token: 0x04004ACD RID: 19149
			public Vector3 outputCameraPosition;

			// Token: 0x04004ACE RID: 19150
			public float outputMinDistance;

			// Token: 0x04004ACF RID: 19151
			public float outputMaxDistance;

			// Token: 0x04004AD0 RID: 19152
			public Quaternion outputCameraRotation;
		}
	}
}
