using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.Networking;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A20 RID: 2592
	public class RunReport
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000F7938 File Offset: 0x000F5B38
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x000F7955 File Offset: 0x000F5B55
		public string gameModeName
		{
			get
			{
				Run gameMode = this.gameMode;
				return ((gameMode != null) ? gameMode.name : null) ?? "InvalidGameMode";
			}
			set
			{
				this.gameMode = GameModeCatalog.FindGameModePrefabComponent(value);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000F7963 File Offset: 0x000F5B63
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x000F7970 File Offset: 0x000F5B70
		public Run gameMode
		{
			get
			{
				return GameModeCatalog.GetGameModePrefabComponent(this.gameModeIndex);
			}
			set
			{
				this.gameModeIndex = ((value != null) ? value.gameModeIndex : GameModeIndex.Invalid);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000F7984 File Offset: 0x000F5B84
		public int playerInfoCount
		{
			get
			{
				return this.playerInfos.Length;
			}
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000F798E File Offset: 0x000F5B8E
		[NotNull]
		public RunReport.PlayerInfo GetPlayerInfo(int i)
		{
			return this.playerInfos[i];
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000F7998 File Offset: 0x000F5B98
		[CanBeNull]
		public RunReport.PlayerInfo GetPlayerInfoSafe(int i)
		{
			return ArrayUtils.GetSafe<RunReport.PlayerInfo>(this.playerInfos, i);
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000F79A8 File Offset: 0x000F5BA8
		public int FindPlayerIndex(LocalUser localUser)
		{
			if (localUser != null)
			{
				for (int i = 0; i < this.playerInfos.Length; i++)
				{
					if (this.playerInfos[i].localUser == localUser)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000F79E0 File Offset: 0x000F5BE0
		public int FindPlayerIndex([CanBeNull] UserProfile userProfile)
		{
			if (userProfile != null)
			{
				for (int i = 0; i < this.playerInfos.Length; i++)
				{
					if (string.Equals(userProfile.fileName, this.playerInfos[i].userProfileFileName, StringComparison.OrdinalIgnoreCase))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000F7A21 File Offset: 0x000F5C21
		[CanBeNull]
		public RunReport.PlayerInfo FindPlayerInfo(LocalUser localUser)
		{
			return ArrayUtils.GetSafe<RunReport.PlayerInfo>(this.playerInfos, this.FindPlayerIndex(localUser));
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000F7A35 File Offset: 0x000F5C35
		[CanBeNull]
		public RunReport.PlayerInfo FindPlayerInfo([CanBeNull] UserProfile userProfile)
		{
			return ArrayUtils.GetSafe<RunReport.PlayerInfo>(this.playerInfos, this.FindPlayerIndex(userProfile));
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000F7A49 File Offset: 0x000F5C49
		[CanBeNull]
		public RunReport.PlayerInfo FindFirstPlayerInfo()
		{
			if (this.playerInfoCount <= 0)
			{
				return null;
			}
			return this.playerInfos[0];
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000F7A60 File Offset: 0x000F5C60
		public static RunReport Generate([NotNull] Run run, GameEndingDef gameEnding)
		{
			RunReport runReport = new RunReport();
			runReport.runGuid = run.GetUniqueId();
			runReport.gameModeIndex = GameModeCatalog.FindGameModeIndex(run.gameObject.name);
			runReport.seed = run.seed;
			runReport.runStartTimeUtc = run.GetStartTimeUtc();
			runReport.snapshotTimeUtc = DateTime.UtcNow;
			runReport.snapshotRunTime = Run.FixedTimeStamp.now;
			runReport.runStopwatchValue = run.GetRunStopwatch();
			runReport.gameEnding = gameEnding;
			runReport.ruleBook.Copy(run.ruleBook);
			runReport.playerInfos = new RunReport.PlayerInfo[PlayerCharacterMasterController.instances.Count];
			for (int i = 0; i < runReport.playerInfos.Length; i++)
			{
				runReport.playerInfos[i] = RunReport.PlayerInfo.Generate(PlayerCharacterMasterController.instances[i], gameEnding);
			}
			runReport.ResolveLocalInformation();
			return runReport;
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000F7B30 File Offset: 0x000F5D30
		private void ResolveLocalInformation()
		{
			RunReport.PlayerInfo[] array = this.playerInfos;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResolveLocalInformation();
			}
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000F7B5C File Offset: 0x000F5D5C
		public void Write(NetworkWriter writer)
		{
			writer.WriteGuid(this.runGuid);
			writer.WritePackedIndex32((int)GameEndingCatalog.GetGameEndingIndex(this.gameEnding));
			writer.WritePackedIndex32((int)this.gameModeIndex);
			writer.Write(this.seed);
			NetworkDateTime networkDateTime = (NetworkDateTime)this.runStartTimeUtc;
			writer.Write(networkDateTime);
			networkDateTime = (NetworkDateTime)this.snapshotTimeUtc;
			writer.Write(networkDateTime);
			writer.Write(this.snapshotRunTime);
			writer.Write(this.runStopwatchValue);
			writer.Write(this.ruleBook);
			writer.Write((byte)this.playerInfos.Length);
			for (int i = 0; i < this.playerInfos.Length; i++)
			{
				this.playerInfos[i].Write(writer);
			}
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000F7C1C File Offset: 0x000F5E1C
		public void Read(NetworkReader reader)
		{
			this.runGuid = reader.ReadGuid();
			this.gameEnding = GameEndingCatalog.GetGameEndingDef((GameEndingIndex)reader.ReadPackedIndex32());
			this.gameModeIndex = (GameModeIndex)reader.ReadPackedIndex32();
			this.seed = reader.ReadUInt64();
			this.runStartTimeUtc = (DateTime)reader.ReadNetworkDateTime();
			this.snapshotTimeUtc = (DateTime)reader.ReadNetworkDateTime();
			this.snapshotRunTime = reader.ReadFixedTimeStamp();
			this.runStopwatchValue = reader.ReadSingle();
			reader.ReadRuleBook(this.ruleBook);
			int newSize = (int)reader.ReadByte();
			Array.Resize<RunReport.PlayerInfo>(ref this.playerInfos, newSize);
			for (int i = 0; i < this.playerInfos.Length; i++)
			{
				if (this.playerInfos[i] == null)
				{
					this.playerInfos[i] = new RunReport.PlayerInfo();
				}
				this.playerInfos[i].Read(reader);
			}
			Array.Sort<RunReport.PlayerInfo>(this.playerInfos, delegate(RunReport.PlayerInfo a, RunReport.PlayerInfo b)
			{
				if (a.isLocalPlayer == b.isLocalPlayer)
				{
					if (a.isLocalPlayer)
					{
						return b.localPlayerIndex - a.localPlayerIndex;
					}
					return 0;
				}
				else
				{
					if (!a.isLocalPlayer)
					{
						return 1;
					}
					return -1;
				}
			});
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x000F7D1C File Offset: 0x000F5F1C
		public static void ToXml(XElement element, RunReport runReport)
		{
			element.RemoveAll();
			element.Add(HGXml.ToXml<string>("version", "2"));
			element.Add(HGXml.ToXml<Guid>("runGuid", runReport.runGuid));
			element.Add(HGXml.ToXml<string>("gameModeName", runReport.gameModeName));
			element.Add(HGXml.ToXml<GameEndingDef>("gameEnding", runReport.gameEnding));
			element.Add(HGXml.ToXml<ulong>("seed", runReport.seed));
			element.Add(HGXml.ToXml<DateTime>("runStartTimeUtc", runReport.runStartTimeUtc));
			element.Add(HGXml.ToXml<DateTime>("snapshotTimeUtc", runReport.snapshotTimeUtc));
			element.Add(HGXml.ToXml<Run.FixedTimeStamp>("snapshotRunTime", runReport.snapshotRunTime));
			element.Add(HGXml.ToXml<float>("runStopwatchValue", runReport.runStopwatchValue));
			element.Add(HGXml.ToXml<RuleBook>("ruleBook", runReport.ruleBook));
			element.Add(HGXml.ToXml<RunReport.PlayerInfo[]>("playerInfos", runReport.playerInfos));
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000F7E20 File Offset: 0x000F6020
		public static bool FromXml(XElement element, ref RunReport runReport)
		{
			string text = "NO_VERSION";
			XElement xelement = element.Element("version");
			if (xelement != null)
			{
				xelement.Deserialize(ref text);
			}
			if (text != "2" && !(text == "1"))
			{
				Debug.LogFormat("Could not load RunReport with non-upgradeable version \"{0}\".", new object[]
				{
					text
				});
				runReport = null;
				return false;
			}
			XElement xelement2 = element.Element("runGuid");
			if (xelement2 != null)
			{
				xelement2.Deserialize(ref runReport.runGuid);
			}
			string gameModeName = runReport.gameModeName;
			XElement xelement3 = element.Element("gameModeName");
			if (xelement3 != null)
			{
				xelement3.Deserialize(ref gameModeName);
			}
			runReport.gameModeName = gameModeName;
			XElement xelement4 = element.Element("gameEnding");
			if (xelement4 != null)
			{
				xelement4.Deserialize(ref runReport.gameEnding);
			}
			XElement xelement5 = element.Element("seed");
			if (xelement5 != null)
			{
				xelement5.Deserialize(ref runReport.seed);
			}
			XElement xelement6 = element.Element("runStartTimeUtc");
			if (xelement6 != null)
			{
				xelement6.Deserialize(ref runReport.runStartTimeUtc);
			}
			XElement xelement7 = element.Element("snapshotTimeUtc");
			if (xelement7 != null)
			{
				xelement7.Deserialize(ref runReport.snapshotTimeUtc);
			}
			XElement xelement8 = element.Element("snapshotRunTime");
			if (xelement8 != null)
			{
				xelement8.Deserialize(ref runReport.snapshotRunTime);
			}
			XElement xelement9 = element.Element("runStopwatchValue");
			if (xelement9 != null)
			{
				xelement9.Deserialize(ref runReport.runStopwatchValue);
			}
			XElement xelement10 = element.Element("ruleBook");
			if (xelement10 != null)
			{
				xelement10.Deserialize(ref runReport.ruleBook);
			}
			XElement xelement11 = element.Element("playerInfos");
			if (xelement11 != null)
			{
				xelement11.Deserialize(ref runReport.playerInfos);
			}
			return true;
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000F7FE4 File Offset: 0x000F61E4
		[SystemInitializer(new Type[]
		{
			typeof(AchievementManager),
			typeof(BodyCatalog),
			typeof(EquipmentCatalog),
			typeof(GameModeCatalog),
			typeof(ItemCatalog),
			typeof(SceneCatalog),
			typeof(StatDef),
			typeof(SurvivorCatalog),
			typeof(UnlockableCatalog)
		})]
		private static void Init()
		{
			RunReport.runReportsFolder = Application.dataPath + "/RunReports/";
			HGXml.Register<RunReport>(new HGXml.Serializer<RunReport>(RunReport.ToXml), new HGXml.Deserializer<RunReport>(RunReport.FromXml));
			HGXml.Register<RunReport.PlayerInfo>(new HGXml.Serializer<RunReport.PlayerInfo>(RunReport.PlayerInfo.ToXml), new HGXml.Deserializer<RunReport.PlayerInfo>(RunReport.PlayerInfo.FromXml));
			HGXml.Register<RunReport.PlayerInfo[]>(new HGXml.Serializer<RunReport.PlayerInfo[]>(RunReport.PlayerInfo.ArrayToXml), new HGXml.Deserializer<RunReport.PlayerInfo[]>(RunReport.PlayerInfo.ArrayFromXml));
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000F805C File Offset: 0x000F625C
		[NotNull]
		private static string FileNameToPath([NotNull] string fileName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}.xml", RunReport.runReportsFolder, fileName);
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000F8074 File Offset: 0x000F6274
		[CanBeNull]
		public static RunReport Load([NotNull] string fileName)
		{
			string text = RunReport.FileNameToPath(fileName);
			RunReport result;
			try
			{
				XElement xelement = XDocument.Load(text).Element("RunReport");
				if (xelement == null)
				{
					Debug.LogFormat("Could not load RunReport {0}: {1}", new object[]
					{
						text,
						"File is malformed."
					});
					result = null;
				}
				else
				{
					RunReport runReport = new RunReport();
					RunReport.FromXml(xelement, ref runReport);
					result = runReport;
				}
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Could not load RunReport {0}: {1}", new object[]
				{
					text,
					ex.Message
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000F810C File Offset: 0x000F630C
		public static bool Save([NotNull] RunReport runReport, [NotNull] string fileName)
		{
			string text = RunReport.FileNameToPath(fileName);
			bool result;
			try
			{
				if (!Directory.Exists(RunReport.runReportsFolder))
				{
					Directory.CreateDirectory(RunReport.runReportsFolder);
				}
				XDocument xdocument = new XDocument();
				xdocument.Add(HGXml.ToXml<RunReport>("RunReport", runReport));
				xdocument.Save(text);
				result = true;
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Could not save RunReport {0}: {1}", new object[]
				{
					text,
					ex.Message
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000F818C File Offset: 0x000F638C
		public static void TestSerialization(RunReport runReport)
		{
			NetworkWriter networkWriter = new NetworkWriter();
			runReport.Write(networkWriter);
			NetworkReader reader = new NetworkReader(networkWriter.AsArray());
			new RunReport().Read(reader);
		}

		// Token: 0x04003B71 RID: 15217
		private const string currentXmlVersion = "2";

		// Token: 0x04003B72 RID: 15218
		public Guid runGuid;

		// Token: 0x04003B73 RID: 15219
		private GameModeIndex gameModeIndex = GameModeIndex.Invalid;

		// Token: 0x04003B74 RID: 15220
		public GameEndingDef gameEnding;

		// Token: 0x04003B75 RID: 15221
		public ulong seed;

		// Token: 0x04003B76 RID: 15222
		public DateTime runStartTimeUtc;

		// Token: 0x04003B77 RID: 15223
		public DateTime snapshotTimeUtc;

		// Token: 0x04003B78 RID: 15224
		public Run.FixedTimeStamp snapshotRunTime;

		// Token: 0x04003B79 RID: 15225
		public float runStopwatchValue;

		// Token: 0x04003B7A RID: 15226
		public RuleBook ruleBook = new RuleBook();

		// Token: 0x04003B7B RID: 15227
		private RunReport.PlayerInfo[] playerInfos = Array.Empty<RunReport.PlayerInfo>();

		// Token: 0x04003B7C RID: 15228
		private static string runReportsFolder;

		// Token: 0x02000A21 RID: 2593
		public class PlayerInfo
		{
			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06003BED RID: 15341 RVA: 0x000F81E2 File Offset: 0x000F63E2
			[CanBeNull]
			public LocalUser localUser
			{
				get
				{
					if (!this.networkUser)
					{
						return null;
					}
					return this.networkUser.localUser;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000F81FE File Offset: 0x000F63FE
			public bool isLocalPlayer
			{
				get
				{
					return this.localPlayerIndex >= 0;
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000F820C File Offset: 0x000F640C
			// (set) Token: 0x06003BF0 RID: 15344 RVA: 0x000F8233 File Offset: 0x000F6433
			public string bodyName
			{
				get
				{
					GameObject bodyPrefab = BodyCatalog.GetBodyPrefab(this.bodyIndex);
					return ((bodyPrefab != null) ? bodyPrefab.gameObject.name : null) ?? "InvalidBody";
				}
				set
				{
					this.bodyIndex = BodyCatalog.FindBodyIndex(value);
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000F8241 File Offset: 0x000F6441
			// (set) Token: 0x06003BF2 RID: 15346 RVA: 0x000F8268 File Offset: 0x000F6468
			public string killerBodyName
			{
				get
				{
					GameObject bodyPrefab = BodyCatalog.GetBodyPrefab(this.killerBodyIndex);
					return ((bodyPrefab != null) ? bodyPrefab.gameObject.name : null) ?? "InvalidBody";
				}
				set
				{
					this.killerBodyIndex = BodyCatalog.FindBodyIndex(value);
				}
			}

			// Token: 0x06003BF3 RID: 15347 RVA: 0x000F8278 File Offset: 0x000F6478
			public void Write(NetworkWriter writer)
			{
				writer.WriteBodyIndex(this.bodyIndex);
				writer.WriteBodyIndex(this.killerBodyIndex);
				writer.Write(this.isDead);
				writer.Write(this.master ? this.master.gameObject : null);
				this.statSheet.Write(writer);
				writer.WritePackedUInt32((uint)this.itemAcquisitionOrder.Length);
				for (int i = 0; i < this.itemAcquisitionOrder.Length; i++)
				{
					writer.Write(this.itemAcquisitionOrder[i]);
				}
				writer.WriteItemStacks(this.itemStacks);
				writer.WritePackedUInt32((uint)this.equipment.Length);
				for (int j = 0; j < this.equipment.Length; j++)
				{
					writer.Write(this.equipment[j]);
				}
				writer.Write(this.finalMessageToken);
			}

			// Token: 0x06003BF4 RID: 15348 RVA: 0x000F834C File Offset: 0x000F654C
			public void Read(NetworkReader reader)
			{
				this.bodyIndex = reader.ReadBodyIndex();
				this.killerBodyIndex = reader.ReadBodyIndex();
				this.isDead = reader.ReadBoolean();
				GameObject gameObject = reader.ReadGameObject();
				this.master = (gameObject ? gameObject.GetComponent<CharacterMaster>() : null);
				this.statSheet.Read(reader);
				int newSize = (int)reader.ReadPackedUInt32();
				Array.Resize<ItemIndex>(ref this.itemAcquisitionOrder, newSize);
				for (int i = 0; i < this.itemAcquisitionOrder.Length; i++)
				{
					ItemIndex itemIndex = reader.ReadItemIndex();
					this.itemAcquisitionOrder[i] = itemIndex;
				}
				reader.ReadItemStacks(this.itemStacks);
				int newSize2 = (int)reader.ReadPackedUInt32();
				Array.Resize<EquipmentIndex>(ref this.equipment, newSize2);
				for (int j = 0; j < this.equipment.Length; j++)
				{
					EquipmentIndex equipmentIndex = reader.ReadEquipmentIndex();
					this.equipment[j] = equipmentIndex;
				}
				this.finalMessageToken = reader.ReadString();
				this.ResolveLocalInformation();
			}

			// Token: 0x06003BF5 RID: 15349 RVA: 0x000F843C File Offset: 0x000F663C
			public void ResolveLocalInformation()
			{
				this.name = Util.GetBestMasterName(this.master);
				PlayerCharacterMasterController playerCharacterMasterController = null;
				if (this.master)
				{
					playerCharacterMasterController = this.master.GetComponent<PlayerCharacterMasterController>();
				}
				this.networkUser = null;
				if (playerCharacterMasterController)
				{
					this.networkUser = playerCharacterMasterController.networkUser;
				}
				this.localPlayerIndex = -1;
				this.userProfileFileName = string.Empty;
				if (this.networkUser && this.networkUser.localUser != null)
				{
					this.localPlayerIndex = this.networkUser.localUser.id;
					this.userProfileFileName = this.networkUser.localUser.userProfile.fileName;
				}
			}

			// Token: 0x06003BF6 RID: 15350 RVA: 0x000F84F0 File Offset: 0x000F66F0
			public static RunReport.PlayerInfo Generate(PlayerCharacterMasterController playerCharacterMasterController, GameEndingDef gameEnding)
			{
				CharacterMaster characterMaster = playerCharacterMasterController.master;
				Inventory inventory = characterMaster.inventory;
				PlayerStatsComponent component = playerCharacterMasterController.GetComponent<PlayerStatsComponent>();
				RunReport.PlayerInfo playerInfo = new RunReport.PlayerInfo();
				playerInfo.networkUser = playerCharacterMasterController.networkUser;
				playerInfo.master = characterMaster;
				playerInfo.bodyIndex = BodyCatalog.FindBodyIndex(characterMaster.bodyPrefab);
				playerInfo.killerBodyIndex = characterMaster.GetKillerBodyIndex();
				playerInfo.isDead = characterMaster.lostBodyToDeath;
				if (playerInfo.killerBodyIndex == BodyIndex.None && gameEnding && gameEnding.defaultKillerOverride)
				{
					playerInfo.killerBodyIndex = BodyCatalog.FindBodyIndex(gameEnding.defaultKillerOverride);
				}
				StatSheet.Copy(component.currentStats, playerInfo.statSheet);
				playerInfo.itemAcquisitionOrder = inventory.itemAcquisitionOrder.ToArray();
				ItemIndex itemIndex = (ItemIndex)0;
				ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
				while (itemIndex < itemCount)
				{
					playerInfo.itemStacks[(int)itemIndex] = inventory.GetItemCount(itemIndex);
					itemIndex++;
				}
				playerInfo.equipment = new EquipmentIndex[inventory.GetEquipmentSlotCount()];
				uint num = 0U;
				while ((ulong)num < (ulong)((long)playerInfo.equipment.Length))
				{
					playerInfo.equipment[(int)num] = inventory.GetEquipment(num).equipmentIndex;
					num += 1U;
				}
				playerInfo.finalMessageToken = playerCharacterMasterController.finalMessageTokenServer;
				return playerInfo;
			}

			// Token: 0x06003BF7 RID: 15351 RVA: 0x000F8614 File Offset: 0x000F6814
			public static void ToXml(XElement element, RunReport.PlayerInfo playerInfo)
			{
				element.RemoveAll();
				element.Add(HGXml.ToXml<string>("name", playerInfo.name));
				element.Add(HGXml.ToXml<string>("bodyName", playerInfo.bodyName));
				element.Add(HGXml.ToXml<string>("killerBodyName", playerInfo.killerBodyName));
				element.Add(HGXml.ToXml<bool>("isDead", playerInfo.isDead));
				element.Add(HGXml.ToXml<StatSheet>("statSheet", playerInfo.statSheet));
				element.Add(HGXml.ToXml<ItemIndex[]>("itemAcquisitionOrder", playerInfo.itemAcquisitionOrder));
				element.Add(HGXml.ToXml<int[]>("itemStacks", playerInfo.itemStacks, RunReport.PlayerInfo.itemStacksRules));
				element.Add(HGXml.ToXml<EquipmentIndex[]>("equipment", playerInfo.equipment, RunReport.PlayerInfo.equipmentRules));
				element.Add(HGXml.ToXml<string>("finalMessageToken", playerInfo.finalMessageToken));
				element.Add(HGXml.ToXml<int>("localPlayerIndex", playerInfo.localPlayerIndex));
				element.Add(HGXml.ToXml<string>("userProfileFileName", playerInfo.userProfileFileName));
			}

			// Token: 0x06003BF8 RID: 15352 RVA: 0x000F8724 File Offset: 0x000F6924
			public static bool FromXml(XElement element, ref RunReport.PlayerInfo playerInfo)
			{
				playerInfo = new RunReport.PlayerInfo();
				XElement xelement = element.Element("name");
				if (xelement != null)
				{
					xelement.Deserialize(ref playerInfo.name);
				}
				string bodyName = playerInfo.bodyName;
				XElement xelement2 = element.Element("bodyName");
				if (xelement2 != null)
				{
					xelement2.Deserialize(ref bodyName);
				}
				playerInfo.bodyName = bodyName;
				string killerBodyName = playerInfo.killerBodyName;
				XElement xelement3 = element.Element("killerBodyName");
				if (xelement3 != null)
				{
					xelement3.Deserialize(ref killerBodyName);
				}
				playerInfo.killerBodyName = killerBodyName;
				XElement xelement4 = element.Element("isDead");
				if (xelement4 != null)
				{
					xelement4.Deserialize(ref playerInfo.isDead);
				}
				XElement xelement5 = element.Element("statSheet");
				if (xelement5 != null)
				{
					xelement5.Deserialize(ref playerInfo.statSheet);
				}
				XElement xelement6 = element.Element("itemAcquisitionOrder");
				if (xelement6 != null)
				{
					xelement6.Deserialize(ref playerInfo.itemAcquisitionOrder);
				}
				XElement xelement7 = element.Element("itemStacks");
				if (xelement7 != null)
				{
					xelement7.Deserialize(ref playerInfo.itemStacks, RunReport.PlayerInfo.itemStacksRules);
				}
				XElement xelement8 = element.Element("equipment");
				if (xelement8 != null)
				{
					xelement8.Deserialize(ref playerInfo.equipment, RunReport.PlayerInfo.equipmentRules);
				}
				XElement xelement9 = element.Element("finalMessageToken");
				if (xelement9 != null)
				{
					xelement9.Deserialize(ref playerInfo.finalMessageToken);
				}
				XElement xelement10 = element.Element("localPlayerIndex");
				if (xelement10 != null)
				{
					xelement10.Deserialize(ref playerInfo.localPlayerIndex);
				}
				XElement xelement11 = element.Element("userProfileFileName");
				if (xelement11 != null)
				{
					xelement11.Deserialize(ref playerInfo.userProfileFileName);
				}
				return true;
			}

			// Token: 0x06003BF9 RID: 15353 RVA: 0x000F88D0 File Offset: 0x000F6AD0
			public static void ArrayToXml(XElement element, RunReport.PlayerInfo[] playerInfos)
			{
				element.RemoveAll();
				for (int i = 0; i < playerInfos.Length; i++)
				{
					element.Add(HGXml.ToXml<RunReport.PlayerInfo>("PlayerInfo", playerInfos[i]));
				}
			}

			// Token: 0x06003BFA RID: 15354 RVA: 0x000F8904 File Offset: 0x000F6B04
			public static bool ArrayFromXml(XElement element, ref RunReport.PlayerInfo[] playerInfos)
			{
				playerInfos = (from e in element.Elements()
				where e.Name == "PlayerInfo"
				select e).Select(delegate(XElement e)
				{
					RunReport.PlayerInfo result = null;
					HGXml.FromXml<RunReport.PlayerInfo>(e, ref result);
					return result;
				}).ToArray<RunReport.PlayerInfo>();
				return true;
			}

			// Token: 0x04003B7D RID: 15229
			[CanBeNull]
			public NetworkUser networkUser;

			// Token: 0x04003B7E RID: 15230
			[CanBeNull]
			public CharacterMaster master;

			// Token: 0x04003B7F RID: 15231
			public int localPlayerIndex = -1;

			// Token: 0x04003B80 RID: 15232
			public string name = string.Empty;

			// Token: 0x04003B81 RID: 15233
			public BodyIndex bodyIndex = BodyIndex.None;

			// Token: 0x04003B82 RID: 15234
			public BodyIndex killerBodyIndex = BodyIndex.None;

			// Token: 0x04003B83 RID: 15235
			public bool isDead;

			// Token: 0x04003B84 RID: 15236
			public StatSheet statSheet = StatSheet.New();

			// Token: 0x04003B85 RID: 15237
			public ItemIndex[] itemAcquisitionOrder = Array.Empty<ItemIndex>();

			// Token: 0x04003B86 RID: 15238
			public int[] itemStacks = ItemCatalog.RequestItemStackArray();

			// Token: 0x04003B87 RID: 15239
			public EquipmentIndex[] equipment = Array.Empty<EquipmentIndex>();

			// Token: 0x04003B88 RID: 15240
			public string finalMessageToken = string.Empty;

			// Token: 0x04003B89 RID: 15241
			public string userProfileFileName = string.Empty;

			// Token: 0x04003B8A RID: 15242
			private static readonly HGXml.SerializationRules<int[]> itemStacksRules = new HGXml.SerializationRules<int[]>
			{
				serializer = delegate(XElement element, int[] value)
				{
					element.RemoveAll();
					element.Add(from itemIndex in ItemCatalog.allItems
					where value[(int)itemIndex] > 0
					select new XElement(ItemCatalog.GetItemDef(itemIndex).name, value[(int)itemIndex]));
				},
				deserializer = delegate(XElement element, ref int[] value)
				{
					Array.Resize<int>(ref value, ItemCatalog.itemCount);
					for (ItemIndex itemIndex = (ItemIndex)0; itemIndex < (ItemIndex)ItemCatalog.itemCount; itemIndex++)
					{
						value[(int)itemIndex] = 0;
					}
					foreach (XElement xelement in element.Elements())
					{
						ItemIndex itemIndex2 = ItemCatalog.FindItemIndex(xelement.Name.LocalName);
						if (ItemCatalog.IsIndexValid(itemIndex2))
						{
							HGXml.FromXml<int>(xelement, ref value[(int)itemIndex2]);
						}
					}
					return true;
				}
			};

			// Token: 0x04003B8B RID: 15243
			private static readonly HGXml.SerializationRules<EquipmentIndex[]> equipmentRules = new HGXml.SerializationRules<EquipmentIndex[]>
			{
				serializer = delegate(XElement element, EquipmentIndex[] value)
				{
					element.Value = string.Join(" ", value.Select(delegate(EquipmentIndex equipmentIndex)
					{
						EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
						return ((equipmentDef != null) ? equipmentDef.name : null) ?? "None";
					}));
				},
				deserializer = delegate(XElement element, ref EquipmentIndex[] value)
				{
					value = element.Value.Split(new char[]
					{
						' '
					}).Select(new Func<string, EquipmentIndex>(EquipmentCatalog.FindEquipmentIndex)).ToArray<EquipmentIndex>();
					return true;
				}
			};
		}
	}
}
