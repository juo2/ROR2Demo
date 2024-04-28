using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000EF RID: 239
	public class ChargeMegaBlaster : BaseSkillState
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.soundID = Util.PlayAttackSpeedSound(this.chargeSoundString, base.gameObject, this.attackSpeedStat);
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			base.characterBody.SetAimTimer(this.duration + 1f);
			Transform transform = base.FindModelChild(this.muzzle);
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

		// Token: 0x0600044B RID: 1099 RVA: 0x00011C9C File Offset: 0x0000FE9C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextState(new ReadyMegaBlaster
					{
						activatorSkillSlot = base.activatorSkillSlot
					});
					return;
				}
				if (!base.IsKeyDownAuthority() && base.fixedAge > this.minimumDuration)
				{
					this.outer.SetNextState(new FireMegaBlasterSmall());
				}
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00011D08 File Offset: 0x0000FF08
		public override void OnExit()
		{
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			AkSoundEngine.StopPlayingID(this.soundID);
			base.OnExit();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000457 RID: 1111
		[SerializeField]
		public float minimumDuration = 0.1f;

		// Token: 0x04000458 RID: 1112
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x04000459 RID: 1113
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400045A RID: 1114
		[SerializeField]
		public string muzzle;

		// Token: 0x0400045B RID: 1115
		[SerializeField]
		public string chargeSoundString;

		// Token: 0x0400045C RID: 1116
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400045D RID: 1117
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400045E RID: 1118
		private float duration;

		// Token: 0x0400045F RID: 1119
		private uint soundID;

		// Token: 0x04000460 RID: 1120
		private GameObject chargeEffectInstance;
	}
}
