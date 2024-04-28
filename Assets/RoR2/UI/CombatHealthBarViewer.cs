using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CD2 RID: 3282
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(RectTransform))]
	public class CombatHealthBarViewer : MonoBehaviour, ILayoutGroup, ILayoutController
	{
		// Token: 0x06004ACE RID: 19150 RVA: 0x001336B5 File Offset: 0x001318B5
		static CombatHealthBarViewer()
		{
			GlobalEventManager.onClientDamageNotified += delegate(DamageDealtMessage msg)
			{
				if (!msg.victim || msg.isSilent)
				{
					return;
				}
				HealthComponent component = msg.victim.GetComponent<HealthComponent>();
				if (!component || component.dontShowHealthbar)
				{
					return;
				}
				TeamIndex objectTeam = TeamComponent.GetObjectTeam(component.gameObject);
				foreach (CombatHealthBarViewer combatHealthBarViewer in CombatHealthBarViewer.instancesList)
				{
					if (msg.attacker == combatHealthBarViewer.viewerBodyObject && combatHealthBarViewer.viewerBodyObject)
					{
						combatHealthBarViewer.HandleDamage(component, objectTeam);
					}
				}
			};
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x001336D6 File Offset: 0x001318D6
		// (set) Token: 0x06004AD0 RID: 19152 RVA: 0x001336DE File Offset: 0x001318DE
		public HealthComponent crosshairTarget { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x001336E7 File Offset: 0x001318E7
		// (set) Token: 0x06004AD2 RID: 19154 RVA: 0x001336EF File Offset: 0x001318EF
		public GameObject viewerBodyObject { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x001336F8 File Offset: 0x001318F8
		// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x00133700 File Offset: 0x00131900
		public CharacterBody viewerBody { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x00133709 File Offset: 0x00131909
		// (set) Token: 0x06004AD6 RID: 19158 RVA: 0x00133711 File Offset: 0x00131911
		public TeamIndex viewerTeamIndex { get; set; }

		// Token: 0x06004AD7 RID: 19159 RVA: 0x0013371A File Offset: 0x0013191A
		private void Update()
		{
			if (this.crosshairTarget)
			{
				CombatHealthBarViewer.HealthBarInfo healthBarInfo = this.GetHealthBarInfo(this.crosshairTarget);
				healthBarInfo.endTime = Mathf.Max(healthBarInfo.endTime, Time.time + 1f);
			}
			this.SetDirty();
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00133756 File Offset: 0x00131956
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.canvas = base.GetComponent<Canvas>();
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00133775 File Offset: 0x00131975
		private void Start()
		{
			this.FindCamera();
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x0013377D File Offset: 0x0013197D
		private void FindCamera()
		{
			this.uiCamera = this.canvas.rootCanvas.worldCamera.GetComponent<UICamera>();
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x0013379A File Offset: 0x0013199A
		private void OnEnable()
		{
			CombatHealthBarViewer.instancesList.Add(this);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x001337A8 File Offset: 0x001319A8
		private void OnDisable()
		{
			CombatHealthBarViewer.instancesList.Remove(this);
			for (int i = this.trackedVictims.Count - 1; i >= 0; i--)
			{
				this.Remove(i);
			}
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001337E0 File Offset: 0x001319E0
		private void Remove(int trackedVictimIndex)
		{
			this.Remove(trackedVictimIndex, this.victimToHealthBarInfo[this.trackedVictims[trackedVictimIndex]]);
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00133800 File Offset: 0x00131A00
		private void Remove(int trackedVictimIndex, CombatHealthBarViewer.HealthBarInfo healthBarInfo)
		{
			this.trackedVictims.RemoveAt(trackedVictimIndex);
			UnityEngine.Object.Destroy(healthBarInfo.healthBarRootObject);
			this.victimToHealthBarInfo.Remove(healthBarInfo.sourceHealthComponent);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0013382B File Offset: 0x00131A2B
		private bool VictimIsValid(HealthComponent victim)
		{
			return victim && victim.alive && (this.victimToHealthBarInfo[victim].endTime > Time.time || victim == this.crosshairTarget);
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x00133865 File Offset: 0x00131A65
		private void LateUpdate()
		{
			this.CleanUp();
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x00133870 File Offset: 0x00131A70
		private void CleanUp()
		{
			for (int i = this.trackedVictims.Count - 1; i >= 0; i--)
			{
				HealthComponent healthComponent = this.trackedVictims[i];
				if (!this.VictimIsValid(healthComponent))
				{
					this.Remove(i, this.victimToHealthBarInfo[healthComponent]);
				}
			}
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x001338C0 File Offset: 0x00131AC0
		private void UpdateAllHealthbarPositions(Camera sceneCam, Camera uiCam)
		{
			if (sceneCam && uiCam)
			{
				foreach (CombatHealthBarViewer.HealthBarInfo healthBarInfo in this.victimToHealthBarInfo.Values)
				{
					if (healthBarInfo.sourceTransform && healthBarInfo.healthBarRootObjectTransform)
					{
						Vector3 position = healthBarInfo.sourceTransform.position;
						position.y += healthBarInfo.verticalOffset;
						Vector3 vector = sceneCam.WorldToScreenPoint(position);
						vector.z = ((vector.z > 0f) ? 1f : -1f);
						Vector3 position2 = uiCam.ScreenToWorldPoint(vector);
						healthBarInfo.healthBarRootObjectTransform.position = position2;
					}
				}
			}
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x001339A8 File Offset: 0x00131BA8
		private void HandleDamage(HealthComponent victimHealthComponent, TeamIndex victimTeam)
		{
			if (this.viewerTeamIndex == victimTeam || victimTeam == TeamIndex.None)
			{
				return;
			}
			CharacterBody body = victimHealthComponent.body;
			if (body && body.GetVisibilityLevel(this.viewerBody) < VisibilityLevel.Revealed)
			{
				return;
			}
			this.GetHealthBarInfo(victimHealthComponent).endTime = Time.time + this.healthBarDuration;
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x001339FC File Offset: 0x00131BFC
		private CombatHealthBarViewer.HealthBarInfo GetHealthBarInfo(HealthComponent victimHealthComponent)
		{
			CombatHealthBarViewer.HealthBarInfo healthBarInfo;
			if (!this.victimToHealthBarInfo.TryGetValue(victimHealthComponent, out healthBarInfo))
			{
				healthBarInfo = new CombatHealthBarViewer.HealthBarInfo();
				healthBarInfo.healthBarRootObject = UnityEngine.Object.Instantiate<GameObject>(this.healthBarPrefab, this.container);
				healthBarInfo.healthBarRootObjectTransform = healthBarInfo.healthBarRootObject.transform;
				healthBarInfo.healthBar = healthBarInfo.healthBarRootObject.GetComponent<HealthBar>();
				healthBarInfo.healthBar.source = victimHealthComponent;
				healthBarInfo.healthBar.viewerBody = this.viewerBody;
				healthBarInfo.healthBarRootObject.GetComponentInChildren<BuffDisplay>().source = victimHealthComponent.body;
				healthBarInfo.sourceHealthComponent = victimHealthComponent;
				healthBarInfo.verticalOffset = 0f;
				Collider component = victimHealthComponent.GetComponent<Collider>();
				if (component)
				{
					healthBarInfo.verticalOffset = component.bounds.extents.y;
				}
				healthBarInfo.sourceTransform = (victimHealthComponent.body.coreTransform ?? victimHealthComponent.transform);
				ModelLocator component2 = victimHealthComponent.GetComponent<ModelLocator>();
				if (component2)
				{
					Transform modelTransform = component2.modelTransform;
					if (modelTransform)
					{
						ChildLocator component3 = modelTransform.GetComponent<ChildLocator>();
						if (component3)
						{
							Transform transform = component3.FindChild("HealthBarOrigin");
							if (transform)
							{
								healthBarInfo.sourceTransform = transform;
								healthBarInfo.verticalOffset = 0f;
							}
						}
					}
				}
				this.victimToHealthBarInfo.Add(victimHealthComponent, healthBarInfo);
				this.trackedVictims.Add(victimHealthComponent);
			}
			return healthBarInfo;
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00133B59 File Offset: 0x00131D59
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

		// Token: 0x06004AE6 RID: 19174 RVA: 0x00133B78 File Offset: 0x00131D78
		private static void LayoutForCamera(UICamera uiCamera)
		{
			Camera camera = uiCamera.camera;
			Camera sceneCam = uiCamera.cameraRigController.sceneCam;
			for (int i = 0; i < CombatHealthBarViewer.instancesList.Count; i++)
			{
				CombatHealthBarViewer.instancesList[i].UpdateAllHealthbarPositions(sceneCam, camera);
			}
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x00133BBF File Offset: 0x00131DBF
		public void SetLayoutHorizontal()
		{
			if (this.uiCamera)
			{
				CombatHealthBarViewer.LayoutForCamera(this.uiCamera);
			}
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x000026ED File Offset: 0x000008ED
		public void SetLayoutVertical()
		{
		}

		// Token: 0x0400479C RID: 18332
		private static readonly List<CombatHealthBarViewer> instancesList = new List<CombatHealthBarViewer>();

		// Token: 0x0400479D RID: 18333
		public RectTransform container;

		// Token: 0x0400479E RID: 18334
		public GameObject healthBarPrefab;

		// Token: 0x0400479F RID: 18335
		public float healthBarDuration;

		// Token: 0x040047A4 RID: 18340
		private const float hoverHealthBarDuration = 1f;

		// Token: 0x040047A5 RID: 18341
		private RectTransform rectTransform;

		// Token: 0x040047A6 RID: 18342
		private Canvas canvas;

		// Token: 0x040047A7 RID: 18343
		private UICamera uiCamera;

		// Token: 0x040047A8 RID: 18344
		private List<HealthComponent> trackedVictims = new List<HealthComponent>();

		// Token: 0x040047A9 RID: 18345
		private Dictionary<HealthComponent, CombatHealthBarViewer.HealthBarInfo> victimToHealthBarInfo = new Dictionary<HealthComponent, CombatHealthBarViewer.HealthBarInfo>();

		// Token: 0x040047AA RID: 18346
		public float zPosition;

		// Token: 0x040047AB RID: 18347
		private const float overheadOffset = 1f;

		// Token: 0x02000CD3 RID: 3283
		private class HealthBarInfo
		{
			// Token: 0x040047AC RID: 18348
			public HealthComponent sourceHealthComponent;

			// Token: 0x040047AD RID: 18349
			public Transform sourceTransform;

			// Token: 0x040047AE RID: 18350
			public GameObject healthBarRootObject;

			// Token: 0x040047AF RID: 18351
			public Transform healthBarRootObjectTransform;

			// Token: 0x040047B0 RID: 18352
			public HealthBar healthBar;

			// Token: 0x040047B1 RID: 18353
			public float verticalOffset;

			// Token: 0x040047B2 RID: 18354
			public float endTime = float.NegativeInfinity;
		}
	}
}
