using System;
using System.Globalization;
using System.Net;

namespace RoR2.Networking
{
	// Token: 0x02000C3A RID: 3130
	public struct AddressPortPair : IEquatable<AddressPortPair>
	{
		// Token: 0x060046C9 RID: 18121 RVA: 0x00124C5A File Offset: 0x00122E5A
		public AddressPortPair(string address, ushort port)
		{
			this.address = address;
			this.port = port;
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x00124C6A File Offset: 0x00122E6A
		public AddressPortPair(IPAddress address, ushort port)
		{
			this.address = address.ToString();
			this.port = port;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x00124C80 File Offset: 0x00122E80
		public static bool TryParse(string str, out AddressPortPair addressPortPair)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int num = str.Length - 1;
				while (num >= 0 && str[num] != ':')
				{
					num--;
				}
				if (num >= 0)
				{
					string text = str.Substring(0, num);
					string s = str.Substring(num + 1, str.Length - num - 1);
					addressPortPair.address = text;
					ushort num2;
					addressPortPair.port = (TextSerialization.TryParseInvariant(s, out num2) ? num2 : 0);
					return true;
				}
			}
			addressPortPair.address = "";
			addressPortPair.port = 0;
			return false;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060046CC RID: 18124 RVA: 0x00124D04 File Offset: 0x00122F04
		public bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.address);
			}
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00124D14 File Offset: 0x00122F14
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.address, this.port);
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x00124D36 File Offset: 0x00122F36
		public bool Equals(AddressPortPair other)
		{
			return string.Equals(this.address, other.address) && this.port == other.port;
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00124D5C File Offset: 0x00122F5C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is AddressPortPair)
			{
				AddressPortPair other = (AddressPortPair)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00124D88 File Offset: 0x00122F88
		public override int GetHashCode()
		{
			return ((this.address != null) ? this.address.GetHashCode() : 0) * 397 ^ this.port.GetHashCode();
		}

		// Token: 0x04004493 RID: 17555
		public string address;

		// Token: 0x04004494 RID: 17556
		public ushort port;
	}
}
