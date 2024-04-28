using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.RaidGauntletEncounter
{
	// Token: 0x0200023F RID: 575
	public class Listening : EntityState
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x00029F76 File Offset: 0x00028176
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002A3BC File Offset: 0x000285BC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				if (base.fixedAge >= 2f)
				{
					this.RegisterShards();
					this.RegisterExitPortal();
				}
				if (base.fixedAge >= 15f && !this.beginGauntletCountdown)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "GAUNTLET_START"
					});
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "GAUNTLET_FIVE_SHARDS_REMAINING"
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
						int num2 = Listening.shardsDestroyedToTriggerEncounter - num;
						if (num2 == 4)
						{
							Chat.SendBroadcastChat(new Chat.SimpleChatMessage
							{
								baseToken = "GAUNTLET_FOUR_SHARDS_REMAINING"
							});
						}
						else if (num2 == 3)
						{
							Chat.SendBroadcastChat(new Chat.SimpleChatMessage
							{
								baseToken = "GAUNTLET_THREE_SHARDS_REMAINING"
							});
						}
						else if (num2 == 2)
						{
							Chat.SendBroadcastChat(new Chat.SimpleChatMessage
							{
								baseToken = "GAUNTLET_TWO_SHARDS_REMAINING"
							});
						}
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
							this.exitPortal.SetActive(false);
							Chat.SendBroadcastChat(new Chat.SimpleChatMessage
							{
								baseToken = "GAUNTLET_END"
							});
						}
					}
					this.previousDestroyedShardCount = num;
				}
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002A6F4 File Offset: 0x000288F4
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
			Debug.Log("Found " + this.shardList.Count + " Gauntlet Shards!");
			this.hasRegisteredShards = true;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002A788 File Offset: 0x00028988
		private void RegisterExitPortal()
		{
			if (this.hasExitPortal)
			{
				return;
			}
			this.exitPortal = GameObject.Find("PortalArena");
			if (this.exitPortal)
			{
				Debug.Log("Found Exit Portal!");
				this.hasExitPortal = true;
				return;
			}
			Debug.Log("WARNING - DID NOT FIND EXIT PORTAL!");
		}

		// Token: 0x04000BE9 RID: 3049
		public static float delayBeforeBeginningEncounter;

		// Token: 0x04000BEA RID: 3050
		public static int shardsDestroyedToTriggerEncounter = 5;

		// Token: 0x04000BEB RID: 3051
		private GameObject exitPortal;

		// Token: 0x04000BEC RID: 3052
		private List<GameObject> shardList = new List<GameObject>();

		// Token: 0x04000BED RID: 3053
		private const float delayBeforeRegisteringShards = 2f;

		// Token: 0x04000BEE RID: 3054
		private bool hasRegisteredShards;

		// Token: 0x04000BEF RID: 3055
		private int previousDestroyedShardCount;

		// Token: 0x04000BF0 RID: 3056
		private bool beginEncounterCountdown;

		// Token: 0x04000BF1 RID: 3057
		private bool beginGauntletCountdown;

		// Token: 0x04000BF2 RID: 3058
		private bool gauntletTwentySecondWarning;

		// Token: 0x04000BF3 RID: 3059
		private bool gauntletTenSecondWarning;

		// Token: 0x04000BF4 RID: 3060
		private bool gauntletFiveSecondWarning;

		// Token: 0x04000BF5 RID: 3061
		private bool beginGauntletFinalCountdown;

		// Token: 0x04000BF6 RID: 3062
		private const float delayBeforeBeginningGauntletCountdown = 15f;

		// Token: 0x04000BF7 RID: 3063
		private float gauntletFinalCountdown;

		// Token: 0x04000BF8 RID: 3064
		private int secondsRemaining = 4;

		// Token: 0x04000BF9 RID: 3065
		private bool gauntletEnd;

		// Token: 0x04000BFA RID: 3066
		private bool hasExitPortal;

		// Token: 0x04000BFB RID: 3067
		private int totalSeconds;
	}
}
