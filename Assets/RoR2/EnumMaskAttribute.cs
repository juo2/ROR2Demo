using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005AB RID: 1451
	public class EnumMaskAttribute : PropertyAttribute
	{
		// Token: 0x06001A45 RID: 6725 RVA: 0x00071523 File Offset: 0x0006F723
		public EnumMaskAttribute(Type enumType)
		{
			this.enumType = enumType;
		}

		// Token: 0x04002076 RID: 8310
		public Type enumType;
	}
}
