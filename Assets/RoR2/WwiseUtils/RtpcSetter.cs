using System;
using UnityEngine;

namespace RoR2.WwiseUtils
{
	// Token: 0x02000AAE RID: 2734
	public struct RtpcSetter
	{
		// Token: 0x06003ED5 RID: 16085 RVA: 0x00103271 File Offset: 0x00101471
		public RtpcSetter(string name, GameObject gameObject = null)
		{
			this.name = name;
			this.id = AkSoundEngine.GetIDFromString(name);
			this.gameObject = gameObject;
			this.expectedEngineValue = float.NegativeInfinity;
			this.value = this.expectedEngineValue;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x001032A4 File Offset: 0x001014A4
		public void FlushIfChanged()
		{
			if (!this.expectedEngineValue.Equals(this.value))
			{
				this.expectedEngineValue = this.value;
				AkSoundEngine.SetRTPCValue(this.id, this.value, this.gameObject);
			}
		}

		// Token: 0x04003D1D RID: 15645
		private readonly string name;

		// Token: 0x04003D1E RID: 15646
		private readonly uint id;

		// Token: 0x04003D1F RID: 15647
		private readonly GameObject gameObject;

		// Token: 0x04003D20 RID: 15648
		private float expectedEngineValue;

		// Token: 0x04003D21 RID: 15649
		public float value;
	}
}
