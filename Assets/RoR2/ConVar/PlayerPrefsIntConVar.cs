using System;
using UnityEngine;

namespace RoR2.ConVar
{
	// Token: 0x02000E3D RID: 3645
	public class PlayerPrefsIntConVar : BaseConVar
	{
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x001599B0 File Offset: 0x00157BB0
		// (set) Token: 0x060053AA RID: 21418 RVA: 0x00159A14 File Offset: 0x00157C14
		public int value
		{
			get
			{
				int value;
				if (this.defaultValueInt == null && int.TryParse(this.defaultValue, out value))
				{
					this.defaultValueInt = new int?(value);
				}
				if (this.defaultValueInt != null)
				{
					return PlayerPrefs.GetInt(this.name, this.defaultValueInt.Value);
				}
				return PlayerPrefs.GetInt(this.name);
			}
			set
			{
				PlayerPrefs.SetInt(this.name, value);
				PlayerPrefs.Save();
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x00009F73 File Offset: 0x00008173
		public PlayerPrefsIntConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00159A28 File Offset: 0x00157C28
		public override void SetString(string newValue)
		{
			int value;
			if (TextSerialization.TryParseInvariant(newValue, out value))
			{
				this.value = value;
			}
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x00159A46 File Offset: 0x00157C46
		public override string GetString()
		{
			return TextSerialization.ToStringInvariant(this.value);
		}

		// Token: 0x04004FA7 RID: 20391
		private int? defaultValueInt;
	}
}
