using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F9 RID: 249
	public class CrushBase : BaseState
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00012B38 File Offset: 0x00010D38
		public override void OnEnter()
		{
			base.OnEnter();
			base.GetAimRay();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.StartAimMode(this.duration + 2f, false);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.AddRecoil(-1f * this.recoilAmplitude, -1.5f * this.recoilAmplitude, -0.25f * this.recoilAmplitude, 0.25f * this.recoilAmplitude);
			base.characterBody.AddSpreadBloom(this.bloom);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (NetworkServer.active)
			{
				ProcChainMask procChainMask = default(ProcChainMask);
				procChainMask.AddProc(ProcType.VoidSurvivorCrush);
				if (this.selfHealFraction > 0f)
				{
					base.healthComponent.HealFraction(this.selfHealFraction, procChainMask);
				}
				else
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = -base.healthComponent.fullCombinedHealth * this.selfHealFraction;
					damageInfo.position = base.characterBody.corePosition;
					damageInfo.force = Vector3.zero;
					damageInfo.damageColorIndex = DamageColorIndex.Default;
					damageInfo.crit = false;
					damageInfo.attacker = null;
					damageInfo.inflictor = null;
					damageInfo.damageType = (DamageType.NonLethal | DamageType.BypassArmor);
					damageInfo.procCoefficient = 0f;
					damageInfo.procChainMask = procChainMask;
					base.healthComponent.TakeDamage(damageInfo);
				}
				VoidSurvivorController component = base.GetComponent<VoidSurvivorController>();
				if (component)
				{
					component.AddCorruption(this.corruptionChange);
				}
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00012CE8 File Offset: 0x00010EE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040004C4 RID: 1220
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x040004C5 RID: 1221
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040004C6 RID: 1222
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040004C7 RID: 1223
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x040004C8 RID: 1224
		[SerializeField]
		public float bloom;

		// Token: 0x040004C9 RID: 1225
		[SerializeField]
		public string muzzle;

		// Token: 0x040004CA RID: 1226
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040004CB RID: 1227
		[SerializeField]
		public string animationStateName;

		// Token: 0x040004CC RID: 1228
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040004CD RID: 1229
		[SerializeField]
		public float selfHealFraction;

		// Token: 0x040004CE RID: 1230
		[SerializeField]
		public float corruptionChange;

		// Token: 0x040004CF RID: 1231
		private float duration;
	}
}
