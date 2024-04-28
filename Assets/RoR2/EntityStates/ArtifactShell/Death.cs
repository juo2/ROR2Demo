using System;
using RoR2;

namespace EntityStates.ArtifactShell
{
	// Token: 0x02000495 RID: 1173
	public class Death : ArtifactShellBaseState
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool interactionAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0005D23B File Offset: 0x0005B43B
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(base.GetComponent<SfxLocator>().deathSound, base.gameObject);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0005D25A File Offset: 0x0005B45A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > Death.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001AD4 RID: 6868
		public static float duration;
	}
}
