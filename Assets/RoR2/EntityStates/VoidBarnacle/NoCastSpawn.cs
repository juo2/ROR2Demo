using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidBarnacle
{
	// Token: 0x02000162 RID: 354
	public class NoCastSpawn : BaseState
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0001AC68 File Offset: 0x00018E68
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.spawnFXPrefab != null)
			{
				EffectManager.SimpleMuzzleFlash(this.spawnFXPrefab, base.gameObject, this.spawnFXTransformName, false);
			}
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateName, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001ACD1 File Offset: 0x00018ED1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x04000793 RID: 1939
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000794 RID: 1940
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000795 RID: 1941
		[SerializeField]
		public string animationPlaybackRateName;

		// Token: 0x04000796 RID: 1942
		[SerializeField]
		public float duration;

		// Token: 0x04000797 RID: 1943
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000798 RID: 1944
		[SerializeField]
		public GameObject spawnFXPrefab;

		// Token: 0x04000799 RID: 1945
		[SerializeField]
		public string spawnFXTransformName;
	}
}
