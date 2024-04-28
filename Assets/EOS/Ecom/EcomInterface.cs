using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000455 RID: 1109
	public sealed class EcomInterface : Handle
	{
		// Token: 0x06001B05 RID: 6917 RVA: 0x000036D3 File Offset: 0x000018D3
		public EcomInterface()
		{
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000036DB File Offset: 0x000018DB
		public EcomInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		public void Checkout(CheckoutOptions options, object clientData, OnCheckoutCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CheckoutOptionsInternal, CheckoutOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnCheckoutCallbackInternal onCheckoutCallbackInternal = new OnCheckoutCallbackInternal(EcomInterface.OnCheckoutCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onCheckoutCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_Checkout(base.InnerHandle, zero, zero2, onCheckoutCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0001C740 File Offset: 0x0001A940
		public Result CopyEntitlementById(CopyEntitlementByIdOptions options, out Entitlement outEntitlement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyEntitlementByIdOptionsInternal, CopyEntitlementByIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyEntitlementById(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero2, out outEntitlement))
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0001C788 File Offset: 0x0001A988
		public Result CopyEntitlementByIndex(CopyEntitlementByIndexOptions options, out Entitlement outEntitlement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyEntitlementByIndexOptionsInternal, CopyEntitlementByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyEntitlementByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero2, out outEntitlement))
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		public Result CopyEntitlementByNameAndIndex(CopyEntitlementByNameAndIndexOptions options, out Entitlement outEntitlement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyEntitlementByNameAndIndexOptionsInternal, CopyEntitlementByNameAndIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyEntitlementByNameAndIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero2, out outEntitlement))
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0001C818 File Offset: 0x0001AA18
		public Result CopyItemById(CopyItemByIdOptions options, out CatalogItem outItem)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyItemByIdOptionsInternal, CopyItemByIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyItemById(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<CatalogItemInternal, CatalogItem>(zero2, out outItem))
			{
				Bindings.EOS_Ecom_CatalogItem_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0001C860 File Offset: 0x0001AA60
		public Result CopyItemImageInfoByIndex(CopyItemImageInfoByIndexOptions options, out KeyImageInfo outImageInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyItemImageInfoByIndexOptionsInternal, CopyItemImageInfoByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyItemImageInfoByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<KeyImageInfoInternal, KeyImageInfo>(zero2, out outImageInfo))
			{
				Bindings.EOS_Ecom_KeyImageInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		public Result CopyItemReleaseByIndex(CopyItemReleaseByIndexOptions options, out CatalogRelease outRelease)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyItemReleaseByIndexOptionsInternal, CopyItemReleaseByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyItemReleaseByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<CatalogReleaseInternal, CatalogRelease>(zero2, out outRelease))
			{
				Bindings.EOS_Ecom_CatalogRelease_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0001C8F0 File Offset: 0x0001AAF0
		public Result CopyOfferById(CopyOfferByIdOptions options, out CatalogOffer outOffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyOfferByIdOptionsInternal, CopyOfferByIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferById(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<CatalogOfferInternal, CatalogOffer>(zero2, out outOffer))
			{
				Bindings.EOS_Ecom_CatalogOffer_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0001C938 File Offset: 0x0001AB38
		public Result CopyOfferByIndex(CopyOfferByIndexOptions options, out CatalogOffer outOffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyOfferByIndexOptionsInternal, CopyOfferByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<CatalogOfferInternal, CatalogOffer>(zero2, out outOffer))
			{
				Bindings.EOS_Ecom_CatalogOffer_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0001C980 File Offset: 0x0001AB80
		public Result CopyOfferImageInfoByIndex(CopyOfferImageInfoByIndexOptions options, out KeyImageInfo outImageInfo)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyOfferImageInfoByIndexOptionsInternal, CopyOfferImageInfoByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferImageInfoByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<KeyImageInfoInternal, KeyImageInfo>(zero2, out outImageInfo))
			{
				Bindings.EOS_Ecom_KeyImageInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		public Result CopyOfferItemByIndex(CopyOfferItemByIndexOptions options, out CatalogItem outItem)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyOfferItemByIndexOptionsInternal, CopyOfferItemByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyOfferItemByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<CatalogItemInternal, CatalogItem>(zero2, out outItem))
			{
				Bindings.EOS_Ecom_CatalogItem_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0001CA10 File Offset: 0x0001AC10
		public Result CopyTransactionById(CopyTransactionByIdOptions options, out Transaction outTransaction)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyTransactionByIdOptionsInternal, CopyTransactionByIdOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyTransactionById(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<Transaction>(zero2, out outTransaction);
			return result;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0001CA50 File Offset: 0x0001AC50
		public Result CopyTransactionByIndex(CopyTransactionByIndexOptions options, out Transaction outTransaction)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyTransactionByIndexOptionsInternal, CopyTransactionByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_CopyTransactionByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet<Transaction>(zero2, out outTransaction);
			return result;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public uint GetEntitlementsByNameCount(GetEntitlementsByNameCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetEntitlementsByNameCountOptionsInternal, GetEntitlementsByNameCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetEntitlementsByNameCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0001CAC0 File Offset: 0x0001ACC0
		public uint GetEntitlementsCount(GetEntitlementsCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetEntitlementsCountOptionsInternal, GetEntitlementsCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetEntitlementsCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		public uint GetItemImageInfoCount(GetItemImageInfoCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetItemImageInfoCountOptionsInternal, GetItemImageInfoCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetItemImageInfoCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0001CB20 File Offset: 0x0001AD20
		public uint GetItemReleaseCount(GetItemReleaseCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetItemReleaseCountOptionsInternal, GetItemReleaseCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetItemReleaseCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0001CB50 File Offset: 0x0001AD50
		public uint GetOfferCount(GetOfferCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetOfferCountOptionsInternal, GetOfferCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetOfferCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0001CB80 File Offset: 0x0001AD80
		public uint GetOfferImageInfoCount(GetOfferImageInfoCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetOfferImageInfoCountOptionsInternal, GetOfferImageInfoCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetOfferImageInfoCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		public uint GetOfferItemCount(GetOfferItemCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetOfferItemCountOptionsInternal, GetOfferItemCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetOfferItemCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0001CBE0 File Offset: 0x0001ADE0
		public uint GetTransactionCount(GetTransactionCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetTransactionCountOptionsInternal, GetTransactionCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_GetTransactionCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0001CC10 File Offset: 0x0001AE10
		public void QueryEntitlements(QueryEntitlementsOptions options, object clientData, OnQueryEntitlementsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryEntitlementsOptionsInternal, QueryEntitlementsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryEntitlementsCallbackInternal onQueryEntitlementsCallbackInternal = new OnQueryEntitlementsCallbackInternal(EcomInterface.OnQueryEntitlementsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryEntitlementsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryEntitlements(base.InnerHandle, zero, zero2, onQueryEntitlementsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0001CC64 File Offset: 0x0001AE64
		public void QueryOffers(QueryOffersOptions options, object clientData, OnQueryOffersCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryOffersOptionsInternal, QueryOffersOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryOffersCallbackInternal onQueryOffersCallbackInternal = new OnQueryOffersCallbackInternal(EcomInterface.OnQueryOffersCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryOffersCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryOffers(base.InnerHandle, zero, zero2, onQueryOffersCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0001CCB8 File Offset: 0x0001AEB8
		public void QueryOwnership(QueryOwnershipOptions options, object clientData, OnQueryOwnershipCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryOwnershipOptionsInternal, QueryOwnershipOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryOwnershipCallbackInternal onQueryOwnershipCallbackInternal = new OnQueryOwnershipCallbackInternal(EcomInterface.OnQueryOwnershipCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryOwnershipCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryOwnership(base.InnerHandle, zero, zero2, onQueryOwnershipCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public void QueryOwnershipToken(QueryOwnershipTokenOptions options, object clientData, OnQueryOwnershipTokenCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryOwnershipTokenOptionsInternal, QueryOwnershipTokenOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryOwnershipTokenCallbackInternal onQueryOwnershipTokenCallbackInternal = new OnQueryOwnershipTokenCallbackInternal(EcomInterface.OnQueryOwnershipTokenCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onQueryOwnershipTokenCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_QueryOwnershipToken(base.InnerHandle, zero, zero2, onQueryOwnershipTokenCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0001CD60 File Offset: 0x0001AF60
		public void RedeemEntitlements(RedeemEntitlementsOptions options, object clientData, OnRedeemEntitlementsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<RedeemEntitlementsOptionsInternal, RedeemEntitlementsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnRedeemEntitlementsCallbackInternal onRedeemEntitlementsCallbackInternal = new OnRedeemEntitlementsCallbackInternal(EcomInterface.OnRedeemEntitlementsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onRedeemEntitlementsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Ecom_RedeemEntitlements(base.InnerHandle, zero, zero2, onRedeemEntitlementsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		[MonoPInvokeCallback(typeof(OnCheckoutCallbackInternal))]
		internal static void OnCheckoutCallbackInternalImplementation(IntPtr data)
		{
			OnCheckoutCallback onCheckoutCallback;
			CheckoutCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnCheckoutCallback, CheckoutCallbackInfoInternal, CheckoutCallbackInfo>(data, out onCheckoutCallback, out data2))
			{
				onCheckoutCallback(data2);
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		[MonoPInvokeCallback(typeof(OnQueryEntitlementsCallbackInternal))]
		internal static void OnQueryEntitlementsCallbackInternalImplementation(IntPtr data)
		{
			OnQueryEntitlementsCallback onQueryEntitlementsCallback;
			QueryEntitlementsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryEntitlementsCallback, QueryEntitlementsCallbackInfoInternal, QueryEntitlementsCallbackInfo>(data, out onQueryEntitlementsCallback, out data2))
			{
				onQueryEntitlementsCallback(data2);
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0001CDF4 File Offset: 0x0001AFF4
		[MonoPInvokeCallback(typeof(OnQueryOffersCallbackInternal))]
		internal static void OnQueryOffersCallbackInternalImplementation(IntPtr data)
		{
			OnQueryOffersCallback onQueryOffersCallback;
			QueryOffersCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryOffersCallback, QueryOffersCallbackInfoInternal, QueryOffersCallbackInfo>(data, out onQueryOffersCallback, out data2))
			{
				onQueryOffersCallback(data2);
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0001CE14 File Offset: 0x0001B014
		[MonoPInvokeCallback(typeof(OnQueryOwnershipCallbackInternal))]
		internal static void OnQueryOwnershipCallbackInternalImplementation(IntPtr data)
		{
			OnQueryOwnershipCallback onQueryOwnershipCallback;
			QueryOwnershipCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryOwnershipCallback, QueryOwnershipCallbackInfoInternal, QueryOwnershipCallbackInfo>(data, out onQueryOwnershipCallback, out data2))
			{
				onQueryOwnershipCallback(data2);
			}
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0001CE34 File Offset: 0x0001B034
		[MonoPInvokeCallback(typeof(OnQueryOwnershipTokenCallbackInternal))]
		internal static void OnQueryOwnershipTokenCallbackInternalImplementation(IntPtr data)
		{
			OnQueryOwnershipTokenCallback onQueryOwnershipTokenCallback;
			QueryOwnershipTokenCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryOwnershipTokenCallback, QueryOwnershipTokenCallbackInfoInternal, QueryOwnershipTokenCallbackInfo>(data, out onQueryOwnershipTokenCallback, out data2))
			{
				onQueryOwnershipTokenCallback(data2);
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0001CE54 File Offset: 0x0001B054
		[MonoPInvokeCallback(typeof(OnRedeemEntitlementsCallbackInternal))]
		internal static void OnRedeemEntitlementsCallbackInternalImplementation(IntPtr data)
		{
			OnRedeemEntitlementsCallback onRedeemEntitlementsCallback;
			RedeemEntitlementsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnRedeemEntitlementsCallback, RedeemEntitlementsCallbackInfoInternal, RedeemEntitlementsCallbackInfo>(data, out onRedeemEntitlementsCallback, out data2))
			{
				onRedeemEntitlementsCallback(data2);
			}
		}

		// Token: 0x04000C92 RID: 3218
		public const int CatalogitemApiLatest = 1;

		// Token: 0x04000C93 RID: 3219
		public const int CatalogitemEntitlementendtimestampUndefined = -1;

		// Token: 0x04000C94 RID: 3220
		public const int CatalogofferApiLatest = 4;

		// Token: 0x04000C95 RID: 3221
		public const int CatalogofferExpirationtimestampUndefined = -1;

		// Token: 0x04000C96 RID: 3222
		public const int CatalogreleaseApiLatest = 1;

		// Token: 0x04000C97 RID: 3223
		public const int CheckoutApiLatest = 1;

		// Token: 0x04000C98 RID: 3224
		public const int CheckoutMaxEntries = 10;

		// Token: 0x04000C99 RID: 3225
		public const int CheckoutentryApiLatest = 1;

		// Token: 0x04000C9A RID: 3226
		public const int CopyentitlementbyidApiLatest = 2;

		// Token: 0x04000C9B RID: 3227
		public const int CopyentitlementbyindexApiLatest = 1;

		// Token: 0x04000C9C RID: 3228
		public const int CopyentitlementbynameandindexApiLatest = 1;

		// Token: 0x04000C9D RID: 3229
		public const int CopyitembyidApiLatest = 1;

		// Token: 0x04000C9E RID: 3230
		public const int CopyitemimageinfobyindexApiLatest = 1;

		// Token: 0x04000C9F RID: 3231
		public const int CopyitemreleasebyindexApiLatest = 1;

		// Token: 0x04000CA0 RID: 3232
		public const int CopyofferbyidApiLatest = 2;

		// Token: 0x04000CA1 RID: 3233
		public const int CopyofferbyindexApiLatest = 2;

		// Token: 0x04000CA2 RID: 3234
		public const int CopyofferimageinfobyindexApiLatest = 1;

		// Token: 0x04000CA3 RID: 3235
		public const int CopyofferitembyindexApiLatest = 1;

		// Token: 0x04000CA4 RID: 3236
		public const int CopytransactionbyidApiLatest = 1;

		// Token: 0x04000CA5 RID: 3237
		public const int CopytransactionbyindexApiLatest = 1;

		// Token: 0x04000CA6 RID: 3238
		public const int EntitlementApiLatest = 2;

		// Token: 0x04000CA7 RID: 3239
		public const int EntitlementEndtimestampUndefined = -1;

		// Token: 0x04000CA8 RID: 3240
		public const int GetentitlementsbynamecountApiLatest = 1;

		// Token: 0x04000CA9 RID: 3241
		public const int GetentitlementscountApiLatest = 1;

		// Token: 0x04000CAA RID: 3242
		public const int GetitemimageinfocountApiLatest = 1;

		// Token: 0x04000CAB RID: 3243
		public const int GetitemreleasecountApiLatest = 1;

		// Token: 0x04000CAC RID: 3244
		public const int GetoffercountApiLatest = 1;

		// Token: 0x04000CAD RID: 3245
		public const int GetofferimageinfocountApiLatest = 1;

		// Token: 0x04000CAE RID: 3246
		public const int GetofferitemcountApiLatest = 1;

		// Token: 0x04000CAF RID: 3247
		public const int GettransactioncountApiLatest = 1;

		// Token: 0x04000CB0 RID: 3248
		public const int ItemownershipApiLatest = 1;

		// Token: 0x04000CB1 RID: 3249
		public const int KeyimageinfoApiLatest = 1;

		// Token: 0x04000CB2 RID: 3250
		public const int QueryentitlementsApiLatest = 2;

		// Token: 0x04000CB3 RID: 3251
		public const int QueryentitlementsMaxEntitlementIds = 32;

		// Token: 0x04000CB4 RID: 3252
		public const int QueryoffersApiLatest = 1;

		// Token: 0x04000CB5 RID: 3253
		public const int QueryownershipApiLatest = 2;

		// Token: 0x04000CB6 RID: 3254
		public const int QueryownershipMaxCatalogIds = 32;

		// Token: 0x04000CB7 RID: 3255
		public const int QueryownershiptokenApiLatest = 2;

		// Token: 0x04000CB8 RID: 3256
		public const int QueryownershiptokenMaxCatalogitemIds = 32;

		// Token: 0x04000CB9 RID: 3257
		public const int RedeementitlementsApiLatest = 1;

		// Token: 0x04000CBA RID: 3258
		public const int RedeementitlementsMaxIds = 32;

		// Token: 0x04000CBB RID: 3259
		public const int TransactionidMaximumLength = 64;
	}
}
