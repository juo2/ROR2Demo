using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Facepunch.Steamworks.Interop;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200015E RID: 350
	public class BaseSteamworks : IDisposable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0003358E File Offset: 0x0003178E
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x00033596 File Offset: 0x00031796
		public uint AppId { get; internal set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0003359F File Offset: 0x0003179F
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x000335A7 File Offset: 0x000317A7
		public Networking Networking { get; internal set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000335B0 File Offset: 0x000317B0
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x000335B8 File Offset: 0x000317B8
		public Inventory Inventory { get; internal set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000335C1 File Offset: 0x000317C1
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x000335C9 File Offset: 0x000317C9
		public Workshop Workshop { get; internal set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060009FC RID: 2556 RVA: 0x000335D4 File Offset: 0x000317D4
		// (remove) Token: 0x060009FD RID: 2557 RVA: 0x0003360C File Offset: 0x0003180C
		internal event Action OnUpdate;

		// Token: 0x060009FE RID: 2558 RVA: 0x00033644 File Offset: 0x00031844
		protected BaseSteamworks(uint appId)
		{
			this.AppId = appId;
			Environment.SetEnvironmentVariable("SteamAppId", this.AppId.ToString());
			Environment.SetEnvironmentVariable("SteamGameId", this.AppId.ToString());
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000336B0 File Offset: 0x000318B0
		~BaseSteamworks()
		{
			this.Dispose();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000336DC File Offset: 0x000318DC
		public virtual void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.Callbacks.Clear();
			foreach (CallbackHandle callbackHandle in this.CallbackHandles)
			{
				callbackHandle.Dispose();
			}
			this.CallbackHandles.Clear();
			foreach (CallResult callResult in this.CallResults)
			{
				callResult.Dispose();
			}
			this.CallResults.Clear();
			if (this.Workshop != null)
			{
				this.Workshop.Dispose();
				this.Workshop = null;
			}
			if (this.Inventory != null)
			{
				this.Inventory.Dispose();
				this.Inventory = null;
			}
			if (this.Networking != null)
			{
				this.Networking.Dispose();
				this.Networking = null;
			}
			if (this.native != null)
			{
				this.native.Dispose();
				this.native = null;
			}
			Environment.SetEnvironmentVariable("SteamAppId", null);
			Environment.SetEnvironmentVariable("SteamGameId", null);
			this.disposed = true;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0003381C File Offset: 0x00031A1C
		protected void SetupCommonInterfaces()
		{
			this.Networking = new Networking(this, this.native.networking);
			this.Inventory = new Inventory(this, this.native.inventory, this.IsGameServer);
			this.Workshop = new Workshop(this, this.native.ugc, this.native.remoteStorage);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0003387F File Offset: 0x00031A7F
		public bool IsValid
		{
			get
			{
				return this.native != null;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0003388A File Offset: 0x00031A8A
		internal virtual bool IsGameServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0003388D File Offset: 0x00031A8D
		internal void RegisterCallbackHandle(CallbackHandle handle)
		{
			this.CallbackHandles.Add(handle);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0003389B File Offset: 0x00031A9B
		internal void RegisterCallResult(CallResult handle)
		{
			this.CallResults.Add(handle);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000338A9 File Offset: 0x00031AA9
		internal void UnregisterCallResult(CallResult handle)
		{
			this.CallResults.Remove(handle);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000338B8 File Offset: 0x00031AB8
		public virtual void Update()
		{
			this.Networking.Update();
			this.RunUpdateCallbacks();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000338CC File Offset: 0x00031ACC
		public void RunUpdateCallbacks()
		{
			if (this.OnUpdate != null)
			{
				this.OnUpdate();
			}
			for (int i = 0; i < this.CallResults.Count; i++)
			{
				this.CallResults[i].Try();
			}
			SourceServerQuery.Cycle();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00033918 File Offset: 0x00031B18
		public void UpdateWhile(Func<bool> func)
		{
			while (func())
			{
				this.Update();
				Task.Delay(1).Wait();
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00033938 File Offset: 0x00031B38
		internal List<Action<object>> CallbackList(Type T)
		{
			List<Action<object>> list = null;
			if (!this.Callbacks.TryGetValue(T, out list))
			{
				list = new List<Action<object>>();
				this.Callbacks[T] = list;
			}
			return list;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0003396C File Offset: 0x00031B6C
		internal void OnCallback<T>(T data)
		{
			foreach (Action<object> action in this.CallbackList(typeof(T)))
			{
				action(data);
			}
			if (this.OnAnyCallback != null)
			{
				this.OnAnyCallback(data);
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000339E8 File Offset: 0x00031BE8
		internal void RegisterCallback<T>(Action<T> func)
		{
			this.CallbackList(typeof(T)).Add(delegate(object o)
			{
				func((T)((object)o));
			});
		}

		// Token: 0x040007B5 RID: 1973
		internal NativeInterface native;

		// Token: 0x040007B6 RID: 1974
		private List<CallbackHandle> CallbackHandles = new List<CallbackHandle>();

		// Token: 0x040007B7 RID: 1975
		private List<CallResult> CallResults = new List<CallResult>();

		// Token: 0x040007B8 RID: 1976
		protected bool disposed;

		// Token: 0x040007B9 RID: 1977
		public Action<object> OnAnyCallback;

		// Token: 0x040007BA RID: 1978
		private Dictionary<Type, List<Action<object>>> Callbacks = new Dictionary<Type, List<Action<object>>>();
	}
}
