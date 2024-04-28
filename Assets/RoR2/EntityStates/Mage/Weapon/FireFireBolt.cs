using System;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x0200029F RID: 671
	public class FireFireBolt : BaseState, SteppedSkillDef.IStepSetter
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x000313C0 File Offset: 0x0002F5C0
		public void SetStep(int i)
		{
			this.gauntlet = (FireFireBolt.Gauntlet)i;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000313CC File Offset: 0x0002F5CC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(this.attackSoundString, base.gameObject, this.attackSoundPitch);
			base.characterBody.SetAimTimer(2f);
			this.animator = base.GetModelAnimator();
			if (this.animator)
			{
				this.childLocator = this.animator.GetComponent<ChildLocator>();
			}
			FireFireBolt.Gauntlet gauntlet = this.gauntlet;
			if (gauntlet != FireFireBolt.Gauntlet.Left)
			{
				if (gauntlet != FireFireBolt.Gauntlet.Right)
				{
					return;
				}
				this.muzzleString = "MuzzleRight";
				if (this.attackSpeedStat < FireFireBolt.attackSpeedAltAnimationThreshold)
				{
					base.PlayCrossfade("Gesture, Additive", "Cast1Right", "FireGauntlet.playbackRate", this.duration, 0.1f);
					this.PlayAnimation("Gesture Left, Additive", "Empty");
					this.PlayAnimation("Gesture Right, Additive", "Empty");
					return;
				}
				base.PlayAnimation("Gesture Right, Additive", "FireGauntletRight", "FireGauntlet.playbackRate", this.duration);
				base.PlayAnimation("Gesture, Additive", "HoldGauntletsUp", "FireGauntlet.playbackRate", this.duration);
				this.FireGauntlet();
				return;
			}
			else
			{
				this.muzzleString = "MuzzleLeft";
				if (this.attackSpeedStat < FireFireBolt.attackSpeedAltAnimationThreshold)
				{
					base.PlayCrossfade("Gesture, Additive", "Cast1Left", "FireGauntlet.playbackRate", this.duration, 0.1f);
					this.PlayAnimation("Gesture Left, Additive", "Empty");
					this.PlayAnimation("Gesture Right, Additive", "Empty");
					return;
				}
				base.PlayAnimation("Gesture Left, Additive", "FireGauntletLeft", "FireGauntlet.playbackRate", this.duration);
				base.PlayAnimation("Gesture, Additive", "HoldGauntletsUp", "FireGauntlet.playbackRate", this.duration);
				this.FireGauntlet();
				return;
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00031584 File Offset: 0x0002F784
		private void FireGauntlet()
		{
			if (this.hasFiredGauntlet)
			{
				return;
			}
			base.characterBody.AddSpreadBloom(FireFireBolt.bloom);
			this.hasFiredGauntlet = true;
			Ray aimRay = base.GetAimRay();
			if (this.childLocator)
			{
				this.muzzleTransform = this.childLocator.FindChild(this.muzzleString);
			}
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzleString, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageCoefficient * this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00031664 File Offset: 0x0002F864
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator.GetFloat("FireGauntlet.fire") > 0f && !this.hasFiredGauntlet)
			{
				this.FireGauntlet();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000316BD File Offset: 0x0002F8BD
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.gauntlet);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.gauntlet = (FireFireBolt.Gauntlet)reader.ReadByte();
		}

		// Token: 0x04000E0D RID: 3597
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000E0E RID: 3598
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x04000E0F RID: 3599
		[SerializeField]
		public float procCoefficient;

		// Token: 0x04000E10 RID: 3600
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04000E11 RID: 3601
		[SerializeField]
		public float force = 20f;

		// Token: 0x04000E12 RID: 3602
		public static float attackSpeedAltAnimationThreshold;

		// Token: 0x04000E13 RID: 3603
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000E14 RID: 3604
		[SerializeField]
		public string attackSoundString;

		// Token: 0x04000E15 RID: 3605
		[SerializeField]
		public float attackSoundPitch;

		// Token: 0x04000E16 RID: 3606
		public static float bloom;

		// Token: 0x04000E17 RID: 3607
		private float duration;

		// Token: 0x04000E18 RID: 3608
		private bool hasFiredGauntlet;

		// Token: 0x04000E19 RID: 3609
		private string muzzleString;

		// Token: 0x04000E1A RID: 3610
		private Transform muzzleTransform;

		// Token: 0x04000E1B RID: 3611
		private Animator animator;

		// Token: 0x04000E1C RID: 3612
		private ChildLocator childLocator;

		// Token: 0x04000E1D RID: 3613
		private FireFireBolt.Gauntlet gauntlet;

		// Token: 0x020002A0 RID: 672
		public enum Gauntlet
		{
			// Token: 0x04000E1F RID: 3615
			Left,
			// Token: 0x04000E20 RID: 3616
			Right
		}
	}
}
