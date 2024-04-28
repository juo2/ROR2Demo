using System;

namespace RoR2.ConVar
{
	// Token: 0x02000E41 RID: 3649
	public class StringConVar : BaseConVar
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x00159B2F File Offset: 0x00157D2F
		// (set) Token: 0x060053BF RID: 21439 RVA: 0x00159B37 File Offset: 0x00157D37
		public string value { get; protected set; }

		// Token: 0x060053C0 RID: 21440 RVA: 0x00009F73 File Offset: 0x00008173
		public StringConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00159B40 File Offset: 0x00157D40
		public override void SetString(string newValue)
		{
			this.value = newValue;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00159B49 File Offset: 0x00157D49
		public override string GetString()
		{
			return this.value;
		}
	}
}
