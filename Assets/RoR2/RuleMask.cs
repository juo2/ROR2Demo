using System;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A18 RID: 2584
	public class RuleMask : SerializableBitArray
	{
		// Token: 0x06003B98 RID: 15256 RVA: 0x000F659E File Offset: 0x000F479E
		public RuleMask() : base(RuleCatalog.ruleCount)
		{
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000F65AC File Offset: 0x000F47AC
		public void Serialize(NetworkWriter writer)
		{
			for (int i = 0; i < this.bytes.Length; i++)
			{
				writer.Write(this.bytes[i]);
			}
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000F65DC File Offset: 0x000F47DC
		public void Deserialize(NetworkReader reader)
		{
			for (int i = 0; i < this.bytes.Length; i++)
			{
				this.bytes[i] = reader.ReadByte();
			}
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000F660C File Offset: 0x000F480C
		public override bool Equals(object obj)
		{
			RuleMask ruleMask = obj as RuleMask;
			if (ruleMask != null)
			{
				for (int i = 0; i < this.bytes.Length; i++)
				{
					if (this.bytes[i] != ruleMask.bytes[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000F664C File Offset: 0x000F484C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.bytes.Length; i++)
			{
				num += (int)this.bytes[i];
			}
			return num;
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x000F667C File Offset: 0x000F487C
		public void Copy([NotNull] RuleMask src)
		{
			byte[] bytes = src.bytes;
			byte[] bytes2 = this.bytes;
			int i = 0;
			int num = bytes2.Length;
			while (i < num)
			{
				bytes2[i] = bytes[i];
				i++;
			}
		}
	}
}
