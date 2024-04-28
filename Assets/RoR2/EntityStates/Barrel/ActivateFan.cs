using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Barrel
{
	// Token: 0x02000470 RID: 1136
	public class ActivateFan : EntityState
	{
		// Token: 0x06001454 RID: 5204 RVA: 0x0005ABD4 File Offset: 0x00058DD4
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "IdleToActive");
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					component.FindChild("JumpVolume").gameObject.SetActive(true);
					component.FindChild("LightBack").gameObject.SetActive(true);
					component.FindChild("LightFront").gameObject.SetActive(true);
				}
			}
			if (base.sfxLocator)
			{
				Util.PlaySound(base.sfxLocator.openSound, base.gameObject);
			}
		}
	}
}
