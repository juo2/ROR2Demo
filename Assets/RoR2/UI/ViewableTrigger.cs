using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DAF RID: 3503
	public class ViewableTrigger : MonoBehaviour
	{
		// Token: 0x06005043 RID: 20547 RVA: 0x0014C1C8 File Offset: 0x0014A3C8
		private void OnEnable()
		{
			ViewableTrigger.TriggerView(this.viewableName);
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0014C1D8 File Offset: 0x0014A3D8
		public static void TriggerView(string viewableName)
		{
			if (string.IsNullOrEmpty(viewableName))
			{
				return;
			}
			foreach (LocalUser localUser in LocalUserManager.readOnlyLocalUsersList)
			{
				localUser.userProfile.MarkViewableAsViewed(viewableName);
			}
		}

		// Token: 0x04004CEC RID: 19692
		[Tooltip("The name of the viewable to mark as viewed when this component becomes enabled.")]
		public string viewableName;
	}
}
