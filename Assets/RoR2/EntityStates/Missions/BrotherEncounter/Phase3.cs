using System;
using EntityStates.BrotherMonster;
using RoR2;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000253 RID: 595
	public class Phase3 : BrotherEncounterPhaseBaseState
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002BB50 File Offset: 0x00029D50
		protected override string phaseControllerChildString
		{
			get
			{
				return "Phase3";
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002BB57 File Offset: 0x00029D57
		protected override EntityState nextState
		{
			get
			{
				return new Phase4();
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002BB5E File Offset: 0x00029D5E
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002BB66 File Offset: 0x00029D66
		public override void OnExit()
		{
			base.KillAllMonsters();
			base.OnExit();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002BB74 File Offset: 0x00029D74
		protected override void OnMemberAddedServer(CharacterMaster master)
		{
			base.OnMemberAddedServer(master);
			if (master.hasBody)
			{
				CharacterBody body = master.GetBody();
				if (body)
				{
					CharacterDeathBehavior component = body.GetComponent<CharacterDeathBehavior>();
					if (component)
					{
						component.deathState = new SerializableEntityStateType(typeof(InstantDeathState));
					}
				}
			}
		}
	}
}
