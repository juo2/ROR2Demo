using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A8E RID: 2702
	public class TypeRestrictedReferenceAttribute : PropertyAttribute
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x000FFD56 File Offset: 0x000FDF56
		public TypeRestrictedReferenceAttribute(params Type[] allowedTypes)
		{
			if (allowedTypes != null)
			{
				this.allowedTypes = allowedTypes;
				return;
			}
			this.allowedTypes = Array.Empty<Type>();
		}

		// Token: 0x04003CBC RID: 15548
		public readonly Type[] allowedTypes;
	}
}
