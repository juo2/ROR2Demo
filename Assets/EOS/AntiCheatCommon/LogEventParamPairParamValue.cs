using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000585 RID: 1413
	public class LogEventParamPairParamValue : ISettable
	{
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x00023BA8 File Offset: 0x00021DA8
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x00023BB0 File Offset: 0x00021DB0
		public AntiCheatCommonEventParamType ParamValueType
		{
			get
			{
				return this.m_ParamValueType;
			}
			private set
			{
				this.m_ParamValueType = value;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00023BBC File Offset: 0x00021DBC
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x00023BDF File Offset: 0x00021DDF
		public IntPtr? ClientHandle
		{
			get
			{
				IntPtr? result;
				Helper.TryMarshalGet<IntPtr?, AntiCheatCommonEventParamType>(this.m_ClientHandle, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<IntPtr?, AntiCheatCommonEventParamType>(ref this.m_ClientHandle, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.ClientHandle, null);
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x00023BF8 File Offset: 0x00021DF8
		// (set) Token: 0x060021E8 RID: 8680 RVA: 0x00023C1B File Offset: 0x00021E1B
		public string String
		{
			get
			{
				string result;
				Helper.TryMarshalGet<string, AntiCheatCommonEventParamType>(this.m_String, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.String);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<string, AntiCheatCommonEventParamType>(ref this.m_String, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.String, null);
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00023C34 File Offset: 0x00021E34
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x00023C57 File Offset: 0x00021E57
		public uint? UInt32
		{
			get
			{
				uint? result;
				Helper.TryMarshalGet<uint?, AntiCheatCommonEventParamType>(this.m_UInt32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt32);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<uint?, AntiCheatCommonEventParamType>(ref this.m_UInt32, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.UInt32, null);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x00023C70 File Offset: 0x00021E70
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x00023C93 File Offset: 0x00021E93
		public int? Int32
		{
			get
			{
				int? result;
				Helper.TryMarshalGet<int?, AntiCheatCommonEventParamType>(this.m_Int32, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int32);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<int?, AntiCheatCommonEventParamType>(ref this.m_Int32, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Int32, null);
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x00023CAC File Offset: 0x00021EAC
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x00023CCF File Offset: 0x00021ECF
		public ulong? UInt64
		{
			get
			{
				ulong? result;
				Helper.TryMarshalGet<ulong?, AntiCheatCommonEventParamType>(this.m_UInt64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.UInt64);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<ulong?, AntiCheatCommonEventParamType>(ref this.m_UInt64, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.UInt64, null);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x00023CE8 File Offset: 0x00021EE8
		// (set) Token: 0x060021F0 RID: 8688 RVA: 0x00023D0B File Offset: 0x00021F0B
		public long? Int64
		{
			get
			{
				long? result;
				Helper.TryMarshalGet<long?, AntiCheatCommonEventParamType>(this.m_Int64, out result, this.m_ParamValueType, AntiCheatCommonEventParamType.Int64);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<long?, AntiCheatCommonEventParamType>(ref this.m_Int64, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Int64, null);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00023D24 File Offset: 0x00021F24
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x00023D47 File Offset: 0x00021F47
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
				Helper.TryMarshalSet<Vec3f, AntiCheatCommonEventParamType>(ref this.m_Vec3f, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Vector3f, null);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00023D60 File Offset: 0x00021F60
		// (set) Token: 0x060021F4 RID: 8692 RVA: 0x00023D83 File Offset: 0x00021F83
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
				Helper.TryMarshalSet<Quat, AntiCheatCommonEventParamType>(ref this.m_Quat, value, ref this.m_ParamValueType, AntiCheatCommonEventParamType.Quat, null);
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00023D9A File Offset: 0x00021F9A
		public static implicit operator LogEventParamPairParamValue(IntPtr value)
		{
			return new LogEventParamPairParamValue
			{
				ClientHandle = new IntPtr?(value)
			};
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00023DAD File Offset: 0x00021FAD
		public static implicit operator LogEventParamPairParamValue(string value)
		{
			return new LogEventParamPairParamValue
			{
				String = value
			};
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00023DBB File Offset: 0x00021FBB
		public static implicit operator LogEventParamPairParamValue(uint value)
		{
			return new LogEventParamPairParamValue
			{
				UInt32 = new uint?(value)
			};
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00023DCE File Offset: 0x00021FCE
		public static implicit operator LogEventParamPairParamValue(int value)
		{
			return new LogEventParamPairParamValue
			{
				Int32 = new int?(value)
			};
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00023DE1 File Offset: 0x00021FE1
		public static implicit operator LogEventParamPairParamValue(ulong value)
		{
			return new LogEventParamPairParamValue
			{
				UInt64 = new ulong?(value)
			};
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public static implicit operator LogEventParamPairParamValue(long value)
		{
			return new LogEventParamPairParamValue
			{
				Int64 = new long?(value)
			};
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00023E07 File Offset: 0x00022007
		public static implicit operator LogEventParamPairParamValue(Vec3f value)
		{
			return new LogEventParamPairParamValue
			{
				Vec3f = value
			};
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00023E15 File Offset: 0x00022015
		public static implicit operator LogEventParamPairParamValue(Quat value)
		{
			return new LogEventParamPairParamValue
			{
				Quat = value
			};
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00023E24 File Offset: 0x00022024
		internal void Set(LogEventParamPairParamValueInternal? other)
		{
			if (other != null)
			{
				this.ClientHandle = other.Value.ClientHandle;
				this.String = other.Value.String;
				this.UInt32 = other.Value.UInt32;
				this.Int32 = other.Value.Int32;
				this.UInt64 = other.Value.UInt64;
				this.Int64 = other.Value.Int64;
				this.Vec3f = other.Value.Vec3f;
				this.Quat = other.Value.Quat;
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00023EE5 File Offset: 0x000220E5
		public void Set(object other)
		{
			this.Set(other as LogEventParamPairParamValueInternal?);
		}

		// Token: 0x04000FF6 RID: 4086
		private AntiCheatCommonEventParamType m_ParamValueType;

		// Token: 0x04000FF7 RID: 4087
		private IntPtr? m_ClientHandle;

		// Token: 0x04000FF8 RID: 4088
		private string m_String;

		// Token: 0x04000FF9 RID: 4089
		private uint? m_UInt32;

		// Token: 0x04000FFA RID: 4090
		private int? m_Int32;

		// Token: 0x04000FFB RID: 4091
		private ulong? m_UInt64;

		// Token: 0x04000FFC RID: 4092
		private long? m_Int64;

		// Token: 0x04000FFD RID: 4093
		private Vec3f m_Vec3f;

		// Token: 0x04000FFE RID: 4094
		private Quat m_Quat;
	}
}
