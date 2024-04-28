using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004D2 RID: 1234
	public static class BackstabManager
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x000627C8 File Offset: 0x000609C8
		public static bool IsBackstab(Vector3 attackerCorePositionToHitPosition, CharacterBody victimBody)
		{
			if (!victimBody.canReceiveBackstab)
			{
				return false;
			}
			Vector3? bodyForward = BackstabManager.GetBodyForward(victimBody);
			return bodyForward != null && Vector3.Dot(attackerCorePositionToHitPosition, bodyForward.Value) > 0f;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00062808 File Offset: 0x00060A08
		private static Vector3? GetBodyForward(CharacterBody characterBody)
		{
			Vector3? result = null;
			if (characterBody.characterDirection)
			{
				result = new Vector3?(characterBody.characterDirection.forward);
			}
			else
			{
				result = new Vector3?(characterBody.transform.forward);
			}
			return result;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00062851 File Offset: 0x00060A51
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			if (BackstabManager.enableVisualizerSystem)
			{
				BackstabManager.InitVisualizerSystem();
			}
			BackstabManager.backstabImpactEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Effects/ImpactEffects/BackstabSpark");
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00062870 File Offset: 0x00060A70
		private static bool ShouldShowBackstab(Vector3 attackerCorePosition, CharacterBody victimBody)
		{
			if (!victimBody.canReceiveBackstab)
			{
				return false;
			}
			Vector3? bodyForward = BackstabManager.GetBodyForward(victimBody);
			return bodyForward != null && Vector3.Dot((attackerCorePosition - victimBody.corePosition).normalized, bodyForward.Value) > BackstabManager.showBackstabThreshold;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000628C0 File Offset: 0x00060AC0
		private static void InitVisualizerSystem()
		{
			CameraRigController.onCameraTargetChanged += BackstabManager.OnCameraTargetChanged;
			CameraRigController.onCameraEnableGlobal += BackstabManager.OnCameraDiscovered;
			CameraRigController.onCameraDisableGlobal += BackstabManager.OnCameraLost;
			SceneCamera.onSceneCameraPreCull += BackstabManager.OnSceneCameraPreCull;
			SceneCamera.onSceneCameraPostRender += BackstabManager.OnSceneCameraPostRender;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00062922 File Offset: 0x00060B22
		private static void OnCameraTargetChanged(CameraRigController camera, GameObject target)
		{
			BackstabManager.RefreshCamera(camera);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00062922 File Offset: 0x00060B22
		private static void OnCameraDiscovered(CameraRigController camera)
		{
			BackstabManager.RefreshCamera(camera);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00062922 File Offset: 0x00060B22
		private static void OnCameraLost(CameraRigController camera)
		{
			BackstabManager.RefreshCamera(camera);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006292C File Offset: 0x00060B2C
		private static void OnSceneCameraPreCull(SceneCamera sceneCam)
		{
			BackstabManager.BackstabVisualizer backstabVisualizer;
			if (BackstabManager.camToVisualizer.TryGetValue(sceneCam.cameraRigController, out backstabVisualizer))
			{
				backstabVisualizer.OnPreCull();
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00062954 File Offset: 0x00060B54
		private static void OnSceneCameraPostRender(SceneCamera sceneCam)
		{
			BackstabManager.BackstabVisualizer backstabVisualizer;
			if (BackstabManager.camToVisualizer.TryGetValue(sceneCam.cameraRigController, out backstabVisualizer))
			{
				backstabVisualizer.OnPostRender();
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006297C File Offset: 0x00060B7C
		private static void RefreshCamera(CameraRigController camera)
		{
			BackstabManager.BackstabVisualizer backstabVisualizer;
			bool flag = BackstabManager.camToVisualizer.TryGetValue(camera, out backstabVisualizer);
			GameObject gameObject = camera.isActiveAndEnabled ? camera.target : null;
			CharacterBody characterBody = gameObject ? gameObject.GetComponent<CharacterBody>() : null;
			bool flag2 = characterBody && characterBody.canPerformBackstab;
			if (flag != flag2)
			{
				if (flag2)
				{
					backstabVisualizer = new BackstabManager.BackstabVisualizer();
					BackstabManager.camToVisualizer.Add(camera, backstabVisualizer);
					backstabVisualizer.Install(camera);
				}
				else
				{
					backstabVisualizer.Uninstall();
					BackstabManager.camToVisualizer.Remove(camera);
					backstabVisualizer = null;
				}
			}
			if (backstabVisualizer != null)
			{
				backstabVisualizer.targetBody = characterBody;
			}
		}

		// Token: 0x04001C1C RID: 7196
		public static GameObject backstabImpactEffectPrefab = null;

		// Token: 0x04001C1D RID: 7197
		private static readonly bool enableVisualizerSystem = false;

		// Token: 0x04001C1E RID: 7198
		private static readonly float showBackstabThreshold = Mathf.Cos(0.7853982f);

		// Token: 0x04001C1F RID: 7199
		private static readonly Dictionary<CameraRigController, BackstabManager.BackstabVisualizer> camToVisualizer = new Dictionary<CameraRigController, BackstabManager.BackstabVisualizer>();

		// Token: 0x020004D3 RID: 1235
		private class BackstabVisualizer
		{
			// Token: 0x06001668 RID: 5736 RVA: 0x00062A32 File Offset: 0x00060C32
			public void Install(CameraRigController newCamera)
			{
				this.camera = newCamera;
				RoR2Application.onLateUpdate += this.UpdateIndicators;
			}

			// Token: 0x06001669 RID: 5737 RVA: 0x00062A4C File Offset: 0x00060C4C
			public void Uninstall()
			{
				RoR2Application.onLateUpdate -= this.UpdateIndicators;
				this.camera = null;
				foreach (KeyValuePair<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo> keyValuePair in this.bodyToIndicator)
				{
					UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
				}
				this.bodyToIndicator.Clear();
				foreach (BackstabManager.BackstabVisualizer.IndicatorInfo indicatorInfo in this.indicatorPool)
				{
					UnityEngine.Object.Destroy(indicatorInfo.gameObject);
				}
				this.indicatorPool.Clear();
			}

			// Token: 0x0600166A RID: 5738 RVA: 0x00062B1C File Offset: 0x00060D1C
			[SystemInitializer(new Type[]
			{

			})]
			private static void OnLoad()
			{
				BackstabManager.BackstabVisualizer.indicatorPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/VFX/BackstabIndicator");
			}

			// Token: 0x0600166B RID: 5739 RVA: 0x00062B30 File Offset: 0x00060D30
			private void UpdateIndicators()
			{
				Vector3 corePosition = this.targetBody.corePosition;
				TeamIndex teamIndex = this.targetBody.teamComponent.teamIndex;
				BackstabManager.BackstabVisualizer.buffer.Clear();
				float unscaledTime = Time.unscaledTime;
				foreach (CharacterBody characterBody in CharacterBody.readOnlyInstancesList)
				{
					bool flag = TeamManager.IsTeamEnemy(teamIndex, characterBody.teamComponent.teamIndex);
					bool flag2 = false;
					if (flag)
					{
						flag2 = BackstabManager.IsBackstab(characterBody.corePosition - corePosition, characterBody);
					}
					if (flag && flag2)
					{
						BackstabManager.BackstabVisualizer.IndicatorInfo indicatorInfo = null;
						if (!this.bodyToIndicator.TryGetValue(characterBody, out indicatorInfo))
						{
							indicatorInfo = this.AddIndicator(characterBody);
						}
						indicatorInfo.lastDisplayTime = unscaledTime;
					}
				}
				List<CharacterBody> list = CollectionPool<CharacterBody, List<CharacterBody>>.RentCollection();
				foreach (KeyValuePair<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo> keyValuePair in this.bodyToIndicator)
				{
					if (keyValuePair.Value.lastDisplayTime != unscaledTime)
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (CharacterBody victimBody in list)
				{
					this.RemoveIndicator(victimBody);
				}
				list = CollectionPool<CharacterBody, List<CharacterBody>>.ReturnCollection(list);
			}

			// Token: 0x0600166C RID: 5740 RVA: 0x00062CA8 File Offset: 0x00060EA8
			private BackstabManager.BackstabVisualizer.IndicatorInfo AddIndicator(CharacterBody victimBody)
			{
				BackstabManager.BackstabVisualizer.IndicatorInfo indicatorInfo;
				if (this.indicatorPool.Count > 0)
				{
					indicatorInfo = this.indicatorPool.Pop();
				}
				else
				{
					indicatorInfo = new BackstabManager.BackstabVisualizer.IndicatorInfo();
					indicatorInfo.gameObject = UnityEngine.Object.Instantiate<GameObject>(BackstabManager.BackstabVisualizer.indicatorPrefab);
					indicatorInfo.transform = indicatorInfo.gameObject.transform;
					indicatorInfo.particleSystem = indicatorInfo.gameObject.GetComponent<ParticleSystem>();
					indicatorInfo.renderer = indicatorInfo.gameObject.GetComponent<ParticleSystemRenderer>();
					indicatorInfo.renderer.enabled = false;
				}
				indicatorInfo.gameObject.SetActive(true);
				indicatorInfo.particleSystem.Play();
				this.bodyToIndicator[victimBody] = indicatorInfo;
				return indicatorInfo;
			}

			// Token: 0x0600166D RID: 5741 RVA: 0x00062D50 File Offset: 0x00060F50
			private void RemoveIndicator(CharacterBody victimBody)
			{
				BackstabManager.BackstabVisualizer.IndicatorInfo indicatorInfo = this.bodyToIndicator[victimBody];
				if (indicatorInfo.gameObject)
				{
					indicatorInfo.particleSystem.Stop();
					indicatorInfo.gameObject.SetActive(false);
				}
				this.bodyToIndicator.Remove(victimBody);
				this.indicatorPool.Push(indicatorInfo);
			}

			// Token: 0x0600166E RID: 5742 RVA: 0x00062DA8 File Offset: 0x00060FA8
			public void OnPreCull()
			{
				foreach (KeyValuePair<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo> keyValuePair in this.bodyToIndicator)
				{
					CharacterBody key = keyValuePair.Key;
					Transform transform = keyValuePair.Value.transform;
					if (key)
					{
						Vector3? bodyForward = BackstabManager.GetBodyForward(key);
						if (bodyForward != null)
						{
							transform.forward = -bodyForward.Value;
						}
						transform.position = key.corePosition - transform.forward * key.radius;
						keyValuePair.Value.renderer.enabled = true;
					}
				}
			}

			// Token: 0x0600166F RID: 5743 RVA: 0x00062E6C File Offset: 0x0006106C
			public void OnPostRender()
			{
				foreach (KeyValuePair<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo> keyValuePair in this.bodyToIndicator)
				{
					keyValuePair.Value.renderer.enabled = false;
				}
			}

			// Token: 0x06001670 RID: 5744 RVA: 0x00062ECC File Offset: 0x000610CC
			private void Update()
			{
				this.UpdateIndicators();
			}

			// Token: 0x04001C20 RID: 7200
			private CameraRigController camera;

			// Token: 0x04001C21 RID: 7201
			public CharacterBody targetBody;

			// Token: 0x04001C22 RID: 7202
			private readonly Dictionary<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo> bodyToIndicator = new Dictionary<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo>();

			// Token: 0x04001C23 RID: 7203
			private static readonly Dictionary<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo> buffer = new Dictionary<CharacterBody, BackstabManager.BackstabVisualizer.IndicatorInfo>();

			// Token: 0x04001C24 RID: 7204
			private readonly Stack<BackstabManager.BackstabVisualizer.IndicatorInfo> indicatorPool = new Stack<BackstabManager.BackstabVisualizer.IndicatorInfo>();

			// Token: 0x04001C25 RID: 7205
			private static GameObject indicatorPrefab;

			// Token: 0x020004D4 RID: 1236
			private class IndicatorInfo
			{
				// Token: 0x04001C26 RID: 7206
				public GameObject gameObject;

				// Token: 0x04001C27 RID: 7207
				public Transform transform;

				// Token: 0x04001C28 RID: 7208
				public ParticleSystem particleSystem;

				// Token: 0x04001C29 RID: 7209
				public ParticleSystemRenderer renderer;

				// Token: 0x04001C2A RID: 7210
				public float lastDisplayTime;
			}
		}
	}
}
