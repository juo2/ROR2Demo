using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000719 RID: 1817
	[RequireComponent(typeof(TeamFilter))]
	public class HealingWard : NetworkBehaviour
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x000A2BB0 File Offset: 0x000A0DB0
		private void Awake()
		{
			this.teamFilter = base.GetComponent<TeamFilter>();
			RaycastHit raycastHit;
			if (NetworkServer.active && this.floorWard && Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, 500f, LayerIndex.world.mask))
			{
				base.transform.position = raycastHit.point;
				base.transform.up = raycastHit.normal;
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000A2C2C File Offset: 0x000A0E2C
		private void Update()
		{
			if (this.rangeIndicator)
			{
				float num = Mathf.SmoothDamp(this.rangeIndicator.localScale.x, this.radius, ref this.rangeIndicatorScaleVelocity, 0.2f);
				this.rangeIndicator.localScale = new Vector3(num, num, num);
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000A2C80 File Offset: 0x000A0E80
		private void FixedUpdate()
		{
			this.healTimer -= Time.fixedDeltaTime;
			if (this.healTimer <= 0f && NetworkServer.active)
			{
				this.healTimer = this.interval;
				this.HealOccupants();
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000A2CBC File Offset: 0x000A0EBC
		private void HealOccupants()
		{
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(this.teamFilter.teamIndex);
			float num = this.radius * this.radius;
			Vector3 position = base.transform.position;
			for (int i = 0; i < teamMembers.Count; i++)
			{
				if ((teamMembers[i].transform.position - position).sqrMagnitude <= num)
				{
					HealthComponent component = teamMembers[i].GetComponent<HealthComponent>();
					if (component)
					{
						float num2 = this.healPoints + component.fullHealth * this.healFraction;
						if (num2 > 0f)
						{
							component.Heal(num2, default(ProcChainMask), true);
						}
					}
				}
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000A2D88 File Offset: 0x000A0F88
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000A2D9B File Offset: 0x000A0F9B
		public float Networkradius
		{
			get
			{
				return this.radius;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.radius, 1U);
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000A2DB0 File Offset: 0x000A0FB0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.radius);
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
				writer.Write(this.radius);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000A2E1C File Offset: 0x000A101C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.radius = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.radius = reader.ReadSingle();
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400296E RID: 10606
		[SyncVar]
		[Tooltip("The area of effect.")]
		public float radius;

		// Token: 0x0400296F RID: 10607
		[Tooltip("How long between heal pulses in the area of effect.")]
		public float interval = 1f;

		// Token: 0x04002970 RID: 10608
		[Tooltip("How many hit points to restore each pulse.")]
		public float healPoints;

		// Token: 0x04002971 RID: 10609
		[Tooltip("What fraction of the healee max health to restore each pulse.")]
		public float healFraction;

		// Token: 0x04002972 RID: 10610
		[Tooltip("The child range indicator object. Will be scaled to the radius.")]
		public Transform rangeIndicator;

		// Token: 0x04002973 RID: 10611
		[Tooltip("Should the ward be floored on start")]
		public bool floorWard;

		// Token: 0x04002974 RID: 10612
		private TeamFilter teamFilter;

		// Token: 0x04002975 RID: 10613
		private float healTimer;

		// Token: 0x04002976 RID: 10614
		private float rangeIndicatorScaleVelocity;
	}
}
