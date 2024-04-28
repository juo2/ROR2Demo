using System;

namespace RoR2.Artifacts
{
	// Token: 0x02000E6B RID: 3691
	public static class FriendlyFireArtifactManager
	{
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x0015C65A File Offset: 0x0015A85A
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.friendlyFireArtifactDef;
			}
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0015C661 File Offset: 0x0015A861
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.onArtifactEnabledGlobal += FriendlyFireArtifactManager.OnArtifactEnabledGlobal;
			RunArtifactManager.onArtifactDisabledGlobal += FriendlyFireArtifactManager.OnArtifactDisabledGlobal;
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0015C685 File Offset: 0x0015A885
		private static void OnArtifactEnabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != FriendlyFireArtifactManager.myArtifact)
			{
				return;
			}
			FriendlyFireManager.friendlyFireMode = FriendlyFireManager.FriendlyFireMode.FriendlyFire;
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0015C69B File Offset: 0x0015A89B
		private static void OnArtifactDisabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != FriendlyFireArtifactManager.myArtifact)
			{
				return;
			}
			FriendlyFireManager.friendlyFireMode = FriendlyFireManager.FriendlyFireMode.Off;
		}
	}
}
