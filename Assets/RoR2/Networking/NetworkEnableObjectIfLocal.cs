using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C36 RID: 3126
	public class NetworkEnableObjectIfLocal : NetworkBehaviour
	{
		// Token: 0x060046AE RID: 18094 RVA: 0x00124823 File Offset: 0x00122A23
		private void Start()
		{
			if (this.target)
			{
				this.target.SetActive(base.hasAuthority);
			}
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x00124843 File Offset: 0x00122A43
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			if (this.target)
			{
				this.target.SetActive(true);
			}
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x00124864 File Offset: 0x00122A64
		public override void OnStopAuthority()
		{
			if (this.target)
			{
				this.target.SetActive(false);
			}
			base.OnStopAuthority();
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x00124888 File Offset: 0x00122A88
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04004488 RID: 17544
		[Tooltip("The GameObject to enable/disable.")]
		public GameObject target;
	}
}
