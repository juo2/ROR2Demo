using System;
using System.Text;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009F1 RID: 2545
	[Serializable]
	public struct ProcChainMask : IEquatable<ProcChainMask>
	{
		// Token: 0x06003ADD RID: 15069 RVA: 0x000F4101 File Offset: 0x000F2301
		public void AddProc(ProcType procType)
		{
			this.mask |= 1U << (int)procType;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000F4116 File Offset: 0x000F2316
		public void RemoveProc(ProcType procType)
		{
			this.mask &= ~(1U << (int)procType);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000F412C File Offset: 0x000F232C
		public bool HasProc(ProcType procType)
		{
			return ((ulong)this.mask & (ulong)(1L << (int)(procType & (ProcType)31U))) > 0UL;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x0000B4B7 File Offset: 0x000096B7
		private static bool StaticCheck()
		{
			return true;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000F4141 File Offset: 0x000F2341
		public bool Equals(ProcChainMask other)
		{
			return this.mask == other.mask;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000F4151 File Offset: 0x000F2351
		public override bool Equals(object obj)
		{
			return obj != null && obj is ProcChainMask && this.Equals((ProcChainMask)obj);
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000F416E File Offset: 0x000F236E
		public override int GetHashCode()
		{
			return this.mask.GetHashCode();
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000F417B File Offset: 0x000F237B
		public override string ToString()
		{
			this.AppendToStringBuilder(ProcChainMask.sharedStringBuilder);
			string result = ProcChainMask.sharedStringBuilder.ToString();
			ProcChainMask.sharedStringBuilder.Clear();
			return result;
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000F41A0 File Offset: 0x000F23A0
		public void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.Append("(");
			bool flag = false;
			for (ProcType procType = ProcType.Behemoth; procType < ProcType.Count; procType += 1U)
			{
				if (this.HasProc(procType))
				{
					if (flag)
					{
						stringBuilder.Append("|");
					}
					stringBuilder.Append(procType.ToString());
					flag = true;
				}
			}
			stringBuilder.Append(")");
		}

		// Token: 0x040039B2 RID: 14770
		[SerializeField]
		public uint mask;

		// Token: 0x040039B3 RID: 14771
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
