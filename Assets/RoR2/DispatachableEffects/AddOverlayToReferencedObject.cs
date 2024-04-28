using System;
using UnityEngine;

namespace RoR2.DispatachableEffects
{
	// Token: 0x02000BF6 RID: 3062
	[RequireComponent(typeof(TemporaryOverlay))]
	[RequireComponent(typeof(EffectComponent))]
	public class AddOverlayToReferencedObject : MonoBehaviour
	{
		// Token: 0x06004584 RID: 17796 RVA: 0x0012146C File Offset: 0x0011F66C
		protected void Start()
		{
			EffectComponent component = base.GetComponent<EffectComponent>();
			this.ApplyOverlay(component.GetReferencedObject(), component.effectData.genericFloat);
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x00121498 File Offset: 0x0011F698
		protected void ApplyOverlay(GameObject targetObject, float duration)
		{
			if (!targetObject)
			{
				return;
			}
			ModelLocator component = targetObject.GetComponent<ModelLocator>();
			if (!component)
			{
				return;
			}
			Transform modelTransform = component.modelTransform;
			if (!modelTransform)
			{
				return;
			}
			CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
			if (!component2)
			{
				return;
			}
			TemporaryOverlay component3 = base.GetComponent<TemporaryOverlay>();
			component3.AddToCharacerModel(component2);
			if (this.effectDataGenericFloatOverridesDuration)
			{
				component3.duration = duration;
			}
		}

		// Token: 0x040043B6 RID: 17334
		public bool effectDataGenericFloatOverridesDuration = true;
	}
}
