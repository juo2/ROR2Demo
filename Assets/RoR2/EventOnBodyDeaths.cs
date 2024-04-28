using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020006CF RID: 1743
	public class EventOnBodyDeaths : MonoBehaviour
	{
		// Token: 0x06002257 RID: 8791 RVA: 0x000944BE File Offset: 0x000926BE
		private void OnEnable()
		{
			GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000944D1 File Offset: 0x000926D1
		private void OnDisable()
		{
			GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000944E4 File Offset: 0x000926E4
		private void OnCharacterDeath(DamageReport damageReport)
		{
			if (damageReport.victimBody)
			{
				for (int i = 0; i < this.bodyNames.Length; i++)
				{
					if (damageReport.victimBody.name.Contains(this.bodyNames[i]))
					{
						this.currentDeathCount++;
						break;
					}
				}
			}
			if (this.currentDeathCount >= this.targetDeathCount)
			{
				UnityEvent unityEvent = this.onAchieved;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x04002759 RID: 10073
		public string[] bodyNames;

		// Token: 0x0400275A RID: 10074
		private int currentDeathCount;

		// Token: 0x0400275B RID: 10075
		public int targetDeathCount;

		// Token: 0x0400275C RID: 10076
		public UnityEvent onAchieved;
	}
}
