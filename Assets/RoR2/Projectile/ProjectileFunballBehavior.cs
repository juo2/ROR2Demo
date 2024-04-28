using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B91 RID: 2961
	[RequireComponent(typeof(TeamFilter))]
	[Obsolete("This component is deprecated and will likely be removed from future releases.", false)]
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileFunballBehavior : NetworkBehaviour
	{
		// Token: 0x06004369 RID: 17257 RVA: 0x0011807A File Offset: 0x0011627A
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x00118088 File Offset: 0x00116288
		private void Start()
		{
			this.Networktimer = -1f;
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x00118098 File Offset: 0x00116298
		private void FixedUpdate()
		{
			if (NetworkServer.active && this.fuseStarted)
			{
				this.Networktimer = this.timer + Time.fixedDeltaTime;
				if (this.timer >= this.duration)
				{
					EffectManager.SpawnEffect(this.explosionPrefab, new EffectData
					{
						origin = base.transform.position,
						scale = this.blastRadius
					}, true);
					new BlastAttack
					{
						attacker = this.projectileController.owner,
						inflictor = base.gameObject,
						teamIndex = this.projectileController.teamFilter.teamIndex,
						position = base.transform.position,
						procChainMask = this.projectileController.procChainMask,
						procCoefficient = this.projectileController.procCoefficient,
						radius = this.blastRadius,
						baseDamage = this.blastDamage,
						baseForce = this.blastForce,
						bonusForce = Vector3.zero,
						crit = false,
						damageType = DamageType.Generic
					}.Fire();
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x001181C4 File Offset: 0x001163C4
		private void OnCollisionEnter(Collision collision)
		{
			this.fuseStarted = true;
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x001181F8 File Offset: 0x001163F8
		// (set) Token: 0x06004370 RID: 17264 RVA: 0x0011820B File Offset: 0x0011640B
		public float Networktimer
		{
			get
			{
				return this.timer;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.timer, 1U);
			}
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x00118220 File Offset: 0x00116420
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.timer);
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
				writer.Write(this.timer);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x0011828C File Offset: 0x0011648C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.timer = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.timer = reader.ReadSingle();
			}
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040041BD RID: 16829
		[Tooltip("The effect to use for the explosion.")]
		public GameObject explosionPrefab;

		// Token: 0x040041BE RID: 16830
		[Tooltip("How many seconds until detonation.")]
		public float duration;

		// Token: 0x040041BF RID: 16831
		[Tooltip("Radius of blast in meters.")]
		public float blastRadius = 1f;

		// Token: 0x040041C0 RID: 16832
		[Tooltip("Maximum damage of blast.")]
		public float blastDamage = 1f;

		// Token: 0x040041C1 RID: 16833
		[Tooltip("Force of blast.")]
		public float blastForce = 1f;

		// Token: 0x040041C2 RID: 16834
		private ProjectileController projectileController;

		// Token: 0x040041C3 RID: 16835
		[SyncVar]
		private float timer;

		// Token: 0x040041C4 RID: 16836
		private bool fuseStarted;
	}
}
