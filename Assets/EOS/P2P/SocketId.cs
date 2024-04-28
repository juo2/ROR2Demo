using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002BA RID: 698
	public class SocketId : ISettable
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00012DC3 File Offset: 0x00010FC3
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00012DCB File Offset: 0x00010FCB
		public string SocketName { get; set; }

		// Token: 0x060011B3 RID: 4531 RVA: 0x00012DD4 File Offset: 0x00010FD4
		internal void Set(SocketIdInternal? other)
		{
			if (other != null)
			{
				this.SocketName = other.Value.SocketName;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00012DFF File Offset: 0x00010FFF
		public void Set(object other)
		{
			this.Set(other as SocketIdInternal?);
		}
	}
}
