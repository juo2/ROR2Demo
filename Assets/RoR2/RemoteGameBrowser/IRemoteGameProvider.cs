using System;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AD8 RID: 2776
	public interface IRemoteGameProvider
	{
		// Token: 0x06003FC6 RID: 16326
		bool RequestRefresh();

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06003FC7 RID: 16327
		// (remove) Token: 0x06003FC8 RID: 16328
		event Action onNewInfoAvailable;

		// Token: 0x06003FC9 RID: 16329
		RemoteGameInfo[] GetKnownGames();

		// Token: 0x06003FCA RID: 16330
		bool IsBusy();
	}
}
