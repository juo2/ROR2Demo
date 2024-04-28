using System;
using System.Collections.Generic;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020004EF RID: 1263
	public class BulletAttack
	{
		// Token: 0x060016F7 RID: 5879 RVA: 0x00065408 File Offset: 0x00063608
		public BulletAttack()
		{
			this.filterCallback = BulletAttack.defaultFilterCallback;
			this.hitCallback = BulletAttack.defaultHitCallback;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000654B0 File Offset: 0x000636B0
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			BulletAttack.sniperTargetHitEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/SniperTargetHitEffect");
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x000654C1 File Offset: 0x000636C1
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x000654C9 File Offset: 0x000636C9
		public Vector3 aimVector
		{
			get
			{
				return this._aimVector;
			}
			set
			{
				this._aimVector = value;
				this._aimVector.Normalize();
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x000654DD File Offset: 0x000636DD
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x000654E5 File Offset: 0x000636E5
		public float maxDistance
		{
			get
			{
				return this._maxDistance;
			}
			set
			{
				if (!float.IsInfinity(value) && !float.IsNaN(value))
				{
					this._maxDistance = value;
					return;
				}
				Debug.LogFormat("BulletAttack.maxDistance was assigned a value other than a finite number. value={0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00065518 File Offset: 0x00063718
		private static bool DefaultHitCallbackImplementation(BulletAttack bulletAttack, ref BulletAttack.BulletHit hitInfo)
		{
			bool result = false;
			if (hitInfo.collider)
			{
				result = ((1 << hitInfo.collider.gameObject.layer & bulletAttack.stopperMask) == 0);
			}
			if (bulletAttack.hitEffectPrefab)
			{
				EffectManager.SimpleImpactEffect(bulletAttack.hitEffectPrefab, hitInfo.point, bulletAttack.HitEffectNormal ? hitInfo.surfaceNormal : (-hitInfo.direction), true);
			}
			if (hitInfo.isSniperHit && BulletAttack.sniperTargetHitEffect != null)
			{
				EffectData effectData = new EffectData
				{
					origin = hitInfo.point,
					rotation = Quaternion.LookRotation(-hitInfo.direction)
				};
				effectData.SetHurtBoxReference(hitInfo.hitHurtBox);
				EffectManager.SpawnEffect(BulletAttack.sniperTargetHitEffect, effectData, true);
			}
			if (hitInfo.collider)
			{
				SurfaceDef objectSurfaceDef = SurfaceDefProvider.GetObjectSurfaceDef(hitInfo.collider, hitInfo.point);
				if (objectSurfaceDef && objectSurfaceDef.impactEffectPrefab)
				{
					EffectData effectData2 = new EffectData
					{
						origin = hitInfo.point,
						rotation = Quaternion.LookRotation(hitInfo.surfaceNormal),
						color = objectSurfaceDef.approximateColor,
						surfaceDefIndex = objectSurfaceDef.surfaceDefIndex
					};
					EffectManager.SpawnEffect(objectSurfaceDef.impactEffectPrefab, effectData2, true);
				}
			}
			GameObject entityObject = hitInfo.entityObject;
			if (entityObject)
			{
				float num = BulletAttack.CalcFalloffFactor(bulletAttack.falloffModel, hitInfo.distance);
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = bulletAttack.damage * num;
				damageInfo.crit = bulletAttack.isCrit;
				damageInfo.attacker = bulletAttack.owner;
				damageInfo.inflictor = bulletAttack.weapon;
				damageInfo.position = hitInfo.point;
				damageInfo.force = hitInfo.direction * (bulletAttack.force * num);
				damageInfo.procChainMask = bulletAttack.procChainMask;
				damageInfo.procCoefficient = bulletAttack.procCoefficient;
				damageInfo.damageType = bulletAttack.damageType;
				damageInfo.damageColorIndex = bulletAttack.damageColorIndex;
				damageInfo.ModifyDamageInfo(hitInfo.damageModifier);
				if (hitInfo.isSniperHit)
				{
					damageInfo.crit = true;
					damageInfo.damageColorIndex = DamageColorIndex.Sniper;
				}
				BulletAttack.ModifyOutgoingDamageCallback modifyOutgoingDamageCallback = bulletAttack.modifyOutgoingDamageCallback;
				if (modifyOutgoingDamageCallback != null)
				{
					modifyOutgoingDamageCallback(bulletAttack, ref hitInfo, damageInfo);
				}
				TeamIndex attackerTeamIndex = TeamIndex.None;
				if (bulletAttack.owner)
				{
					TeamComponent component = bulletAttack.owner.GetComponent<TeamComponent>();
					if (component)
					{
						attackerTeamIndex = component.teamIndex;
					}
				}
				HealthComponent healthComponent = null;
				if (hitInfo.hitHurtBox)
				{
					healthComponent = hitInfo.hitHurtBox.healthComponent;
				}
				bool flag = healthComponent && FriendlyFireManager.ShouldDirectHitProceed(healthComponent, attackerTeamIndex);
				if (NetworkServer.active)
				{
					if (flag)
					{
						healthComponent.TakeDamage(damageInfo);
						GlobalEventManager.instance.OnHitEnemy(damageInfo, hitInfo.entityObject);
					}
					GlobalEventManager.instance.OnHitAll(damageInfo, hitInfo.entityObject);
				}
				else if (ClientScene.ready)
				{
					BulletAttack.messageWriter.StartMessage(53);
					int currentLogLevel = LogFilter.currentLogLevel;
					LogFilter.currentLogLevel = 4;
					BulletAttack.messageWriter.Write(entityObject);
					LogFilter.currentLogLevel = currentLogLevel;
					BulletAttack.messageWriter.Write(damageInfo);
					BulletAttack.messageWriter.Write(flag);
					BulletAttack.messageWriter.FinishMessage();
					ClientScene.readyConnection.SendWriter(BulletAttack.messageWriter, QosChannelIndex.defaultReliable.intVal);
				}
			}
			return result;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00065868 File Offset: 0x00063A68
		private static float CalcFalloffFactor(BulletAttack.FalloffModel falloffModel, float distance)
		{
			switch (falloffModel)
			{
			case BulletAttack.FalloffModel.DefaultBullet:
				return 0.5f + Mathf.Clamp01(Mathf.InverseLerp(60f, 25f, distance)) * 0.5f;
			case BulletAttack.FalloffModel.Buckshot:
				return 0.25f + Mathf.Clamp01(Mathf.InverseLerp(25f, 7f, distance)) * 0.75f;
			}
			return 1f;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000658D2 File Offset: 0x00063AD2
		[Obsolete("Use .defaultHitCallback instead.", false)]
		public static BulletAttack.HitCallback DefaultHitCallback
		{
			get
			{
				return BulletAttack.defaultHitCallback;
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000658DC File Offset: 0x00063ADC
		private static bool DefaultFilterCallbackImplementation(BulletAttack bulletAttack, ref BulletAttack.BulletHit hitInfo)
		{
			HurtBox component = hitInfo.collider.GetComponent<HurtBox>();
			return (!component || !component.healthComponent || !(component.healthComponent.gameObject == bulletAttack.weapon)) && hitInfo.entityObject != bulletAttack.weapon;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00065935 File Offset: 0x00063B35
		[Obsolete("Use .defaultFilterCallback instead.", false)]
		public static BulletAttack.FilterCallback DefaultFilterCallback
		{
			get
			{
				return BulletAttack.defaultFilterCallback;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0006593C File Offset: 0x00063B3C
		private void InitBulletHitFromOriginHit(ref BulletAttack.BulletHit bulletHit, Vector3 direction, Collider hitCollider)
		{
			bulletHit.direction = direction;
			bulletHit.point = this.origin;
			bulletHit.surfaceNormal = -direction;
			bulletHit.distance = 0f;
			bulletHit.collider = hitCollider;
			HurtBox component = bulletHit.collider.GetComponent<HurtBox>();
			bulletHit.hitHurtBox = component;
			bulletHit.entityObject = ((component && component.healthComponent) ? component.healthComponent.gameObject : bulletHit.collider.gameObject);
			bulletHit.damageModifier = (component ? component.damageModifier : HurtBox.DamageModifier.Normal);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000659D8 File Offset: 0x00063BD8
		private void InitBulletHitFromRaycastHit(ref BulletAttack.BulletHit bulletHit, Vector3 origin, Vector3 direction, ref RaycastHit raycastHit)
		{
			bulletHit.direction = direction;
			bulletHit.surfaceNormal = raycastHit.normal;
			bulletHit.distance = raycastHit.distance;
			bulletHit.collider = raycastHit.collider;
			bulletHit.point = ((bulletHit.distance == 0f) ? origin : raycastHit.point);
			HurtBox component = bulletHit.collider.GetComponent<HurtBox>();
			bulletHit.hitHurtBox = component;
			bulletHit.entityObject = ((component && component.healthComponent) ? component.healthComponent.gameObject : bulletHit.collider.gameObject);
			bulletHit.damageModifier = (component ? component.damageModifier : HurtBox.DamageModifier.Normal);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00065A8D File Offset: 0x00063C8D
		private bool ProcessHit(ref BulletAttack.BulletHit hitInfo)
		{
			if (!this.filterCallback(this, ref hitInfo))
			{
				return true;
			}
			if (this.sniper)
			{
				hitInfo.isSniperHit = BulletAttack.IsSniperTargetHit(hitInfo);
			}
			return this.hitCallback(this, ref hitInfo);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00065AC4 File Offset: 0x00063CC4
		private static bool IsSniperTargetHit(in BulletAttack.BulletHit hitInfo)
		{
			if (hitInfo.hitHurtBox && hitInfo.hitHurtBox.hurtBoxGroup)
			{
				foreach (HurtBox hurtBox in hitInfo.hitHurtBox.hurtBoxGroup.hurtBoxes)
				{
					if (hurtBox && hurtBox.isSniperTarget && Vector3.ProjectOnPlane(hitInfo.point - hurtBox.transform.position, hitInfo.direction).sqrMagnitude <= HurtBox.sniperTargetRadiusSqr)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00065B58 File Offset: 0x00063D58
		private GameObject ProcessHitList(List<BulletAttack.BulletHit> hits, ref Vector3 endPosition, List<GameObject> ignoreList)
		{
			int count = hits.Count;
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = i;
			}
			for (int j = 0; j < count; j++)
			{
				float num = this.maxDistance;
				int num2 = j;
				for (int k = j; k < count; k++)
				{
					int index = array[k];
					if (hits[index].distance < num)
					{
						num = hits[index].distance;
						num2 = k;
					}
				}
				GameObject entityObject = hits[array[num2]].entityObject;
				if (!ignoreList.Contains(entityObject))
				{
					ignoreList.Add(entityObject);
					BulletAttack.BulletHit bulletHit = hits[array[num2]];
					if (!this.ProcessHit(ref bulletHit))
					{
						endPosition = hits[array[num2]].point;
						return entityObject;
					}
				}
				array[num2] = array[j];
			}
			return null;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00065C34 File Offset: 0x00063E34
		private static GameObject LookUpColliderEntityObject(Collider collider)
		{
			HurtBox component = collider.GetComponent<HurtBox>();
			if (!component || !component.healthComponent)
			{
				return collider.gameObject;
			}
			return component.healthComponent.gameObject;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00065C6F File Offset: 0x00063E6F
		private static Collider[] PhysicsOverlapPoint(Vector3 point, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore)
		{
			return Physics.OverlapBox(point, Vector3.zero, Quaternion.identity, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00065C84 File Offset: 0x00063E84
		public void Fire()
		{
			Vector3[] array = new Vector3[this.bulletCount];
			Vector3 up = Vector3.up;
			Vector3 axis = Vector3.Cross(up, this.aimVector);
			int num = 0;
			while ((long)num < (long)((ulong)this.bulletCount))
			{
				float x = UnityEngine.Random.Range(this.minSpread, this.maxSpread);
				float z = UnityEngine.Random.Range(0f, 360f);
				Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
				float y = vector.y;
				vector.y = 0f;
				float angle = (Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f) * this.spreadYawScale;
				float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f * this.spreadPitchScale;
				array[num] = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * this.aimVector);
				num++;
			}
			int muzzleIndex = -1;
			Vector3 vector2 = this.origin;
			if (!this.weapon)
			{
				this.weapon = this.owner;
			}
			if (this.weapon)
			{
				ModelLocator component = this.weapon.GetComponent<ModelLocator>();
				if (component && component.modelTransform)
				{
					ChildLocator component2 = component.modelTransform.GetComponent<ChildLocator>();
					if (component2)
					{
						muzzleIndex = component2.FindChildIndex(this.muzzleName);
					}
				}
			}
			int num2 = 0;
			while ((long)num2 < (long)((ulong)this.bulletCount))
			{
				this.FireSingle(array[num2], muzzleIndex);
				num2++;
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00065E40 File Offset: 0x00064040
		private void FireSingle(Vector3 normal, int muzzleIndex)
		{
			float maxDistance = this.maxDistance;
			Vector3 vector = this.origin + normal * this.maxDistance;
			List<BulletAttack.BulletHit> list = new List<BulletAttack.BulletHit>();
			bool flag = this.radius == 0f || this.smartCollision;
			bool flag2 = this.radius != 0f;
			HashSet<GameObject> hashSet = null;
			if (this.smartCollision)
			{
				hashSet = new HashSet<GameObject>();
			}
			if (flag)
			{
				RaycastHit[] array = Physics.RaycastAll(this.origin, normal, maxDistance, this.hitMask, this.queryTriggerInteraction);
				float num = float.NegativeInfinity;
				for (int i = 0; i < array.Length; i++)
				{
					BulletAttack.BulletHit bulletHit = default(BulletAttack.BulletHit);
					this.InitBulletHitFromRaycastHit(ref bulletHit, this.origin, normal, ref array[i]);
					list.Add(bulletHit);
					if (this.smartCollision)
					{
						hashSet.Add(bulletHit.entityObject);
					}
					num = ((num < bulletHit.distance) ? bulletHit.distance : num);
				}
				if (num != float.NegativeInfinity)
				{
					maxDistance = num;
				}
			}
			if (flag2)
			{
				LayerMask mask = this.hitMask;
				if (this.smartCollision)
				{
					mask &= ~LayerIndex.world.mask;
				}
				RaycastHit[] array2 = Physics.SphereCastAll(this.origin, this.radius, normal, maxDistance, mask, this.queryTriggerInteraction);
				for (int j = 0; j < array2.Length; j++)
				{
					BulletAttack.BulletHit bulletHit2 = default(BulletAttack.BulletHit);
					this.InitBulletHitFromRaycastHit(ref bulletHit2, this.origin, normal, ref array2[j]);
					if (!this.smartCollision || !hashSet.Contains(bulletHit2.entityObject))
					{
						list.Add(bulletHit2);
					}
				}
			}
			this.ProcessHitList(list, ref vector, new List<GameObject>());
			if (this.tracerEffectPrefab)
			{
				EffectData effectData = new EffectData
				{
					origin = vector,
					start = this.origin
				};
				effectData.SetChildLocatorTransformReference(this.weapon, muzzleIndex);
				EffectManager.SpawnEffect(this.tracerEffectPrefab, effectData, true);
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00066050 File Offset: 0x00064250
		[NetworkMessageHandler(msgType = 53, server = true)]
		private static void HandleBulletDamage(NetworkMessage netMsg)
		{
			NetworkReader reader = netMsg.reader;
			GameObject gameObject = reader.ReadGameObject();
			DamageInfo damageInfo = reader.ReadDamageInfo();
			if (reader.ReadBoolean() && gameObject)
			{
				HealthComponent component = gameObject.GetComponent<HealthComponent>();
				if (component)
				{
					component.TakeDamage(damageInfo);
				}
				GlobalEventManager.instance.OnHitEnemy(damageInfo, gameObject);
			}
			GlobalEventManager.instance.OnHitAll(damageInfo, gameObject);
		}

		// Token: 0x04001CA1 RID: 7329
		private static GameObject sniperTargetHitEffect;

		// Token: 0x04001CA2 RID: 7330
		public GameObject owner;

		// Token: 0x04001CA3 RID: 7331
		public GameObject weapon;

		// Token: 0x04001CA4 RID: 7332
		public float damage = 1f;

		// Token: 0x04001CA5 RID: 7333
		public bool isCrit;

		// Token: 0x04001CA6 RID: 7334
		public float force = 1f;

		// Token: 0x04001CA7 RID: 7335
		public ProcChainMask procChainMask;

		// Token: 0x04001CA8 RID: 7336
		public float procCoefficient = 1f;

		// Token: 0x04001CA9 RID: 7337
		public DamageType damageType;

		// Token: 0x04001CAA RID: 7338
		public DamageColorIndex damageColorIndex;

		// Token: 0x04001CAB RID: 7339
		public bool sniper;

		// Token: 0x04001CAC RID: 7340
		public BulletAttack.FalloffModel falloffModel = BulletAttack.FalloffModel.DefaultBullet;

		// Token: 0x04001CAD RID: 7341
		public GameObject tracerEffectPrefab;

		// Token: 0x04001CAE RID: 7342
		public GameObject hitEffectPrefab;

		// Token: 0x04001CAF RID: 7343
		public string muzzleName = "";

		// Token: 0x04001CB0 RID: 7344
		public bool HitEffectNormal = true;

		// Token: 0x04001CB1 RID: 7345
		public Vector3 origin;

		// Token: 0x04001CB2 RID: 7346
		private Vector3 _aimVector;

		// Token: 0x04001CB3 RID: 7347
		private float _maxDistance = 200f;

		// Token: 0x04001CB4 RID: 7348
		public float radius;

		// Token: 0x04001CB5 RID: 7349
		public uint bulletCount = 1U;

		// Token: 0x04001CB6 RID: 7350
		public float minSpread;

		// Token: 0x04001CB7 RID: 7351
		public float maxSpread;

		// Token: 0x04001CB8 RID: 7352
		public float spreadPitchScale = 1f;

		// Token: 0x04001CB9 RID: 7353
		public float spreadYawScale = 1f;

		// Token: 0x04001CBA RID: 7354
		public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

		// Token: 0x04001CBB RID: 7355
		private static readonly LayerMask defaultHitMask = LayerIndex.world.mask | LayerIndex.entityPrecise.mask;

		// Token: 0x04001CBC RID: 7356
		public LayerMask hitMask = BulletAttack.defaultHitMask;

		// Token: 0x04001CBD RID: 7357
		private static readonly LayerMask defaultStopperMask = BulletAttack.defaultHitMask;

		// Token: 0x04001CBE RID: 7358
		public LayerMask stopperMask = BulletAttack.defaultStopperMask;

		// Token: 0x04001CBF RID: 7359
		public bool smartCollision;

		// Token: 0x04001CC0 RID: 7360
		public BulletAttack.HitCallback hitCallback;

		// Token: 0x04001CC1 RID: 7361
		public static readonly BulletAttack.HitCallback defaultHitCallback = new BulletAttack.HitCallback(BulletAttack.DefaultHitCallbackImplementation);

		// Token: 0x04001CC2 RID: 7362
		public BulletAttack.ModifyOutgoingDamageCallback modifyOutgoingDamageCallback;

		// Token: 0x04001CC3 RID: 7363
		private static NetworkWriter messageWriter = new NetworkWriter();

		// Token: 0x04001CC4 RID: 7364
		public BulletAttack.FilterCallback filterCallback;

		// Token: 0x04001CC5 RID: 7365
		public static readonly BulletAttack.FilterCallback defaultFilterCallback = new BulletAttack.FilterCallback(BulletAttack.DefaultFilterCallbackImplementation);

		// Token: 0x020004F0 RID: 1264
		public enum FalloffModel
		{
			// Token: 0x04001CC7 RID: 7367
			None,
			// Token: 0x04001CC8 RID: 7368
			DefaultBullet,
			// Token: 0x04001CC9 RID: 7369
			Buckshot
		}

		// Token: 0x020004F1 RID: 1265
		// (Invoke) Token: 0x0600170E RID: 5902
		public delegate bool HitCallback(BulletAttack bulletAttack, ref BulletAttack.BulletHit hitInfo);

		// Token: 0x020004F2 RID: 1266
		// (Invoke) Token: 0x06001712 RID: 5906
		public delegate void ModifyOutgoingDamageCallback(BulletAttack bulletAttack, ref BulletAttack.BulletHit hitInfo, DamageInfo damageInfo);

		// Token: 0x020004F3 RID: 1267
		// (Invoke) Token: 0x06001716 RID: 5910
		public delegate bool FilterCallback(BulletAttack bulletAttack, ref BulletAttack.BulletHit hitInfo);

		// Token: 0x020004F4 RID: 1268
		public struct BulletHit
		{
			// Token: 0x04001CCA RID: 7370
			public Vector3 direction;

			// Token: 0x04001CCB RID: 7371
			public Vector3 point;

			// Token: 0x04001CCC RID: 7372
			public Vector3 surfaceNormal;

			// Token: 0x04001CCD RID: 7373
			public float distance;

			// Token: 0x04001CCE RID: 7374
			public Collider collider;

			// Token: 0x04001CCF RID: 7375
			public HurtBox hitHurtBox;

			// Token: 0x04001CD0 RID: 7376
			public GameObject entityObject;

			// Token: 0x04001CD1 RID: 7377
			public HurtBox.DamageModifier damageModifier;

			// Token: 0x04001CD2 RID: 7378
			public bool isSniperHit;
		}
	}
}
