using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006B7 RID: 1719
	[RequireComponent(typeof(NetworkIdentity))]
	public sealed class DummyPingableInteraction : MonoBehaviour, IInteractable, IDisplayNameProvider
	{
		// Token: 0x06002175 RID: 8565 RVA: 0x000903B8 File Offset: 0x0008E5B8
		public string GetContextString(Interactor activator)
		{
			return Language.GetString(this.contextToken);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000903C5 File Offset: 0x0008E5C5
		public Interactability GetInteractability(Interactor activator)
		{
			return this.interactability;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnInteractionBegin(Interactor activator)
		{
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000903CD File Offset: 0x0008E5CD
		public string GetDisplayName()
		{
			return Language.GetString(this.displayNameToken);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return true;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000903DA File Offset: 0x0008E5DA
		public void OnEnable()
		{
			InstanceTracker.Add<DummyPingableInteraction>(this);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000903E2 File Offset: 0x0008E5E2
		public void OnDisable()
		{
			InstanceTracker.Remove<DummyPingableInteraction>(this);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000903EA File Offset: 0x0008E5EA
		public bool ShouldShowOnScanner()
		{
			return this.interactability > Interactability.Disabled;
		}

		// Token: 0x040026DD RID: 9949
		public string displayNameToken = "DUMMYINTERACTION_NAME";

		// Token: 0x040026DE RID: 9950
		public string contextToken = "DUMMYINTERACTION_CONTEXT";

		// Token: 0x040026DF RID: 9951
		public Interactability interactability = Interactability.ConditionsNotMet;
	}
}
