using System;
using System.Text;
using HG;
using UnityEngine;

namespace RoR2.ConVar
{
	// Token: 0x02000E3C RID: 3644
	public abstract class BaseConVar
	{
		// Token: 0x060053A1 RID: 21409 RVA: 0x00159738 File Offset: 0x00157938
		protected BaseConVar(string name, ConVarFlags flags, string defaultValue, string helpText)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.flags = flags;
			this.defaultValue = defaultValue;
			if (helpText == null)
			{
				throw new ArgumentNullException("helpText");
			}
			this.helpText = helpText;
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00159788 File Offset: 0x00157988
		public void AttemptSetString(string newValue)
		{
			try
			{
				this.SetString(newValue);
			}
			catch (ConCommandException ex)
			{
				Debug.LogFormat("Could not set value of ConVar \"{0}\" to \"{1}\": {2}", new object[]
				{
					this.name,
					newValue,
					ex.Message
				});
			}
		}

		// Token: 0x060053A3 RID: 21411
		public abstract void SetString(string newValue);

		// Token: 0x060053A4 RID: 21412
		public abstract string GetString();

		// Token: 0x060053A5 RID: 21413 RVA: 0x001597D8 File Offset: 0x001579D8
		protected static void GetEnumValue<T>(string str, ref T dest) where T : struct, Enum
		{
			T t;
			if (Enum.TryParse<T>(str, out t))
			{
				dest = t;
				return;
			}
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.Append("Provided value \"").Append(str).Append("\"").Append(" is not a recognized option. Recognized options: { ");
			bool flag = false;
			foreach (string value in Enum.GetNames(typeof(T)))
			{
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					flag = true;
				}
				stringBuilder.Append("\"").Append(value).Append("\"");
			}
			stringBuilder.Append(" }");
			string message = stringBuilder.ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			throw new ConCommandException(message);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x001598A0 File Offset: 0x00157AA0
		protected static int ParseIntInvariant(string str)
		{
			int result;
			if (TextSerialization.TryParseInvariant(str, out result))
			{
				return result;
			}
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.Append("Provided value \"").Append(str).Append("\"").Append(" is not a valid number.");
			string message = stringBuilder.ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			throw new ConCommandException(message);
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x001598F8 File Offset: 0x00157AF8
		protected static float ParseFloatInvariant(string str)
		{
			float result;
			if (TextSerialization.TryParseInvariant(str, out result))
			{
				return result;
			}
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.Append("Provided value \"").Append(str).Append("\"").Append(" is not a valid number.");
			string message = stringBuilder.ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			throw new ConCommandException(message);
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00159950 File Offset: 0x00157B50
		protected static bool ParseBoolInvariant(string str)
		{
			int num;
			if (TextSerialization.TryParseInvariant(str, out num))
			{
				if (num == 0)
				{
					return false;
				}
				if (num == 1)
				{
					return true;
				}
			}
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.Append("Provided value \"").Append(str).Append("\"").Append(" was neither \"0\" nor \"1\".");
			string message = stringBuilder.ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			throw new ConCommandException(message);
		}

		// Token: 0x04004FA3 RID: 20387
		public string name;

		// Token: 0x04004FA4 RID: 20388
		public ConVarFlags flags;

		// Token: 0x04004FA5 RID: 20389
		public string defaultValue;

		// Token: 0x04004FA6 RID: 20390
		public string helpText;
	}
}
