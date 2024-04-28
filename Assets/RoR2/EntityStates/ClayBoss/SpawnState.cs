using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayBoss
{
	// Token: 0x0200040B RID: 1035
	public class SpawnState : BaseState
	{
		// Token: 0x0600129C RID: 4764 RVA: 0x000533E4 File Offset: 0x000515E4
		public override void OnEnter()
		{
			base.OnEnter();
			ChildLocator component = base.GetModelTransform().GetComponent<ChildLocator>();
			if (component)
			{
				Transform transform = component.FindChild(SpawnState.spawnEffectChildString);
				if (transform)
				{
					UnityEngine.Object.Instantiate<GameObject>(SpawnState.spawnEffectPrefab, transform.position, Quaternion.identity);
				}
			}
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00053460 File Offset: 0x00051660
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001804 RID: 6148
		public static float duration;

		// Token: 0x04001805 RID: 6149
		public static string spawnSoundString;

		// Token: 0x04001806 RID: 6150
		public static GameObject spawnEffectPrefab;

		// Token: 0x04001807 RID: 6151
		public static string spawnEffectChildString;
	}
}
