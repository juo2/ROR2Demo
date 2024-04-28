using System;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200089D RID: 2205
	[RequireComponent(typeof(NetworkIdentity))]
	public class SkillReloader : MonoBehaviour
	{
		// Token: 0x060030C5 RID: 12485 RVA: 0x000CF38A File Offset: 0x000CD58A
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000CF398 File Offset: 0x000CD598
		private void Start()
		{
			this.timer = 0f;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000CF3A8 File Offset: 0x000CD5A8
		private void FixedUpdate()
		{
			if (Util.HasEffectiveAuthority(this.networkIdentity))
			{
				bool flag = this.stateMachine.state.GetType() == typeof(Idle) && !this.stateMachine.HasPendingState();
				if (this.skill.stock < this.skill.maxStock && flag)
				{
					this.timer += Time.fixedDeltaTime;
				}
				else
				{
					this.timer = 0f;
				}
				if (this.timer >= this.reloadDelay || (this.skill.stock == 0 && flag))
				{
					this.stateMachine.SetNextState(EntityStateCatalog.InstantiateState(this.reloadState));
				}
			}
		}

		// Token: 0x0400326E RID: 12910
		private NetworkIdentity networkIdentity;

		// Token: 0x0400326F RID: 12911
		public GenericSkill skill;

		// Token: 0x04003270 RID: 12912
		public EntityStateMachine stateMachine;

		// Token: 0x04003271 RID: 12913
		public SerializableEntityStateType reloadState;

		// Token: 0x04003272 RID: 12914
		public float reloadDelay = 0.2f;

		// Token: 0x04003273 RID: 12915
		private float timer;
	}
}
