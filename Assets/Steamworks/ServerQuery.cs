using System;

namespace Facepunch.Steamworks
{
	// Token: 0x0200017C RID: 380
	public class ServerQuery
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x00039F43 File Offset: 0x00038143
		internal ServerQuery(Server s)
		{
			this.server = s;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00039F54 File Offset: 0x00038154
		public unsafe bool GetOutgoingPacket(out ServerQuery.Packet packet)
		{
			packet = default(ServerQuery.Packet);
			byte[] array;
			byte* value;
			if ((array = ServerQuery.buffer) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			uint address = 0U;
			ushort port = 0;
			int nextOutgoingPacket = this.server.native.gameServer.GetNextOutgoingPacket((IntPtr)((void*)value), ServerQuery.buffer.Length, out address, out port);
			if (nextOutgoingPacket == 0)
			{
				return false;
			}
			packet.Size = nextOutgoingPacket;
			packet.Data = ServerQuery.buffer;
			packet.Address = address;
			packet.Port = port;
			return true;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00039FD8 File Offset: 0x000381D8
		public unsafe void Handle(byte[] data, int size, uint address, ushort port)
		{
			fixed (byte[] array = data)
			{
				byte* value;
				if (data == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				this.server.native.gameServer.HandleIncomingPacket((IntPtr)((void*)value), size, address, port);
			}
		}

		// Token: 0x0400087F RID: 2175
		internal Server server;

		// Token: 0x04000880 RID: 2176
		internal static byte[] buffer = new byte[65536];

		// Token: 0x0200029F RID: 671
		public struct Packet
		{
			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00067DA1 File Offset: 0x00065FA1
			// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00067DA9 File Offset: 0x00065FA9
			public uint Address { get; internal set; }

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06001F69 RID: 8041 RVA: 0x00067DB2 File Offset: 0x00065FB2
			// (set) Token: 0x06001F6A RID: 8042 RVA: 0x00067DBA File Offset: 0x00065FBA
			public ushort Port { get; internal set; }

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06001F6B RID: 8043 RVA: 0x00067DC3 File Offset: 0x00065FC3
			// (set) Token: 0x06001F6C RID: 8044 RVA: 0x00067DCB File Offset: 0x00065FCB
			public byte[] Data { get; internal set; }

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06001F6D RID: 8045 RVA: 0x00067DD4 File Offset: 0x00065FD4
			// (set) Token: 0x06001F6E RID: 8046 RVA: 0x00067DDC File Offset: 0x00065FDC
			public int Size { get; internal set; }
		}
	}
}
