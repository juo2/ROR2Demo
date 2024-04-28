using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000508 RID: 1288
	public class ColoredTokenChatMessage : SubjectChatMessage
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x000672D4 File Offset: 0x000654D4
		public override string ConstructChatString()
		{
			string @string = Language.GetString(base.GetResolvedToken());
			string subjectName = base.GetSubjectName();
			string[] array = new string[1 + this.paramTokens.Length];
			array[0] = subjectName;
			Array.Copy(this.paramTokens, 0, array, 1, this.paramTokens.Length);
			for (int i = 1; i < array.Length; i++)
			{
				int num = i - 1;
				if (num < this.paramColors.Length)
				{
					array[i] = Util.GenerateColoredString(Language.GetString(array[i]), this.paramColors[num]);
				}
				else
				{
					array[i] = Language.GetString(array[i]);
				}
			}
			string format = @string;
			object[] args = array;
			return string.Format(format, args);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00067374 File Offset: 0x00065574
		public override void Serialize(NetworkWriter writer)
		{
			base.Serialize(writer);
			writer.Write((byte)this.paramTokens.Length);
			for (int i = 0; i < this.paramTokens.Length; i++)
			{
				writer.Write(this.paramTokens[i]);
			}
			writer.Write((byte)this.paramColors.Length);
			for (int j = 0; j < this.paramColors.Length; j++)
			{
				writer.Write(this.paramColors[j]);
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000673EC File Offset: 0x000655EC
		public override void Deserialize(NetworkReader reader)
		{
			base.Deserialize(reader);
			this.paramTokens = new string[(int)reader.ReadByte()];
			for (int i = 0; i < this.paramTokens.Length; i++)
			{
				this.paramTokens[i] = reader.ReadString();
			}
			this.paramColors = new Color32[(int)reader.ReadByte()];
			for (int j = 0; j < this.paramColors.Length; j++)
			{
				this.paramColors[j] = reader.ReadColor32();
			}
		}

		// Token: 0x04001D0B RID: 7435
		private static readonly string[] empty = new string[0];

		// Token: 0x04001D0C RID: 7436
		public string[] paramTokens = ColoredTokenChatMessage.empty;

		// Token: 0x04001D0D RID: 7437
		public Color32[] paramColors = new Color32[0];
	}
}
