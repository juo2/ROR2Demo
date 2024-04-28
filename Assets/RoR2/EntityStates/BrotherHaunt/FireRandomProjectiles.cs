using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Navigation;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.BrotherHaunt
{
	// Token: 0x0200023C RID: 572
	public class FireRandomProjectiles : BaseState
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00029C6D File Offset: 0x00027E6D
		public override void OnEnter()
		{
			base.OnEnter();
			this.charges = FireRandomProjectiles.initialCharges;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00029C80 File Offset: 0x00027E80
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.chargeTimer -= Time.fixedDeltaTime;
				if (this.chargeTimer <= 0f)
				{
					this.chargeTimer = FireRandomProjectiles.chargeRechargeDuration;
					this.charges = Mathf.Min(this.charges + 1, FireRandomProjectiles.maximumCharges);
				}
				if (UnityEngine.Random.value < FireRandomProjectiles.chanceToFirePerSecond && this.charges > 0)
				{
					this.FireProjectile();
				}
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00029CF8 File Offset: 0x00027EF8
		private void FireProjectile()
		{
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			if (groundNodes)
			{
				List<NodeGraph.NodeIndex> activeNodesForHullMaskWithFlagConditions = groundNodes.GetActiveNodesForHullMaskWithFlagConditions(HullMask.Golem, NodeFlags.None, NodeFlags.NoCharacterSpawn);
				NodeGraph.NodeIndex nodeIndex = activeNodesForHullMaskWithFlagConditions[UnityEngine.Random.Range(0, activeNodesForHullMaskWithFlagConditions.Count)];
				this.charges--;
				Vector3 a;
				groundNodes.GetNodePosition(nodeIndex, out a);
				ProjectileManager.instance.FireProjectile(new FireProjectileInfo
				{
					projectilePrefab = FireRandomProjectiles.projectilePrefab,
					owner = base.gameObject,
					damage = this.damageStat * FireRandomProjectiles.damageCoefficient,
					position = a + Vector3.up * FireRandomProjectiles.projectileVerticalOffset,
					rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f)
				});
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000BBE RID: 3006
		public static GameObject projectilePrefab;

		// Token: 0x04000BBF RID: 3007
		public static float damageCoefficient;

		// Token: 0x04000BC0 RID: 3008
		public static int initialCharges;

		// Token: 0x04000BC1 RID: 3009
		public static int maximumCharges;

		// Token: 0x04000BC2 RID: 3010
		public static float chargeRechargeDuration;

		// Token: 0x04000BC3 RID: 3011
		public static float chanceToFirePerSecond;

		// Token: 0x04000BC4 RID: 3012
		public static float projectileVerticalOffset;

		// Token: 0x04000BC5 RID: 3013
		private int charges;

		// Token: 0x04000BC6 RID: 3014
		private float chargeTimer;
	}
}
