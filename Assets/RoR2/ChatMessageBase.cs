using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000507 RID: 1287
	public abstract class ChatMessageBase : MessageBase
	{
		// Token: 0x06001776 RID: 6006 RVA: 0x00067168 File Offset: 0x00065368
		static ChatMessageBase()
		{
			ChatMessageBase.BuildMessageTypeNetMap();
		}

		// Token: 0x06001777 RID: 6007
		public abstract string ConstructChatString();

		// Token: 0x06001778 RID: 6008 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnProcessed()
		{
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000671A0 File Offset: 0x000653A0
		private static void BuildMessageTypeNetMap()
		{
			foreach (Type type in typeof(ChatMessageBase).Assembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof(ChatMessageBase)))
				{
					ChatMessageBase.chatMessageTypeToIndex.Add(type, (byte)ChatMessageBase.chatMessageIndexToType.Count);
					ChatMessageBase.chatMessageIndexToType.Add(type);
				}
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00067208 File Offset: 0x00065408
		protected string GetObjectName(GameObject namedObject)
		{
			string result = "???";
			if (namedObject)
			{
				result = namedObject.name;
				NetworkUser networkUser = namedObject.GetComponent<NetworkUser>();
				if (!networkUser)
				{
					networkUser = Util.LookUpBodyNetworkUser(namedObject);
				}
				if (networkUser)
				{
					result = Util.EscapeRichTextForTextMeshPro(networkUser.userName);
				}
			}
			return result;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00067255 File Offset: 0x00065455
		public byte GetTypeIndex()
		{
			return ChatMessageBase.chatMessageTypeToIndex[base.GetType()];
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00067268 File Offset: 0x00065468
		public static ChatMessageBase Instantiate(byte typeIndex)
		{
			Type type = ChatMessageBase.chatMessageIndexToType[(int)typeIndex];
			if (ChatMessageBase.cvChatDebug.value)
			{
				Debug.LogFormat("Received chat message typeIndex={0} type={1}", new object[]
				{
					typeIndex,
					(type != null) ? type.Name : null
				});
			}
			if (type != null)
			{
				return (ChatMessageBase)Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000026ED File Offset: 0x000008ED
		public override void Serialize(NetworkWriter writer)
		{
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000026ED File Offset: 0x000008ED
		public override void Deserialize(NetworkReader reader)
		{
		}

		// Token: 0x04001D08 RID: 7432
		private static readonly BoolConVar cvChatDebug = new BoolConVar("chat_debug", ConVarFlags.None, "0", "Enables logging of chat network messages.");

		// Token: 0x04001D09 RID: 7433
		private static readonly Dictionary<Type, byte> chatMessageTypeToIndex = new Dictionary<Type, byte>();

		// Token: 0x04001D0A RID: 7434
		private static readonly List<Type> chatMessageIndexToType = new List<Type>();
	}
}
