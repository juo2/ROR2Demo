using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2;
using RoR2.Audio;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidCamp
{
	// Token: 0x0200015E RID: 350
	public class Idle : EntityState
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x0001A7F8 File Offset: 0x000189F8
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.baseAnimationLayerName, this.baseAnimationStateName);
			this.PlayAnimation(this.additiveAnimationLayerName, this.additiveAnimationStateName);
			this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
			this.countdown = this.initialClearCheckDelay;
			this.indicatedNetIds = new HashSet<NetworkInstanceId>();
			base.GetComponent<OutsideInteractableLocker>().enabled = true;
			ObjectivePanelController.collectObjectiveSources += this.OnCollectObjectiveSources;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A87C File Offset: 0x00018A7C
		private void OnCollectObjectiveSources(CharacterMaster master, List<ObjectivePanelController.ObjectiveSourceDescriptor> objectiveSourcesList)
		{
			objectiveSourcesList.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
			{
				master = master,
				objectiveType = typeof(Idle.VoidCampObjectiveTracker),
				source = base.gameObject
			});
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.countdown -= Time.fixedDeltaTime;
			if (this.countdown < 0f)
			{
				this.countdown = this.clearCheckRate;
				ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Void);
				int count = teamMembers.Count;
				if (count <= 0)
				{
					this.outer.SetNextState(new Deactivate());
					return;
				}
				if (this.hasEnabledIndicators || count <= this.indicatorMaxTeamCountThreshold)
				{
					this.hasEnabledIndicators = true;
					foreach (TeamComponent teamComponent in teamMembers)
					{
						if (teamComponent && teamComponent.body && teamComponent.body.master)
						{
							this.RequestIndicatorForMaster(teamComponent.body.master);
						}
					}
				}
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		public override void OnExit()
		{
			LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			base.GetComponent<OutsideInteractableLocker>().enabled = false;
			ObjectivePanelController.collectObjectiveSources -= this.OnCollectObjectiveSources;
			base.OnExit();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		protected void RequestIndicatorForMaster(CharacterMaster master)
		{
			if (!this.indicatedNetIds.Contains(master.netId))
			{
				GameObject bodyObject = master.GetBodyObject();
				if (bodyObject)
				{
					TeamComponent component = bodyObject.GetComponent<TeamComponent>();
					if (component)
					{
						this.indicatedNetIds.Add(master.netId);
						component.RequestDefaultIndicator(this.indicatorPrefab);
					}
				}
			}
		}

		// Token: 0x0400077B RID: 1915
		[SerializeField]
		public string baseAnimationLayerName;

		// Token: 0x0400077C RID: 1916
		[SerializeField]
		public string baseAnimationStateName;

		// Token: 0x0400077D RID: 1917
		[SerializeField]
		public string additiveAnimationLayerName;

		// Token: 0x0400077E RID: 1918
		[SerializeField]
		public string additiveAnimationStateName;

		// Token: 0x0400077F RID: 1919
		[SerializeField]
		public float clearCheckRate;

		// Token: 0x04000780 RID: 1920
		[SerializeField]
		public float initialClearCheckDelay;

		// Token: 0x04000781 RID: 1921
		[SerializeField]
		public LoopSoundDef loopSoundDef;

		// Token: 0x04000782 RID: 1922
		[SerializeField]
		public int indicatorMaxTeamCountThreshold;

		// Token: 0x04000783 RID: 1923
		[SerializeField]
		public GameObject indicatorPrefab;

		// Token: 0x04000784 RID: 1924
		private LoopSoundManager.SoundLoopPtr loopPtr;

		// Token: 0x04000785 RID: 1925
		private bool hasEnabledIndicators;

		// Token: 0x04000786 RID: 1926
		private HashSet<NetworkInstanceId> indicatedNetIds;

		// Token: 0x04000787 RID: 1927
		private float countdown;

		// Token: 0x0200015F RID: 351
		private class VoidCampObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x06000628 RID: 1576 RVA: 0x0001AA34 File Offset: 0x00018C34
			protected override string GenerateString()
			{
				int count = TeamComponent.GetTeamMembers(TeamIndex.Void).Count;
				return string.Format(Language.GetString("OBJECTIVE_VOIDCAMP"), count);
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool IsDirty()
			{
				return true;
			}
		}
	}
}
