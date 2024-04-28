using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MiniMushroom
{
	// Token: 0x02000270 RID: 624
	public class Plant : BaseState
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x0002C6CC File Offset: 0x0002A8CC
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Plant", "PlantLoop");
			this.maxDuration = Plant.baseMaxDuration / this.attackSpeedStat;
			this.minDuration = Plant.baseMinDuration / this.attackSpeedStat;
			this.soundID = Util.PlaySound(Plant.healSoundLoop, base.characterBody.modelLocator.modelTransform.gameObject);
			if (!NetworkServer.active)
			{
				return;
			}
			if (this.mushroomWard == null)
			{
				this.mushroomWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MiniMushroomWard"), base.characterBody.footPosition, Quaternion.identity);
				this.mushroomWard.GetComponent<TeamFilter>().teamIndex = base.teamComponent.teamIndex;
				if (this.mushroomWard)
				{
					HealingWard component = this.mushroomWard.GetComponent<HealingWard>();
					component.healFraction = Plant.healFraction;
					component.healPoints = 0f;
					component.Networkradius = Plant.mushroomRadius;
				}
				NetworkServer.Spawn(this.mushroomWard);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				bool flag = base.inputBank.moveVector.sqrMagnitude > 0.1f;
				if (base.fixedAge > this.maxDuration || (base.fixedAge > this.minDuration && flag))
				{
					this.outer.SetNextState(new UnPlant());
				}
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002C838 File Offset: 0x0002AA38
		public override void OnExit()
		{
			this.PlayAnimation("Plant", "Empty");
			AkSoundEngine.StopPlayingID(this.soundID);
			Util.PlaySound(Plant.healSoundStop, base.gameObject);
			if (this.mushroomWard)
			{
				EntityState.Destroy(this.mushroomWard);
			}
			base.OnExit();
		}

		// Token: 0x04000C6B RID: 3179
		public static float healFraction;

		// Token: 0x04000C6C RID: 3180
		public static float baseMaxDuration;

		// Token: 0x04000C6D RID: 3181
		public static float baseMinDuration;

		// Token: 0x04000C6E RID: 3182
		public static float mushroomRadius;

		// Token: 0x04000C6F RID: 3183
		public static string healSoundLoop;

		// Token: 0x04000C70 RID: 3184
		public static string healSoundStop;

		// Token: 0x04000C71 RID: 3185
		private float maxDuration;

		// Token: 0x04000C72 RID: 3186
		private float minDuration;

		// Token: 0x04000C73 RID: 3187
		private GameObject mushroomWard;

		// Token: 0x04000C74 RID: 3188
		private uint soundID;
	}
}
