using System;
using RoR2;

namespace EntityStates.Scrapper
{
	// Token: 0x020001CF RID: 463
	public class Scrapping : ScrapperBaseState
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool enableInteraction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000232EC File Offset: 0x000214EC
		public override void OnEnter()
		{
			base.OnEnter();
			base.FindModelChild("ScrappingEffect").gameObject.SetActive(true);
			Util.PlaySound(Scrapping.enterSoundString, base.gameObject);
			base.PlayAnimation("Base", "Scrapping", "Scrapping.playbackRate", Scrapping.duration);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00023340 File Offset: 0x00021540
		public override void OnExit()
		{
			base.FindModelChild("ScrappingEffect").gameObject.SetActive(false);
			Util.PlaySound(Scrapping.exitSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002336F File Offset: 0x0002156F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > Scrapping.duration)
			{
				this.outer.SetNextState(new ScrappingToIdle());
			}
		}

		// Token: 0x040009C0 RID: 2496
		public static string enterSoundString;

		// Token: 0x040009C1 RID: 2497
		public static string exitSoundString;

		// Token: 0x040009C2 RID: 2498
		public static float duration;
	}
}
