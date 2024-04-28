using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.HudOverlay;
using RoR2.UI;
using UnityEngine;

namespace RoR2.VoidRaidCrab
{
	// Token: 0x02000B6F RID: 2927
	public class VoidRaidCrabHealthBarOverlayProvider : MonoBehaviour
	{
		// Token: 0x060042A7 RID: 17063 RVA: 0x00114200 File Offset: 0x00112400
		private void OnEnable()
		{
			this.overlayControllers.Clear();
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Player);
			this.overlayControllers = new List<OverlayController>();
			foreach (TeamComponent teamComponent in teamMembers)
			{
				OverlayController overlayController = HudOverlayManager.AddOverlay(teamComponent.gameObject, new OverlayCreationParams
				{
					prefab = this.healthbarOverlayPrefab,
					childLocatorEntry = this.overlayChildLocatorEntryName
				});
				overlayController.onInstanceAdded += this.OnOverlayInstanceAdded;
				this.overlayControllers.Add(overlayController);
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x001142A8 File Offset: 0x001124A8
		private void OnOverlayInstanceAdded(OverlayController overlayController, GameObject instance)
		{
			if (instance && this.phasedInventorySetter)
			{
				VoidRaidCrabHealthBarPipController component = instance.GetComponent<VoidRaidCrabHealthBarPipController>();
				if (component)
				{
					component.InitializePips(this.phasedInventorySetter);
				}
			}
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x001142E8 File Offset: 0x001124E8
		private void OnDisable()
		{
			foreach (OverlayController overlayController in this.overlayControllers)
			{
				HudOverlayManager.RemoveOverlay(overlayController);
			}
			this.overlayControllers.Clear();
		}

		// Token: 0x040040A0 RID: 16544
		[SerializeField]
		private GameObject healthbarOverlayPrefab;

		// Token: 0x040040A1 RID: 16545
		[SerializeField]
		private string overlayChildLocatorEntryName;

		// Token: 0x040040A2 RID: 16546
		[SerializeField]
		private PhasedInventorySetter phasedInventorySetter;

		// Token: 0x040040A3 RID: 16547
		private List<OverlayController> overlayControllers = new List<OverlayController>();
	}
}
