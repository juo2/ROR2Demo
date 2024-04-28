using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000180 RID: 384
	public class ChargeSonicBoom : BaseState
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x0001CE2C File Offset: 0x0001B02C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeSonicBoom.baseDuration / this.attackSpeedStat;
			Util.PlaySound(this.sound, base.gameObject);
			base.characterBody.SetAimTimer(3f);
			if (this.chargeEffectPrefab)
			{
				Transform transform = base.FindModelChild(ChargeSonicBoom.muzzleName);
				if (transform)
				{
					this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform);
					this.chargeEffect.transform.localPosition = Vector3.zero;
					this.chargeEffect.transform.localRotation = Quaternion.identity;
				}
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001CED0 File Offset: 0x0001B0D0
		public override void OnExit()
		{
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
				this.chargeEffect = null;
			}
			base.OnExit();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001CEF7 File Offset: 0x0001B0F7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.GetNextState());
				return;
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001CF1F File Offset: 0x0001B11F
		protected virtual EntityState GetNextState()
		{
			return new FireSonicBoom();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000839 RID: 2105
		[SerializeField]
		public string sound;

		// Token: 0x0400083A RID: 2106
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400083B RID: 2107
		public static string muzzleName;

		// Token: 0x0400083C RID: 2108
		public static float baseDuration;

		// Token: 0x0400083D RID: 2109
		private float duration;

		// Token: 0x0400083E RID: 2110
		private GameObject chargeEffect;
	}
}
