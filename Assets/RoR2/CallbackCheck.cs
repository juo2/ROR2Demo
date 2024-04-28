using System;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x020004F5 RID: 1269
	public class CallbackCheck<TResult, TArg> where TResult : struct
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x00066124 File Offset: 0x00064324
		public void AddCallback([NotNull] CallbackCheck<TResult, TArg>.CallbackDelegate callback)
		{
			if (this.callbacks.Length <= this.callbackCount + 1)
			{
				Array.Resize<CallbackCheck<TResult, TArg>.CallbackDelegate>(ref this.callbacks, this.callbackCount + 1);
			}
			CallbackCheck<TResult, TArg>.CallbackDelegate[] array = this.callbacks;
			int num = this.callbackCount;
			this.callbackCount = num + 1;
			array[num] = callback;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00066170 File Offset: 0x00064370
		public void RemoveCallback([NotNull] CallbackCheck<TResult, TArg>.CallbackDelegate callback)
		{
			for (int i = 0; i < this.callbackCount; i++)
			{
				if (this.callbacks[i] == callback)
				{
					int num = this.callbackCount - 1;
					while (i < num)
					{
						this.callbacks[i] = this.callbacks[i + 1];
						i++;
					}
					CallbackCheck<TResult, TArg>.CallbackDelegate[] array = this.callbacks;
					int num2 = this.callbackCount - 1;
					this.callbackCount = num2;
					array[num2] = null;
					return;
				}
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000661D8 File Offset: 0x000643D8
		public void Clear()
		{
			Array.Clear(this.callbacks, 0, this.callbackCount);
			this.callbackCount = 0;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000661F4 File Offset: 0x000643F4
		public TResult? Evaluate(TArg arg)
		{
			TResult? result = null;
			for (int i = 0; i < this.callbackCount; i++)
			{
				this.callbacks[i](arg, ref result);
				if (result != null)
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x04001CD3 RID: 7379
		private CallbackCheck<TResult, TArg>.CallbackDelegate[] callbacks = Array.Empty<CallbackCheck<TResult, TArg>.CallbackDelegate>();

		// Token: 0x04001CD4 RID: 7380
		private int callbackCount;

		// Token: 0x020004F6 RID: 1270
		// (Invoke) Token: 0x0600171F RID: 5919
		public delegate void CallbackDelegate(TArg arg, ref TResult? resultOverride);
	}
}
