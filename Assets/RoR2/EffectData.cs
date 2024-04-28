using System;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using RoR2.Audio;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200059A RID: 1434
	public class EffectData
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0006FAFB File Offset: 0x0006DCFB
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0006FB03 File Offset: 0x0006DD03
		public Vector3 origin
		{
			get
			{
				return this._origin;
			}
			set
			{
				if (!Util.PositionIsValid(value))
				{
					Debug.LogFormat("EffectData.origin assignment position is invalid! Position={0}", new object[]
					{
						value
					});
					return;
				}
				this._origin = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0006FB2E File Offset: 0x0006DD2E
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x0006FB36 File Offset: 0x0006DD36
		public GameObject rootObject { get; private set; } = EffectData.defaultRootObject;

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0006FB3F File Offset: 0x0006DD3F
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0006FB47 File Offset: 0x0006DD47
		public short modelChildIndex { get; private set; } = EffectData.defaultModelChildIndex;

		// Token: 0x060019DB RID: 6619 RVA: 0x0006FB50 File Offset: 0x0006DD50
		public static void Copy([NotNull] EffectData src, [NotNull] EffectData dest)
		{
			dest.origin = src.origin;
			dest.rotation = src.rotation;
			dest.rootObject = src.rootObject;
			dest.modelChildIndex = src.modelChildIndex;
			dest.scale = src.scale;
			dest.color = src.color;
			dest.start = src.start;
			dest.surfaceDefIndex = src.surfaceDefIndex;
			dest.genericUInt = src.genericUInt;
			dest.genericFloat = src.genericFloat;
			dest.genericBool = src.genericBool;
			dest.networkSoundEventIndex = src.networkSoundEventIndex;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0006FBED File Offset: 0x0006DDED
		public void SetNetworkedObjectReference(GameObject networkedObject)
		{
			this.rootObject = networkedObject;
			this.modelChildIndex = -1;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0006FBFD File Offset: 0x0006DDFD
		public GameObject ResolveNetworkedObjectReference()
		{
			return this.rootObject;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0006FC08 File Offset: 0x0006DE08
		public void SetHurtBoxReference(HurtBox hurtBox)
		{
			if (!hurtBox || !hurtBox.healthComponent)
			{
				this.rootObject = null;
				this.modelChildIndex = -1;
				return;
			}
			this.rootObject = hurtBox.healthComponent.gameObject;
			this.modelChildIndex = hurtBox.indexInGroup;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0006FC58 File Offset: 0x0006DE58
		public void SetHurtBoxReference(GameObject gameObject)
		{
			HurtBox hurtBox = (gameObject != null) ? gameObject.GetComponent<HurtBox>() : null;
			if (hurtBox)
			{
				this.SetHurtBoxReference(hurtBox);
				return;
			}
			this.rootObject = gameObject;
			this.modelChildIndex = -1;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0006FC90 File Offset: 0x0006DE90
		public GameObject ResolveHurtBoxReference()
		{
			if (this.modelChildIndex == -1)
			{
				return this.rootObject;
			}
			if (!this.rootObject)
			{
				return null;
			}
			ModelLocator component = this.rootObject.GetComponent<ModelLocator>();
			if (!component)
			{
				return null;
			}
			Transform modelTransform = component.modelTransform;
			if (!modelTransform)
			{
				return null;
			}
			HurtBoxGroup component2 = modelTransform.GetComponent<HurtBoxGroup>();
			if (!component2)
			{
				return null;
			}
			HurtBox safe = ArrayUtils.GetSafe<HurtBox>(component2.hurtBoxes, (int)this.modelChildIndex);
			if (!safe)
			{
				return null;
			}
			return safe.gameObject;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0006FD16 File Offset: 0x0006DF16
		public void SetChildLocatorTransformReference(GameObject rootObject, int childIndex)
		{
			this.rootObject = rootObject;
			this.modelChildIndex = (short)childIndex;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0006FD28 File Offset: 0x0006DF28
		public Transform ResolveChildLocatorTransformReference()
		{
			if (this.rootObject)
			{
				if (this.modelChildIndex == -1)
				{
					return this.rootObject.transform;
				}
				ModelLocator component = this.rootObject.GetComponent<ModelLocator>();
				if (component && component.modelTransform)
				{
					ChildLocator component2 = component.modelTransform.GetComponent<ChildLocator>();
					if (component2)
					{
						return component2.FindChild((int)this.modelChildIndex);
					}
				}
			}
			return null;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0006FD9C File Offset: 0x0006DF9C
		public EffectData Clone()
		{
			EffectData effectData = new EffectData();
			EffectData.Copy(this, effectData);
			return effectData;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0006FDB7 File Offset: 0x0006DFB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ColorEquals(in Color32 x, in Color32 y)
		{
			return x.r == y.r && x.g == y.g && x.b == y.b && x.a == y.a;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0006FDF4 File Offset: 0x0006DFF4
		public void Serialize(NetworkWriter writer)
		{
			uint num = 0U;
			bool flag = !this.rotation.Equals(EffectData.defaultRotation);
			bool flag2 = this.rootObject != EffectData.defaultRootObject;
			bool flag3 = this.modelChildIndex != EffectData.defaultModelChildIndex;
			bool flag4 = this.scale != EffectData.defaultScale;
			bool flag5 = !EffectData.ColorEquals(this.color, EffectData.defaultColor);
			bool flag6 = !this.start.Equals(EffectData.defaultStart);
			bool flag7 = this.surfaceDefIndex != EffectData.defaultSurfaceDefIndex;
			bool flag8 = this.genericUInt != EffectData.defaultGenericUInt;
			bool flag9 = this.genericFloat != EffectData.defaultGenericFloat;
			bool flag10 = this.genericBool != EffectData.defaultGenericBool;
			bool flag11 = this.networkSoundEventIndex != EffectData.defaultNetworkSoundEventIndex;
			if (flag)
			{
				num |= EffectData.useNonDefaultRotationFlag;
			}
			if (flag2)
			{
				num |= EffectData.useNonDefaultRootObjectFlag;
			}
			if (flag3)
			{
				num |= EffectData.useNonDefaultModelChildIndexFlag;
			}
			if (flag4)
			{
				num |= EffectData.useNonDefaultScaleFlag;
			}
			if (flag5)
			{
				num |= EffectData.useNonDefaultColorFlag;
			}
			if (flag6)
			{
				num |= EffectData.useNonDefaultStartFlag;
			}
			if (flag7)
			{
				num |= EffectData.useNonDefaultSurfaceDefIndexFlag;
			}
			if (flag8)
			{
				num |= EffectData.useNonDefaultGenericUIntFlag;
			}
			if (flag9)
			{
				num |= EffectData.useNonDefaultGenericFloatFlag;
			}
			if (flag10)
			{
				num |= EffectData.useNonDefaultGenericBoolFlag;
			}
			if (flag11)
			{
				num |= EffectData.useNonDefaultNetworkSoundEventIndexFlag;
			}
			writer.WritePackedUInt32(num);
			writer.Write(this.origin);
			if (flag)
			{
				writer.Write(this.rotation);
			}
			if (flag2)
			{
				writer.Write(this.rootObject);
			}
			if (flag3)
			{
				writer.Write((byte)(this.modelChildIndex + 1));
			}
			if (flag4)
			{
				writer.Write(this.scale);
			}
			if (flag5)
			{
				writer.Write(this.color);
			}
			if (flag6)
			{
				writer.Write(this.start);
			}
			if (flag7)
			{
				writer.WritePackedIndex32((int)this.surfaceDefIndex);
			}
			if (flag8)
			{
				writer.WritePackedUInt32(this.genericUInt);
			}
			if (flag9)
			{
				writer.Write(this.genericFloat);
			}
			if (flag10)
			{
				writer.Write(this.genericBool);
			}
			if (flag11)
			{
				writer.WriteNetworkSoundEventIndex(this.networkSoundEventIndex);
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00070010 File Offset: 0x0006E210
		public void Deserialize(NetworkReader reader)
		{
			EffectData.<>c__DisplayClass54_0 CS$<>8__locals1;
			CS$<>8__locals1.flags = reader.ReadPackedUInt32();
			bool flag = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultRotationFlag, ref CS$<>8__locals1);
			bool flag2 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultRootObjectFlag, ref CS$<>8__locals1);
			bool flag3 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultModelChildIndexFlag, ref CS$<>8__locals1);
			bool flag4 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultScaleFlag, ref CS$<>8__locals1);
			bool flag5 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultColorFlag, ref CS$<>8__locals1);
			bool flag6 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultStartFlag, ref CS$<>8__locals1);
			bool flag7 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultSurfaceDefIndexFlag, ref CS$<>8__locals1);
			bool flag8 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultGenericUIntFlag, ref CS$<>8__locals1);
			bool flag9 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultGenericFloatFlag, ref CS$<>8__locals1);
			bool flag10 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultGenericBoolFlag, ref CS$<>8__locals1);
			bool flag11 = EffectData.<Deserialize>g__HasFlag|54_0(EffectData.useNonDefaultNetworkSoundEventIndexFlag, ref CS$<>8__locals1);
			this.origin = reader.ReadVector3();
			this.rotation = (flag ? reader.ReadQuaternion() : EffectData.defaultRotation);
			this.rootObject = (flag2 ? reader.ReadGameObject() : EffectData.defaultRootObject);
			this.modelChildIndex = (flag3 ? ((short)(reader.ReadByte() - 1)) : EffectData.defaultModelChildIndex);
			this.scale = (flag4 ? reader.ReadSingle() : EffectData.defaultScale);
			this.color = (flag5 ? reader.ReadColor32() : EffectData.defaultColor);
			this.start = (flag6 ? reader.ReadVector3() : EffectData.defaultStart);
			this.surfaceDefIndex = (SurfaceDefIndex)(flag7 ? reader.ReadPackedIndex32() : ((int)EffectData.defaultSurfaceDefIndex));
			this.genericUInt = (flag8 ? reader.ReadPackedUInt32() : EffectData.defaultGenericUInt);
			this.genericFloat = (flag9 ? reader.ReadSingle() : EffectData.defaultGenericFloat);
			this.genericBool = (flag10 ? reader.ReadBoolean() : EffectData.defaultGenericBool);
			this.networkSoundEventIndex = (flag11 ? reader.ReadNetworkSoundEventIndex() : EffectData.defaultNetworkSoundEventIndex);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00070315 File Offset: 0x0006E515
		[CompilerGenerated]
		internal static bool <Deserialize>g__HasFlag|54_0(in uint mask, ref EffectData.<>c__DisplayClass54_0 A_1)
		{
			return (A_1.flags & mask) > 0U;
		}

		// Token: 0x0400201F RID: 8223
		private Vector3 _origin;

		// Token: 0x04002020 RID: 8224
		public Quaternion rotation = EffectData.defaultRotation;

		// Token: 0x04002023 RID: 8227
		public float scale = EffectData.defaultScale;

		// Token: 0x04002024 RID: 8228
		public Color32 color = EffectData.defaultColor;

		// Token: 0x04002025 RID: 8229
		public Vector3 start = EffectData.defaultStart;

		// Token: 0x04002026 RID: 8230
		public SurfaceDefIndex surfaceDefIndex = EffectData.defaultSurfaceDefIndex;

		// Token: 0x04002027 RID: 8231
		public uint genericUInt = EffectData.defaultGenericUInt;

		// Token: 0x04002028 RID: 8232
		public float genericFloat = EffectData.defaultGenericFloat;

		// Token: 0x04002029 RID: 8233
		public bool genericBool = EffectData.defaultGenericBool;

		// Token: 0x0400202A RID: 8234
		public NetworkSoundEventIndex networkSoundEventIndex = EffectData.defaultNetworkSoundEventIndex;

		// Token: 0x0400202B RID: 8235
		private static readonly uint useNonDefaultRotationFlag = 1U;

		// Token: 0x0400202C RID: 8236
		private static readonly uint useNonDefaultRootObjectFlag = 2U;

		// Token: 0x0400202D RID: 8237
		private static readonly uint useNonDefaultModelChildIndexFlag = 4U;

		// Token: 0x0400202E RID: 8238
		private static readonly uint useNonDefaultScaleFlag = 8U;

		// Token: 0x0400202F RID: 8239
		private static readonly uint useNonDefaultColorFlag = 16U;

		// Token: 0x04002030 RID: 8240
		private static readonly uint useNonDefaultStartFlag = 32U;

		// Token: 0x04002031 RID: 8241
		private static readonly uint useNonDefaultSurfaceDefIndexFlag = 64U;

		// Token: 0x04002032 RID: 8242
		private static readonly uint useNonDefaultGenericUIntFlag = 128U;

		// Token: 0x04002033 RID: 8243
		private static readonly uint useNonDefaultGenericFloatFlag = 256U;

		// Token: 0x04002034 RID: 8244
		private static readonly uint useNonDefaultGenericBoolFlag = 512U;

		// Token: 0x04002035 RID: 8245
		private static readonly uint useNonDefaultNetworkSoundEventIndexFlag = 1024U;

		// Token: 0x04002036 RID: 8246
		private static readonly Quaternion defaultRotation = Quaternion.identity;

		// Token: 0x04002037 RID: 8247
		private static readonly GameObject defaultRootObject = null;

		// Token: 0x04002038 RID: 8248
		private static readonly short defaultModelChildIndex = -1;

		// Token: 0x04002039 RID: 8249
		private static readonly float defaultScale = 1f;

		// Token: 0x0400203A RID: 8250
		private static readonly Color32 defaultColor = Color.white;

		// Token: 0x0400203B RID: 8251
		private static readonly Vector3 defaultStart = Vector3.zero;

		// Token: 0x0400203C RID: 8252
		private static readonly SurfaceDefIndex defaultSurfaceDefIndex = SurfaceDefIndex.Invalid;

		// Token: 0x0400203D RID: 8253
		private static readonly uint defaultGenericUInt = 0U;

		// Token: 0x0400203E RID: 8254
		private static readonly float defaultGenericFloat = 0f;

		// Token: 0x0400203F RID: 8255
		private static readonly bool defaultGenericBool = false;

		// Token: 0x04002040 RID: 8256
		private static readonly NetworkSoundEventIndex defaultNetworkSoundEventIndex = NetworkSoundEventIndex.Invalid;
	}
}
