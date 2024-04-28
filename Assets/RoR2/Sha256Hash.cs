using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A54 RID: 2644
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct Sha256Hash : IEquatable<Sha256Hash>
	{
		// Token: 0x06003CCC RID: 15564 RVA: 0x000FAF7C File Offset: 0x000F917C
		public static Sha256Hash FromHexString(string hexString, int startIndex = 0)
		{
			Sha256Hash.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.hexString = hexString;
			if (startIndex + CS$<>8__locals1.hexString.Length < 64)
			{
				throw new ArgumentException("Not enough characters in string.");
			}
			CS$<>8__locals1.readPos = startIndex;
			ulong 00_ = Sha256Hash.<FromHexString>g__ReadULong|5_2(ref CS$<>8__locals1);
			ulong 08_ = Sha256Hash.<FromHexString>g__ReadULong|5_2(ref CS$<>8__locals1);
			ulong 16_ = Sha256Hash.<FromHexString>g__ReadULong|5_2(ref CS$<>8__locals1);
			ulong 24_ = Sha256Hash.<FromHexString>g__ReadULong|5_2(ref CS$<>8__locals1);
			return Sha256Hash.FromULong4(00_, 08_, 16_, 24_);
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000FAFDC File Offset: 0x000F91DC
		public static Sha256Hash FromULong4(ulong _00_07, ulong _08_15, ulong _16_23, ulong _24_31)
		{
			return new Sha256Hash
			{
				_00_07 = _00_07,
				_08_15 = _08_15,
				_16_23 = _16_23,
				_24_31 = _24_31
			};
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000FB012 File Offset: 0x000F9212
		public void ToULong4(out ulong _00_07, out ulong _08_15, out ulong _16_23, out ulong _24_31)
		{
			_00_07 = this._00_07;
			_08_15 = this._08_15;
			_16_23 = this._16_23;
			_24_31 = this._24_31;
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000FB038 File Offset: 0x000F9238
		public static Sha256Hash FromBytes(byte[] bytes, int startIndex = 0)
		{
			if (startIndex + bytes.Length < 32)
			{
				throw new ArgumentException("Not enough bytes in buffer.");
			}
			return new Sha256Hash
			{
				_00_07 = BitConverter.ToUInt64(bytes, startIndex),
				_08_15 = BitConverter.ToUInt64(bytes, startIndex + 8),
				_16_23 = BitConverter.ToUInt64(bytes, startIndex + 16),
				_24_31 = BitConverter.ToUInt64(bytes, startIndex + 24)
			};
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000FB0A2 File Offset: 0x000F92A2
		public bool Equals(Sha256Hash other)
		{
			return this._00_07 == other._00_07 && this._08_15 == other._08_15 && this._16_23 == other._16_23 && this._24_31 == other._24_31;
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000FB0E0 File Offset: 0x000F92E0
		public override bool Equals(object obj)
		{
			if (obj is Sha256Hash)
			{
				Sha256Hash other = (Sha256Hash)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000FB108 File Offset: 0x000F9308
		public override int GetHashCode()
		{
			return ((this._00_07.GetHashCode() * 397 ^ this._08_15.GetHashCode()) * 397 ^ this._16_23.GetHashCode()) * 397 ^ this._24_31.GetHashCode();
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000FB158 File Offset: 0x000F9358
		public static StringBuilder AppendSha256HashHex(StringBuilder stringBuilder, in Sha256Hash hash)
		{
			Sha256Hash.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.stringBuilder = stringBuilder;
			CS$<>8__locals1.stringBuilder.EnsureCapacity(CS$<>8__locals1.stringBuilder.Length + 64);
			Sha256Hash.<AppendSha256HashHex>g__AppendULong|12_0(hash._00_07, ref CS$<>8__locals1);
			Sha256Hash.<AppendSha256HashHex>g__AppendULong|12_0(hash._08_15, ref CS$<>8__locals1);
			Sha256Hash.<AppendSha256HashHex>g__AppendULong|12_0(hash._16_23, ref CS$<>8__locals1);
			Sha256Hash.<AppendSha256HashHex>g__AppendULong|12_0(hash._24_31, ref CS$<>8__locals1);
			return CS$<>8__locals1.stringBuilder;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000FB1C1 File Offset: 0x000F93C1
		public override string ToString()
		{
			return Sha256Hash.AppendSha256HashHex(new StringBuilder(64), this).ToString();
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000FB1E4 File Offset: 0x000F93E4
		[CompilerGenerated]
		internal static byte <FromHexString>g__ReadNibble|5_0(ref Sha256Hash.<>c__DisplayClass5_0 A_0)
		{
			string hexString = A_0.hexString;
			int readPos = A_0.readPos;
			A_0.readPos = readPos + 1;
			char c = hexString[readPos];
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (byte)('\n' + (c - 'a'));
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)('\n' + (c - 'A'));
			}
			return 0;
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000FB248 File Offset: 0x000F9448
		[CompilerGenerated]
		internal static byte <FromHexString>g__ReadByte|5_1(ref Sha256Hash.<>c__DisplayClass5_0 A_0)
		{
			byte b = Sha256Hash.<FromHexString>g__ReadNibble|5_0(ref A_0);
			byte b2 = Sha256Hash.<FromHexString>g__ReadNibble|5_0(ref A_0);
			return b << 4 | b2;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000FB268 File Offset: 0x000F9468
		[CompilerGenerated]
		internal static ulong <FromHexString>g__ReadULong|5_2(ref Sha256Hash.<>c__DisplayClass5_0 A_0)
		{
			Sha256Hash.ulongToByteBuffer[0] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[1] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[2] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[3] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[4] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[5] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[6] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			Sha256Hash.ulongToByteBuffer[7] = Sha256Hash.<FromHexString>g__ReadByte|5_1(ref A_0);
			return BitConverter.ToUInt64(Sha256Hash.ulongToByteBuffer, 0);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000FB2E8 File Offset: 0x000F94E8
		[CompilerGenerated]
		internal static void <AppendSha256HashHex>g__AppendULong|12_0(ulong value, ref Sha256Hash.<>c__DisplayClass12_0 A_1)
		{
			A_1.stringBuilder.AppendByteHexValue((byte)(value & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 8 & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 16 & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 24 & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 32 & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 40 & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 48 & 255UL));
			A_1.stringBuilder.AppendByteHexValue((byte)(value >> 56 & 255UL));
		}

		// Token: 0x04003BEE RID: 15342
		[SerializeField]
		private ulong _00_07;

		// Token: 0x04003BEF RID: 15343
		[SerializeField]
		private ulong _08_15;

		// Token: 0x04003BF0 RID: 15344
		[SerializeField]
		private ulong _16_23;

		// Token: 0x04003BF1 RID: 15345
		[SerializeField]
		private ulong _24_31;

		// Token: 0x04003BF2 RID: 15346
		private static readonly byte[] ulongToByteBuffer = new byte[8];
	}
}
