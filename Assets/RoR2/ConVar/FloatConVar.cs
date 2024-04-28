using System;

namespace RoR2.ConVar
{
	// Token: 0x02000E40 RID: 3648
	public class FloatConVar : BaseConVar
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060053B9 RID: 21433 RVA: 0x00159AE3 File Offset: 0x00157CE3
		// (set) Token: 0x060053BA RID: 21434 RVA: 0x00159AEB File Offset: 0x00157CEB
		public float value { get; protected set; }

		// Token: 0x060053BB RID: 21435 RVA: 0x00009F73 File Offset: 0x00008173
		public FloatConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
		{
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00159AF4 File Offset: 0x00157CF4
		public override void SetString(string newValue)
		{
			float num;
			if (TextSerialization.TryParseInvariant(newValue, out num) && !float.IsNaN(num) && !float.IsInfinity(num))
			{
				this.value = num;
			}
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x00159B22 File Offset: 0x00157D22
		public override string GetString()
		{
			return TextSerialization.ToStringInvariant(this.value);
		}
	}
}
