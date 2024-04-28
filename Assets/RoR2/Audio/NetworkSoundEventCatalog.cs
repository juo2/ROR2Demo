using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Audio
{
	// Token: 0x02000E5C RID: 3676
	public static class NetworkSoundEventCatalog
	{
		// Token: 0x06005427 RID: 21543 RVA: 0x0015B4DE File Offset: 0x001596DE
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			NetworkSoundEventCatalog.SetNetworkSoundEvents(ContentManager.networkSoundEventDefs);
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0015B4EC File Offset: 0x001596EC
		public static void SetNetworkSoundEvents(NetworkSoundEventDef[] newEntries)
		{
			NetworkSoundEventCatalog.eventNameToIndexTable.Clear();
			NetworkSoundEventCatalog.eventIdToIndexTable.Clear();
			ArrayUtils.CloneTo<NetworkSoundEventDef>(newEntries, ref NetworkSoundEventCatalog.entries);
			Array.Sort<NetworkSoundEventDef>(NetworkSoundEventCatalog.entries, (NetworkSoundEventDef a, NetworkSoundEventDef b) => string.CompareOrdinal(a.name, b.name));
			for (int i = 0; i < NetworkSoundEventCatalog.entries.Length; i++)
			{
				NetworkSoundEventDef networkSoundEventDef = NetworkSoundEventCatalog.entries[i];
				networkSoundEventDef.index = (NetworkSoundEventIndex)i;
				networkSoundEventDef.akId = AkSoundEngine.GetIDFromString(networkSoundEventDef.eventName);
				if (networkSoundEventDef.akId == 0U)
				{
					Debug.LogErrorFormat("Error during network sound registration: Wwise event \"{0}\" does not exist.", new object[]
					{
						networkSoundEventDef.eventName
					});
				}
			}
			for (int j = 0; j < NetworkSoundEventCatalog.entries.Length; j++)
			{
				NetworkSoundEventDef networkSoundEventDef2 = NetworkSoundEventCatalog.entries[j];
				NetworkSoundEventCatalog.eventNameToIndexTable[networkSoundEventDef2.eventName] = networkSoundEventDef2.index;
				NetworkSoundEventCatalog.eventIdToIndexTable[networkSoundEventDef2.akId] = networkSoundEventDef2.index;
			}
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0015B5DC File Offset: 0x001597DC
		public static NetworkSoundEventIndex FindNetworkSoundEventIndex(string eventName)
		{
			NetworkSoundEventIndex result;
			if (NetworkSoundEventCatalog.eventNameToIndexTable.TryGetValue(eventName, out result))
			{
				return result;
			}
			return NetworkSoundEventIndex.Invalid;
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0015B5FC File Offset: 0x001597FC
		public static NetworkSoundEventIndex FindNetworkSoundEventIndex(uint akEventId)
		{
			NetworkSoundEventIndex result;
			if (NetworkSoundEventCatalog.eventIdToIndexTable.TryGetValue(akEventId, out result))
			{
				return result;
			}
			return NetworkSoundEventIndex.Invalid;
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0015B61B File Offset: 0x0015981B
		public static uint GetAkIdFromNetworkSoundEventIndex(NetworkSoundEventIndex eventIndex)
		{
			if (eventIndex == NetworkSoundEventIndex.Invalid)
			{
				return 0U;
			}
			return NetworkSoundEventCatalog.entries[(int)eventIndex].akId;
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00060D48 File Offset: 0x0005EF48
		public static void WriteNetworkSoundEventIndex(this NetworkWriter writer, NetworkSoundEventIndex networkSoundEventIndex)
		{
			writer.WritePackedUInt32((uint)(networkSoundEventIndex + 1));
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x00060D53 File Offset: 0x0005EF53
		public static NetworkSoundEventIndex ReadNetworkSoundEventIndex(this NetworkReader reader)
		{
			return (NetworkSoundEventIndex)(reader.ReadPackedUInt32() - 1U);
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0015B630 File Offset: 0x00159830
		public static string GetEventNameFromId(uint akEventId)
		{
			NetworkSoundEventIndex networkSoundEventIndex;
			if (NetworkSoundEventCatalog.eventIdToIndexTable.TryGetValue(akEventId, out networkSoundEventIndex))
			{
				return NetworkSoundEventCatalog.entries[(int)networkSoundEventIndex].eventName;
			}
			return null;
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0015B65A File Offset: 0x0015985A
		public static string GetEventNameFromNetworkIndex(NetworkSoundEventIndex networkSoundEventIndex)
		{
			NetworkSoundEventDef safe = ArrayUtils.GetSafe<NetworkSoundEventDef>(NetworkSoundEventCatalog.entries, (int)networkSoundEventIndex);
			if (safe == null)
			{
				return null;
			}
			return safe.eventName;
		}

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x06005430 RID: 21552 RVA: 0x0015B672 File Offset: 0x00159872
		// (remove) Token: 0x06005431 RID: 21553 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<NetworkSoundEventDef>> getSoundEventDefs
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<NetworkSoundEventDef>("RoR2.Audio.NetworkSoundCatalog.getSoundEventDefs", value, LegacyModContentPackProvider.instance.registrationContentPack.networkSoundEventDefs);
			}
			remove
			{
			}
		}

		// Token: 0x04004FF9 RID: 20473
		private static NetworkSoundEventDef[] entries = Array.Empty<NetworkSoundEventDef>();

		// Token: 0x04004FFA RID: 20474
		private static readonly Dictionary<string, NetworkSoundEventIndex> eventNameToIndexTable = new Dictionary<string, NetworkSoundEventIndex>();

		// Token: 0x04004FFB RID: 20475
		private static readonly Dictionary<uint, NetworkSoundEventIndex> eventIdToIndexTable = new Dictionary<uint, NetworkSoundEventIndex>();
	}
}
