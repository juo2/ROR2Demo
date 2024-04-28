using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000005 RID: 5
	internal class CallResult<T> : CallResult
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002199 File Offset: 0x00000399
		internal CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<T, bool> callbackFunction, CallResult<T>.ConvertFromPointer fromPointer, int resultSize, int callbackId) : base(steamworks, call)
		{
			this.ResultSize = resultSize;
			this.CallbackId = callbackId;
			this.CallbackFunction = callbackFunction;
			this.ConvertFromPointerFunction = fromPointer;
			this.Steamworks.RegisterCallResult(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D5 File Offset: 0x000003D5
		public override string ToString()
		{
			return string.Format("CallResult( {0}, {1}, {2}b )", typeof(T).Name, this.CallbackId, this.ResultSize);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002208 File Offset: 0x00000408
		internal unsafe override void RunCallback()
		{
			bool flag = false;
			byte[] array;
			byte* value;
			if ((array = CallResult<T>.resultBuffer) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			if (!this.Steamworks.native.utils.GetAPICallResult(this.Call, (IntPtr)((void*)value), CallResult<T>.resultBuffer.Length, this.CallbackId, ref flag) || flag)
			{
				this.CallbackFunction(default(T), true);
				return;
			}
			T arg = this.ConvertFromPointerFunction((IntPtr)((void*)value));
			this.CallbackFunction(arg, false);
			array = null;
		}

		// Token: 0x0400000B RID: 11
		private static byte[] resultBuffer = new byte[16384];

		// Token: 0x0400000C RID: 12
		private Action<T, bool> CallbackFunction;

		// Token: 0x0400000D RID: 13
		private CallResult<T>.ConvertFromPointer ConvertFromPointerFunction;

		// Token: 0x0400000E RID: 14
		internal int ResultSize = -1;

		// Token: 0x0400000F RID: 15
		internal int CallbackId;

		// Token: 0x0200018C RID: 396
		// (Invoke) Token: 0x06000C45 RID: 3141
		internal delegate T ConvertFromPointer(IntPtr p);
	}
}
