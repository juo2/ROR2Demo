using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200057F RID: 1407
	public class DamageReport
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x0006CF50 File Offset: 0x0006B150
		public DamageReport(DamageInfo damageInfo, HealthComponent victim, float damageDealt, float combinedHealthBeforeDamage)
		{
			this.damageInfo = damageInfo;
			this.victim = victim;
			this.victimBody = (victim ? victim.body : null);
			if (this.victimBody)
			{
				this.victimBodyIndex = this.victimBody.bodyIndex;
				this.victimMaster = this.victimBody.master;
				this.victimIsElite = this.victimBody.isElite;
				this.victimIsBoss = this.victimBody.isBoss;
				this.victimIsChampion = this.victimBody.isChampion;
				if (this.victimBody.teamComponent)
				{
					this.victimTeamIndex = this.victimBody.teamComponent.teamIndex;
				}
				else
				{
					this.victimTeamIndex = TeamIndex.None;
				}
			}
			else
			{
				this.victimBodyIndex = BodyIndex.None;
				this.victimTeamIndex = TeamIndex.None;
				this.victimIsElite = false;
				this.victimIsBoss = false;
				this.victimIsChampion = false;
			}
			this.attacker = damageInfo.attacker;
			this.attackerBody = (this.attacker ? this.attacker.GetComponent<CharacterBody>() : null);
			if (this.attackerBody)
			{
				this.attackerBodyIndex = this.attackerBody.bodyIndex;
				if (this.attackerBody.teamComponent)
				{
					this.attackerTeamIndex = this.attackerBody.teamComponent.teamIndex;
				}
				else
				{
					this.attackerTeamIndex = TeamIndex.None;
				}
				this.attackerOwnerBodyIndex = BodyIndex.None;
				this.attackerMaster = this.attackerBody.master;
				if (this.attackerMaster && this.attackerMaster.minionOwnership)
				{
					this.attackerOwnerMaster = this.attackerMaster.minionOwnership.ownerMaster;
					if (this.attackerOwnerMaster)
					{
						CharacterBody body = this.attackerOwnerMaster.GetBody();
						if (body)
						{
							this.attackerOwnerBodyIndex = body.bodyIndex;
						}
					}
				}
			}
			else
			{
				this.attackerBodyIndex = BodyIndex.None;
				this.attackerTeamIndex = TeamIndex.None;
				this.attackerOwnerBodyIndex = BodyIndex.None;
			}
			if (victim)
			{
				this.hitLowHealth = victim.isHealthLow;
			}
			else
			{
				this.hitLowHealth = false;
			}
			if (damageInfo != null)
			{
				this.dotType = damageInfo.dotIndex;
			}
			this.damageDealt = damageDealt;
			this.combinedHealthBeforeDamage = combinedHealthBeforeDamage;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x0006D18E File Offset: 0x0006B38E
		public bool isFriendlyFire
		{
			get
			{
				return this.attackerTeamIndex == this.victimTeamIndex && this.attackerTeamIndex != TeamIndex.None;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x0006D1AC File Offset: 0x0006B3AC
		public bool isFallDamage
		{
			get
			{
				return (this.damageInfo.damageType & DamageType.FallDamage) > DamageType.Generic;
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0006D1C4 File Offset: 0x0006B3C4
		public StringBuilder AppendToStringBuilderMultiline(StringBuilder stringBuilder)
		{
			stringBuilder.Append("VictimBody=").AppendLine(DamageReport.<AppendToStringBuilderMultiline>g__ObjToString|25_0(this.victimBody));
			stringBuilder.Append("VictimTeamIndex=").AppendLine(this.victimTeamIndex.ToString());
			stringBuilder.Append("VictimMaster=").AppendLine(DamageReport.<AppendToStringBuilderMultiline>g__ObjToString|25_0(this.victimMaster));
			stringBuilder.Append("AttackerBody=").AppendLine(DamageReport.<AppendToStringBuilderMultiline>g__ObjToString|25_0(this.attackerBody));
			stringBuilder.Append("AttackerTeamIndex=").AppendLine(this.attackerTeamIndex.ToString());
			stringBuilder.Append("AttackerMaster=").AppendLine(DamageReport.<AppendToStringBuilderMultiline>g__ObjToString|25_0(this.attackerMaster));
			stringBuilder.Append("Inflictor=").AppendLine(DamageReport.<AppendToStringBuilderMultiline>g__ObjToString|25_0(this.damageInfo.inflictor));
			stringBuilder.Append("Damage=").AppendLine(this.damageInfo.damage.ToString());
			stringBuilder.Append("Crit=").AppendLine(this.damageInfo.crit.ToString());
			stringBuilder.Append("ProcChainMask=").AppendLine(this.damageInfo.procChainMask.ToString());
			stringBuilder.Append("ProcCoefficient=").AppendLine(this.damageInfo.procCoefficient.ToString());
			stringBuilder.Append("DamageType=").AppendLine(this.damageInfo.damageType.ToString());
			stringBuilder.Append("DotIndex=").AppendLine(this.damageInfo.dotIndex.ToString());
			stringBuilder.Append("DamageColorIndex=").AppendLine(this.damageInfo.damageColorIndex.ToString());
			stringBuilder.Append("Position=").AppendLine(this.damageInfo.position.ToString());
			stringBuilder.Append("Force=").AppendLine(this.damageInfo.force.ToString());
			stringBuilder.Append("Rejected=").AppendLine(this.damageInfo.rejected.ToString());
			return stringBuilder;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0006D415 File Offset: 0x0006B615
		public override string ToString()
		{
			return this.AppendToStringBuilderMultiline(new StringBuilder()).ToString();
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0006D427 File Offset: 0x0006B627
		[CompilerGenerated]
		internal static string <AppendToStringBuilderMultiline>g__ObjToString|25_0(object obj)
		{
			if (obj == null)
			{
				return "null";
			}
			return obj.ToString();
		}

		// Token: 0x04001F94 RID: 8084
		public readonly HealthComponent victim;

		// Token: 0x04001F95 RID: 8085
		public readonly CharacterBody victimBody;

		// Token: 0x04001F96 RID: 8086
		public readonly BodyIndex victimBodyIndex;

		// Token: 0x04001F97 RID: 8087
		public readonly TeamIndex victimTeamIndex;

		// Token: 0x04001F98 RID: 8088
		public readonly CharacterMaster victimMaster;

		// Token: 0x04001F99 RID: 8089
		public readonly bool victimIsElite;

		// Token: 0x04001F9A RID: 8090
		public readonly bool victimIsBoss;

		// Token: 0x04001F9B RID: 8091
		public readonly bool victimIsChampion;

		// Token: 0x04001F9C RID: 8092
		public readonly DamageInfo damageInfo;

		// Token: 0x04001F9D RID: 8093
		public readonly GameObject attacker;

		// Token: 0x04001F9E RID: 8094
		public readonly CharacterBody attackerBody;

		// Token: 0x04001F9F RID: 8095
		public readonly BodyIndex attackerBodyIndex;

		// Token: 0x04001FA0 RID: 8096
		public readonly TeamIndex attackerTeamIndex;

		// Token: 0x04001FA1 RID: 8097
		public readonly CharacterMaster attackerMaster;

		// Token: 0x04001FA2 RID: 8098
		public readonly CharacterMaster attackerOwnerMaster;

		// Token: 0x04001FA3 RID: 8099
		public readonly BodyIndex attackerOwnerBodyIndex;

		// Token: 0x04001FA4 RID: 8100
		public readonly DotController.DotIndex dotType;

		// Token: 0x04001FA5 RID: 8101
		public readonly float damageDealt;

		// Token: 0x04001FA6 RID: 8102
		public readonly float combinedHealthBeforeDamage;

		// Token: 0x04001FA7 RID: 8103
		public readonly bool hitLowHealth;
	}
}
