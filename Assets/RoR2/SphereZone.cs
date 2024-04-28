using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008A2 RID: 2210
	public class SphereZone : BaseZoneBehavior, IZone
	{
		// Token: 0x060030E1 RID: 12513 RVA: 0x000CFCCE File Offset: 0x000CDECE
		private void OnEnable()
		{
			if (this.rangeIndicator)
			{
				this.rangeIndicator.gameObject.SetActive(true);
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000CFCEE File Offset: 0x000CDEEE
		private void OnDisable()
		{
			if (this.rangeIndicator)
			{
				this.rangeIndicator.gameObject.SetActive(false);
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000CFD10 File Offset: 0x000CDF10
		private void Update()
		{
			if (this.rangeIndicator)
			{
				float num = Mathf.SmoothDamp(this.rangeIndicator.localScale.x, this.radius, ref this.rangeIndicatorScaleVelocity, this.indicatorSmoothTime);
				this.rangeIndicator.localScale = new Vector3(num, num, num);
			}
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000CFD68 File Offset: 0x000CDF68
		public override bool IsInBounds(Vector3 position)
		{
			Vector3 vector = position - base.transform.position;
			if (this.isInverted)
			{
				return vector.sqrMagnitude > this.radius * this.radius;
			}
			return vector.sqrMagnitude <= this.radius * this.radius;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000CFDD4 File Offset: 0x000CDFD4
		// (set) Token: 0x060030E8 RID: 12520 RVA: 0x000CFDE7 File Offset: 0x000CDFE7
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

		// Token: 0x060030E9 RID: 12521 RVA: 0x000CFDFC File Offset: 0x000CDFFC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			if (forceAll)
			{
				writer.Write(this.radius);
				return true;
			}
			bool flag2 = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag2)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag2 = true;
				}
				writer.Write(this.radius);
			}
			if (!flag2)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag2 || flag;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000CFE74 File Offset: 0x000CE074
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
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

		// Token: 0x060030EB RID: 12523 RVA: 0x00077BBF File Offset: 0x00075DBF
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x0400328D RID: 12941
		[Tooltip("The area of effect.")]
		[SyncVar]
		public float radius;

		// Token: 0x0400328E RID: 12942
		[Tooltip("The child range indicator object. Will be scaled to the radius.")]
		public Transform rangeIndicator;

		// Token: 0x0400328F RID: 12943
		[Tooltip("The time it takes the range indicator to update")]
		public float indicatorSmoothTime = 0.2f;

		// Token: 0x04003290 RID: 12944
		[Tooltip("If false, \"IsInBounds\" returns true when inside the sphere.  If true, outside the sphere is considered in bounds.")]
		public bool isInverted;

		// Token: 0x04003291 RID: 12945
		private float rangeIndicatorScaleVelocity;
	}
}
