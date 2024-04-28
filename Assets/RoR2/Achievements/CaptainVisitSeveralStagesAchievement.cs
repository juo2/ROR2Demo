using System;
using System.Collections.Generic;
using HG;

namespace RoR2.Achievements
{
	// Token: 0x02000E7D RID: 3709
	[RegisterAchievement("CaptainVisitSeveralStages", "Skills.Captain.CaptainSupplyDropEquipmentRestock", "CompleteMainEnding", null)]
	public class CaptainVisitSeveralStagesAchievement : BaseAchievement
	{
		// Token: 0x060054EC RID: 21740 RVA: 0x0015D615 File Offset: 0x0015B815
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CaptainBody");
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0015D6BD File Offset: 0x0015B8BD
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			this.visitedScenes = CollectionPool<SceneDef, List<SceneDef>>.RentCollection();
			SceneCatalog.onMostRecentSceneDefChanged += this.HandleMostRecentSceneDefChanged;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0015D6E1 File Offset: 0x0015B8E1
		protected override void OnBodyRequirementBroken()
		{
			SceneCatalog.onMostRecentSceneDefChanged -= this.HandleMostRecentSceneDefChanged;
			this.visitedScenes = CollectionPool<SceneDef, List<SceneDef>>.ReturnCollection(this.visitedScenes);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0015D70B File Offset: 0x0015B90B
		private void HandleMostRecentSceneDefChanged(SceneDef newSceneDef)
		{
			if (!this.visitedScenes.Contains(newSceneDef))
			{
				this.visitedScenes.Add(newSceneDef);
			}
			if (this.visitedScenes.Count >= CaptainVisitSeveralStagesAchievement.requirement)
			{
				base.Grant();
			}
		}

		// Token: 0x04005047 RID: 20551
		private static readonly int requirement = 10;

		// Token: 0x04005048 RID: 20552
		private List<SceneDef> visitedScenes;
	}
}
