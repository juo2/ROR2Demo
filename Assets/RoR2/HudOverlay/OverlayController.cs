using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.HudOverlay
{
	// Token: 0x02000BF2 RID: 3058
	public class OverlayController
	{
		// Token: 0x140000DD RID: 221
		// (add) Token: 0x0600456B RID: 17771 RVA: 0x00120D8C File Offset: 0x0011EF8C
		// (remove) Token: 0x0600456C RID: 17772 RVA: 0x00120DC4 File Offset: 0x0011EFC4
		public event Action<OverlayController, GameObject> onInstanceAdded;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x0600456D RID: 17773 RVA: 0x00120DFC File Offset: 0x0011EFFC
		// (remove) Token: 0x0600456E RID: 17774 RVA: 0x00120E34 File Offset: 0x0011F034
		public event Action<OverlayController, GameObject> onInstanceRemove;

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600456F RID: 17775 RVA: 0x00120E69 File Offset: 0x0011F069
		public IReadOnlyList<GameObject> instancesList
		{
			get
			{
				return this._instancesList;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x00120E71 File Offset: 0x0011F071
		// (set) Token: 0x06004571 RID: 17777 RVA: 0x00120E7C File Offset: 0x0011F07C
		public bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				if (this._active == value)
				{
					return;
				}
				this._active = value;
				foreach (GameObject gameObject in this._instancesList)
				{
					gameObject.SetActive(this._active);
				}
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x00120EE4 File Offset: 0x0011F0E4
		// (set) Token: 0x06004573 RID: 17779 RVA: 0x00120EEC File Offset: 0x0011F0EC
		public float alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				if (this._alpha.Equals(value))
				{
					return;
				}
				this._alpha = value;
				foreach (GameObject instance in this._instancesList)
				{
					this.PushAlphaToInstance(instance);
				}
			}
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00120F58 File Offset: 0x0011F158
		public OverlayController(TargetTracker owner, OverlayCreationParams creationParams)
		{
			this.owner = owner;
			this.creationParams = creationParams;
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x00120F8C File Offset: 0x0011F18C
		public void OnInstanceAdded(GameObject instance)
		{
			this._instancesList.Add(instance);
			try
			{
				Action<OverlayController, GameObject> action = this.onInstanceAdded;
				if (action != null)
				{
					action(this, instance);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			instance.SetActive(this.active);
			this.PushAlphaToInstance(instance);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x00120FE4 File Offset: 0x0011F1E4
		public void OnInstanceRemoved(GameObject instance)
		{
			try
			{
				Action<OverlayController, GameObject> action = this.onInstanceRemove;
				if (action != null)
				{
					action(this, instance);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			this._instancesList.Remove(instance);
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x0012102C File Offset: 0x0011F22C
		private void PushAlphaToInstance(GameObject instance)
		{
			CanvasGroup component = instance.GetComponent<CanvasGroup>();
			if (component)
			{
				component.alpha = this.alpha;
			}
		}

		// Token: 0x0400439F RID: 17311
		public readonly TargetTracker owner;

		// Token: 0x040043A0 RID: 17312
		public readonly OverlayCreationParams creationParams;

		// Token: 0x040043A3 RID: 17315
		private readonly List<GameObject> _instancesList = new List<GameObject>();

		// Token: 0x040043A4 RID: 17316
		private bool _active = true;

		// Token: 0x040043A5 RID: 17317
		private float _alpha = 1f;
	}
}
