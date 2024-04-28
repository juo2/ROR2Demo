using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VagrantNovaItem
{
	// Token: 0x02000167 RID: 359
	public class BaseVagrantNovaItemState : BaseBodyAttachmentState
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0001AFB1 File Offset: 0x000191B1
		protected int GetItemStack()
		{
			if (!base.attachedBody || !base.attachedBody.inventory)
			{
				return 1;
			}
			return base.attachedBody.inventory.GetItemCount(RoR2Content.Items.NovaOnLowHealth);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001AFEC File Offset: 0x000191EC
		public override void OnEnter()
		{
			base.OnEnter();
			ChildLocator component = base.GetComponent<ChildLocator>();
			if (component)
			{
				Transform transform = component.FindChild("ChargeSparks");
				this.chargeSparks = ((transform != null) ? transform.GetComponent<ParticleSystem>() : null);
				if (this.chargeSparks)
				{
					ParticleSystem.ShapeModule shape = this.chargeSparks.shape;
					SkinnedMeshRenderer skinnedMeshRenderer = this.FindAttachedBodyMainRenderer();
					if (skinnedMeshRenderer)
					{
						shape.skinnedMeshRenderer = this.FindAttachedBodyMainRenderer();
						ParticleSystem.MainModule main = this.chargeSparks.main;
						float x = skinnedMeshRenderer.transform.lossyScale.x;
						main.startSize = 0.5f / x;
					}
				}
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001B098 File Offset: 0x00019298
		protected void SetChargeSparkEmissionRateMultiplier(float multiplier)
		{
			if (this.chargeSparks)
			{
				this.chargeSparks.emission.rateOverTimeMultiplier = multiplier * 20f;
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001B0CC File Offset: 0x000192CC
		private SkinnedMeshRenderer FindAttachedBodyMainRenderer()
		{
			if (!base.attachedBody)
			{
				return null;
			}
			ModelLocator modelLocator = base.attachedBody.modelLocator;
			CharacterModel.RendererInfo[] array;
			if (modelLocator == null)
			{
				array = null;
			}
			else
			{
				CharacterModel component = modelLocator.modelTransform.GetComponent<CharacterModel>();
				array = ((component != null) ? component.baseRendererInfos : null);
			}
			CharacterModel.RendererInfo[] array2 = array;
			if (array2 == null)
			{
				return null;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				SkinnedMeshRenderer result;
				if ((result = (array2[i].renderer as SkinnedMeshRenderer)) != null)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x040007AB RID: 1963
		protected ParticleSystem chargeSparks;
	}
}
