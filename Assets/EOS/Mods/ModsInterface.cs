using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002CB RID: 715
	public sealed class ModsInterface : Handle
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x000036D3 File Offset: 0x000018D3
		public ModsInterface()
		{
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000036DB File Offset: 0x000018DB
		public ModsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000135D4 File Offset: 0x000117D4
		public Result CopyModInfo(CopyModInfoOptions options, out ModInfo outEnumeratedMods)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyModInfoOptionsInternal, CopyModInfoOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Mods_CopyModInfo(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<ModInfoInternal, ModInfo>(zero2, out outEnumeratedMods))
			{
				Bindings.EOS_Mods_ModInfo_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0001361C File Offset: 0x0001181C
		public void EnumerateMods(EnumerateModsOptions options, object clientData, OnEnumerateModsCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<EnumerateModsOptionsInternal, EnumerateModsOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnEnumerateModsCallbackInternal onEnumerateModsCallbackInternal = new OnEnumerateModsCallbackInternal(ModsInterface.OnEnumerateModsCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onEnumerateModsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_EnumerateMods(base.InnerHandle, zero, zero2, onEnumerateModsCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00013670 File Offset: 0x00011870
		public void InstallMod(InstallModOptions options, object clientData, OnInstallModCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<InstallModOptionsInternal, InstallModOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnInstallModCallbackInternal onInstallModCallbackInternal = new OnInstallModCallbackInternal(ModsInterface.OnInstallModCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onInstallModCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_InstallMod(base.InnerHandle, zero, zero2, onInstallModCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000136C4 File Offset: 0x000118C4
		public void UninstallMod(UninstallModOptions options, object clientData, OnUninstallModCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UninstallModOptionsInternal, UninstallModOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUninstallModCallbackInternal onUninstallModCallbackInternal = new OnUninstallModCallbackInternal(ModsInterface.OnUninstallModCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUninstallModCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_UninstallMod(base.InnerHandle, zero, zero2, onUninstallModCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00013718 File Offset: 0x00011918
		public void UpdateMod(UpdateModOptions options, object clientData, OnUpdateModCallback completionDelegate)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<UpdateModOptionsInternal, UpdateModOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnUpdateModCallbackInternal onUpdateModCallbackInternal = new OnUpdateModCallbackInternal(ModsInterface.OnUpdateModCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionDelegate, onUpdateModCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_UpdateMod(base.InnerHandle, zero, zero2, onUpdateModCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0001376C File Offset: 0x0001196C
		[MonoPInvokeCallback(typeof(OnEnumerateModsCallbackInternal))]
		internal static void OnEnumerateModsCallbackInternalImplementation(IntPtr data)
		{
			OnEnumerateModsCallback onEnumerateModsCallback;
			EnumerateModsCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnEnumerateModsCallback, EnumerateModsCallbackInfoInternal, EnumerateModsCallbackInfo>(data, out onEnumerateModsCallback, out data2))
			{
				onEnumerateModsCallback(data2);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0001378C File Offset: 0x0001198C
		[MonoPInvokeCallback(typeof(OnInstallModCallbackInternal))]
		internal static void OnInstallModCallbackInternalImplementation(IntPtr data)
		{
			OnInstallModCallback onInstallModCallback;
			InstallModCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnInstallModCallback, InstallModCallbackInfoInternal, InstallModCallbackInfo>(data, out onInstallModCallback, out data2))
			{
				onInstallModCallback(data2);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000137AC File Offset: 0x000119AC
		[MonoPInvokeCallback(typeof(OnUninstallModCallbackInternal))]
		internal static void OnUninstallModCallbackInternalImplementation(IntPtr data)
		{
			OnUninstallModCallback onUninstallModCallback;
			UninstallModCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUninstallModCallback, UninstallModCallbackInfoInternal, UninstallModCallbackInfo>(data, out onUninstallModCallback, out data2))
			{
				onUninstallModCallback(data2);
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000137CC File Offset: 0x000119CC
		[MonoPInvokeCallback(typeof(OnUpdateModCallbackInternal))]
		internal static void OnUpdateModCallbackInternalImplementation(IntPtr data)
		{
			OnUpdateModCallback onUpdateModCallback;
			UpdateModCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnUpdateModCallback, UpdateModCallbackInfoInternal, UpdateModCallbackInfo>(data, out onUpdateModCallback, out data2))
			{
				onUpdateModCallback(data2);
			}
		}

		// Token: 0x04000890 RID: 2192
		public const int CopymodinfoApiLatest = 1;

		// Token: 0x04000891 RID: 2193
		public const int EnumeratemodsApiLatest = 1;

		// Token: 0x04000892 RID: 2194
		public const int InstallmodApiLatest = 1;

		// Token: 0x04000893 RID: 2195
		public const int ModIdentifierApiLatest = 1;

		// Token: 0x04000894 RID: 2196
		public const int ModinfoApiLatest = 1;

		// Token: 0x04000895 RID: 2197
		public const int UninstallmodApiLatest = 1;

		// Token: 0x04000896 RID: 2198
		public const int UpdatemodApiLatest = 1;
	}
}
