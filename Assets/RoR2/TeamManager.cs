using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B7 RID: 2231
	public class TeamManager : NetworkBehaviour
	{
		// Token: 0x060031A2 RID: 12706 RVA: 0x000D27C4 File Offset: 0x000D09C4
		static TeamManager()
		{
			List<ulong> list = new List<ulong>();
			list.Add(0UL);
			list.Add(0UL);
			TeamManager.naturalLevelCap = 2U;
			for (;;)
			{
				ulong num = (ulong)TeamManager.InitialCalcExperience(TeamManager.naturalLevelCap, 20.0, 1.55);
				if (num <= list[list.Count - 1])
				{
					break;
				}
				list.Add(num);
				TeamManager.naturalLevelCap += 1U;
			}
			TeamManager.naturalLevelCap -= 1U;
			TeamManager.levelToExperienceTable = list.ToArray();
			TeamManager.hardExpCap = TeamManager.levelToExperienceTable[TeamManager.levelToExperienceTable.Length - 1];
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000D2860 File Offset: 0x000D0A60
		private static double InitialCalcLevel(double experience, double experienceForFirstLevelUp = 20.0, double growthRate = 1.55)
		{
			return Math.Max(Math.Log(1.0 - experience / experienceForFirstLevelUp * (1.0 - growthRate), growthRate) + 1.0, 1.0);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000D2899 File Offset: 0x000D0A99
		private static double InitialCalcExperience(double level, double experienceForFirstLevelUp = 20.0, double growthRate = 1.55)
		{
			return Math.Max(experienceForFirstLevelUp * ((1.0 - Math.Pow(growthRate, level - 1.0)) / (1.0 - growthRate)), 0.0);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000D28D4 File Offset: 0x000D0AD4
		private static uint FindLevelForExperience(ulong experience)
		{
			uint num = 1U;
			while ((ulong)num < (ulong)((long)TeamManager.levelToExperienceTable.Length))
			{
				if (TeamManager.levelToExperienceTable[(int)num] > experience)
				{
					return num - 1U;
				}
				num += 1U;
			}
			return TeamManager.naturalLevelCap;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000D2908 File Offset: 0x000D0B08
		private static ulong GetExperienceForLevel(uint level)
		{
			if (level > TeamManager.naturalLevelCap)
			{
				level = TeamManager.naturalLevelCap;
			}
			return TeamManager.levelToExperienceTable[(int)level];
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x000D2928 File Offset: 0x000D0B28
		// (set) Token: 0x060031A7 RID: 12711 RVA: 0x000D2920 File Offset: 0x000D0B20
		public static TeamManager instance { get; private set; }

		// Token: 0x060031A9 RID: 12713 RVA: 0x000D292F File Offset: 0x000D0B2F
		private void OnEnable()
		{
			TeamManager.instance = SingletonHelper.Assign<TeamManager>(TeamManager.instance, this);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000D2941 File Offset: 0x000D0B41
		private void OnDisable()
		{
			TeamManager.instance = SingletonHelper.Unassign<TeamManager>(TeamManager.instance, this);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000D2954 File Offset: 0x000D0B54
		private void Start()
		{
			if (NetworkServer.active)
			{
				for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
				{
					this.SetTeamExperience(teamIndex, 0UL);
				}
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000D2980 File Offset: 0x000D0B80
		[Server]
		public void GiveTeamMoney(TeamIndex teamIndex, uint money)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeamManager::GiveTeamMoney(RoR2.TeamIndex,System.UInt32)' called on client");
				return;
			}
			int num = Run.instance ? Run.instance.livingPlayerCount : 0;
			if (num != 0)
			{
				money = (uint)Mathf.CeilToInt(money / (float)num);
			}
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				CharacterBody component = teamMembers[i].GetComponent<CharacterBody>();
				if (component && component.isPlayerControlled)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						master.GiveMoney(money);
					}
				}
			}
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000D2A1C File Offset: 0x000D0C1C
		[Server]
		public void GiveTeamExperience(TeamIndex teamIndex, ulong experience)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeamManager::GiveTeamExperience(RoR2.TeamIndex,System.UInt64)' called on client");
				return;
			}
			ulong num = this.teamExperience[(int)teamIndex];
			ulong num2 = num + experience;
			if (num2 < num)
			{
				num2 = ulong.MaxValue;
			}
			this.SetTeamExperience(teamIndex, num2);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000D2A5C File Offset: 0x000D0C5C
		private void SetTeamExperience(TeamIndex teamIndex, ulong newExperience)
		{
			if (newExperience > TeamManager.hardExpCap)
			{
				newExperience = TeamManager.hardExpCap;
			}
			this.teamExperience[(int)teamIndex] = newExperience;
			uint num = this.teamLevels[(int)teamIndex];
			uint num2 = TeamManager.FindLevelForExperience(newExperience);
			if (num != num2)
			{
				ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
				for (int i = 0; i < teamMembers.Count; i++)
				{
					CharacterBody component = teamMembers[i].GetComponent<CharacterBody>();
					if (component)
					{
						component.OnTeamLevelChanged();
					}
				}
				this.teamLevels[(int)teamIndex] = num2;
				this.teamCurrentLevelExperience[(int)teamIndex] = TeamManager.GetExperienceForLevel(num2);
				this.teamNextLevelExperience[(int)teamIndex] = TeamManager.GetExperienceForLevel(num2 + 1U);
				if (num < num2)
				{
					GlobalEventManager.OnTeamLevelUp(teamIndex);
				}
			}
			if (NetworkServer.active)
			{
				base.SetDirtyBit(1U << (int)teamIndex);
			}
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000D2B10 File Offset: 0x000D0D10
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = initialState ? 31U : base.syncVarDirtyBits;
			writer.WritePackedUInt32(num);
			if (num == 0U)
			{
				return false;
			}
			for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
			{
				if ((num & 1U << (int)teamIndex) != 0U)
				{
					writer.WritePackedUInt64(this.teamExperience[(int)teamIndex]);
				}
			}
			return true;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000D2B60 File Offset: 0x000D0D60
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			uint num = reader.ReadPackedUInt32();
			for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
			{
				if ((num & 1U << (int)teamIndex) != 0U)
				{
					ulong newExperience = reader.ReadPackedUInt64();
					this.SetTeamExperience(teamIndex, newExperience);
				}
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000D2B9A File Offset: 0x000D0D9A
		public ulong GetTeamExperience(TeamIndex teamIndex)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return 0UL;
			}
			return this.teamExperience[(int)teamIndex];
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000D2BAF File Offset: 0x000D0DAF
		public ulong GetTeamCurrentLevelExperience(TeamIndex teamIndex)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return 0UL;
			}
			return this.teamCurrentLevelExperience[(int)teamIndex];
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000D2BC4 File Offset: 0x000D0DC4
		public ulong GetTeamNextLevelExperience(TeamIndex teamIndex)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return 0UL;
			}
			return this.teamNextLevelExperience[(int)teamIndex];
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000D2BD9 File Offset: 0x000D0DD9
		public uint GetTeamLevel(TeamIndex teamIndex)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return 0U;
			}
			return this.teamLevels[(int)teamIndex];
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000D2BED File Offset: 0x000D0DED
		public void SetTeamLevel(TeamIndex teamIndex, uint newLevel)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return;
			}
			if (this.GetTeamLevel(teamIndex) == newLevel)
			{
				return;
			}
			this.SetTeamExperience(teamIndex, TeamManager.GetExperienceForLevel(newLevel));
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000D2C10 File Offset: 0x000D0E10
		public static bool IsTeamEnemy(TeamIndex teamA, TeamIndex teamB)
		{
			return teamA != teamB;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000D2C1C File Offset: 0x000D0E1C
		[ConCommand(commandName = "team_set_level", flags = (ConVarFlags.ExecuteOnServer | ConVarFlags.Cheat), helpText = "Sets the team specified by the first argument to the level specified by the second argument.")]
		private static void CCTeamSetLevel(ConCommandArgs args)
		{
			TeamIndex argEnum = args.GetArgEnum<TeamIndex>(0);
			ulong argULong = args.GetArgULong(1);
			if (!TeamManager.instance)
			{
				throw new ConCommandException("No TeamManager exists.");
			}
			TeamManager.instance.SetTeamLevel(argEnum, (uint)argULong);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400331A RID: 13082
		public static readonly uint naturalLevelCap;

		// Token: 0x0400331B RID: 13083
		private static readonly ulong[] levelToExperienceTable;

		// Token: 0x0400331C RID: 13084
		public static readonly ulong hardExpCap;

		// Token: 0x0400331E RID: 13086
		private ulong[] teamExperience = new ulong[5];

		// Token: 0x0400331F RID: 13087
		private uint[] teamLevels = new uint[5];

		// Token: 0x04003320 RID: 13088
		private ulong[] teamCurrentLevelExperience = new ulong[5];

		// Token: 0x04003321 RID: 13089
		private ulong[] teamNextLevelExperience = new ulong[5];

		// Token: 0x04003322 RID: 13090
		private const uint teamExperienceDirtyBitsMask = 31U;
	}
}
