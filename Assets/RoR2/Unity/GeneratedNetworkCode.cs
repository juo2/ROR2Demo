using System;
using System.Runtime.InteropServices;
using RoR2;
using RoR2.Networking;
using UnityEngine.Networking;

namespace Unity
{
	// Token: 0x02000F1F RID: 3871
	[StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
	public class GeneratedNetworkCode
	{
		// Token: 0x060057B6 RID: 22454 RVA: 0x00161BD8 File Offset: 0x0015FDD8
		public static void _ReadStructSyncListPickupIndex_VoidSuppressorBehavior(NetworkReader reader, VoidSuppressorBehavior.SyncListPickupIndex instance)
		{
			ushort num = reader.ReadUInt16();
			instance.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				instance.AddInternal(instance.DeserializeItem(reader));
			}
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x00161C14 File Offset: 0x0015FE14
		public static void _WriteStructSyncListPickupIndex_VoidSuppressorBehavior(NetworkWriter writer, VoidSuppressorBehavior.SyncListPickupIndex value)
		{
			ushort count = value.Count;
			writer.Write(count);
			for (ushort num = 0; num < count; num += 1)
			{
				value.SerializeItem(writer, value.GetItem((int)num));
			}
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x00161C54 File Offset: 0x0015FE54
		public static void _ReadStructSyncListUserVote_None(NetworkReader reader, SyncListUserVote instance)
		{
			ushort num = reader.ReadUInt16();
			instance.Clear();
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				instance.AddInternal(instance.DeserializeItem(reader));
			}
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x00161C90 File Offset: 0x0015FE90
		public static void _WriteStructSyncListUserVote_None(NetworkWriter writer, SyncListUserVote value)
		{
			ushort count = value.Count;
			writer.Write(count);
			for (ushort num = 0; num < count; num += 1)
			{
				value.SerializeItem(writer, value.GetItem((int)num));
			}
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x00161CD0 File Offset: 0x0015FED0
		public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x00161D24 File Offset: 0x0015FF24
		public static string[] _ReadArrayString_None(NetworkReader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return new string[0];
			}
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = reader.ReadString();
			}
			return array;
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x00161D74 File Offset: 0x0015FF74
		public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00161DC5 File Offset: 0x0015FFC5
		public static void _WriteNetworkGuid_None(NetworkWriter writer, NetworkGuid value)
		{
			writer.WritePackedUInt64(value._a);
			writer.WritePackedUInt64(value._b);
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x00161DDF File Offset: 0x0015FFDF
		public static void _WriteNetworkDateTime_None(NetworkWriter writer, NetworkDateTime value)
		{
			writer.WritePackedUInt64((ulong)value._binaryValue);
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x00161DED File Offset: 0x0015FFED
		public static void _WriteRunStopwatch_Run(NetworkWriter writer, Run.RunStopwatch value)
		{
			writer.Write(value.offsetFromFixedTime);
			writer.Write(value.isPaused);
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00161E08 File Offset: 0x00160008
		public static NetworkGuid _ReadNetworkGuid_None(NetworkReader reader)
		{
			return new NetworkGuid
			{
				_a = reader.ReadPackedUInt64(),
				_b = reader.ReadPackedUInt64()
			};
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x00161E40 File Offset: 0x00160040
		public static NetworkDateTime _ReadNetworkDateTime_None(NetworkReader reader)
		{
			return new NetworkDateTime
			{
				_binaryValue = (long)reader.ReadPackedUInt64()
			};
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x00161E68 File Offset: 0x00160068
		public static Run.RunStopwatch _ReadRunStopwatch_Run(NetworkReader reader)
		{
			return new Run.RunStopwatch
			{
				offsetFromFixedTime = reader.ReadSingle(),
				isPaused = reader.ReadBoolean()
			};
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x00161E9E File Offset: 0x0016009E
		public static void _WriteNetworkMasterIndex_MasterCatalog(NetworkWriter writer, MasterCatalog.NetworkMasterIndex value)
		{
			writer.WritePackedUInt32(value.i);
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x00161EAC File Offset: 0x001600AC
		public static MasterCatalog.NetworkMasterIndex _ReadNetworkMasterIndex_MasterCatalog(NetworkReader reader)
		{
			return new MasterCatalog.NetworkMasterIndex
			{
				i = reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x00161ED3 File Offset: 0x001600D3
		public static void _WritePickupIndex_None(NetworkWriter writer, PickupIndex value)
		{
			writer.WritePackedUInt32((uint)value.value);
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x00161EE4 File Offset: 0x001600E4
		public static PickupIndex _ReadPickupIndex_None(NetworkReader reader)
		{
			return new PickupIndex
			{
				value = (int)reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x00161F0C File Offset: 0x0016010C
		public static PhysForceInfo _ReadPhysForceInfo_None(NetworkReader reader)
		{
			return new PhysForceInfo
			{
				force = reader.ReadVector3()
			};
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x00161F33 File Offset: 0x00160133
		public static void _WritePhysForceInfo_None(NetworkWriter writer, PhysForceInfo value)
		{
			writer.Write(value.force);
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x00161F44 File Offset: 0x00160144
		public static CharacterMotor.HitGroundInfo _ReadHitGroundInfo_CharacterMotor(NetworkReader reader)
		{
			return new CharacterMotor.HitGroundInfo
			{
				velocity = reader.ReadVector3(),
				position = reader.ReadVector3()
			};
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x00161F7A File Offset: 0x0016017A
		public static void _WriteHitGroundInfo_CharacterMotor(NetworkWriter writer, CharacterMotor.HitGroundInfo value)
		{
			writer.Write(value.velocity);
			writer.Write(value.position);
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x00161F94 File Offset: 0x00160194
		public static void _WriteFixedTimeStamp_Run(NetworkWriter writer, Run.FixedTimeStamp value)
		{
			writer.Write(value.t);
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x00161FA4 File Offset: 0x001601A4
		public static Run.FixedTimeStamp _ReadFixedTimeStamp_Run(NetworkReader reader)
		{
			return new Run.FixedTimeStamp
			{
				t = reader.ReadSingle()
			};
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00161FCB File Offset: 0x001601CB
		public static void _WriteHurtBoxReference_None(NetworkWriter writer, HurtBoxReference value)
		{
			writer.Write(value.rootObject);
			writer.WritePackedUInt32((uint)value.hurtBoxIndexPlusOne);
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x00161FE8 File Offset: 0x001601E8
		public static HurtBoxReference _ReadHurtBoxReference_None(NetworkReader reader)
		{
			return new HurtBoxReference
			{
				rootObject = reader.ReadGameObject(),
				hurtBoxIndexPlusOne = (byte)reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0016201E File Offset: 0x0016021E
		public static void _WriteParentIdentifier_NetworkParent(NetworkWriter writer, NetworkParent.ParentIdentifier value)
		{
			writer.WritePackedUInt32((uint)value.indexInParentChildLocatorPlusOne);
			writer.Write(value.parentNetworkInstanceId);
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x00162038 File Offset: 0x00160238
		public static NetworkParent.ParentIdentifier _ReadParentIdentifier_NetworkParent(NetworkReader reader)
		{
			return new NetworkParent.ParentIdentifier
			{
				indexInParentChildLocatorPlusOne = (byte)reader.ReadPackedUInt32(),
				parentNetworkInstanceId = reader.ReadNetworkId()
			};
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x00162070 File Offset: 0x00160270
		public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x001620C4 File Offset: 0x001602C4
		public static UnlockableIndex[] _ReadArrayUnlockableIndex_None(NetworkReader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return new UnlockableIndex[0];
			}
			UnlockableIndex[] array = new UnlockableIndex[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (UnlockableIndex)reader.ReadInt32();
			}
			return array;
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00162114 File Offset: 0x00160314
		public static void _WriteArrayUnlockableIndex_None(NetworkWriter writer, UnlockableIndex[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write((int)value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00162165 File Offset: 0x00160365
		public static void _WriteNetworkUserId_None(NetworkWriter writer, NetworkUserId value)
		{
			writer.WritePackedUInt64(value.value);
			writer.Write(value.strValue);
			writer.WritePackedUInt32((uint)value.subId);
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0016218C File Offset: 0x0016038C
		public static NetworkUserId _ReadNetworkUserId_None(NetworkReader reader)
		{
			return new NetworkUserId
			{
				value = reader.ReadPackedUInt64(),
				strValue = reader.ReadString(),
				subId = (byte)reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x001621D4 File Offset: 0x001603D4
		public static PingerController.PingInfo _ReadPingInfo_PingerController(NetworkReader reader)
		{
			return new PingerController.PingInfo
			{
				active = reader.ReadBoolean(),
				origin = reader.ReadVector3(),
				normal = reader.ReadVector3(),
				targetNetworkIdentity = reader.ReadNetworkIdentity()
			};
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00162228 File Offset: 0x00160428
		public static void _WritePingInfo_PingerController(NetworkWriter writer, PingerController.PingInfo value)
		{
			writer.Write(value.active);
			writer.Write(value.origin);
			writer.Write(value.normal);
			writer.Write(value.targetNetworkIdentity);
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x0016225C File Offset: 0x0016045C
		public static CubicBezier3 _ReadCubicBezier3_None(NetworkReader reader)
		{
			return new CubicBezier3
			{
				a = reader.ReadVector3(),
				b = reader.ReadVector3(),
				c = reader.ReadVector3(),
				d = reader.ReadVector3()
			};
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x001622B0 File Offset: 0x001604B0
		public static WormBodyPositions2.KeyFrame _ReadKeyFrame_WormBodyPositions2(NetworkReader reader)
		{
			return new WormBodyPositions2.KeyFrame
			{
				curve = GeneratedNetworkCode._ReadCubicBezier3_None(reader),
				length = reader.ReadSingle(),
				time = reader.ReadSingle()
			};
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x001622F5 File Offset: 0x001604F5
		public static void _WriteCubicBezier3_None(NetworkWriter writer, CubicBezier3 value)
		{
			writer.Write(value.a);
			writer.Write(value.b);
			writer.Write(value.c);
			writer.Write(value.d);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00162327 File Offset: 0x00160527
		public static void _WriteKeyFrame_WormBodyPositions2(NetworkWriter writer, WormBodyPositions2.KeyFrame value)
		{
			GeneratedNetworkCode._WriteCubicBezier3_None(writer, value.curve);
			writer.Write(value.length);
			writer.Write(value.time);
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x00162350 File Offset: 0x00160550
		public static float[] _ReadArraySingle_None(NetworkReader reader)
		{
			int num = (int)reader.ReadUInt16();
			if (num == 0)
			{
				return new float[0];
			}
			float[] array = new float[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = reader.ReadSingle();
			}
			return array;
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x001623A0 File Offset: 0x001605A0
		public static void _WriteArraySingle_None(NetworkWriter writer, float[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x001623F1 File Offset: 0x001605F1
		public static void _WriteCSteamID_None(NetworkWriter writer, CSteamID value)
		{
			writer.Write(value.stringValue);
			writer.WritePackedUInt64(value.steamValue);
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x0016240C File Offset: 0x0016060C
		public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x00162460 File Offset: 0x00160660
		public static CSteamID _ReadCSteamID_None(NetworkReader reader)
		{
			return new CSteamID
			{
				stringValue = reader.ReadString(),
				steamValue = reader.ReadPackedUInt64()
			};
		}

		// Token: 0x060057E1 RID: 22497 RVA: 0x00162498 File Offset: 0x00160698
		public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001624EC File Offset: 0x001606EC
		public static void _WriteArrayString_None(NetworkWriter writer, string[] value)
		{
			if (value == null)
			{
				writer.Write(0);
				return;
			}
			ushort value2 = (ushort)value.Length;
			writer.Write(value2);
			ushort num = 0;
			while ((int)num < value.Length)
			{
				writer.Write(value[(int)num]);
				num += 1;
			}
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x0016253D File Offset: 0x0016073D
		public static void _WriteUserID_None(NetworkWriter writer, UserID value)
		{
			GeneratedNetworkCode._WriteCSteamID_None(writer, value.CID);
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x0016254C File Offset: 0x0016074C
		public static UserID _ReadUserID_None(NetworkReader reader)
		{
			return new UserID
			{
				CID = GeneratedNetworkCode._ReadCSteamID_None(reader)
			};
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x00162574 File Offset: 0x00160774
		public static ServerAchievementIndex _ReadServerAchievementIndex_None(NetworkReader reader)
		{
			return new ServerAchievementIndex
			{
				intValue = (int)reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x0016259B File Offset: 0x0016079B
		public static void _WriteServerAchievementIndex_None(NetworkWriter writer, ServerAchievementIndex value)
		{
			writer.WritePackedUInt32((uint)value.intValue);
		}
	}
}
