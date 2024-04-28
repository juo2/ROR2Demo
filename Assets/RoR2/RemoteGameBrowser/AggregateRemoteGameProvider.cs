using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HG;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000ACE RID: 2766
	public class AggregateRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x06003F8D RID: 16269 RVA: 0x001068A4 File Offset: 0x00104AA4
		public override void Dispose()
		{
			for (int i = this.providers.Length - 1; i >= 0; i--)
			{
				this.OnProviderLost(this.providers[i]);
			}
			this.providers = Array.Empty<IRemoteGameProvider>();
			base.Dispose();
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x001068E8 File Offset: 0x00104AE8
		public override bool RequestRefresh()
		{
			IRemoteGameProvider[] array = this.providers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RequestRefresh();
			}
			return true;
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00106914 File Offset: 0x00104B14
		public void AddProvider(IRemoteGameProvider provider)
		{
			ArrayUtils.ArrayAppend<IRemoteGameProvider>(ref this.providers, provider);
			this.OnProviderDiscovered(provider);
			base.SetDirty();
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x00106930 File Offset: 0x00104B30
		public void RemoveProvider(IRemoteGameProvider provider)
		{
			int num = Array.IndexOf<IRemoteGameProvider>(this.providers, provider);
			if (num == -1)
			{
				return;
			}
			this.RemoveProviderAt(num);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x00106958 File Offset: 0x00104B58
		public void SetProviderAdded(IRemoteGameProvider provider, bool shouldUse)
		{
			int num = Array.IndexOf<IRemoteGameProvider>(this.providers, provider);
			if (num == -1)
			{
				if (shouldUse)
				{
					this.AddProvider(provider);
					return;
				}
			}
			else if (!shouldUse)
			{
				this.RemoveProviderAt(num);
			}
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x0010698B File Offset: 0x00104B8B
		private void RemoveProviderAt(int index)
		{
			this.OnProviderLost(this.providers[index]);
			ArrayUtils.ArrayRemoveAtAndResize<IRemoteGameProvider>(ref this.providers, index, 1);
			base.SetDirty();
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x001069AE File Offset: 0x00104BAE
		private void OnProviderDiscovered(IRemoteGameProvider provider)
		{
			provider.onNewInfoAvailable += this.OnProviderNewInfoAvailable;
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x001069C2 File Offset: 0x00104BC2
		private void OnProviderLost(IRemoteGameProvider provider)
		{
			provider.onNewInfoAvailable -= this.OnProviderNewInfoAvailable;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x001069D6 File Offset: 0x00104BD6
		private void OnProviderNewInfoAvailable()
		{
			base.SetDirty();
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x001069DE File Offset: 0x00104BDE
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			IList<IRemoteGameProvider> providers = this.providers;
			return new Task<RemoteGameInfo[]>(delegate()
			{
				cancellationToken.ThrowIfCancellationRequested();
				IEnumerable<RemoteGameInfo>[] array = new IEnumerable<RemoteGameInfo>[providers.Count];
				for (int i = 0; i < providers.Count; i++)
				{
					array[i] = providers[i].GetKnownGames();
				}
				cancellationToken.ThrowIfCancellationRequested();
				int num = 0;
				for (int j = 0; j < providers.Count; j++)
				{
					num += array[j].Count<RemoteGameInfo>();
				}
				cancellationToken.ThrowIfCancellationRequested();
				RemoteGameInfo[] array2 = new RemoteGameInfo[num];
				int k = 0;
				int num2 = 0;
				while (k < providers.Count)
				{
					cancellationToken.ThrowIfCancellationRequested();
					foreach (RemoteGameInfo remoteGameInfo in providers[k].GetKnownGames())
					{
						array2[num2++] = remoteGameInfo;
					}
					k++;
				}
				return array2;
			});
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x00106A08 File Offset: 0x00104C08
		public override bool IsBusy()
		{
			return base.IsBusy() || this.<IsBusy>g__IsAnyProviderBusy|11_0();
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x00106A30 File Offset: 0x00104C30
		[CompilerGenerated]
		private bool <IsBusy>g__IsAnyProviderBusy|11_0()
		{
			for (int i = 0; i < this.providers.Length; i++)
			{
				if (this.providers[i].IsBusy())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003DED RID: 15853
		private IRemoteGameProvider[] providers = Array.Empty<IRemoteGameProvider>();
	}
}
