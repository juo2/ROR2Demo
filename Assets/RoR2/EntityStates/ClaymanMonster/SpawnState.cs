using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClaymanMonster
{
	// Token: 0x020003F9 RID: 1017
	public class SpawnState : BaseState
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x000515F4 File Offset: 0x0004F7F4
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
			if (SpawnState.spawnSoundString.Length > 0)
			{
				Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			}
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0005167D File Offset: 0x0004F87D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001767 RID: 5991
		public static float duration;

		// Token: 0x04001768 RID: 5992
		public static string spawnSoundString;

		// Token: 0x04001769 RID: 5993
		public static GameObject spawnEffectPrefab;

		// Token: 0x0400176A RID: 5994
		public static string spawnEffectChildString;
	}
}
