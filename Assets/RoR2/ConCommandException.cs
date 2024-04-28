using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Facepunch.Steamworks;

namespace RoR2
{
	// Token: 0x02000670 RID: 1648
	[Serializable]
	public class ConCommandException : Exception
	{
		// Token: 0x06002024 RID: 8228 RVA: 0x0008A2F0 File Offset: 0x000884F0
		public ConCommandException()
		{
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0008A2F8 File Offset: 0x000884F8
		public ConCommandException(string message) : base(message)
		{
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0008A301 File Offset: 0x00088501
		public ConCommandException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0008A30B File Offset: 0x0008850B
		protected ConCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0008A315 File Offset: 0x00088515
		public static void CheckSteamworks()
		{
			if (Client.Instance == null)
			{
				throw new ConCommandException("Steamworks client not available.");
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0008A329 File Offset: 0x00088529
		public static void CheckArgumentCount(List<string> args, int requiredArgCount)
		{
			if (args.Count < requiredArgCount)
			{
				throw new ConCommandException(string.Format("{0} argument(s) required, {1} argument(s) provided.", requiredArgCount, args.Count));
			}
		}
	}
}
