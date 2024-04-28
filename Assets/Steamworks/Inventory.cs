using System;
using System.Collections.Generic;
using System.Linq;
using Facepunch.Steamworks.Callbacks;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000177 RID: 375
	public class Inventory : IDisposable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000B9B RID: 2971 RVA: 0x00038728 File Offset: 0x00036928
		// (remove) Token: 0x06000B9C RID: 2972 RVA: 0x00038760 File Offset: 0x00036960
		public event Action OnUpdate;

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00038795 File Offset: 0x00036995
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x0003879D File Offset: 0x0003699D
		private bool IsServer { get; set; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000B9F RID: 2975 RVA: 0x000387A8 File Offset: 0x000369A8
		// (remove) Token: 0x06000BA0 RID: 2976 RVA: 0x000387E0 File Offset: 0x000369E0
		public event Action OnDefinitionsUpdated;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000BA1 RID: 2977 RVA: 0x00038818 File Offset: 0x00036A18
		// (remove) Token: 0x06000BA2 RID: 2978 RVA: 0x00038850 File Offset: 0x00036A50
		public event Action<Inventory.Result> OnInventoryResultReady;

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00038888 File Offset: 0x00036A88
		internal Inventory(BaseSteamworks steamworks, SteamInventory c, bool server)
		{
			this.IsServer = server;
			this.inventory = c;
			steamworks.RegisterCallback<SteamInventoryDefinitionUpdate_t>(new Action<SteamInventoryDefinitionUpdate_t>(this.onDefinitionsUpdated));
			if (!server)
			{
				Inventory.Result.Pending = new Dictionary<int, Inventory.Result>();
			}
			this.FetchItemDefinitions();
			this.LoadDefinitions();
			this.UpdatePrices();
			if (!server)
			{
				steamworks.RegisterCallback<SteamInventoryResultReady_t>(new Action<SteamInventoryResultReady_t>(this.onResultReady));
				steamworks.RegisterCallback<SteamInventoryFullUpdate_t>(new Action<SteamInventoryFullUpdate_t>(this.onFullUpdate));
				this.Refresh();
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0003890F File Offset: 0x00036B0F
		private void onDefinitionsUpdated(SteamInventoryDefinitionUpdate_t obj)
		{
			this.LoadDefinitions();
			this.UpdatePrices();
			if (this.OnDefinitionsUpdated != null)
			{
				this.OnDefinitionsUpdated();
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00038934 File Offset: 0x00036B34
		private bool LoadDefinitions()
		{
			SteamItemDef_t[] itemDefinitionIDs = this.inventory.GetItemDefinitionIDs();
			if (itemDefinitionIDs == null)
			{
				return false;
			}
			this.Definitions = (from x in itemDefinitionIDs
			select this.CreateDefinition(x)).ToArray<Inventory.Definition>();
			Inventory.Definition[] definitions = this.Definitions;
			for (int i = 0; i < definitions.Length; i++)
			{
				definitions[i].Link(this.Definitions);
			}
			return true;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00038994 File Offset: 0x00036B94
		private void onFullUpdate(SteamInventoryFullUpdate_t data)
		{
			Inventory.Result result = new Inventory.Result(this, data.Handle, false);
			result.Fill();
			this.onResult(result, true);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000389C0 File Offset: 0x00036BC0
		private void onResultReady(SteamInventoryResultReady_t data)
		{
			Inventory.Result result;
			if (Inventory.Result.Pending.TryGetValue(data.Handle, out result))
			{
				result.OnSteamResult(data);
				if (data.Result == SteamNative.Result.OK)
				{
					this.onResult(result, false);
				}
				Inventory.Result.Pending.Remove(data.Handle);
				result.Dispose();
			}
			else
			{
				result = new Inventory.Result(this, data.Handle, false);
				result.Fill();
			}
			Action<Inventory.Result> onInventoryResultReady = this.OnInventoryResultReady;
			if (onInventoryResultReady == null)
			{
				return;
			}
			onInventoryResultReady(result);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00038A38 File Offset: 0x00036C38
		private void onResult(Inventory.Result r, bool isFullUpdate)
		{
			if (r.IsSuccess)
			{
				if (isFullUpdate)
				{
					if (r.Timestamp < this.LastTimestamp)
					{
						return;
					}
					this.SerializedItems = r.Serialize();
					this.SerializedExpireTime = DateTime.Now.Add(TimeSpan.FromMinutes(60.0));
				}
				this.LastTimestamp = r.Timestamp;
				this.ApplyResult(r, isFullUpdate);
			}
			r.Dispose();
			r = null;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00038AAC File Offset: 0x00036CAC
		internal void ApplyResult(Inventory.Result r, bool isFullUpdate)
		{
			if (this.IsServer)
			{
				return;
			}
			if (r.IsSuccess && r.Items != null)
			{
				if (this.Items == null)
				{
					this.Items = new Inventory.Item[0];
				}
				if (isFullUpdate)
				{
					this.Items = r.Items;
				}
				else
				{
					this.Items = (from x in this.Items.UnionSelect(r.Items, (Inventory.Item oldItem, Inventory.Item newItem) => newItem)
					where !r.Removed.Contains(x)
					where !r.Consumed.Contains(x)
					select x).ToArray<Inventory.Item>();
				}
				Action onUpdate = this.OnUpdate;
				if (onUpdate == null)
				{
					return;
				}
				onUpdate();
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00038B8B File Offset: 0x00036D8B
		public void Dispose()
		{
			this.inventory = null;
			this.Items = null;
			this.SerializedItems = null;
			if (!this.IsServer)
			{
				Inventory.Result.Pending = null;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00038BB0 File Offset: 0x00036DB0
		[Obsolete("No longer required, will be removed in a later version")]
		public void PlaytimeHeartbeat()
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00038BB4 File Offset: 0x00036DB4
		public void Refresh()
		{
			if (this.IsServer)
			{
				return;
			}
			SteamInventoryResult_t value = 0;
			if (!this.inventory.GetAllItems(ref value) || value == -1)
			{
				Console.WriteLine("GetAllItems failed!?");
				return;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00038BF4 File Offset: 0x00036DF4
		public Inventory.Definition CreateDefinition(int id)
		{
			return new Inventory.Definition(this, id);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00038BFD File Offset: 0x00036DFD
		public void FetchItemDefinitions()
		{
			this.inventory.LoadItemDefinitions();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00038C0B File Offset: 0x00036E0B
		public void Update()
		{
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00038C0D File Offset: 0x00036E0D
		public IEnumerable<Inventory.Definition> DefinitionsWithPrices
		{
			get
			{
				if (this.Definitions == null)
				{
					yield break;
				}
				int num;
				for (int i = 0; i < this.Definitions.Length; i = num + 1)
				{
					if (this.Definitions[i].LocalPrice > 0.0)
					{
						yield return this.Definitions[i];
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00038C20 File Offset: 0x00036E20
		public static float PriceCategoryToFloat(string price)
		{
			if (string.IsNullOrEmpty(price))
			{
				return 0f;
			}
			price = price.Replace("1;VLV", "");
			int num = 0;
			if (!int.TryParse(price, out num))
			{
				return 0f;
			}
			return (float)int.Parse(price) / 100f;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00038C6C File Offset: 0x00036E6C
		public Inventory.Definition FindDefinition(int DefinitionId)
		{
			if (this.Definitions == null)
			{
				return null;
			}
			for (int i = 0; i < this.Definitions.Length; i++)
			{
				if (this.Definitions[i].Id == DefinitionId)
				{
					return this.Definitions[i];
				}
			}
			return null;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00038CB0 File Offset: 0x00036EB0
		public unsafe Inventory.Result Deserialize(byte[] data, int dataLength = -1)
		{
			if (data == null)
			{
				throw new ArgumentException("data should nto be null");
			}
			if (dataLength == -1)
			{
				dataLength = data.Length;
			}
			SteamInventoryResult_t value = -1;
			byte* value2;
			if (data == null || data.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &data[0];
			}
			if (!this.inventory.DeserializeResult(ref value, (IntPtr)((void*)value2), (uint)dataLength, false) || value == -1)
			{
				return null;
			}
			Inventory.Result result = new Inventory.Result(this, value, false);
			result.Fill();
			return result;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00038D28 File Offset: 0x00036F28
		public Inventory.Result CraftItem(Inventory.Item[] list, Inventory.Definition target)
		{
			SteamInventoryResult_t value = -1;
			SteamItemDef_t[] pArrayGenerate = new SteamItemDef_t[]
			{
				new SteamItemDef_t
				{
					Value = target.Id
				}
			};
			uint[] punArrayGenerateQuantity = new uint[]
			{
				1U
			};
			SteamItemInstanceID_t[] array = (from x in list
			select x.Id).ToArray<SteamItemInstanceID_t>();
			uint[] punArrayDestroyQuantity = (from x in list
			select 1U).ToArray<uint>();
			if (!this.inventory.ExchangeItems(ref value, pArrayGenerate, punArrayGenerateQuantity, 1U, array, punArrayDestroyQuantity, (uint)array.Length))
			{
				return null;
			}
			return new Inventory.Result(this, value, true);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00038DEC File Offset: 0x00036FEC
		public Inventory.Result CraftItem(Inventory.Item.Amount[] list, Inventory.Definition target)
		{
			SteamInventoryResult_t value = -1;
			SteamItemDef_t[] pArrayGenerate = new SteamItemDef_t[]
			{
				new SteamItemDef_t
				{
					Value = target.Id
				}
			};
			uint[] punArrayGenerateQuantity = new uint[]
			{
				1U
			};
			SteamItemInstanceID_t[] array = (from x in list
			select x.Item.Id).ToArray<SteamItemInstanceID_t>();
			uint[] punArrayDestroyQuantity = (from x in list
			select (uint)x.Quantity).ToArray<uint>();
			if (!this.inventory.ExchangeItems(ref value, pArrayGenerate, punArrayGenerateQuantity, 1U, array, punArrayDestroyQuantity, (uint)array.Length))
			{
				return null;
			}
			return new Inventory.Result(this, value, true);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00038EAE File Offset: 0x000370AE
		public Inventory.Result SplitStack(Inventory.Item item, int quantity = 1)
		{
			return item.SplitStack(quantity);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00038EB8 File Offset: 0x000370B8
		public Inventory.Result Stack(Inventory.Item source, Inventory.Item dest, int quantity = 1)
		{
			SteamInventoryResult_t value = -1;
			if (!this.inventory.TransferItemQuantity(ref value, source.Id, (uint)quantity, dest.Id))
			{
				return null;
			}
			return new Inventory.Result(this, value, true);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00038F04 File Offset: 0x00037104
		public Inventory.Result GenerateItem(Inventory.Definition target, int amount)
		{
			SteamInventoryResult_t value = -1;
			SteamItemDef_t[] pArrayItemDefs = new SteamItemDef_t[]
			{
				new SteamItemDef_t
				{
					Value = target.Id
				}
			};
			uint[] punArrayQuantity = new uint[]
			{
				(uint)amount
			};
			if (!this.inventory.GenerateItems(ref value, pArrayItemDefs, punArrayQuantity, 1U))
			{
				return null;
			}
			return new Inventory.Result(this, value, true);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00038F68 File Offset: 0x00037168
		public bool StartPurchase(Inventory.Definition[] items, Inventory.StartPurchaseSuccess callback = null)
		{
			IEnumerable<IGrouping<int, Inventory.Definition>> source = from x in items
			group x by x.Id;
			SteamItemDef_t[] pArrayItemDefs = (from x in source
			select new SteamItemDef_t
			{
				Value = x.Key
			}).ToArray<SteamItemDef_t>();
			uint[] array = (from x in source
			select (uint)x.Count<Inventory.Definition>()).ToArray<uint>();
			return this.inventory.StartPurchase(pArrayItemDefs, array, (uint)array.Length, delegate(SteamInventoryStartPurchaseResult_t result, bool error)
			{
				if (error)
				{
					Inventory.StartPurchaseSuccess callback2 = callback;
					if (callback2 == null)
					{
						return;
					}
					callback2(0UL, 0UL);
					return;
				}
				else
				{
					Inventory.StartPurchaseSuccess callback3 = callback;
					if (callback3 == null)
					{
						return;
					}
					callback3(result.OrderID, result.TransID);
					return;
				}
			}) != null;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0003901B File Offset: 0x0003721B
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00039023 File Offset: 0x00037223
		public string Currency { get; private set; }

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003902C File Offset: 0x0003722C
		public void UpdatePrices()
		{
			if (this.IsServer)
			{
				return;
			}
			this.inventory.RequestPrices(delegate(SteamInventoryRequestPricesResult_t result, bool b)
			{
				this.Currency = result.Currency;
				if (this.Definitions == null)
				{
					return;
				}
				for (int i = 0; i < this.Definitions.Length; i++)
				{
					this.Definitions[i].UpdatePrice();
				}
				Action onUpdate = this.OnUpdate;
				if (onUpdate == null)
				{
					return;
				}
				onUpdate();
			});
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00039050 File Offset: 0x00037250
		public void TriggerPromoDrop(int definitionId)
		{
			SteamInventoryResult_t resultHandle = 0;
			this.inventory.AddPromoItem(ref resultHandle, definitionId);
			this.inventory.DestroyResult(resultHandle);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00039084 File Offset: 0x00037284
		public void TriggerItemDrop(int definitionId)
		{
			SteamInventoryResult_t resultHandle = 0;
			this.inventory.TriggerItemDrop(ref resultHandle, definitionId);
			this.inventory.DestroyResult(resultHandle);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000390B8 File Offset: 0x000372B8
		public void GrantAllPromoItems()
		{
			SteamInventoryResult_t resultHandle = 0;
			this.inventory.GrantPromoItems(ref resultHandle);
			this.inventory.DestroyResult(resultHandle);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000390E8 File Offset: 0x000372E8
		internal Inventory.Item ItemFrom(SteamInventoryResult_t handle, SteamItemDetails_t detail, int index)
		{
			Dictionary<string, string> dictionary = null;
			string text;
			if (this.EnableItemProperties && this.inventory.GetResultItemProperty(handle, (uint)index, null, out text))
			{
				dictionary = new Dictionary<string, string>();
				foreach (string text2 in text.Split(new char[]
				{
					','
				}))
				{
					string value;
					if (this.inventory.GetResultItemProperty(handle, (uint)index, text2, out value))
					{
						if (text2 == "error")
						{
							Console.Write("Steam item error: ");
							Console.WriteLine(value);
							return null;
						}
						dictionary.Add(text2, value);
					}
				}
			}
			return new Inventory.Item(this, detail.ItemId, (int)detail.Quantity, detail.Definition)
			{
				Properties = dictionary
			};
		}

		// Token: 0x04000852 RID: 2130
		public Inventory.Item[] Items;

		// Token: 0x04000853 RID: 2131
		public byte[] SerializedItems;

		// Token: 0x04000854 RID: 2132
		public DateTime SerializedExpireTime;

		// Token: 0x04000855 RID: 2133
		public bool EnableItemProperties = true;

		// Token: 0x04000856 RID: 2134
		internal uint LastTimestamp;

		// Token: 0x04000857 RID: 2135
		internal SteamInventory inventory;

		// Token: 0x0400085B RID: 2139
		public Inventory.Definition[] Definitions;

		// Token: 0x02000288 RID: 648
		// (Invoke) Token: 0x06001E67 RID: 7783
		public delegate void StartPurchaseSuccess(ulong orderId, ulong transactionId);

		// Token: 0x02000289 RID: 649
		public class Definition
		{
			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00065BD0 File Offset: 0x00063DD0
			// (set) Token: 0x06001E6B RID: 7787 RVA: 0x00065BD8 File Offset: 0x00063DD8
			public int Id { get; private set; }

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00065BE1 File Offset: 0x00063DE1
			// (set) Token: 0x06001E6D RID: 7789 RVA: 0x00065BE9 File Offset: 0x00063DE9
			public string Name { get; set; }

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00065BF2 File Offset: 0x00063DF2
			// (set) Token: 0x06001E6F RID: 7791 RVA: 0x00065BFA File Offset: 0x00063DFA
			public string Description { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00065C03 File Offset: 0x00063E03
			// (set) Token: 0x06001E71 RID: 7793 RVA: 0x00065C0B File Offset: 0x00063E0B
			public string IconUrl { get; set; }

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00065C14 File Offset: 0x00063E14
			// (set) Token: 0x06001E73 RID: 7795 RVA: 0x00065C1C File Offset: 0x00063E1C
			public string IconLargeUrl { get; set; }

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00065C25 File Offset: 0x00063E25
			// (set) Token: 0x06001E75 RID: 7797 RVA: 0x00065C2D File Offset: 0x00063E2D
			public string Type { get; set; }

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06001E76 RID: 7798 RVA: 0x00065C36 File Offset: 0x00063E36
			// (set) Token: 0x06001E77 RID: 7799 RVA: 0x00065C3E File Offset: 0x00063E3E
			public string ExchangeSchema { get; set; }

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00065C47 File Offset: 0x00063E47
			// (set) Token: 0x06001E79 RID: 7801 RVA: 0x00065C4F File Offset: 0x00063E4F
			public Inventory.Recipe[] Recipes { get; set; }

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00065C58 File Offset: 0x00063E58
			// (set) Token: 0x06001E7B RID: 7803 RVA: 0x00065C60 File Offset: 0x00063E60
			public Inventory.Recipe[] IngredientFor { get; set; }

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00065C69 File Offset: 0x00063E69
			// (set) Token: 0x06001E7D RID: 7805 RVA: 0x00065C71 File Offset: 0x00063E71
			public DateTime Created { get; set; }

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00065C7A File Offset: 0x00063E7A
			// (set) Token: 0x06001E7F RID: 7807 RVA: 0x00065C82 File Offset: 0x00063E82
			public DateTime Modified { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00065C8B File Offset: 0x00063E8B
			// (set) Token: 0x06001E81 RID: 7809 RVA: 0x00065C93 File Offset: 0x00063E93
			public string PriceCategory { get; set; }

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00065C9C File Offset: 0x00063E9C
			// (set) Token: 0x06001E83 RID: 7811 RVA: 0x00065CA4 File Offset: 0x00063EA4
			public double PriceDollars { get; internal set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06001E84 RID: 7812 RVA: 0x00065CAD File Offset: 0x00063EAD
			// (set) Token: 0x06001E85 RID: 7813 RVA: 0x00065CB5 File Offset: 0x00063EB5
			public double LocalPrice { get; internal set; }

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06001E86 RID: 7814 RVA: 0x00065CBE File Offset: 0x00063EBE
			// (set) Token: 0x06001E87 RID: 7815 RVA: 0x00065CC6 File Offset: 0x00063EC6
			public string LocalPriceFormatted { get; internal set; }

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06001E88 RID: 7816 RVA: 0x00065CCF File Offset: 0x00063ECF
			// (set) Token: 0x06001E89 RID: 7817 RVA: 0x00065CD7 File Offset: 0x00063ED7
			public bool Marketable { get; set; }

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06001E8A RID: 7818 RVA: 0x00065CE0 File Offset: 0x00063EE0
			public bool IsGenerator
			{
				get
				{
					return this.Type == "generator";
				}
			}

			// Token: 0x06001E8B RID: 7819 RVA: 0x00065CF2 File Offset: 0x00063EF2
			internal Definition(Inventory i, int id)
			{
				this.inventory = i;
				this.Id = id;
				this.SetupCommonProperties();
				this.UpdatePrice();
			}

			// Token: 0x06001E8C RID: 7820 RVA: 0x00065D14 File Offset: 0x00063F14
			public void SetProperty(string name, string value)
			{
				if (this.customProperties == null)
				{
					this.customProperties = new Dictionary<string, string>();
				}
				if (!this.customProperties.ContainsKey(name))
				{
					this.customProperties.Add(name, value);
					return;
				}
				this.customProperties[name] = value;
			}

			// Token: 0x06001E8D RID: 7821 RVA: 0x00065D54 File Offset: 0x00063F54
			public T GetProperty<T>(string name)
			{
				string stringProperty = this.GetStringProperty(name);
				T result;
				if (string.IsNullOrEmpty(stringProperty))
				{
					result = default(T);
					return result;
				}
				try
				{
					result = (T)((object)Convert.ChangeType(stringProperty, typeof(T)));
				}
				catch (Exception)
				{
					result = default(T);
				}
				return result;
			}

			// Token: 0x06001E8E RID: 7822 RVA: 0x00065DB4 File Offset: 0x00063FB4
			public string GetStringProperty(string name)
			{
				string empty = string.Empty;
				if (this.customProperties != null && this.customProperties.ContainsKey(name))
				{
					return this.customProperties[name];
				}
				if (!this.inventory.inventory.GetItemDefinitionProperty(this.Id, name, out empty))
				{
					return string.Empty;
				}
				return empty;
			}

			// Token: 0x06001E8F RID: 7823 RVA: 0x00065E14 File Offset: 0x00064014
			public bool GetBoolProperty(string name)
			{
				string stringProperty = this.GetStringProperty(name);
				return stringProperty.Length != 0 && stringProperty[0] != '0' && stringProperty[0] != 'F' && stringProperty[0] != 'f';
			}

			// Token: 0x06001E90 RID: 7824 RVA: 0x00065E58 File Offset: 0x00064058
			internal void SetupCommonProperties()
			{
				this.Name = this.GetStringProperty("name");
				this.Description = this.GetStringProperty("description");
				this.Created = this.GetProperty<DateTime>("timestamp");
				this.Modified = this.GetProperty<DateTime>("modified");
				this.ExchangeSchema = this.GetStringProperty("exchange");
				this.IconUrl = this.GetStringProperty("icon_url");
				this.IconLargeUrl = this.GetStringProperty("icon_url_large");
				this.Type = this.GetStringProperty("type");
				this.PriceCategory = this.GetStringProperty("price_category");
				this.Marketable = this.GetBoolProperty("marketable");
				if (!string.IsNullOrEmpty(this.PriceCategory))
				{
					this.PriceDollars = (double)Inventory.PriceCategoryToFloat(this.PriceCategory);
				}
			}

			// Token: 0x06001E91 RID: 7825 RVA: 0x00065F2E File Offset: 0x0006412E
			public void TriggerItemDrop()
			{
				this.inventory.TriggerItemDrop(this.Id);
			}

			// Token: 0x06001E92 RID: 7826 RVA: 0x00065F41 File Offset: 0x00064141
			public void TriggerPromoDrop()
			{
				this.inventory.TriggerPromoDrop(this.Id);
			}

			// Token: 0x06001E93 RID: 7827 RVA: 0x00065F54 File Offset: 0x00064154
			internal void Link(Inventory.Definition[] definitions)
			{
				this.LinkExchange(definitions);
			}

			// Token: 0x06001E94 RID: 7828 RVA: 0x00065F60 File Offset: 0x00064160
			private void LinkExchange(Inventory.Definition[] definitions)
			{
				if (string.IsNullOrEmpty(this.ExchangeSchema))
				{
					return;
				}
				string[] source = this.ExchangeSchema.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				this.Recipes = (from x in source
				select Inventory.Recipe.FromString(x, definitions, this)).ToArray<Inventory.Recipe>();
			}

			// Token: 0x06001E95 RID: 7829 RVA: 0x00065FC4 File Offset: 0x000641C4
			internal void InRecipe(Inventory.Recipe r)
			{
				if (this.IngredientFor == null)
				{
					this.IngredientFor = new Inventory.Recipe[0];
				}
				this.IngredientFor = new List<Inventory.Recipe>(this.IngredientFor)
				{
					r
				}.ToArray();
			}

			// Token: 0x06001E96 RID: 7830 RVA: 0x00066004 File Offset: 0x00064204
			internal void UpdatePrice()
			{
				ulong num;
				if (this.inventory.inventory.GetItemPrice(this.Id, out num))
				{
					this.LocalPrice = num / 100.0;
					this.LocalPriceFormatted = Utility.FormatPrice(this.inventory.Currency, num);
					return;
				}
				this.LocalPrice = 0.0;
				this.LocalPriceFormatted = null;
			}

			// Token: 0x04000C3C RID: 3132
			internal Inventory inventory;

			// Token: 0x04000C4D RID: 3149
			private Dictionary<string, string> customProperties;
		}

		// Token: 0x0200028A RID: 650
		public struct Recipe
		{
			// Token: 0x06001E97 RID: 7831 RVA: 0x00066074 File Offset: 0x00064274
			internal static Inventory.Recipe FromString(string part, Inventory.Definition[] definitions, Inventory.Definition Result)
			{
				Inventory.Recipe recipe = default(Inventory.Recipe);
				recipe.Result = Result;
				string[] source = part.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				recipe.Ingredients = (from x in source
				select Inventory.Recipe.Ingredient.FromString(x, definitions) into x
				where x.DefinitionId != 0
				select x).ToArray<Inventory.Recipe.Ingredient>();
				foreach (Inventory.Recipe.Ingredient ingredient in recipe.Ingredients)
				{
					if (ingredient.Definition != null)
					{
						ingredient.Definition.InRecipe(recipe);
					}
				}
				return recipe;
			}

			// Token: 0x04000C4E RID: 3150
			public Inventory.Definition Result;

			// Token: 0x04000C4F RID: 3151
			public Inventory.Recipe.Ingredient[] Ingredients;

			// Token: 0x020002C5 RID: 709
			public struct Ingredient
			{
				// Token: 0x06002D89 RID: 11657 RVA: 0x0006833C File Offset: 0x0006653C
				internal static Inventory.Recipe.Ingredient FromString(string part, Inventory.Definition[] definitions)
				{
					Inventory.Recipe.Ingredient i = default(Inventory.Recipe.Ingredient);
					i.Count = 1;
					try
					{
						if (part.Contains('x'))
						{
							int num = part.IndexOf('x');
							int count = 0;
							if (int.TryParse(part.Substring(num + 1), out count))
							{
								i.Count = count;
							}
							part = part.Substring(0, num);
						}
						i.DefinitionId = int.Parse(part);
						i.Definition = definitions.FirstOrDefault((Inventory.Definition x) => x.Id == i.DefinitionId);
					}
					catch (Exception)
					{
						return i;
					}
					return i;
				}

				// Token: 0x04000D4C RID: 3404
				public int DefinitionId;

				// Token: 0x04000D4D RID: 3405
				public Inventory.Definition Definition;

				// Token: 0x04000D4E RID: 3406
				public int Count;
			}
		}

		// Token: 0x0200028B RID: 651
		public class Item : IEquatable<Inventory.Item>
		{
			// Token: 0x06001E98 RID: 7832 RVA: 0x0006612D File Offset: 0x0006432D
			internal Item(Inventory Inventory, ulong Id, int Quantity, int DefinitionId)
			{
				this.Inventory = Inventory;
				this.Id = Id;
				this.Quantity = Quantity;
				this.DefinitionId = DefinitionId;
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00066152 File Offset: 0x00064352
			// (set) Token: 0x06001E9A RID: 7834 RVA: 0x0006615A File Offset: 0x0006435A
			public Dictionary<string, string> Properties { get; internal set; }

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00066163 File Offset: 0x00064363
			public Inventory.Definition Definition
			{
				get
				{
					if (this._cachedDefinition != null)
					{
						return this._cachedDefinition;
					}
					this._cachedDefinition = this.Inventory.FindDefinition(this.DefinitionId);
					return this._cachedDefinition;
				}
			}

			// Token: 0x06001E9C RID: 7836 RVA: 0x00066191 File Offset: 0x00064391
			public bool Equals(Inventory.Item other)
			{
				return other != null && (this == other || this.Id == other.Id);
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x000661AC File Offset: 0x000643AC
			public override bool Equals(object obj)
			{
				return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Inventory.Item)obj)));
			}

			// Token: 0x06001E9E RID: 7838 RVA: 0x000661DA File Offset: 0x000643DA
			public override int GetHashCode()
			{
				return this.Id.GetHashCode();
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x000661E7 File Offset: 0x000643E7
			public static bool operator ==(Inventory.Item left, Inventory.Item right)
			{
				return object.Equals(left, right);
			}

			// Token: 0x06001EA0 RID: 7840 RVA: 0x000661F0 File Offset: 0x000643F0
			public static bool operator !=(Inventory.Item left, Inventory.Item right)
			{
				return !object.Equals(left, right);
			}

			// Token: 0x06001EA1 RID: 7841 RVA: 0x000661FC File Offset: 0x000643FC
			public Inventory.Result Consume(int amount = 1)
			{
				SteamInventoryResult_t value = -1;
				if (!this.Inventory.inventory.ConsumeItem(ref value, this.Id, (uint)amount))
				{
					return null;
				}
				return new Inventory.Result(this.Inventory, value, true);
			}

			// Token: 0x06001EA2 RID: 7842 RVA: 0x00066244 File Offset: 0x00064444
			public Inventory.Result SplitStack(int quantity = 1)
			{
				SteamInventoryResult_t value = -1;
				if (!this.Inventory.inventory.TransferItemQuantity(ref value, this.Id, (uint)quantity, 18446744073709551615UL))
				{
					return null;
				}
				return new Inventory.Result(this.Inventory, value, true);
			}

			// Token: 0x06001EA3 RID: 7843 RVA: 0x00066293 File Offset: 0x00064493
			private void UpdatingProperties()
			{
				if (!this.Inventory.EnableItemProperties)
				{
					throw new InvalidOperationException("Item properties are disabled.");
				}
				if (this.updateHandle != 0UL)
				{
					return;
				}
				this.updateHandle = this.Inventory.inventory.StartUpdateProperties();
			}

			// Token: 0x06001EA4 RID: 7844 RVA: 0x000662D1 File Offset: 0x000644D1
			public bool SetProperty(string name, string value)
			{
				this.UpdatingProperties();
				this.Properties[name] = value.ToString();
				return this.Inventory.inventory.SetProperty(this.updateHandle, this.Id, name, value);
			}

			// Token: 0x06001EA5 RID: 7845 RVA: 0x0006630E File Offset: 0x0006450E
			public bool SetProperty(string name, bool value)
			{
				this.UpdatingProperties();
				this.Properties[name] = value.ToString();
				return this.Inventory.inventory.SetProperty0(this.updateHandle, this.Id, name, value);
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x0006634C File Offset: 0x0006454C
			public bool SetProperty(string name, long value)
			{
				this.UpdatingProperties();
				this.Properties[name] = value.ToString();
				return this.Inventory.inventory.SetProperty1(this.updateHandle, this.Id, name, value);
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x0006638A File Offset: 0x0006458A
			public bool SetProperty(string name, float value)
			{
				this.UpdatingProperties();
				this.Properties[name] = value.ToString();
				return this.Inventory.inventory.SetProperty2(this.updateHandle, this.Id, name, value);
			}

			// Token: 0x06001EA8 RID: 7848 RVA: 0x000663C8 File Offset: 0x000645C8
			public bool SubmitProperties()
			{
				if (this.updateHandle == 0UL)
				{
					throw new Exception("SubmitProperties called without updating properties");
				}
				bool result;
				try
				{
					SteamInventoryResult_t resultHandle = -1;
					if (!this.Inventory.inventory.SubmitUpdateProperties(this.updateHandle, ref resultHandle))
					{
						result = false;
					}
					else
					{
						this.Inventory.inventory.DestroyResult(resultHandle);
						result = true;
					}
				}
				finally
				{
					this.updateHandle = 0UL;
				}
				return result;
			}

			// Token: 0x04000C50 RID: 3152
			public ulong Id;

			// Token: 0x04000C51 RID: 3153
			public int Quantity;

			// Token: 0x04000C52 RID: 3154
			public int DefinitionId;

			// Token: 0x04000C53 RID: 3155
			internal Inventory Inventory;

			// Token: 0x04000C55 RID: 3157
			private Inventory.Definition _cachedDefinition;

			// Token: 0x04000C56 RID: 3158
			public bool TradeLocked;

			// Token: 0x04000C57 RID: 3159
			private SteamInventoryUpdateHandle_t updateHandle;

			// Token: 0x020002C8 RID: 712
			public struct Amount
			{
				// Token: 0x04000D52 RID: 3410
				public Inventory.Item Item;

				// Token: 0x04000D53 RID: 3411
				public int Quantity;
			}
		}

		// Token: 0x0200028C RID: 652
		public class Result : IDisposable
		{
			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00066448 File Offset: 0x00064648
			// (set) Token: 0x06001EAA RID: 7850 RVA: 0x00066450 File Offset: 0x00064650
			private SteamInventoryResult_t Handle { get; set; } = -1;

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00066459 File Offset: 0x00064659
			// (set) Token: 0x06001EAC RID: 7852 RVA: 0x00066461 File Offset: 0x00064661
			public Inventory.Item[] Items { get; internal set; }

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0006646A File Offset: 0x0006466A
			// (set) Token: 0x06001EAE RID: 7854 RVA: 0x00066472 File Offset: 0x00064672
			public Inventory.Item[] Removed { get; internal set; }

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0006647B File Offset: 0x0006467B
			// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x00066483 File Offset: 0x00064683
			public Inventory.Item[] Consumed { get; internal set; }

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0006648C File Offset: 0x0006468C
			public bool IsPending
			{
				get
				{
					if (this._gotResult)
					{
						return false;
					}
					if (this.Status() == Facepunch.Steamworks.Callbacks.Result.OK)
					{
						this.Fill();
						return false;
					}
					return this.Status() == Facepunch.Steamworks.Callbacks.Result.Pending;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x000664B3 File Offset: 0x000646B3
			// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x000664BB File Offset: 0x000646BB
			internal uint Timestamp { get; private set; }

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x000664C4 File Offset: 0x000646C4
			internal bool IsSuccess
			{
				get
				{
					return this.Items != null || (this.Handle != -1 && this.Status() == Facepunch.Steamworks.Callbacks.Result.OK);
				}
			}

			// Token: 0x06001EB5 RID: 7861 RVA: 0x000664E9 File Offset: 0x000646E9
			internal Facepunch.Steamworks.Callbacks.Result Status()
			{
				if (this.Handle == -1)
				{
					return Facepunch.Steamworks.Callbacks.Result.InvalidParam;
				}
				return (Facepunch.Steamworks.Callbacks.Result)this.inventory.inventory.GetResultStatus(this.Handle);
			}

			// Token: 0x06001EB6 RID: 7862 RVA: 0x00066511 File Offset: 0x00064711
			internal Result(Inventory inventory, int Handle, bool pending)
			{
				if (pending)
				{
					Inventory.Result.Pending.Add(Handle, this);
				}
				this.Handle = Handle;
				this.inventory = inventory;
			}

			// Token: 0x06001EB7 RID: 7863 RVA: 0x00066548 File Offset: 0x00064748
			internal void Fill()
			{
				if (this._gotResult)
				{
					return;
				}
				if (this.Items != null)
				{
					return;
				}
				if (this.Status() != Facepunch.Steamworks.Callbacks.Result.OK)
				{
					return;
				}
				this._gotResult = true;
				this.Timestamp = this.inventory.inventory.GetResultTimestamp(this.Handle);
				SteamItemDetails_t[] resultItems = this.inventory.inventory.GetResultItems(this.Handle);
				if (resultItems == null)
				{
					return;
				}
				List<Inventory.Item> list = new List<Inventory.Item>();
				List<Inventory.Item> list2 = new List<Inventory.Item>();
				List<Inventory.Item> list3 = new List<Inventory.Item>();
				for (int i = 0; i < resultItems.Length; i++)
				{
					Inventory.Item item = this.inventory.ItemFrom(this.Handle, resultItems[i], i);
					if (!(item == null))
					{
						if ((resultItems[i].Flags & 256) != 0)
						{
							list2.Add(item);
						}
						else if ((resultItems[i].Flags & 512) != 0)
						{
							list3.Add(item);
						}
						else
						{
							list.Add(item);
						}
					}
				}
				this.Items = list.ToArray();
				this.Removed = list2.ToArray();
				this.Consumed = list3.ToArray();
				if (this.OnResult != null)
				{
					this.OnResult(this);
				}
			}

			// Token: 0x06001EB8 RID: 7864 RVA: 0x00066678 File Offset: 0x00064878
			internal void OnSteamResult(SteamInventoryResultReady_t data)
			{
				if (data.Result == SteamNative.Result.OK)
				{
					this.Fill();
				}
			}

			// Token: 0x06001EB9 RID: 7865 RVA: 0x0006668C File Offset: 0x0006488C
			internal unsafe byte[] Serialize()
			{
				uint num = 0U;
				if (!this.inventory.inventory.SerializeResult(this.Handle, IntPtr.Zero, out num))
				{
					return null;
				}
				byte[] array = new byte[num];
				byte[] array2;
				byte* value;
				if ((array2 = array) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				if (!this.inventory.inventory.SerializeResult(this.Handle, (IntPtr)((void*)value), out num))
				{
					return null;
				}
				array2 = null;
				return array;
			}

			// Token: 0x06001EBA RID: 7866 RVA: 0x00066700 File Offset: 0x00064900
			public void Dispose()
			{
				if (this.Handle != -1 && this.inventory != null)
				{
					this.inventory.inventory.DestroyResult(this.Handle);
					this.Handle = -1;
				}
				this.inventory = null;
			}

			// Token: 0x04000C58 RID: 3160
			internal static Dictionary<int, Inventory.Result> Pending;

			// Token: 0x04000C59 RID: 3161
			internal Inventory inventory;

			// Token: 0x04000C5B RID: 3163
			public Action<Inventory.Result> OnResult;

			// Token: 0x04000C5F RID: 3167
			protected bool _gotResult;
		}
	}
}
