using System;

namespace RoR2
{
	// Token: 0x02000751 RID: 1873
	[Serializable]
	public struct CharacterFlightParameters : IEquatable<CharacterFlightParameters>
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x000A8E87 File Offset: 0x000A7087
		public bool Equals(CharacterFlightParameters other)
		{
			return this.channeledFlightGranterCount == other.channeledFlightGranterCount;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000A8E98 File Offset: 0x000A7098
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is CharacterFlightParameters)
			{
				CharacterFlightParameters other = (CharacterFlightParameters)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000A8EC4 File Offset: 0x000A70C4
		public override int GetHashCode()
		{
			return this.channeledFlightGranterCount;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000A8ECC File Offset: 0x000A70CC
		public bool CheckShouldUseFlight()
		{
			return this.channeledFlightGranterCount > 0;
		}

		// Token: 0x04002AC0 RID: 10944
		public int channeledFlightGranterCount;
	}
}
