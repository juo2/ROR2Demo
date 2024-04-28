using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000133 RID: 307
	public class ChargeMultiBeam : BaseMultiBeamState
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00017594 File Offset: 0x00015794
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator && this.chargeEffectPrefab)
			{
				Transform transform = modelChildLocator.FindChild(this.muzzleName) ?? base.characterBody.coreTransform;
				if (transform)
				{
					this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
					this.chargeEffectInstance.transform.parent = transform;
					ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = this.duration;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.enterSoundString))
			{
				if (this.isSoundScaledByAttackSpeed)
				{
					Util.PlayAttackSpeedSound(this.enterSoundString, base.gameObject, this.attackSpeedStat);
				}
				else
				{
					Util.PlaySound(this.enterSoundString, base.gameObject);
				}
			}
			this.warningLaserEnabled = true;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000176AE File Offset: 0x000158AE
		public override void OnExit()
		{
			this.warningLaserEnabled = false;
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000176C8 File Offset: 0x000158C8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new FireMultiBeamSmall());
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000176F6 File Offset: 0x000158F6
		public override void Update()
		{
			base.Update();
			this.UpdateWarningLaser();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00017704 File Offset: 0x00015904
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00017714 File Offset: 0x00015914
		private bool warningLaserEnabled
		{
			get
			{
				return this.warningLaserVfxInstance;
			}
			set
			{
				if (value == this.warningLaserEnabled)
				{
					return;
				}
				if (value)
				{
					if (this.warningLaserVfxPrefab)
					{
						this.warningLaserVfxInstance = UnityEngine.Object.Instantiate<GameObject>(this.warningLaserVfxPrefab);
						this.warningLaserVfxInstanceRayAttackIndicator = this.warningLaserVfxInstance.GetComponent<RayAttackIndicator>();
						this.UpdateWarningLaser();
						return;
					}
				}
				else
				{
					EntityState.Destroy(this.warningLaserVfxInstance);
					this.warningLaserVfxInstance = null;
					this.warningLaserVfxInstanceRayAttackIndicator = null;
				}
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017780 File Offset: 0x00015980
		private void UpdateWarningLaser()
		{
			if (this.warningLaserVfxInstanceRayAttackIndicator)
			{
				this.warningLaserVfxInstanceRayAttackIndicator.attackRange = BaseMultiBeamState.beamMaxDistance;
				Ray attackRay;
				Vector3 vector;
				base.CalcBeamPath(out attackRay, out vector);
				this.warningLaserVfxInstanceRayAttackIndicator.attackRay = attackRay;
			}
		}

		// Token: 0x04000668 RID: 1640
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000669 RID: 1641
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400066A RID: 1642
		[SerializeField]
		public GameObject warningLaserVfxPrefab;

		// Token: 0x0400066B RID: 1643
		[SerializeField]
		public new string muzzleName;

		// Token: 0x0400066C RID: 1644
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400066D RID: 1645
		[SerializeField]
		public bool isSoundScaledByAttackSpeed;

		// Token: 0x0400066E RID: 1646
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400066F RID: 1647
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000670 RID: 1648
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000671 RID: 1649
		private float duration;

		// Token: 0x04000672 RID: 1650
		private GameObject chargeEffectInstance;

		// Token: 0x04000673 RID: 1651
		private GameObject warningLaserVfxInstance;

		// Token: 0x04000674 RID: 1652
		private RayAttackIndicator warningLaserVfxInstanceRayAttackIndicator;
	}
}
