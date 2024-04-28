using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using RoR2.ConVar;
using RoR2.Networking;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020004FD RID: 1277
	public static class Chat
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0006692B File Offset: 0x00064B2B
		public static ReadOnlyCollection<string> readOnlyLog
		{
			get
			{
				return Chat._readOnlyLog;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001741 RID: 5953 RVA: 0x00066934 File Offset: 0x00064B34
		// (remove) Token: 0x06001742 RID: 5954 RVA: 0x00066968 File Offset: 0x00064B68
		public static event Action onChatChanged;

		// Token: 0x06001743 RID: 5955 RVA: 0x0006699C File Offset: 0x00064B9C
		public static void AddMessage(string message)
		{
			int num = Mathf.Max(Chat.cvChatMaxMessages.value, 1);
			while (Chat.log.Count > num)
			{
				Chat.log.RemoveAt(0);
			}
			Chat.log.Add(message);
			if (Chat.onChatChanged != null)
			{
				Chat.onChatChanged();
			}
			Debug.Log(message);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000669F6 File Offset: 0x00064BF6
		public static void Clear()
		{
			Chat.log.Clear();
			Action action = Chat.onChatChanged;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00066A11 File Offset: 0x00064C11
		public static void SendBroadcastChat(ChatMessageBase message)
		{
			Chat.SendBroadcastChat(message, QosChannelIndex.chat.intVal);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00066A24 File Offset: 0x00064C24
		public static void SendBroadcastChat(ChatMessageBase message, int channelIndex)
		{
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(59);
			networkWriter.Write(message.GetTypeIndex());
			networkWriter.Write(message);
			networkWriter.FinishMessage();
			foreach (NetworkConnection networkConnection in NetworkServer.connections)
			{
				if (networkConnection != null)
				{
					networkConnection.SendWriter(networkWriter, channelIndex);
				}
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00066AA0 File Offset: 0x00064CA0
		public static void SendPlayerConnectedMessage(NetworkUser user)
		{
			Chat.SendBroadcastChat(new Chat.PlayerChatMessage
			{
				networkPlayerName = user.GetNetworkPlayerName(),
				baseToken = "PLAYER_CONNECTED"
			});
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00066AC3 File Offset: 0x00064CC3
		public static void SendPlayerDisconnectedMessage(NetworkUser user)
		{
			Chat.SendBroadcastChat(new Chat.PlayerChatMessage
			{
				networkPlayerName = user.GetNetworkPlayerName(),
				baseToken = "PLAYER_DISCONNECTED"
			});
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00066AE6 File Offset: 0x00064CE6
		public static void AddPickupMessage(CharacterBody body, string pickupToken, Color32 pickupColor, uint pickupQuantity)
		{
			Chat.AddMessage(new Chat.PlayerPickupChatMessage
			{
				subjectAsCharacterBody = body,
				baseToken = "PLAYER_PICKUP",
				pickupToken = pickupToken,
				pickupColor = pickupColor,
				pickupQuantity = pickupQuantity
			});
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00066B1C File Offset: 0x00064D1C
		[NetworkMessageHandler(msgType = 59, client = true)]
		private static void HandleBroadcastChat(NetworkMessage netMsg)
		{
			ChatMessageBase chatMessageBase = ChatMessageBase.Instantiate(netMsg.reader.ReadByte());
			if (chatMessageBase != null)
			{
				chatMessageBase.Deserialize(netMsg.reader);
				Chat.AddMessage(chatMessageBase);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00066B50 File Offset: 0x00064D50
		private static void AddMessage(ChatMessageBase message)
		{
			string text = message.ConstructChatString();
			if (text != null)
			{
				Chat.AddMessage(text);
				message.OnProcessed();
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00066B73 File Offset: 0x00064D73
		[ConCommand(commandName = "say", flags = ConVarFlags.ExecuteOnServer, helpText = "Sends a chat message.")]
		private static void CCSay(ConCommandArgs args)
		{
			args.CheckArgumentCount(1);
			if (args.sender)
			{
				Chat.SendBroadcastChat(new Chat.UserChatMessage
				{
					sender = args.sender.gameObject,
					text = args[0]
				});
			}
		}

		// Token: 0x04001CF0 RID: 7408
		private static List<string> log = new List<string>();

		// Token: 0x04001CF1 RID: 7409
		private static ReadOnlyCollection<string> _readOnlyLog = Chat.log.AsReadOnly();

		// Token: 0x04001CF3 RID: 7411
		private static IntConVar cvChatMaxMessages = new IntConVar("chat_max_messages", ConVarFlags.None, "30", "Maximum number of chat messages to store.");

		// Token: 0x020004FE RID: 1278
		public class UserChatMessage : ChatMessageBase
		{
			// Token: 0x0600174E RID: 5966 RVA: 0x00066BE8 File Offset: 0x00064DE8
			public override string ConstructChatString()
			{
				if (this.sender)
				{
					NetworkUser component = this.sender.GetComponent<NetworkUser>();
					if (component)
					{
						return string.Format(CultureInfo.InvariantCulture, "<color=#e5eefc>{0}: {1}</color>", Util.EscapeRichTextForTextMeshPro(component.userName), Util.EscapeRichTextForTextMeshPro(this.text));
					}
				}
				return null;
			}

			// Token: 0x0600174F RID: 5967 RVA: 0x00066C3D File Offset: 0x00064E3D
			public override void OnProcessed()
			{
				base.OnProcessed();
				Util.PlaySound("Play_UI_chatMessage", RoR2Application.instance.gameObject);
			}

			// Token: 0x06001751 RID: 5969 RVA: 0x00066C62 File Offset: 0x00064E62
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.sender);
				writer.Write(this.text);
			}

			// Token: 0x06001752 RID: 5970 RVA: 0x00066C7C File Offset: 0x00064E7C
			public override void Deserialize(NetworkReader reader)
			{
				this.sender = reader.ReadGameObject();
				this.text = reader.ReadString();
			}

			// Token: 0x04001CF4 RID: 7412
			public GameObject sender;

			// Token: 0x04001CF5 RID: 7413
			public string text;
		}

		// Token: 0x020004FF RID: 1279
		public class NpcChatMessage : ChatMessageBase
		{
			// Token: 0x06001753 RID: 5971 RVA: 0x00066C96 File Offset: 0x00064E96
			public override string ConstructChatString()
			{
				return Language.GetStringFormatted(this.formatStringToken, new object[]
				{
					Language.GetString(this.baseToken)
				});
			}

			// Token: 0x06001754 RID: 5972 RVA: 0x00066CB7 File Offset: 0x00064EB7
			public override void OnProcessed()
			{
				base.OnProcessed();
				if (this.sender)
				{
					Util.PlaySound(this.sound, this.sender);
				}
			}

			// Token: 0x06001756 RID: 5974 RVA: 0x00066CDE File Offset: 0x00064EDE
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.sender);
				writer.Write(this.baseToken);
				writer.Write(this.sound);
				writer.Write(this.formatStringToken);
			}

			// Token: 0x06001757 RID: 5975 RVA: 0x00066D10 File Offset: 0x00064F10
			public override void Deserialize(NetworkReader reader)
			{
				this.sender = reader.ReadGameObject();
				this.baseToken = reader.ReadString();
				this.sound = reader.ReadString();
				this.formatStringToken = reader.ReadString();
			}

			// Token: 0x04001CF6 RID: 7414
			public GameObject sender;

			// Token: 0x04001CF7 RID: 7415
			public string baseToken;

			// Token: 0x04001CF8 RID: 7416
			public string sound;

			// Token: 0x04001CF9 RID: 7417
			public string formatStringToken;
		}

		// Token: 0x02000500 RID: 1280
		public class SimpleChatMessage : ChatMessageBase
		{
			// Token: 0x06001758 RID: 5976 RVA: 0x00066D44 File Offset: 0x00064F44
			public override string ConstructChatString()
			{
				string text = Language.GetString(this.baseToken);
				if (this.paramTokens != null && this.paramTokens.Length != 0)
				{
					IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
					string format = text;
					object[] args = this.paramTokens;
					text = string.Format(invariantCulture, format, args);
				}
				return text;
			}

			// Token: 0x0600175A RID: 5978 RVA: 0x00066D83 File Offset: 0x00064F83
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.baseToken);
				GeneratedNetworkCode._WriteArrayString_None(writer, this.paramTokens);
			}

			// Token: 0x0600175B RID: 5979 RVA: 0x00066D9D File Offset: 0x00064F9D
			public override void Deserialize(NetworkReader reader)
			{
				this.baseToken = reader.ReadString();
				this.paramTokens = GeneratedNetworkCode._ReadArrayString_None(reader);
			}

			// Token: 0x04001CFA RID: 7418
			public string baseToken;

			// Token: 0x04001CFB RID: 7419
			public string[] paramTokens;
		}

		// Token: 0x02000501 RID: 1281
		public class BodyChatMessage : ChatMessageBase
		{
			// Token: 0x0600175C RID: 5980 RVA: 0x00066DB8 File Offset: 0x00064FB8
			public override string ConstructChatString()
			{
				GameObject gameObject = this.bodyObject;
				CharacterBody characterBody = (gameObject != null) ? gameObject.GetComponent<CharacterBody>() : null;
				if (characterBody)
				{
					string bestBodyName = Util.GetBestBodyName(characterBody.gameObject);
					return string.Format(CultureInfo.InvariantCulture, "<color=#e5eefc>{0}: {1}</color>", Util.EscapeRichTextForTextMeshPro(bestBodyName), Language.GetString(this.token));
				}
				return null;
			}

			// Token: 0x0600175D RID: 5981 RVA: 0x00066C3D File Offset: 0x00064E3D
			public override void OnProcessed()
			{
				base.OnProcessed();
				Util.PlaySound("Play_UI_chatMessage", RoR2Application.instance.gameObject);
			}

			// Token: 0x0600175F RID: 5983 RVA: 0x00066E0E File Offset: 0x0006500E
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.bodyObject);
				writer.Write(this.token);
			}

			// Token: 0x06001760 RID: 5984 RVA: 0x00066E28 File Offset: 0x00065028
			public override void Deserialize(NetworkReader reader)
			{
				this.bodyObject = reader.ReadGameObject();
				this.token = reader.ReadString();
			}

			// Token: 0x04001CFC RID: 7420
			public GameObject bodyObject;

			// Token: 0x04001CFD RID: 7421
			public string token;
		}

		// Token: 0x02000502 RID: 1282
		public class SubjectFormatChatMessage : SubjectChatMessage
		{
			// Token: 0x06001761 RID: 5985 RVA: 0x00066E44 File Offset: 0x00065044
			public override string ConstructChatString()
			{
				string @string = Language.GetString(base.GetResolvedToken());
				string subjectName = base.GetSubjectName();
				string[] array = new string[1 + this.paramTokens.Length];
				array[0] = subjectName;
				Array.Copy(this.paramTokens, 0, array, 1, this.paramTokens.Length);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = Language.GetString(array[i]);
				}
				string format = @string;
				object[] args = array;
				return string.Format(format, args);
			}

			// Token: 0x06001762 RID: 5986 RVA: 0x00066EB4 File Offset: 0x000650B4
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				writer.Write((byte)this.paramTokens.Length);
				for (int i = 0; i < this.paramTokens.Length; i++)
				{
					writer.Write(this.paramTokens[i]);
				}
			}

			// Token: 0x06001763 RID: 5987 RVA: 0x00066EF8 File Offset: 0x000650F8
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				this.paramTokens = new string[(int)reader.ReadByte()];
				for (int i = 0; i < this.paramTokens.Length; i++)
				{
					this.paramTokens[i] = reader.ReadString();
				}
			}

			// Token: 0x04001CFE RID: 7422
			private static readonly string[] empty = new string[0];

			// Token: 0x04001CFF RID: 7423
			public string[] paramTokens = Chat.SubjectFormatChatMessage.empty;
		}

		// Token: 0x02000503 RID: 1283
		public class PlayerPickupChatMessage : SubjectChatMessage
		{
			// Token: 0x06001766 RID: 5990 RVA: 0x00066F60 File Offset: 0x00065160
			public override string ConstructChatString()
			{
				string subjectName = base.GetSubjectName();
				string @string = Language.GetString(base.GetResolvedToken());
				string arg = "";
				if (this.pickupQuantity != 1U)
				{
					arg = "(" + this.pickupQuantity + ")";
				}
				string text = Language.GetString(this.pickupToken) ?? "???";
				text = Util.GenerateColoredString(text, this.pickupColor);
				try
				{
					return string.Format(@string, subjectName, text, arg);
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
				return "";
			}

			// Token: 0x06001767 RID: 5991 RVA: 0x00066FF8 File Offset: 0x000651F8
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				writer.Write(this.pickupToken);
				writer.Write(this.pickupColor);
				writer.WritePackedUInt32(this.pickupQuantity);
			}

			// Token: 0x06001768 RID: 5992 RVA: 0x00067025 File Offset: 0x00065225
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				this.pickupToken = reader.ReadString();
				this.pickupColor = reader.ReadColor32();
				this.pickupQuantity = reader.ReadPackedUInt32();
			}

			// Token: 0x04001D00 RID: 7424
			public string pickupToken;

			// Token: 0x04001D01 RID: 7425
			public Color32 pickupColor;

			// Token: 0x04001D02 RID: 7426
			public uint pickupQuantity;
		}

		// Token: 0x02000504 RID: 1284
		public class PlayerDeathChatMessage : Chat.SubjectFormatChatMessage
		{
			// Token: 0x0600176A RID: 5994 RVA: 0x0006705C File Offset: 0x0006525C
			public override string ConstructChatString()
			{
				string text = base.ConstructChatString();
				if (text != null)
				{
					return "<style=cDeath><sprite name=\"Skull\" tint=1> " + text + " <sprite name=\"Skull\" tint=1></style>";
				}
				return text;
			}

			// Token: 0x0600176B RID: 5995 RVA: 0x00067085 File Offset: 0x00065285
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
			}

			// Token: 0x0600176C RID: 5996 RVA: 0x0006708E File Offset: 0x0006528E
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
			}
		}

		// Token: 0x02000505 RID: 1285
		public class NamedObjectChatMessage : ChatMessageBase
		{
			// Token: 0x0600176E RID: 5998 RVA: 0x0006709F File Offset: 0x0006529F
			public override string ConstructChatString()
			{
				return string.Format(Language.GetString(this.baseToken), base.GetObjectName(this.namedObject));
			}

			// Token: 0x06001770 RID: 6000 RVA: 0x000670BD File Offset: 0x000652BD
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.namedObject);
				writer.Write(this.baseToken);
				GeneratedNetworkCode._WriteArrayString_None(writer, this.paramTokens);
			}

			// Token: 0x06001771 RID: 6001 RVA: 0x000670E3 File Offset: 0x000652E3
			public override void Deserialize(NetworkReader reader)
			{
				this.namedObject = reader.ReadGameObject();
				this.baseToken = reader.ReadString();
				this.paramTokens = GeneratedNetworkCode._ReadArrayString_None(reader);
			}

			// Token: 0x04001D03 RID: 7427
			public GameObject namedObject;

			// Token: 0x04001D04 RID: 7428
			public string baseToken;

			// Token: 0x04001D05 RID: 7429
			public string[] paramTokens;
		}

		// Token: 0x02000506 RID: 1286
		public class PlayerChatMessage : ChatMessageBase
		{
			// Token: 0x06001772 RID: 6002 RVA: 0x00067109 File Offset: 0x00065309
			public override string ConstructChatString()
			{
				return string.Format(Language.GetString(this.baseToken), this.networkPlayerName.GetResolvedName());
			}

			// Token: 0x06001773 RID: 6003 RVA: 0x00067126 File Offset: 0x00065326
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				writer.Write(this.networkPlayerName);
				writer.Write(this.baseToken);
			}

			// Token: 0x06001774 RID: 6004 RVA: 0x00067147 File Offset: 0x00065347
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				this.networkPlayerName = reader.ReadNetworkPlayerName();
				this.baseToken = reader.ReadString();
			}

			// Token: 0x04001D06 RID: 7430
			public NetworkPlayerName networkPlayerName;

			// Token: 0x04001D07 RID: 7431
			public string baseToken;
		}
	}
}
