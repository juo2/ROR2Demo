using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D2F RID: 3375
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(HudElement))]
	public class LoaderHookCrosshairController : MonoBehaviour
	{
		// Token: 0x06004CFB RID: 19707 RVA: 0x0013DAA8 File Offset: 0x0013BCA8
		private void Awake()
		{
			this.hudElement = base.GetComponent<HudElement>();
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0013DAB6 File Offset: 0x0013BCB6
		private void SetAvailable(bool newIsAvailable)
		{
			if (this.isAvailable == newIsAvailable)
			{
				return;
			}
			this.isAvailable = newIsAvailable;
			(this.isAvailable ? this.onAvailable : this.onUnavailable).Invoke();
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0013DAE4 File Offset: 0x0013BCE4
		private void SetInRange(bool newInRange)
		{
			if (this.inRange == newInRange)
			{
				return;
			}
			this.inRange = newInRange;
			UnityEvent unityEvent = this.inRange ? this.onEnterRange : this.onExitRange;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0013DB18 File Offset: 0x0013BD18
		private void Update()
		{
			if (!this.hudElement.targetCharacterBody)
			{
				this.SetAvailable(false);
				this.SetInRange(false);
				return;
			}
			this.SetAvailable(this.hudElement.targetCharacterBody.skillLocator.secondary.CanExecute());
			bool flag = false;
			if (this.isAvailable)
			{
				RaycastHit raycastHit;
				flag = this.hudElement.targetCharacterBody.inputBank.GetAimRaycast(this.range, out raycastHit);
			}
			this.SetInRange(flag);
		}

		// Token: 0x040049FE RID: 18942
		public float range;

		// Token: 0x040049FF RID: 18943
		public UnityEvent onAvailable;

		// Token: 0x04004A00 RID: 18944
		public UnityEvent onUnavailable;

		// Token: 0x04004A01 RID: 18945
		public UnityEvent onEnterRange;

		// Token: 0x04004A02 RID: 18946
		public UnityEvent onExitRange;

		// Token: 0x04004A03 RID: 18947
		private HudElement hudElement;

		// Token: 0x04004A04 RID: 18948
		private bool isAvailable;

		// Token: 0x04004A05 RID: 18949
		private bool inRange;
	}
}
