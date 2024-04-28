using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Barrel
{
	// Token: 0x02000473 RID: 1139
	public class OpeningLunar : BaseState
	{
		// Token: 0x0600145C RID: 5212 RVA: 0x0005AD18 File Offset: 0x00058F18
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Opening", "Opening.playbackRate", OpeningLunar.duration);
			if (base.sfxLocator)
			{
				Util.PlaySound(base.sfxLocator.openSound, base.gameObject);
			}
			this.StopSteamEffect();
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0005AD6F File Offset: 0x00058F6F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= OpeningLunar.duration)
			{
				this.outer.SetNextState(new Opened());
				return;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0005AD98 File Offset: 0x00058F98
		private void StopSteamEffect()
		{
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("SteamEffect");
					if (transform)
					{
						ParticleSystem component2 = transform.GetComponent<ParticleSystem>();
						if (component2)
						{
							component2.main.loop = false;
						}
					}
				}
			}
		}

		// Token: 0x04001A26 RID: 6694
		public static float duration = 1f;
	}
}
