using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MegaConstruct
{
	// Token: 0x02000289 RID: 649
	public class SpawnMinorConstructs : BaseState
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0002FD64 File Offset: 0x0002DF64
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			MasterSpawnSlotController component = base.GetComponent<MasterSpawnSlotController>();
			if (NetworkServer.active && component)
			{
				component.SpawnRandomOpen(this.numToSpawn, Run.instance.stageRng, base.gameObject, null);
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002FDFF File Offset: 0x0002DFFF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000D7C RID: 3452
		[SerializeField]
		public float duration;

		// Token: 0x04000D7D RID: 3453
		[SerializeField]
		public int numToSpawn;

		// Token: 0x04000D7E RID: 3454
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000D7F RID: 3455
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000D80 RID: 3456
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000D81 RID: 3457
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000D82 RID: 3458
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000D83 RID: 3459
		[SerializeField]
		public string enterSoundString;
	}
}
