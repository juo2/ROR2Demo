using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200092D RID: 2349
	public class Indicator
	{
		// Token: 0x0600350E RID: 13582 RVA: 0x000E0A25 File Offset: 0x000DEC25
		public Indicator(GameObject owner, GameObject visualizerPrefab)
		{
			this.owner = owner;
			this._visualizerPrefab = visualizerPrefab;
			this.visualizerRenderers = Array.Empty<Renderer>();
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000E0A4D File Offset: 0x000DEC4D
		// (set) Token: 0x06003510 RID: 13584 RVA: 0x000E0A55 File Offset: 0x000DEC55
		public GameObject visualizerPrefab
		{
			get
			{
				return this._visualizerPrefab;
			}
			set
			{
				if (this._visualizerPrefab == value)
				{
					return;
				}
				this._visualizerPrefab = value;
				if (this.visualizerInstance)
				{
					this.DestroyVisualizer();
					this.InstantiateVisualizer();
				}
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000E0A86 File Offset: 0x000DEC86
		// (set) Token: 0x06003512 RID: 13586 RVA: 0x000E0A8E File Offset: 0x000DEC8E
		private protected GameObject visualizerInstance { protected get; private set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000E0A97 File Offset: 0x000DEC97
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x000E0A9F File Offset: 0x000DEC9F
		private protected Transform visualizerTransform { protected get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000E0AA8 File Offset: 0x000DECA8
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x000E0AB0 File Offset: 0x000DECB0
		private protected Renderer[] visualizerRenderers { protected get; private set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000E0AB9 File Offset: 0x000DECB9
		public bool hasVisualizer
		{
			get
			{
				return this.visualizerInstance;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000E0AC6 File Offset: 0x000DECC6
		// (set) Token: 0x06003519 RID: 13593 RVA: 0x000E0ACE File Offset: 0x000DECCE
		public bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				if (this._active == value)
				{
					return;
				}
				this._active = value;
				if (this.active)
				{
					Indicator.IndicatorManager.AddIndicator(this);
					return;
				}
				Indicator.IndicatorManager.RemoveIndicator(this);
			}
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000E0AF6 File Offset: 0x000DECF6
		public void SetVisualizerInstantiated(bool newVisualizerInstantiated)
		{
			if (this.visualizerInstance != newVisualizerInstantiated)
			{
				if (newVisualizerInstantiated)
				{
					this.InstantiateVisualizer();
					return;
				}
				this.DestroyVisualizer();
			}
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000E0B16 File Offset: 0x000DED16
		private void InstantiateVisualizer()
		{
			this.visualizerInstance = UnityEngine.Object.Instantiate<GameObject>(this.visualizerPrefab);
			this.OnInstantiateVisualizer();
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000E0B2F File Offset: 0x000DED2F
		private void DestroyVisualizer()
		{
			this.OnDestroyVisualizer();
			UnityEngine.Object.Destroy(this.visualizerInstance);
			this.visualizerInstance = null;
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000E0B49 File Offset: 0x000DED49
		public void OnInstantiateVisualizer()
		{
			this.visualizerTransform = this.visualizerInstance.transform;
			this.visualizerRenderers = this.visualizerInstance.GetComponentsInChildren<Renderer>();
			this.SetVisibleInternal(this.visible);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000E0B79 File Offset: 0x000DED79
		public virtual void OnDestroyVisualizer()
		{
			this.visualizerTransform = null;
			this.visualizerRenderers = Array.Empty<Renderer>();
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void UpdateVisualizer()
		{
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000E0B90 File Offset: 0x000DED90
		public virtual void PositionForUI(Camera sceneCamera, Camera uiCamera)
		{
			if (this.targetTransform)
			{
				Vector3 position = this.targetTransform.position;
				Vector3 vector = sceneCamera.WorldToScreenPoint(position);
				vector.z = ((vector.z > 0f) ? 1f : -1f);
				Vector3 position2 = uiCamera.ScreenToWorldPoint(vector);
				if (this.visualizerTransform != null)
				{
					this.visualizerTransform.position = position2;
				}
			}
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000E0C00 File Offset: 0x000DEE00
		public void SetVisible(bool newVisible)
		{
			newVisible &= this.targetTransform;
			if (this.visible != newVisible)
			{
				this.SetVisibleInternal(newVisible);
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000E0C24 File Offset: 0x000DEE24
		private void SetVisibleInternal(bool newVisible)
		{
			this.visible = newVisible;
			Renderer[] visualizerRenderers = this.visualizerRenderers;
			for (int i = 0; i < visualizerRenderers.Length; i++)
			{
				visualizerRenderers[i].enabled = newVisible;
			}
		}

		// Token: 0x040035FD RID: 13821
		private GameObject _visualizerPrefab;

		// Token: 0x040035FE RID: 13822
		public readonly GameObject owner;

		// Token: 0x040035FF RID: 13823
		public Transform targetTransform;

		// Token: 0x04003603 RID: 13827
		private bool _active;

		// Token: 0x04003604 RID: 13828
		private bool visible = true;

		// Token: 0x0200092E RID: 2350
		private static class IndicatorManager
		{
			// Token: 0x06003523 RID: 13603 RVA: 0x000E0C56 File Offset: 0x000DEE56
			public static void AddIndicator([NotNull] Indicator indicator)
			{
				Indicator.IndicatorManager.runningIndicators.Add(indicator);
				Indicator.IndicatorManager.RebuildVisualizer(indicator);
			}

			// Token: 0x06003524 RID: 13604 RVA: 0x000E0C69 File Offset: 0x000DEE69
			public static void RemoveIndicator([NotNull] Indicator indicator)
			{
				indicator.SetVisualizerInstantiated(false);
				Indicator.IndicatorManager.runningIndicators.Remove(indicator);
			}

			// Token: 0x06003525 RID: 13605 RVA: 0x000E0C80 File Offset: 0x000DEE80
			static IndicatorManager()
			{
				CameraRigController.onCameraTargetChanged += delegate(CameraRigController cameraRigController, GameObject target)
				{
					Indicator.IndicatorManager.RebuildVisualizerForAll();
				};
				UICamera.onUICameraPreRender += Indicator.IndicatorManager.OnPreRenderUI;
				UICamera.onUICameraPostRender += Indicator.IndicatorManager.OnPostRenderUI;
				RoR2Application.onUpdate += Indicator.IndicatorManager.Update;
			}

			// Token: 0x06003526 RID: 13606 RVA: 0x000E0CE0 File Offset: 0x000DEEE0
			private static void RebuildVisualizerForAll()
			{
				foreach (Indicator indicator in Indicator.IndicatorManager.runningIndicators)
				{
					Indicator.IndicatorManager.RebuildVisualizer(indicator);
				}
			}

			// Token: 0x06003527 RID: 13607 RVA: 0x000E0D30 File Offset: 0x000DEF30
			private static void Update()
			{
				foreach (Indicator indicator in Indicator.IndicatorManager.runningIndicators)
				{
					if (indicator.hasVisualizer)
					{
						indicator.UpdateVisualizer();
					}
				}
			}

			// Token: 0x06003528 RID: 13608 RVA: 0x000E0D8C File Offset: 0x000DEF8C
			private static void RebuildVisualizer(Indicator indicator)
			{
				bool visualizerInstantiated = false;
				using (IEnumerator<CameraRigController> enumerator = CameraRigController.readOnlyInstancesList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.target == indicator.owner)
						{
							visualizerInstantiated = true;
							break;
						}
					}
				}
				indicator.SetVisualizerInstantiated(visualizerInstantiated);
			}

			// Token: 0x06003529 RID: 13609 RVA: 0x000E0DF0 File Offset: 0x000DEFF0
			private static void OnPreRenderUI(UICamera uiCam)
			{
				GameObject target = uiCam.cameraRigController.target;
				Camera sceneCam = uiCam.cameraRigController.sceneCam;
				foreach (Indicator indicator in Indicator.IndicatorManager.runningIndicators)
				{
					bool flag = target == indicator.owner;
					indicator.SetVisible(target == indicator.owner);
					if (flag)
					{
						indicator.PositionForUI(sceneCam, uiCam.camera);
					}
				}
			}

			// Token: 0x0600352A RID: 13610 RVA: 0x000E0E80 File Offset: 0x000DF080
			private static void OnPostRenderUI(UICamera uiCamera)
			{
				foreach (Indicator indicator in Indicator.IndicatorManager.runningIndicators)
				{
					indicator.SetVisible(true);
				}
			}

			// Token: 0x04003605 RID: 13829
			private static readonly List<Indicator> runningIndicators = new List<Indicator>();
		}
	}
}
