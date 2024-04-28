using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x0200010A RID: 266
	public class SwingMeleeBase : BasicMeleeAttack
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00014341 File Offset: 0x00012541
		protected override bool allowExitFire
		{
			get
			{
				return base.characterBody && !base.characterBody.isSprinting;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00014360 File Offset: 0x00012560
		public override void OnEnter()
		{
			base.OnEnter();
			base.characterDirection.forward = base.GetAimRay().direction;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001438C File Offset: 0x0001258C
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00014394 File Offset: 0x00012594
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001439D File Offset: 0x0001259D
		protected override void PlayAnimation()
		{
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParameter, this.duration);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000143BD File Offset: 0x000125BD
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			base.characterBody.AddSpreadBloom(this.bloom);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000143D6 File Offset: 0x000125D6
		protected override void BeginMeleeAttackEffect()
		{
			base.AddRecoil(-0.1f * this.recoilAmplitude, 0.1f * this.recoilAmplitude, -1f * this.recoilAmplitude, 1f * this.recoilAmplitude);
			base.BeginMeleeAttackEffect();
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000556 RID: 1366
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x04000557 RID: 1367
		[SerializeField]
		public float bloom;

		// Token: 0x04000558 RID: 1368
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000559 RID: 1369
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400055A RID: 1370
		[SerializeField]
		public string animationPlaybackRateParameter;
	}
}
