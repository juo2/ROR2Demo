using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007E7 RID: 2023
	public class OnDestroyCallback : MonoBehaviour
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x000BB2FA File Offset: 0x000B94FA
		public void OnDestroy()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000BB310 File Offset: 0x000B9510
		public static OnDestroyCallback AddCallback(GameObject gameObject, Action<OnDestroyCallback> callback)
		{
			OnDestroyCallback onDestroyCallback = gameObject.AddComponent<OnDestroyCallback>();
			onDestroyCallback.callback = callback;
			return onDestroyCallback;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000BB31F File Offset: 0x000B951F
		public static void RemoveCallback(OnDestroyCallback callbackComponent)
		{
			callbackComponent.callback = null;
			UnityEngine.Object.Destroy(callbackComponent);
		}

		// Token: 0x04002E22 RID: 11810
		private Action<OnDestroyCallback> callback;
	}
}
