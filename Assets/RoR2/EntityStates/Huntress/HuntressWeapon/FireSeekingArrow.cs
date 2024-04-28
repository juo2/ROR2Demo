using System;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x02000322 RID: 802
	public class FireSeekingArrow : BaseState
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x0003DDD0 File Offset: 0x0003BFD0
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			this.huntressTracker = base.GetComponent<HuntressTracker>();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				this.animator = modelTransform.GetComponent<Animator>();
			}
			Util.PlayAttackSpeedSound(this.attackSoundString, base.gameObject, this.attackSpeedStat);
			if (this.huntressTracker && base.isAuthority)
			{
				this.initialOrbTarget = this.huntressTracker.GetTrackingTarget();
			}
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.arrowReloadDuration = this.baseArrowReloadDuration / this.attackSpeedStat;
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration + 1f);
			}
			base.PlayCrossfade("Gesture, Override", "FireSeekingShot", "FireSeekingShot.playbackRate", this.duration, this.duration * 0.2f / this.attackSpeedStat);
			base.PlayCrossfade("Gesture, Additive", "FireSeekingShot", "FireSeekingShot.playbackRate", this.duration, this.duration * 0.2f / this.attackSpeedStat);
			this.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003DF1B File Offset: 0x0003C11B
		public override void OnExit()
		{
			base.OnExit();
			this.FireOrbArrow();
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003DF29 File Offset: 0x0003C129
		protected virtual GenericDamageOrb CreateArrowOrb()
		{
			return new HuntressArrowOrb();
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003DF30 File Offset: 0x0003C130
		private void FireOrbArrow()
		{
			if (this.firedArrowCount >= this.maxArrowCount || this.arrowReloadTimer > 0f || !NetworkServer.active)
			{
				return;
			}
			this.firedArrowCount++;
			this.arrowReloadTimer = this.arrowReloadDuration;
			GenericDamageOrb genericDamageOrb = this.CreateArrowOrb();
			genericDamageOrb.damageValue = base.characterBody.damage * this.orbDamageCoefficient;
			genericDamageOrb.isCrit = this.isCrit;
			genericDamageOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
			genericDamageOrb.attacker = base.gameObject;
			genericDamageOrb.procCoefficient = this.orbProcCoefficient;
			HurtBox hurtBox = this.initialOrbTarget;
			if (hurtBox)
			{
				Transform transform = this.childLocator.FindChild(this.muzzleString);
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzleString, true);
				genericDamageOrb.origin = transform.position;
				genericDamageOrb.target = hurtBox;
				OrbManager.instance.AddOrb(genericDamageOrb);
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003E028 File Offset: 0x0003C228
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.arrowReloadTimer -= Time.fixedDeltaTime;
			if (this.animator.GetFloat("FireSeekingShot.fire") > 0f)
			{
				this.FireOrbArrow();
			}
			if (base.fixedAge > this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003E08C File Offset: 0x0003C28C
		public override void OnSerialize(NetworkWriter writer)
		{
			writer.Write(HurtBoxReference.FromHurtBox(this.initialOrbTarget));
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		public override void OnDeserialize(NetworkReader reader)
		{
			this.initialOrbTarget = reader.ReadHurtBoxReference().ResolveHurtBox();
		}

		// Token: 0x040011F1 RID: 4593
		[SerializeField]
		public float orbDamageCoefficient;

		// Token: 0x040011F2 RID: 4594
		[SerializeField]
		public float orbProcCoefficient;

		// Token: 0x040011F3 RID: 4595
		[SerializeField]
		public string muzzleString;

		// Token: 0x040011F4 RID: 4596
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x040011F5 RID: 4597
		[SerializeField]
		public string attackSoundString;

		// Token: 0x040011F6 RID: 4598
		[SerializeField]
		public float baseDuration;

		// Token: 0x040011F7 RID: 4599
		[SerializeField]
		public int maxArrowCount;

		// Token: 0x040011F8 RID: 4600
		[SerializeField]
		public float baseArrowReloadDuration;

		// Token: 0x040011F9 RID: 4601
		private float duration;

		// Token: 0x040011FA RID: 4602
		protected float arrowReloadDuration;

		// Token: 0x040011FB RID: 4603
		private float arrowReloadTimer;

		// Token: 0x040011FC RID: 4604
		protected bool isCrit;

		// Token: 0x040011FD RID: 4605
		private int firedArrowCount;

		// Token: 0x040011FE RID: 4606
		private HurtBox initialOrbTarget;

		// Token: 0x040011FF RID: 4607
		private ChildLocator childLocator;

		// Token: 0x04001200 RID: 4608
		private HuntressTracker huntressTracker;

		// Token: 0x04001201 RID: 4609
		private Animator animator;
	}
}
