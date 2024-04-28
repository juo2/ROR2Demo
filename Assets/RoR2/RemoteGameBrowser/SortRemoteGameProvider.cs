using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AEC RID: 2796
	public class SortRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x0600402C RID: 16428 RVA: 0x001092DE File Offset: 0x001074DE
		public SortRemoteGameProvider([NotNull] IRemoteGameProvider source)
		{
			this.source = source;
			source.onNewInfoAvailable += this.OnSourceNewInfoAvailable;
			this.maxTasks = 1;
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x00109306 File Offset: 0x00107506
		public override void Dispose()
		{
			this.source.onNewInfoAvailable -= this.OnSourceNewInfoAvailable;
			base.Dispose();
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x001069D6 File Offset: 0x00104BD6
		private void OnSourceNewInfoAvailable()
		{
			base.SetDirty();
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x00109328 File Offset: 0x00107528
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			SortRemoteGameProvider.Parameters parameters = this.GetParameters();
			RemoteGameInfo[] input = this.source.GetKnownGames();
			RemoteGameInfo[] output = new RemoteGameInfo[input.Length];
			SortRemoteGameProvider.Sorter sorter = SortRemoteGameProvider.sorters[parameters.sorterIndex];
			bool ascending = parameters.ascending;
			return new Task<RemoteGameInfo[]>(delegate()
			{
				SortRemoteGameProvider.Sort(input, output, sorter, ascending, cancellationToken);
				return output;
			});
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x0010939A File Offset: 0x0010759A
		public override bool RequestRefresh()
		{
			return this.source.RequestRefresh();
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x001093A7 File Offset: 0x001075A7
		public override bool IsBusy()
		{
			return base.IsBusy() || this.source.IsBusy();
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x001093C0 File Offset: 0x001075C0
		private static void Sort(RemoteGameInfo[] input, RemoteGameInfo[] output, SortRemoteGameProvider.Sorter sorter, bool ascending, CancellationToken cancellationToken)
		{
			SortRemoteGameProvider.<>c__DisplayClass8_0 CS$<>8__locals1 = new SortRemoteGameProvider.<>c__DisplayClass8_0();
			CS$<>8__locals1.input = input;
			cancellationToken.ThrowIfCancellationRequested();
			int[] array = new int[CS$<>8__locals1.input.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			cancellationToken.ThrowIfCancellationRequested();
			CS$<>8__locals1.comparer = sorter.comparer;
			cancellationToken.ThrowIfCancellationRequested();
			Array.Sort<int>(array, new Comparison<int>(CS$<>8__locals1.<Sort>g__Compare|0));
			cancellationToken.ThrowIfCancellationRequested();
			if (ascending)
			{
				for (int j = 0; j < array.Length; j++)
				{
					output[j] = CS$<>8__locals1.input[array[j]];
				}
				return;
			}
			for (int k = 0; k < array.Length; k++)
			{
				output[k] = CS$<>8__locals1.input[array[array.Length - 1 - k]];
			}
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x0010948C File Offset: 0x0010768C
		public SortRemoteGameProvider.Parameters GetParameters()
		{
			SortRemoteGameProvider.Parameters result;
			lock (this)
			{
				result = this.currentParameters;
			}
			return result;
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x001094CC File Offset: 0x001076CC
		public void SetParameters(SortRemoteGameProvider.Parameters newParameters)
		{
			if (this.currentParameters.Equals(newParameters))
			{
				return;
			}
			this.SetParametersInternal(newParameters);
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x001094E4 File Offset: 0x001076E4
		private void SetParametersInternal(SortRemoteGameProvider.Parameters newParameters)
		{
			lock (this)
			{
				this.currentParameters = newParameters;
				base.SetDirty();
			}
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x00109528 File Offset: 0x00107728
		private static int ComparePing(in RemoteGameInfo a, in RemoteGameInfo b)
		{
			int num = a.ping ?? int.MinValue;
			int value = b.ping ?? int.MinValue;
			return num.CompareTo(value);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0010957C File Offset: 0x0010777C
		private static int ComparePlayerCount(in RemoteGameInfo a, in RemoteGameInfo b)
		{
			int lesserPlayerCount = a.lesserPlayerCount;
			return lesserPlayerCount.CompareTo(b.lesserPlayerCount);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x001095A0 File Offset: 0x001077A0
		private static int CompareMaxPlayerCount(in RemoteGameInfo a, in RemoteGameInfo b)
		{
			int lesserPlayerCount = a.lesserPlayerCount;
			return lesserPlayerCount.CompareTo(b.lesserPlayerCount);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x001095C1 File Offset: 0x001077C1
		private static int CompareName(in RemoteGameInfo a, in RemoteGameInfo b)
		{
			return a.name.CompareTo(b.name);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x001095D4 File Offset: 0x001077D4
		public static int CompareAvailableSlots(in RemoteGameInfo a, in RemoteGameInfo b)
		{
			int availableSlots = a.availableSlots;
			return availableSlots.CompareTo(b.availableSlots);
		}

		// Token: 0x04003E8A RID: 16010
		private readonly IRemoteGameProvider source;

		// Token: 0x04003E8B RID: 16011
		private SortRemoteGameProvider.Parameters currentParameters;

		// Token: 0x04003E8C RID: 16012
		public static SortRemoteGameProvider.Sorter[] sorters = new SortRemoteGameProvider.Sorter[]
		{
			new SortRemoteGameProvider.Sorter
			{
				nameToken = "GAME_BROWSER_SORTER_PING",
				comparer = new SortRemoteGameProvider.RemoteGameProviderComparison(SortRemoteGameProvider.ComparePing)
			},
			new SortRemoteGameProvider.Sorter
			{
				nameToken = "GAME_BROWSER_SORTER_NAME",
				comparer = new SortRemoteGameProvider.RemoteGameProviderComparison(SortRemoteGameProvider.CompareName)
			},
			new SortRemoteGameProvider.Sorter
			{
				nameToken = "GAME_BROWSER_SORTER_PLAYER_COUNT",
				comparer = new SortRemoteGameProvider.RemoteGameProviderComparison(SortRemoteGameProvider.ComparePlayerCount)
			},
			new SortRemoteGameProvider.Sorter
			{
				nameToken = "GAME_BROWSER_SORTER_MAX_PLAYER_COUNT",
				comparer = new SortRemoteGameProvider.RemoteGameProviderComparison(SortRemoteGameProvider.CompareMaxPlayerCount)
			},
			new SortRemoteGameProvider.Sorter
			{
				nameToken = "GAME_BROWSER_SORTER_AVAILABLE_SLOTS",
				comparer = new SortRemoteGameProvider.RemoteGameProviderComparison(SortRemoteGameProvider.CompareAvailableSlots)
			}
		};

		// Token: 0x02000AED RID: 2797
		// (Invoke) Token: 0x0600403D RID: 16445
		public delegate int RemoteGameProviderComparison(in RemoteGameInfo a, in RemoteGameInfo b);

		// Token: 0x02000AEE RID: 2798
		public struct Parameters : IEquatable<SortRemoteGameProvider.Parameters>
		{
			// Token: 0x06004040 RID: 16448 RVA: 0x001096C9 File Offset: 0x001078C9
			public bool Equals(SortRemoteGameProvider.Parameters other)
			{
				return this.sorterIndex == other.sorterIndex && this.ascending == other.ascending;
			}

			// Token: 0x06004041 RID: 16449 RVA: 0x001096EC File Offset: 0x001078EC
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is SortRemoteGameProvider.Parameters)
				{
					SortRemoteGameProvider.Parameters other = (SortRemoteGameProvider.Parameters)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06004042 RID: 16450 RVA: 0x00109718 File Offset: 0x00107918
			public override int GetHashCode()
			{
				return this.sorterIndex * 397 ^ this.ascending.GetHashCode();
			}

			// Token: 0x04003E8D RID: 16013
			public int sorterIndex;

			// Token: 0x04003E8E RID: 16014
			public bool ascending;
		}

		// Token: 0x02000AEF RID: 2799
		public class Sorter
		{
			// Token: 0x04003E8F RID: 16015
			public string nameToken;

			// Token: 0x04003E90 RID: 16016
			public SortRemoteGameProvider.RemoteGameProviderComparison comparer;
		}
	}
}
