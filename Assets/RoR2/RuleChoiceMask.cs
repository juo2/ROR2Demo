using System;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A19 RID: 2585
	public class RuleChoiceMask : SerializableBitArray
	{
		// Token: 0x06003B9E RID: 15262 RVA: 0x000F66AD File Offset: 0x000F48AD
		public RuleChoiceMask() : base(RuleCatalog.choiceCount)
		{
		}

		// Token: 0x170005A5 RID: 1445
		public bool this[RuleChoiceDef choiceDef]
		{
			get
			{
				return base[choiceDef.globalIndex];
			}
			set
			{
				base[choiceDef.globalIndex] = value;
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000F66D8 File Offset: 0x000F48D8
		public void Serialize(NetworkWriter writer)
		{
			for (int i = 0; i < this.bytes.Length; i++)
			{
				writer.Write(this.bytes[i]);
			}
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000F6708 File Offset: 0x000F4908
		public void Deserialize(NetworkReader reader)
		{
			for (int i = 0; i < this.bytes.Length; i++)
			{
				this.bytes[i] = reader.ReadByte();
			}
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000F6738 File Offset: 0x000F4938
		public override bool Equals(object obj)
		{
			RuleChoiceMask ruleChoiceMask = obj as RuleChoiceMask;
			if (ruleChoiceMask != null)
			{
				for (int i = 0; i < this.bytes.Length; i++)
				{
					if (this.bytes[i] != ruleChoiceMask.bytes[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000F6778 File Offset: 0x000F4978
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.bytes.Length; i++)
			{
				num += (int)this.bytes[i];
			}
			return num;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000F67A8 File Offset: 0x000F49A8
		public void Copy([NotNull] RuleChoiceMask src)
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
