using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Commando
{
	// Token: 0x020003E2 RID: 994
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x0004E6C4 File Offset: 0x0004C8C4
		public override void OnEnter()
		{
			base.OnEnter();
			Vector3 vector = Vector3.up * 3f;
			if (base.characterMotor)
			{
				vector += base.characterMotor.velocity;
				base.characterMotor.enabled = false;
			}
			if (base.cachedModelTransform)
			{
				RagdollController component = base.cachedModelTransform.GetComponent<RagdollController>();
				if (component)
				{
					component.BeginRagdoll(vector);
				}
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004E73A File Offset: 0x0004C93A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge > 4f)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001693 RID: 5779
		private Vector3 previousPosition;

		// Token: 0x04001694 RID: 5780
		private float upSpeedVelocity;

		// Token: 0x04001695 RID: 5781
		private float upSpeed;

		// Token: 0x04001696 RID: 5782
		private Animator modelAnimator;
	}
}
