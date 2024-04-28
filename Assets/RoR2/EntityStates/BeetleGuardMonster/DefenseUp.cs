using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BeetleGuardMonster
{
	// Token: 0x0200046B RID: 1131
	public class DefenseUp : BaseState
	{
		// Token: 0x06001439 RID: 5177 RVA: 0x0005A448 File Offset: 0x00058648
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = DefenseUp.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayCrossfade("Body", "DefenseUp", "DefenseUp.playbackRate", this.duration, 0.2f);
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0005A4A8 File Offset: 0x000586A8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("DefenseUp.activate") > 0.5f && !this.hasCastBuff)
			{
				ScaleParticleSystemDuration component = UnityEngine.Object.Instantiate<GameObject>(DefenseUp.defenseUpPrefab, base.transform.position, Quaternion.identity, base.transform).GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = DefenseUp.buffDuration;
				}
				this.hasCastBuff = true;
				if (NetworkServer.active)
				{
					base.characterBody.AddTimedBuff(JunkContent.Buffs.EnrageAncientWisp, DefenseUp.buffDuration);
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040019FD RID: 6653
		public static float baseDuration = 3.5f;

		// Token: 0x040019FE RID: 6654
		public static float buffDuration = 8f;

		// Token: 0x040019FF RID: 6655
		public static GameObject defenseUpPrefab;

		// Token: 0x04001A00 RID: 6656
		private Animator modelAnimator;

		// Token: 0x04001A01 RID: 6657
		private float duration;

		// Token: 0x04001A02 RID: 6658
		private bool hasCastBuff;
	}
}
