using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.ParentEgg
{
	// Token: 0x02000227 RID: 551
	public class PreHatch : BaseEggState
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x00027D89 File Offset: 0x00025F89
		public override void OnEnter()
		{
			base.OnEnter();
			base.GetComponent<CharacterDeathBehavior>().deathState = new SerializableEntityStateType(typeof(Hatch));
			if (NetworkServer.active)
			{
				base.healthComponent.Suicide(null, null, DamageType.Generic);
			}
		}
	}
}
