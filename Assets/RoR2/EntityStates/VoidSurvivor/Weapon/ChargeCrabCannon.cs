using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F6 RID: 246
	public class ChargeCrabCannon : BaseSkillState
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0001253C File Offset: 0x0001073C
		public override void OnEnter()
		{
			base.OnEnter();
			this.voidSurvivorController = base.GetComponent<VoidSurvivorController>();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			this.durationPerGrenade = this.baseDurationPerGrenade / this.attackSpeedStat;
			Util.PlaySound(this.chargeLoopStartSoundString, base.gameObject);
			this.AddGrenade();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(this.muzzle);
					if (transform && this.chargeEffectPrefab)
					{
						this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeEffectInstance.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.durationPerGrenade;
						}
					}
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012626 File Offset: 0x00010826
		public override void OnExit()
		{
			base.OnExit();
			Util.PlaySound(this.chargeLoopStopSoundString, base.gameObject);
			this.PlayAnimation(this.animationLayerName, "BufferEmpty");
			EntityState.Destroy(this.chargeEffectInstance);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001265C File Offset: 0x0001085C
		private void AddGrenade()
		{
			this.grenadeCount++;
			if (this.voidSurvivorController && NetworkServer.active)
			{
				this.voidSurvivorController.AddCorruption(this.corruptionPerGrenade);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012694 File Offset: 0x00010894
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterBody.SetAimTimer(3f);
			this.nextGrenadeStopwatch += Time.fixedDeltaTime;
			if (this.nextGrenadeStopwatch > this.durationPerGrenade && base.activatorSkillSlot.stock > 0)
			{
				this.AddGrenade();
				this.nextGrenadeStopwatch -= this.durationPerGrenade;
				base.activatorSkillSlot.DeductStock(1);
			}
			float value = this.bloomPerGrenade * (float)this.lastGrenadeCount;
			base.characterBody.SetSpreadBloom(value, true);
			if (this.lastGrenadeCount < this.grenadeCount)
			{
				Util.PlaySound(this.chargeStockSoundString, base.gameObject);
			}
			if (!base.IsKeyDownAuthority() && base.fixedAge > this.minimumDuration / this.attackSpeedStat && base.isAuthority)
			{
				FireCrabCannon fireCrabCannon = new FireCrabCannon();
				fireCrabCannon.grenadeCountMax = this.grenadeCount;
				this.outer.SetNextState(fireCrabCannon);
				return;
			}
			this.lastGrenadeCount = this.grenadeCount;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000497 RID: 1175
		[SerializeField]
		public float baseDurationPerGrenade;

		// Token: 0x04000498 RID: 1176
		[SerializeField]
		public float minimumDuration;

		// Token: 0x04000499 RID: 1177
		[SerializeField]
		public string muzzle;

		// Token: 0x0400049A RID: 1178
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400049B RID: 1179
		[SerializeField]
		public string chargeStockSoundString;

		// Token: 0x0400049C RID: 1180
		[SerializeField]
		public string chargeLoopStartSoundString;

		// Token: 0x0400049D RID: 1181
		[SerializeField]
		public string chargeLoopStopSoundString;

		// Token: 0x0400049E RID: 1182
		[SerializeField]
		public float bloomPerGrenade;

		// Token: 0x0400049F RID: 1183
		[SerializeField]
		public float corruptionPerGrenade;

		// Token: 0x040004A0 RID: 1184
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040004A1 RID: 1185
		[SerializeField]
		public string animationStateName;

		// Token: 0x040004A2 RID: 1186
		private VoidSurvivorController voidSurvivorController;

		// Token: 0x040004A3 RID: 1187
		private GameObject chargeEffectInstance;

		// Token: 0x040004A4 RID: 1188
		private int grenadeCount;

		// Token: 0x040004A5 RID: 1189
		private int lastGrenadeCount;

		// Token: 0x040004A6 RID: 1190
		private float durationPerGrenade;

		// Token: 0x040004A7 RID: 1191
		private float nextGrenadeStopwatch;
	}
}
