using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BA2 RID: 2978
	public class ProjectileInflictTimedBuff : MonoBehaviour, IOnDamageInflictedServerReceiver
	{
		// Token: 0x060043B2 RID: 17330 RVA: 0x00119688 File Offset: 0x00117888
		public void OnDamageInflictedServer(DamageReport damageReport)
		{
			CharacterBody victimBody = damageReport.victimBody;
			if (victimBody)
			{
				victimBody.AddTimedBuff(this.buffDef.buffIndex, this.duration);
			}
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x001196BB File Offset: 0x001178BB
		private void OnValidate()
		{
			if (!this.buffDef)
			{
				Debug.LogWarningFormat(this, "ProjectileInflictTimedBuff {0} has no buff specified.", new object[]
				{
					this
				});
			}
		}

		// Token: 0x04004217 RID: 16919
		public BuffDef buffDef;

		// Token: 0x04004218 RID: 16920
		public float duration;
	}
}
