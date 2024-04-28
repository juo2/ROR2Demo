using System;
using UnityEngine;

namespace RoR2.WwiseUtils
{
	// Token: 0x02000AAD RID: 2733
	public class RtpcSetBehavior : MonoBehaviour
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x00103216 File Offset: 0x00101416
		// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x00103223 File Offset: 0x00101423
		public float value
		{
			get
			{
				return this.rtpcSetter.value;
			}
			set
			{
				this.rtpcSetter.value = value;
				this.rtpcSetter.FlushIfChanged();
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x0010323C File Offset: 0x0010143C
		private void Start()
		{
			this.rtpcSetter = new RtpcSetter(this.valueName, base.gameObject);
			this.rtpcSetter.value = this.initialValue;
			this.rtpcSetter.FlushIfChanged();
		}

		// Token: 0x04003D1A RID: 15642
		[SerializeField]
		private string valueName;

		// Token: 0x04003D1B RID: 15643
		[SerializeField]
		private float initialValue;

		// Token: 0x04003D1C RID: 15644
		private RtpcSetter rtpcSetter;
	}
}
