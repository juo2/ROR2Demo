using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Audio;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000B5 RID: 181
	public class BasicMeleeAttack : BaseState
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C608 File Offset: 0x0000A808
		protected bool authorityInHitPause
		{
			get
			{
				return this.hitPauseTimer > 0f;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C617 File Offset: 0x0000A817
		private bool meleeAttackHasBegun
		{
			get
			{
				return this.meleeAttackStartTime.hasPassed;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C624 File Offset: 0x0000A824
		protected bool authorityHasFiredAtAll
		{
			get
			{
				return this.meleeAttackTicks > 0;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C62F File Offset: 0x0000A82F
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000C637 File Offset: 0x0000A837
		private protected bool isCritAuthority { protected get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool allowExitFire
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C640 File Offset: 0x0000A840
		public virtual string GetHitBoxGroupName()
		{
			return this.hitBoxGroupName;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C648 File Offset: 0x0000A848
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.CalcDuration();
			if (this.duration <= Time.fixedDeltaTime * 2f)
			{
				this.forceFire = true;
			}
			base.StartAimMode(2f, false);
			Util.PlaySound(this.beginStateSoundString, base.gameObject);
			this.animator = base.GetModelAnimator();
			if (base.isAuthority)
			{
				this.isCritAuthority = base.RollCrit();
				this.hitBoxGroup = base.FindHitBoxGroup(this.GetHitBoxGroupName());
				if (this.hitBoxGroup)
				{
					OverlapAttack overlapAttack = new OverlapAttack();
					overlapAttack.attacker = base.gameObject;
					overlapAttack.damage = this.damageCoefficient * this.damageStat;
					overlapAttack.damageColorIndex = DamageColorIndex.Default;
					overlapAttack.damageType = DamageType.Generic;
					overlapAttack.forceVector = this.forceVector;
					overlapAttack.hitBoxGroup = this.hitBoxGroup;
					overlapAttack.hitEffectPrefab = this.hitEffectPrefab;
					NetworkSoundEventDef networkSoundEventDef = this.impactSound;
					overlapAttack.impactSound = ((networkSoundEventDef != null) ? networkSoundEventDef.index : NetworkSoundEventIndex.Invalid);
					overlapAttack.inflictor = base.gameObject;
					overlapAttack.isCrit = this.isCritAuthority;
					overlapAttack.procChainMask = default(ProcChainMask);
					overlapAttack.pushAwayForce = this.pushAwayForce;
					overlapAttack.procCoefficient = this.procCoefficient;
					overlapAttack.teamIndex = base.GetTeam();
					this.overlapAttack = overlapAttack;
				}
			}
			this.PlayAnimation();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C7A6 File Offset: 0x0000A9A6
		protected virtual float CalcDuration()
		{
			if (this.ignoreAttackSpeed)
			{
				return this.baseDuration;
			}
			return this.baseDuration / this.attackSpeedStat;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (string.IsNullOrEmpty(this.mecanimHitboxActiveParameter))
			{
				this.BeginMeleeAttackEffect();
			}
			else if (this.animator.GetFloat(this.mecanimHitboxActiveParameter) > 0.5f)
			{
				this.BeginMeleeAttackEffect();
			}
			if (base.isAuthority)
			{
				this.AuthorityFixedUpdate();
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C818 File Offset: 0x0000AA18
		protected void AuthorityTriggerHitPause()
		{
			if (base.characterMotor)
			{
				this.storedHitPauseVelocity += base.characterMotor.velocity;
				base.characterMotor.velocity = Vector3.zero;
			}
			if (this.animator)
			{
				this.animator.speed = 0f;
			}
			if (this.swingEffectInstance)
			{
				ScaleParticleSystemDuration component = this.swingEffectInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = 20f;
				}
			}
			this.hitPauseTimer = (this.scaleHitPauseDurationAndVelocityWithAttackSpeed ? (this.hitPauseDuration / this.attackSpeedStat) : this.hitPauseDuration);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C8CC File Offset: 0x0000AACC
		protected virtual void BeginMeleeAttackEffect()
		{
			if (this.meleeAttackStartTime != Run.FixedTimeStamp.positiveInfinity)
			{
				return;
			}
			this.meleeAttackStartTime = Run.FixedTimeStamp.now;
			Util.PlaySound(this.beginSwingSoundString, base.gameObject);
			if (this.swingEffectPrefab)
			{
				Transform transform = base.FindModelChild(this.swingEffectMuzzleString);
				if (transform)
				{
					this.swingEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.swingEffectPrefab, transform);
					ScaleParticleSystemDuration component = this.swingEffectInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = component.initialDuration;
					}
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C960 File Offset: 0x0000AB60
		protected virtual void AuthorityExitHitPause()
		{
			this.hitPauseTimer = 0f;
			this.storedHitPauseVelocity.y = Mathf.Max(this.storedHitPauseVelocity.y, this.scaleHitPauseDurationAndVelocityWithAttackSpeed ? (this.shorthopVelocityFromHit / Mathf.Sqrt(this.attackSpeedStat)) : this.shorthopVelocityFromHit);
			if (base.characterMotor)
			{
				base.characterMotor.velocity = this.storedHitPauseVelocity;
			}
			this.storedHitPauseVelocity = Vector3.zero;
			if (this.animator)
			{
				this.animator.speed = 1f;
			}
			if (this.swingEffectInstance)
			{
				ScaleParticleSystemDuration component = this.swingEffectInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = component.initialDuration;
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void PlayAnimation()
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnMeleeHitAuthority()
		{
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000CA28 File Offset: 0x0000AC28
		private void AuthorityFireAttack()
		{
			this.AuthorityModifyOverlapAttack(this.overlapAttack);
			this.hitResults.Clear();
			this.authorityHitThisFixedUpdate = this.overlapAttack.Fire(this.hitResults);
			this.meleeAttackTicks++;
			if (this.authorityHitThisFixedUpdate)
			{
				this.AuthorityTriggerHitPause();
				this.OnMeleeHitAuthority();
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000CA88 File Offset: 0x0000AC88
		protected virtual void AuthorityFixedUpdate()
		{
			if (this.authorityInHitPause)
			{
				this.hitPauseTimer -= Time.fixedDeltaTime;
				if (base.characterMotor)
				{
					base.characterMotor.velocity = Vector3.zero;
				}
				base.fixedAge -= Time.fixedDeltaTime;
				if (!this.authorityInHitPause)
				{
					this.AuthorityExitHitPause();
				}
			}
			else if (this.forceForwardVelocity && base.characterMotor && base.characterDirection)
			{
				Vector3 vector = base.characterDirection.forward * this.forwardVelocityCurve.Evaluate(base.fixedAge / this.duration);
				Vector3 velocity = base.characterMotor.velocity;
				base.characterMotor.AddDisplacement(new Vector3(vector.x, 0f, vector.z));
			}
			this.authorityHitThisFixedUpdate = false;
			if (this.overlapAttack != null && (string.IsNullOrEmpty(this.mecanimHitboxActiveParameter) || this.animator.GetFloat(this.mecanimHitboxActiveParameter) > 0.5f || this.forceFire))
			{
				this.AuthorityFireAttack();
			}
			if (this.duration <= base.fixedAge)
			{
				this.AuthorityOnFinish();
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public override void OnExit()
		{
			if (base.isAuthority)
			{
				if (!this.outer.destroying && !this.authorityHasFiredAtAll && this.allowExitFire)
				{
					this.BeginMeleeAttackEffect();
					this.AuthorityFireAttack();
				}
				if (this.authorityInHitPause)
				{
					this.AuthorityExitHitPause();
				}
			}
			if (this.swingEffectInstance)
			{
				EntityState.Destroy(this.swingEffectInstance);
			}
			if (this.animator)
			{
				this.animator.speed = 1f;
			}
			base.OnExit();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		protected virtual void AuthorityOnFinish()
		{
			this.outer.SetNextStateToMain();
		}

		// Token: 0x0400032A RID: 810
		[SerializeField]
		public float baseDuration;

		// Token: 0x0400032B RID: 811
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x0400032C RID: 812
		[SerializeField]
		public string hitBoxGroupName;

		// Token: 0x0400032D RID: 813
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x0400032E RID: 814
		[SerializeField]
		public float procCoefficient;

		// Token: 0x0400032F RID: 815
		[SerializeField]
		public float pushAwayForce;

		// Token: 0x04000330 RID: 816
		[SerializeField]
		public Vector3 forceVector;

		// Token: 0x04000331 RID: 817
		[SerializeField]
		public float hitPauseDuration;

		// Token: 0x04000332 RID: 818
		[SerializeField]
		public GameObject swingEffectPrefab;

		// Token: 0x04000333 RID: 819
		[SerializeField]
		public string swingEffectMuzzleString;

		// Token: 0x04000334 RID: 820
		[SerializeField]
		public string mecanimHitboxActiveParameter;

		// Token: 0x04000335 RID: 821
		[SerializeField]
		public float shorthopVelocityFromHit;

		// Token: 0x04000336 RID: 822
		[SerializeField]
		public string beginStateSoundString;

		// Token: 0x04000337 RID: 823
		[SerializeField]
		public string beginSwingSoundString;

		// Token: 0x04000338 RID: 824
		[SerializeField]
		public NetworkSoundEventDef impactSound;

		// Token: 0x04000339 RID: 825
		[SerializeField]
		public bool forceForwardVelocity;

		// Token: 0x0400033A RID: 826
		[SerializeField]
		public AnimationCurve forwardVelocityCurve;

		// Token: 0x0400033B RID: 827
		[SerializeField]
		public bool scaleHitPauseDurationAndVelocityWithAttackSpeed;

		// Token: 0x0400033C RID: 828
		[SerializeField]
		public bool ignoreAttackSpeed;

		// Token: 0x0400033D RID: 829
		protected float duration;

		// Token: 0x0400033E RID: 830
		protected HitBoxGroup hitBoxGroup;

		// Token: 0x0400033F RID: 831
		protected Animator animator;

		// Token: 0x04000340 RID: 832
		private OverlapAttack overlapAttack;

		// Token: 0x04000341 RID: 833
		protected bool authorityHitThisFixedUpdate;

		// Token: 0x04000342 RID: 834
		protected float hitPauseTimer;

		// Token: 0x04000343 RID: 835
		protected Vector3 storedHitPauseVelocity;

		// Token: 0x04000344 RID: 836
		private Run.FixedTimeStamp meleeAttackStartTime = Run.FixedTimeStamp.positiveInfinity;

		// Token: 0x04000345 RID: 837
		private GameObject swingEffectInstance;

		// Token: 0x04000346 RID: 838
		private int meleeAttackTicks;

		// Token: 0x04000347 RID: 839
		protected List<HurtBox> hitResults = new List<HurtBox>();

		// Token: 0x04000349 RID: 841
		private bool forceFire;
	}
}
