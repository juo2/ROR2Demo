using System;
using RoR2;

namespace EntityStates.ParentEgg
{
	// Token: 0x02000226 RID: 550
	public class IncubateState : BaseEggState
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00027CDC File Offset: 0x00025EDC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = base.controller.incubationDuration;
			this.PlayAnimation("Body", "Spawn");
			Util.PlaySound(base.controller.podSpawnSound, base.gameObject);
			EffectManager.SimpleEffect(base.controller.spawnEffect, base.gameObject.transform.position, base.transform.rotation, true);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00027D53 File Offset: 0x00025F53
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new PreHatch());
			}
		}

		// Token: 0x04000B32 RID: 2866
		private float duration;
	}
}
