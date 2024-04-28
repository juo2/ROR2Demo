using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D0D RID: 3341
	public class HUDBossHealthBarController : MonoBehaviour
	{
		// Token: 0x06004C29 RID: 19497 RVA: 0x00139ED4 File Offset: 0x001380D4
		private void FixedUpdate()
		{
			List<BossGroup> instancesList = InstanceTracker.GetInstancesList<BossGroup>();
			int num = 0;
			for (int i = 0; i < instancesList.Count; i++)
			{
				if (instancesList[i].shouldDisplayHealthBarOnHud)
				{
					num++;
				}
			}
			this.SetListeningForClientDamageNotified(num > 1);
			if (this.currentBossGroup && !this.currentBossGroup.shouldDisplayHealthBarOnHud)
			{
				this.currentBossGroup = null;
			}
			if (num > 0)
			{
				if (num == 1 || !this.currentBossGroup)
				{
					for (int j = 0; j < instancesList.Count; j++)
					{
						if (instancesList[j].shouldDisplayHealthBarOnHud)
						{
							this.currentBossGroup = instancesList[j];
							return;
						}
					}
					return;
				}
			}
			else
			{
				this.currentBossGroup = null;
			}
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00139F81 File Offset: 0x00138181
		private void OnDisable()
		{
			this.SetListeningForClientDamageNotified(false);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x00139F8C File Offset: 0x0013818C
		private void OnClientDamageNotified(DamageDealtMessage damageDealtMessage)
		{
			if (!this.nextAllowedSourceUpdateTime.hasPassed)
			{
				return;
			}
			if (!damageDealtMessage.victim)
			{
				return;
			}
			CharacterBody component = damageDealtMessage.victim.GetComponent<CharacterBody>();
			if (!component)
			{
				return;
			}
			if (component.isBoss && damageDealtMessage.attacker == this.hud.targetBodyObject)
			{
				BossGroup bossGroup = BossGroup.FindBossGroup(component);
				if (bossGroup && bossGroup.shouldDisplayHealthBarOnHud)
				{
					this.currentBossGroup = bossGroup;
					this.nextAllowedSourceUpdateTime = Run.TimeStamp.now + 1f;
				}
			}
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x0013A018 File Offset: 0x00138218
		private void SetListeningForClientDamageNotified(bool newListeningForClientDamageNotified)
		{
			if (newListeningForClientDamageNotified == this.listeningForClientDamageNotified)
			{
				return;
			}
			this.listeningForClientDamageNotified = newListeningForClientDamageNotified;
			if (this.listeningForClientDamageNotified)
			{
				GlobalEventManager.onClientDamageNotified += this.OnClientDamageNotified;
				return;
			}
			GlobalEventManager.onClientDamageNotified -= this.OnClientDamageNotified;
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0013A058 File Offset: 0x00138258
		private void LateUpdate()
		{
			bool flag = this.currentBossGroup && this.currentBossGroup.combatSquad.memberCount > 0;
			this.container.SetActive(flag);
			if (flag)
			{
				float totalObservedHealth = this.currentBossGroup.totalObservedHealth;
				float totalMaxObservedMaxHealth = this.currentBossGroup.totalMaxObservedMaxHealth;
				float num = (totalMaxObservedMaxHealth == 0f) ? 0f : Mathf.Clamp01(totalObservedHealth / totalMaxObservedMaxHealth);
				this.delayedTotalHealthFraction = Mathf.Clamp(Mathf.SmoothDamp(this.delayedTotalHealthFraction, num, ref this.healthFractionVelocity, 0.1f, float.PositiveInfinity, Time.deltaTime), num, 1f);
				this.fillRectImage.fillAmount = num;
				this.delayRectImage.fillAmount = this.delayedTotalHealthFraction;
				HUDBossHealthBarController.sharedStringBuilder.Clear().AppendInt(Mathf.CeilToInt(totalObservedHealth), 1U, uint.MaxValue).Append("/").AppendInt(Mathf.CeilToInt(totalMaxObservedMaxHealth), 1U, uint.MaxValue);
				this.healthLabel.SetText(HUDBossHealthBarController.sharedStringBuilder);
				this.bossNameLabel.SetText(this.currentBossGroup.bestObservedName, true);
				this.bossSubtitleLabel.SetText(this.currentBossGroup.bestObservedSubtitle, true);
			}
		}

		// Token: 0x0400490D RID: 18701
		public HUD hud;

		// Token: 0x0400490E RID: 18702
		public GameObject container;

		// Token: 0x0400490F RID: 18703
		public Image fillRectImage;

		// Token: 0x04004910 RID: 18704
		public Image delayRectImage;

		// Token: 0x04004911 RID: 18705
		public TextMeshProUGUI healthLabel;

		// Token: 0x04004912 RID: 18706
		public TextMeshProUGUI bossNameLabel;

		// Token: 0x04004913 RID: 18707
		public TextMeshProUGUI bossSubtitleLabel;

		// Token: 0x04004914 RID: 18708
		private BossGroup currentBossGroup;

		// Token: 0x04004915 RID: 18709
		private float delayedTotalHealthFraction;

		// Token: 0x04004916 RID: 18710
		private float healthFractionVelocity;

		// Token: 0x04004917 RID: 18711
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x04004918 RID: 18712
		private Run.TimeStamp nextAllowedSourceUpdateTime = Run.TimeStamp.negativeInfinity;

		// Token: 0x04004919 RID: 18713
		private bool listeningForClientDamageNotified;
	}
}
