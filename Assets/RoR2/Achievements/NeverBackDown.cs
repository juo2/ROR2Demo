using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EBB RID: 3771
	[RegisterAchievement("NeverBackDown", "Items.FocusConvergence", null, typeof(NeverBackDown))]
	public class NeverBackDown : BaseAchievement
	{
		// Token: 0x060055EE RID: 21998 RVA: 0x0015EEB9 File Offset: 0x0015D0B9
		public override void OnInstall()
		{
			base.OnInstall();
			TeleporterInteraction.onTeleporterBeginChargingGlobal += this.OnTeleporterBeginCharging;
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterCharged;
			Run.onRunStartGlobal += this.OnRunStartGlobal;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0015EEF4 File Offset: 0x0015D0F4
		public override void OnUninstall()
		{
			this.checkingForFailure = false;
			Run.onRunStartGlobal -= this.OnRunStartGlobal;
			TeleporterInteraction.onTeleporterBeginChargingGlobal -= this.OnTeleporterBeginCharging;
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterCharged;
			base.OnUninstall();
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060055F0 RID: 22000 RVA: 0x0015EF41 File Offset: 0x0015D141
		// (set) Token: 0x060055F1 RID: 22001 RVA: 0x0015EF49 File Offset: 0x0015D149
		private bool checkingForFailure
		{
			get
			{
				return this._checkingForFailure;
			}
			set
			{
				if (this._checkingForFailure == value)
				{
					return;
				}
				this._checkingForFailure = value;
				if (this._checkingForFailure)
				{
					RoR2Application.onFixedUpdate += this.CheckForFailure;
					return;
				}
				RoR2Application.onFixedUpdate -= this.CheckForFailure;
			}
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x0015EF87 File Offset: 0x0015D187
		private void OnRunStartGlobal(Run run)
		{
			this.levels = 0;
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x0015EF90 File Offset: 0x0015D190
		private void OnTeleporterBeginCharging(TeleporterInteraction teleporterInteraction)
		{
			this.hasLeftRadius = false;
			this.teleporterStartChargingTime = Run.FixedTimeStamp.now;
			this.checkingForFailure = true;
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x0015EFAB File Offset: 0x0015D1AB
		private void OnTeleporterCharged(TeleporterInteraction teleporterInteraction)
		{
			if (!this.hasLeftRadius)
			{
				this.levels++;
			}
			if (this.levels >= NeverBackDown.requirement)
			{
				base.Grant();
			}
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x0015EFD8 File Offset: 0x0015D1D8
		private void CheckForFailure()
		{
			if (!TeleporterInteraction.instance || TeleporterInteraction.instance.isCharged)
			{
				this.checkingForFailure = false;
				return;
			}
			if (base.localUser.cachedBody && (this.teleporterStartChargingTime + NeverBackDown.gracePeriod).hasPassed && !TeleporterInteraction.instance.holdoutZoneController.IsBodyInChargingRadius(base.localUser.cachedBody))
			{
				this.hasLeftRadius = true;
				this.levels = 0;
				this.checkingForFailure = false;
			}
		}

		// Token: 0x0400506D RID: 20589
		private bool _checkingForFailure;

		// Token: 0x0400506E RID: 20590
		private static readonly int requirement = 4;

		// Token: 0x0400506F RID: 20591
		private bool hasLeftRadius;

		// Token: 0x04005070 RID: 20592
		private int levels;

		// Token: 0x04005071 RID: 20593
		private Run.FixedTimeStamp teleporterStartChargingTime = Run.FixedTimeStamp.negativeInfinity;

		// Token: 0x04005072 RID: 20594
		private static readonly float gracePeriod = 2f;
	}
}
