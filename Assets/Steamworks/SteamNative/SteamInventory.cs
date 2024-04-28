using System;
using System.Linq;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000062 RID: 98
	internal class SteamInventory : IDisposable
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00004754 File Offset: 0x00002954
		internal SteamInventory(BaseSteamworks steamworks, IntPtr pointer)
		{
			this.steamworks = steamworks;
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64(pointer);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32(pointer);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32(pointer);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64(pointer);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac(pointer);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000047D1 File Offset: 0x000029D1
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000047E8 File Offset: 0x000029E8
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004804 File Offset: 0x00002A04
		public bool AddPromoItem(ref SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef)
		{
			return this.platform.ISteamInventory_AddPromoItem(ref pResultHandle.Value, itemDef.Value);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000481D File Offset: 0x00002A1D
		public bool AddPromoItems(ref SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint unArrayLength)
		{
			return this.platform.ISteamInventory_AddPromoItems(ref pResultHandle.Value, (from x in pArrayItemDefs
			select x.Value).ToArray<int>(), unArrayLength);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000485B File Offset: 0x00002A5B
		public bool CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected)
		{
			return this.platform.ISteamInventory_CheckResultSteamID(resultHandle.Value, steamIDExpected.Value);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004874 File Offset: 0x00002A74
		public bool ConsumeItem(ref SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity)
		{
			return this.platform.ISteamInventory_ConsumeItem(ref pResultHandle.Value, itemConsume.Value, unQuantity);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000488E File Offset: 0x00002A8E
		public bool DeserializeResult(ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE)
		{
			return this.platform.ISteamInventory_DeserializeResult(ref pOutResultHandle.Value, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000048A5 File Offset: 0x00002AA5
		public void DestroyResult(SteamInventoryResult_t resultHandle)
		{
			this.platform.ISteamInventory_DestroyResult(resultHandle.Value);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000048B8 File Offset: 0x00002AB8
		public bool ExchangeItems(ref SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayGenerate, uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, SteamItemInstanceID_t[] pArrayDestroy, uint[] punArrayDestroyQuantity, uint unArrayDestroyLength)
		{
			return this.platform.ISteamInventory_ExchangeItems(ref pResultHandle.Value, (from x in pArrayGenerate
			select x.Value).ToArray<int>(), punArrayGenerateQuantity, unArrayGenerateLength, (from x in pArrayDestroy
			select x.Value).ToArray<ulong>(), punArrayDestroyQuantity, unArrayDestroyLength);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004932 File Offset: 0x00002B32
		public bool GenerateItems(ref SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			return this.platform.ISteamInventory_GenerateItems(ref pResultHandle.Value, (from x in pArrayItemDefs
			select x.Value).ToArray<int>(), punArrayQuantity, unArrayLength);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004972 File Offset: 0x00002B72
		public bool GetAllItems(ref SteamInventoryResult_t pResultHandle)
		{
			return this.platform.ISteamInventory_GetAllItems(ref pResultHandle.Value);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004988 File Offset: 0x00002B88
		public unsafe SteamItemDef_t[] GetEligiblePromoItemDefinitionIDs(CSteamID steamID)
		{
			uint num = 0U;
			if (!this.platform.ISteamInventory_GetEligiblePromoItemDefinitionIDs(steamID.Value, IntPtr.Zero, out num) || num == 0U)
			{
				return null;
			}
			SteamItemDef_t[] array = new SteamItemDef_t[num];
			SteamItemDef_t[] array2;
			void* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = (void*)(&array2[0]);
			}
			if (!this.platform.ISteamInventory_GetEligiblePromoItemDefinitionIDs(steamID.Value, (IntPtr)value, out num))
			{
				return null;
			}
			return array;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000049F4 File Offset: 0x00002BF4
		public unsafe SteamItemDef_t[] GetItemDefinitionIDs()
		{
			uint num = 0U;
			if (!this.platform.ISteamInventory_GetItemDefinitionIDs(IntPtr.Zero, out num) || num == 0U)
			{
				return null;
			}
			SteamItemDef_t[] array = new SteamItemDef_t[num];
			SteamItemDef_t[] array2;
			void* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = (void*)(&array2[0]);
			}
			if (!this.platform.ISteamInventory_GetItemDefinitionIDs((IntPtr)value, out num))
			{
				return null;
			}
			return array;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004A54 File Offset: 0x00002C54
		public bool GetItemDefinitionProperty(SteamItemDef_t iDefinition, string pchPropertyName, out string pchValueBuffer)
		{
			pchValueBuffer = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint num = 4096U;
			bool flag = this.platform.ISteamInventory_GetItemDefinitionProperty(iDefinition.Value, pchPropertyName, stringBuilder, out num);
			if (!flag)
			{
				return flag;
			}
			pchValueBuffer = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004A9A File Offset: 0x00002C9A
		public bool GetItemPrice(SteamItemDef_t iDefinition, out ulong pPrice)
		{
			return this.platform.ISteamInventory_GetItemPrice(iDefinition.Value, out pPrice);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004AAE File Offset: 0x00002CAE
		public bool GetItemsByID(ref SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs)
		{
			return this.platform.ISteamInventory_GetItemsByID(ref pResultHandle.Value, (from x in pInstanceIDs
			select x.Value).ToArray<ulong>(), unCountInstanceIDs);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004AEC File Offset: 0x00002CEC
		public bool GetItemsWithPrices(IntPtr pArrayItemDefs, IntPtr pPrices, uint unArrayLength)
		{
			return this.platform.ISteamInventory_GetItemsWithPrices(pArrayItemDefs, pPrices, unArrayLength);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004AFC File Offset: 0x00002CFC
		public uint GetNumItemsWithPrices()
		{
			return this.platform.ISteamInventory_GetNumItemsWithPrices();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004B0C File Offset: 0x00002D0C
		public bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, string pchPropertyName, out string pchValueBuffer)
		{
			pchValueBuffer = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint num = 4096U;
			bool flag = this.platform.ISteamInventory_GetResultItemProperty(resultHandle.Value, unItemIndex, pchPropertyName, stringBuilder, out num);
			if (!flag)
			{
				return flag;
			}
			pchValueBuffer = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004B58 File Offset: 0x00002D58
		public unsafe SteamItemDetails_t[] GetResultItems(SteamInventoryResult_t resultHandle)
		{
			uint num = 0U;
			if (!this.platform.ISteamInventory_GetResultItems(resultHandle.Value, IntPtr.Zero, out num) || num == 0U)
			{
				return null;
			}
			SteamItemDetails_t[] array = new SteamItemDetails_t[num];
			SteamItemDetails_t[] array2;
			void* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = (void*)(&array2[0]);
			}
			if (!this.platform.ISteamInventory_GetResultItems(resultHandle.Value, (IntPtr)value, out num))
			{
				return null;
			}
			return array;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004BC3 File Offset: 0x00002DC3
		public Result GetResultStatus(SteamInventoryResult_t resultHandle)
		{
			return this.platform.ISteamInventory_GetResultStatus(resultHandle.Value);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004BD6 File Offset: 0x00002DD6
		public uint GetResultTimestamp(SteamInventoryResult_t resultHandle)
		{
			return this.platform.ISteamInventory_GetResultTimestamp(resultHandle.Value);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004BE9 File Offset: 0x00002DE9
		public bool GrantPromoItems(ref SteamInventoryResult_t pResultHandle)
		{
			return this.platform.ISteamInventory_GrantPromoItems(ref pResultHandle.Value);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004BFC File Offset: 0x00002DFC
		public bool LoadItemDefinitions()
		{
			return this.platform.ISteamInventory_LoadItemDefinitions();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004C09 File Offset: 0x00002E09
		public bool RemoveProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName)
		{
			return this.platform.ISteamInventory_RemoveProperty(handle.Value, nItemID.Value, pchPropertyName);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004C24 File Offset: 0x00002E24
		public CallbackHandle RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID, Action<SteamInventoryEligiblePromoItemDefIDs_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(steamID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SteamInventoryEligiblePromoItemDefIDs_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004C68 File Offset: 0x00002E68
		public CallbackHandle RequestPrices(Action<SteamInventoryRequestPricesResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamInventory_RequestPrices();
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SteamInventoryRequestPricesResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004CA5 File Offset: 0x00002EA5
		public void SendItemDropHeartbeat()
		{
			this.platform.ISteamInventory_SendItemDropHeartbeat();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004CB2 File Offset: 0x00002EB2
		public bool SerializeResult(SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, out uint punOutBufferSize)
		{
			return this.platform.ISteamInventory_SerializeResult(resultHandle.Value, pOutBuffer, out punOutBufferSize);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, string pchPropertyValue)
		{
			return this.platform.ISteamInventory_SetProperty(handle.Value, nItemID.Value, pchPropertyName, pchPropertyValue);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004CE3 File Offset: 0x00002EE3
		public bool SetProperty0(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, bool bValue)
		{
			return this.platform.ISteamInventory_SetProperty0(handle.Value, nItemID.Value, pchPropertyName, bValue);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004CFF File Offset: 0x00002EFF
		public bool SetProperty1(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, long nValue)
		{
			return this.platform.ISteamInventory_SetProperty0(handle.Value, nItemID.Value, pchPropertyName, nValue);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004D1B File Offset: 0x00002F1B
		public bool SetProperty2(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, float flValue)
		{
			return this.platform.ISteamInventory_SetProperty0(handle.Value, nItemID.Value, pchPropertyName, flValue);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004D38 File Offset: 0x00002F38
		public CallbackHandle StartPurchase(SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength, Action<SteamInventoryStartPurchaseResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamInventory_StartPurchase((from x in pArrayItemDefs
			select x.Value).ToArray<int>(), punArrayQuantity, unArrayLength);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SteamInventoryStartPurchaseResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004DA3 File Offset: 0x00002FA3
		public SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			return this.platform.ISteamInventory_StartUpdateProperties();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public bool SubmitUpdateProperties(SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle)
		{
			return this.platform.ISteamInventory_SubmitUpdateProperties(handle.Value, ref pResultHandle.Value);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004DCC File Offset: 0x00002FCC
		public bool TradeItems(ref SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, SteamItemInstanceID_t[] pArrayGive, uint[] pArrayGiveQuantity, uint nArrayGiveLength, SteamItemInstanceID_t[] pArrayGet, uint[] pArrayGetQuantity, uint nArrayGetLength)
		{
			return this.platform.ISteamInventory_TradeItems(ref pResultHandle.Value, steamIDTradePartner.Value, (from x in pArrayGive
			select x.Value).ToArray<ulong>(), pArrayGiveQuantity, nArrayGiveLength, (from x in pArrayGet
			select x.Value).ToArray<ulong>(), pArrayGetQuantity, nArrayGetLength);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004E4D File Offset: 0x0000304D
		public bool TransferItemQuantity(ref SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest)
		{
			return this.platform.ISteamInventory_TransferItemQuantity(ref pResultHandle.Value, itemIdSource.Value, unQuantity, itemIdDest.Value);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004E6E File Offset: 0x0000306E
		public bool TriggerItemDrop(ref SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition)
		{
			return this.platform.ISteamInventory_TriggerItemDrop(ref pResultHandle.Value, dropListDefinition.Value);
		}

		// Token: 0x04000474 RID: 1140
		internal Platform.Interface platform;

		// Token: 0x04000475 RID: 1141
		internal BaseSteamworks steamworks;
	}
}
