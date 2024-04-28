using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D9A RID: 3482
	[RequireComponent(typeof(TMP_InputField))]
	public class SubmitInputFieldOnEnterKey : MonoBehaviour
	{
		// Token: 0x06004FB1 RID: 20401 RVA: 0x00149CDF File Offset: 0x00147EDF
		private void Awake()
		{
			this.inputField = base.GetComponent<TMP_InputField>();
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00149CED File Offset: 0x00147EED
		private void Update()
		{
			if (this.inputField.isFocused && this.inputField.text != "")
			{
				Input.GetKeyDown(KeyCode.Return);
			}
		}

		// Token: 0x04004C4F RID: 19535
		private TMP_InputField inputField;
	}
}
