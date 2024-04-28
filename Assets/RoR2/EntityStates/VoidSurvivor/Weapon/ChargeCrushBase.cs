using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F8 RID: 248
	public class ChargeCrushBase : BaseSkillState
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x000129F0 File Offset: 0x00010BF0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.soundID = Util.PlayAttackSpeedSound(this.chargeSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackParameterName, this.duration);
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

		// Token: 0x06000474 RID: 1140 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public override void OnExit()
		{
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			AkSoundEngine.StopPlayingID(this.soundID);
			base.OnExit();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040004BA RID: 1210
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040004BB RID: 1211
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x040004BC RID: 1212
		[SerializeField]
		public string muzzle;

		// Token: 0x040004BD RID: 1213
		[SerializeField]
		public string chargeSoundString;

		// Token: 0x040004BE RID: 1214
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040004BF RID: 1215
		[SerializeField]
		public string animationStateName;

		// Token: 0x040004C0 RID: 1216
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x040004C1 RID: 1217
		protected float duration;

		// Token: 0x040004C2 RID: 1218
		private uint soundID;

		// Token: 0x040004C3 RID: 1219
		private GameObject chargeEffectInstance;
	}
}
