using System;
using System.Collections.Generic;
using RoR2.WwiseUtils;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CE3 RID: 3299
	[RequireComponent(typeof(RectTransform))]
	public class CrosshairManager : MonoBehaviour
	{
		// Token: 0x06004B32 RID: 19250 RVA: 0x00135003 File Offset: 0x00133203
		private void OnEnable()
		{
			CrosshairManager.instancesList.Add(this);
			this.rtpcDamageDirection = new RtpcSetter("damageDirection", RoR2Application.instance.gameObject);
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x0013502A File Offset: 0x0013322A
		private void OnDisable()
		{
			CrosshairManager.instancesList.Remove(this);
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x00135038 File Offset: 0x00133238
		private static void StaticLateUpdate()
		{
			for (int i = 0; i < CrosshairManager.instancesList.Count; i++)
			{
				CrosshairManager.instancesList[i].DoLateUpdate();
			}
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x0013506C File Offset: 0x0013326C
		private void DoLateUpdate()
		{
			if (this.cameraRigController)
			{
				this.UpdateCrosshair(this.cameraRigController.target ? this.cameraRigController.target.GetComponent<CharacterBody>() : null, this.cameraRigController.crosshairWorldPosition, this.cameraRigController.uiCam);
			}
			this.UpdateHitMarker();
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x001350D0 File Offset: 0x001332D0
		private void UpdateCrosshair(CharacterBody targetBody, Vector3 crosshairWorldPosition, Camera uiCamera)
		{
			GameObject gameObject = null;
			bool flag = targetBody && !targetBody.currentVehicle;
			CrosshairManager.ShouldShowCrosshairDelegate shouldShowCrosshairDelegate = this.shouldShowCrosshairGlobal;
			if (shouldShowCrosshairDelegate != null)
			{
				shouldShowCrosshairDelegate(this, ref flag);
			}
			if (flag && targetBody)
			{
				if (!this.cameraRigController.hasOverride)
				{
					if (targetBody && targetBody.healthComponent.alive)
					{
						if (!targetBody.isSprinting)
						{
							gameObject = (targetBody.hideCrosshair ? null : CrosshairUtils.GetCrosshairPrefabForBody(targetBody));
						}
						else
						{
							gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/SprintingCrosshair");
						}
					}
				}
				else
				{
					gameObject = CrosshairUtils.GetCrosshairPrefabForBody(targetBody);
				}
				CrosshairManager.PickCrosshairDelegate pickCrosshairDelegate = this.pickCrosshairGlobal;
				if (pickCrosshairDelegate != null)
				{
					pickCrosshairDelegate(this, ref gameObject);
				}
			}
			if (gameObject != this.currentCrosshairPrefab)
			{
				if (this.crosshairController)
				{
					UnityEngine.Object.Destroy(this.crosshairController.gameObject);
					this.crosshairController = null;
				}
				if (gameObject)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, this.container);
					this.crosshairController = gameObject2.GetComponent<CrosshairController>();
					this.crosshairHudElement = gameObject2.GetComponent<HudElement>();
				}
				this.currentCrosshairPrefab = gameObject;
			}
			if (this.crosshairController)
			{
				((RectTransform)this.crosshairController.gameObject.transform).anchoredPosition = new Vector2(0.5f, 0.5f);
			}
			if (this.crosshairHudElement)
			{
				this.crosshairHudElement.targetCharacterBody = targetBody;
			}
		}

		// Token: 0x14000106 RID: 262
		// (add) Token: 0x06004B37 RID: 19255 RVA: 0x00135234 File Offset: 0x00133434
		// (remove) Token: 0x06004B38 RID: 19256 RVA: 0x0013526C File Offset: 0x0013346C
		public event CrosshairManager.ShouldShowCrosshairDelegate shouldShowCrosshairGlobal;

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x06004B39 RID: 19257 RVA: 0x001352A4 File Offset: 0x001334A4
		// (remove) Token: 0x06004B3A RID: 19258 RVA: 0x001352DC File Offset: 0x001334DC
		public event CrosshairManager.PickCrosshairDelegate pickCrosshairGlobal;

		// Token: 0x06004B3B RID: 19259 RVA: 0x00135314 File Offset: 0x00133514
		public void RefreshHitmarker(bool crit)
		{
			this.hitmarkerTimer = 0.2f;
			this.hitmarker.gameObject.SetActive(false);
			this.hitmarker.gameObject.SetActive(true);
			Util.PlaySound("Play_UI_hit", RoR2Application.instance.gameObject);
			if (crit)
			{
				Util.PlaySound("Play_UI_crit", RoR2Application.instance.gameObject);
			}
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x0013537C File Offset: 0x0013357C
		private void UpdateHitMarker()
		{
			this.hitmarkerAlpha = Mathf.Pow(this.hitmarkerTimer / 0.2f, 0.75f);
			this.hitmarkerTimer = Mathf.Max(0f, this.hitmarkerTimer - Time.deltaTime);
			if (this.hitmarker)
			{
				Color color = this.hitmarker.color;
				color.a = this.hitmarkerAlpha;
				this.hitmarker.color = color;
			}
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x001353F4 File Offset: 0x001335F4
		private static void HandleHitMarker(DamageDealtMessage damageDealtMessage)
		{
			for (int i = 0; i < CrosshairManager.instancesList.Count; i++)
			{
				CrosshairManager crosshairManager = CrosshairManager.instancesList[i];
				if (crosshairManager.cameraRigController)
				{
					GameObject target = crosshairManager.cameraRigController.target;
					if (damageDealtMessage.attacker == target)
					{
						crosshairManager.RefreshHitmarker(damageDealtMessage.crit);
					}
					else if (damageDealtMessage.victim == target)
					{
						Transform transform = crosshairManager.cameraRigController.transform;
						Vector3 position = transform.position;
						Vector3 forward = transform.forward;
						Vector3 position2 = transform.position;
						Vector3 vector = damageDealtMessage.position - position;
						float num = Vector2.SignedAngle(new Vector2(vector.x, vector.z), new Vector2(forward.x, forward.z));
						if (num < 0f)
						{
							num += 360f;
						}
						crosshairManager.rtpcDamageDirection.value = num;
						Util.PlaySound("Play_UI_takeDamage", RoR2Application.instance.gameObject);
					}
				}
			}
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x00135504 File Offset: 0x00133704
		static CrosshairManager()
		{
			GlobalEventManager.onClientDamageNotified += CrosshairManager.HandleHitMarker;
			RoR2Application.onLateUpdate += CrosshairManager.StaticLateUpdate;
		}

		// Token: 0x040047EB RID: 18411
		[Tooltip("The transform which should act as the container for the crosshair.")]
		public RectTransform container;

		// Token: 0x040047EC RID: 18412
		public CameraRigController cameraRigController;

		// Token: 0x040047ED RID: 18413
		[Tooltip("The hitmarker image.")]
		public Image hitmarker;

		// Token: 0x040047EE RID: 18414
		private float hitmarkerAlpha;

		// Token: 0x040047EF RID: 18415
		private float hitmarkerTimer;

		// Token: 0x040047F0 RID: 18416
		private const float hitmarkerDuration = 0.2f;

		// Token: 0x040047F1 RID: 18417
		private GameObject currentCrosshairPrefab;

		// Token: 0x040047F2 RID: 18418
		public CrosshairController crosshairController;

		// Token: 0x040047F3 RID: 18419
		private HudElement crosshairHudElement;

		// Token: 0x040047F4 RID: 18420
		private RtpcSetter rtpcDamageDirection;

		// Token: 0x040047F5 RID: 18421
		private static readonly List<CrosshairManager> instancesList = new List<CrosshairManager>();

		// Token: 0x02000CE4 RID: 3300
		// (Invoke) Token: 0x06004B41 RID: 19265
		public delegate void ShouldShowCrosshairDelegate(CrosshairManager crosshairManager, ref bool shouldShow);

		// Token: 0x02000CE5 RID: 3301
		// (Invoke) Token: 0x06004B45 RID: 19269
		public delegate void PickCrosshairDelegate(CrosshairManager crosshairManager, ref GameObject crosshairPrefab);
	}
}
