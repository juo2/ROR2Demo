using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A8F RID: 2703
	internal static class UnitySystemConsoleRedirector
	{
		// Token: 0x06003E07 RID: 15879 RVA: 0x000FFD74 File Offset: 0x000FDF74
		public static void Redirect()
		{
			UnitySystemConsoleRedirector.oldOut = Console.Out;
			Console.SetOut(new UnitySystemConsoleRedirector.OutWriter());
			UnitySystemConsoleRedirector.oldError = Console.Error;
			Console.SetError(new UnitySystemConsoleRedirector.ErrorWriter());
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000FFD9E File Offset: 0x000FDF9E
		public static void Disengage()
		{
			if (UnitySystemConsoleRedirector.oldOut != null)
			{
				Console.SetOut(UnitySystemConsoleRedirector.oldOut);
				UnitySystemConsoleRedirector.oldOut = null;
			}
			if (UnitySystemConsoleRedirector.oldError != null)
			{
				Console.SetError(UnitySystemConsoleRedirector.oldError);
				UnitySystemConsoleRedirector.oldError = null;
			}
		}

		// Token: 0x04003CBD RID: 15549
		private static TextWriter oldOut;

		// Token: 0x04003CBE RID: 15550
		private static TextWriter oldError;

		// Token: 0x02000A90 RID: 2704
		private class OutWriter : UnitySystemConsoleRedirector.UnityTextWriter
		{
			// Token: 0x06003E09 RID: 15881 RVA: 0x0006AA15 File Offset: 0x00068C15
			public override void WriteBufferToUnity(string str)
			{
				Debug.Log(str);
			}
		}

		// Token: 0x02000A91 RID: 2705
		private class ErrorWriter : UnitySystemConsoleRedirector.UnityTextWriter
		{
			// Token: 0x06003E0B RID: 15883 RVA: 0x000FFDD6 File Offset: 0x000FDFD6
			public override void WriteBufferToUnity(string str)
			{
				Debug.LogError(str);
			}
		}

		// Token: 0x02000A92 RID: 2706
		private abstract class UnityTextWriter : TextWriter
		{
			// Token: 0x06003E0D RID: 15885 RVA: 0x000FFDDE File Offset: 0x000FDFDE
			public override void Flush()
			{
				this.WriteBufferToUnity(this.buffer.ToString());
				this.buffer.Length = 0;
			}

			// Token: 0x06003E0E RID: 15886
			public abstract void WriteBufferToUnity(string str);

			// Token: 0x06003E0F RID: 15887 RVA: 0x000FFE00 File Offset: 0x000FE000
			public override void Write(string value)
			{
				this.buffer.Append(value);
				if (value != null)
				{
					int length = value.Length;
					if (length > 0 && value[length - 1] == '\n')
					{
						this.Flush();
					}
				}
			}

			// Token: 0x06003E10 RID: 15888 RVA: 0x000FFE3B File Offset: 0x000FE03B
			public override void Write(char value)
			{
				this.buffer.Append(value);
				if (value == '\n')
				{
					this.Flush();
				}
			}

			// Token: 0x06003E11 RID: 15889 RVA: 0x000FFE55 File Offset: 0x000FE055
			public override void Write(char[] value, int index, int count)
			{
				this.Write(new string(value, index, count));
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000FFE65 File Offset: 0x000FE065
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x04003CBF RID: 15551
			private StringBuilder buffer = new StringBuilder();
		}
	}
}
