using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007A3 RID: 1955
	public class MapZone : MonoBehaviour
	{
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600294F RID: 10575 RVA: 0x000B2B7C File Offset: 0x000B0D7C
		// (remove) Token: 0x06002950 RID: 10576 RVA: 0x000B2BB4 File Offset: 0x000B0DB4
		public event Action<CharacterBody> onBodyTeleport;

		// Token: 0x06002951 RID: 10577 RVA: 0x000B2BE9 File Offset: 0x000B0DE9
		static MapZone()
		{
			VehicleSeat.onPassengerExitGlobal += MapZone.CheckCharacterOnVehicleExit;
			RoR2Application.onFixedUpdate += MapZone.StaticFixedUpdate;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000B2C18 File Offset: 0x000B0E18
		private static bool TestColliders(Collider characterCollider, Collider triggerCollider)
		{
			Vector3 vector;
			float num;
			return Physics.ComputePenetration(characterCollider, characterCollider.transform.position, characterCollider.transform.rotation, triggerCollider, triggerCollider.transform.position, triggerCollider.transform.rotation, out vector, out num);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000B2C5C File Offset: 0x000B0E5C
		private void Awake()
		{
			this.collider = base.GetComponent<Collider>();
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000B2C6A File Offset: 0x000B0E6A
		public void OnTriggerEnter(Collider other)
		{
			if (this.triggerType == MapZone.TriggerType.TriggerEnter)
			{
				this.TryZoneStart(other);
				return;
			}
			this.TryZoneEnd(other);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000B2C84 File Offset: 0x000B0E84
		public void OnTriggerExit(Collider other)
		{
			if (this.triggerType == MapZone.TriggerType.TriggerExit)
			{
				this.TryZoneStart(other);
				return;
			}
			this.TryZoneEnd(other);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000B2CA0 File Offset: 0x000B0EA0
		private void TryZoneStart(Collider other)
		{
			CharacterBody component = other.GetComponent<CharacterBody>();
			if (!component)
			{
				return;
			}
			if (component.currentVehicle)
			{
				this.queuedCollisions.Add(new MapZone.CollisionInfo(this, other));
				return;
			}
			TeamComponent teamComponent = component.teamComponent;
			MapZone.ZoneType zoneType = this.zoneType;
			if (zoneType != MapZone.ZoneType.OutOfBounds)
			{
				if (zoneType != MapZone.ZoneType.KickOutPlayers)
				{
					return;
				}
				if (teamComponent.teamIndex == TeamIndex.Player)
				{
					this.TeleportBody(component);
				}
			}
			else
			{
				bool flag = false;
				if (component.inventory)
				{
					flag = (component.inventory.GetItemCount(RoR2Content.Items.InvadingDoppelganger) > 0 || component.inventory.GetItemCount(RoR2Content.Items.TeleportWhenOob) > 0);
				}
				if (teamComponent.teamIndex == TeamIndex.Player || flag)
				{
					this.TeleportBody(component);
					return;
				}
				if (NetworkServer.active)
				{
					if (Physics.GetIgnoreLayerCollision(base.gameObject.layer, other.gameObject.layer))
					{
						return;
					}
					Debug.LogFormat("Killing {0} for being out of bounds.", new object[]
					{
						component.gameObject
					});
					HealthComponent healthComponent = component.healthComponent;
					if (healthComponent)
					{
						if (component.master)
						{
							component.master.TrueKill(healthComponent.lastHitAttacker, base.gameObject, DamageType.OutOfBounds);
							return;
						}
						healthComponent.Suicide(healthComponent.lastHitAttacker, base.gameObject, DamageType.OutOfBounds);
						return;
					}
					else if (component.master)
					{
						component.master.TrueKill(null, null, DamageType.OutOfBounds);
						return;
					}
				}
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000B2E13 File Offset: 0x000B1013
		private void TryZoneEnd(Collider other)
		{
			if (this.queuedCollisions.Count == 0)
			{
				return;
			}
			if (this.queuedCollisions.Contains(new MapZone.CollisionInfo(this, other)))
			{
				this.queuedCollisions.Remove(new MapZone.CollisionInfo(this, other));
			}
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000B2E4C File Offset: 0x000B104C
		private void ProcessQueuedCollisionsForCollider(Collider collider)
		{
			for (int i = this.queuedCollisions.Count - 1; i >= 0; i--)
			{
				if (this.queuedCollisions[i].otherCollider == collider)
				{
					this.queuedCollisions.RemoveAt(i);
					this.TryZoneStart(collider);
				}
			}
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000B2E98 File Offset: 0x000B1098
		private static void StaticFixedUpdate()
		{
			int i = 0;
			int count = MapZone.collidersToCheckInFixedUpdate.Count;
			while (i < count)
			{
				Collider exists = MapZone.collidersToCheckInFixedUpdate.Dequeue();
				if (exists)
				{
					foreach (MapZone mapZone in InstanceTracker.GetInstancesList<MapZone>())
					{
						mapZone.ProcessQueuedCollisionsForCollider(exists);
					}
				}
				i++;
			}
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000B2F14 File Offset: 0x000B1114
		private static void CheckCharacterOnVehicleExit(VehicleSeat vehicleSeat, GameObject passengerObject)
		{
			Collider component = passengerObject.GetComponent<Collider>();
			MapZone.collidersToCheckInFixedUpdate.Enqueue(component);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000B2F33 File Offset: 0x000B1133
		private void OnEnable()
		{
			InstanceTracker.Add<MapZone>(this);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000B2F3B File Offset: 0x000B113B
		private void OnDisable()
		{
			InstanceTracker.Remove<MapZone>(this);
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000B2F44 File Offset: 0x000B1144
		private void TeleportBody(CharacterBody characterBody)
		{
			if (!Util.HasEffectiveAuthority(characterBody.gameObject))
			{
				return;
			}
			if (!Physics.GetIgnoreLayerCollision(base.gameObject.layer, characterBody.gameObject.layer))
			{
				Vector3 vector = Run.instance.FindSafeTeleportPosition(characterBody, this.explicitDestination, 0f, this.destinationIdealRadius);
				Debug.Log("tp back");
				TeleportHelper.TeleportBody(characterBody, vector);
				GameObject teleportEffectPrefab = Run.instance.GetTeleportEffectPrefab(characterBody.gameObject);
				if (this.explicitSpawnEffectPrefab)
				{
					teleportEffectPrefab = this.explicitSpawnEffectPrefab;
				}
				if (teleportEffectPrefab)
				{
					EffectManager.SimpleEffect(teleportEffectPrefab, vector, Quaternion.identity, true);
				}
				Action<CharacterBody> action = this.onBodyTeleport;
				if (action == null)
				{
					return;
				}
				action(characterBody);
			}
		}

		// Token: 0x04002CB5 RID: 11445
		public MapZone.TriggerType triggerType;

		// Token: 0x04002CB6 RID: 11446
		public MapZone.ZoneType zoneType;

		// Token: 0x04002CB7 RID: 11447
		public Transform explicitDestination;

		// Token: 0x04002CB8 RID: 11448
		public GameObject explicitSpawnEffectPrefab;

		// Token: 0x04002CB9 RID: 11449
		public float destinationIdealRadius;

		// Token: 0x04002CBA RID: 11450
		private Collider collider;

		// Token: 0x04002CBB RID: 11451
		private readonly List<MapZone.CollisionInfo> queuedCollisions = new List<MapZone.CollisionInfo>();

		// Token: 0x04002CBC RID: 11452
		private static readonly Queue<Collider> collidersToCheckInFixedUpdate = new Queue<Collider>();

		// Token: 0x020007A4 RID: 1956
		public enum TriggerType
		{
			// Token: 0x04002CBE RID: 11454
			TriggerExit,
			// Token: 0x04002CBF RID: 11455
			TriggerEnter
		}

		// Token: 0x020007A5 RID: 1957
		public enum ZoneType
		{
			// Token: 0x04002CC1 RID: 11457
			OutOfBounds,
			// Token: 0x04002CC2 RID: 11458
			KickOutPlayers
		}

		// Token: 0x020007A6 RID: 1958
		private struct CollisionInfo : IEquatable<MapZone.CollisionInfo>
		{
			// Token: 0x0600295F RID: 10591 RVA: 0x000B3008 File Offset: 0x000B1208
			public CollisionInfo(MapZone mapZone, Collider otherCollider)
			{
				this.mapZone = mapZone;
				this.otherCollider = otherCollider;
			}

			// Token: 0x06002960 RID: 10592 RVA: 0x000B3018 File Offset: 0x000B1218
			public bool Equals(MapZone.CollisionInfo other)
			{
				return this.mapZone == other.mapZone && this.otherCollider == other.otherCollider;
			}

			// Token: 0x06002961 RID: 10593 RVA: 0x000B3038 File Offset: 0x000B1238
			public override bool Equals(object obj)
			{
				return obj is MapZone.CollisionInfo && this.Equals((MapZone.CollisionInfo)obj);
			}

			// Token: 0x06002962 RID: 10594 RVA: 0x000B3050 File Offset: 0x000B1250
			public override int GetHashCode()
			{
				return this.otherCollider.GetHashCode();
			}

			// Token: 0x04002CC3 RID: 11459
			public readonly MapZone mapZone;

			// Token: 0x04002CC4 RID: 11460
			public readonly Collider otherCollider;
		}
	}
}
