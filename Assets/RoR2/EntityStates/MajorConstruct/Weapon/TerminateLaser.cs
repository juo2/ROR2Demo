using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MajorConstruct.Weapon
{
	// Token: 0x02000294 RID: 660
	public class TerminateLaser : BaseState
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0000C072 File Offset: 0x0000A272
		public TerminateLaser()
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00030649 File Offset: 0x0002E849
		public TerminateLaser(Vector3 blastPosition)
		{
			this.blastPosition = blastPosition;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00030658 File Offset: 0x0002E858
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackParameterName, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (base.isAuthority)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * this.blastDamageCoefficient,
					baseForce = this.blastForceMagnitude,
					position = this.blastPosition,
					radius = this.blastRadius,
					bonusForce = this.blastBonusForce
				}.Fire();
				EffectData effectData = new EffectData
				{
					origin = this.blastPosition,
					scale = this.blastRadius
				};
				EffectManager.SpawnEffect(this.explosionEffectPrefab, effectData, true);
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003076D File Offset: 0x0002E96D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000DC6 RID: 3526
		[SerializeField]
		public float duration;

		// Token: 0x04000DC7 RID: 3527
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000DC8 RID: 3528
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000DC9 RID: 3529
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x04000DCA RID: 3530
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000DCB RID: 3531
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000DCC RID: 3532
		[SerializeField]
		public GameObject explosionEffectPrefab;

		// Token: 0x04000DCD RID: 3533
		[SerializeField]
		public float blastDamageCoefficient;

		// Token: 0x04000DCE RID: 3534
		[SerializeField]
		public float blastForceMagnitude;

		// Token: 0x04000DCF RID: 3535
		[SerializeField]
		public float blastRadius;

		// Token: 0x04000DD0 RID: 3536
		[SerializeField]
		public Vector3 blastBonusForce;

		// Token: 0x04000DD1 RID: 3537
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000DD2 RID: 3538
		private Vector3 blastPosition;
	}
}
