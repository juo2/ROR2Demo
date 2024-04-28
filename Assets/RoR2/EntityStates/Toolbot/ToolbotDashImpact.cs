using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x0200019E RID: 414
	public class ToolbotDashImpact : BaseState
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				if (this.victimHealthComponent)
				{
					DamageInfo damageInfo = new DamageInfo
					{
						attacker = base.gameObject,
						damage = this.damageStat * ToolbotDash.knockbackDamageCoefficient * this.damageBoostFromSpeed,
						crit = this.isCrit,
						procCoefficient = 1f,
						damageColorIndex = DamageColorIndex.Item,
						damageType = DamageType.Stun1s,
						position = base.characterBody.corePosition
					};
					this.victimHealthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, this.victimHealthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, this.victimHealthComponent.gameObject);
				}
				base.healthComponent.TakeDamageForce(this.idealDirection * -ToolbotDash.knockbackForce, true, false);
			}
			if (base.isAuthority)
			{
				base.AddRecoil(-0.5f * ToolbotDash.recoilAmplitude * 3f, -0.5f * ToolbotDash.recoilAmplitude * 3f, -0.5f * ToolbotDash.recoilAmplitude * 8f, 0.5f * ToolbotDash.recoilAmplitude * 3f);
				EffectManager.SimpleImpactEffect(ToolbotDash.knockbackEffectPrefab, base.characterBody.corePosition, base.characterDirection.forward, true);
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001FF38 File Offset: 0x0001E138
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.victimHealthComponent ? this.victimHealthComponent.gameObject : null);
			writer.Write(this.idealDirection);
			writer.Write(this.damageBoostFromSpeed);
			writer.Write(this.isCrit);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001FF94 File Offset: 0x0001E194
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			GameObject gameObject = reader.ReadGameObject();
			this.victimHealthComponent = (gameObject ? gameObject.GetComponent<HealthComponent>() : null);
			this.idealDirection = reader.ReadVector3();
			this.damageBoostFromSpeed = reader.ReadSingle();
			this.isCrit = reader.ReadBoolean();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04000906 RID: 2310
		public HealthComponent victimHealthComponent;

		// Token: 0x04000907 RID: 2311
		public Vector3 idealDirection;

		// Token: 0x04000908 RID: 2312
		public float damageBoostFromSpeed;

		// Token: 0x04000909 RID: 2313
		public bool isCrit;
	}
}
