using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace RoR2
{
	// Token: 0x02000786 RID: 1926
	public class ItemDisplay : MonoBehaviour
	{
		// Token: 0x06002855 RID: 10325 RVA: 0x000AF17A File Offset: 0x000AD37A
		public VisibilityLevel GetVisibilityLevel()
		{
			return this.visibilityLevel;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000AF184 File Offset: 0x000AD384
		public void SetVisibilityLevel(VisibilityLevel newVisibilityLevel)
		{
			if (this.visibilityLevel != newVisibilityLevel)
			{
				this.visibilityLevel = newVisibilityLevel;
				switch (this.visibilityLevel)
				{
				case VisibilityLevel.Invisible:
					for (int i = 0; i < this.rendererInfos.Length; i++)
					{
						CharacterModel.RendererInfo rendererInfo = this.rendererInfos[i];
						rendererInfo.renderer.enabled = false;
						rendererInfo.renderer.shadowCastingMode = ShadowCastingMode.Off;
					}
					return;
				case VisibilityLevel.Cloaked:
					for (int j = 0; j < this.rendererInfos.Length; j++)
					{
						CharacterModel.RendererInfo rendererInfo2 = this.rendererInfos[j];
						rendererInfo2.renderer.enabled = (!rendererInfo2.ignoreOverlays && (!this.isDead || !rendererInfo2.hideOnDeath));
						rendererInfo2.renderer.shadowCastingMode = ShadowCastingMode.Off;
						rendererInfo2.renderer.material = CharacterModel.cloakedMaterial;
					}
					return;
				case VisibilityLevel.Revealed:
					for (int k = 0; k < this.rendererInfos.Length; k++)
					{
						CharacterModel.RendererInfo rendererInfo3 = this.rendererInfos[k];
						rendererInfo3.renderer.enabled = (!rendererInfo3.ignoreOverlays && (!this.isDead || !rendererInfo3.hideOnDeath));
						rendererInfo3.renderer.shadowCastingMode = ShadowCastingMode.Off;
						rendererInfo3.renderer.material = CharacterModel.revealedMaterial;
					}
					return;
				case VisibilityLevel.Visible:
					for (int l = 0; l < this.rendererInfos.Length; l++)
					{
						CharacterModel.RendererInfo rendererInfo4 = this.rendererInfos[l];
						bool flag = !this.isDead || !rendererInfo4.hideOnDeath;
						rendererInfo4.renderer.enabled = flag;
						rendererInfo4.renderer.shadowCastingMode = (flag ? ShadowCastingMode.Off : ShadowCastingMode.On);
						rendererInfo4.renderer.material = rendererInfo4.defaultMaterial;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000AF344 File Offset: 0x000AD544
		public void OnDeath()
		{
			this.isDead = true;
			for (int i = 0; i < this.rendererInfos.Length; i++)
			{
				CharacterModel.RendererInfo rendererInfo = this.rendererInfos[i];
				if (rendererInfo.hideOnDeath)
				{
					rendererInfo.renderer.enabled = false;
					rendererInfo.renderer.shadowCastingMode = ShadowCastingMode.Off;
				}
			}
			GameObject[] array = this.hideOnDeathObjects;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(false);
			}
		}

		// Token: 0x04002C03 RID: 11267
		public CharacterModel.RendererInfo[] rendererInfos;

		// Token: 0x04002C04 RID: 11268
		public GameObject[] hideOnDeathObjects;

		// Token: 0x04002C05 RID: 11269
		private VisibilityLevel visibilityLevel = VisibilityLevel.Visible;

		// Token: 0x04002C06 RID: 11270
		private bool isDead;
	}
}
