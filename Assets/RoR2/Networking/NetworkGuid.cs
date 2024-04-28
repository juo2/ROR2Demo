using System;
using System.Runtime.InteropServices;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C45 RID: 3141
	[Serializable]
	public struct NetworkGuid : IEquatable<NetworkGuid>
	{
		// Token: 0x0600470F RID: 18191 RVA: 0x001258FC File Offset: 0x00123AFC
		public static explicit operator NetworkGuid(Guid guid)
		{
			return new NetworkGuid.ConverterUnion
			{
				guildValue = guid
			}.networkGuidValue;
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x00125920 File Offset: 0x00123B20
		public static explicit operator Guid(NetworkGuid networkGuid)
		{
			return new NetworkGuid.ConverterUnion
			{
				networkGuidValue = networkGuid
			}.guildValue;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00125943 File Offset: 0x00123B43
		public bool Equals(NetworkGuid other)
		{
			return this._a == other._a && this._b == other._b;
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00125964 File Offset: 0x00123B64
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is NetworkGuid)
			{
				NetworkGuid other = (NetworkGuid)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00125990 File Offset: 0x00123B90
		public override int GetHashCode()
		{
			return this._a.GetHashCode() * 397 ^ this._b.GetHashCode();
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x001259AF File Offset: 0x00123BAF
		public static bool operator ==(NetworkGuid left, NetworkGuid right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x001259B9 File Offset: 0x00123BB9
		public static bool operator !=(NetworkGuid left, NetworkGuid right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x001259C6 File Offset: 0x00123BC6
		public void Serialize(NetworkWriter writer)
		{
			writer.Write(this._a);
			writer.Write(this._b);
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x001259E0 File Offset: 0x00123BE0
		public void Deserialize(NetworkReader reader)
		{
			this._a = reader.ReadUInt64();
			this._b = reader.ReadUInt64();
		}

		// Token: 0x040044B7 RID: 17591
		public ulong _a;

		// Token: 0x040044B8 RID: 17592
		public ulong _b;

		// Token: 0x02000C46 RID: 3142
		[StructLayout(LayoutKind.Explicit)]
		private struct ConverterUnion
		{
			// Token: 0x040044B9 RID: 17593
			[FieldOffset(0)]
			public Guid guildValue;

			// Token: 0x040044BA RID: 17594
			[FieldOffset(0)]
			public NetworkGuid networkGuidValue;
		}
	}
}
