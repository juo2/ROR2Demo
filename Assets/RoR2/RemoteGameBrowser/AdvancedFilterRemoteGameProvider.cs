using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000ACA RID: 2762
	public class AdvancedFilterRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x06003F80 RID: 16256 RVA: 0x001063EB File Offset: 0x001045EB
		public new AdvancedFilterRemoteGameProvider.SearchFilters GetSearchFilters()
		{
			return this.searchFilters;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x001063F3 File Offset: 0x001045F3
		public void SetSearchFilters(AdvancedFilterRemoteGameProvider.SearchFilters newSearchFilters)
		{
			if (this.searchFilters.Equals(newSearchFilters))
			{
				return;
			}
			this.searchFilters = newSearchFilters;
			base.SetDirty();
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x00106411 File Offset: 0x00104611
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			RemoteGameInfo[] srcInfo = this.src.GetKnownGames();
			AdvancedFilterRemoteGameProvider.SearchFilters capturedSearchFilters = this.searchFilters;
			return new Task<RemoteGameInfo[]>(delegate()
			{
				AdvancedFilterRemoteGameProvider.<>c__DisplayClass5_1 CS$<>8__locals2;
				CS$<>8__locals2.localSearchFilters = capturedSearchFilters;
				bool[] array = new bool[srcInfo.Length];
				int num = 0;
				for (int i = 0; i < srcInfo.Length; i++)
				{
					if (AdvancedFilterRemoteGameProvider.<CreateTask>g__PassesFilters|5_1(srcInfo[i], ref CS$<>8__locals2))
					{
						array[i] = true;
						num++;
					}
				}
				RemoteGameInfo[] array2 = new RemoteGameInfo[num];
				int j = 0;
				int num2 = 0;
				while (j < array2.Length)
				{
					while (!array[num2])
					{
						num2++;
					}
					array2[j] = srcInfo[num2++];
					j++;
				}
				return array2;
			});
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x00106445 File Offset: 0x00104645
		public override bool RequestRefresh()
		{
			return this.src.RequestRefresh();
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x00106452 File Offset: 0x00104652
		public AdvancedFilterRemoteGameProvider([NotNull] IRemoteGameProvider src)
		{
			this.src = src;
			this.src.onNewInfoAvailable += base.SetDirty;
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x00106478 File Offset: 0x00104678
		public override void Dispose()
		{
			this.src.onNewInfoAvailable -= base.SetDirty;
			base.Dispose();
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x00106497 File Offset: 0x00104697
		public override bool IsBusy()
		{
			if (!base.IsBusy())
			{
				IRemoteGameProvider remoteGameProvider = this.src;
				return remoteGameProvider != null && remoteGameProvider.IsBusy();
			}
			return true;
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x001064B4 File Offset: 0x001046B4
		[CompilerGenerated]
		internal static bool <CreateTask>g__PassesFilters|5_1(in RemoteGameInfo remoteGameInfo, ref AdvancedFilterRemoteGameProvider.<>c__DisplayClass5_1 A_1)
		{
			if (!A_1.localSearchFilters.allowPassword)
			{
				bool? hasPassword = remoteGameInfo.hasPassword;
				if (hasPassword.GetValueOrDefault(false))
				{
					return false;
				}
			}
			if (A_1.localSearchFilters.requiredSlots > 0 && remoteGameInfo.availableSlots < A_1.localSearchFilters.requiredSlots)
			{
				return false;
			}
			if (A_1.localSearchFilters.maxPing > 0 && remoteGameInfo.ping != null && remoteGameInfo.ping.Value > A_1.localSearchFilters.maxPing)
			{
				return false;
			}
			if (A_1.localSearchFilters.minMaxPlayers > remoteGameInfo.lesserMaxPlayers)
			{
				return false;
			}
			if (A_1.localSearchFilters.maxMaxPlayers < remoteGameInfo.greaterMaxPlayers)
			{
				return false;
			}
			DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(remoteGameInfo.currentDifficultyIndex ?? DifficultyIndex.Invalid);
			if (difficultyDef == null)
			{
				return false;
			}
			if (!A_1.localSearchFilters.allowDifficultyEasy)
			{
				DifficultyIndex? currentDifficultyIndex = remoteGameInfo.currentDifficultyIndex;
				DifficultyIndex difficultyIndex = DifficultyIndex.Easy;
				if (currentDifficultyIndex.GetValueOrDefault() == difficultyIndex & currentDifficultyIndex != null)
				{
					return false;
				}
			}
			if (!A_1.localSearchFilters.allowDifficultyNormal)
			{
				DifficultyIndex? currentDifficultyIndex = remoteGameInfo.currentDifficultyIndex;
				DifficultyIndex difficultyIndex = DifficultyIndex.Normal;
				if (currentDifficultyIndex.GetValueOrDefault() == difficultyIndex & currentDifficultyIndex != null)
				{
					return false;
				}
			}
			if (!A_1.localSearchFilters.allowDifficultyHard && difficultyDef.countsAsHardMode)
			{
				return false;
			}
			if (!A_1.localSearchFilters.showGamesWithRuleVoting)
			{
				RemoteGameInfo remoteGameInfo2 = remoteGameInfo;
				if (remoteGameInfo2.HasTag("rv1"))
				{
					return false;
				}
			}
			if (!A_1.localSearchFilters.showGamesWithoutRuleVoting)
			{
				RemoteGameInfo remoteGameInfo2 = remoteGameInfo;
				if (remoteGameInfo2.HasTag("rv0"))
				{
					return false;
				}
			}
			return A_1.localSearchFilters.allowInProgressGames || string.IsNullOrEmpty(remoteGameInfo.currentSceneName) || remoteGameInfo.currentSceneName == "lobby";
		}

		// Token: 0x04003DDD RID: 15837
		private IRemoteGameProvider src;

		// Token: 0x04003DDE RID: 15838
		private new AdvancedFilterRemoteGameProvider.SearchFilters searchFilters;

		// Token: 0x02000ACB RID: 2763
		public new struct SearchFilters : IEquatable<AdvancedFilterRemoteGameProvider.SearchFilters>
		{
			// Token: 0x06003F88 RID: 16264 RVA: 0x00106670 File Offset: 0x00104870
			public bool Equals(AdvancedFilterRemoteGameProvider.SearchFilters other)
			{
				return this.allowPassword == other.allowPassword && this.requiredSlots == other.requiredSlots && this.maxPing == other.maxPing && this.minMaxPlayers == other.minMaxPlayers && this.maxMaxPlayers == other.maxMaxPlayers && this.allowDifficultyEasy == other.allowDifficultyEasy && this.allowDifficultyNormal == other.allowDifficultyNormal && this.allowDifficultyHard == other.allowDifficultyHard && this.showGamesWithRuleVoting == other.showGamesWithRuleVoting && this.showGamesWithoutRuleVoting == other.showGamesWithoutRuleVoting && this.allowInProgressGames == other.allowInProgressGames;
			}

			// Token: 0x06003F89 RID: 16265 RVA: 0x0010671C File Offset: 0x0010491C
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is AdvancedFilterRemoteGameProvider.SearchFilters)
				{
					AdvancedFilterRemoteGameProvider.SearchFilters other = (AdvancedFilterRemoteGameProvider.SearchFilters)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06003F8A RID: 16266 RVA: 0x00106748 File Offset: 0x00104948
			public override int GetHashCode()
			{
				return (((((((((this.allowPassword.GetHashCode() * 397 ^ this.requiredSlots) * 397 ^ this.maxPing) * 397 ^ this.minMaxPlayers) * 397 ^ this.maxMaxPlayers) * 397 ^ this.allowDifficultyEasy.GetHashCode()) * 397 ^ this.allowDifficultyNormal.GetHashCode()) * 397 ^ this.allowDifficultyHard.GetHashCode()) * 397 ^ this.showGamesWithRuleVoting.GetHashCode()) * 397 ^ this.showGamesWithoutRuleVoting.GetHashCode()) * 397 ^ this.allowInProgressGames.GetHashCode();
			}

			// Token: 0x04003DDF RID: 15839
			public bool allowPassword;

			// Token: 0x04003DE0 RID: 15840
			public int requiredSlots;

			// Token: 0x04003DE1 RID: 15841
			public int maxPing;

			// Token: 0x04003DE2 RID: 15842
			public int minMaxPlayers;

			// Token: 0x04003DE3 RID: 15843
			public int maxMaxPlayers;

			// Token: 0x04003DE4 RID: 15844
			public bool allowDifficultyEasy;

			// Token: 0x04003DE5 RID: 15845
			public bool allowDifficultyNormal;

			// Token: 0x04003DE6 RID: 15846
			public bool allowDifficultyHard;

			// Token: 0x04003DE7 RID: 15847
			public bool showGamesWithRuleVoting;

			// Token: 0x04003DE8 RID: 15848
			public bool showGamesWithoutRuleVoting;

			// Token: 0x04003DE9 RID: 15849
			public bool allowInProgressGames;
		}
	}
}
