using System;
using System.Collections.Generic;
using RoR2.Audio;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000979 RID: 2425
	public class OverlapAttack
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x000E7EDC File Offset: 0x000E60DC
		// (set) Token: 0x06003710 RID: 14096 RVA: 0x000E7EE4 File Offset: 0x000E60E4
		public Vector3 lastFireAverageHitPosition { get; private set; }

		// Token: 0x06003711 RID: 14097 RVA: 0x000E7EF0 File Offset: 0x000E60F0
		private bool HurtBoxPassesFilter(HurtBox hurtBox)
		{
			return !hurtBox.healthComponent || ((!(hurtBox.healthComponent.gameObject == this.attacker) || this.attackerFiltering != AttackerFiltering.NeverHitSelf) && (!(this.attacker == null) || !(hurtBox.healthComponent.gameObject.GetComponent<MaulingRock>() != null)) && !this.ignoredHealthComponentList.Contains(hurtBox.healthComponent) && FriendlyFireManager.ShouldDirectHitProceed(hurtBox.healthComponent, this.teamIndex));
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000E7F84 File Offset: 0x000E6184
		public bool Fire(List<HurtBox> hitResults = null)
		{
			if (!this.hitBoxGroup)
			{
				return false;
			}
			foreach (HitBox hitBox in this.hitBoxGroup.hitBoxes)
			{
				if (hitBox && hitBox.enabled && hitBox.gameObject && hitBox.gameObject.activeInHierarchy && hitBox.transform)
				{
					Transform transform = hitBox.transform;
					Vector3 position = transform.position;
					Vector3 vector = transform.lossyScale * 0.5f;
					Quaternion rotation = transform.rotation;
					if (float.IsInfinity(vector.x) || float.IsInfinity(vector.y) || float.IsInfinity(vector.z))
					{
						Chat.AddMessage("Aborting OverlapAttack.Fire: hitBoxHalfExtents are infinite.");
					}
					else if (float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z))
					{
						Chat.AddMessage("Aborting OverlapAttack.Fire: hitBoxHalfExtents are NaN.");
					}
					else if (float.IsInfinity(position.x) || float.IsInfinity(position.y) || float.IsInfinity(position.z))
					{
						Chat.AddMessage("Aborting OverlapAttack.Fire: hitBoxCenter is infinite.");
					}
					else if (float.IsNaN(position.x) || float.IsNaN(position.y) || float.IsNaN(position.z))
					{
						Chat.AddMessage("Aborting OverlapAttack.Fire: hitBoxCenter is NaN.");
					}
					else if (float.IsInfinity(rotation.x) || float.IsInfinity(rotation.y) || float.IsInfinity(rotation.z) || float.IsInfinity(rotation.w))
					{
						Chat.AddMessage("Aborting OverlapAttack.Fire: hitBoxRotation is infinite.");
					}
					else if (float.IsNaN(rotation.x) || float.IsNaN(rotation.y) || float.IsNaN(rotation.z) || float.IsNaN(rotation.w))
					{
						Chat.AddMessage("Aborting OverlapAttack.Fire: hitBoxRotation is NaN.");
					}
					else
					{
						Collider[] array = Physics.OverlapBox(position, vector, rotation, LayerIndex.entityPrecise.mask);
						int num = array.Length;
						int num2 = 0;
						for (int j = 0; j < num; j++)
						{
							if (array[j])
							{
								HurtBox component = array[j].GetComponent<HurtBox>();
								if (component && this.HurtBoxPassesFilter(component) && component.transform)
								{
									Vector3 position2 = component.transform.position;
									this.overlapList.Add(new OverlapAttack.OverlapInfo
									{
										hurtBox = component,
										hitPosition = position2,
										pushDirection = (position2 - position).normalized
									});
									this.ignoredHealthComponentList.Add(component.healthComponent);
									if (hitResults != null)
									{
										hitResults.Add(component);
									}
									num2++;
								}
								if (num2 >= this.maximumOverlapTargets)
								{
									break;
								}
							}
						}
					}
				}
			}
			this.ProcessHits(this.overlapList);
			bool result = this.overlapList.Count > 0;
			this.overlapList.Clear();
			return result;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000E82BC File Offset: 0x000E64BC
		[NetworkMessageHandler(msgType = 71, client = false, server = true)]
		public static void HandleOverlapAttackHits(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<OverlapAttack.OverlapAttackMessage>(OverlapAttack.incomingMessage);
			OverlapAttack.PerformDamage(OverlapAttack.incomingMessage.attacker, OverlapAttack.incomingMessage.inflictor, OverlapAttack.incomingMessage.damage, OverlapAttack.incomingMessage.isCrit, OverlapAttack.incomingMessage.procChainMask, OverlapAttack.incomingMessage.procCoefficient, OverlapAttack.incomingMessage.damageColorIndex, OverlapAttack.incomingMessage.damageType, OverlapAttack.incomingMessage.forceVector, OverlapAttack.incomingMessage.pushAwayForce, OverlapAttack.incomingMessage.overlapInfoList);
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000E8348 File Offset: 0x000E6548
		private void ProcessHits(List<OverlapAttack.OverlapInfo> hitList)
		{
			if (hitList.Count == 0)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			float d = 1f / (float)hitList.Count;
			for (int i = 0; i < hitList.Count; i++)
			{
				OverlapAttack.OverlapInfo overlapInfo = hitList[i];
				if (this.hitEffectPrefab)
				{
					Vector3 forward = -hitList[i].pushDirection;
					EffectManager.SpawnEffect(this.hitEffectPrefab, new EffectData
					{
						origin = overlapInfo.hitPosition,
						rotation = Util.QuaternionSafeLookRotation(forward),
						networkSoundEventIndex = this.impactSound
					}, true);
				}
				vector += overlapInfo.hitPosition * d;
				SurfaceDefProvider surfaceDefProvider = overlapInfo.hurtBox ? overlapInfo.hurtBox.GetComponent<SurfaceDefProvider>() : null;
				if (surfaceDefProvider && surfaceDefProvider.surfaceDef)
				{
					SurfaceDef objectSurfaceDef = SurfaceDefProvider.GetObjectSurfaceDef(hitList[i].hurtBox.collider, hitList[i].hitPosition);
					if (objectSurfaceDef)
					{
						if (objectSurfaceDef.impactEffectPrefab)
						{
							EffectManager.SpawnEffect(objectSurfaceDef.impactEffectPrefab, new EffectData
							{
								origin = overlapInfo.hitPosition,
								rotation = ((overlapInfo.pushDirection == Vector3.zero) ? Quaternion.identity : Util.QuaternionSafeLookRotation(overlapInfo.pushDirection)),
								color = objectSurfaceDef.approximateColor,
								scale = 2f
							}, true);
						}
						if (objectSurfaceDef.impactSoundString != null && objectSurfaceDef.impactSoundString.Length != 0)
						{
							Util.PlaySound(objectSurfaceDef.impactSoundString, hitList[i].hurtBox.gameObject);
						}
					}
				}
			}
			this.lastFireAverageHitPosition = vector;
			if (NetworkServer.active)
			{
				OverlapAttack.PerformDamage(this.attacker, this.inflictor, this.damage, this.isCrit, this.procChainMask, this.procCoefficient, this.damageColorIndex, this.damageType, this.forceVector, this.pushAwayForce, hitList);
				return;
			}
			OverlapAttack.outgoingMessage.attacker = this.attacker;
			OverlapAttack.outgoingMessage.inflictor = this.inflictor;
			OverlapAttack.outgoingMessage.damage = this.damage;
			OverlapAttack.outgoingMessage.isCrit = this.isCrit;
			OverlapAttack.outgoingMessage.procChainMask = this.procChainMask;
			OverlapAttack.outgoingMessage.procCoefficient = this.procCoefficient;
			OverlapAttack.outgoingMessage.damageColorIndex = this.damageColorIndex;
			OverlapAttack.outgoingMessage.damageType = this.damageType;
			OverlapAttack.outgoingMessage.forceVector = this.forceVector;
			OverlapAttack.outgoingMessage.pushAwayForce = this.pushAwayForce;
			Util.CopyList<OverlapAttack.OverlapInfo>(hitList, OverlapAttack.outgoingMessage.overlapInfoList);
			PlatformSystems.networkManager.client.connection.SendByChannel(71, OverlapAttack.outgoingMessage, QosChannelIndex.defaultReliable.intVal);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000E8634 File Offset: 0x000E6834
		private static void PerformDamage(GameObject attacker, GameObject inflictor, float damage, bool isCrit, ProcChainMask procChainMask, float procCoefficient, DamageColorIndex damageColorIndex, DamageType damageType, Vector3 forceVector, float pushAwayForce, List<OverlapAttack.OverlapInfo> hitList)
		{
			for (int i = 0; i < hitList.Count; i++)
			{
				OverlapAttack.OverlapInfo overlapInfo = hitList[i];
				if (overlapInfo.hurtBox)
				{
					HealthComponent healthComponent = overlapInfo.hurtBox.healthComponent;
					if (healthComponent)
					{
						DamageInfo damageInfo = new DamageInfo();
						damageInfo.attacker = attacker;
						damageInfo.inflictor = inflictor;
						damageInfo.force = forceVector + pushAwayForce * overlapInfo.pushDirection;
						damageInfo.damage = damage;
						damageInfo.crit = isCrit;
						damageInfo.position = overlapInfo.hitPosition;
						damageInfo.procChainMask = procChainMask;
						damageInfo.procCoefficient = procCoefficient;
						damageInfo.damageColorIndex = damageColorIndex;
						damageInfo.damageType = damageType;
						damageInfo.ModifyDamageInfo(overlapInfo.hurtBox.damageModifier);
						healthComponent.TakeDamage(damageInfo);
						GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
						GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
					}
				}
			}
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000E872B File Offset: 0x000E692B
		public void ResetIgnoredHealthComponents()
		{
			this.ignoredHealthComponentList.Clear();
		}

		// Token: 0x04003756 RID: 14166
		public GameObject attacker;

		// Token: 0x04003757 RID: 14167
		public GameObject inflictor;

		// Token: 0x04003758 RID: 14168
		public TeamIndex teamIndex;

		// Token: 0x04003759 RID: 14169
		public AttackerFiltering attackerFiltering = AttackerFiltering.NeverHitSelf;

		// Token: 0x0400375A RID: 14170
		public Vector3 forceVector = Vector3.zero;

		// Token: 0x0400375B RID: 14171
		public float pushAwayForce;

		// Token: 0x0400375C RID: 14172
		public float damage = 1f;

		// Token: 0x0400375D RID: 14173
		public bool isCrit;

		// Token: 0x0400375E RID: 14174
		public ProcChainMask procChainMask;

		// Token: 0x0400375F RID: 14175
		public float procCoefficient = 1f;

		// Token: 0x04003760 RID: 14176
		public HitBoxGroup hitBoxGroup;

		// Token: 0x04003761 RID: 14177
		public GameObject hitEffectPrefab;

		// Token: 0x04003762 RID: 14178
		public NetworkSoundEventIndex impactSound = NetworkSoundEventIndex.Invalid;

		// Token: 0x04003763 RID: 14179
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003764 RID: 14180
		public DamageType damageType;

		// Token: 0x04003765 RID: 14181
		public int maximumOverlapTargets = 100;

		// Token: 0x04003766 RID: 14182
		private readonly List<HealthComponent> ignoredHealthComponentList = new List<HealthComponent>();

		// Token: 0x04003768 RID: 14184
		private readonly List<OverlapAttack.OverlapInfo> overlapList = new List<OverlapAttack.OverlapInfo>();

		// Token: 0x04003769 RID: 14185
		private static readonly OverlapAttack.OverlapAttackMessage incomingMessage = new OverlapAttack.OverlapAttackMessage();

		// Token: 0x0400376A RID: 14186
		private static readonly OverlapAttack.OverlapAttackMessage outgoingMessage = new OverlapAttack.OverlapAttackMessage();

		// Token: 0x0200097A RID: 2426
		private struct OverlapInfo
		{
			// Token: 0x0400376B RID: 14187
			public HurtBox hurtBox;

			// Token: 0x0400376C RID: 14188
			public Vector3 hitPosition;

			// Token: 0x0400376D RID: 14189
			public Vector3 pushDirection;
		}

		// Token: 0x0200097B RID: 2427
		public struct AttackInfo
		{
			// Token: 0x0400376E RID: 14190
			public GameObject attacker;

			// Token: 0x0400376F RID: 14191
			public GameObject inflictor;

			// Token: 0x04003770 RID: 14192
			public float damage;

			// Token: 0x04003771 RID: 14193
			public bool isCrit;

			// Token: 0x04003772 RID: 14194
			public float procCoefficient;

			// Token: 0x04003773 RID: 14195
			public DamageColorIndex damageColorIndex;

			// Token: 0x04003774 RID: 14196
			public DamageType damageType;

			// Token: 0x04003775 RID: 14197
			public Vector3 forceVector;
		}

		// Token: 0x0200097C RID: 2428
		private class OverlapAttackMessage : MessageBase
		{
			// Token: 0x06003719 RID: 14105 RVA: 0x000E87B0 File Offset: 0x000E69B0
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				writer.Write(this.attacker);
				writer.Write(this.inflictor);
				writer.Write(this.damage);
				writer.Write(this.isCrit);
				writer.Write(this.procChainMask);
				writer.Write(this.procCoefficient);
				writer.Write(this.damageColorIndex);
				writer.Write(this.damageType);
				writer.Write(this.forceVector);
				writer.Write(this.pushAwayForce);
				writer.WritePackedUInt32((uint)this.overlapInfoList.Count);
				foreach (OverlapAttack.OverlapInfo overlapInfo in this.overlapInfoList)
				{
					writer.Write(HurtBoxReference.FromHurtBox(overlapInfo.hurtBox));
					writer.Write(overlapInfo.hitPosition);
					writer.Write(overlapInfo.pushDirection);
				}
			}

			// Token: 0x0600371A RID: 14106 RVA: 0x000E88B8 File Offset: 0x000E6AB8
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				this.attacker = reader.ReadGameObject();
				this.inflictor = reader.ReadGameObject();
				this.damage = reader.ReadSingle();
				this.isCrit = reader.ReadBoolean();
				this.procChainMask = reader.ReadProcChainMask();
				this.procCoefficient = reader.ReadSingle();
				this.damageColorIndex = reader.ReadDamageColorIndex();
				this.damageType = reader.ReadDamageType();
				this.forceVector = reader.ReadVector3();
				this.pushAwayForce = reader.ReadSingle();
				this.overlapInfoList.Clear();
				int i = 0;
				int num = (int)reader.ReadPackedUInt32();
				while (i < num)
				{
					OverlapAttack.OverlapInfo item = default(OverlapAttack.OverlapInfo);
					GameObject gameObject = reader.ReadHurtBoxReference().ResolveGameObject();
					item.hurtBox = ((gameObject != null) ? gameObject.GetComponent<HurtBox>() : null);
					item.hitPosition = reader.ReadVector3();
					item.pushDirection = reader.ReadVector3();
					this.overlapInfoList.Add(item);
					i++;
				}
			}

			// Token: 0x04003776 RID: 14198
			public GameObject attacker;

			// Token: 0x04003777 RID: 14199
			public GameObject inflictor;

			// Token: 0x04003778 RID: 14200
			public float damage;

			// Token: 0x04003779 RID: 14201
			public bool isCrit;

			// Token: 0x0400377A RID: 14202
			public ProcChainMask procChainMask;

			// Token: 0x0400377B RID: 14203
			public float procCoefficient;

			// Token: 0x0400377C RID: 14204
			public DamageColorIndex damageColorIndex;

			// Token: 0x0400377D RID: 14205
			public DamageType damageType;

			// Token: 0x0400377E RID: 14206
			public Vector3 forceVector;

			// Token: 0x0400377F RID: 14207
			public float pushAwayForce;

			// Token: 0x04003780 RID: 14208
			public readonly List<OverlapAttack.OverlapInfo> overlapInfoList = new List<OverlapAttack.OverlapInfo>();
		}
	}
}
