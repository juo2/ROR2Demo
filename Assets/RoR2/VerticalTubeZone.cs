using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008EC RID: 2284
	public class VerticalTubeZone : BaseZoneBehavior, IZone
	{
		// Token: 0x0600334F RID: 13135 RVA: 0x000D849E File Offset: 0x000D669E
		private void OnEnable()
		{
			if (this.rangeIndicator)
			{
				this.rangeIndicator.gameObject.SetActive(true);
			}
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000D84BE File Offset: 0x000D66BE
		private void OnDisable()
		{
			if (this.rangeIndicator)
			{
				this.rangeIndicator.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000D84E0 File Offset: 0x000D66E0
		private void Update()
		{
			if (this.rangeIndicator)
			{
				float num = Mathf.SmoothDamp(this.rangeIndicator.localScale.x, this.radius, ref this.rangeIndicatorScaleVelocity, this.indicatorSmoothTime);
				this.rangeIndicator.localScale = new Vector3(num, this.rangeIndicator.localScale.y, num);
			}
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000D8544 File Offset: 0x000D6744
		public override bool IsInBounds(Vector3 position)
		{
			Vector3 vector = position - base.transform.position;
			vector.y = 0f;
			return vector.sqrMagnitude <= this.radius * this.radius;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000D859C File Offset: 0x000D679C
		// (set) Token: 0x06003356 RID: 13142 RVA: 0x000D85AF File Offset: 0x000D67AF
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

		// Token: 0x06003357 RID: 13143 RVA: 0x000D85C4 File Offset: 0x000D67C4
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

		// Token: 0x06003358 RID: 13144 RVA: 0x000D863C File Offset: 0x000D683C
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

		// Token: 0x06003359 RID: 13145 RVA: 0x00077BBF File Offset: 0x00075DBF
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x04003460 RID: 13408
		[SyncVar]
		[Tooltip("The area of effect.")]
		public float radius;

		// Token: 0x04003461 RID: 13409
		[Tooltip("The child range indicator object. Will be scaled to the radius.")]
		public Transform rangeIndicator;

		// Token: 0x04003462 RID: 13410
		[Tooltip("The time it takes the range indicator to update")]
		public float indicatorSmoothTime = 0.2f;

		// Token: 0x04003463 RID: 13411
		private float rangeIndicatorScaleVelocity;
	}
}
