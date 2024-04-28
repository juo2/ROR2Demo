using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Interactables.MSObelisk
{
	// Token: 0x020002F1 RID: 753
	public class EndingGame : BaseState
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x00038D16 File Offset: 0x00036F16
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00038D2C File Offset: 0x00036F2C
		private void FixedUpdateServer()
		{
			this.destroyTimer -= Time.fixedDeltaTime;
			if (!this.beginEndingGame)
			{
				if (this.destroyTimer <= 0f)
				{
					this.destroyTimer = EndingGame.timeBetweenDestroy;
					ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Player);
					if (teamMembers.Count <= 0)
					{
						this.beginEndingGame = true;
						return;
					}
					GameObject gameObject = teamMembers[0].gameObject;
					CharacterBody component = gameObject.GetComponent<CharacterBody>();
					if (component)
					{
						EffectManager.SpawnEffect(EndingGame.destroyEffectPrefab, new EffectData
						{
							origin = component.corePosition,
							scale = component.radius
						}, true);
						EntityState.Destroy(gameObject.gameObject);
						return;
					}
				}
			}
			else
			{
				this.endGameTimer += Time.fixedDeltaTime;
				if (this.endGameTimer >= EndingGame.timeUntilEndGame && Run.instance)
				{
					this.DoFinalAction();
				}
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00038E0C File Offset: 0x0003700C
		private void DoFinalAction()
		{
			bool flag = false;
			for (int i = 0; i < CharacterMaster.readOnlyInstancesList.Count; i++)
			{
				if (CharacterMaster.readOnlyInstancesList[i].inventory.GetItemCount(RoR2Content.Items.LunarTrinket) > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.outer.SetNextState(new TransitionToNextStage());
				return;
			}
			Run.instance.BeginGameOver(RoR2Content.GameEndings.ObliterationEnding);
			this.outer.SetNextState(new Idle());
		}

		// Token: 0x0400107F RID: 4223
		public static GameObject destroyEffectPrefab;

		// Token: 0x04001080 RID: 4224
		public static float timeBetweenDestroy;

		// Token: 0x04001081 RID: 4225
		public static float timeUntilEndGame;

		// Token: 0x04001082 RID: 4226
		private float destroyTimer;

		// Token: 0x04001083 RID: 4227
		private float endGameTimer;

		// Token: 0x04001084 RID: 4228
		private bool beginEndingGame;
	}
}
