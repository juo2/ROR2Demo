using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000600 RID: 1536
	public class BoxZone : BaseZoneBehavior, IZone
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x00077AC8 File Offset: 0x00075CC8
		public override bool IsInBounds(Vector3 position)
		{
			Vector3 vector = position - base.transform.position;
			vector.x = Mathf.Abs(vector.x);
			vector.y = Mathf.Abs(vector.y);
			vector.z = Mathf.Abs(vector.z);
			Vector3 vector2 = base.transform.lossyScale * 0.5f;
			if (this.isInverted)
			{
				return vector.x > vector2.x && vector.y > vector2.y && vector.z > vector2.z;
			}
			return vector.x < vector2.x && vector.y < vector2.y && vector.z < vector2.z;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00077B9C File Offset: 0x00075D9C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			bool flag2;
			return flag2 || flag;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00077BB5 File Offset: 0x00075DB5
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00077BBF File Offset: 0x00075DBF
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x040021E4 RID: 8676
		[Tooltip("If false, \"IsInBounds\" returns true when inside the box.  If true, outside the box is considered in bounds.")]
		[SerializeField]
		private bool isInverted;
	}
}
