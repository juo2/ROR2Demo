using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x0200049B RID: 1179
	public class Enrage : BaseState
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x0005DD04 File Offset: 0x0005BF04
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Enrage.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayCrossfade("Gesture", "Enrage", "Enrage.playbackRate", this.duration, 0.2f);
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0005DD64 File Offset: 0x0005BF64
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("Enrage.activate") > 0.5f && !this.hasCastBuff)
			{
				EffectData effectData = new EffectData();
				effectData.origin = base.transform.position;
				effectData.SetNetworkedObjectReference(base.gameObject);
				EffectManager.SpawnEffect(Enrage.enragePrefab, effectData, true);
				this.hasCastBuff = true;
				base.characterBody.AddBuff(JunkContent.Buffs.EnrageAncientWisp);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0005DE14 File Offset: 0x0005C014
		private static void PullEnemies(Vector3 position, Vector3 direction, float coneAngle, float maxDistance, float force, TeamIndex excludedTeam)
		{
			float num = Mathf.Cos(coneAngle * 0.5f * 0.017453292f);
			foreach (Collider collider in Physics.OverlapSphere(position, maxDistance))
			{
				Vector3 position2 = collider.transform.position;
				Vector3 normalized = (position - position2).normalized;
				if (Vector3.Dot(-normalized, direction) >= num)
				{
					TeamComponent component = collider.GetComponent<TeamComponent>();
					if (component)
					{
						TeamIndex teamIndex = component.teamIndex;
						if (teamIndex != excludedTeam)
						{
							CharacterMotor component2 = collider.GetComponent<CharacterMotor>();
							if (component2)
							{
								component2.ApplyForce(normalized * force, false, false);
							}
							Rigidbody component3 = collider.GetComponent<Rigidbody>();
							if (component3)
							{
								component3.AddForce(normalized * force, ForceMode.Impulse);
							}
						}
					}
				}
			}
		}

		// Token: 0x04001AF7 RID: 6903
		public static float baseDuration = 3.5f;

		// Token: 0x04001AF8 RID: 6904
		public static GameObject enragePrefab;

		// Token: 0x04001AF9 RID: 6905
		private Animator modelAnimator;

		// Token: 0x04001AFA RID: 6906
		private float duration;

		// Token: 0x04001AFB RID: 6907
		private bool hasCastBuff;
	}
}
