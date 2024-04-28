using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.CameraModes;
using RoR2.ConVar;
using RoR2.Networking;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200060B RID: 1547
	public class CameraRigController : MonoBehaviour
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x000788E0 File Offset: 0x00076AE0
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x000788E8 File Offset: 0x00076AE8
		public bool disableSpectating { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x000788F1 File Offset: 0x00076AF1
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x000788F9 File Offset: 0x00076AF9
		public CameraModeBase cameraMode
		{
			get
			{
				return this._cameraMode;
			}
			set
			{
				if (this._cameraMode == value)
				{
					return;
				}
				CameraModeBase cameraMode = this._cameraMode;
				if (cameraMode != null)
				{
					cameraMode.OnUninstall(this);
				}
				this._cameraMode = value;
				CameraModeBase cameraMode2 = this._cameraMode;
				if (cameraMode2 == null)
				{
					return;
				}
				cameraMode2.OnInstall(this);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0007892F File Offset: 0x00076B2F
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00078937 File Offset: 0x00076B37
		public HUD hud { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00078940 File Offset: 0x00076B40
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x00078948 File Offset: 0x00076B48
		public GameObject firstPersonTarget { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00078951 File Offset: 0x00076B51
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x00078959 File Offset: 0x00076B59
		public TeamIndex targetTeamIndex { get; private set; } = TeamIndex.None;

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00078962 File Offset: 0x00076B62
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0007896A File Offset: 0x00076B6A
		public CharacterBody targetBody { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00078973 File Offset: 0x00076B73
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x0007897B File Offset: 0x00076B7B
		public Vector3 rawScreenShakeDisplacement { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00078984 File Offset: 0x00076B84
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0007898C File Offset: 0x00076B8C
		public Vector3 crosshairWorldPosition { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00078995 File Offset: 0x00076B95
		public bool hasOverride
		{
			get
			{
				return this.overrideCam != null;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x000789A0 File Offset: 0x00076BA0
		public bool isControlAllowed
		{
			get
			{
				return !this.hasOverride || this.overrideCam.IsUserControlAllowed(this);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x000789B8 File Offset: 0x00076BB8
		public bool isHudAllowed
		{
			get
			{
				return this.target && (!this.hasOverride || this.overrideCam.IsHudAllowed(this));
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x000789DF File Offset: 0x00076BDF
		// (set) Token: 0x06001C6E RID: 7278 RVA: 0x000789E8 File Offset: 0x00076BE8
		public GameObject target
		{
			get
			{
				return this._target;
			}
			private set
			{
				if (this._target == value)
				{
					return;
				}
				GameObject target = this._target;
				this._target = value;
				bool flag = this._target;
				this.targetParams = (flag ? this.target.GetComponent<CameraTargetParams>() : null);
				this.targetBody = (flag ? this.target.GetComponent<CharacterBody>() : null);
				CameraModeBase cameraMode = this.cameraMode;
				if (cameraMode != null)
				{
					cameraMode.OnTargetChanged(this, new CameraModeBase.OnTargetChangedArgs
					{
						oldTarget = target,
						newTarget = this._target
					});
				}
				Action<CameraRigController, GameObject> action = CameraRigController.onCameraTargetChanged;
				if (action == null)
				{
					return;
				}
				action(this, this.target);
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00078A90 File Offset: 0x00076C90
		private void StartStateLerp(float lerpDuration)
		{
			this.lerpCameraState = this.currentCameraState;
			if (lerpDuration > 0f)
			{
				this.lerpCameraTime = 0f;
				this.lerpCameraTimeScale = 1f / lerpDuration;
				return;
			}
			this.lerpCameraTime = 1f;
			this.lerpCameraTimeScale = 0f;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00078AE0 File Offset: 0x00076CE0
		public void SetOverrideCam(ICameraStateProvider newOverrideCam, float lerpDuration = 1f)
		{
			if (newOverrideCam == this.overrideCam)
			{
				return;
			}
			if (this.overrideCam != null && newOverrideCam == null)
			{
				CameraModeBase cameraMode = this.cameraMode;
				if (cameraMode != null)
				{
					cameraMode.MatchState(this.cameraModeContext, this.currentCameraState);
				}
			}
			this.overrideCam = newOverrideCam;
			this.StartStateLerp(lerpDuration);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x00078B2D File Offset: 0x00076D2D
		public bool IsOverrideCam(ICameraStateProvider testOverrideCam)
		{
			return this.overrideCam == testOverrideCam;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00078B38 File Offset: 0x00076D38
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00078B40 File Offset: 0x00076D40
		public NetworkUser viewer
		{
			get
			{
				return this._viewer;
			}
			set
			{
				this._viewer = value;
				this.localUserViewer = (this._viewer ? this._viewer.localUser : null);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00078B6A File Offset: 0x00076D6A
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00078B74 File Offset: 0x00076D74
		public LocalUser localUserViewer
		{
			get
			{
				return this._localUserViewer;
			}
			private set
			{
				if (this._localUserViewer == value)
				{
					return;
				}
				if (this._localUserViewer != null)
				{
					this._localUserViewer.cameraRigController = null;
				}
				this._localUserViewer = value;
				if (this._localUserViewer != null)
				{
					this._localUserViewer.cameraRigController = this;
				}
				if (this.hud)
				{
					this.hud.localUserViewer = this._localUserViewer;
				}
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00078BD8 File Offset: 0x00076DD8
		private void Start()
		{
			if (this.createHud)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/HUDSimple"));
				this.hud = gameObject.GetComponent<HUD>();
				this.hud.cameraRigController = this;
				this.hud.GetComponent<Canvas>().worldCamera = this.uiCam;
				this.hud.GetComponent<CrosshairManager>().cameraRigController = this;
				this.hud.localUserViewer = this.localUserViewer;
			}
			if (this.uiCam)
			{
				this.uiCam.transform.parent = null;
				this.uiCam.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
			}
			if (!DamageNumberManager.instance)
			{
				UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/DamageNumberManager"));
			}
			this.currentCameraState = new CameraState
			{
				position = base.transform.position,
				rotation = base.transform.rotation,
				fov = this.baseFov
			};
			this.cameraMode = CameraModePlayerBasic.playerBasic;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00078CEC File Offset: 0x00076EEC
		private void OnEnable()
		{
			CameraRigController.instancesList.Add(this);
			if (this.uiCam)
			{
				this.uiCam.gameObject.SetActive(true);
			}
			if (this.hud)
			{
				this.hud.gameObject.SetActive(true);
			}
			Action<CameraRigController> action = CameraRigController.onCameraEnableGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00078D50 File Offset: 0x00076F50
		private void OnDisable()
		{
			Action<CameraRigController> action = CameraRigController.onCameraDisableGlobal;
			if (action != null)
			{
				action(this);
			}
			if (this.uiCam)
			{
				this.uiCam.gameObject.SetActive(false);
			}
			if (this.hud)
			{
				this.hud.gameObject.SetActive(false);
			}
			CameraRigController.instancesList.Remove(this);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00078DB8 File Offset: 0x00076FB8
		private void OnDestroy()
		{
			this.cameraMode = null;
			if (this.uiCam)
			{
				UnityEngine.Object.Destroy(this.uiCam.gameObject);
			}
			if (this.hud)
			{
				UnityEngine.Object.Destroy(this.hud.gameObject);
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00078E08 File Offset: 0x00077008
		private void Update()
		{
			this.target = this.nextTarget;
			if (this.targetBody)
			{
				this.targetTeamIndex = this.targetBody.teamComponent.teamIndex;
			}
			if (Time.deltaTime == 0f || this.isCutscene)
			{
				return;
			}
			this.lerpCameraTime += Time.deltaTime * this.lerpCameraTimeScale;
			this.firstPersonTarget = null;
			this.sceneCam.rect = this.viewport;
			this.GenerateCameraModeContext(out this.cameraModeContext);
			CameraState cameraState = this.currentCameraState;
			if (this.cameraMode != null)
			{
				CameraModeBase.CollectLookInputResult collectLookInputResult;
				this.cameraMode.CollectLookInput(this.cameraModeContext, out collectLookInputResult);
				CameraModeBase cameraMode = this.cameraMode;
				CameraModeBase.ApplyLookInputArgs applyLookInputArgs = default(CameraModeBase.ApplyLookInputArgs);
				applyLookInputArgs.lookInput = collectLookInputResult.lookInput;
				cameraMode.ApplyLookInput(this.cameraModeContext, applyLookInputArgs);
				CameraModeBase.UpdateResult updateResult;
				this.cameraMode.Update(this.cameraModeContext, out updateResult);
				cameraState = updateResult.cameraState;
				this.firstPersonTarget = updateResult.firstPersonTarget;
				this.crosshairWorldPosition = updateResult.crosshairWorldPosition;
				this.SetSprintParticlesActive(updateResult.showSprintParticles);
			}
			if (this.hud)
			{
				CharacterMaster targetMaster = this.targetBody ? this.targetBody.master : null;
				this.hud.targetMaster = targetMaster;
			}
			CameraState cameraState2 = cameraState;
			if (this.overrideCam != null)
			{
				UnityEngine.Object exists;
				if ((exists = (this.overrideCam as UnityEngine.Object)) == null || exists)
				{
					this.overrideCam.GetCameraState(this, ref cameraState2);
				}
				else
				{
					this.overrideCam = null;
				}
			}
			if (this.lerpCameraTime >= 1f)
			{
				this.currentCameraState = cameraState2;
			}
			else
			{
				this.currentCameraState = CameraState.Lerp(ref this.lerpCameraState, ref cameraState2, CameraRigController.RemapLerpTime(this.lerpCameraTime));
			}
			this.SetCameraState(this.currentCameraState);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00078FDC File Offset: 0x000771DC
		private void GenerateCameraModeContext(out CameraModeBase.CameraModeContext result)
		{
			result.cameraInfo = default(CameraModeBase.CameraInfo);
			result.targetInfo = default(CameraModeBase.TargetInfo);
			result.viewerInfo = default(CameraModeBase.ViewerInfo);
			ref CameraModeBase.TargetInfo ptr = ref result.targetInfo;
			ref CameraModeBase.ViewerInfo ptr2 = ref result.viewerInfo;
			result.cameraInfo.cameraRigController = this;
			result.cameraInfo.sceneCam = this.sceneCam;
			result.cameraInfo.overrideCam = this.overrideCam;
			result.cameraInfo.previousCameraState = this.currentCameraState;
			result.cameraInfo.baseFov = this.baseFov;
			ptr.target = (this.target ? this.target : null);
			ptr.body = this.targetBody;
			ref CameraModeBase.TargetInfo ptr3 = ref ptr;
			GameObject target = ptr.target;
			ptr3.inputBank = ((target != null) ? target.GetComponent<InputBankTest>() : null);
			ptr.targetParams = this.targetParams;
			ref CameraModeBase.TargetInfo ptr4 = ref ptr;
			GameObject target2 = ptr.target;
			TeamIndex? teamIndex;
			if (target2 == null)
			{
				teamIndex = null;
			}
			else
			{
				TeamComponent component = target2.GetComponent<TeamComponent>();
				teamIndex = ((component != null) ? new TeamIndex?(component.teamIndex) : null);
			}
			ptr4.teamIndex = (teamIndex ?? TeamIndex.None);
			ptr.isSprinting = (ptr.body && ptr.body.isSprinting);
			ref CameraModeBase.TargetInfo ptr5 = ref ptr;
			CharacterBody body = ptr.body;
			ptr5.master = ((body != null) ? body.master : null);
			ref CameraModeBase.TargetInfo ptr6 = ref ptr;
			CharacterMaster master = ptr.master;
			NetworkUser networkUser;
			if (master == null)
			{
				networkUser = null;
			}
			else
			{
				PlayerCharacterMasterController playerCharacterMasterController = master.playerCharacterMasterController;
				networkUser = ((playerCharacterMasterController != null) ? playerCharacterMasterController.networkUser : null);
			}
			ptr6.networkUser = networkUser;
			ref CameraModeBase.TargetInfo ptr7 = ref ptr;
			NetworkUser networkUser2 = ptr.networkUser;
			ptr7.networkedViewAngles = ((networkUser2 != null) ? networkUser2.GetComponent<NetworkedViewAngles>() : null);
			ref CameraModeBase.TargetInfo ptr8 = ref ptr;
			bool isViewerControlled;
			if (ptr.networkUser)
			{
				NetworkUser networkUser3 = ptr.networkUser;
				LocalUser localUserViewer = this.localUserViewer;
				isViewerControlled = (networkUser3 == ((localUserViewer != null) ? localUserViewer.currentNetworkUser : null));
			}
			else
			{
				isViewerControlled = false;
			}
			ptr8.isViewerControlled = isViewerControlled;
			ptr2.localUser = this.localUserViewer;
			ref CameraModeBase.ViewerInfo ptr9 = ref ptr2;
			LocalUser localUserViewer2 = this.localUserViewer;
			ptr9.userProfile = ((localUserViewer2 != null) ? localUserViewer2.userProfile : null);
			ref CameraModeBase.ViewerInfo ptr10 = ref ptr2;
			LocalUser localUserViewer3 = this.localUserViewer;
			ptr10.inputPlayer = ((localUserViewer3 != null) ? localUserViewer3.inputPlayer : null);
			ref CameraModeBase.ViewerInfo ptr11 = ref ptr2;
			LocalUser localUserViewer4 = this.localUserViewer;
			ptr11.eventSystem = ((localUserViewer4 != null) ? localUserViewer4.eventSystem : null);
			ptr2.hasCursor = (ptr2.eventSystem && ptr2.eventSystem.isCursorVisible);
			ref CameraModeBase.ViewerInfo ptr12 = ref ptr2;
			LocalUser localUserViewer5 = this.localUserViewer;
			ptr12.isUIFocused = (localUserViewer5 != null && localUserViewer5.isUIFocused);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00079224 File Offset: 0x00077424
		public float Raycast(Ray ray, float maxDistance, float wallCushion)
		{
			RaycastHit[] array = Physics.SphereCastAll(ray, wallCushion, maxDistance, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
			float num = maxDistance;
			for (int i = 0; i < array.Length; i++)
			{
				float distance = array[i].distance;
				if (distance < num)
				{
					Collider collider = array[i].collider;
					if (collider && !collider.GetComponent<NonSolidToCamera>())
					{
						num = distance;
					}
				}
			}
			return num;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0007929C File Offset: 0x0007749C
		private static float RemapLerpTime(float t)
		{
			float num = 1f;
			float num2 = 0f;
			float num3 = 1f;
			if ((t /= num / 2f) < 1f)
			{
				return num3 / 2f * t * t + num2;
			}
			return -num3 / 2f * ((t -= 1f) * (t - 2f) - 1f) + num2;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00079300 File Offset: 0x00077500
		private void SetCameraState(CameraState cameraState)
		{
			this.currentCameraState = cameraState;
			float d = (this.localUserViewer == null) ? 1f : this.localUserViewer.userProfile.screenShakeScale;
			Vector3 position = cameraState.position;
			this.rawScreenShakeDisplacement = ShakeEmitter.ComputeTotalShakeAtPoint(cameraState.position);
			Vector3 vector = this.rawScreenShakeDisplacement * d;
			Vector3 position2 = position + vector;
			if (vector != Vector3.zero)
			{
				Vector3 origin = position;
				Vector3 direction = vector;
				RaycastHit raycastHit;
				if (Physics.SphereCast(origin, this.sceneCam.nearClipPlane, direction, out raycastHit, vector.magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					position2 = position + vector.normalized * raycastHit.distance;
				}
			}
			base.transform.SetPositionAndRotation(position2, cameraState.rotation);
			if (this.sceneCam)
			{
				this.sceneCam.fieldOfView = cameraState.fov;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000793ED File Offset: 0x000775ED
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x000793F5 File Offset: 0x000775F5
		public HurtBox lastCrosshairHurtBox { get; private set; }

		// Token: 0x06001C81 RID: 7297 RVA: 0x00079400 File Offset: 0x00077600
		public static Ray ModifyAimRayIfApplicable(Ray originalAimRay, GameObject target, out float extraRaycastDistance)
		{
			CameraRigController cameraRigController = null;
			for (int i = 0; i < CameraRigController.readOnlyInstancesList.Count; i++)
			{
				CameraRigController cameraRigController2 = CameraRigController.readOnlyInstancesList[i];
				if (cameraRigController2.target == target && cameraRigController2._localUserViewer.cachedBodyObject == target && !cameraRigController2.hasOverride)
				{
					cameraRigController = cameraRigController2;
					break;
				}
			}
			if (cameraRigController)
			{
				Camera camera = cameraRigController.sceneCam;
				extraRaycastDistance = (originalAimRay.origin - camera.transform.position).magnitude;
				return camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
			}
			extraRaycastDistance = 0f;
			return originalAimRay;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000794B4 File Offset: 0x000776B4
		private void SetSprintParticlesActive(bool newSprintParticlesActive)
		{
			if (this.sprintingParticleSystem)
			{
				ParticleSystem.MainModule main = this.sprintingParticleSystem.main;
				if (newSprintParticlesActive)
				{
					main.loop = true;
					if (!this.sprintingParticleSystem.isPlaying)
					{
						this.sprintingParticleSystem.Play();
						return;
					}
				}
				else
				{
					main.loop = false;
				}
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00079508 File Offset: 0x00077708
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			SceneCamera.onSceneCameraPreCull += delegate(SceneCamera sceneCam)
			{
				sceneCam.cameraRigController.sprintingParticleSystem.gameObject.layer = LayerIndex.defaultLayer.intVal;
			};
			SceneCamera.onSceneCameraPostRender += delegate(SceneCamera sceneCam)
			{
				sceneCam.cameraRigController.sprintingParticleSystem.gameObject.layer = LayerIndex.noDraw.intVal;
			};
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00079560 File Offset: 0x00077760
		public static bool IsObjectSpectatedByAnyCamera(GameObject gameObject)
		{
			for (int i = 0; i < CameraRigController.instancesList.Count; i++)
			{
				if (CameraRigController.instancesList[i].target == gameObject)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001C85 RID: 7301 RVA: 0x000795A0 File Offset: 0x000777A0
		// (remove) Token: 0x06001C86 RID: 7302 RVA: 0x000795D4 File Offset: 0x000777D4
		public static event Action<CameraRigController, GameObject> onCameraTargetChanged;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001C87 RID: 7303 RVA: 0x00079608 File Offset: 0x00077808
		// (remove) Token: 0x06001C88 RID: 7304 RVA: 0x0007963C File Offset: 0x0007783C
		public static event Action<CameraRigController> onCameraEnableGlobal;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001C89 RID: 7305 RVA: 0x00079670 File Offset: 0x00077870
		// (remove) Token: 0x06001C8A RID: 7306 RVA: 0x000796A4 File Offset: 0x000778A4
		public static event Action<CameraRigController> onCameraDisableGlobal;

		// Token: 0x06001C8B RID: 7307 RVA: 0x000796D7 File Offset: 0x000778D7
		[ContextMenu("Print Debug Info")]
		private void PrintDebugInfo()
		{
			Debug.LogFormat("hasOverride={0} overrideCam={1} isControlAllowed={2}", new object[]
			{
				this.hasOverride,
				this.overrideCam,
				this.isControlAllowed
			});
		}

		// Token: 0x0400221E RID: 8734
		[Header("Component References")]
		[Tooltip("The main camera for rendering the scene.")]
		public Camera sceneCam;

		// Token: 0x0400221F RID: 8735
		[Tooltip("The UI camera.")]
		public Camera uiCam;

		// Token: 0x04002220 RID: 8736
		[Tooltip("The skybox camera.")]
		public Camera skyboxCam;

		// Token: 0x04002221 RID: 8737
		[Tooltip("The particle system to play while sprinting.")]
		public ParticleSystem sprintingParticleSystem;

		// Token: 0x04002222 RID: 8738
		[Tooltip("The default FOV of this camera.")]
		[Header("Camera Parameters")]
		public float baseFov = 60f;

		// Token: 0x04002223 RID: 8739
		[Tooltip("The viewport to use.")]
		public Rect viewport = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04002224 RID: 8740
		[Tooltip("The maximum distance of the raycast used to determine the aim vector.")]
		public float maxAimRaycastDistance = 1000f;

		// Token: 0x04002225 RID: 8741
		[Tooltip("If true, treat this camera as though it's in a cutscene")]
		public bool isCutscene;

		// Token: 0x04002226 RID: 8742
		[Header("Near-Camera Character Dithering")]
		public bool enableFading = true;

		// Token: 0x04002227 RID: 8743
		public float fadeStartDistance = 1f;

		// Token: 0x04002228 RID: 8744
		public float fadeEndDistance = 4f;

		// Token: 0x04002229 RID: 8745
		[Header("Behavior")]
		[Tooltip("Whether or not to create a HUD.")]
		public bool createHud = true;

		// Token: 0x0400222A RID: 8746
		[Tooltip("Whether or not this camera being enabled forces player-owned cameras to be disabled.")]
		public bool suppressPlayerCameras;

		// Token: 0x0400222B RID: 8747
		[Header("Target (for debug only)")]
		public GameObject nextTarget;

		// Token: 0x0400222D RID: 8749
		private CameraModeBase _cameraMode;

		// Token: 0x04002234 RID: 8756
		private GameObject _target;

		// Token: 0x04002235 RID: 8757
		private CameraState desiredCameraState;

		// Token: 0x04002236 RID: 8758
		private CameraState currentCameraState;

		// Token: 0x04002237 RID: 8759
		private CameraState lerpCameraState;

		// Token: 0x04002238 RID: 8760
		private float lerpCameraTime = 1f;

		// Token: 0x04002239 RID: 8761
		private float lerpCameraTimeScale = 1f;

		// Token: 0x0400223A RID: 8762
		private ICameraStateProvider overrideCam;

		// Token: 0x0400223B RID: 8763
		private CameraModeBase.CameraModeContext cameraModeContext;

		// Token: 0x0400223C RID: 8764
		private NetworkUser _viewer;

		// Token: 0x0400223D RID: 8765
		private LocalUser _localUserViewer;

		// Token: 0x0400223E RID: 8766
		private CameraTargetParams targetParams;

		// Token: 0x0400223F RID: 8767
		public CameraRigController.AimAssistInfo lastAimAssist;

		// Token: 0x04002240 RID: 8768
		public CameraRigController.AimAssistInfo aimAssist;

		// Token: 0x04002242 RID: 8770
		private static List<CameraRigController> instancesList = new List<CameraRigController>();

		// Token: 0x04002243 RID: 8771
		public static readonly ReadOnlyCollection<CameraRigController> readOnlyInstancesList = CameraRigController.instancesList.AsReadOnly();

		// Token: 0x04002247 RID: 8775
		public static FloatConVar aimStickExponent = new FloatConVar("aim_stick_exponent", ConVarFlags.None, "1", "The exponent for stick input used for aiming.");

		// Token: 0x04002248 RID: 8776
		public static FloatConVar aimStickDualZoneThreshold = new FloatConVar("aim_stick_dual_zone_threshold", ConVarFlags.None, "0.90", "The threshold for stick dual zone behavior.");

		// Token: 0x04002249 RID: 8777
		public static FloatConVar aimStickDualZoneSlope = new FloatConVar("aim_stick_dual_zone_slope", ConVarFlags.None, "0.40", "The slope value for stick dual zone behavior.");

		// Token: 0x0400224A RID: 8778
		public static FloatConVar aimStickDualZoneSmoothing = new FloatConVar("aim_stick_smoothing", ConVarFlags.None, "0.05", "The smoothing value for stick aiming.");

		// Token: 0x0400224B RID: 8779
		public static FloatConVar aimStickGlobalScale = new FloatConVar("aim_stick_global_scale", ConVarFlags.Archive, "1.00", "The global sensitivity scale for stick aiming.");

		// Token: 0x0400224C RID: 8780
		public static FloatConVar aimStickAssistMinSlowdownScale = new FloatConVar("aim_stick_assist_min_slowdown_scale", ConVarFlags.None, "1", "The MAX amount the sensitivity scales down when passing over an enemy.");

		// Token: 0x0400224D RID: 8781
		public static FloatConVar aimStickAssistMaxSlowdownScale = new FloatConVar("aim_stick_assist_max_slowdown_scale", ConVarFlags.None, "0.4", "The MAX amount the sensitivity scales down when passing over an enemy.");

		// Token: 0x0400224E RID: 8782
		public static FloatConVar aimStickAssistMinDelta = new FloatConVar("aim_stick_assist_min_delta", ConVarFlags.None, "0", "The MIN amount in radians the aim assist will turn towards");

		// Token: 0x0400224F RID: 8783
		public static FloatConVar aimStickAssistMaxDelta = new FloatConVar("aim_stick_assist_max_delta", ConVarFlags.None, "1.57", "The MAX amount in radians the aim assist will turn towards");

		// Token: 0x04002250 RID: 8784
		public static FloatConVar aimStickAssistMaxInputHelp = new FloatConVar("aim_stick_assist_max_input_help", ConVarFlags.None, "0.2", "The amount, from 0-1, that the aim assist will actually ADD magnitude towards. Helps you keep target while strafing. CURRENTLY UNUSED.");

		// Token: 0x04002251 RID: 8785
		public static FloatConVar aimStickAssistMaxSize = new FloatConVar("aim_stick_assist_max_size", ConVarFlags.None, "3", "The size, as a coefficient, of the aim assist 'white' zone.");

		// Token: 0x04002252 RID: 8786
		public static FloatConVar aimStickAssistMinSize = new FloatConVar("aim_stick_assist_min_size", ConVarFlags.None, "1", "The minimum size, as a percentage of the GUI, of the aim assist 'red' zone.");

		// Token: 0x04002253 RID: 8787
		public static BoolConVar enableSprintSensitivitySlowdown = new BoolConVar("enable_sprint_sensitivity_slowdown", ConVarFlags.Archive, "1", "Enables sensitivity reduction while sprinting.");

		// Token: 0x04002254 RID: 8788
		private float hitmarkerAlpha;

		// Token: 0x04002255 RID: 8789
		private float hitmarkerTimer;

		// Token: 0x0200060C RID: 1548
		[Obsolete("CameraMode objects are now used instead of enums.", true)]
		public enum CameraMode
		{
			// Token: 0x04002257 RID: 8791
			None,
			// Token: 0x04002258 RID: 8792
			PlayerBasic,
			// Token: 0x04002259 RID: 8793
			Fly,
			// Token: 0x0400225A RID: 8794
			SpectateOrbit,
			// Token: 0x0400225B RID: 8795
			SpectateUser
		}

		// Token: 0x0200060D RID: 1549
		public struct AimAssistInfo
		{
			// Token: 0x0400225C RID: 8796
			public HurtBox aimAssistHurtbox;

			// Token: 0x0400225D RID: 8797
			public Vector3 localPositionOnHurtbox;

			// Token: 0x0400225E RID: 8798
			public Vector3 worldPosition;
		}
	}
}
