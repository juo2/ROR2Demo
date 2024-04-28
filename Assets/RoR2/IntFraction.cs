using System;
using System.Text;
using HG;

namespace RoR2
{
	// Token: 0x02000938 RID: 2360
	public readonly struct IntFraction : IEquatable<IntFraction>
	{
		// Token: 0x06003559 RID: 13657 RVA: 0x000E1A47 File Offset: 0x000DFC47
		public IntFraction(int numerator, int denominator)
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000E1A58 File Offset: 0x000DFC58
		public override string ToString()
		{
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			this.AppendToStringBuilder(stringBuilder);
			string result = stringBuilder.ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			return result;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000E1A7F File Offset: 0x000DFC7F
		public void AppendToStringBuilder(StringBuilder stringBuilder)
		{
			stringBuilder.AppendInt(this.numerator, 1U, uint.MaxValue).Append("/").AppendInt(this.denominator, 1U, uint.MaxValue);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000E1AA8 File Offset: 0x000DFCA8
		private static bool TryParse(string str, int startIndex, int length, out int result)
		{
			result = 0;
			int i = startIndex;
			int num = startIndex + length;
			if (startIndex < 0 || str.Length < num)
			{
				return false;
			}
			bool flag = false;
			if (i < num && str[i] == '-')
			{
				flag = true;
				i++;
			}
			if (i >= num || !char.IsDigit(str[i]))
			{
				return false;
			}
			while (i < num)
			{
				int num2 = (int)(str[i] - '0');
				if (num2 <= 0 || num2 >= 10)
				{
					break;
				}
				result *= 10;
				result += num2;
				i++;
			}
			if (flag)
			{
				result = -result;
			}
			return true;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000E1B2C File Offset: 0x000DFD2C
		public static bool TryParse(string str, out IntFraction result)
		{
			result = default(IntFraction);
			int num = str.IndexOf('/');
			if (num == -1)
			{
				return false;
			}
			int num2;
			if (!IntFraction.TryParse(str, 0, num, out num2))
			{
				return false;
			}
			int num3 = num + 1;
			num = str.Length - num3;
			int num4;
			if (!IntFraction.TryParse(str, num3, str.Length - num, out num4))
			{
				return false;
			}
			result = new IntFraction(num2, num4);
			return true;
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000E1B8F File Offset: 0x000DFD8F
		public bool Equals(IntFraction other)
		{
			return this.numerator == other.numerator && this.denominator == other.denominator;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000E1BB0 File Offset: 0x000DFDB0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is IntFraction)
			{
				IntFraction other = (IntFraction)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000E1BDC File Offset: 0x000DFDDC
		public override int GetHashCode()
		{
			return this.numerator * 397 ^ this.denominator;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000E1BF1 File Offset: 0x000DFDF1
		public static explicit operator float(in IntFraction value)
		{
			return (float)value.numerator / (float)value.denominator;
		}

		// Token: 0x04003621 RID: 13857
		public readonly int numerator;

		// Token: 0x04003622 RID: 13858
		public readonly int denominator;
	}
}
