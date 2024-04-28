using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidMegaCrab
{
	// Token: 0x02000148 RID: 328
	public class SpawnState : BaseState
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0001887C File Offset: 0x00016A7C
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.spawnSoundString, base.gameObject);
			if (this.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.spawnEffectPrefab, base.gameObject, this.spawnMuzzleName, false);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				modelTransform.GetComponent<PrintController>().enabled = true;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000188FF File Offset: 0x00016AFF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040006C7 RID: 1735
		[SerializeField]
		public float duration = 4f;

		// Token: 0x040006C8 RID: 1736
		[SerializeField]
		public string spawnSoundString;

		// Token: 0x040006C9 RID: 1737
		[SerializeField]
		public GameObject spawnEffectPrefab;

		// Token: 0x040006CA RID: 1738
		[SerializeField]
		public string spawnMuzzleName;

		// Token: 0x040006CB RID: 1739
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040006CC RID: 1740
		[SerializeField]
		public string animationStateName;

		// Token: 0x040006CD RID: 1741
		[SerializeField]
		public string animationPlaybackRateParam;
	}
}
