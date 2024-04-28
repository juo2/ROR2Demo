using System;
using System.Linq;

namespace Facepunch.Steamworks
{
	// Token: 0x02000163 RID: 355
	public class Auth
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x000347F8 File Offset: 0x000329F8
		public unsafe Auth.Ticket GetAuthSessionTicket()
		{
			byte[] array = new byte[1024];
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			uint count = 0U;
			uint num = this.client.native.user.GetAuthSessionTicket((IntPtr)((void*)value), array.Length, out count);
			if (num == 0U)
			{
				return null;
			}
			return new Auth.Ticket
			{
				client = this.client,
				Data = array.Take((int)count).ToArray<byte>(),
				Handle = num
			};
		}

		// Token: 0x040007E3 RID: 2019
		internal Client client;

		// Token: 0x02000259 RID: 601
		public class Ticket : IDisposable
		{
			// Token: 0x06001D9A RID: 7578 RVA: 0x00063F98 File Offset: 0x00062198
			public void Cancel()
			{
				if (this.client.IsValid && this.Handle != 0U)
				{
					this.client.native.user.CancelAuthTicket(this.Handle);
					this.Handle = 0U;
					this.Data = null;
				}
			}

			// Token: 0x06001D9B RID: 7579 RVA: 0x00063FE8 File Offset: 0x000621E8
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04000B90 RID: 2960
			internal Client client;

			// Token: 0x04000B91 RID: 2961
			public byte[] Data;

			// Token: 0x04000B92 RID: 2962
			public uint Handle;
		}
	}
}
