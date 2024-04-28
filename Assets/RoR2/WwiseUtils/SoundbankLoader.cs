using System;
using UnityEngine;

namespace RoR2.WwiseUtils
{
	// Token: 0x02000AAF RID: 2735
	public class SoundbankLoader : MonoBehaviour
	{
		// Token: 0x06003ED7 RID: 16087 RVA: 0x001032E0 File Offset: 0x001014E0
		private void Start()
		{
			for (int i = 0; i < this.soundbankStrings.Length; i++)
			{
				AkBankManager.LoadBank(this.soundbankStrings[i], this.decodeBank, this.saveDecodedBank);
			}
		}

		// Token: 0x04003D22 RID: 15650
		public string[] soundbankStrings;

		// Token: 0x04003D23 RID: 15651
		public bool decodeBank;

		// Token: 0x04003D24 RID: 15652
		public bool saveDecodedBank;
	}
}
