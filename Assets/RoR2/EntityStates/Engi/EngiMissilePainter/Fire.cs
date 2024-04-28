using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.EngiMissilePainter
{
	// Token: 0x020003B7 RID: 951
	public class Fire : BaseEngiMissilePainterState
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x0004ACBE File Offset: 0x00048EBE
		public override void OnEnter()
		{
			base.OnEnter();
			this.durationPerMissile = Fire.baseDurationPerMissile / this.attackSpeedStat;
			this.PlayAnimation("Gesture, Additive", "IdleHarpoons");
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0004ACE8 File Offset: 0x00048EE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			bool flag = false;
			if (base.isAuthority)
			{
				this.stopwatch += Time.fixedDeltaTime;
				if (this.stopwatch >= this.durationPerMissile)
				{
					this.stopwatch -= this.durationPerMissile;
					while (this.fireIndex < this.targetsList.Count)
					{
						List<HurtBox> list = this.targetsList;
						int num = this.fireIndex;
						this.fireIndex = num + 1;
						HurtBox hurtBox = list[num];
						if (hurtBox.healthComponent && hurtBox.healthComponent.alive)
						{
							string text = (this.fireIndex % 2 == 0) ? "MuzzleLeft" : "MuzzleRight";
							Vector3 position = base.inputBank.aimOrigin;
							Transform transform = base.FindModelChild(text);
							if (transform != null)
							{
								position = transform.position;
							}
							EffectManager.SimpleMuzzleFlash(Fire.muzzleflashEffectPrefab, base.gameObject, text, true);
							this.FireMissile(hurtBox, position);
							flag = true;
							break;
						}
						base.activatorSkillSlot.AddOneStock();
					}
					if (this.fireIndex >= this.targetsList.Count)
					{
						this.outer.SetNextState(new Finish());
					}
				}
			}
			if (flag)
			{
				this.PlayAnimation((this.fireIndex % 2 == 0) ? "Gesture Left Cannon, Additive" : "Gesture Right Cannon, Additive", "FireHarpoon");
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0004AE44 File Offset: 0x00049044
		private void FireMissile(HurtBox target, Vector3 position)
		{
			MissileUtils.FireMissile(base.inputBank.aimOrigin, base.characterBody, default(ProcChainMask), target.gameObject, this.damageStat * Fire.damageCoefficient, base.RollCrit(), Fire.projectilePrefab, DamageColorIndex.Default, Vector3.up, 0f, false);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0004AE99 File Offset: 0x00049099
		public override void OnExit()
		{
			base.OnExit();
			base.PlayCrossfade("Gesture, Additive", "ExitHarpoons", 0.1f);
		}

		// Token: 0x0400157D RID: 5501
		public static float baseDurationPerMissile;

		// Token: 0x0400157E RID: 5502
		public static float damageCoefficient;

		// Token: 0x0400157F RID: 5503
		public static GameObject projectilePrefab;

		// Token: 0x04001580 RID: 5504
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04001581 RID: 5505
		public List<HurtBox> targetsList;

		// Token: 0x04001582 RID: 5506
		private int fireIndex;

		// Token: 0x04001583 RID: 5507
		private float durationPerMissile;

		// Token: 0x04001584 RID: 5508
		private float stopwatch;
	}
}
