using System;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C3E RID: 3134
	public class NetworkAnimatorParams : NetworkBehaviour
	{
		// Token: 0x060046E3 RID: 18147 RVA: 0x00125114 File Offset: 0x00123314
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.targetAnimatorParamHashes = new int[this.targetAnimatorParamNames.Length];
			for (int i = 0; i < this.targetAnimatorParamNames.Length; i++)
			{
				this.targetAnimatorParamHashes[i] = Animator.StringToHash(this.targetAnimatorParamNames[i]);
			}
			this.targetAnimatorParamNetworkValues = new NetworkLerpedFloat[this.targetAnimatorParamNames.Length];
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x0012517B File Offset: 0x0012337B
		private void FixedUpdate()
		{
			if (Util.HasEffectiveAuthority(this.networkIdentity))
			{
				this.FixedUpdateAuthority();
			}
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x00125190 File Offset: 0x00123390
		private void Update()
		{
			if (!Util.HasEffectiveAuthority(this.networkIdentity))
			{
				this.ApplyAnimatorParamsNonAuthority();
			}
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x001251A8 File Offset: 0x001233A8
		private void FixedUpdateAuthority()
		{
			bool flag;
			this.CollectAnimatorParamsAuthority(out flag);
			this.animatorParamsDirtyAuthority = (this.animatorParamsDirtyAuthority || flag);
			this.transmitTimerAuthority -= Time.fixedDeltaTime;
			if (this.transmitTimerAuthority <= 0f)
			{
				this.transmitTimerAuthority = this.transmitInterval;
				if (this.animatorParamsDirtyAuthority)
				{
					this.animatorParamsDirtyAuthority = false;
					if (NetworkServer.active)
					{
						base.SetDirtyBit(NetworkAnimatorParams.animatorParamsDirtyBit);
						return;
					}
					float[] array = new float[this.targetAnimatorParamNetworkValues.Length];
					for (int i = 0; i < this.targetAnimatorParamNetworkValues.Length; i++)
					{
						array[i] = this.targetAnimatorParamNetworkValues[i].GetAuthoritativeValue();
					}
					this.CallCmdReceiveAnimatorParams(array);
				}
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x00125254 File Offset: 0x00123454
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = initialState ? NetworkAnimatorParams.allDirtyBits : base.syncVarDirtyBits;
			bool flag = (num & NetworkAnimatorParams.animatorParamsDirtyBit) > 0U;
			writer.WritePackedUInt32(num);
			if (flag)
			{
				for (int i = 0; i < this.targetAnimatorParamNetworkValues.Length; i++)
				{
					writer.Write(this.targetAnimatorParamNetworkValues[i].GetAuthoritativeValue());
				}
			}
			return num > 0U;
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x001252B4 File Offset: 0x001234B4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadPackedUInt32() & NetworkAnimatorParams.animatorParamsDirtyBit) > 0U)
			{
				for (int i = 0; i < this.targetAnimatorParamNetworkValues.Length; i++)
				{
					this.targetAnimatorParamNetworkValues[i].PushValue(reader.ReadSingle());
				}
			}
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x001252FC File Offset: 0x001234FC
		private void ApplyAnimatorParamsNonAuthority()
		{
			for (int i = 0; i < this.targetAnimatorParamHashes.Length; i++)
			{
				this.targetAnimator.SetFloat(this.targetAnimatorParamHashes[i], this.targetAnimatorParamNetworkValues[i].GetCurrentValue(false));
			}
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00125344 File Offset: 0x00123544
		private void CollectAnimatorParamsAuthority(out bool animatorParamsChanged)
		{
			animatorParamsChanged = false;
			for (int i = 0; i < this.targetAnimatorParamHashes.Length; i++)
			{
				float @float = this.targetAnimator.GetFloat(this.targetAnimatorParamHashes[i]);
				ref NetworkLerpedFloat ptr = ref this.targetAnimatorParamNetworkValues[i];
				float currentValue = ptr.GetCurrentValue(true);
				if (@float != currentValue)
				{
					animatorParamsChanged = true;
					ptr.SetValueImmediate(@float);
				}
			}
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x001253A0 File Offset: 0x001235A0
		[Command]
		private void CmdReceiveAnimatorParams(float[] animatorParamValues)
		{
			int i = 0;
			int num = Math.Min(animatorParamValues.Length, this.targetAnimatorParamNetworkValues.Length);
			while (i < num)
			{
				this.targetAnimatorParamNetworkValues[i].PushValue(animatorParamValues[i]);
				i++;
			}
			base.SetDirtyBit(NetworkAnimatorParams.animatorParamsDirtyBit);
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x001253FC File Offset: 0x001235FC
		static NetworkAnimatorParams()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkAnimatorParams), NetworkAnimatorParams.kCmdCmdReceiveAnimatorParams, new NetworkBehaviour.CmdDelegate(NetworkAnimatorParams.InvokeCmdCmdReceiveAnimatorParams));
			NetworkCRC.RegisterBehaviour("NetworkAnimatorParams", 0);
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00125452 File Offset: 0x00123652
		protected static void InvokeCmdCmdReceiveAnimatorParams(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdReceiveAnimatorParams called on client.");
				return;
			}
			((NetworkAnimatorParams)obj).CmdReceiveAnimatorParams(GeneratedNetworkCode._ReadArraySingle_None(reader));
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x0012547C File Offset: 0x0012367C
		public void CallCmdReceiveAnimatorParams(float[] animatorParamValues)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdReceiveAnimatorParams called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdReceiveAnimatorParams(animatorParamValues);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkAnimatorParams.kCmdCmdReceiveAnimatorParams);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WriteArraySingle_None(networkWriter, animatorParamValues);
			base.SendCommandInternal(networkWriter, 0, "CmdReceiveAnimatorParams");
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400449D RID: 17565
		public Animator targetAnimator;

		// Token: 0x0400449E RID: 17566
		public string[] targetAnimatorParamNames;

		// Token: 0x0400449F RID: 17567
		public float transmitInterval = 0.1f;

		// Token: 0x040044A0 RID: 17568
		private NetworkIdentity networkIdentity;

		// Token: 0x040044A1 RID: 17569
		private static readonly uint animatorParamsDirtyBit = 1U;

		// Token: 0x040044A2 RID: 17570
		private static readonly uint allDirtyBits = NetworkAnimatorParams.animatorParamsDirtyBit;

		// Token: 0x040044A3 RID: 17571
		private int[] targetAnimatorParamHashes;

		// Token: 0x040044A4 RID: 17572
		private NetworkLerpedFloat[] targetAnimatorParamNetworkValues;

		// Token: 0x040044A5 RID: 17573
		private float transmitTimerAuthority;

		// Token: 0x040044A6 RID: 17574
		private bool animatorParamsDirtyAuthority;

		// Token: 0x040044A7 RID: 17575
		private static int kCmdCmdReceiveAnimatorParams = -1267054443;
	}
}
