using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008A5 RID: 2213
	public class SprintEffectController : MonoBehaviour
	{
		// Token: 0x06003105 RID: 12549 RVA: 0x000D02EB File Offset: 0x000CE4EB
		private void Awake()
		{
			if (this.characterBody && Util.IsPrefab(this.characterBody.gameObject) && !Util.IsPrefab(base.gameObject))
			{
				this.characterBody = null;
			}
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000D0320 File Offset: 0x000CE520
		private void FixedUpdate()
		{
			bool flag = true;
			if (this.characterModel)
			{
				flag = (this.characterModel.visibility > VisibilityLevel.Cloaked);
			}
			if (this.characterBody)
			{
				if (this.characterBody.characterMotor && this.characterBody.healthComponent.alive && this.characterBody.isSprinting && (!this.mustBeGrounded || this.characterBody.characterMotor.isGrounded) && (!this.mustBeVisible || flag))
				{
					if (this.loopRootObject)
					{
						this.loopRootObject.SetActive(true);
					}
					for (int i = 0; i < this.loopSystems.Length; i++)
					{
						ParticleSystem particleSystem = this.loopSystems[i];
						particleSystem.main.loop = true;
						if (!particleSystem.isPlaying)
						{
							particleSystem.Play();
						}
					}
					return;
				}
				if (this.loopRootObject)
				{
					this.loopRootObject.SetActive(false);
				}
				for (int j = 0; j < this.loopSystems.Length; j++)
				{
					this.loopSystems[j].main.loop = false;
				}
			}
		}

		// Token: 0x040032A1 RID: 12961
		public ParticleSystem[] loopSystems;

		// Token: 0x040032A2 RID: 12962
		public GameObject loopRootObject;

		// Token: 0x040032A3 RID: 12963
		public bool mustBeGrounded;

		// Token: 0x040032A4 RID: 12964
		public bool mustBeVisible;

		// Token: 0x040032A5 RID: 12965
		public CharacterBody characterBody;

		// Token: 0x040032A6 RID: 12966
		public CharacterModel characterModel;
	}
}
