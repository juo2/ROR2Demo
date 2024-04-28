using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000251 RID: 593
	public class Phase1 : BrotherEncounterPhaseBaseState
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002B988 File Offset: 0x00029B88
		protected override string phaseControllerChildString
		{
			get
			{
				return "Phase1";
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002B98F File Offset: 0x00029B8F
		protected override EntityState nextState
		{
			get
			{
				return new Phase2();
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002B996 File Offset: 0x00029B96
		public override void OnEnter()
		{
			base.KillAllMonsters();
			base.OnEnter();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002B9A4 File Offset: 0x00029BA4
		protected override void PreEncounterBegin()
		{
			base.PreEncounterBegin();
			Transform transform = this.childLocator.FindChild("CenterOrbEffect");
			transform.gameObject.SetActive(false);
			EffectManager.SpawnEffect(Phase1.centerOrbDestroyEffect, new EffectData
			{
				origin = transform.transform.position
			}, false);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.hasPlayedPrespawnSound && base.fixedAge > Phase1.prespawnSoundDelay)
			{
				Transform transform = this.childLocator.FindChild("CenterOrbEffect");
				Util.PlaySound(Phase1.prespawnSoundString, transform.gameObject);
				this.hasPlayedPrespawnSound = true;
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002BA49 File Offset: 0x00029C49
		protected override void OnMemberAddedServer(CharacterMaster master)
		{
			base.OnMemberAddedServer(master);
		}

		// Token: 0x04000C36 RID: 3126
		public static string prespawnSoundString;

		// Token: 0x04000C37 RID: 3127
		public static float prespawnSoundDelay;

		// Token: 0x04000C38 RID: 3128
		public static GameObject centerOrbDestroyEffect;

		// Token: 0x04000C39 RID: 3129
		private bool hasPlayedPrespawnSound;
	}
}
