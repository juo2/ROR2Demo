using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000586 RID: 1414
	public static class DevCommands
	{
		// Token: 0x0600195C RID: 6492 RVA: 0x0006D921 File Offset: 0x0006BB21
		private static void AddTokenIfDefault(List<string> lines, string token)
		{
			if (!string.IsNullOrEmpty(token) && Language.GetString(token) == token)
			{
				lines.Add(string.Format("\t\t\"{0}\": \"{0}\",", token));
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006D948 File Offset: 0x0006BB48
		[ConCommand(commandName = "language_generate_tokens", flags = ConVarFlags.None, helpText = "Generates default token definitions to be inserted into a JSON language file.")]
		private static void CCLanguageGenerateTokens(ConCommandArgs args)
		{
			List<string> list = new List<string>();
			foreach (ItemDef itemDef in ItemCatalog.allItems.Select(new Func<ItemIndex, ItemDef>(ItemCatalog.GetItemDef)))
			{
				DevCommands.AddTokenIfDefault(list, itemDef.nameToken);
				DevCommands.AddTokenIfDefault(list, itemDef.pickupToken);
				DevCommands.AddTokenIfDefault(list, itemDef.descriptionToken);
			}
			list.Add("\r\n");
			foreach (EquipmentDef equipmentDef in EquipmentCatalog.allEquipment.Select(new Func<EquipmentIndex, EquipmentDef>(EquipmentCatalog.GetEquipmentDef)))
			{
				DevCommands.AddTokenIfDefault(list, equipmentDef.nameToken);
				DevCommands.AddTokenIfDefault(list, equipmentDef.pickupToken);
				DevCommands.AddTokenIfDefault(list, equipmentDef.descriptionToken);
			}
			Debug.Log(string.Join("\r\n", list));
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0006DA5C File Offset: 0x0006BC5C
		[ConCommand(commandName = "rng_test_roll", flags = ConVarFlags.None, helpText = "Tests the RNG. First argument is a percent chance, second argument is a number of rolls to perform. Result is the average number of rolls that passed.")]
		private static void CCTestRng(ConCommandArgs args)
		{
			float argFloat = args.GetArgFloat(0);
			ulong argULong = args.GetArgULong(1);
			ulong num = 0UL;
			for (ulong num2 = 0UL; num2 < argULong; num2 += 1UL)
			{
				if (RoR2Application.rng.RangeFloat(0f, 100f) < argFloat)
				{
					num += 1UL;
				}
			}
			Debug.Log(num / argULong * 100.0);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0006DAC4 File Offset: 0x0006BCC4
		[ConCommand(commandName = "getpos", flags = ConVarFlags.None, helpText = "Prints the current position of the sender's body.")]
		private static void CCGetPos(ConCommandArgs args)
		{
			Vector3 position = args.GetSenderBody().transform.position;
			Debug.LogFormat("{0} {1} {2}", new object[]
			{
				position.x,
				position.y,
				position.z
			});
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0006DB20 File Offset: 0x0006BD20
		[ConCommand(commandName = "setpos", flags = ConVarFlags.Cheat, helpText = "Teleports the sender's body to the specified position.")]
		private static void CCSetPos(ConCommandArgs args)
		{
			Component senderBody = args.GetSenderBody();
			Vector3 newPosition = new Vector3(args.GetArgFloat(0), args.GetArgFloat(1), args.GetArgFloat(2));
			TeleportHelper.TeleportGameObject(senderBody.gameObject, newPosition);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0006DB60 File Offset: 0x0006BD60
		[ConCommand(commandName = "create_object_from_resources", flags = (ConVarFlags.ExecuteOnServer | ConVarFlags.Cheat), helpText = "Instantiates an object from the Resources folder where the sender is looking.")]
		private static void CreateObjectFromResources(ConCommandArgs args)
		{
			Component senderBody = args.GetSenderBody();
			GameObject gameObject = LegacyResourcesAPI.Load<GameObject>(args.GetArgString(0));
			if (!gameObject)
			{
				throw new ConCommandException("Prefab could not be found at the specified path. Argument must be a Resources/-relative path to a prefab.");
			}
			RaycastHit raycastHit;
			if (senderBody.GetComponent<InputBankTest>().GetAimRaycast(float.PositiveInfinity, out raycastHit))
			{
				Vector3 point = raycastHit.point;
				Quaternion identity = Quaternion.identity;
				NetworkServer.Spawn(UnityEngine.Object.Instantiate<GameObject>(gameObject, point, identity));
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0006DBC4 File Offset: 0x0006BDC4
		[ConCommand(commandName = "resources_load_async_test", flags = ConVarFlags.None, helpText = "Tests Resources.LoadAsync. Loads the asset at the specified path and prints out the results of the operation.")]
		private static void CCResourcesLoadAsyncTest(ConCommandArgs args)
		{
			DevCommands.<>c__DisplayClass6_0 CS$<>8__locals1 = new DevCommands.<>c__DisplayClass6_0();
			CS$<>8__locals1.path = args.GetArgString(0);
			CS$<>8__locals1.request = Resources.LoadAsync(CS$<>8__locals1.path);
			CS$<>8__locals1.request.completed += CS$<>8__locals1.<CCResourcesLoadAsyncTest>g__Check|0;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0006DC0D File Offset: 0x0006BE0D
		private static UnityEngine.Object FindObjectFromInstanceID(int instanceId)
		{
			return (UnityEngine.Object)typeof(UnityEngine.Object).GetMethod("FindObjectFromInstanceID", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[]
			{
				instanceId
			});
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0006DC40 File Offset: 0x0006BE40
		[ConCommand(commandName = "dump_object_info", flags = ConVarFlags.None, helpText = "Prints debug info about the object with the provided instance ID.")]
		private static void CCDumpObjectInfo(ConCommandArgs args)
		{
			int argInt = args.GetArgInt(0);
			UnityEngine.Object @object = DevCommands.FindObjectFromInstanceID(argInt);
			if (!@object)
			{
				throw new Exception(string.Format("Object is not valid. objectInstanceId={0}", argInt));
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(@object.name);
			stringBuilder.AppendLine(string.Format("  instanceId={0}", @object.GetInstanceID()));
			stringBuilder.AppendLine("  type=" + @object.GetType().FullName);
			GameObject gameObject = null;
			GameObject gameObject2;
			Component component;
			if ((gameObject2 = (@object as GameObject)) != null)
			{
				gameObject = gameObject2;
			}
			else if ((component = (@object as Component)) != null)
			{
				gameObject = component.gameObject;
			}
			if (gameObject)
			{
				stringBuilder.Append("  scene=\"" + gameObject.scene.name + "\"");
				stringBuilder.Append("  transformPath=" + Util.BuildPrefabTransformPath(gameObject.transform.root, gameObject.transform, false, true));
			}
			args.Log(stringBuilder.ToString());
		}

		// Token: 0x02000587 RID: 1415
		private class CvSvNetLogObjectIds : ToggleVirtualConVar
		{
			// Token: 0x06001965 RID: 6501 RVA: 0x0006DD4E File Offset: 0x0006BF4E
			public CvSvNetLogObjectIds(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				this.monitoredField = typeof(NetworkIdentity).GetField("s_NextNetworkId", BindingFlags.Static | BindingFlags.NonPublic);
			}

			// Token: 0x06001966 RID: 6502 RVA: 0x0006DD78 File Offset: 0x0006BF78
			protected override void OnEnable()
			{
				RoR2Application.onFixedUpdate += this.Update;
				RoR2Application.onUpdate += this.Update;
				this.writer = new StreamWriter("net_id_log.txt", false);
				this.highestObservedId = this.GetCurrentHighestID();
			}

			// Token: 0x06001967 RID: 6503 RVA: 0x0006DDC4 File Offset: 0x0006BFC4
			protected override void OnDisable()
			{
				this.writer.Dispose();
				this.writer = null;
				RoR2Application.onUpdate -= this.Update;
				RoR2Application.onFixedUpdate -= this.Update;
			}

			// Token: 0x06001968 RID: 6504 RVA: 0x0006DDFA File Offset: 0x0006BFFA
			private uint GetCurrentHighestID()
			{
				return (uint)this.monitoredField.GetValue(null);
			}

			// Token: 0x06001969 RID: 6505 RVA: 0x0006DE10 File Offset: 0x0006C010
			private void Update()
			{
				if (!NetworkServer.active)
				{
					return;
				}
				uint currentHighestID = this.GetCurrentHighestID();
				while (this.highestObservedId < currentHighestID)
				{
					GameObject gameObject = NetworkServer.FindLocalObject(new NetworkInstanceId(this.highestObservedId));
					this.writer.WriteLine(string.Format("[{0, 0:D10}]={1}", this.highestObservedId, gameObject ? gameObject.name : "null"));
					this.highestObservedId += 1U;
				}
			}

			// Token: 0x04001FC2 RID: 8130
			private static readonly DevCommands.CvSvNetLogObjectIds instance = new DevCommands.CvSvNetLogObjectIds("sv_net_log_object_ids", ConVarFlags.None, "0", "Logs objects associated with each network id to net_id_log.txt as encountered by the server.");

			// Token: 0x04001FC3 RID: 8131
			private uint highestObservedId;

			// Token: 0x04001FC4 RID: 8132
			private FieldInfo monitoredField;

			// Token: 0x04001FC5 RID: 8133
			private TextWriter writer;
		}
	}
}
