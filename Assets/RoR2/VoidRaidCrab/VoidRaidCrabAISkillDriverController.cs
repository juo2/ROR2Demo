using System;
using EntityStates;
using RoR2.CharacterAI;
using UnityEngine;

namespace RoR2.VoidRaidCrab
{
	// Token: 0x02000B6E RID: 2926
	public class VoidRaidCrabAISkillDriverController : MonoBehaviour
	{
		// Token: 0x0600429F RID: 17055 RVA: 0x00113EA3 File Offset: 0x001120A3
		public bool CanUseWeaponSkills()
		{
			return this.bodyStateMachine.state is GenericCharacterMain && !this.isGauntletSkillAvailable && !this.ShouldUseFinalStandSkill();
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x00113ECA File Offset: 0x001120CA
		public bool CanUseBodySkills()
		{
			return this.bodyStateMachine.IsInMainState() && !this.isGauntletSkillAvailable && !this.ShouldUseFinalStandSkill();
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x00113EEC File Offset: 0x001120EC
		public bool ShouldUseGauntletSkills()
		{
			return this.isGauntletSkillAvailable && this.bodyStateMachine.IsInMainState() && !this.ShouldUseFinalStandSkill();
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x00113F0E File Offset: 0x0011210E
		public bool ShouldUseFinalStandSkill()
		{
			return this.CanUsePhaseSkillDriver(this.finalStandSkillDriver);
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x00113F1C File Offset: 0x0011211C
		private void FixedUpdate()
		{
			if (this.master && (!this.bodyStateMachine || !this.healthComponent))
			{
				GameObject bodyObject = this.master.GetBodyObject();
				if (bodyObject)
				{
					if (!this.bodyStateMachine)
					{
						this.bodyStateMachine = EntityStateMachine.FindByCustomName(bodyObject, this.bodyStateMachineName);
					}
					if (!this.healthComponent)
					{
						this.healthComponent = bodyObject.GetComponent<HealthComponent>();
					}
				}
			}
			if (this.bodyStateMachine)
			{
				this.isGauntletSkillAvailable = false;
				foreach (AISkillDriver driver in this.gauntletSkillDrivers)
				{
					this.isGauntletSkillAvailable = (this.isGauntletSkillAvailable || this.CanUsePhaseSkillDriver(driver));
				}
				if (this.debugForceSkillDriver)
				{
					this.SetSkillDriversEnabled(this.weaponSkillDrivers, false);
					this.SetSkillDriversEnabled(this.bodySkillDrivers, false);
					this.SetSkillDriversEnabled(this.gauntletSkillDrivers, false);
					if (this.finalStandSkillDriver)
					{
						this.finalStandSkillDriver.enabled = false;
					}
					bool flag = false;
					foreach (AISkillDriver aiskillDriver in this.weaponSkillDrivers)
					{
						if (aiskillDriver == this.debugForceSkillDriver)
						{
							aiskillDriver.enabled = this.CanUseWeaponSkills();
							flag = true;
						}
					}
					if (!flag)
					{
						foreach (AISkillDriver aiskillDriver2 in this.bodySkillDrivers)
						{
							if (aiskillDriver2 == this.debugForceSkillDriver)
							{
								aiskillDriver2.enabled = this.CanUseBodySkills();
								flag = true;
							}
						}
					}
					if (!flag)
					{
						foreach (AISkillDriver aiskillDriver3 in this.gauntletSkillDrivers)
						{
							if (aiskillDriver3 == this.debugForceSkillDriver)
							{
								aiskillDriver3.enabled = this.ShouldUseGauntletSkills();
								flag = true;
							}
						}
					}
					if (!flag && this.debugForceSkillDriver == this.finalStandSkillDriver)
					{
						this.debugForceSkillDriver.enabled = this.ShouldUseFinalStandSkill();
						flag = true;
					}
					if (!flag)
					{
						this.debugForceSkillDriver.enabled = true;
						return;
					}
				}
				else
				{
					this.SetSkillDriversEnabled(this.weaponSkillDrivers, this.CanUseWeaponSkills());
					this.SetSkillDriversEnabled(this.bodySkillDrivers, this.CanUseBodySkills());
					this.SetSkillDriversEnabled(this.gauntletSkillDrivers, this.ShouldUseGauntletSkills());
					if (this.finalStandSkillDriver)
					{
						this.finalStandSkillDriver.enabled = this.ShouldUseFinalStandSkill();
					}
				}
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x00114168 File Offset: 0x00112368
		private void SetSkillDriversEnabled(AISkillDriver[] skillDrivers, bool driverEnabled)
		{
			for (int i = 0; i < skillDrivers.Length; i++)
			{
				skillDrivers[i].enabled = driverEnabled;
			}
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x00114190 File Offset: 0x00112390
		private bool CanUsePhaseSkillDriver(AISkillDriver driver)
		{
			if (driver)
			{
				float num = 1f;
				if (this.healthComponent)
				{
					num = this.healthComponent.combinedHealthFraction;
				}
				return num < driver.maxUserHealthFraction && (driver.timesSelected < driver.maxTimesSelected || driver.maxTimesSelected < 0);
			}
			return false;
		}

		// Token: 0x04004096 RID: 16534
		[SerializeField]
		private AISkillDriver[] weaponSkillDrivers;

		// Token: 0x04004097 RID: 16535
		[SerializeField]
		private AISkillDriver[] bodySkillDrivers;

		// Token: 0x04004098 RID: 16536
		[SerializeField]
		private AISkillDriver[] gauntletSkillDrivers;

		// Token: 0x04004099 RID: 16537
		[SerializeField]
		private AISkillDriver finalStandSkillDriver;

		// Token: 0x0400409A RID: 16538
		[SerializeField]
		private CharacterMaster master;

		// Token: 0x0400409B RID: 16539
		[SerializeField]
		private string bodyStateMachineName = "Body";

		// Token: 0x0400409C RID: 16540
		[SerializeField]
		private AISkillDriver debugForceSkillDriver;

		// Token: 0x0400409D RID: 16541
		private EntityStateMachine bodyStateMachine;

		// Token: 0x0400409E RID: 16542
		private HealthComponent healthComponent;

		// Token: 0x0400409F RID: 16543
		private bool isGauntletSkillAvailable;
	}
}
