using System;

namespace EntityStates.Scrapper
{
	// Token: 0x020001CE RID: 462
	public class WaitToBeginScrapping : ScrapperBaseState
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool enableInteraction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000232BF File Offset: 0x000214BF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > WaitToBeginScrapping.duration)
			{
				this.outer.SetNextState(new Scrapping());
			}
		}

		// Token: 0x040009BF RID: 2495
		public static float duration;
	}
}
