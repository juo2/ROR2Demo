using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.Missions.Arena.NullWard
{
	// Token: 0x02000265 RID: 613
	public class Active : NullWardBaseState
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002BF98 File Offset: 0x0002A198
		public override void OnEnter()
		{
			base.OnEnter();
			this.holdoutZoneController = base.GetComponent<HoldoutZoneController>();
			this.holdoutZoneController.enabled = true;
			this.holdoutZoneController.baseRadius = NullWardBaseState.wardRadiusOn;
			this.purchaseInteraction.SetAvailable(false);
			base.arenaMissionController.rewardSpawnPosition = this.childLocator.FindChild("RewardSpawn").gameObject;
			base.arenaMissionController.monsterSpawnPosition = this.childLocator.FindChild("MonsterSpawn").gameObject;
			this.childLocator.FindChild("ActiveEffect").gameObject.SetActive(true);
			if (NetworkServer.active)
			{
				base.arenaMissionController.BeginRound();
			}
			if (base.isAuthority)
			{
				Active.startTime = Run.FixedTimeStamp.now;
			}
			Util.PlaySound(Active.soundEntryEvent, base.gameObject);
			Util.PlaySound(Active.soundLoopStartEvent, base.gameObject);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002C080 File Offset: 0x0002A280
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.sphereZone.Networkradius = this.holdoutZoneController.currentRadius;
			if (base.isAuthority && this.holdoutZoneController.charge >= 1f)
			{
				this.outer.SetNextState(new Complete());
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
		public override void OnExit()
		{
			if (this.holdoutZoneController)
			{
				this.holdoutZoneController.enabled = false;
			}
			Util.PlaySound(Active.soundLoopEndEvent, base.gameObject);
			this.childLocator.FindChild("ActiveEffect").gameObject.SetActive(false);
			this.childLocator.FindChild("WardOnEffect").gameObject.SetActive(false);
			base.OnExit();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002C147 File Offset: 0x0002A347
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(Active.startTime);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002C15B File Offset: 0x0002A35B
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			Active.startTime = reader.ReadFixedTimeStamp();
		}

		// Token: 0x04000C49 RID: 3145
		public static string soundEntryEvent;

		// Token: 0x04000C4A RID: 3146
		public static string soundLoopStartEvent;

		// Token: 0x04000C4B RID: 3147
		public static string soundLoopEndEvent;

		// Token: 0x04000C4C RID: 3148
		private static Run.FixedTimeStamp startTime;

		// Token: 0x04000C4D RID: 3149
		private HoldoutZoneController holdoutZoneController;
	}
}
