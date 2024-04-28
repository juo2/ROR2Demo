using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006D8 RID: 1752
	public class FireAuraController : NetworkBehaviour
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x00094FD8 File Offset: 0x000931D8
		private void FixedUpdate()
		{
			this.timer += Time.fixedDeltaTime;
			CharacterBody characterBody = null;
			float num = 0f;
			if (this.owner)
			{
				characterBody = this.owner.GetComponent<CharacterBody>();
				num = (characterBody ? Mathf.Lerp(characterBody.radius * 0.5f, characterBody.radius * 6f, 1f - Mathf.Abs(-1f + 2f * this.timer / 8f)) : 0f);
				base.transform.position = this.owner.transform.position;
				base.transform.localScale = new Vector3(num, num, num);
			}
			if (NetworkServer.active)
			{
				if (!this.owner)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
				this.attackStopwatch += Time.fixedDeltaTime;
				if (characterBody && this.attackStopwatch >= 0.25f)
				{
					this.attackStopwatch = 0f;
					BlastAttack blastAttack = new BlastAttack();
					blastAttack.attacker = this.owner;
					blastAttack.inflictor = base.gameObject;
					blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
					blastAttack.position = base.transform.position;
					blastAttack.procCoefficient = 0.1f;
					blastAttack.radius = num;
					blastAttack.baseForce = 0f;
					blastAttack.baseDamage = 1f * characterBody.damage;
					blastAttack.bonusForce = Vector3.zero;
					blastAttack.crit = false;
					blastAttack.damageType = DamageType.Generic;
					blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
					blastAttack.Fire();
				}
				if (this.timer >= 8f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x000951A0 File Offset: 0x000933A0
		// (set) Token: 0x0600228D RID: 8845 RVA: 0x000951B3 File Offset: 0x000933B3
		public GameObject Networkowner
		{
			get
			{
				return this.owner;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.owner, 1U, ref this.___ownerNetId);
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000951D0 File Offset: 0x000933D0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
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
				writer.Write(this.owner);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0009523C File Offset: 0x0009343C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___ownerNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.owner = reader.ReadGameObject();
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0009527D File Offset: 0x0009347D
		public override void PreStartClient()
		{
			if (!this.___ownerNetId.IsEmpty())
			{
				this.Networkowner = ClientScene.FindLocalObject(this.___ownerNetId);
			}
		}

		// Token: 0x0400278D RID: 10125
		private const float fireAttackRadiusMin = 0.5f;

		// Token: 0x0400278E RID: 10126
		private const float fireAttackRadiusMax = 6f;

		// Token: 0x0400278F RID: 10127
		private const float fireDamageCoefficient = 1f;

		// Token: 0x04002790 RID: 10128
		private const float fireProcCoefficient = 0.1f;

		// Token: 0x04002791 RID: 10129
		private const float maxTimer = 8f;

		// Token: 0x04002792 RID: 10130
		private float timer;

		// Token: 0x04002793 RID: 10131
		private float attackStopwatch;

		// Token: 0x04002794 RID: 10132
		[SyncVar]
		public GameObject owner;

		// Token: 0x04002795 RID: 10133
		private NetworkInstanceId ___ownerNetId;
	}
}
