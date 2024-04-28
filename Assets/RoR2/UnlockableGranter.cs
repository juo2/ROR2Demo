using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008DF RID: 2271
	public class UnlockableGranter : MonoBehaviour
	{
		// Token: 0x060032F7 RID: 13047 RVA: 0x000D6C20 File Offset: 0x000D4E20
		public void GrantUnlockable(Interactor interactor)
		{
			string text = this.unlockableString;
			if (!this.unlockableDef && !string.IsNullOrEmpty(text))
			{
				this.unlockableDef = UnlockableCatalog.GetUnlockableDef(text);
			}
			CharacterBody component = interactor.GetComponent<CharacterBody>();
			if (component)
			{
				Run.instance.GrantUnlockToSinglePlayer(this.unlockableDef, component);
			}
		}

		// Token: 0x04003409 RID: 13321
		[Obsolete("'unlockableString' will be discontinued. Use 'unlockableDef' instead.", false)]
		[Tooltip("'unlockableString' will be discontinued. Use 'unlockableDef' instead.")]
		public string unlockableString;

		// Token: 0x0400340A RID: 13322
		public UnlockableDef unlockableDef;
	}
}
