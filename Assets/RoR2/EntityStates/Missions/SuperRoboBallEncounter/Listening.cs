using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.SuperRoboBallEncounter
{
	// Token: 0x0200023D RID: 573
	public class Listening : EntityState
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x00029DD1 File Offset: 0x00027FD1
		public override void OnEnter()
		{
			base.OnEnter();
			this.scriptedCombatEncounter = base.GetComponent<ScriptedCombatEncounter>();
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00029DE8 File Offset: 0x00027FE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				if (base.fixedAge >= 2f)
				{
					this.RegisterEggs();
				}
				if (this.hasRegisteredEggs)
				{
					int num = 0;
					for (int i = 0; i < this.eggList.Count; i++)
					{
						if (this.eggList[i] == null)
						{
							num++;
						}
					}
					int num2 = Listening.eggsDestroyedToTriggerEncounter - 1;
					if (this.previousDestroyedEggCount < num2 && num >= num2)
					{
						Chat.SendBroadcastChat(new Chat.SimpleChatMessage
						{
							baseToken = "VULTURE_EGG_WARNING"
						});
					}
					if (num >= Listening.eggsDestroyedToTriggerEncounter && !this.beginEncounterCountdown)
					{
						this.encounterCountdown = Listening.delayBeforeBeginningEncounter;
						this.beginEncounterCountdown = true;
						Chat.SendBroadcastChat(new Chat.SimpleChatMessage
						{
							baseToken = "VULTURE_EGG_BEGIN"
						});
					}
					if (this.beginEncounterCountdown)
					{
						this.encounterCountdown -= Time.fixedDeltaTime;
						if (this.encounterCountdown <= 0f)
						{
							this.scriptedCombatEncounter.BeginEncounter();
							this.outer.SetNextState(new Idle());
						}
					}
					this.previousDestroyedEggCount = num;
				}
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00029F00 File Offset: 0x00028100
		private void RegisterEggs()
		{
			if (this.hasRegisteredEggs)
			{
				return;
			}
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				if (readOnlyInstancesList[i].name.Contains("VultureEgg"))
				{
					this.eggList.Add(readOnlyInstancesList[i].gameObject);
				}
			}
			this.hasRegisteredEggs = true;
		}

		// Token: 0x04000BC7 RID: 3015
		public static float delayBeforeBeginningEncounter;

		// Token: 0x04000BC8 RID: 3016
		public static int eggsDestroyedToTriggerEncounter;

		// Token: 0x04000BC9 RID: 3017
		private ScriptedCombatEncounter scriptedCombatEncounter;

		// Token: 0x04000BCA RID: 3018
		private List<GameObject> eggList = new List<GameObject>();

		// Token: 0x04000BCB RID: 3019
		private const float delayBeforeRegisteringEggs = 2f;

		// Token: 0x04000BCC RID: 3020
		private bool hasRegisteredEggs;

		// Token: 0x04000BCD RID: 3021
		private int previousDestroyedEggCount;

		// Token: 0x04000BCE RID: 3022
		private bool beginEncounterCountdown;

		// Token: 0x04000BCF RID: 3023
		private float encounterCountdown;
	}
}
