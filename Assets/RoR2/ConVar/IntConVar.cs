using System;

namespace RoR2.ConVar
{
	// Token: 0x02000E3F RID: 3647
	public class IntConVar : BaseConVar
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x00159AA6 File Offset: 0x00157CA6
		// (set) Token: 0x060053B5 RID: 21429 RVA: 0x00159AAE File Offset: 0x00157CAE
		public int value { get; protected set; }

		// Token: 0x060053B6 RID: 21430 RVA: 0x00009F73 File Offset: 0x00008173
		public IntConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00159AB8 File Offset: 0x00157CB8
		public override void SetString(string newValue)
		{
			int value;
			if (TextSerialization.TryParseInvariant(newValue, out value))
			{
				this.value = value;
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00159AD6 File Offset: 0x00157CD6
		public override string GetString()
		{
			return TextSerialization.ToStringInvariant(this.value);
		}
	}
}
