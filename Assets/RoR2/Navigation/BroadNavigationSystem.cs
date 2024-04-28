using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B33 RID: 2867
	public abstract class BroadNavigationSystem : IDisposable
	{
		// Token: 0x0600413F RID: 16703 RVA: 0x0010E879 File Offset: 0x0010CA79
		static BroadNavigationSystem()
		{
			RoR2Application.onFixedUpdate += BroadNavigationSystem.StaticUpdate;
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x0010E898 File Offset: 0x0010CA98
		private static void StaticUpdate()
		{
			if (Time.fixedDeltaTime == 0f)
			{
				return;
			}
			foreach (BroadNavigationSystem broadNavigationSystem in BroadNavigationSystem.instancesList)
			{
				broadNavigationSystem.Update(Time.fixedDeltaTime);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x0010E8FC File Offset: 0x0010CAFC
		// (set) Token: 0x06004142 RID: 16706 RVA: 0x0010E904 File Offset: 0x0010CB04
		private protected int agentCount { protected get; private set; }

		// Token: 0x06004143 RID: 16707 RVA: 0x0010E90D File Offset: 0x0010CB0D
		public BroadNavigationSystem()
		{
			BroadNavigationSystem.instancesList.Add(this);
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x0010E944 File Offset: 0x0010CB44
		public virtual void Dispose()
		{
			BroadNavigationSystem.instancesList.Remove(this);
			for (BroadNavigationSystem.AgentHandle agentHandle = (BroadNavigationSystem.AgentHandle)0; agentHandle < (BroadNavigationSystem.AgentHandle)this.allAgentData.Length; agentHandle++)
			{
				if (this.allAgentData[(int)agentHandle].isValid)
				{
					this.DestroyAgentInternal(agentHandle);
				}
			}
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x0010E98C File Offset: 0x0010CB8C
		public void RequestAgent(ref BroadNavigationSystem.Agent agent)
		{
			BroadNavigationSystem.AgentHandle agentHandle = (BroadNavigationSystem.AgentHandle)this.agentIndexAllocator.RequestIndex();
			this.CreateAgentInternal(agentHandle);
			agent = this.GetAgent(agentHandle);
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0010E9BA File Offset: 0x0010CBBA
		public void ReturnAgent(ref BroadNavigationSystem.Agent agent)
		{
			this.DestroyAgentInternal(agent.handle);
			this.agentIndexAllocator.FreeIndex((int)agent.handle);
			agent = BroadNavigationSystem.Agent.invalid;
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0010E9E4 File Offset: 0x0010CBE4
		public BroadNavigationSystem.Agent GetAgent(BroadNavigationSystem.AgentHandle agentHandle)
		{
			return new BroadNavigationSystem.Agent(this, agentHandle);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x0010E9ED File Offset: 0x0010CBED
		public bool IsValidHandle(in BroadNavigationSystem.AgentHandle handle)
		{
			return ArrayUtils.IsInBounds<BroadNavigationSystem.BaseAgentData>(this.allAgentData, (int)handle) && this.allAgentData[(int)handle].isValid;
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0010EA12 File Offset: 0x0010CC12
		public void GetAgentOutput(in BroadNavigationSystem.AgentHandle handle, out BroadNavigationSystem.AgentOutput output)
		{
			output = this.agentOutputs[(int)handle];
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x0010EA27 File Offset: 0x0010CC27
		private void Update(float deltaTime)
		{
			this.localTime += deltaTime;
			this.UpdateAgentsInternal(deltaTime);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x0010EA40 File Offset: 0x0010CC40
		private void CreateAgentInternal(in BroadNavigationSystem.AgentHandle index)
		{
			int agentCount = this.agentCount + 1;
			this.agentCount = agentCount;
			ArrayUtils.EnsureCapacity<BroadNavigationSystem.BaseAgentData>(ref this.allAgentData, this.agentCount);
			ArrayUtils.EnsureCapacity<BroadNavigationSystem.AgentOutput>(ref this.agentOutputs, this.agentCount);
			BroadNavigationSystem.BaseAgentData[] array = this.allAgentData;
			BroadNavigationSystem.AgentHandle agentHandle = index;
			array[(int)agentHandle].isValid = true;
			array[(int)agentHandle].localTime = 0f;
			this.CreateAgent(index);
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x0010EAA4 File Offset: 0x0010CCA4
		private void DestroyAgentInternal(in BroadNavigationSystem.AgentHandle index)
		{
			this.DestroyAgent(index);
			this.allAgentData[(int)index] = default(BroadNavigationSystem.BaseAgentData);
			this.agentOutputs[(int)index] = default(BroadNavigationSystem.AgentOutput);
			int agentCount = this.agentCount - 1;
			this.agentCount = agentCount;
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x0010EAF0 File Offset: 0x0010CCF0
		private void UpdateAgentsInternal(float deltaTime)
		{
			for (BroadNavigationSystem.AgentHandle agentHandle = (BroadNavigationSystem.AgentHandle)0; agentHandle < (BroadNavigationSystem.AgentHandle)this.allAgentData.Length; agentHandle++)
			{
				ref BroadNavigationSystem.BaseAgentData ptr = ref this.allAgentData[(int)agentHandle];
				if (ptr.enabled)
				{
					ptr.localTime += deltaTime;
				}
			}
			this.UpdateAgents(deltaTime);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x0010EB38 File Offset: 0x0010CD38
		private void ConfigureAgentFromBodyInternal(in BroadNavigationSystem.AgentHandle index, CharacterBody body)
		{
			ref BroadNavigationSystem.BaseAgentData ptr = ref this.allAgentData[(int)index];
			ptr.maxWalkSpeed = 0f;
			ptr.maxSlopeAngle = 0f;
			ptr.maxJumpHeight = 0f;
			ptr.currentPosition = null;
			if (body)
			{
				ptr.maxWalkSpeed = body.moveSpeed;
				ref BroadNavigationSystem.BaseAgentData ptr2 = ref ptr;
				CharacterMotor characterMotor = body.characterMotor;
				ptr2.maxSlopeAngle = ((characterMotor != null) ? characterMotor.Motor.MaxStableSlopeAngle : 0f);
				ptr.maxJumpHeight = body.maxJumpHeight;
				ptr.currentPosition = new Vector3?(body.footPosition);
			}
			this.ConfigureAgentFromBody(index, body);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x0010EBDA File Offset: 0x0010CDDA
		private void InvalidateAgentPathInternal(in BroadNavigationSystem.AgentHandle index)
		{
			this.InvalidateAgentPath(index);
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0010EBE3 File Offset: 0x0010CDE3
		protected ref readonly BroadNavigationSystem.BaseAgentData GetAgentData(in BroadNavigationSystem.AgentHandle agentHandle)
		{
			return ref this.allAgentData[(int)agentHandle];
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x0010EBF2 File Offset: 0x0010CDF2
		protected void SetAgentOutput(in BroadNavigationSystem.AgentHandle agentHandle, in BroadNavigationSystem.AgentOutput output)
		{
			BroadNavigationSystem.AgentOutput[] array = this.agentOutputs;
			BroadNavigationSystem.AgentHandle agentHandle2 = agentHandle;
			array[(int)agentHandle2] = output;
			array[(int)agentHandle2].time = this.localTime;
		}

		// Token: 0x06004152 RID: 16722
		protected abstract void CreateAgent(in BroadNavigationSystem.AgentHandle index);

		// Token: 0x06004153 RID: 16723
		protected abstract void DestroyAgent(in BroadNavigationSystem.AgentHandle index);

		// Token: 0x06004154 RID: 16724
		protected abstract void UpdateAgents(float deltaTime);

		// Token: 0x06004155 RID: 16725
		protected abstract void ConfigureAgentFromBody(in BroadNavigationSystem.AgentHandle index, CharacterBody body);

		// Token: 0x06004156 RID: 16726
		protected abstract void InvalidateAgentPath(in BroadNavigationSystem.AgentHandle index);

		// Token: 0x04003FAC RID: 16300
		private static readonly List<BroadNavigationSystem> instancesList = new List<BroadNavigationSystem>();

		// Token: 0x04003FAD RID: 16301
		private IndexAllocator agentIndexAllocator = new IndexAllocator();

		// Token: 0x04003FAF RID: 16303
		private BroadNavigationSystem.BaseAgentData[] allAgentData = Array.Empty<BroadNavigationSystem.BaseAgentData>();

		// Token: 0x04003FB0 RID: 16304
		private BroadNavigationSystem.AgentOutput[] agentOutputs = Array.Empty<BroadNavigationSystem.AgentOutput>();

		// Token: 0x04003FB1 RID: 16305
		protected float localTime;

		// Token: 0x02000B34 RID: 2868
		public enum AgentHandle
		{
			// Token: 0x04003FB3 RID: 16307
			None = -1
		}

		// Token: 0x02000B35 RID: 2869
		protected struct BaseAgentData
		{
			// Token: 0x04003FB4 RID: 16308
			public bool enabled;

			// Token: 0x04003FB5 RID: 16309
			public bool isValid;

			// Token: 0x04003FB6 RID: 16310
			public float localTime;

			// Token: 0x04003FB7 RID: 16311
			public Vector3? currentPosition;

			// Token: 0x04003FB8 RID: 16312
			public Vector3? goalPosition;

			// Token: 0x04003FB9 RID: 16313
			public float maxWalkSpeed;

			// Token: 0x04003FBA RID: 16314
			public float maxSlopeAngle;

			// Token: 0x04003FBB RID: 16315
			public float maxJumpHeight;
		}

		// Token: 0x02000B36 RID: 2870
		public struct AgentOutput
		{
			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06004157 RID: 16727 RVA: 0x0010EC18 File Offset: 0x0010CE18
			// (set) Token: 0x06004158 RID: 16728 RVA: 0x0010EC20 File Offset: 0x0010CE20
			public float desiredJumpVelocity { get; set; }

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06004159 RID: 16729 RVA: 0x0010EC29 File Offset: 0x0010CE29
			// (set) Token: 0x0600415A RID: 16730 RVA: 0x0010EC31 File Offset: 0x0010CE31
			public Vector3? nextPosition { get; set; }

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x0600415B RID: 16731 RVA: 0x0010EC3A File Offset: 0x0010CE3A
			// (set) Token: 0x0600415C RID: 16732 RVA: 0x0010EC42 File Offset: 0x0010CE42
			public float lastPathUpdate { get; set; }

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x0600415D RID: 16733 RVA: 0x0010EC4B File Offset: 0x0010CE4B
			// (set) Token: 0x0600415E RID: 16734 RVA: 0x0010EC53 File Offset: 0x0010CE53
			public bool targetReachable { get; set; }

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x0600415F RID: 16735 RVA: 0x0010EC5C File Offset: 0x0010CE5C
			// (set) Token: 0x06004160 RID: 16736 RVA: 0x0010EC64 File Offset: 0x0010CE64
			public float time { get; set; }
		}

		// Token: 0x02000B37 RID: 2871
		public readonly struct Agent
		{
			// Token: 0x06004161 RID: 16737 RVA: 0x0010EC6D File Offset: 0x0010CE6D
			public Agent(BroadNavigationSystem system, BroadNavigationSystem.AgentHandle handle)
			{
				this.handle = handle;
				this.system = system;
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06004162 RID: 16738 RVA: 0x0010EC7D File Offset: 0x0010CE7D
			private ref BroadNavigationSystem.BaseAgentData agentData
			{
				get
				{
					return ref this.system.allAgentData[(int)this.handle];
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06004163 RID: 16739 RVA: 0x0010EC95 File Offset: 0x0010CE95
			// (set) Token: 0x06004164 RID: 16740 RVA: 0x0010ECA2 File Offset: 0x0010CEA2
			public bool enabled
			{
				get
				{
					return this.agentData.enabled;
				}
				set
				{
					this.agentData.enabled = value;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x06004165 RID: 16741 RVA: 0x0010ECB0 File Offset: 0x0010CEB0
			// (set) Token: 0x06004166 RID: 16742 RVA: 0x0010ECBD File Offset: 0x0010CEBD
			public float maxSlopeAngle
			{
				get
				{
					return this.agentData.maxSlopeAngle;
				}
				set
				{
					this.agentData.maxSlopeAngle = value;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06004167 RID: 16743 RVA: 0x0010ECCB File Offset: 0x0010CECB
			// (set) Token: 0x06004168 RID: 16744 RVA: 0x0010ECD8 File Offset: 0x0010CED8
			public float maxWalkSpeed
			{
				get
				{
					return this.agentData.maxWalkSpeed;
				}
				set
				{
					this.agentData.maxWalkSpeed = value;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06004169 RID: 16745 RVA: 0x0010ECE6 File Offset: 0x0010CEE6
			// (set) Token: 0x0600416A RID: 16746 RVA: 0x0010ECF3 File Offset: 0x0010CEF3
			public float maxJumpHeight
			{
				get
				{
					return this.agentData.maxJumpHeight;
				}
				set
				{
					this.agentData.maxJumpHeight = value;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x0600416B RID: 16747 RVA: 0x0010ED01 File Offset: 0x0010CF01
			// (set) Token: 0x0600416C RID: 16748 RVA: 0x0010ED0E File Offset: 0x0010CF0E
			public Vector3? currentPosition
			{
				get
				{
					return this.agentData.currentPosition;
				}
				set
				{
					this.agentData.currentPosition = value;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x0600416D RID: 16749 RVA: 0x0010ED1C File Offset: 0x0010CF1C
			// (set) Token: 0x0600416E RID: 16750 RVA: 0x0010ED29 File Offset: 0x0010CF29
			public Vector3? goalPosition
			{
				get
				{
					return this.agentData.goalPosition;
				}
				set
				{
					this.agentData.goalPosition = value;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x0600416F RID: 16751 RVA: 0x0010ED37 File Offset: 0x0010CF37
			public BroadNavigationSystem.AgentOutput output
			{
				get
				{
					return this.system.agentOutputs[(int)this.handle];
				}
			}

			// Token: 0x06004170 RID: 16752 RVA: 0x0010ED4F File Offset: 0x0010CF4F
			public void ConfigureFromBody(CharacterBody body)
			{
				this.system.ConfigureAgentFromBodyInternal(this.handle, body);
			}

			// Token: 0x06004171 RID: 16753 RVA: 0x0010ED63 File Offset: 0x0010CF63
			public void InvalidatePath()
			{
				this.system.InvalidateAgentPathInternal(this.handle);
			}

			// Token: 0x04003FC1 RID: 16321
			public static readonly BroadNavigationSystem.Agent invalid = new BroadNavigationSystem.Agent(null, BroadNavigationSystem.AgentHandle.None);

			// Token: 0x04003FC2 RID: 16322
			public readonly BroadNavigationSystem system;

			// Token: 0x04003FC3 RID: 16323
			public readonly BroadNavigationSystem.AgentHandle handle;
		}
	}
}
