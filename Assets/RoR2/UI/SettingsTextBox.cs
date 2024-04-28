using System;
using TMPro;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D84 RID: 3460
	public class SettingsTextBox : BaseSettingsControl
	{
		// Token: 0x06004F4A RID: 20298 RVA: 0x0014806A File Offset: 0x0014626A
		protected new void OnEnable()
		{
			base.OnEnable();
			TMP_InputField tmp_InputField = this.textbox;
			if (tmp_InputField == null)
			{
				return;
			}
			TMP_InputField.OnChangeEvent onValueChanged = tmp_InputField.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.AddListener(new UnityAction<string>(this.OnTextBoxValueChanged));
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x00148098 File Offset: 0x00146298
		protected void OnDisable()
		{
			TMP_InputField tmp_InputField = this.textbox;
			if (tmp_InputField == null)
			{
				return;
			}
			TMP_InputField.OnChangeEvent onValueChanged = tmp_InputField.onValueChanged;
			if (onValueChanged == null)
			{
				return;
			}
			onValueChanged.RemoveListener(new UnityAction<string>(this.OnTextBoxValueChanged));
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x001480C0 File Offset: 0x001462C0
		private void OnTextBoxValueChanged(string newValue)
		{
			base.SubmitSetting(newValue);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x001480CC File Offset: 0x001462CC
		protected override void OnUpdateControls()
		{
			string currentValue = base.GetCurrentValue();
			TMP_InputField tmp_InputField = this.textbox;
			if (tmp_InputField == null)
			{
				return;
			}
			tmp_InputField.SetTextWithoutNotify(currentValue);
		}

		// Token: 0x04004BF7 RID: 19447
		public TMP_InputField textbox;
	}
}
