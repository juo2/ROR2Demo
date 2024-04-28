using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000586 RID: 1414
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct LogEventParamPairParamValueInternal : ISettable, IDisposable
	{
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00023EF8 File Offset: 0x000220F8
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x00023F1B File Offset: 0x0002211B
		public IntPtr? ClientHandle
		{
			get
			{
				IntPtr? result;
				Helper.TryMarshalGet<AntiCheatCommonEventParamType>(this.m_ClientHandle, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<IntPtr, AntiCheatCommonEventParamType>(ref this.m_ClientHandle, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle, this);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00023F3C File Offset: 0x0002213C
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x00023F5F File Offset: 0x0002215F
		public string String
		{
			get
			{
				string result;
				Helper.TryMarshalGet<AntiCheatCommonEventParamType>(this.m_String, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.String);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<AntiCheatCommonEventParamType>(ref this.m_String, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.String, this);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00023F80 File Offset: 0x00022180
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x00023FA3 File Offset: 0x000221A3
		public uint? UInt32
		{
			get
			{
				uint? result;
				Helper.TryMarshalGet<uint, AntiCheatCommonEventParamType>(this.m_UInt32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt32);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<uint, AntiCheatCommonEventParamType>(ref this.m_UInt32, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.UInt32, this);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x00023FC4 File Offset: 0x000221C4
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x00023FE7 File Offset: 0x000221E7
		public int? Int32
		{
			get
			{
				int? result;
				Helper.TryMarshalGet<int, AntiCheatCommonEventParamType>(this.m_Int32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int32);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<int, AntiCheatCommonEventParamType>(ref this.m_Int32, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Int32, this);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00024008 File Offset: 0x00022208
		// (set) Token: 0x06002209 RID: 8713 RVA: 0x0002402B File Offset: 0x0002222B
		public ulong? UInt64
		{
			get
			{
				ulong? result;
				Helper.TryMarshalGet<ulong, AntiCheatCommonEventParamType>(this.m_UInt64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt64);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<ulong, AntiCheatCommonEventParamType>(ref this.m_UInt64, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.UInt64, this);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0002404C File Offset: 0x0002224C
		// (set) Token: 0x0600220B RID: 8715 RVA: 0x0002406F File Offset: 0x0002226F
		public long? Int64
		{
			get
			{
				long? result;
				Helper.TryMarshalGet<long, AntiCheatCommonEventParamType>(this.m_Int64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int64);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<long, AntiCheatCommonEventParamType>(ref this.m_Int64, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Int64, this);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00024090 File Offset: 0x00022290
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x000240B8 File Offset: 0x000222B8
		public Vec3f Vec3f
		{
			get
			{
				Vec3f result;
				Helper.TryMarshalGet<Vec3f, AntiCheatCommonEventParamType>(this.m_Vec3f, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Vector3f);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, AntiCheatCommonEventParamType>(ref this.m_Vec3f, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Vector3f, this);
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000240DC File Offset: 0x000222DC
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x00024104 File Offset: 0x00022304
		public Quat Quat
		{
			get
			{
				Quat result;
				Helper.TryMarshalGet<Quat, AntiCheatCommonEventParamType>(this.m_Quat, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Quat);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<QuatInternal, AntiCheatCommonEventParamType>(ref this.m_Quat, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Quat, this);
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00024128 File Offset: 0x00022328
		public void Set(LogEventParamPairParamValue other)
		{
			if (other != null)
			{
				this.ClientHandle = other.ClientHandle;
				this.String = other.String;
				this.UInt32 = other.UInt32;
				this.Int32 = other.Int32;
				this.UInt64 = other.UInt64;
				this.Int64 = other.Int64;
				this.Vec3f = other.Vec3f;
				this.Quat = other.Quat;
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00024198 File Offset: 0x00022398
		public void Set(object other)
		{
			this.Set(other as LogEventParamPairParamValue);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000241A6 File Offset: 0x000223A6
		public void Dispose()
		{
			Helper.TryMarshalDispose<AntiCheatCommonEventParamType>(ref this.m_ClientHandle, this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle);
			Helper.TryMarshalDispose<AntiCheatCommonEventParamType>(ref this.m_String, this.m_ParamValueType, AntiCheatCommonEventParamType.String);
			Helper.TryMarshalDispose<Vec3fInternal>(ref this.m_Vec3f);
			Helper.TryMarshalDispose<QuatInternal>(ref this.m_Quat);
		}

		// Token: 0x04000FFF RID: 4095
		[FieldOffset(0)]
		private AntiCheatCommonEventParamType m_ParamValueType;

		// Token: 0x04001000 RID: 4096
		[FieldOffset(8)]
		private IntPtr m_ClientHandle;

		// Token: 0x04001001 RID: 4097
		[FieldOffset(8)]
		private IntPtr m_String;

		// Token: 0x04001002 RID: 4098
		[FieldOffset(8)]
		private uint m_UInt32;

		// Token: 0x04001003 RID: 4099
		[FieldOffset(8)]
		private int m_Int32;

		// Token: 0x04001004 RID: 4100
		[FieldOffset(8)]
		private ulong m_UInt64;

		// Token: 0x04001005 RID: 4101
		[FieldOffset(8)]
		private long m_Int64;

		// Token: 0x04001006 RID: 4102
		[FieldOffset(8)]
		private Vec3fInternal m_Vec3f;

		// Token: 0x04001007 RID: 4103
		[FieldOffset(8)]
		private QuatInternal m_Quat;
	}
}
