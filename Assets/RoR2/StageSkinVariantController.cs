using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008A8 RID: 2216
	public class StageSkinVariantController : MonoBehaviour
	{
		// Token: 0x0600313D RID: 12605 RVA: 0x000D0EC0 File Offset: 0x000CF0C0
		private void Awake()
		{
			if (SceneInfo.instance)
			{
				for (int i = 0; i < this.stageSkinVariants.Length; i++)
				{
					StageSkinVariantController.StageSkinVariant stageSkinVariant = this.stageSkinVariants[i];
					for (int j = 0; j < stageSkinVariant.childObjects.Length; j++)
					{
						stageSkinVariant.childObjects[j].SetActive(false);
					}
				}
				int k = 0;
				while (k < this.stageSkinVariants.Length)
				{
					StageSkinVariantController.StageSkinVariant stageSkinVariant2 = this.stageSkinVariants[k];
					if (SceneInfo.instance.sceneDef.nameToken == stageSkinVariant2.stageNameToken)
					{
						for (int l = 0; l < stageSkinVariant2.childObjects.Length; l++)
						{
							stageSkinVariant2.childObjects[l].SetActive(true);
						}
						if (stageSkinVariant2.replacementRenderInfos.Length != 0)
						{
							this.characterModel.baseRendererInfos = stageSkinVariant2.replacementRenderInfos;
							return;
						}
						break;
					}
					else
					{
						k++;
					}
				}
			}
		}

		// Token: 0x040032BB RID: 12987
		public StageSkinVariantController.StageSkinVariant[] stageSkinVariants;

		// Token: 0x040032BC RID: 12988
		public CharacterModel characterModel;

		// Token: 0x020008A9 RID: 2217
		[Serializable]
		public struct StageSkinVariant
		{
			// Token: 0x040032BD RID: 12989
			public string stageNameToken;

			// Token: 0x040032BE RID: 12990
			public CharacterModel.RendererInfo[] replacementRenderInfos;

			// Token: 0x040032BF RID: 12991
			public GameObject[] childObjects;
		}
	}
}
