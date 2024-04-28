using System;
using System.Collections.Generic;
using RoR2.UI;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006FC RID: 1788
	public class GenericObjectiveProvider : MonoBehaviour
	{
		// Token: 0x06002488 RID: 9352 RVA: 0x0009BF25 File Offset: 0x0009A125
		private void OnEnable()
		{
			if (!InstanceTracker.Any<GenericObjectiveProvider>())
			{
				ObjectivePanelController.collectObjectiveSources += GenericObjectiveProvider.collectObjectiveSourcesDelegate;
			}
			InstanceTracker.Add<GenericObjectiveProvider>(this);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0009BF3E File Offset: 0x0009A13E
		private void OnDisable()
		{
			InstanceTracker.Remove<GenericObjectiveProvider>(this);
			if (!InstanceTracker.Any<GenericObjectiveProvider>())
			{
				ObjectivePanelController.collectObjectiveSources -= GenericObjectiveProvider.collectObjectiveSourcesDelegate;
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0009BF58 File Offset: 0x0009A158
		private static void CollectObjectiveSources(CharacterMaster viewer, List<ObjectivePanelController.ObjectiveSourceDescriptor> dest)
		{
			foreach (GenericObjectiveProvider source in InstanceTracker.GetInstancesList<GenericObjectiveProvider>())
			{
				dest.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
				{
					master = viewer,
					objectiveType = typeof(GenericObjectiveProvider.GenericObjectiveTracker),
					source = source
				});
			}
		}

		// Token: 0x040028AE RID: 10414
		public string objectiveToken;

		// Token: 0x040028AF RID: 10415
		public bool markCompletedOnRetired = true;

		// Token: 0x040028B0 RID: 10416
		private static readonly Action<CharacterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor>> collectObjectiveSourcesDelegate = new Action<CharacterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor>>(GenericObjectiveProvider.CollectObjectiveSources);

		// Token: 0x020006FD RID: 1789
		private class GenericObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x0600248D RID: 9357 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
			protected override string GenerateString()
			{
				GenericObjectiveProvider genericObjectiveProvider = (GenericObjectiveProvider)this.sourceDescriptor.source;
				this.previousToken = genericObjectiveProvider.objectiveToken;
				return Language.GetString(genericObjectiveProvider.objectiveToken);
			}

			// Token: 0x0600248E RID: 9358 RVA: 0x0009C02D File Offset: 0x0009A22D
			protected override bool IsDirty()
			{
				return ((GenericObjectiveProvider)this.sourceDescriptor.source).objectiveToken != this.previousToken;
			}

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x0600248F RID: 9359 RVA: 0x0009C04F File Offset: 0x0009A24F
			protected override bool shouldConsiderComplete
			{
				get
				{
					return this.retired && ((GenericObjectiveProvider)this.sourceDescriptor.source).markCompletedOnRetired;
				}
			}

			// Token: 0x040028B1 RID: 10417
			private string previousToken;
		}
	}
}
