using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CA8 RID: 3240
	public class DebugStats : MonoBehaviour
	{
		// Token: 0x060049DE RID: 18910 RVA: 0x0012F4C6 File Offset: 0x0012D6C6
		private void Awake()
		{
			DebugStats.fpsTimestamps = new Queue<float>();
			this.fpsTimestampCount = (int)(this.fpsAverageTime / this.fpsAverageFrequency);
			DebugStats.budgetTimestamps = new Queue<float>();
			this.budgetTimestampCount = (int)(this.budgetAverageTime / this.budgetAverageFrequency);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0012F504 File Offset: 0x0012D704
		private float GetAverageFPS()
		{
			if (DebugStats.fpsTimestamps.Count == 0)
			{
				return 0f;
			}
			float num = 0f;
			foreach (float num2 in DebugStats.fpsTimestamps)
			{
				num += num2;
			}
			num /= (float)DebugStats.fpsTimestamps.Count;
			return Mathf.Round(num);
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x0012F580 File Offset: 0x0012D780
		private float GetAverageParticleCost()
		{
			if (DebugStats.budgetTimestamps.Count == 0)
			{
				return 0f;
			}
			float num = 0f;
			foreach (float num2 in DebugStats.budgetTimestamps)
			{
				num += num2;
			}
			num /= (float)DebugStats.budgetTimestamps.Count;
			return Mathf.Round(num);
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0012F5FC File Offset: 0x0012D7FC
		private void Update()
		{
			this.statsRoot.SetActive(DebugStats.showStats);
			if (!DebugStats.showStats)
			{
				return;
			}
			this.fpsStopwatch += Time.unscaledDeltaTime;
			this.fpsDisplayStopwatch += Time.unscaledDeltaTime;
			float num = 1f / Time.unscaledDeltaTime;
			if (this.fpsStopwatch >= 1f / this.fpsAverageFrequency)
			{
				this.fpsStopwatch = 0f;
				DebugStats.fpsTimestamps.Enqueue(num);
				if (DebugStats.fpsTimestamps.Count > this.fpsTimestampCount)
				{
					DebugStats.fpsTimestamps.Dequeue();
				}
			}
			if (this.fpsDisplayStopwatch > this.fpsAverageDisplayUpdateFrequency)
			{
				this.fpsDisplayStopwatch = 0f;
				float averageFPS = this.GetAverageFPS();
				this.fpsAverageText.text = string.Concat(new string[]
				{
					"FPS: ",
					Mathf.Round(num).ToString(),
					" (",
					averageFPS.ToString(),
					")"
				});
				TextMeshProUGUI textMeshProUGUI = this.fpsAverageText;
				textMeshProUGUI.text = string.Concat(new string[]
				{
					textMeshProUGUI.text,
					"\n ms: ",
					(Mathf.Round(100000f / num) / 100f).ToString(),
					"(",
					(Mathf.Round(100000f / averageFPS) / 100f).ToString(),
					")"
				});
			}
			this.budgetStopwatch += Time.unscaledDeltaTime;
			this.budgetDisplayStopwatch += Time.unscaledDeltaTime;
			float num2 = (float)VFXBudget.totalCost;
			if (this.budgetStopwatch >= 1f / this.budgetAverageFrequency)
			{
				this.budgetStopwatch = 0f;
				DebugStats.budgetTimestamps.Enqueue(num2);
				if (DebugStats.budgetTimestamps.Count > this.budgetTimestampCount)
				{
					DebugStats.budgetTimestamps.Dequeue();
				}
			}
			if (this.budgetDisplayStopwatch > 1f)
			{
				this.budgetDisplayStopwatch = 0f;
				float averageParticleCost = this.GetAverageParticleCost();
				this.budgetAverageText.text = string.Concat(new string[]
				{
					"Particle Cost: ",
					Mathf.Round(num2).ToString(),
					" (",
					averageParticleCost.ToString(),
					")"
				});
			}
			if (this.teamText)
			{
				string str = "Allies: " + TeamComponent.GetTeamMembers(TeamIndex.Player).Count + "\n";
				string str2 = "Enemies: " + TeamComponent.GetTeamMembers(TeamIndex.Monster).Count + "\n";
				string str3 = "Bosses: " + BossGroup.GetTotalBossCount() + "\n";
				string text = "Directors:";
				foreach (CombatDirector combatDirector in CombatDirector.instancesList)
				{
					string text2 = "\n==[" + combatDirector.gameObject.name + "]==";
					if (combatDirector.enabled)
					{
						string text3 = "\n - Credit: " + combatDirector.monsterCredit.ToString();
						string text4 = "\n - Target: " + (combatDirector.currentSpawnTarget ? combatDirector.currentSpawnTarget.name : "null");
						string text5 = "\n - Last Spawn Card: ";
						if (combatDirector.lastAttemptedMonsterCard != null && combatDirector.lastAttemptedMonsterCard.spawnCard)
						{
							text5 += combatDirector.lastAttemptedMonsterCard.spawnCard.name;
						}
						string text6 = "\n - Reroll Timer: " + Mathf.Round(combatDirector.monsterSpawnTimer);
						text2 = string.Concat(new string[]
						{
							text2,
							text3,
							text4,
							text5,
							text6
						});
					}
					else
					{
						text2 += " (Off)";
					}
					text = text + text2 + "\n \n";
				}
				this.teamText.text = str + str2 + str3 + text;
			}
		}

		// Token: 0x040046A0 RID: 18080
		public GameObject statsRoot;

		// Token: 0x040046A1 RID: 18081
		public TextMeshProUGUI fpsAverageText;

		// Token: 0x040046A2 RID: 18082
		public float fpsAverageFrequency = 1f;

		// Token: 0x040046A3 RID: 18083
		public float fpsAverageTime = 60f;

		// Token: 0x040046A4 RID: 18084
		public float fpsAverageDisplayUpdateFrequency = 1f;

		// Token: 0x040046A5 RID: 18085
		private float fpsStopwatch;

		// Token: 0x040046A6 RID: 18086
		private float fpsDisplayStopwatch;

		// Token: 0x040046A7 RID: 18087
		private static Queue<float> fpsTimestamps;

		// Token: 0x040046A8 RID: 18088
		private int fpsTimestampCount;

		// Token: 0x040046A9 RID: 18089
		public TextMeshProUGUI budgetAverageText;

		// Token: 0x040046AA RID: 18090
		public float budgetAverageFrequency = 1f;

		// Token: 0x040046AB RID: 18091
		public float budgetAverageTime = 60f;

		// Token: 0x040046AC RID: 18092
		private const float budgetAverageDisplayUpdateFrequency = 1f;

		// Token: 0x040046AD RID: 18093
		private float budgetStopwatch;

		// Token: 0x040046AE RID: 18094
		private float budgetDisplayStopwatch;

		// Token: 0x040046AF RID: 18095
		private static Queue<float> budgetTimestamps;

		// Token: 0x040046B0 RID: 18096
		private int budgetTimestampCount;

		// Token: 0x040046B1 RID: 18097
		private static bool showStats;

		// Token: 0x040046B2 RID: 18098
		public TextMeshProUGUI teamText;
	}
}
