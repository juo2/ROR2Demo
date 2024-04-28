using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Facepunch.Steamworks
{
	// Token: 0x02000181 RID: 385
	public static class Utility
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x0003A2E2 File Offset: 0x000384E2
		internal static uint Swap(uint x)
		{
			return ((x & 255U) << 24) + ((x & 65280U) << 8) + ((x & 16711680U) >> 8) + ((x & 4278190080U) >> 24);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003A30D File Offset: 0x0003850D
		public static uint IpToInt32(this IPAddress ipAddress)
		{
			return Utility.Swap((uint)ipAddress.Address);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0003A31B File Offset: 0x0003851B
		public static IPAddress Int32ToIp(uint ipAddress)
		{
			return new IPAddress((long)((ulong)Utility.Swap(ipAddress)));
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003A329 File Offset: 0x00038529
		internal static string FormatPrice(string currency, ulong price)
		{
			return Utility.FormatPrice(currency, price / 100.0);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003A340 File Offset: 0x00038540
		public static string FormatPrice(string currency, double price)
		{
			string text = price.ToString("0.00");
			uint num = <PrivateImplementationDetails>.ComputeStringHash(currency);
			if (num <= 2221184599U)
			{
				if (num <= 962568984U)
				{
					if (num <= 331211313U)
					{
						if (num <= 203100298U)
						{
							if (num != 44711862U)
							{
								if (num == 203100298U)
								{
									if (currency == "MXN")
									{
										return "Mex$ " + text;
									}
								}
							}
							else if (currency == "IDR")
							{
								return "Rp" + text;
							}
						}
						else if (num != 303913107U)
						{
							if (num == 331211313U)
							{
								if (currency == "ILS")
								{
									return "₪" + text;
								}
							}
						}
						else if (currency == "MYR")
						{
							return "RM " + text;
						}
					}
					else if (num <= 533790082U)
					{
						if (num != 385263313U)
						{
							if (num == 533790082U)
							{
								if (currency == "ZAR")
								{
									return "R " + text;
								}
							}
						}
						else if (currency == "SAR")
						{
							return "SR " + text;
						}
					}
					else if (num != 871170383U)
					{
						if (num != 961436151U)
						{
							if (num == 962568984U)
							{
								if (currency == "CHF")
								{
									return "Fr. " + text;
								}
							}
						}
						else if (currency == "CAD")
						{
							return "$" + text + " CAD";
						}
					}
					else if (currency == "QAR")
					{
						return "QR " + text;
					}
				}
				else if (num <= 1713324697U)
				{
					if (num <= 1163896872U)
					{
						if (num != 1097334389U)
						{
							if (num == 1163896872U)
							{
								if (currency == "TWD")
								{
									return "NT$ " + text;
								}
							}
						}
						else if (currency == "COP")
						{
							return "COL$ " + text;
						}
					}
					else if (num != 1198147198U)
					{
						if (num != 1568567338U)
						{
							if (num == 1713324697U)
							{
								if (currency == "CRC")
								{
									return "₡" + text;
								}
							}
						}
						else if (currency == "JPY")
						{
							return "¥" + text;
						}
					}
					else if (currency == "CLP")
					{
						return "$" + text + " CLP";
					}
				}
				else if (num <= 1828432737U)
				{
					if (num != 1774092687U)
					{
						if (num == 1828432737U)
						{
							if (currency == "SGD")
							{
								return "S$ " + text;
							}
						}
					}
					else if (currency == "NZD")
					{
						return "$" + text + " NZD";
					}
				}
				else if (num != 2175213072U)
				{
					if (num != 2208215117U)
					{
						if (num == 2221184599U)
						{
							if (currency == "CNY")
							{
								return text + "元";
							}
						}
					}
					else if (currency == "AUD")
					{
						return "$" + text + " AUD";
					}
				}
				else if (currency == "HKD")
				{
					return "HK$ " + text;
				}
			}
			else if (num <= 3277126311U)
			{
				if (num <= 2742539069U)
				{
					if (num <= 2607537575U)
					{
						if (num != 2390414266U)
						{
							if (num == 2607537575U)
							{
								if (currency == "USD")
								{
									return "$" + text;
								}
							}
						}
						else if (currency == "UYU")
						{
							return "$U " + text;
						}
					}
					else if (num != 2683950351U)
					{
						if (num != 2712123334U)
						{
							if (num == 2742539069U)
							{
								if (currency == "AED")
								{
									return text + "د.إ";
								}
							}
						}
						else if (currency == "RUB")
						{
							return "₽" + text;
						}
					}
					else if (currency == "PHP")
					{
						return "₱" + text;
					}
				}
				else if (num <= 2934852707U)
				{
					if (num != 2896936139U)
					{
						if (num == 2934852707U)
						{
							if (currency == "NOK")
							{
								return text + " kr";
							}
						}
					}
					else if (currency == "ARS")
					{
						return "$" + text + " ARS";
					}
				}
				else if (num != 3001173901U)
				{
					if (num != 3012466097U)
					{
						if (num == 3277126311U)
						{
							if (currency == "EUR")
							{
								return "€" + text;
							}
						}
					}
					else if (currency == "UAH")
					{
						return "₴" + text;
					}
				}
				else if (currency == "KRW")
				{
					return "₩" + text;
				}
			}
			else if (num <= 3998770030U)
			{
				if (num <= 3639174388U)
				{
					if (num != 3589126041U)
					{
						if (num == 3639174388U)
						{
							if (currency == "GBP")
							{
								return "£" + text;
							}
						}
					}
					else if (currency == "KWD")
					{
						return "KD " + text;
					}
				}
				else if (num != 3670251684U)
				{
					if (num != 3754783660U)
					{
						if (num == 3998770030U)
						{
							if (currency == "TRY")
							{
								return "₺" + text;
							}
						}
					}
					else if (currency == "KZT")
					{
						return text + "₸";
					}
				}
				else if (currency == "INR")
				{
					return "₹" + text;
				}
			}
			else if (num <= 4093711632U)
			{
				if (num != 4043176179U)
				{
					if (num == 4093711632U)
					{
						if (currency == "PEN")
						{
							return "S/. " + text;
						}
					}
				}
				else if (currency == "VND")
				{
					return "₫" + text;
				}
			}
			else if (num != 4115227625U)
			{
				if (num != 4126134037U)
				{
					if (num == 4288438971U)
					{
						if (currency == "BRL")
						{
							return "R$ " + text;
						}
					}
				}
				else if (currency == "PLN")
				{
					return "zł " + text;
				}
			}
			else if (currency == "THB")
			{
				return "฿" + text;
			}
			return text + " " + currency;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0003AB08 File Offset: 0x00038D08
		public static string ReadNullTerminatedUTF8String(this BinaryReader br, byte[] buffer = null)
		{
			if (buffer == null)
			{
				buffer = new byte[1024];
			}
			int num = 0;
			byte b;
			while ((b = br.ReadByte()) != 0 && num < buffer.Length)
			{
				buffer[num] = b;
				num++;
			}
			return Encoding.UTF8.GetString(buffer, 0, num);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0003AB4D File Offset: 0x00038D4D
		public static IEnumerable<T> UnionSelect<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, T> selector) where T : IEquatable<T>
		{
			Dictionary<T, T> items = new Dictionary<T, T>();
			foreach (T t in first)
			{
				items[t] = t;
			}
			foreach (T t2 in second)
			{
				T arg;
				if (items.TryGetValue(t2, out arg))
				{
					items.Remove(t2);
					yield return selector(arg, t2);
				}
			}
			IEnumerator<T> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x020002A3 RID: 675
		internal static class Epoch
		{
			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00067E3C File Offset: 0x0006603C
			public static int Current
			{
				get
				{
					return (int)DateTime.UtcNow.Subtract(Utility.Epoch.epoch).TotalSeconds;
				}
			}

			// Token: 0x06001F74 RID: 8052 RVA: 0x00067E64 File Offset: 0x00066064
			public static DateTime ToDateTime(decimal unixTime)
			{
				return Utility.Epoch.epoch.AddSeconds((double)((long)unixTime));
			}

			// Token: 0x06001F75 RID: 8053 RVA: 0x00067E88 File Offset: 0x00066088
			public static uint FromDateTime(DateTime dt)
			{
				return (uint)dt.Subtract(Utility.Epoch.epoch).TotalSeconds;
			}

			// Token: 0x04000D22 RID: 3362
			private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		}
	}
}
