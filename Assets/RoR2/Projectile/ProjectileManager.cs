using System;
using System.Collections.Generic;
using RoR2.Networking;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BA8 RID: 2984
	public class ProjectileManager : MonoBehaviour
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x00119D31 File Offset: 0x00117F31
		// (set) Token: 0x060043CA RID: 17354 RVA: 0x00119D38 File Offset: 0x00117F38
		public static ProjectileManager instance { get; private set; }

		// Token: 0x060043CB RID: 17355 RVA: 0x00119D40 File Offset: 0x00117F40
		private void Awake()
		{
			this.predictionManager = new ProjectileManager.PredictionManager();
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x00119D4D File Offset: 0x00117F4D
		private void OnEnable()
		{
			ProjectileManager.instance = SingletonHelper.Assign<ProjectileManager>(ProjectileManager.instance, this);
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x00119D5F File Offset: 0x00117F5F
		private void OnDisable()
		{
			ProjectileManager.instance = SingletonHelper.Unassign<ProjectileManager>(ProjectileManager.instance, this);
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x00119D71 File Offset: 0x00117F71
		[NetworkMessageHandler(msgType = 49, server = true)]
		private static void HandlePlayerFireProjectile(NetworkMessage netMsg)
		{
			if (ProjectileManager.instance)
			{
				ProjectileManager.instance.HandlePlayerFireProjectileInternal(netMsg);
			}
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x00119D8A File Offset: 0x00117F8A
		[NetworkMessageHandler(msgType = 50, client = true)]
		private static void HandleReleaseProjectilePredictionId(NetworkMessage netMsg)
		{
			if (ProjectileManager.instance)
			{
				ProjectileManager.instance.HandleReleaseProjectilePredictionIdInternal(netMsg);
			}
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x00119DA4 File Offset: 0x00117FA4
		public void FireProjectile(GameObject prefab, Vector3 position, Quaternion rotation, GameObject owner, float damage, float force, bool crit, DamageColorIndex damageColorIndex = DamageColorIndex.Default, GameObject target = null, float speedOverride = -1f)
		{
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = prefab,
				position = position,
				rotation = rotation,
				owner = owner,
				damage = damage,
				force = force,
				crit = crit,
				damageColorIndex = damageColorIndex,
				target = target,
				speedOverride = speedOverride,
				fuseOverride = -1f,
				damageTypeOverride = null
			};
			this.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x00119E32 File Offset: 0x00118032
		public void FireProjectile(FireProjectileInfo fireProjectileInfo)
		{
			if (NetworkServer.active)
			{
				this.FireProjectileServer(fireProjectileInfo, null, 0, 0.0);
				return;
			}
			this.FireProjectileClient(fireProjectileInfo, NetworkManager.singleton.client);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x00119E60 File Offset: 0x00118060
		private void FireProjectileClient(FireProjectileInfo fireProjectileInfo, NetworkClient client)
		{
			int projectileIndex = ProjectileCatalog.GetProjectileIndex(fireProjectileInfo.projectilePrefab);
			if (projectileIndex == -1)
			{
				Debug.LogErrorFormat(fireProjectileInfo.projectilePrefab, "Prefab {0} is not a registered projectile prefab.", new object[]
				{
					fireProjectileInfo.projectilePrefab
				});
				return;
			}
			bool allowPrediction = ProjectileCatalog.GetProjectilePrefabProjectileControllerComponent(projectileIndex).allowPrediction;
			ushort predictionId = 0;
			if (allowPrediction)
			{
				ProjectileController component = UnityEngine.Object.Instantiate<GameObject>(fireProjectileInfo.projectilePrefab, fireProjectileInfo.position, fireProjectileInfo.rotation).GetComponent<ProjectileController>();
				ProjectileManager.InitializeProjectile(component, fireProjectileInfo);
				this.predictionManager.RegisterPrediction(component);
				predictionId = component.predictionId;
			}
			this.fireMsg.sendTime = (double)Run.instance.time;
			this.fireMsg.prefabIndexPlusOne = Util.IntToUintPlusOne(projectileIndex);
			this.fireMsg.position = fireProjectileInfo.position;
			this.fireMsg.rotation = fireProjectileInfo.rotation;
			this.fireMsg.owner = fireProjectileInfo.owner;
			this.fireMsg.predictionId = predictionId;
			this.fireMsg.damage = fireProjectileInfo.damage;
			this.fireMsg.force = fireProjectileInfo.force;
			this.fireMsg.crit = fireProjectileInfo.crit;
			this.fireMsg.damageColorIndex = fireProjectileInfo.damageColorIndex;
			this.fireMsg.speedOverride = fireProjectileInfo.speedOverride;
			this.fireMsg.fuseOverride = fireProjectileInfo.fuseOverride;
			this.fireMsg.useDamageTypeOverride = (fireProjectileInfo.damageTypeOverride != null);
			this.fireMsg.damageTypeOverride = (fireProjectileInfo.damageTypeOverride ?? DamageType.Generic);
			if (fireProjectileInfo.target)
			{
				HurtBox component2 = fireProjectileInfo.target.GetComponent<HurtBox>();
				this.fireMsg.target = (component2 ? HurtBoxReference.FromHurtBox(component2) : HurtBoxReference.FromRootObject(fireProjectileInfo.target));
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(49);
			networkWriter.Write(this.fireMsg);
			networkWriter.FinishMessage();
			client.SendWriter(networkWriter, 0);
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x0011A05C File Offset: 0x0011825C
		private static void InitializeProjectile(ProjectileController projectileController, FireProjectileInfo fireProjectileInfo)
		{
			GameObject gameObject = projectileController.gameObject;
			ProjectileDamage component = gameObject.GetComponent<ProjectileDamage>();
			TeamFilter component2 = gameObject.GetComponent<TeamFilter>();
			ProjectileNetworkTransform component3 = gameObject.GetComponent<ProjectileNetworkTransform>();
			ProjectileTargetComponent component4 = gameObject.GetComponent<ProjectileTargetComponent>();
			ProjectileSimple component5 = gameObject.GetComponent<ProjectileSimple>();
			projectileController.Networkowner = fireProjectileInfo.owner;
			projectileController.procChainMask = fireProjectileInfo.procChainMask;
			if (component2)
			{
				component2.teamIndex = TeamComponent.GetObjectTeam(fireProjectileInfo.owner);
			}
			if (component3)
			{
				component3.SetValuesFromTransform();
			}
			if (component4 && fireProjectileInfo.target)
			{
				CharacterBody component6 = fireProjectileInfo.target.GetComponent<CharacterBody>();
				component4.target = (component6 ? component6.coreTransform : fireProjectileInfo.target.transform);
			}
			if (fireProjectileInfo.useSpeedOverride && component5)
			{
				component5.desiredForwardSpeed = fireProjectileInfo.speedOverride;
			}
			if (fireProjectileInfo.useFuseOverride)
			{
				ProjectileImpactExplosion component7 = gameObject.GetComponent<ProjectileImpactExplosion>();
				if (component7)
				{
					component7.lifetime = fireProjectileInfo.fuseOverride;
				}
				ProjectileFuse component8 = gameObject.GetComponent<ProjectileFuse>();
				if (component8)
				{
					component8.fuse = fireProjectileInfo.fuseOverride;
				}
			}
			if (component)
			{
				component.damage = fireProjectileInfo.damage;
				component.force = fireProjectileInfo.force;
				component.crit = fireProjectileInfo.crit;
				component.damageColorIndex = fireProjectileInfo.damageColorIndex;
				if (fireProjectileInfo.damageTypeOverride != null)
				{
					component.damageType = fireProjectileInfo.damageTypeOverride.Value;
				}
			}
			projectileController.DispatchOnInitialized();
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x0011A1E0 File Offset: 0x001183E0
		private void FireProjectileServer(FireProjectileInfo fireProjectileInfo, NetworkConnection clientAuthorityOwner = null, ushort predictionId = 0, double fastForwardTime = 0.0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(fireProjectileInfo.projectilePrefab, fireProjectileInfo.position, fireProjectileInfo.rotation);
			ProjectileController component = gameObject.GetComponent<ProjectileController>();
			component.NetworkpredictionId = predictionId;
			ProjectileManager.InitializeProjectile(component, fireProjectileInfo);
			NetworkIdentity component2 = gameObject.GetComponent<NetworkIdentity>();
			if (clientAuthorityOwner != null && component2.localPlayerAuthority)
			{
				NetworkServer.SpawnWithClientAuthority(gameObject, clientAuthorityOwner);
				return;
			}
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x0011A23C File Offset: 0x0011843C
		public void OnServerProjectileDestroyed(ProjectileController projectile)
		{
			if (projectile.predictionId != 0)
			{
				NetworkConnection clientAuthorityOwner = projectile.clientAuthorityOwner;
				if (clientAuthorityOwner != null)
				{
					this.ReleasePredictionId(clientAuthorityOwner, projectile.predictionId);
				}
			}
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x0011A268 File Offset: 0x00118468
		public void OnClientProjectileReceived(ProjectileController projectile)
		{
			if (projectile.predictionId != 0 && projectile.hasAuthority)
			{
				this.predictionManager.OnAuthorityProjectileReceived(projectile);
			}
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x0011A288 File Offset: 0x00118488
		private void ReleasePredictionId(NetworkConnection owner, ushort predictionId)
		{
			this.releasePredictionIdMsg.predictionId = predictionId;
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(50);
			networkWriter.Write(this.releasePredictionIdMsg);
			networkWriter.FinishMessage();
			owner.SendWriter(networkWriter, 0);
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x0011A2CC File Offset: 0x001184CC
		private void HandlePlayerFireProjectileInternal(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ProjectileManager.PlayerFireProjectileMessage>(this.fireMsg);
			GameObject projectilePrefab = ProjectileCatalog.GetProjectilePrefab(Util.UintToIntMinusOne(this.fireMsg.prefabIndexPlusOne));
			if (projectilePrefab == null)
			{
				this.ReleasePredictionId(netMsg.conn, this.fireMsg.predictionId);
				return;
			}
			this.FireProjectileServer(new FireProjectileInfo
			{
				projectilePrefab = projectilePrefab,
				position = this.fireMsg.position,
				rotation = this.fireMsg.rotation,
				owner = this.fireMsg.owner,
				damage = this.fireMsg.damage,
				force = this.fireMsg.force,
				crit = this.fireMsg.crit,
				target = this.fireMsg.target.ResolveGameObject(),
				damageColorIndex = this.fireMsg.damageColorIndex,
				speedOverride = this.fireMsg.speedOverride,
				fuseOverride = this.fireMsg.fuseOverride,
				damageTypeOverride = (this.fireMsg.useDamageTypeOverride ? new DamageType?(this.fireMsg.damageTypeOverride) : null)
			}, netMsg.conn, this.fireMsg.predictionId, (double)Run.instance.time - this.fireMsg.sendTime);
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0011A443 File Offset: 0x00118643
		private void HandleReleaseProjectilePredictionIdInternal(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ProjectileManager.ReleasePredictionIdMessage>(this.releasePredictionIdMsg);
			this.predictionManager.ReleasePredictionId(this.releasePredictionIdMsg.predictionId);
		}

		// Token: 0x04004243 RID: 16963
		private ProjectileManager.PredictionManager predictionManager;

		// Token: 0x04004244 RID: 16964
		private ProjectileManager.PlayerFireProjectileMessage fireMsg = new ProjectileManager.PlayerFireProjectileMessage();

		// Token: 0x04004245 RID: 16965
		private ProjectileManager.ReleasePredictionIdMessage releasePredictionIdMsg = new ProjectileManager.ReleasePredictionIdMessage();

		// Token: 0x02000BA9 RID: 2985
		private class PlayerFireProjectileMessage : MessageBase
		{
			// Token: 0x060043DC RID: 17372 RVA: 0x0011A488 File Offset: 0x00118688
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.sendTime);
				writer.WritePackedUInt32(this.prefabIndexPlusOne);
				writer.Write(this.position);
				writer.Write(this.rotation);
				writer.Write(this.owner);
				GeneratedNetworkCode._WriteHurtBoxReference_None(writer, this.target);
				writer.Write(this.damage);
				writer.Write(this.force);
				writer.Write(this.crit);
				writer.WritePackedUInt32((uint)this.predictionId);
				writer.Write((int)this.damageColorIndex);
				writer.Write(this.speedOverride);
				writer.Write(this.fuseOverride);
				writer.Write(this.useDamageTypeOverride);
				writer.Write((int)this.damageTypeOverride);
			}

			// Token: 0x060043DD RID: 17373 RVA: 0x0011A54C File Offset: 0x0011874C
			public override void Deserialize(NetworkReader reader)
			{
				this.sendTime = reader.ReadDouble();
				this.prefabIndexPlusOne = reader.ReadPackedUInt32();
				this.position = reader.ReadVector3();
				this.rotation = reader.ReadQuaternion();
				this.owner = reader.ReadGameObject();
				this.target = GeneratedNetworkCode._ReadHurtBoxReference_None(reader);
				this.damage = reader.ReadSingle();
				this.force = reader.ReadSingle();
				this.crit = reader.ReadBoolean();
				this.predictionId = (ushort)reader.ReadPackedUInt32();
				this.damageColorIndex = (DamageColorIndex)reader.ReadInt32();
				this.speedOverride = reader.ReadSingle();
				this.fuseOverride = reader.ReadSingle();
				this.useDamageTypeOverride = reader.ReadBoolean();
				this.damageTypeOverride = (DamageType)reader.ReadInt32();
			}

			// Token: 0x04004246 RID: 16966
			public double sendTime;

			// Token: 0x04004247 RID: 16967
			public uint prefabIndexPlusOne;

			// Token: 0x04004248 RID: 16968
			public Vector3 position;

			// Token: 0x04004249 RID: 16969
			public Quaternion rotation;

			// Token: 0x0400424A RID: 16970
			public GameObject owner;

			// Token: 0x0400424B RID: 16971
			public HurtBoxReference target;

			// Token: 0x0400424C RID: 16972
			public float damage;

			// Token: 0x0400424D RID: 16973
			public float force;

			// Token: 0x0400424E RID: 16974
			public bool crit;

			// Token: 0x0400424F RID: 16975
			public ushort predictionId;

			// Token: 0x04004250 RID: 16976
			public DamageColorIndex damageColorIndex;

			// Token: 0x04004251 RID: 16977
			public float speedOverride;

			// Token: 0x04004252 RID: 16978
			public float fuseOverride;

			// Token: 0x04004253 RID: 16979
			public bool useDamageTypeOverride;

			// Token: 0x04004254 RID: 16980
			public DamageType damageTypeOverride;
		}

		// Token: 0x02000BAA RID: 2986
		private class ReleasePredictionIdMessage : MessageBase
		{
			// Token: 0x060043DF RID: 17375 RVA: 0x0011A60D File Offset: 0x0011880D
			public override void Serialize(NetworkWriter writer)
			{
				writer.WritePackedUInt32((uint)this.predictionId);
			}

			// Token: 0x060043E0 RID: 17376 RVA: 0x0011A61B File Offset: 0x0011881B
			public override void Deserialize(NetworkReader reader)
			{
				this.predictionId = (ushort)reader.ReadPackedUInt32();
			}

			// Token: 0x04004255 RID: 16981
			public ushort predictionId;
		}

		// Token: 0x02000BAB RID: 2987
		private class PredictionManager
		{
			// Token: 0x060043E1 RID: 17377 RVA: 0x0011A629 File Offset: 0x00118829
			public ProjectileController FindPredictedProjectileController(ushort predictionId)
			{
				return this.predictions[predictionId];
			}

			// Token: 0x060043E2 RID: 17378 RVA: 0x0011A638 File Offset: 0x00118838
			public void OnAuthorityProjectileReceived(ProjectileController authoritativeProjectile)
			{
				ProjectileController projectileController;
				if (authoritativeProjectile.hasAuthority && authoritativeProjectile.predictionId != 0 && this.predictions.TryGetValue(authoritativeProjectile.predictionId, out projectileController))
				{
					authoritativeProjectile.ghost = projectileController.ghost;
					if (authoritativeProjectile.ghost)
					{
						authoritativeProjectile.ghost.authorityTransform = authoritativeProjectile.transform;
					}
				}
			}

			// Token: 0x060043E3 RID: 17379 RVA: 0x0011A694 File Offset: 0x00118894
			public void ReleasePredictionId(ushort predictionId)
			{
				ProjectileController projectileController = this.predictions[predictionId];
				this.predictions.Remove(predictionId);
				if (projectileController && projectileController.gameObject)
				{
					UnityEngine.Object.Destroy(projectileController.gameObject);
				}
			}

			// Token: 0x060043E4 RID: 17380 RVA: 0x0011A6DB File Offset: 0x001188DB
			public void RegisterPrediction(ProjectileController predictedProjectile)
			{
				predictedProjectile.NetworkpredictionId = this.RequestPredictionId();
				this.predictions[predictedProjectile.predictionId] = predictedProjectile;
				predictedProjectile.isPrediction = true;
			}

			// Token: 0x060043E5 RID: 17381 RVA: 0x0011A704 File Offset: 0x00118904
			private ushort RequestPredictionId()
			{
				for (ushort num = 1; num < 32767; num += 1)
				{
					if (!this.predictions.ContainsKey(num))
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x04004256 RID: 16982
			private Dictionary<ushort, ProjectileController> predictions = new Dictionary<ushort, ProjectileController>();
		}
	}
}
