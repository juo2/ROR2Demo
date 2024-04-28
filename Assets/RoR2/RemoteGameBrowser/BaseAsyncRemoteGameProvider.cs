using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AD0 RID: 2768
	public abstract class BaseAsyncRemoteGameProvider : IRemoteGameProvider, IDisposable
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x00106B66 File Offset: 0x00104D66
		// (set) Token: 0x06003F9D RID: 16285 RVA: 0x00106B6E File Offset: 0x00104D6E
		private protected bool disposed { protected get; private set; }

		// Token: 0x06003F9E RID: 16286 RVA: 0x00106B77 File Offset: 0x00104D77
		public BaseAsyncRemoteGameProvider.SearchFilters GetSearchFilters()
		{
			return this.searchFilters;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00106B7F File Offset: 0x00104D7F
		public void SetSearchFilters(BaseAsyncRemoteGameProvider.SearchFilters newSearchFilters)
		{
			if (this.searchFilters.Equals(newSearchFilters))
			{
				return;
			}
			this.searchFilters = newSearchFilters;
			if (this.refreshOnFiltersChanged)
			{
				this.RequestRefresh();
			}
			this.SetDirty();
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x00106BAC File Offset: 0x00104DAC
		protected void SetDirty()
		{
			object obj = this.disposedLock;
			lock (obj)
			{
				if (!this.disposed)
				{
					object obj2 = this.isDirtyLock;
					lock (obj2)
					{
						if (!this.isDirty)
						{
							this.isDirty = true;
							RoR2Application.onNextUpdate += this.DirtyUpdate;
						}
					}
				}
			}
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x00106C3C File Offset: 0x00104E3C
		protected void DirtyUpdate()
		{
			object obj = this.disposedLock;
			lock (obj)
			{
				if (!this.disposed)
				{
					List<BaseAsyncRemoteGameProvider.TaskInfo> obj2 = this.activeTasks;
					lock (obj2)
					{
						if (this.activeTasks.Count < this.maxTasks)
						{
							object obj3 = this.isDirtyLock;
							lock (obj3)
							{
								this.isDirty = false;
								this.GenerateNewTask();
							}
						}
					}
				}
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00106CF8 File Offset: 0x00104EF8
		private void GenerateNewTask()
		{
			BaseAsyncRemoteGameProvider.<>c__DisplayClass20_0 CS$<>8__locals1 = new BaseAsyncRemoteGameProvider.<>c__DisplayClass20_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationTokenSource = new CancellationTokenSource();
			CS$<>8__locals1.cancellationToken = CS$<>8__locals1.cancellationTokenSource.Token;
			CS$<>8__locals1.innerTask = this.CreateTask(CS$<>8__locals1.cancellationToken);
			List<BaseAsyncRemoteGameProvider.TaskInfo> obj = this.activeTasks;
			lock (obj)
			{
				Task task = Task.Run(delegate()
				{
					BaseAsyncRemoteGameProvider.<>c__DisplayClass20_0.<<GenerateNewTask>b__0>d <<GenerateNewTask>b__0>d;
					<<GenerateNewTask>b__0>d.<>4__this = CS$<>8__locals1;
					<<GenerateNewTask>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<GenerateNewTask>b__0>d.<>1__state = -1;
					AsyncTaskMethodBuilder <>t__builder = <<GenerateNewTask>b__0>d.<>t__builder;
					<>t__builder.Start<BaseAsyncRemoteGameProvider.<>c__DisplayClass20_0.<<GenerateNewTask>b__0>d>(ref <<GenerateNewTask>b__0>d);
					return <<GenerateNewTask>b__0>d.<>t__builder.Task;
				}, CS$<>8__locals1.cancellationToken);
				List<BaseAsyncRemoteGameProvider.TaskInfo> list = this.activeTasks;
				BaseAsyncRemoteGameProvider.TaskInfo item = default(BaseAsyncRemoteGameProvider.TaskInfo);
				item.task = task;
				item.cancellationTokenSource = CS$<>8__locals1.cancellationTokenSource;
				int num = this.taskNumberProvider;
				this.taskNumberProvider = num + 1;
				item.taskNumber = num;
				list.Add(item);
			}
		}

		// Token: 0x06003FA3 RID: 16291
		protected abstract Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken);

		// Token: 0x06003FA4 RID: 16292 RVA: 0x00106DCC File Offset: 0x00104FCC
		protected void OnTaskComplete(BaseAsyncRemoteGameProvider.TaskInfo taskInfo)
		{
			List<BaseAsyncRemoteGameProvider.TaskInfo> obj = this.activeTasks;
			lock (obj)
			{
				for (int i = this.activeTasks.Count - 1; i >= 0; i--)
				{
					if (this.activeTasks[i].taskNumber < taskInfo.taskNumber)
					{
						this.activeTasks[i].Cancel();
					}
				}
			}
			Action action = this.onNewInfoAvailable;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x00106E5C File Offset: 0x0010505C
		public virtual void Dispose()
		{
			object obj = this.disposedLock;
			lock (obj)
			{
				this.disposed = true;
				List<BaseAsyncRemoteGameProvider.TaskInfo> obj2 = this.activeTasks;
				lock (obj2)
				{
					foreach (BaseAsyncRemoteGameProvider.TaskInfo taskInfo in this.activeTasks)
					{
						taskInfo.Cancel();
					}
				}
			}
		}

		// Token: 0x06003FA6 RID: 16294
		public abstract bool RequestRefresh();

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06003FA7 RID: 16295 RVA: 0x00106F08 File Offset: 0x00105108
		// (remove) Token: 0x06003FA8 RID: 16296 RVA: 0x00106F40 File Offset: 0x00105140
		public event Action onNewInfoAvailable;

		// Token: 0x06003FA9 RID: 16297 RVA: 0x00106F75 File Offset: 0x00105175
		public RemoteGameInfo[] GetKnownGames()
		{
			return this.gameInfos;
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00106F7D File Offset: 0x0010517D
		public virtual bool IsBusy()
		{
			return this.activeTasks.Count > 0;
		}

		// Token: 0x04003DF0 RID: 15856
		private RemoteGameInfo[] gameInfos = Array.Empty<RemoteGameInfo>();

		// Token: 0x04003DF1 RID: 15857
		private readonly object gameInfosLock = new object();

		// Token: 0x04003DF2 RID: 15858
		private readonly object isDirtyLock = new object();

		// Token: 0x04003DF3 RID: 15859
		private readonly object disposedLock = new object();

		// Token: 0x04003DF4 RID: 15860
		private readonly List<BaseAsyncRemoteGameProvider.TaskInfo> activeTasks = new List<BaseAsyncRemoteGameProvider.TaskInfo>();

		// Token: 0x04003DF5 RID: 15861
		private int taskNumberProvider;

		// Token: 0x04003DF6 RID: 15862
		protected int maxTasks = 2;

		// Token: 0x04003DF8 RID: 15864
		private bool isDirty;

		// Token: 0x04003DF9 RID: 15865
		public bool refreshOnFiltersChanged = true;

		// Token: 0x04003DFA RID: 15866
		protected BaseAsyncRemoteGameProvider.SearchFilters searchFilters = new BaseAsyncRemoteGameProvider.SearchFilters
		{
			allowMismatchedMods = false
		};

		// Token: 0x02000AD1 RID: 2769
		public struct SearchFilters : IEquatable<BaseAsyncRemoteGameProvider.SearchFilters>
		{
			// Token: 0x06003FAC RID: 16300 RVA: 0x00106FFF File Offset: 0x001051FF
			public bool Equals(BaseAsyncRemoteGameProvider.SearchFilters other)
			{
				return this.allowMismatchedMods == other.allowMismatchedMods;
			}

			// Token: 0x06003FAD RID: 16301 RVA: 0x00107010 File Offset: 0x00105210
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is BaseAsyncRemoteGameProvider.SearchFilters)
				{
					BaseAsyncRemoteGameProvider.SearchFilters other = (BaseAsyncRemoteGameProvider.SearchFilters)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06003FAE RID: 16302 RVA: 0x0010703C File Offset: 0x0010523C
			public override int GetHashCode()
			{
				return this.allowMismatchedMods.GetHashCode();
			}

			// Token: 0x04003DFC RID: 15868
			public bool allowMismatchedMods;
		}

		// Token: 0x02000AD2 RID: 2770
		protected struct TaskInfo
		{
			// Token: 0x06003FAF RID: 16303 RVA: 0x00107049 File Offset: 0x00105249
			public void Cancel()
			{
				this.cancellationTokenSource.Cancel();
			}

			// Token: 0x04003DFD RID: 15869
			public Task task;

			// Token: 0x04003DFE RID: 15870
			public CancellationTokenSource cancellationTokenSource;

			// Token: 0x04003DFF RID: 15871
			public int taskNumber;
		}
	}
}
