using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D24 RID: 3364
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class InputSourceFilter : MonoBehaviour
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x0013C6F8 File Offset: 0x0013A8F8
		protected MPEventSystem eventSystem
		{
			get
			{
				MPEventSystemLocator mpeventSystemLocator = this.eventSystemLocator;
				if (mpeventSystemLocator == null)
				{
					return null;
				}
				return mpeventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0013C70B File Offset: 0x0013A90B
		private void Start()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.Refresh(true);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0013C720 File Offset: 0x0013A920
		private void Update()
		{
			this.Refresh(false);
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0013C72C File Offset: 0x0013A92C
		private void Refresh(bool forceRefresh = false)
		{
			MPEventSystem eventSystem = this.eventSystem;
			MPEventSystem.InputSource? inputSource = (eventSystem != null) ? new MPEventSystem.InputSource?(eventSystem.currentInputSource) : null;
			MPEventSystem.InputSource inputSource2 = this.requiredInputSource;
			bool flag = inputSource.GetValueOrDefault() == inputSource2 & inputSource != null;
			if (flag != this.wasOn || forceRefresh)
			{
				for (int i = 0; i < this.objectsToFilter.Length; i++)
				{
					this.objectsToFilter[i].SetActive(flag);
				}
			}
			this.wasOn = flag;
		}

		// Token: 0x040049AD RID: 18861
		public MPEventSystem.InputSource requiredInputSource;

		// Token: 0x040049AE RID: 18862
		public GameObject[] objectsToFilter;

		// Token: 0x040049AF RID: 18863
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040049B0 RID: 18864
		private bool wasOn;
	}
}
