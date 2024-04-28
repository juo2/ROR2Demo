using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B5 RID: 2229
	[DisallowMultipleComponent]
	public class TeamComponent : NetworkBehaviour, ILifeBehavior
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000D21E4 File Offset: 0x000D03E4
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x000D21EC File Offset: 0x000D03EC
		public CharacterBody body { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000D221D File Offset: 0x000D041D
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x000D21F5 File Offset: 0x000D03F5
		public TeamIndex teamIndex
		{
			get
			{
				return this._teamIndex;
			}
			set
			{
				if (this._teamIndex == value)
				{
					return;
				}
				this._teamIndex = value;
				if (Application.isPlaying)
				{
					base.SetDirtyBit(1U);
					this.OnChangeTeam(value);
				}
			}
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000D2225 File Offset: 0x000D0425
		private static bool TeamIsValid(TeamIndex teamIndex)
		{
			return teamIndex >= TeamIndex.Neutral && teamIndex < TeamIndex.Count;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000D2231 File Offset: 0x000D0431
		private void OnChangeTeam(TeamIndex newTeamIndex)
		{
			this.OnLeaveTeam(this.oldTeamIndex);
			this.OnJoinTeam(newTeamIndex);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000D2246 File Offset: 0x000D0446
		private void OnLeaveTeam(TeamIndex oldTeamIndex)
		{
			if (TeamComponent.TeamIsValid(oldTeamIndex))
			{
				TeamComponent.teamsList[(int)oldTeamIndex].Remove(this);
			}
			Action<TeamComponent, TeamIndex> action = TeamComponent.onLeaveTeamGlobal;
			if (action == null)
			{
				return;
			}
			action(this, oldTeamIndex);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000D2270 File Offset: 0x000D0470
		private void OnJoinTeam(TeamIndex newTeamIndex)
		{
			if (TeamComponent.TeamIsValid(newTeamIndex))
			{
				TeamComponent.teamsList[(int)newTeamIndex].Add(this);
			}
			TeamComponent.indicatorSetupQueue.Enqueue(this);
			HurtBox[] array;
			if (!this.body)
			{
				array = null;
			}
			else
			{
				HurtBoxGroup hurtBoxGroup = this.body.hurtBoxGroup;
				array = ((hurtBoxGroup != null) ? hurtBoxGroup.hurtBoxes : null);
			}
			HurtBox[] array2 = array;
			if (array2 != null)
			{
				HurtBox[] array3 = array2;
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i].teamIndex = newTeamIndex;
				}
			}
			this.oldTeamIndex = newTeamIndex;
			Action<TeamComponent, TeamIndex> action = TeamComponent.onJoinTeamGlobal;
			if (action == null)
			{
				return;
			}
			action(this, newTeamIndex);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000D22FC File Offset: 0x000D04FC
		private static void ProcessIndicatorSetupRequests()
		{
			while (TeamComponent.indicatorSetupQueue.Count > 0)
			{
				TeamComponent teamComponent = TeamComponent.indicatorSetupQueue.Dequeue();
				if (teamComponent)
				{
					try
					{
						teamComponent.SetupIndicator();
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
				}
			}
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000D234C File Offset: 0x000D054C
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			RoR2Application.onFixedUpdate += TeamComponent.ProcessIndicatorSetupRequests;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000D235F File Offset: 0x000D055F
		public void RequestDefaultIndicator(GameObject newIndicatorPrefab)
		{
			this.defaultIndicatorPrefab = newIndicatorPrefab;
			TeamComponent.indicatorSetupQueue.Enqueue(this);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000D2374 File Offset: 0x000D0574
		private void SetupIndicator()
		{
			if (this.indicator)
			{
				return;
			}
			if (this.body)
			{
				CharacterMaster master = this.body.master;
				bool flag = master && master.isBoss;
				GameObject gameObject = this.defaultIndicatorPrefab;
				if (master && this.teamIndex == TeamIndex.Player)
				{
					gameObject = LegacyResourcesAPI.Load<GameObject>(this.body.isPlayerControlled ? "Prefabs/PositionIndicators/PlayerPositionIndicator" : "Prefabs/PositionIndicators/NPCPositionIndicator");
				}
				else if (flag)
				{
					gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/PositionIndicators/BossPositionIndicator");
				}
				if (this.indicator)
				{
					UnityEngine.Object.Destroy(this.indicator);
					this.indicator = null;
				}
				if (gameObject)
				{
					this.indicator = UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform);
					this.indicator.GetComponent<PositionIndicator>().targetTransform = this.body.coreTransform;
					Nameplate component = this.indicator.GetComponent<Nameplate>();
					if (component)
					{
						component.SetBody(this.body);
					}
				}
			}
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000D2478 File Offset: 0x000D0678
		static TeamComponent()
		{
			TeamComponent.teamsList = new List<TeamComponent>[5];
			TeamComponent.readonlyTeamsList = new ReadOnlyCollection<TeamComponent>[TeamComponent.teamsList.Length];
			for (int i = 0; i < TeamComponent.teamsList.Length; i++)
			{
				TeamComponent.teamsList[i] = new List<TeamComponent>();
				TeamComponent.readonlyTeamsList[i] = TeamComponent.teamsList[i].AsReadOnly();
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000D24EB File Offset: 0x000D06EB
		private void Awake()
		{
			this.body = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000D24F9 File Offset: 0x000D06F9
		public void Start()
		{
			this.SetupIndicator();
			if (this.oldTeamIndex != this.teamIndex)
			{
				this.OnChangeTeam(this.teamIndex);
			}
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000D251B File Offset: 0x000D071B
		private void OnDestroy()
		{
			this.teamIndex = TeamIndex.None;
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		public void OnDeathStart()
		{
			base.enabled = false;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000D2524 File Offset: 0x000D0724
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			writer.Write(this.teamIndex);
			return initialState || base.syncVarDirtyBits > 0U;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000D2540 File Offset: 0x000D0740
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			this.teamIndex = reader.ReadTeamIndex();
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000D254E File Offset: 0x000D074E
		public static ReadOnlyCollection<TeamComponent> GetTeamMembers(TeamIndex teamIndex)
		{
			if (!TeamComponent.TeamIsValid(teamIndex))
			{
				return TeamComponent.emptyTeamMembers;
			}
			return TeamComponent.readonlyTeamsList[(int)teamIndex];
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000D2568 File Offset: 0x000D0768
		public static TeamIndex GetObjectTeam(GameObject gameObject)
		{
			if (gameObject)
			{
				TeamComponent component = gameObject.GetComponent<TeamComponent>();
				if (component)
				{
					return component.teamIndex;
				}
			}
			return TeamIndex.None;
		}

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06003190 RID: 12688 RVA: 0x000D2594 File Offset: 0x000D0794
		// (remove) Token: 0x06003191 RID: 12689 RVA: 0x000D25C8 File Offset: 0x000D07C8
		public static event Action<TeamComponent, TeamIndex> onJoinTeamGlobal;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06003192 RID: 12690 RVA: 0x000D25FC File Offset: 0x000D07FC
		// (remove) Token: 0x06003193 RID: 12691 RVA: 0x000D2630 File Offset: 0x000D0830
		public static event Action<TeamComponent, TeamIndex> onLeaveTeamGlobal;

		// Token: 0x06003195 RID: 12693 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400330C RID: 13068
		public bool hideAllyCardDisplay;

		// Token: 0x0400330D RID: 13069
		[SerializeField]
		private TeamIndex _teamIndex = TeamIndex.None;

		// Token: 0x0400330F RID: 13071
		private TeamIndex oldTeamIndex = TeamIndex.None;

		// Token: 0x04003310 RID: 13072
		private GameObject defaultIndicatorPrefab;

		// Token: 0x04003311 RID: 13073
		private GameObject indicator;

		// Token: 0x04003312 RID: 13074
		private static readonly Queue<TeamComponent> indicatorSetupQueue = new Queue<TeamComponent>();

		// Token: 0x04003313 RID: 13075
		private static List<TeamComponent>[] teamsList;

		// Token: 0x04003314 RID: 13076
		private static ReadOnlyCollection<TeamComponent>[] readonlyTeamsList;

		// Token: 0x04003315 RID: 13077
		private static ReadOnlyCollection<TeamComponent> emptyTeamMembers = new List<TeamComponent>().AsReadOnly();
	}
}
