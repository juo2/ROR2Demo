using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.TeleporterHealNovaController
{
	// Token: 0x020001B4 RID: 436
	public class TeleporterHealNovaGeneratorMain : BaseState
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x00021720 File Offset: 0x0001F920
		public override void OnEnter()
		{
			base.OnEnter();
			Transform parent = base.transform.parent;
			if (parent)
			{
				this.holdoutZone = parent.GetComponentInParent<HoldoutZoneController>();
				this.previousPulseFraction = this.GetCurrentTeleporterChargeFraction();
			}
			TeamFilter component = base.GetComponent<TeamFilter>();
			this.teamIndex = (component ? component.teamIndex : TeamIndex.None);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002177D File Offset: 0x0001F97D
		private float GetCurrentTeleporterChargeFraction()
		{
			return this.holdoutZone.charge;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002178C File Offset: 0x0001F98C
		public override void FixedUpdate()
		{
			if (NetworkServer.active && Time.fixedDeltaTime > 0f)
			{
				if (!this.holdoutZone || this.holdoutZone.charge >= 1f)
				{
					EntityState.Destroy(this.outer.gameObject);
					return;
				}
				if (this.secondsUntilPulseAvailable > 0f)
				{
					this.secondsUntilPulseAvailable -= Time.fixedDeltaTime;
					return;
				}
				this.pulseCount = TeleporterHealNovaGeneratorMain.CalculatePulseCount(this.teamIndex);
				float num = TeleporterHealNovaGeneratorMain.CalculateNextPulseFraction(this.pulseCount, this.previousPulseFraction);
				float currentTeleporterChargeFraction = this.GetCurrentTeleporterChargeFraction();
				if (num < currentTeleporterChargeFraction)
				{
					this.Pulse();
					this.previousPulseFraction = num;
					this.secondsUntilPulseAvailable = this.minSecondsBetweenPulses;
				}
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002184C File Offset: 0x0001FA4C
		private static int CalculatePulseCount(TeamIndex teamIndex)
		{
			int num = 0;
			ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CharacterMaster characterMaster = readOnlyInstancesList[i];
				if (characterMaster.teamIndex == teamIndex)
				{
					num += characterMaster.inventory.GetItemCount(RoR2Content.Items.TPHealingNova);
				}
			}
			return num;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00021898 File Offset: 0x0001FA98
		private static float CalculateNextPulseFraction(int pulseCount, float previousPulseFraction)
		{
			float num = 1f / (float)(pulseCount + 1);
			for (int i = 1; i <= pulseCount; i++)
			{
				float num2 = (float)i * num;
				if (num2 > previousPulseFraction)
				{
					return num2;
				}
			}
			return 1f;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000218CC File Offset: 0x0001FACC
		protected void Pulse()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(TeleporterHealNovaGeneratorMain.pulsePrefab, base.transform.position, base.transform.rotation, base.transform.parent);
			gameObject.GetComponent<TeamFilter>().teamIndex = this.teamIndex;
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x04000965 RID: 2405
		public static GameObject pulsePrefab;

		// Token: 0x04000966 RID: 2406
		[SerializeField]
		public float minSecondsBetweenPulses;

		// Token: 0x04000967 RID: 2407
		private HoldoutZoneController holdoutZone;

		// Token: 0x04000968 RID: 2408
		private TeamIndex teamIndex;

		// Token: 0x04000969 RID: 2409
		private float previousPulseFraction;

		// Token: 0x0400096A RID: 2410
		private int pulseCount;

		// Token: 0x0400096B RID: 2411
		private float secondsUntilPulseAvailable;
	}
}
