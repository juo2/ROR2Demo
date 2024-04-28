using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000D46 RID: 3398
	public class MPEventSystemProvider : MonoBehaviour
	{
		// Token: 0x06004DBE RID: 19902 RVA: 0x0014077D File Offset: 0x0013E97D
		private void Awake()
		{
			this.ResolveEventSystem();
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x0014077D File Offset: 0x0013E97D
		private void OnEnable()
		{
			this.ResolveEventSystem();
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x00140785 File Offset: 0x0013E985
		private void OnDisable()
		{
			this.resolvedEventSystem = null;
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x00140790 File Offset: 0x0013E990
		private void OnDestroy()
		{
			for (int i = this.listeners.Count - 1; i >= 0; i--)
			{
				this.listeners[i].OnProviderDestroyed(this);
			}
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x0014077D File Offset: 0x0013E97D
		private void Update()
		{
			this.ResolveEventSystem();
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x001407C7 File Offset: 0x0013E9C7
		// (set) Token: 0x06004DC4 RID: 19908 RVA: 0x001407CF File Offset: 0x0013E9CF
		public MPEventSystem eventSystem
		{
			get
			{
				return this._eventSystem;
			}
			set
			{
				if (this._eventSystem == value)
				{
					return;
				}
				this._eventSystem = value;
				this.ResolveEventSystem();
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x001407E8 File Offset: 0x0013E9E8
		// (set) Token: 0x06004DC6 RID: 19910 RVA: 0x001407F0 File Offset: 0x0013E9F0
		public MPEventSystem resolvedEventSystem
		{
			get
			{
				return this._resolvedEventSystem;
			}
			private set
			{
				if (this._resolvedEventSystem == value)
				{
					return;
				}
				this._resolvedEventSystem = value;
				for (int i = this.listeners.Count - 1; i >= 0; i--)
				{
					this.listeners[i].eventSystem = this._resolvedEventSystem;
				}
			}
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x0014083D File Offset: 0x0013EA3D
		private void ResolveEventSystem()
		{
			if (this.eventSystem)
			{
				this.resolvedEventSystem = this.eventSystem;
				return;
			}
			if (this.fallBackToMainEventSystem)
			{
				this.resolvedEventSystem = MPEventSystemManager.primaryEventSystem;
			}
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x0014086C File Offset: 0x0013EA6C
		internal void AddListener(MPEventSystemLocator listener)
		{
			this.listeners.Add(listener);
			listener.eventSystem = this.resolvedEventSystem;
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x00140886 File Offset: 0x0013EA86
		internal void RemoveListener(MPEventSystemLocator listener)
		{
			listener.eventSystem = null;
			this.listeners.Remove(listener);
		}

		// Token: 0x04004A87 RID: 19079
		[SerializeField]
		[FormerlySerializedAs("eventSystem")]
		private MPEventSystem _eventSystem;

		// Token: 0x04004A88 RID: 19080
		public bool fallBackToMainEventSystem = true;

		// Token: 0x04004A89 RID: 19081
		private MPEventSystem _resolvedEventSystem;

		// Token: 0x04004A8A RID: 19082
		private List<MPEventSystemLocator> listeners = new List<MPEventSystemLocator>();
	}
}
