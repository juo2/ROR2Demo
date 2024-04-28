using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.RaidGauntletEncounterSlowDeath
{
	// Token: 0x0200023E RID: 574
	public class Listening : EntityState
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x00029F76 File Offset: 0x00028176
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00029F80 File Offset: 0x00028180
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				if (base.fixedAge >= 2f)
				{
					this.RegisterShards();
					this.RegisterGauntletMissionController();
				}
				if (base.fixedAge >= 15f && !this.beginGauntletCountdown)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "GAUNTLET_START"
					});
					this.beginGauntletCountdown = true;
				}
				if (base.fixedAge >= 25f && !this.gauntletTwentySecondWarning)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "GAUNTLET_TWENTY_SECONDS_WARNING"
					});
					this.gauntletTwentySecondWarning = true;
				}
				if (base.fixedAge >= 35f && !this.gauntletTenSecondWarning)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "GAUNTLET_TEN_SECONDS_WARNING"
					});
					this.gauntletTenSecondWarning = true;
				}
				if (base.fixedAge >= 40f && !this.gauntletFiveSecondWarning)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "GAUNTLET_FIVE_SECONDS_WARNING"
					});
					this.gauntletFiveSecondWarning = true;
					this.beginGauntletFinalCountdown = true;
				}
				if (this.hasRegisteredShards)
				{
					int num = 0;
					for (int i = 0; i < this.shardList.Count; i++)
					{
						if (this.shardList[i] == null)
						{
							num++;
						}
					}
					if (this.previousDestroyedShardCount != num)
					{
						int num2 = Listening.shardsDestroyedToTriggerEncounter;
					}
					int num3 = Listening.shardsDestroyedToTriggerEncounter - 1;
					if (this.previousDestroyedShardCount < num3 && num >= num3)
					{
						Chat.SendBroadcastChat(new Chat.SimpleChatMessage
						{
							baseToken = "GAUNTLET_ONE_SHARD_REMAINING"
						});
					}
					if (num >= Listening.shardsDestroyedToTriggerEncounter && !this.beginEncounterCountdown)
					{
						this.beginEncounterCountdown = true;
						Chat.SendBroadcastChat(new Chat.SimpleChatMessage
						{
							baseToken = "GAUNTLET_ALL_SHARDS_DESTROYED"
						});
					}
					if (this.beginGauntletFinalCountdown && !this.gauntletEnd)
					{
						this.gauntletFinalCountdown += Time.fixedDeltaTime;
						if (this.gauntletFinalCountdown >= 1f)
						{
							Debug.Log(this.secondsRemaining + " seconds remaining!");
							this.secondsRemaining--;
							this.totalSeconds++;
							if (this.totalSeconds == 1)
							{
								Chat.SendBroadcastChat(new Chat.SimpleChatMessage
								{
									baseToken = "GAUNTLET_FOUR_SECONDS_REMAINING"
								});
							}
							else if (this.totalSeconds == 2)
							{
								Chat.SendBroadcastChat(new Chat.SimpleChatMessage
								{
									baseToken = "GAUNTLET_THREE_SECONDS_REMAINING"
								});
							}
							else if (this.totalSeconds == 3)
							{
								Chat.SendBroadcastChat(new Chat.SimpleChatMessage
								{
									baseToken = "GAUNTLET_TWO_SECONDS_REMAINING"
								});
							}
							else if (this.totalSeconds == 4)
							{
								Chat.SendBroadcastChat(new Chat.SimpleChatMessage
								{
									baseToken = "GAUNTLET_ONE_SECOND_REMAINING"
								});
							}
							this.gauntletFinalCountdown = 0f;
						}
						if (this.secondsRemaining == 0)
						{
							this.gauntletEnd = true;
							Chat.SendBroadcastChat(new Chat.SimpleChatMessage
							{
								baseToken = "GAUNTLET_SLOWDEATH_END"
							});
						}
					}
					if (this.gauntletEnd && !this.theEnd)
					{
						this.gauntletMissionController.GauntletMissionTimesUp();
						this.theEnd = true;
					}
					this.previousDestroyedShardCount = num;
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002A268 File Offset: 0x00028468
		private void RegisterShards()
		{
			if (this.hasRegisteredShards)
			{
				return;
			}
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				if (readOnlyInstancesList[i].name.Contains("GauntletShard"))
				{
					this.shardList.Add(readOnlyInstancesList[i].gameObject);
					Debug.Log("Found a Gauntlet Shard!");
				}
			}
			Listening.shardsDestroyedToTriggerEncounter = this.shardList.Count;
			Debug.Log("Found " + this.shardList.Count + " Gauntlet Shards!");
			this.hasRegisteredShards = true;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002A30C File Offset: 0x0002850C
		private void RegisterGauntletMissionController()
		{
			if (this.hasGauntletMissionController)
			{
				return;
			}
			this.gMC = GameObject.Find("GauntletMissionController");
			this.gauntletMissionController = this.gMC.GetComponent<GauntletMissionController>();
			if (this.gauntletMissionController)
			{
				this.slowDeathEffect = this.gauntletMissionController.clearedEffect;
				this.degenTickFrequency = this.gauntletMissionController.degenTickFrequency;
				this.percentDegenPerSecond = this.gauntletMissionController.percentDegenPerSecond;
				Debug.Log("Found Gauntlet Mission Controller!");
				this.hasGauntletMissionController = true;
				return;
			}
			Debug.Log("WARNING - DID NOT Gauntlet Mission Controller!");
		}

		// Token: 0x04000BD0 RID: 3024
		public static float delayBeforeBeginningEncounter;

		// Token: 0x04000BD1 RID: 3025
		public static int shardsDestroyedToTriggerEncounter;

		// Token: 0x04000BD2 RID: 3026
		private List<GameObject> shardList = new List<GameObject>();

		// Token: 0x04000BD3 RID: 3027
		private const float delayBeforeRegisteringShards = 2f;

		// Token: 0x04000BD4 RID: 3028
		private bool hasRegisteredShards;

		// Token: 0x04000BD5 RID: 3029
		private int previousDestroyedShardCount;

		// Token: 0x04000BD6 RID: 3030
		private bool beginEncounterCountdown;

		// Token: 0x04000BD7 RID: 3031
		private bool beginGauntletCountdown;

		// Token: 0x04000BD8 RID: 3032
		private bool gauntletTwentySecondWarning;

		// Token: 0x04000BD9 RID: 3033
		private bool gauntletTenSecondWarning;

		// Token: 0x04000BDA RID: 3034
		private bool gauntletFiveSecondWarning;

		// Token: 0x04000BDB RID: 3035
		private bool beginGauntletFinalCountdown;

		// Token: 0x04000BDC RID: 3036
		private const float delayBeforeBeginningGauntletCountdown = 15f;

		// Token: 0x04000BDD RID: 3037
		private float gauntletFinalCountdown;

		// Token: 0x04000BDE RID: 3038
		private int secondsRemaining = 4;

		// Token: 0x04000BDF RID: 3039
		private bool gauntletEnd;

		// Token: 0x04000BE0 RID: 3040
		private int totalSeconds;

		// Token: 0x04000BE1 RID: 3041
		private GameObject slowDeathEffect;

		// Token: 0x04000BE2 RID: 3042
		private bool slowDeathEffectActive;

		// Token: 0x04000BE3 RID: 3043
		private GauntletMissionController gauntletMissionController;

		// Token: 0x04000BE4 RID: 3044
		private GameObject gMC;

		// Token: 0x04000BE5 RID: 3045
		private float degenTickFrequency;

		// Token: 0x04000BE6 RID: 3046
		private float percentDegenPerSecond;

		// Token: 0x04000BE7 RID: 3047
		private bool hasGauntletMissionController;

		// Token: 0x04000BE8 RID: 3048
		private bool theEnd;
	}
}
