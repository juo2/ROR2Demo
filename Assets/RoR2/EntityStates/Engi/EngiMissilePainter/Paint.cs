using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Engi.EngiMissilePainter
{
	// Token: 0x020003B4 RID: 948
	public class Paint : BaseEngiMissilePainterState
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x0004A4B8 File Offset: 0x000486B8
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.targetsList = new List<HurtBox>();
				this.targetIndicators = new Dictionary<HurtBox, Paint.IndicatorInfo>();
				this.stickyTargetIndicator = new Indicator(base.gameObject, Paint.stickyTargetIndicatorPrefab);
				this.search = new BullseyeSearch();
			}
			base.PlayCrossfade("Gesture, Additive", "PrepHarpoons", 0.1f);
			Util.PlaySound(Paint.enterSoundString, base.gameObject);
			this.loopSoundID = Util.PlaySound(Paint.loopSoundString, base.gameObject);
			if (Paint.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, Paint.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			this.engiConfirmTargetDummySkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("EngiConfirmTargetDummy"));
			this.engiCancelTargetingDummySkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("EngiCancelTargetingDummy"));
			base.skillLocator.primary.SetSkillOverride(this, this.engiConfirmTargetDummySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			base.skillLocator.secondary.SetSkillOverride(this, this.engiCancelTargetingDummySkillDef, GenericSkill.SkillOverridePriority.Contextual);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004A5C4 File Offset: 0x000487C4
		public override void OnExit()
		{
			if (base.isAuthority && !this.outer.destroying && !this.queuedFiringState)
			{
				for (int i = 0; i < this.targetsList.Count; i++)
				{
					base.activatorSkillSlot.AddOneStock();
				}
			}
			base.skillLocator.secondary.UnsetSkillOverride(this, this.engiCancelTargetingDummySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			base.skillLocator.primary.UnsetSkillOverride(this, this.engiConfirmTargetDummySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			if (this.targetIndicators != null)
			{
				foreach (KeyValuePair<HurtBox, Paint.IndicatorInfo> keyValuePair in this.targetIndicators)
				{
					keyValuePair.Value.indicator.active = false;
				}
			}
			if (this.stickyTargetIndicator != null)
			{
				this.stickyTargetIndicator.active = false;
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.PlayCrossfade("Gesture, Additive", "ExitHarpoons", 0.1f);
			Util.PlaySound(Paint.exitSoundString, base.gameObject);
			Util.PlaySound(Paint.stopLoopSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004A6FC File Offset: 0x000488FC
		private void AddTargetAuthority(HurtBox hurtBox)
		{
			if (base.activatorSkillSlot.stock == 0)
			{
				return;
			}
			Util.PlaySound(Paint.lockOnSoundString, base.gameObject);
			this.targetsList.Add(hurtBox);
			Paint.IndicatorInfo indicatorInfo;
			if (!this.targetIndicators.TryGetValue(hurtBox, out indicatorInfo))
			{
				indicatorInfo = new Paint.IndicatorInfo
				{
					refCount = 0,
					indicator = new Paint.EngiMissileIndicator(base.gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiMissileTrackingIndicator"))
				};
				indicatorInfo.indicator.targetTransform = hurtBox.transform;
				indicatorInfo.indicator.active = true;
			}
			indicatorInfo.refCount++;
			indicatorInfo.indicator.missileCount = indicatorInfo.refCount;
			this.targetIndicators[hurtBox] = indicatorInfo;
			base.activatorSkillSlot.DeductStock(1);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004A7C8 File Offset: 0x000489C8
		private void RemoveTargetAtAuthority(int i)
		{
			HurtBox key = this.targetsList[i];
			this.targetsList.RemoveAt(i);
			Paint.IndicatorInfo indicatorInfo;
			if (this.targetIndicators.TryGetValue(key, out indicatorInfo))
			{
				indicatorInfo.refCount--;
				indicatorInfo.indicator.missileCount = indicatorInfo.refCount;
				this.targetIndicators[key] = indicatorInfo;
				if (indicatorInfo.refCount == 0)
				{
					indicatorInfo.indicator.active = false;
					this.targetIndicators.Remove(key);
				}
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0004A84C File Offset: 0x00048A4C
		private void CleanTargetsList()
		{
			for (int i = this.targetsList.Count - 1; i >= 0; i--)
			{
				HurtBox hurtBox = this.targetsList[i];
				if (!hurtBox.healthComponent || !hurtBox.healthComponent.alive)
				{
					this.RemoveTargetAtAuthority(i);
					base.activatorSkillSlot.AddOneStock();
				}
			}
			for (int j = this.targetsList.Count - 1; j >= base.activatorSkillSlot.maxStock; j--)
			{
				this.RemoveTargetAtAuthority(j);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004A8D3 File Offset: 0x00048AD3
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(3f);
			if (base.isAuthority)
			{
				this.AuthorityFixedUpdate();
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0004A8FC File Offset: 0x00048AFC
		private void GetCurrentTargetInfo(out HurtBox currentTargetHurtBox, out HealthComponent currentTargetHealthComponent)
		{
			Ray aimRay = base.GetAimRay();
			this.search.filterByDistinctEntity = true;
			this.search.filterByLoS = true;
			this.search.minDistanceFilter = 0f;
			this.search.maxDistanceFilter = Paint.maxDistance;
			this.search.minAngleFilter = 0f;
			this.search.maxAngleFilter = Paint.maxAngle;
			this.search.viewer = base.characterBody;
			this.search.searchOrigin = aimRay.origin;
			this.search.searchDirection = aimRay.direction;
			this.search.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
			this.search.teamMaskFilter = TeamMask.GetUnprotectedTeams(base.GetTeam());
			this.search.RefreshCandidates();
			this.search.FilterOutGameObject(base.gameObject);
			foreach (HurtBox hurtBox in this.search.GetResults())
			{
				if (hurtBox.healthComponent && hurtBox.healthComponent.alive)
				{
					currentTargetHurtBox = hurtBox;
					currentTargetHealthComponent = hurtBox.healthComponent;
					return;
				}
			}
			currentTargetHurtBox = null;
			currentTargetHealthComponent = null;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0004AA48 File Offset: 0x00048C48
		private void AuthorityFixedUpdate()
		{
			this.CleanTargetsList();
			bool flag = false;
			HurtBox hurtBox;
			HealthComponent y;
			this.GetCurrentTargetInfo(out hurtBox, out y);
			if (hurtBox)
			{
				this.stackStopwatch += Time.fixedDeltaTime;
				if (base.inputBank.skill1.down && (this.previousHighlightTargetHealthComponent != y || this.stackStopwatch > Paint.stackInterval / this.attackSpeedStat || base.inputBank.skill1.justPressed))
				{
					this.stackStopwatch = 0f;
					this.AddTargetAuthority(hurtBox);
				}
			}
			if (base.inputBank.skill1.justReleased)
			{
				flag = true;
			}
			if (base.inputBank.skill2.justReleased)
			{
				this.outer.SetNextStateToMain();
				return;
			}
			if (base.inputBank.skill3.justReleased)
			{
				if (this.releasedKeyOnce)
				{
					flag = true;
				}
				this.releasedKeyOnce = true;
			}
			if (hurtBox != this.previousHighlightTargetHurtBox)
			{
				this.previousHighlightTargetHurtBox = hurtBox;
				this.previousHighlightTargetHealthComponent = y;
				this.stickyTargetIndicator.targetTransform = ((hurtBox && base.activatorSkillSlot.stock != 0) ? hurtBox.transform : null);
				this.stackStopwatch = 0f;
			}
			this.stickyTargetIndicator.active = this.stickyTargetIndicator.targetTransform;
			if (flag)
			{
				this.queuedFiringState = true;
				this.outer.SetNextState(new Fire
				{
					targetsList = this.targetsList,
					activatorSkillSlot = base.activatorSkillSlot
				});
			}
		}

		// Token: 0x04001563 RID: 5475
		public static GameObject crosshairOverridePrefab;

		// Token: 0x04001564 RID: 5476
		public static GameObject stickyTargetIndicatorPrefab;

		// Token: 0x04001565 RID: 5477
		public static float stackInterval;

		// Token: 0x04001566 RID: 5478
		public static string enterSoundString;

		// Token: 0x04001567 RID: 5479
		public static string exitSoundString;

		// Token: 0x04001568 RID: 5480
		public static string loopSoundString;

		// Token: 0x04001569 RID: 5481
		public static string lockOnSoundString;

		// Token: 0x0400156A RID: 5482
		public static string stopLoopSoundString;

		// Token: 0x0400156B RID: 5483
		public static float maxAngle;

		// Token: 0x0400156C RID: 5484
		public static float maxDistance;

		// Token: 0x0400156D RID: 5485
		private List<HurtBox> targetsList;

		// Token: 0x0400156E RID: 5486
		private Dictionary<HurtBox, Paint.IndicatorInfo> targetIndicators;

		// Token: 0x0400156F RID: 5487
		private Indicator stickyTargetIndicator;

		// Token: 0x04001570 RID: 5488
		private SkillDef engiConfirmTargetDummySkillDef;

		// Token: 0x04001571 RID: 5489
		private SkillDef engiCancelTargetingDummySkillDef;

		// Token: 0x04001572 RID: 5490
		private bool releasedKeyOnce;

		// Token: 0x04001573 RID: 5491
		private float stackStopwatch;

		// Token: 0x04001574 RID: 5492
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04001575 RID: 5493
		private BullseyeSearch search;

		// Token: 0x04001576 RID: 5494
		private bool queuedFiringState;

		// Token: 0x04001577 RID: 5495
		private uint loopSoundID;

		// Token: 0x04001578 RID: 5496
		private HealthComponent previousHighlightTargetHealthComponent;

		// Token: 0x04001579 RID: 5497
		private HurtBox previousHighlightTargetHurtBox;

		// Token: 0x020003B5 RID: 949
		private struct IndicatorInfo
		{
			// Token: 0x0400157A RID: 5498
			public int refCount;

			// Token: 0x0400157B RID: 5499
			public Paint.EngiMissileIndicator indicator;
		}

		// Token: 0x020003B6 RID: 950
		private class EngiMissileIndicator : Indicator
		{
			// Token: 0x060010FF RID: 4351 RVA: 0x0004ABC8 File Offset: 0x00048DC8
			public override void UpdateVisualizer()
			{
				base.UpdateVisualizer();
				Transform transform = base.visualizerTransform.Find("DotOrigin");
				for (int i = transform.childCount - 1; i >= this.missileCount; i--)
				{
					EntityState.Destroy(transform.GetChild(i));
				}
				for (int j = transform.childCount; j < this.missileCount; j++)
				{
					UnityEngine.Object.Instantiate<GameObject>(base.visualizerPrefab.transform.Find("DotOrigin/DotTemplate").gameObject, transform);
				}
				if (transform.childCount > 0)
				{
					float num = 360f / (float)transform.childCount;
					float num2 = (float)(transform.childCount - 1) * 90f;
					for (int k = 0; k < transform.childCount; k++)
					{
						Transform child = transform.GetChild(k);
						child.gameObject.SetActive(true);
						child.localRotation = Quaternion.Euler(0f, 0f, num2 + (float)k * num);
					}
				}
			}

			// Token: 0x06001100 RID: 4352 RVA: 0x0004ACB4 File Offset: 0x00048EB4
			public EngiMissileIndicator(GameObject owner, GameObject visualizerPrefab) : base(owner, visualizerPrefab)
			{
			}

			// Token: 0x0400157C RID: 5500
			public int missileCount;
		}
	}
}
