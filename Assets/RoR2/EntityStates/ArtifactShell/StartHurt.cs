using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.ArtifactShell
{
	// Token: 0x02000490 RID: 1168
	public class StartHurt : ArtifactShellBaseState
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x0005CD95 File Offset: 0x0005AF95
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.healthComponent.combinedHealthFraction >= 1f)
			{
				Util.PlaySound(StartHurt.firstHurtSound, base.gameObject);
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0005CDC0 File Offset: 0x0005AFC0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= StartHurt.baseDuration)
			{
				this.outer.SetNextState(new Hurt());
			}
		}

		// Token: 0x04001ABA RID: 6842
		public static float baseDuration = 0.25f;

		// Token: 0x04001ABB RID: 6843
		public static string firstHurtSound;
	}
}
