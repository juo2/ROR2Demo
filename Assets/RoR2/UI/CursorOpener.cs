using System;
using System.Collections.ObjectModel;
using HG;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CEF RID: 3311
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class CursorOpener : MonoBehaviour
	{
		// Token: 0x06004B60 RID: 19296 RVA: 0x001358D4 File Offset: 0x00133AD4
		private void CacheComponents()
		{
			MPEventSystemLocator component = base.GetComponent<MPEventSystemLocator>();
			if (component != this.eventSystemLocator)
			{
				if (this.eventSystemLocator != null)
				{
					this.eventSystemLocator.onEventSystemDiscovered -= this.OnEventSystemDiscovered;
					this.eventSystemLocator.onEventSystemLost -= this.OnEventSystemLost;
					if (this.eventSystemLocator.eventSystem != null)
					{
						this.OnEventSystemLost(this.eventSystemLocator.eventSystem);
					}
				}
				this.eventSystemLocator = component;
				if (this.eventSystemLocator != null)
				{
					this.eventSystemLocator.onEventSystemDiscovered += this.OnEventSystemDiscovered;
					this.eventSystemLocator.onEventSystemLost += this.OnEventSystemLost;
					if (this.eventSystemLocator.eventSystem != null)
					{
						this.OnEventSystemDiscovered(this.eventSystemLocator.eventSystem);
					}
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x001359A3 File Offset: 0x00133BA3
		// (set) Token: 0x06004B62 RID: 19298 RVA: 0x001359AB File Offset: 0x00133BAB
		protected bool opening
		{
			get
			{
				return this._opening;
			}
			set
			{
				if (this._opening == value)
				{
					return;
				}
				this._opening = value;
				this.RebuildLinks();
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x001359C4 File Offset: 0x00133BC4
		// (set) Token: 0x06004B64 RID: 19300 RVA: 0x001359CC File Offset: 0x00133BCC
		public bool forceCursorForGamePad
		{
			get
			{
				return this._forceCursorForGamepad;
			}
			set
			{
				if (this._forceCursorForGamepad == value)
				{
					return;
				}
				if (this.linkedEventSystemCount > 0)
				{
					this.ClearLinkedEventSystems();
				}
				this._forceCursorForGamepad = value;
				this.RebuildLinks();
			}
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x001359F4 File Offset: 0x00133BF4
		protected void ClearLinkedEventSystems()
		{
			this.SetLinkedEventSystems(Array.Empty<MPEventSystem>(), 0);
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x00135A04 File Offset: 0x00133C04
		protected void SetLinkedEventSystems(MPEventSystem[] newLinkedEventSystems, int newLinkedEventSystemCount)
		{
			for (int i = this.linkedEventSystemCount - 1; i >= 0; i--)
			{
				ref MPEventSystem ptr = ref this.linkedEventSystems[i];
				MPEventSystem mpeventSystem = ptr;
				int num = mpeventSystem.cursorOpenerCount - 1;
				mpeventSystem.cursorOpenerCount = num;
				if (this._forceCursorForGamepad)
				{
					MPEventSystem mpeventSystem2 = ptr;
					num = mpeventSystem2.cursorOpenerForGamepadCount - 1;
					mpeventSystem2.cursorOpenerForGamepadCount = num;
				}
				ptr = null;
			}
			ArrayUtils.EnsureCapacity<MPEventSystem>(ref this.linkedEventSystems, newLinkedEventSystemCount);
			for (int j = 0; j < newLinkedEventSystemCount; j++)
			{
				ref MPEventSystem ptr2 = ref this.linkedEventSystems[j];
				ptr2 = newLinkedEventSystems[j];
				MPEventSystem mpeventSystem3 = ptr2;
				int num = mpeventSystem3.cursorOpenerCount + 1;
				mpeventSystem3.cursorOpenerCount = num;
				if (this._forceCursorForGamepad)
				{
					MPEventSystem mpeventSystem4 = ptr2;
					num = mpeventSystem4.cursorOpenerForGamepadCount + 1;
					mpeventSystem4.cursorOpenerForGamepadCount = num;
				}
			}
			this.linkedEventSystemCount = newLinkedEventSystemCount;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00135AC0 File Offset: 0x00133CC0
		protected void RebuildLinks()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			bool fallBackToMainEventSystem = this.eventSystemLocator.eventSystemProvider.fallBackToMainEventSystem;
			int newLinkedEventSystemCount = 0;
			if (this.opening)
			{
				if (fallBackToMainEventSystem)
				{
					ReadOnlyCollection<MPEventSystem> readOnlyInstancesList = MPEventSystem.readOnlyInstancesList;
					ArrayUtils.EnsureCapacity<MPEventSystem>(ref CursorOpener.buffer, readOnlyInstancesList.Count);
					for (int i = 0; i < readOnlyInstancesList.Count; i++)
					{
						CursorOpener.buffer[i] = readOnlyInstancesList[i];
					}
					newLinkedEventSystemCount = readOnlyInstancesList.Count;
				}
				else if (this.eventSystemLocator.eventSystem)
				{
					CursorOpener.buffer[0] = eventSystem;
					newLinkedEventSystemCount = 1;
				}
			}
			this.SetLinkedEventSystems(CursorOpener.buffer, newLinkedEventSystemCount);
			ArrayUtils.Clear<MPEventSystem>(CursorOpener.buffer, ref newLinkedEventSystemCount);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00135B6F File Offset: 0x00133D6F
		private void OnEventSystemDiscovered(MPEventSystem discoveredEventSystem)
		{
			if (this.opening)
			{
				this.RebuildLinks();
			}
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00135B6F File Offset: 0x00133D6F
		private void OnEventSystemLost(MPEventSystem lostEventSystem)
		{
			if (this.opening)
			{
				this.RebuildLinks();
			}
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x00135B7F File Offset: 0x00133D7F
		protected void Awake()
		{
			this.CacheComponents();
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x00135B87 File Offset: 0x00133D87
		protected void OnEnable()
		{
			this.opening = true;
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00135B90 File Offset: 0x00133D90
		protected void OnDisable()
		{
			this.opening = false;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00135B99 File Offset: 0x00133D99
		[AssetCheck(typeof(CursorOpener))]
		private static void CheckCursorOpener(AssetCheckArgs args)
		{
			if (!((CursorOpener)args.asset).GetComponent<MPEventSystemLocator>())
			{
				args.Log("Missing MPEventSystemLocator.", null);
			}
		}

		// Token: 0x0400480E RID: 18446
		[Tooltip("If enabled, the cursor will be shown even if the user is on a gamepad.")]
		[SerializeField]
		private bool _forceCursorForGamepad;

		// Token: 0x0400480F RID: 18447
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004810 RID: 18448
		private static MPEventSystem[] buffer = new MPEventSystem[8];

		// Token: 0x04004811 RID: 18449
		private bool _opening;

		// Token: 0x04004812 RID: 18450
		protected int linkedEventSystemCount;

		// Token: 0x04004813 RID: 18451
		protected MPEventSystem[] linkedEventSystems = Array.Empty<MPEventSystem>();
	}
}
