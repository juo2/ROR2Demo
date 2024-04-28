using System;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C43 RID: 3139
	[Serializable]
	public struct NetworkDateTime : IEquatable<NetworkDateTime>
	{
		// Token: 0x06004702 RID: 18178 RVA: 0x001257D4 File Offset: 0x001239D4
		public static explicit operator NetworkDateTime(DateTime dateTime)
		{
			return new NetworkDateTime
			{
				_binaryValue = dateTime.ToBinary()
			};
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x001257F8 File Offset: 0x001239F8
		public static explicit operator DateTime(NetworkDateTime networkDateTime)
		{
			return DateTime.FromBinary(networkDateTime._binaryValue);
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x00125805 File Offset: 0x00123A05
		public static void Serialize(in NetworkDateTime networkDateTime, NetworkWriter writer)
		{
			writer.Write(networkDateTime._binaryValue);
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x00125813 File Offset: 0x00123A13
		public static void Deserialize(out NetworkDateTime networkDateTime, NetworkReader reader)
		{
			networkDateTime._binaryValue = reader.ReadInt64();
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00125821 File Offset: 0x00123A21
		public bool Equals(NetworkDateTime other)
		{
			return this._binaryValue == other._binaryValue;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x00125834 File Offset: 0x00123A34
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is NetworkDateTime)
			{
				NetworkDateTime other = (NetworkDateTime)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00125860 File Offset: 0x00123A60
		public override int GetHashCode()
		{
			return this._binaryValue.GetHashCode();
		}

		// Token: 0x040044B6 RID: 17590
		public long _binaryValue;
	}
}
