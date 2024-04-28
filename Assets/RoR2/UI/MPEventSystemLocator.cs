using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D45 RID: 3397
	public class MPEventSystemLocator : MonoBehaviour
	{
		// Token: 0x06004DB0 RID: 19888 RVA: 0x001405CD File Offset: 0x0013E7CD
		private void Awake()
		{
			this.eventSystemProvider = base.GetComponentInParent<MPEventSystemProvider>();
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x001405DB File Offset: 0x0013E7DB
		private void OnDestroy()
		{
			this.eventSystemProvider = null;
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x001405E4 File Offset: 0x0013E7E4
		// (set) Token: 0x06004DB3 RID: 19891 RVA: 0x001405EC File Offset: 0x0013E7EC
		public MPEventSystemProvider eventSystemProvider
		{
			get
			{
				return this._eventSystemProvider;
			}
			internal set
			{
				if (this._eventSystemProvider == value)
				{
					return;
				}
				MPEventSystemProvider eventSystemProvider = this._eventSystemProvider;
				if (eventSystemProvider != null)
				{
					eventSystemProvider.RemoveListener(this);
				}
				this._eventSystemProvider = value;
				MPEventSystemProvider eventSystemProvider2 = this._eventSystemProvider;
				if (eventSystemProvider2 == null)
				{
					return;
				}
				eventSystemProvider2.AddListener(this);
			}
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x00140622 File Offset: 0x0013E822
		private void OnEventSystemDiscovered(MPEventSystem discoveredEventSystem)
		{
			Action<MPEventSystem> action = this.onEventSystemDiscovered;
			if (action == null)
			{
				return;
			}
			action(discoveredEventSystem);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x00140635 File Offset: 0x0013E835
		private void OnEventSystemLost(MPEventSystem lostEventSystem)
		{
			Action<MPEventSystem> action = this.onEventSystemLost;
			if (action == null)
			{
				return;
			}
			action(lostEventSystem);
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x00140648 File Offset: 0x0013E848
		internal void OnProviderDestroyed(MPEventSystemProvider destroyedEventSystemProvider)
		{
			if (destroyedEventSystemProvider == this.eventSystemProvider)
			{
				this.eventSystemProvider = null;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x0014065A File Offset: 0x0013E85A
		// (set) Token: 0x06004DB8 RID: 19896 RVA: 0x00140662 File Offset: 0x0013E862
		public MPEventSystem eventSystem
		{
			get
			{
				return this._eventSystem;
			}
			internal set
			{
				if (this._eventSystem == value)
				{
					return;
				}
				if (this._eventSystem != null)
				{
					this.OnEventSystemLost(this._eventSystem);
				}
				this._eventSystem = value;
				if (this._eventSystem != null)
				{
					this.OnEventSystemDiscovered(this._eventSystem);
				}
			}
		}

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06004DB9 RID: 19897 RVA: 0x001406A0 File Offset: 0x0013E8A0
		// (remove) Token: 0x06004DBA RID: 19898 RVA: 0x001406D8 File Offset: 0x0013E8D8
		public event Action<MPEventSystem> onEventSystemDiscovered;

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x06004DBB RID: 19899 RVA: 0x00140710 File Offset: 0x0013E910
		// (remove) Token: 0x06004DBC RID: 19900 RVA: 0x00140748 File Offset: 0x0013E948
		public event Action<MPEventSystem> onEventSystemLost;

		// Token: 0x04004A83 RID: 19075
		private MPEventSystemProvider _eventSystemProvider;

		// Token: 0x04004A84 RID: 19076
		private MPEventSystem _eventSystem;
	}
}
