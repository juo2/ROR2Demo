using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Drone
{
	// Token: 0x020003C3 RID: 963
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x0600112E RID: 4398 RVA: 0x0004B9DC File Offset: 0x00049BDC
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.initialSoundString, base.gameObject);
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.forcePID.enabled = false;
				base.rigidbodyMotor.rigid.useGravity = true;
				base.rigidbodyMotor.rigid.AddForce(Vector3.up * this.forceAmount, ForceMode.Force);
				base.rigidbodyMotor.rigid.collisionDetectionMode = CollisionDetectionMode.Continuous;
			}
			if (base.rigidbodyDirection)
			{
				base.rigidbodyDirection.enabled = false;
			}
			if (this.initialExplosionEffect)
			{
				EffectManager.SpawnEffect(this.deathExplosionEffect, new EffectData
				{
					origin = base.characterBody.corePosition,
					scale = base.characterBody.radius + this.deathEffectRadius
				}, false);
			}
			if (base.isAuthority && this.destroyOnImpact)
			{
				this.rigidbodyCollisionListener = base.gameObject.AddComponent<DeathState.RigidbodyCollisionListener>();
				this.rigidbodyCollisionListener.deathState = this;
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004BAEF File Offset: 0x00049CEF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge > this.deathDuration)
			{
				this.Explode();
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004BB12 File Offset: 0x00049D12
		public void Explode()
		{
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004BB20 File Offset: 0x00049D20
		public virtual void OnImpactServer(Vector3 contactPoint)
		{
			string text = BodyCatalog.GetBodyName(base.characterBody.bodyIndex);
			text = text.Replace("Body", "");
			text = "iscBroken" + text;
			SpawnCard spawnCard = LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/" + text);
			if (spawnCard != null)
			{
				DirectorPlacementRule placementRule = new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Direct,
					position = contactPoint
				};
				GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, new Xoroshiro128Plus(0UL)));
				if (gameObject)
				{
					PurchaseInteraction component = gameObject.GetComponent<PurchaseInteraction>();
					if (component && component.costType == CostTypeIndex.Money)
					{
						component.Networkcost = Run.instance.GetDifficultyScaledCost(component.cost);
					}
				}
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004BBE0 File Offset: 0x00049DE0
		public override void OnExit()
		{
			if (this.deathExplosionEffect)
			{
				EffectManager.SpawnEffect(this.deathExplosionEffect, new EffectData
				{
					origin = base.characterBody.corePosition,
					scale = base.characterBody.radius + this.deathEffectRadius
				}, false);
			}
			if (this.rigidbodyCollisionListener)
			{
				EntityState.Destroy(this.rigidbodyCollisionListener);
			}
			Util.PlaySound(this.deathSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x040015BB RID: 5563
		[SerializeField]
		public GameObject initialExplosionEffect;

		// Token: 0x040015BC RID: 5564
		[SerializeField]
		public GameObject deathExplosionEffect;

		// Token: 0x040015BD RID: 5565
		[SerializeField]
		public string initialSoundString;

		// Token: 0x040015BE RID: 5566
		[SerializeField]
		public string deathSoundString;

		// Token: 0x040015BF RID: 5567
		[SerializeField]
		public float deathEffectRadius;

		// Token: 0x040015C0 RID: 5568
		[SerializeField]
		public float forceAmount = 20f;

		// Token: 0x040015C1 RID: 5569
		[SerializeField]
		public float deathDuration = 2f;

		// Token: 0x040015C2 RID: 5570
		[SerializeField]
		public bool destroyOnImpact;

		// Token: 0x040015C3 RID: 5571
		private DeathState.RigidbodyCollisionListener rigidbodyCollisionListener;

		// Token: 0x020003C4 RID: 964
		public class RigidbodyCollisionListener : MonoBehaviour
		{
			// Token: 0x06001134 RID: 4404 RVA: 0x0004BC84 File Offset: 0x00049E84
			private void OnCollisionEnter(Collision collision)
			{
				this.deathState.OnImpactServer(collision.GetContact(0).point);
				this.deathState.Explode();
			}

			// Token: 0x040015C4 RID: 5572
			public DeathState deathState;
		}
	}
}
