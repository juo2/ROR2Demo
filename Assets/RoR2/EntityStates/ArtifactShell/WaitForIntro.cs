using System;
using UnityEngine.Networking;

namespace EntityStates.ArtifactShell
{
	// Token: 0x02000492 RID: 1170
	public class WaitForIntro : ArtifactShellBaseState
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool interactionAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0005CFAB File Offset: 0x0005B1AB
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = WaitForIntro.baseDuration;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005CFBE File Offset: 0x0005B1BE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new WaitForKey());
			}
		}

		// Token: 0x04001AC3 RID: 6851
		public static float baseDuration = 10f;

		// Token: 0x04001AC4 RID: 6852
		private float duration;
	}
}
