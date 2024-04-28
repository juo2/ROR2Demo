using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AD9 RID: 2777
	public class PageRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x06003FCB RID: 16331 RVA: 0x001079BF File Offset: 0x00105BBF
		public PageRemoteGameProvider([NotNull] IRemoteGameProvider source)
		{
			this.source = source;
			source.onNewInfoAvailable += this.OnSourceNewInfoAvailable;
			this.maxTasks = 1;
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x001079EE File Offset: 0x00105BEE
		public override void Dispose()
		{
			this.source.onNewInfoAvailable -= this.OnSourceNewInfoAvailable;
			base.Dispose();
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x001069D6 File Offset: 0x00104BD6
		private void OnSourceNewInfoAvailable()
		{
			base.SetDirty();
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x00107A0D File Offset: 0x00105C0D
		public void SetGamesPerPage(int newGamesPerPage)
		{
			if (newGamesPerPage < 1)
			{
				newGamesPerPage = 1;
			}
			if (newGamesPerPage == this.gamesPerPage)
			{
				return;
			}
			this.gamesPerPage = newGamesPerPage;
			base.SetDirty();
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x00107A2D File Offset: 0x00105C2D
		public bool CanGoToNextPage()
		{
			return this.pageIndex + 1 < this.maxPages;
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x00107A3F File Offset: 0x00105C3F
		public bool CanGoToPreviousPage()
		{
			return this.pageIndex - 1 >= 0;
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x00107A50 File Offset: 0x00105C50
		public bool GoToNextPage()
		{
			bool result;
			lock (this)
			{
				if (!this.CanGoToNextPage())
				{
					result = false;
				}
				else
				{
					this.pageIndex++;
					base.SetDirty();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x00107AA8 File Offset: 0x00105CA8
		public bool GoToPreviousPage()
		{
			bool result;
			lock (this)
			{
				if (!this.CanGoToPreviousPage())
				{
					result = false;
				}
				else
				{
					this.pageIndex--;
					base.SetDirty();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x00107B00 File Offset: 0x00105D00
		public void GetCurrentPageInfo(out int pageIndex, out int maxPages)
		{
			lock (this)
			{
				pageIndex = this.pageIndex;
				maxPages = this.maxPages;
			}
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x00107B48 File Offset: 0x00105D48
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			return new Task<RemoteGameInfo[]>(delegate()
			{
				RemoteGameInfo[] knownGames = this.source.GetKnownGames();
				RemoteGameInfo[] array;
				lock (this)
				{
					this.maxPages = (knownGames.Length + this.gamesPerPage - 1) / this.gamesPerPage;
					this.pageIndex = Math.Max(Math.Min(this.pageIndex, this.maxPages - 1), 0);
					int num = Math.Min(this.gamesPerPage * this.pageIndex, knownGames.Length);
					int num2 = Mathf.Min(num + this.gamesPerPage, knownGames.Length);
					if (num2 == num)
					{
						return Array.Empty<RemoteGameInfo>();
					}
					array = new RemoteGameInfo[num2 - num];
					int i = num;
					int num3 = 0;
					while (i < num2)
					{
						array[num3++] = knownGames[i];
						i++;
					}
				}
				return array;
			});
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x00107B5B File Offset: 0x00105D5B
		public override bool RequestRefresh()
		{
			return this.source.RequestRefresh();
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x00107B68 File Offset: 0x00105D68
		public override bool IsBusy()
		{
			if (!base.IsBusy())
			{
				IRemoteGameProvider remoteGameProvider = this.source;
				return remoteGameProvider != null && remoteGameProvider.IsBusy();
			}
			return true;
		}

		// Token: 0x04003E0D RID: 15885
		private int gamesPerPage = 1;

		// Token: 0x04003E0E RID: 15886
		private int pageIndex;

		// Token: 0x04003E0F RID: 15887
		private int maxPages;

		// Token: 0x04003E10 RID: 15888
		private readonly IRemoteGameProvider source;
	}
}
