using System;
using System.Collections.Generic;
using System.Linq;
using EntityStates.VoidJailer.Weapon;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidJailer
{
	// Token: 0x02000152 RID: 338
	public class Capture : BaseState
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00019194 File Offset: 0x00017394
		public override void OnEnter()
		{
			this.muzzleTransform = base.FindModelChild(Capture.muzzleName);
			if (NetworkServer.active)
			{
				this.InitializeTethers();
			}
			if (NetworkServer.active && base.isAuthority && this.tetherControllers.Count == 0)
			{
				this.outer.SetNextState(new ExitCapture());
				return;
			}
			base.OnEnter();
			this.duration /= this.attackSpeedStat;
			this.FireTethers();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001920C File Offset: 0x0001740C
		private void InitializeTethers()
		{
			Vector3 position = this.muzzleTransform.position;
			float breakDistanceSqr = Capture.maxTetherDistance * Capture.maxTetherDistance;
			this.tetherControllers = new List<JailerTetherController>();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = position;
			bullseyeSearch.maxDistanceFilter = Capture.maxTetherDistance;
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.teamComponent.teamIndex);
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.filterByLoS = true;
			bullseyeSearch.searchDirection = Vector3.up;
			bullseyeSearch.RefreshCandidates();
			bullseyeSearch.FilterOutGameObject(base.gameObject);
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			for (int i = 0; i < list.Count; i++)
			{
				GameObject gameObject = list[i].healthComponent.gameObject;
				if (gameObject && !Capture.TargetIsTethered(gameObject.GetComponent<CharacterBody>()))
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(Capture.tetherPrefab, position, Quaternion.identity, this.muzzleTransform);
					JailerTetherController component = gameObject2.GetComponent<JailerTetherController>();
					component.NetworkownerRoot = base.gameObject;
					component.Networkorigin = this.muzzleTransform.gameObject;
					component.NetworktargetRoot = gameObject;
					component.breakDistanceSqr = breakDistanceSqr;
					component.damageCoefficientPerTick = Capture.damagePerSecond / Capture.damageTickFrequency;
					component.tickInterval = 1f / Capture.damageTickFrequency;
					component.tickTimer = (float)i * 0.1f;
					component.NetworkreelSpeed = Capture.tetherReelSpeed;
					component.SetTetheredBuff(Capture.tetherDebuff);
					this.tetherControllers.Add(component);
					NetworkServer.Spawn(gameObject2);
				}
			}
			this.initialized = true;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00019398 File Offset: 0x00017598
		private static bool TargetIsTethered(CharacterBody characterBody)
		{
			return characterBody != null && characterBody.HasBuff(Capture.tetherDebuff);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000193B0 File Offset: 0x000175B0
		private void FireTethers()
		{
			base.PlayAnimation(Capture.animationLayerName, Capture.animationStateName, Capture.animationPlaybackRateName, this.duration);
			this.soundID = Util.PlayAttackSpeedSound(Capture.captureLoopSoundString, base.gameObject, this.attackSpeedStat);
			if (this.muzzleTransform)
			{
				if (Capture.captureRangeEffect)
				{
					this.rangeEffect = UnityEngine.Object.Instantiate<GameObject>(Capture.captureRangeEffect, base.characterBody.transform);
					this.rangeEffect.transform.localScale = Capture.effectScaleCoefficient * (Capture.maxTetherDistance + Capture.effectScaleAddition) * Vector3.one;
					ScaleParticleSystemDuration component = this.rangeEffect.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = this.duration;
					}
				}
				if (Capture.chargeVfxPrefab)
				{
					this._chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(Capture.chargeVfxPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation, this.muzzleTransform);
					ScaleParticleSystemDuration component2 = this._chargeVfxInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component2)
					{
						component2.newDuration = this.duration;
					}
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000194D0 File Offset: 0x000176D0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.initialized)
			{
				this.UpdateTethers(base.gameObject.transform.position);
				if (this.tetherControllers.Count == 0 || base.fixedAge >= this.duration)
				{
					this.outer.SetNextState(new ExitCapture());
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001952C File Offset: 0x0001772C
		private void UpdateTethers(Vector3 origin)
		{
			for (int i = this.tetherControllers.Count - 1; i >= 0; i--)
			{
				JailerTetherController jailerTetherController = this.tetherControllers[i];
				if (!jailerTetherController)
				{
					this.tetherControllers.RemoveAt(i);
				}
				else
				{
					CharacterBody targetBody = jailerTetherController.GetTargetBody();
					if (!(targetBody == null) && !targetBody.HasBuff(Capture.innerRangeDebuff) && Vector3.Distance(origin, targetBody.transform.position) < Capture.innerRange)
					{
						float num = this.duration - base.fixedAge;
						targetBody.AddTimedBuff(Capture.innerRangeDebuff, Capture.innerRangeDebuffDuration);
						if (this.shouldModifyTetherDuration)
						{
							this.duration = ((num > Capture.innerRangeDebuffDuration) ? Capture.innerRangeDebuffDuration : (Capture.innerRangeDebuffDuration + base.fixedAge));
							base.PlayAnimation(Capture.animationLayerName, Capture.animationStateName, Capture.animationPlaybackRateName, Capture.innerRangeDebuffDuration);
							this.shouldModifyTetherDuration = !Capture.singleDurationReset;
							if (this._chargeVfxInstance)
							{
								ScaleParticleSystemDuration component = this._chargeVfxInstance.GetComponent<ScaleParticleSystemDuration>();
								if (component)
								{
									component.newDuration = this.duration;
								}
							}
						}
						jailerTetherController.NetworkreelSpeed = 0f;
					}
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019668 File Offset: 0x00017868
		public override void OnExit()
		{
			this.DestroyTethers();
			if (this.rangeEffect)
			{
				EntityState.Destroy(this.rangeEffect);
			}
			if (this._chargeVfxInstance)
			{
				EntityState.Destroy(this._chargeVfxInstance);
			}
			AkSoundEngine.StopPlayingID(this.soundID);
			base.OnExit();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000196BC File Offset: 0x000178BC
		private void DestroyTethers()
		{
			if (this.tetherControllers != null)
			{
				for (int i = this.tetherControllers.Count - 1; i >= 0; i--)
				{
					if (this.tetherControllers[i])
					{
						EntityState.Destroy(this.tetherControllers[i].gameObject);
					}
				}
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000703 RID: 1795
		public static string animationLayerName;

		// Token: 0x04000704 RID: 1796
		public static string animationStateName;

		// Token: 0x04000705 RID: 1797
		public static string animationPlaybackRateName;

		// Token: 0x04000706 RID: 1798
		public static GameObject chargeVfxPrefab;

		// Token: 0x04000707 RID: 1799
		[SerializeField]
		public float duration;

		// Token: 0x04000708 RID: 1800
		public static GameObject tetherPrefab;

		// Token: 0x04000709 RID: 1801
		public static GameObject captureRangeEffect;

		// Token: 0x0400070A RID: 1802
		public static float effectScaleCoefficient;

		// Token: 0x0400070B RID: 1803
		public static float effectScaleAddition;

		// Token: 0x0400070C RID: 1804
		public static string muzzleName;

		// Token: 0x0400070D RID: 1805
		public static string tetherOriginName;

		// Token: 0x0400070E RID: 1806
		public static float maxTetherDistance;

		// Token: 0x0400070F RID: 1807
		public static float tetherReelSpeed;

		// Token: 0x04000710 RID: 1808
		public static float damagePerSecond;

		// Token: 0x04000711 RID: 1809
		public static float damageTickFrequency;

		// Token: 0x04000712 RID: 1810
		public static BuffDef tetherDebuff;

		// Token: 0x04000713 RID: 1811
		public static float innerRange;

		// Token: 0x04000714 RID: 1812
		public static BuffDef innerRangeDebuff;

		// Token: 0x04000715 RID: 1813
		public static float innerRangeDebuffDuration;

		// Token: 0x04000716 RID: 1814
		public static GameObject innerRangeDebuffEffect;

		// Token: 0x04000717 RID: 1815
		public static bool singleDurationReset;

		// Token: 0x04000718 RID: 1816
		public static string captureLoopSoundString;

		// Token: 0x04000719 RID: 1817
		private Transform muzzleTransform;

		// Token: 0x0400071A RID: 1818
		private List<JailerTetherController> tetherControllers = new List<JailerTetherController>();

		// Token: 0x0400071B RID: 1819
		private uint soundID;

		// Token: 0x0400071C RID: 1820
		private bool initialized;

		// Token: 0x0400071D RID: 1821
		private bool shouldModifyTetherDuration = true;

		// Token: 0x0400071E RID: 1822
		private GameObject rangeEffect;

		// Token: 0x0400071F RID: 1823
		private GameObject _chargeVfxInstance;
	}
}
