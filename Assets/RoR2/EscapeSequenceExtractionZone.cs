using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006CC RID: 1740
	public class EscapeSequenceExtractionZone : MonoBehaviour
	{
		// Token: 0x06002234 RID: 8756 RVA: 0x00093F1B File Offset: 0x0009211B
		private void Awake()
		{
			this.playersInRadius = new List<CharacterBody>();
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00093F28 File Offset: 0x00092128
		private void FixedUpdate()
		{
			this.livingPlayerCount = EscapeSequenceExtractionZone.CountLivingPlayers(this.teamIndex);
			this.playersInRadius.Clear();
			this.GetPlayersInRadius(base.transform.position, this.radius * this.radius, this.teamIndex, this.playersInRadius);
			if (NetworkServer.active && this.livingPlayerCount > 0 && this.playersInRadius.Count >= this.livingPlayerCount)
			{
				this.HandleEndingServer();
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00093FA5 File Offset: 0x000921A5
		private void OnEnable()
		{
			if (InstanceTracker.GetInstancesList<EscapeSequenceExtractionZone>().Count == 0)
			{
				ObjectivePanelController.collectObjectiveSources += EscapeSequenceExtractionZone.ReportObjectives;
			}
			InstanceTracker.Add<EscapeSequenceExtractionZone>(this);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00093FCA File Offset: 0x000921CA
		private void OnDisable()
		{
			InstanceTracker.Remove<EscapeSequenceExtractionZone>(this);
			if (InstanceTracker.GetInstancesList<EscapeSequenceExtractionZone>().Count == 0)
			{
				ObjectivePanelController.collectObjectiveSources -= EscapeSequenceExtractionZone.ReportObjectives;
			}
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00093FEF File Offset: 0x000921EF
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(base.transform.position, this.radius);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00094014 File Offset: 0x00092214
		public void KillAllStragglers()
		{
			List<CharacterMaster> list = new List<CharacterMaster>(CharacterMaster.readOnlyInstancesList);
			for (int i = 0; i < list.Count; i++)
			{
				CharacterMaster characterMaster = list[i];
				if (characterMaster && !this.playersInRadius.Contains(characterMaster.GetBody()))
				{
					characterMaster.TrueKill(null, null, DamageType.Silent | DamageType.VoidDeath);
				}
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0009406D File Offset: 0x0009226D
		public void HandleEndingServer()
		{
			if (Run.instance.isGameOverServer)
			{
				return;
			}
			this.KillAllStragglers();
			if (this.playersInRadius.Count > 0)
			{
				Run.instance.BeginGameOver(this.successEnding);
			}
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000940A0 File Offset: 0x000922A0
		private static void ReportObjectives(CharacterMaster characterMaster, List<ObjectivePanelController.ObjectiveSourceDescriptor> dest)
		{
			List<EscapeSequenceExtractionZone> instancesList = InstanceTracker.GetInstancesList<EscapeSequenceExtractionZone>();
			for (int i = 0; i < instancesList.Count; i++)
			{
				EscapeSequenceExtractionZone escapeSequenceExtractionZone = instancesList[i];
				if (characterMaster.teamIndex == escapeSequenceExtractionZone.teamIndex)
				{
					ObjectivePanelController.ObjectiveSourceDescriptor objectiveSourceDescriptor = default(ObjectivePanelController.ObjectiveSourceDescriptor);
					objectiveSourceDescriptor.master = characterMaster;
					objectiveSourceDescriptor.objectiveType = typeof(EscapeSequenceExtractionZone.EscapeSequenceExtractionZoneObjectiveTracker);
					objectiveSourceDescriptor.source = escapeSequenceExtractionZone;
				}
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00094104 File Offset: 0x00092304
		private static bool IsPointInRadius(Vector3 origin, float chargingRadiusSqr, Vector3 point)
		{
			return (point - origin).sqrMagnitude <= chargingRadiusSqr;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00094126 File Offset: 0x00092326
		private static bool IsBodyInRadius(Vector3 origin, float chargingRadiusSqr, CharacterBody characterBody)
		{
			return EscapeSequenceExtractionZone.IsPointInRadius(origin, chargingRadiusSqr, characterBody.corePosition);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00094138 File Offset: 0x00092338
		private static int CountLivingPlayers(TeamIndex teamIndex)
		{
			int num = 0;
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				if (teamMembers[i].body.isPlayerControlled)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00094178 File Offset: 0x00092378
		public int CountPlayersInRadius(Vector3 origin, float chargingRadiusSqr, TeamIndex teamIndex)
		{
			int num = 0;
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				TeamComponent teamComponent = teamMembers[i];
				if (teamComponent.body.isPlayerControlled && EscapeSequenceExtractionZone.IsBodyInRadius(origin, chargingRadiusSqr, teamComponent.body))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000941C8 File Offset: 0x000923C8
		private int GetPlayersInRadius(Vector3 origin, float chargingRadiusSqr, TeamIndex teamIndex, List<CharacterBody> dest)
		{
			int result = 0;
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
			for (int i = 0; i < teamMembers.Count; i++)
			{
				TeamComponent teamComponent = teamMembers[i];
				if (teamComponent.body.isPlayerControlled && EscapeSequenceExtractionZone.IsBodyInRadius(origin, chargingRadiusSqr, teamComponent.body))
				{
					dest.Add(teamComponent.body);
				}
			}
			return result;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00094221 File Offset: 0x00092421
		public bool IsBodyInRadius(CharacterBody body)
		{
			return body && EscapeSequenceExtractionZone.IsBodyInRadius(base.transform.position, this.radius * this.radius, body);
		}

		// Token: 0x04002751 RID: 10065
		public float radius;

		// Token: 0x04002752 RID: 10066
		public string objectiveToken;

		// Token: 0x04002753 RID: 10067
		public GameEndingDef successEnding;

		// Token: 0x04002754 RID: 10068
		private int livingPlayerCount;

		// Token: 0x04002755 RID: 10069
		private List<CharacterBody> playersInRadius;

		// Token: 0x04002756 RID: 10070
		private TeamIndex teamIndex = TeamIndex.Player;

		// Token: 0x020006CD RID: 1741
		private class EscapeSequenceExtractionZoneObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06002243 RID: 8771 RVA: 0x0009425A File Offset: 0x0009245A
			private EscapeSequenceExtractionZone extractionZone
			{
				get
				{
					return (EscapeSequenceExtractionZone)this.sourceDescriptor.source;
				}
			}

			// Token: 0x06002244 RID: 8772 RVA: 0x0009426C File Offset: 0x0009246C
			public override string ToString()
			{
				this.cachedLivingPlayersCount = this.extractionZone.livingPlayerCount;
				this.cachedPlayersInRadiusCount = this.extractionZone.playersInRadius.Count;
				string text = Language.GetString(this.extractionZone.objectiveToken);
				if (this.cachedLivingPlayersCount >= 1)
				{
					text = Language.GetStringFormatted("OBJECTIVE_FRACTION_PROGRESS_FORMAT", new object[]
					{
						text,
						this.cachedPlayersInRadiusCount,
						this.cachedLivingPlayersCount
					});
				}
				return text;
			}

			// Token: 0x06002245 RID: 8773 RVA: 0x000942EC File Offset: 0x000924EC
			protected override bool IsDirty()
			{
				return this.cachedLivingPlayersCount != this.extractionZone.livingPlayerCount && this.cachedPlayersInRadiusCount != this.extractionZone.playersInRadius.Count;
			}

			// Token: 0x04002757 RID: 10071
			private int cachedLivingPlayersCount;

			// Token: 0x04002758 RID: 10072
			private int cachedPlayersInRadiusCount;
		}
	}
}
