using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000C3 RID: 195
	public abstract class GenericCharacterSpawnState : BaseState
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn1", "Spawn1.playbackRate", this.duration);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000EBA9 File Offset: 0x0000CDA9
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000396 RID: 918
		[SerializeField]
		public float duration = 2f;

		// Token: 0x04000397 RID: 919
		[SerializeField]
		public string spawnSoundString;
	}
}
