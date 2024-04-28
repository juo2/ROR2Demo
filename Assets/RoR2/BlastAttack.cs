using System;
using System.Runtime.CompilerServices;
using HG;
using RoR2.Networking;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020004D8 RID: 1240
	public class BlastAttack
	{
		// Token: 0x06001687 RID: 5767 RVA: 0x000633A0 File Offset: 0x000615A0
		public BlastAttack.Result Fire()
		{
			BlastAttack.HitPoint[] array = this.CollectHits();
			this.HandleHits(array);
			return new BlastAttack.Result
			{
				hitCount = array.Length,
				hitPoints = array
			};
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000633D8 File Offset: 0x000615D8
		[NetworkMessageHandler(msgType = 75, client = false, server = true)]
		private static void HandleReportBlastAttackDamage(NetworkMessage netMsg)
		{
			NetworkReader reader = netMsg.reader;
			BlastAttack.BlastAttackDamageInfo blastAttackDamageInfo = default(BlastAttack.BlastAttackDamageInfo);
			blastAttackDamageInfo.Read(reader);
			BlastAttack.PerformDamageServer(blastAttackDamageInfo);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00063404 File Offset: 0x00061604
		private BlastAttack.HitPoint[] CollectHits()
		{
			Vector3 vector = this.position;
			Collider[] array = Physics.OverlapSphere(vector, this.radius, LayerIndex.entityPrecise.mask);
			int num = array.Length;
			int num2 = 0;
			BlastAttack.<>c__DisplayClass28_0 CS$<>8__locals1;
			CS$<>8__locals1.encounteredHealthComponentsLength = 0;
			CS$<>8__locals1.hitOrderBufferLength = 0;
			ArrayUtils.EnsureCapacity<BlastAttack.HitPoint>(ref BlastAttack.hitPointsBuffer, num);
			ArrayUtils.EnsureCapacity<int>(ref BlastAttack.hitOrderBuffer, num);
			ArrayUtils.EnsureCapacity<HealthComponent>(ref BlastAttack.encounteredHealthComponentsBuffer, num);
			for (int i = 0; i < num; i++)
			{
				Collider collider = array[i];
				HurtBox component = collider.GetComponent<HurtBox>();
				if (component)
				{
					HealthComponent healthComponent = component.healthComponent;
					if (healthComponent)
					{
						bool flag = true;
						switch (this.attackerFiltering)
						{
						case AttackerFiltering.Default:
							flag = true;
							break;
						case AttackerFiltering.AlwaysHit:
							flag = false;
							break;
						case AttackerFiltering.NeverHitSelf:
							flag = true;
							if (healthComponent.gameObject == this.attacker)
							{
								goto IL_16B;
							}
							break;
						case AttackerFiltering.AlwaysHitSelf:
							flag = true;
							if (healthComponent.gameObject == this.attacker)
							{
								flag = false;
							}
							break;
						}
						if (!flag || FriendlyFireManager.ShouldSplashHitProceed(healthComponent, this.teamIndex))
						{
							Vector3 vector2 = collider.transform.position;
							Vector3 hitNormal = vector - vector2;
							float sqrMagnitude = hitNormal.sqrMagnitude;
							BlastAttack.hitPointsBuffer[num2++] = new BlastAttack.HitPoint
							{
								hurtBox = component,
								hitPosition = vector2,
								hitNormal = hitNormal,
								distanceSqr = sqrMagnitude
							};
						}
					}
				}
				IL_16B:;
			}
			if (true)
			{
				for (int j = 0; j < num2; j++)
				{
					ref BlastAttack.HitPoint ptr = ref BlastAttack.hitPointsBuffer[j];
					RaycastHit raycastHit;
					if (ptr.hurtBox != null && ptr.distanceSqr > 0f && ptr.hurtBox.collider.Raycast(new Ray(vector, -ptr.hitNormal), out raycastHit, this.radius))
					{
						ptr.distanceSqr = (vector - raycastHit.point).sqrMagnitude;
						ptr.hitPosition = raycastHit.point;
						ptr.hitNormal = raycastHit.normal;
					}
				}
			}
			CS$<>8__locals1.hitOrderBufferLength = num2;
			for (int k = 0; k < num2; k++)
			{
				BlastAttack.hitOrderBuffer[k] = k;
			}
			bool flag2 = true;
			while (flag2)
			{
				flag2 = false;
				for (int l = 1; l < CS$<>8__locals1.hitOrderBufferLength; l++)
				{
					int num3 = l - 1;
					if (BlastAttack.hitPointsBuffer[BlastAttack.hitOrderBuffer[num3]].distanceSqr > BlastAttack.hitPointsBuffer[BlastAttack.hitOrderBuffer[l]].distanceSqr)
					{
						Util.Swap<int>(ref BlastAttack.hitOrderBuffer[num3], ref BlastAttack.hitOrderBuffer[l]);
						flag2 = true;
					}
				}
			}
			bool flag3 = this.losType == BlastAttack.LoSType.None || this.losType == BlastAttack.LoSType.NearestHit;
			for (int m = 0; m < CS$<>8__locals1.hitOrderBufferLength; m++)
			{
				int num4 = BlastAttack.hitOrderBuffer[m];
				ref BlastAttack.HitPoint ptr2 = ref BlastAttack.hitPointsBuffer[num4];
				HealthComponent healthComponent2 = ptr2.hurtBox.healthComponent;
				if (!BlastAttack.<CollectHits>g__EntityIsMarkedEncountered|28_1(healthComponent2, ref CS$<>8__locals1))
				{
					BlastAttack.<CollectHits>g__MarkEntityAsEncountered|28_2(healthComponent2, ref CS$<>8__locals1);
				}
				else if (flag3)
				{
					ptr2.hurtBox = null;
				}
			}
			BlastAttack.<CollectHits>g__ClearEncounteredEntities|28_3(ref CS$<>8__locals1);
			BlastAttack.<CollectHits>g__CondenseHitOrderBuffer|28_0(ref CS$<>8__locals1);
			BlastAttack.LoSType loSType = this.losType;
			if (loSType != BlastAttack.LoSType.None && loSType == BlastAttack.LoSType.NearestHit)
			{
				NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(CS$<>8__locals1.hitOrderBufferLength, Allocator.TempJob, NativeArrayOptions.ClearMemory);
				NativeArray<RaycastHit> results = new NativeArray<RaycastHit>(CS$<>8__locals1.hitOrderBufferLength, Allocator.TempJob, NativeArrayOptions.ClearMemory);
				int n = 0;
				int num5 = 0;
				while (n < CS$<>8__locals1.hitOrderBufferLength)
				{
					int num6 = BlastAttack.hitOrderBuffer[n];
					ref BlastAttack.HitPoint ptr3 = ref BlastAttack.hitPointsBuffer[num6];
					commands[num5++] = new RaycastCommand(vector, ptr3.hitPosition - vector, Mathf.Sqrt(ptr3.distanceSqr), LayerIndex.world.mask, 1);
					n++;
				}
				bool queriesHitTriggers = Physics.queriesHitTriggers;
				Physics.queriesHitTriggers = true;
				RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle)).Complete();
				Physics.queriesHitTriggers = queriesHitTriggers;
				int num7 = 0;
				int num8 = 0;
				while (num7 < CS$<>8__locals1.hitOrderBufferLength)
				{
					int num9 = BlastAttack.hitOrderBuffer[num7];
					ref BlastAttack.HitPoint ptr4 = ref BlastAttack.hitPointsBuffer[num9];
					if (ptr4.hurtBox != null && results[num8++].collider)
					{
						ptr4.hurtBox = null;
					}
					num7++;
				}
				results.Dispose();
				commands.Dispose();
				BlastAttack.<CollectHits>g__CondenseHitOrderBuffer|28_0(ref CS$<>8__locals1);
			}
			BlastAttack.HitPoint[] array2 = new BlastAttack.HitPoint[CS$<>8__locals1.hitOrderBufferLength];
			for (int num10 = 0; num10 < CS$<>8__locals1.hitOrderBufferLength; num10++)
			{
				int num11 = BlastAttack.hitOrderBuffer[num10];
				array2[num10] = BlastAttack.hitPointsBuffer[num11];
			}
			ArrayUtils.Clear<BlastAttack.HitPoint>(BlastAttack.hitPointsBuffer, ref num2);
			BlastAttack.<CollectHits>g__ClearEncounteredEntities|28_3(ref CS$<>8__locals1);
			return array2;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000638EC File Offset: 0x00061AEC
		private void HandleHits(BlastAttack.HitPoint[] hitPoints)
		{
			Vector3 b = this.position;
			foreach (BlastAttack.HitPoint hitPoint in hitPoints)
			{
				float num = Mathf.Sqrt(hitPoint.distanceSqr);
				float num2 = 0f;
				Vector3 a = (num > 0f) ? ((hitPoint.hitPosition - b) / num) : Vector3.zero;
				HealthComponent healthComponent = hitPoint.hurtBox ? hitPoint.hurtBox.healthComponent : null;
				if (healthComponent)
				{
					switch (this.falloffModel)
					{
					case BlastAttack.FalloffModel.None:
						num2 = 1f;
						break;
					case BlastAttack.FalloffModel.Linear:
						num2 = 1f - Mathf.Clamp01(num / this.radius);
						break;
					case BlastAttack.FalloffModel.SweetSpot:
						num2 = 1f - ((num > this.radius / 2f) ? 0.75f : 0f);
						break;
					}
					BlastAttack.BlastAttackDamageInfo blastAttackDamageInfo = new BlastAttack.BlastAttackDamageInfo
					{
						attacker = this.attacker,
						inflictor = this.inflictor,
						crit = this.crit,
						damage = this.baseDamage * num2,
						damageColorIndex = this.damageColorIndex,
						damageModifier = hitPoint.hurtBox.damageModifier,
						damageType = (this.damageType | DamageType.AOE),
						force = this.bonusForce * num2 + this.baseForce * num2 * a,
						position = hitPoint.hitPosition,
						procChainMask = this.procChainMask,
						procCoefficient = this.procCoefficient,
						hitHealthComponent = healthComponent,
						canRejectForce = this.canRejectForce
					};
					if (NetworkServer.active)
					{
						BlastAttack.PerformDamageServer(blastAttackDamageInfo);
					}
					else
					{
						BlastAttack.ClientReportDamage(blastAttackDamageInfo);
					}
					if (this.impactEffect != EffectIndex.Invalid)
					{
						EffectData effectData = new EffectData();
						effectData.origin = hitPoint.hitPosition;
						effectData.rotation = Quaternion.LookRotation(-a);
						EffectManager.SpawnEffect(this.impactEffect, effectData, true);
					}
				}
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00063B0C File Offset: 0x00061D0C
		private static void ClientReportDamage(in BlastAttack.BlastAttackDamageInfo blastAttackDamageInfo)
		{
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(75);
			BlastAttack.BlastAttackDamageInfo blastAttackDamageInfo2 = blastAttackDamageInfo;
			blastAttackDamageInfo2.Write(networkWriter);
			networkWriter.FinishMessage();
			PlatformSystems.networkManager.client.connection.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00063B5C File Offset: 0x00061D5C
		private static void PerformDamageServer(in BlastAttack.BlastAttackDamageInfo blastAttackDamageInfo)
		{
			if (!blastAttackDamageInfo.hitHealthComponent)
			{
				return;
			}
			DamageInfo damageInfo = new DamageInfo();
			damageInfo.attacker = blastAttackDamageInfo.attacker;
			damageInfo.inflictor = blastAttackDamageInfo.inflictor;
			damageInfo.damage = blastAttackDamageInfo.damage;
			damageInfo.crit = blastAttackDamageInfo.crit;
			damageInfo.force = blastAttackDamageInfo.force;
			damageInfo.procChainMask = blastAttackDamageInfo.procChainMask;
			damageInfo.procCoefficient = blastAttackDamageInfo.procCoefficient;
			damageInfo.damageType = blastAttackDamageInfo.damageType;
			damageInfo.damageColorIndex = blastAttackDamageInfo.damageColorIndex;
			damageInfo.position = blastAttackDamageInfo.position;
			damageInfo.canRejectForce = blastAttackDamageInfo.canRejectForce;
			damageInfo.ModifyDamageInfo(blastAttackDamageInfo.damageModifier);
			blastAttackDamageInfo.hitHealthComponent.TakeDamage(damageInfo);
			GlobalEventManager.instance.OnHitEnemy(damageInfo, blastAttackDamageInfo.hitHealthComponent.gameObject);
			GlobalEventManager.instance.OnHitAll(damageInfo, blastAttackDamageInfo.hitHealthComponent.gameObject);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00063CA8 File Offset: 0x00061EA8
		[CompilerGenerated]
		internal static void <CollectHits>g__CondenseHitOrderBuffer|28_0(ref BlastAttack.<>c__DisplayClass28_0 A_0)
		{
			for (int i = 0; i < A_0.hitOrderBufferLength; i++)
			{
				int num = 0;
				for (int j = i; j < A_0.hitOrderBufferLength; j++)
				{
					int num2 = BlastAttack.hitOrderBuffer[j];
					if (BlastAttack.hitPointsBuffer[num2].hurtBox != null)
					{
						break;
					}
					num++;
				}
				if (num > 0)
				{
					ArrayUtils.ArrayRemoveAt<int>(BlastAttack.hitOrderBuffer, ref A_0.hitOrderBufferLength, i, num);
				}
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00063D10 File Offset: 0x00061F10
		[CompilerGenerated]
		internal static bool <CollectHits>g__EntityIsMarkedEncountered|28_1(HealthComponent healthComponent, ref BlastAttack.<>c__DisplayClass28_0 A_1)
		{
			for (int i = 0; i < A_1.encounteredHealthComponentsLength; i++)
			{
				if (BlastAttack.encounteredHealthComponentsBuffer[i] == healthComponent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00063D3C File Offset: 0x00061F3C
		[CompilerGenerated]
		internal static void <CollectHits>g__MarkEntityAsEncountered|28_2(HealthComponent healthComponent, ref BlastAttack.<>c__DisplayClass28_0 A_1)
		{
			HealthComponent[] array = BlastAttack.encounteredHealthComponentsBuffer;
			int encounteredHealthComponentsLength = A_1.encounteredHealthComponentsLength;
			A_1.encounteredHealthComponentsLength = encounteredHealthComponentsLength + 1;
			array[encounteredHealthComponentsLength] = healthComponent;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00063D61 File Offset: 0x00061F61
		[CompilerGenerated]
		internal static void <CollectHits>g__ClearEncounteredEntities|28_3(ref BlastAttack.<>c__DisplayClass28_0 A_0)
		{
			Array.Clear(BlastAttack.encounteredHealthComponentsBuffer, 0, A_0.encounteredHealthComponentsLength);
			A_0.encounteredHealthComponentsLength = 0;
		}

		// Token: 0x04001C3D RID: 7229
		public GameObject attacker;

		// Token: 0x04001C3E RID: 7230
		public GameObject inflictor;

		// Token: 0x04001C3F RID: 7231
		public TeamIndex teamIndex;

		// Token: 0x04001C40 RID: 7232
		public AttackerFiltering attackerFiltering;

		// Token: 0x04001C41 RID: 7233
		public Vector3 position;

		// Token: 0x04001C42 RID: 7234
		public float radius;

		// Token: 0x04001C43 RID: 7235
		public BlastAttack.FalloffModel falloffModel = BlastAttack.FalloffModel.Linear;

		// Token: 0x04001C44 RID: 7236
		public float baseDamage;

		// Token: 0x04001C45 RID: 7237
		public float baseForce;

		// Token: 0x04001C46 RID: 7238
		public Vector3 bonusForce;

		// Token: 0x04001C47 RID: 7239
		public bool crit;

		// Token: 0x04001C48 RID: 7240
		public DamageType damageType;

		// Token: 0x04001C49 RID: 7241
		public DamageColorIndex damageColorIndex;

		// Token: 0x04001C4A RID: 7242
		public BlastAttack.LoSType losType;

		// Token: 0x04001C4B RID: 7243
		public EffectIndex impactEffect = EffectIndex.Invalid;

		// Token: 0x04001C4C RID: 7244
		public bool canRejectForce = true;

		// Token: 0x04001C4D RID: 7245
		public ProcChainMask procChainMask;

		// Token: 0x04001C4E RID: 7246
		public float procCoefficient = 1f;

		// Token: 0x04001C4F RID: 7247
		private static readonly int initialBufferSize = 256;

		// Token: 0x04001C50 RID: 7248
		private static BlastAttack.HitPoint[] hitPointsBuffer = new BlastAttack.HitPoint[BlastAttack.initialBufferSize];

		// Token: 0x04001C51 RID: 7249
		private static int[] hitOrderBuffer = new int[BlastAttack.initialBufferSize];

		// Token: 0x04001C52 RID: 7250
		private static HealthComponent[] encounteredHealthComponentsBuffer = new HealthComponent[BlastAttack.initialBufferSize];

		// Token: 0x020004D9 RID: 1241
		public enum FalloffModel
		{
			// Token: 0x04001C54 RID: 7252
			None,
			// Token: 0x04001C55 RID: 7253
			Linear,
			// Token: 0x04001C56 RID: 7254
			SweetSpot
		}

		// Token: 0x020004DA RID: 1242
		public enum LoSType
		{
			// Token: 0x04001C58 RID: 7256
			None,
			// Token: 0x04001C59 RID: 7257
			NearestHit
		}

		// Token: 0x020004DB RID: 1243
		public struct HitPoint
		{
			// Token: 0x04001C5A RID: 7258
			public HurtBox hurtBox;

			// Token: 0x04001C5B RID: 7259
			public Vector3 hitPosition;

			// Token: 0x04001C5C RID: 7260
			public Vector3 hitNormal;

			// Token: 0x04001C5D RID: 7261
			public float distanceSqr;
		}

		// Token: 0x020004DC RID: 1244
		public struct Result
		{
			// Token: 0x04001C5E RID: 7262
			public int hitCount;

			// Token: 0x04001C5F RID: 7263
			public BlastAttack.HitPoint[] hitPoints;
		}

		// Token: 0x020004DD RID: 1245
		private struct BlastAttackDamageInfo
		{
			// Token: 0x06001693 RID: 5779 RVA: 0x00063D7C File Offset: 0x00061F7C
			public void Write(NetworkWriter writer)
			{
				writer.Write(this.attacker);
				writer.Write(this.inflictor);
				writer.Write(this.crit);
				writer.Write(this.damage);
				writer.Write(this.damageColorIndex);
				writer.Write((byte)this.damageModifier);
				writer.Write(this.damageType);
				writer.Write(this.force);
				writer.Write(this.position);
				writer.Write(this.procChainMask);
				writer.Write(this.procCoefficient);
				writer.Write(this.hitHealthComponent.netId);
				writer.Write(this.canRejectForce);
			}

			// Token: 0x06001694 RID: 5780 RVA: 0x00063E30 File Offset: 0x00062030
			public void Read(NetworkReader reader)
			{
				this.attacker = reader.ReadGameObject();
				this.inflictor = reader.ReadGameObject();
				this.crit = reader.ReadBoolean();
				this.damage = reader.ReadSingle();
				this.damageColorIndex = reader.ReadDamageColorIndex();
				this.damageModifier = (HurtBox.DamageModifier)reader.ReadByte();
				this.damageType = reader.ReadDamageType();
				this.force = reader.ReadVector3();
				this.position = reader.ReadVector3();
				this.procChainMask = reader.ReadProcChainMask();
				this.procCoefficient = reader.ReadSingle();
				GameObject gameObject = reader.ReadGameObject();
				this.hitHealthComponent = (gameObject ? gameObject.GetComponent<HealthComponent>() : null);
				this.canRejectForce = reader.ReadBoolean();
			}

			// Token: 0x04001C60 RID: 7264
			public GameObject attacker;

			// Token: 0x04001C61 RID: 7265
			public GameObject inflictor;

			// Token: 0x04001C62 RID: 7266
			public bool crit;

			// Token: 0x04001C63 RID: 7267
			public float damage;

			// Token: 0x04001C64 RID: 7268
			public DamageColorIndex damageColorIndex;

			// Token: 0x04001C65 RID: 7269
			public HurtBox.DamageModifier damageModifier;

			// Token: 0x04001C66 RID: 7270
			public DamageType damageType;

			// Token: 0x04001C67 RID: 7271
			public Vector3 force;

			// Token: 0x04001C68 RID: 7272
			public Vector3 position;

			// Token: 0x04001C69 RID: 7273
			public ProcChainMask procChainMask;

			// Token: 0x04001C6A RID: 7274
			public float procCoefficient;

			// Token: 0x04001C6B RID: 7275
			public HealthComponent hitHealthComponent;

			// Token: 0x04001C6C RID: 7276
			public bool canRejectForce;
		}
	}
}
