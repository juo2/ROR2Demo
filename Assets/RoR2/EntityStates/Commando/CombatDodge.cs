using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando
{
	// Token: 0x020003E4 RID: 996
	public class CombatDodge : DodgeState
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x0004EBBC File Offset: 0x0004CDBC
		public override void OnEnter()
		{
			base.OnEnter();
			this.search = new BullseyeSearch();
			this.search.searchDirection = Vector3.zero;
			this.search.teamMaskFilter = TeamMask.allButNeutral;
			this.search.teamMaskFilter.RemoveTeam(base.characterBody.teamComponent.teamIndex);
			this.search.filterByLoS = true;
			this.search.sortMode = BullseyeSearch.SortMode.Distance;
			this.search.maxDistanceFilter = CombatDodge.range;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004EC44 File Offset: 0x0004CE44
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			float num = base.fixedAge / CombatDodge.durationToFire;
			if (this.bulletsFired < CombatDodge.bulletCount && num > (float)this.bulletsFired / (float)CombatDodge.bulletCount)
			{
				if (this.bulletsFired % 2 == 0)
				{
					this.PlayAnimation("Gesture Additive, Left", "FirePistol, Left");
					this.FireBullet("MuzzleLeft");
					return;
				}
				this.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
				this.FireBullet("MuzzleRight");
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		private HurtBox PickNextTarget()
		{
			this.search.searchOrigin = base.GetAimRay().origin;
			this.search.RefreshCandidates();
			List<HurtBox> list = this.search.GetResults().ToList<HurtBox>();
			if (list.Count <= 0)
			{
				return null;
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004ED24 File Offset: 0x0004CF24
		private void FireBullet(string targetMuzzle)
		{
			this.bulletsFired++;
			base.AddRecoil(-0.4f * CombatDodge.recoilAmplitude, -0.8f * CombatDodge.recoilAmplitude, -0.3f * CombatDodge.recoilAmplitude, 0.3f * CombatDodge.recoilAmplitude);
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				aimRay.direction = UnityEngine.Random.onUnitSphere;
				HurtBox hurtBox = this.PickNextTarget();
				if (hurtBox)
				{
					aimRay.direction = hurtBox.transform.position - aimRay.origin;
				}
				Util.PlaySound(CombatDodge.firePistolSoundString, base.gameObject);
				if (CombatDodge.muzzleEffectPrefab)
				{
					EffectManager.SimpleMuzzleFlash(CombatDodge.muzzleEffectPrefab, base.gameObject, targetMuzzle, false);
				}
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					damage = CombatDodge.damageCoefficient * this.damageStat,
					force = CombatDodge.force,
					tracerEffectPrefab = CombatDodge.tracerEffectPrefab,
					muzzleName = targetMuzzle,
					hitEffectPrefab = CombatDodge.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					radius = 0.1f,
					smartCollision = true
				}.Fire();
			}
		}

		// Token: 0x040016A2 RID: 5794
		public static float durationToFire;

		// Token: 0x040016A3 RID: 5795
		public static int bulletCount;

		// Token: 0x040016A4 RID: 5796
		public static GameObject muzzleEffectPrefab;

		// Token: 0x040016A5 RID: 5797
		public static GameObject tracerEffectPrefab;

		// Token: 0x040016A6 RID: 5798
		public static GameObject hitEffectPrefab;

		// Token: 0x040016A7 RID: 5799
		public static float damageCoefficient;

		// Token: 0x040016A8 RID: 5800
		public static float force;

		// Token: 0x040016A9 RID: 5801
		public static string firePistolSoundString;

		// Token: 0x040016AA RID: 5802
		public static float recoilAmplitude = 1f;

		// Token: 0x040016AB RID: 5803
		public static float range;

		// Token: 0x040016AC RID: 5804
		private int bulletsFired;

		// Token: 0x040016AD RID: 5805
		private BullseyeSearch search;
	}
}
