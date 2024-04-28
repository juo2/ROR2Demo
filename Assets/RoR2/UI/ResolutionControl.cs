using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D71 RID: 3441
	public class ResolutionControl : BaseSettingsControl
	{
		// Token: 0x06004EDB RID: 20187 RVA: 0x00145F46 File Offset: 0x00144146
		private static Vector2Int ResolutionToVector2Int(Resolution resolution)
		{
			return new Vector2Int(resolution.width, resolution.height);
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x00145F5B File Offset: 0x0014415B
		private ResolutionControl.ResolutionOption GetCurrentSelectedResolutionOption()
		{
			if (this.resolutionDropdown.value >= 0)
			{
				return this.resolutionOptions[this.resolutionDropdown.value];
			}
			return null;
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x00145F80 File Offset: 0x00144180
		private void GenerateResolutionOptions()
		{
			Resolution[] array = Screen.resolutions;
			this.resolutionOptions = (from v in array.Select(new Func<Resolution, Vector2Int>(ResolutionControl.ResolutionToVector2Int)).Distinct<Vector2Int>()
			select new ResolutionControl.ResolutionOption
			{
				size = v
			}).ToArray<ResolutionControl.ResolutionOption>();
			foreach (ResolutionControl.ResolutionOption resolutionOption in this.resolutionOptions)
			{
				foreach (Resolution resolution in array)
				{
					if (ResolutionControl.ResolutionToVector2Int(resolution) == resolutionOption.size)
					{
						resolutionOption.supportedRefreshRates.Add(resolution.refreshRate);
					}
				}
			}
			List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
			foreach (ResolutionControl.ResolutionOption resolutionOption2 in this.resolutionOptions)
			{
				list.Add(new TMP_Dropdown.OptionData
				{
					text = resolutionOption2.GenerateDisplayString()
				});
			}
			this.resolutionDropdown.ClearOptions();
			this.resolutionDropdown.AddOptions(list);
			int value = -1;
			Vector2Int lhs = ResolutionControl.ResolutionToVector2Int(Screen.currentResolution);
			for (int k = 0; k < this.resolutionOptions.Length; k++)
			{
				if (lhs == this.resolutionOptions[k].size)
				{
					value = k;
					break;
				}
			}
			this.resolutionDropdown.value = value;
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x001460E8 File Offset: 0x001442E8
		private void GenerateRefreshRateOptions()
		{
			this.refreshRateDropdown.ClearOptions();
			ResolutionControl.ResolutionOption currentSelectedResolutionOption = this.GetCurrentSelectedResolutionOption();
			if (currentSelectedResolutionOption == null)
			{
				return;
			}
			List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
			foreach (int num in currentSelectedResolutionOption.supportedRefreshRates)
			{
				list.Add(new TMP_Dropdown.OptionData(num.ToString() + "Hz"));
			}
			this.refreshRateDropdown.AddOptions(list);
			int num2 = currentSelectedResolutionOption.supportedRefreshRates.IndexOf(Screen.currentResolution.refreshRate);
			if (num2 == -1)
			{
				num2 = currentSelectedResolutionOption.supportedRefreshRates.Count - 1;
			}
			this.refreshRateDropdown.value = num2;
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x001461B4 File Offset: 0x001443B4
		protected new void Awake()
		{
			base.Awake();
			this.resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnResolutionDropdownValueChanged));
			this.refreshRateDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnRefreshRateDropdownValueChanged));
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x001461F4 File Offset: 0x001443F4
		protected new void OnEnable()
		{
			base.OnEnable();
			this.GenerateResolutionOptions();
			this.GenerateRefreshRateOptions();
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x00146208 File Offset: 0x00144408
		private void OnResolutionDropdownValueChanged(int newValue)
		{
			if (newValue < 0)
			{
				return;
			}
			this.GenerateRefreshRateOptions();
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x00146215 File Offset: 0x00144415
		private void OnRefreshRateDropdownValueChanged(int newValue)
		{
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0014621C File Offset: 0x0014441C
		public void SubmitCurrentValue()
		{
			if (this.resolutionDropdown.value == -1 || this.refreshRateDropdown.value == -1)
			{
				return;
			}
			ResolutionControl.ResolutionOption resolutionOption = this.resolutionOptions[this.resolutionDropdown.value];
			base.SubmitSetting(string.Format(CultureInfo.InvariantCulture, "{0}x{1}x{2}", resolutionOption.size.x, resolutionOption.size.y, resolutionOption.supportedRefreshRates[this.refreshRateDropdown.value]));
		}

		// Token: 0x04004B8C RID: 19340
		public MPDropdown resolutionDropdown;

		// Token: 0x04004B8D RID: 19341
		public MPDropdown refreshRateDropdown;

		// Token: 0x04004B8E RID: 19342
		private Resolution[] resolutions;

		// Token: 0x04004B8F RID: 19343
		private ResolutionControl.ResolutionOption[] resolutionOptions = Array.Empty<ResolutionControl.ResolutionOption>();

		// Token: 0x02000D72 RID: 3442
		private class ResolutionOption
		{
			// Token: 0x06004EE5 RID: 20197 RVA: 0x001462BC File Offset: 0x001444BC
			public string GenerateDisplayString()
			{
				return string.Format("{0}x{1}", this.size.x, this.size.y);
			}

			// Token: 0x04004B90 RID: 19344
			public Vector2Int size;

			// Token: 0x04004B91 RID: 19345
			public readonly List<int> supportedRefreshRates = new List<int>();
		}
	}
}
