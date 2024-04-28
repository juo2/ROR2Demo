using System;

namespace RoR2.ConVar
{
	// Token: 0x02000E3E RID: 3646
	public class BoolConVar : BaseConVar
	{
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x00159A53 File Offset: 0x00157C53
		// (set) Token: 0x060053AF RID: 21423 RVA: 0x00159A5B File Offset: 0x00157C5B
		public bool value { get; protected set; }

		// Token: 0x060053B0 RID: 21424 RVA: 0x00009F73 File Offset: 0x00008173
		public BoolConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00159A64 File Offset: 0x00157C64
		public void SetBool(bool newValue)
		{
			this.value = newValue;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00159A70 File Offset: 0x00157C70
		public override void SetString(string newValue)
		{
			int num;
			if (TextSerialization.TryParseInvariant(newValue, out num))
			{
				this.value = (num != 0);
			}
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00159A91 File Offset: 0x00157C91
		public override string GetString()
		{
			if (!this.value)
			{
				return "0";
			}
			return "1";
		}
	}
}
