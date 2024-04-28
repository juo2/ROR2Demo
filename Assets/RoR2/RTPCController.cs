using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000839 RID: 2105
	public class RTPCController : MonoBehaviour
	{
		// Token: 0x06002DEE RID: 11758 RVA: 0x000C36B0 File Offset: 0x000C18B0
		private void Start()
		{
			if (this.akSoundString.Length > 0)
			{
				Util.PlaySound(this.akSoundString, base.gameObject, this.rtpcString, this.rtpcValue);
				return;
			}
			AkSoundEngine.SetRTPCValue(this.rtpcString, this.rtpcValue, base.gameObject);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000C3702 File Offset: 0x000C1902
		private void FixedUpdate()
		{
			if (this.useCurveInstead)
			{
				this.fixedAge += Time.fixedDeltaTime;
				AkSoundEngine.SetRTPCValue(this.rtpcString, this.rtpcValueCurve.Evaluate(this.fixedAge), base.gameObject);
			}
		}

		// Token: 0x04002FD4 RID: 12244
		public string akSoundString;

		// Token: 0x04002FD5 RID: 12245
		public string rtpcString;

		// Token: 0x04002FD6 RID: 12246
		public float rtpcValue;

		// Token: 0x04002FD7 RID: 12247
		public bool useCurveInstead;

		// Token: 0x04002FD8 RID: 12248
		public AnimationCurve rtpcValueCurve;

		// Token: 0x04002FD9 RID: 12249
		private float fixedAge;
	}
}
