using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004D0 RID: 1232
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class ConditionalHideAttribute : PropertyAttribute
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00062775 File Offset: 0x00060975
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x0006277D File Offset: 0x0006097D
		public string comparedPropertyName { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00062786 File Offset: 0x00060986
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x0006278E File Offset: 0x0006098E
		public object comparedValue { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00062797 File Offset: 0x00060997
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x0006279F File Offset: 0x0006099F
		public ConditionalHideAttribute.DisablingType disablingType { get; private set; }

		// Token: 0x0600165B RID: 5723 RVA: 0x000627A8 File Offset: 0x000609A8
		public ConditionalHideAttribute(string comparedPropertyName, object comparedValue, ConditionalHideAttribute.DisablingType disablingType = ConditionalHideAttribute.DisablingType.DontDraw)
		{
			this.comparedPropertyName = comparedPropertyName;
			this.comparedValue = comparedValue;
			this.disablingType = disablingType;
		}

		// Token: 0x020004D1 RID: 1233
		public enum DisablingType
		{
			// Token: 0x04001C1A RID: 7194
			ReadOnly = 2,
			// Token: 0x04001C1B RID: 7195
			DontDraw
		}
	}
}
