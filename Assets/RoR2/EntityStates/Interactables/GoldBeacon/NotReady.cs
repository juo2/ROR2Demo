using System;

namespace EntityStates.Interactables.GoldBeacon
{
	// Token: 0x020002F4 RID: 756
	public class NotReady : GoldBeaconBaseState
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00038F63 File Offset: 0x00037163
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x00038F6A File Offset: 0x0003716A
		public static int count { get; private set; }

		// Token: 0x06000D7D RID: 3453 RVA: 0x00038F72 File Offset: 0x00037172
		public override void OnEnter()
		{
			base.OnEnter();
			base.SetReady(false);
			NotReady.count++;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00038F8D File Offset: 0x0003718D
		public override void OnExit()
		{
			NotReady.count--;
			base.OnExit();
		}
	}
}
