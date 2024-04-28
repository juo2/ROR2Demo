using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GravekeeperBoss
{
	// Token: 0x0200034A RID: 842
	public class PrepHook : BaseState
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x000411C8 File Offset: 0x0003F3C8
		public override void OnEnter()
		{
			base.OnEnter();
			base.fixedAge = 0f;
			this.duration = PrepHook.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayCrossfade("Body", "PrepHook", "PrepHook.playbackRate", this.duration, 0.5f);
				this.modelAnimator.GetComponent<AimAnimator>().enabled = true;
			}
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.inputBank.aimDirection;
			}
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(PrepHook.muzzleString);
					if (transform && PrepHook.chargeEffectPrefab)
					{
						this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(PrepHook.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeInstance.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.duration;
						}
					}
				}
			}
			Util.PlayAttackSpeedSound(PrepHook.attackString, base.gameObject, this.attackSpeedStat);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00041307 File Offset: 0x0003F507
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireHook());
				return;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00041336 File Offset: 0x0003F536
		public override void OnExit()
		{
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040012E1 RID: 4833
		public static float baseDuration = 3f;

		// Token: 0x040012E2 RID: 4834
		public static GameObject chargeEffectPrefab;

		// Token: 0x040012E3 RID: 4835
		public static string muzzleString;

		// Token: 0x040012E4 RID: 4836
		public static string attackString;

		// Token: 0x040012E5 RID: 4837
		private float duration;

		// Token: 0x040012E6 RID: 4838
		private GameObject chargeInstance;

		// Token: 0x040012E7 RID: 4839
		private Animator modelAnimator;
	}
}
