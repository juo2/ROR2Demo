using System;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x02000299 RID: 665
	public abstract class BaseChargeBombState : BaseSkillState
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00030F0F File Offset: 0x0002F10F
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00030F17 File Offset: 0x0002F117
		private protected float duration { protected get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00030F20 File Offset: 0x0002F120
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00030F28 File Offset: 0x0002F128
		private protected Animator animator { protected get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00030F31 File Offset: 0x0002F131
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00030F39 File Offset: 0x0002F139
		private protected ChildLocator childLocator { protected get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00030F42 File Offset: 0x0002F142
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00030F4A File Offset: 0x0002F14A
		private protected GameObject chargeEffectInstance { protected get; private set; }

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00030F54 File Offset: 0x0002F154
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.animator = base.GetModelAnimator();
			this.childLocator = base.GetModelChildLocator();
			if (this.childLocator)
			{
				Transform transform = this.childLocator.FindChild("MuzzleBetween") ?? base.characterBody.coreTransform;
				if (transform && this.chargeEffectPrefab)
				{
					this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
					this.chargeEffectInstance.transform.parent = transform;
					ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
					ObjectScaleCurve component2 = this.chargeEffectInstance.GetComponent<ObjectScaleCurve>();
					if (component)
					{
						component.newDuration = this.duration;
					}
					if (component2)
					{
						component2.timeMax = this.duration;
					}
				}
			}
			this.PlayChargeAnimation();
			this.loopSoundInstanceId = Util.PlayAttackSpeedSound(this.chargeSoundString, base.gameObject, this.attackSpeedStat);
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			base.StartAimMode(this.duration + 2f, false);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000310A0 File Offset: 0x0002F2A0
		public override void OnExit()
		{
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			AkSoundEngine.StopPlayingID(this.loopSoundInstanceId);
			if (!this.outer.destroying)
			{
				this.PlayAnimation("Gesture, Additive", "Empty");
			}
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000310F7 File Offset: 0x0002F2F7
		protected float CalcCharge()
		{
			return Mathf.Clamp01(base.fixedAge / this.duration);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0003110C File Offset: 0x0002F30C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			float charge = this.CalcCharge();
			if (base.isAuthority && ((!base.IsKeyDownAuthority() && base.fixedAge >= this.minChargeDuration) || base.fixedAge >= this.duration))
			{
				BaseThrowBombState nextState = this.GetNextState();
				nextState.charge = charge;
				this.outer.SetNextState(nextState);
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003116C File Offset: 0x0002F36C
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(Util.Remap(this.CalcCharge(), 0f, 1f, this.minBloomRadius, this.maxBloomRadius), true);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000BCF RID: 3023
		protected abstract BaseThrowBombState GetNextState();

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000311A1 File Offset: 0x0002F3A1
		protected virtual void PlayChargeAnimation()
		{
			base.PlayAnimation("Gesture, Additive", "ChargeNovaBomb", "ChargeNovaBomb.playbackRate", this.duration);
		}

		// Token: 0x04000DF7 RID: 3575
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04000DF8 RID: 3576
		[SerializeField]
		public string chargeSoundString;

		// Token: 0x04000DF9 RID: 3577
		[SerializeField]
		public float baseDuration = 1.5f;

		// Token: 0x04000DFA RID: 3578
		[SerializeField]
		public float minBloomRadius;

		// Token: 0x04000DFB RID: 3579
		[SerializeField]
		public float maxBloomRadius;

		// Token: 0x04000DFC RID: 3580
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000DFD RID: 3581
		[SerializeField]
		public float minChargeDuration = 0.5f;

		// Token: 0x04000E01 RID: 3585
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000E03 RID: 3587
		private uint loopSoundInstanceId;
	}
}
