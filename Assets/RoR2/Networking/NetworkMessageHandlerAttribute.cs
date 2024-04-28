using System;
using System.Collections.Generic;
using System.Reflection;
using HG.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C58 RID: 3160
	[MeansImplicitUse]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class NetworkMessageHandlerAttribute : SearchableAttribute
	{
		// Token: 0x06004787 RID: 18311 RVA: 0x001273F4 File Offset: 0x001255F4
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void CollectHandlers()
		{
			NetworkMessageHandlerAttribute.clientMessageHandlers.Clear();
			NetworkMessageHandlerAttribute.serverMessageHandlers.Clear();
			HashSet<short> hashSet = new HashSet<short>();
			foreach (SearchableAttribute searchableAttribute in SearchableAttribute.GetInstances<NetworkMessageHandlerAttribute>())
			{
				NetworkMessageHandlerAttribute networkMessageHandlerAttribute = (NetworkMessageHandlerAttribute)searchableAttribute;
				MethodInfo methodInfo = networkMessageHandlerAttribute.target as MethodInfo;
				if (!(methodInfo == null) && methodInfo.IsStatic)
				{
					networkMessageHandlerAttribute.messageHandler = (NetworkMessageDelegate)Delegate.CreateDelegate(typeof(NetworkMessageDelegate), methodInfo);
					if (networkMessageHandlerAttribute.messageHandler != null)
					{
						if (networkMessageHandlerAttribute.client)
						{
							NetworkMessageHandlerAttribute.clientMessageHandlers.Add(networkMessageHandlerAttribute);
							hashSet.Add(networkMessageHandlerAttribute.msgType);
						}
						if (networkMessageHandlerAttribute.server)
						{
							NetworkMessageHandlerAttribute.serverMessageHandlers.Add(networkMessageHandlerAttribute);
							hashSet.Add(networkMessageHandlerAttribute.msgType);
						}
					}
					if (networkMessageHandlerAttribute.messageHandler == null)
					{
						Debug.LogWarningFormat("Could not register message handler for {0}. The function signature is likely incorrect.", new object[]
						{
							methodInfo.Name
						});
					}
					if (!networkMessageHandlerAttribute.client && !networkMessageHandlerAttribute.server)
					{
						Debug.LogWarningFormat("Could not register message handler for {0}. It is marked as neither server nor client.", new object[]
						{
							methodInfo.Name
						});
					}
				}
			}
			for (short num = 48; num < 78; num += 1)
			{
				if (!hashSet.Contains(num))
				{
					Debug.LogWarningFormat("Network message MsgType.Highest + {0} is unregistered.", new object[]
					{
						(int)(num - 47)
					});
				}
			}
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00127570 File Offset: 0x00125770
		public static void RegisterServerMessages()
		{
			foreach (NetworkMessageHandlerAttribute networkMessageHandlerAttribute in NetworkMessageHandlerAttribute.serverMessageHandlers)
			{
				NetworkServer.RegisterHandler(networkMessageHandlerAttribute.msgType, networkMessageHandlerAttribute.messageHandler);
			}
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x001275CC File Offset: 0x001257CC
		public static void RegisterClientMessages(NetworkClient client)
		{
			foreach (NetworkMessageHandlerAttribute networkMessageHandlerAttribute in NetworkMessageHandlerAttribute.clientMessageHandlers)
			{
				client.RegisterHandler(networkMessageHandlerAttribute.msgType, networkMessageHandlerAttribute.messageHandler);
			}
		}

		// Token: 0x040044F8 RID: 17656
		public short msgType;

		// Token: 0x040044F9 RID: 17657
		public bool server;

		// Token: 0x040044FA RID: 17658
		public bool client;

		// Token: 0x040044FB RID: 17659
		private NetworkMessageDelegate messageHandler;

		// Token: 0x040044FC RID: 17660
		private static List<NetworkMessageHandlerAttribute> clientMessageHandlers = new List<NetworkMessageHandlerAttribute>();

		// Token: 0x040044FD RID: 17661
		private static List<NetworkMessageHandlerAttribute> serverMessageHandlers = new List<NetworkMessageHandlerAttribute>();
	}
}
