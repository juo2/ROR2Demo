using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008A0 RID: 2208
	public class SnailAnimator : MonoBehaviour
	{
		// Token: 0x060030D2 RID: 12498 RVA: 0x000CF96F File Offset: 0x000CDB6F
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
			this.characterModel = base.GetComponentInParent<CharacterModel>();
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000CF98C File Offset: 0x000CDB8C
		private void FixedUpdate()
		{
			if (this.characterModel)
			{
				CharacterBody body = this.characterModel.body;
				if (body)
				{
					bool outOfDanger = body.outOfDanger;
					if (outOfDanger && !this.lastOutOfDanger)
					{
						this.animator.SetBool("spawn", true);
						this.animator.SetBool("hide", false);
						Util.PlaySound("Play_item_proc_slug_emerge", this.characterModel.gameObject);
						this.healEffectSystem.main.loop = true;
						this.healEffectSystem.Play();
					}
					else if (!outOfDanger && this.lastOutOfDanger)
					{
						this.animator.SetBool("hide", true);
						this.animator.SetBool("spawn", false);
						Util.PlaySound("Play_item_proc_slug_hide", this.characterModel.gameObject);
						this.healEffectSystem.main.loop = false;
					}
					this.lastOutOfDanger = outOfDanger;
				}
			}
		}

		// Token: 0x04003283 RID: 12931
		public ParticleSystem healEffectSystem;

		// Token: 0x04003284 RID: 12932
		private bool lastOutOfDanger;

		// Token: 0x04003285 RID: 12933
		private Animator animator;

		// Token: 0x04003286 RID: 12934
		private CharacterModel characterModel;
	}
}
