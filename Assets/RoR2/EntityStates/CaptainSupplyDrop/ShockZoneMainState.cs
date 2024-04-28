using System;
using RoR2;
using UnityEngine;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000418 RID: 1048
	public class ShockZoneMainState : BaseMainState
	{
		// Token: 0x060012D9 RID: 4825 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override Interactability GetInteractability(Interactor activator)
		{
			return Interactability.Disabled;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0005408A File Offset: 0x0005228A
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00054094 File Offset: 0x00052294
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.shockTimer += Time.fixedDeltaTime;
			if (this.shockTimer > 1f / ShockZoneMainState.shockFrequency)
			{
				this.shockTimer -= 1f / ShockZoneMainState.shockFrequency;
				this.Shock();
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000540EC File Offset: 0x000522EC
		private void Shock()
		{
			new BlastAttack
			{
				radius = ShockZoneMainState.shockRadius,
				baseDamage = 0f,
				damageType = (DamageType.Silent | DamageType.Shock5s),
				falloffModel = BlastAttack.FalloffModel.None,
				attacker = null,
				teamIndex = this.teamFilter.teamIndex,
				position = base.transform.position
			}.Fire();
			if (ShockZoneMainState.shockEffectPrefab)
			{
				EffectManager.SpawnEffect(ShockZoneMainState.shockEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = ShockZoneMainState.shockRadius
				}, false);
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0005418D File Offset: 0x0005238D
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0400183B RID: 6203
		public static GameObject shockEffectPrefab;

		// Token: 0x0400183C RID: 6204
		public static float shockRadius;

		// Token: 0x0400183D RID: 6205
		public static float shockDamageCoefficient;

		// Token: 0x0400183E RID: 6206
		public static float shockFrequency;

		// Token: 0x0400183F RID: 6207
		private float shockTimer;
	}
}
