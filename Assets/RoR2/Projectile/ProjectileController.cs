using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RoR2.Audio;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B88 RID: 2952
	[RequireComponent(typeof(TeamFilter))]
	public class ProjectileController : NetworkBehaviour
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x001168DD File Offset: 0x00114ADD
		// (set) Token: 0x06004320 RID: 17184 RVA: 0x001168E5 File Offset: 0x00114AE5
		public TeamFilter teamFilter { get; private set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x001168EE File Offset: 0x00114AEE
		// (set) Token: 0x06004322 RID: 17186 RVA: 0x001168F6 File Offset: 0x00114AF6
		public ProjectileGhostController ghost { get; set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x001168FF File Offset: 0x00114AFF
		// (set) Token: 0x06004324 RID: 17188 RVA: 0x00116907 File Offset: 0x00114B07
		public bool isPrediction { get; set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x00116910 File Offset: 0x00114B10
		// (set) Token: 0x06004326 RID: 17190 RVA: 0x00116918 File Offset: 0x00114B18
		public bool shouldPlaySounds { get; set; }

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06004327 RID: 17191 RVA: 0x00116924 File Offset: 0x00114B24
		// (remove) Token: 0x06004328 RID: 17192 RVA: 0x0011695C File Offset: 0x00114B5C
		public event Action<ProjectileController> onInitialized;

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x00116991 File Offset: 0x00114B91
		// (set) Token: 0x0600432A RID: 17194 RVA: 0x00116999 File Offset: 0x00114B99
		public ProcChainMask procChainMask { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x001169A2 File Offset: 0x00114BA2
		// (set) Token: 0x0600432C RID: 17196 RVA: 0x001169AA File Offset: 0x00114BAA
		public NetworkConnection clientAuthorityOwner { get; private set; }

		// Token: 0x0600432D RID: 17197 RVA: 0x001169B4 File Offset: 0x00114BB4
		private void Awake()
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.myColliders = base.GetComponents<Collider>();
			for (int i = 0; i < this.myColliders.Length; i++)
			{
				this.myColliders[i].enabled = false;
			}
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x00116A08 File Offset: 0x00114C08
		private void Start()
		{
			for (int i = 0; i < this.myColliders.Length; i++)
			{
				this.myColliders[i].enabled = true;
			}
			this.IgnoreCollisionsWithOwner(true);
			if (!this.isPrediction && !NetworkServer.active)
			{
				ProjectileManager.instance.OnClientProjectileReceived(this);
			}
			GameObject gameObject = ProjectileGhostReplacementManager.FindProjectileGhostPrefab(this);
			this.shouldPlaySounds = false;
			if (this.isPrediction || !this.allowPrediction || !base.hasAuthority)
			{
				this.shouldPlaySounds = true;
				if (gameObject)
				{
					Transform transform = base.transform;
					if (this.ghostTransformAnchor)
					{
						transform = this.ghostTransformAnchor;
					}
					this.ghost = UnityEngine.Object.Instantiate<GameObject>(gameObject, transform.position, transform.rotation).GetComponent<ProjectileGhostController>();
					if (this.isPrediction)
					{
						this.ghost.predictionTransform = transform;
					}
					else
					{
						this.ghost.authorityTransform = transform;
					}
					this.ghost.enabled = true;
				}
			}
			this.clientAuthorityOwner = base.GetComponent<NetworkIdentity>().clientAuthorityOwner;
			if (this.shouldPlaySounds)
			{
				PointSoundManager.EmitSoundLocal((AkEventIdArg)this.startSound, base.transform.position);
				if (this.flightSoundLoop)
				{
					Util.PlaySound(this.flightSoundLoop.startSoundName, base.gameObject);
				}
			}
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x00116B4C File Offset: 0x00114D4C
		private void OnDestroy()
		{
			if (NetworkServer.active && ProjectileManager.instance != null)
			{
				ProjectileManager.instance.OnServerProjectileDestroyed(this);
			}
			if (this.shouldPlaySounds && this.flightSoundLoop)
			{
				Util.PlaySound(this.flightSoundLoop.stopSoundName, base.gameObject);
			}
			if (this.ghost && this.isPrediction)
			{
				UnityEngine.Object.Destroy(this.ghost.gameObject);
			}
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x00116BC9 File Offset: 0x00114DC9
		private void OnEnable()
		{
			InstanceTracker.Add<ProjectileController>(this);
			this.IgnoreCollisionsWithOwner(true);
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x00116BD8 File Offset: 0x00114DD8
		private void OnDisable()
		{
			InstanceTracker.Remove<ProjectileController>(this);
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x00116BE0 File Offset: 0x00114DE0
		public void IgnoreCollisionsWithOwner(bool shouldIgnore)
		{
			if (!this.owner)
			{
				return;
			}
			ModelLocator component = this.owner.GetComponent<ModelLocator>();
			if (!component)
			{
				return;
			}
			Transform modelTransform = component.modelTransform;
			if (!modelTransform)
			{
				return;
			}
			HurtBoxGroup component2 = modelTransform.GetComponent<HurtBoxGroup>();
			if (!component2)
			{
				return;
			}
			HurtBox[] hurtBoxes = component2.hurtBoxes;
			for (int i = 0; i < hurtBoxes.Length; i++)
			{
				List<Collider> gameObjectComponents = GetComponentsCache<Collider>.GetGameObjectComponents(hurtBoxes[i].gameObject);
				int j = 0;
				int count = gameObjectComponents.Count;
				while (j < count)
				{
					Collider collider = gameObjectComponents[j];
					for (int k = 0; k < this.myColliders.Length; k++)
					{
						Collider collider2 = this.myColliders[k];
						Physics.IgnoreCollision(collider, collider2, shouldIgnore);
					}
					j++;
				}
				GetComponentsCache<Collider>.ReturnBuffer(gameObjectComponents);
			}
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x00116CB1 File Offset: 0x00114EB1
		private static Vector3 EstimateContactPoint(ContactPoint[] contacts, Collider collider)
		{
			if (contacts.Length == 0)
			{
				return collider.transform.position;
			}
			return contacts[0].point;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x00116CCF File Offset: 0x00114ECF
		private static Vector3 EstimateContactNormal(ContactPoint[] contacts)
		{
			if (contacts.Length == 0)
			{
				return Vector3.zero;
			}
			return contacts[0].normal;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x00116CE8 File Offset: 0x00114EE8
		public void OnCollisionEnter(Collision collision)
		{
			if (NetworkServer.active || this.isPrediction)
			{
				ContactPoint[] contacts = collision.contacts;
				ProjectileImpactInfo impactInfo = new ProjectileImpactInfo
				{
					collider = collision.collider,
					estimatedPointOfImpact = ProjectileController.EstimateContactPoint(contacts, collision.collider),
					estimatedImpactNormal = ProjectileController.EstimateContactNormal(contacts)
				};
				IProjectileImpactBehavior[] components = base.GetComponents<IProjectileImpactBehavior>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].OnProjectileImpact(impactInfo);
				}
			}
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x00116D68 File Offset: 0x00114F68
		public void OnTriggerEnter(Collider collider)
		{
			if (NetworkServer.active && this.canImpactOnTrigger)
			{
				Vector3 vector = Vector3.zero;
				if (this.rigidbody)
				{
					vector = this.rigidbody.velocity;
				}
				ProjectileImpactInfo impactInfo = new ProjectileImpactInfo
				{
					collider = collider,
					estimatedPointOfImpact = base.transform.position,
					estimatedImpactNormal = -vector.normalized
				};
				IProjectileImpactBehavior[] components = base.GetComponents<IProjectileImpactBehavior>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].OnProjectileImpact(impactInfo);
				}
			}
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x00116E00 File Offset: 0x00115000
		public void DispatchOnInitialized()
		{
			Action<ProjectileController> action = this.onInitialized;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x00116E14 File Offset: 0x00115014
		private void OnValidate()
		{
			if (Application.IsPlaying(this))
			{
				return;
			}
			bool localPlayerAuthority = base.GetComponent<NetworkIdentity>().localPlayerAuthority;
			if (this.allowPrediction && !localPlayerAuthority)
			{
				Debug.LogWarningFormat(base.gameObject, "ProjectileController: {0} allows predictions, so it should have localPlayerAuthority=true", new object[]
				{
					base.gameObject
				});
			}
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x00116E84 File Offset: 0x00115084
		// (set) Token: 0x0600433C RID: 17212 RVA: 0x00116E97 File Offset: 0x00115097
		public ushort NetworkpredictionId
		{
			get
			{
				return this.predictionId;
			}
			[param: In]
			set
			{
				base.SetSyncVar<ushort>(value, ref this.predictionId, 1U);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x00116EAC File Offset: 0x001150AC
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x00116EBF File Offset: 0x001150BF
		public GameObject Networkowner
		{
			get
			{
				return this.owner;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.owner, 2U, ref this.___ownerNetId);
			}
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x00116EDC File Offset: 0x001150DC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.predictionId);
				writer.Write(this.owner);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.predictionId);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.owner);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x00116F88 File Offset: 0x00115188
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.predictionId = (ushort)reader.ReadPackedUInt32();
				this.___ownerNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.predictionId = (ushort)reader.ReadPackedUInt32();
			}
			if ((num & 2) != 0)
			{
				this.owner = reader.ReadGameObject();
			}
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x00116FEE File Offset: 0x001151EE
		public override void PreStartClient()
		{
			if (!this.___ownerNetId.IsEmpty())
			{
				this.Networkowner = ClientScene.FindLocalObject(this.___ownerNetId);
			}
		}

		// Token: 0x04004145 RID: 16709
		[HideInInspector]
		[Tooltip("This is assigned to the prefab automatically by ProjectileCatalog at runtime. Do not set this value manually.")]
		public int catalogIndex = -1;

		// Token: 0x04004146 RID: 16710
		[Tooltip("The prefab to instantiate as the visual representation of this projectile. The prefab must have a ProjectileGhostController attached.")]
		public GameObject ghostPrefab;

		// Token: 0x04004147 RID: 16711
		[Tooltip("The transform for the ghost to follow. If null, the transform of this object will be used instead.")]
		public Transform ghostTransformAnchor;

		// Token: 0x04004148 RID: 16712
		[Tooltip("The sound to play on Start(). Use this field to ensure the sound only plays once when prediction creates two instances.")]
		public string startSound;

		// Token: 0x04004149 RID: 16713
		[Tooltip("Prevents this projectile from being deleted by gameplay events, like Captain's defense matrix.")]
		public bool cannotBeDeleted;

		// Token: 0x0400414A RID: 16714
		[SerializeField]
		[Tooltip("The sound loop to play while this object exists. Use this field to ensure the sound only plays once when prediction creates two instances.")]
		private LoopSoundDef flightSoundLoop;

		// Token: 0x0400414B RID: 16715
		private Rigidbody rigidbody;

		// Token: 0x0400414F RID: 16719
		public bool canImpactOnTrigger;

		// Token: 0x04004151 RID: 16721
		public bool allowPrediction = true;

		// Token: 0x04004152 RID: 16722
		[SyncVar]
		[NonSerialized]
		public ushort predictionId;

		// Token: 0x04004153 RID: 16723
		[SyncVar]
		[HideInInspector]
		public GameObject owner;

		// Token: 0x04004157 RID: 16727
		public float procCoefficient = 1f;

		// Token: 0x04004158 RID: 16728
		private Collider[] myColliders;

		// Token: 0x04004159 RID: 16729
		private NetworkInstanceId ___ownerNetId;
	}
}
