using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000D28 RID: 3368
	public class LanguageDropdownController : MonoBehaviour
	{
		// Token: 0x06004CCD RID: 19661 RVA: 0x0013D164 File Offset: 0x0013B364
		public void Awake()
		{
			this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDropdownValueChanged));
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0013D182 File Offset: 0x0013B382
		private void OnEnable()
		{
			this.SetupDropdownOptions();
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0013D18C File Offset: 0x0013B38C
		private void SetupDropdownOptions()
		{
			this.dropdown.ClearOptions();
			this.languages = Language.GetAllLanguages().ToArray<Language>();
			string @string = Language.LanguageConVar.instance.GetString();
			List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>(this.languages.Length + 1);
			int num = -1;
			int num2 = -1;
			Language language = Language.FindLanguageByName(Language.GetPlatformLanguageName() ?? "");
			this.isProvidingPlatformOption = (language != null);
			if (this.isProvidingPlatformOption)
			{
				list.Add(new TMP_Dropdown.OptionData(Language.GetStringFormatted("LANGUAGE_PLATFORM", new object[]
				{
					language.selfName
				}), language.iconSprite));
				num2++;
				if (@string == "platform")
				{
					num = num2;
				}
			}
			for (int i = 0; i < this.languages.Length; i++)
			{
				num2++;
				Language language2 = this.languages[i];
				list.Add(new TMP_Dropdown.OptionData(language2.selfName, language2.iconSprite));
				if (Language.currentLanguage == language2)
				{
					num = num2;
				}
			}
			this.dropdown.AddOptions(list);
			if (num != -1)
			{
				this.dropdown.SetValueWithoutNotify(num);
			}
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0013D2A0 File Offset: 0x0013B4A0
		private void OnDropdownValueChanged(int value)
		{
			if (this.isProvidingPlatformOption)
			{
				if (value == 0)
				{
					Language.LanguageConVar.instance.SetString("platform");
					return;
				}
				value--;
			}
			Language safe = ArrayUtils.GetSafe<Language>(this.languages, value);
			if (safe == null)
			{
				return;
			}
			Language.LanguageConVar.instance.SetString(safe.name);
		}

		// Token: 0x040049D0 RID: 18896
		public MPDropdown dropdown;

		// Token: 0x040049D1 RID: 18897
		public GameObject stripPrefab;

		// Token: 0x040049D2 RID: 18898
		private Language[] languages = Array.Empty<Language>();

		// Token: 0x040049D3 RID: 18899
		private bool isProvidingPlatformOption;
	}
}
