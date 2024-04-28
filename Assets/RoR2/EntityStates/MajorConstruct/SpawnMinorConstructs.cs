using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MajorConstruct
{
	// Token: 0x0200028C RID: 652
	public class SpawnMinorConstructs : BaseState
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00030168 File Offset: 0x0002E368
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

		// Token: 0x06000B85 RID: 2949 RVA: 0x00030203 File Offset: 0x0002E403
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000D9C RID: 3484
		[SerializeField]
		public float duration;

		// Token: 0x04000D9D RID: 3485
		[SerializeField]
		public int numToSpawn;

		// Token: 0x04000D9E RID: 3486
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000D9F RID: 3487
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000DA0 RID: 3488
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000DA1 RID: 3489
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000DA2 RID: 3490
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000DA3 RID: 3491
		[SerializeField]
		public string enterSoundString;
	}
}
