using System;

namespace EntityStates.Scrapper
{
	// Token: 0x020001D1 RID: 465
	public class Idle : ScrapperBaseState
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool enableInteraction
		{
			get
			{
				return true;
			}
		}
	}
}
