using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000CF RID: 207
	public class MageCalibrate : BaseState
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000F814 File Offset: 0x0000DA14
		public override void OnEnter()
		{
			this.calibrationController = base.GetComponent<MageCalibrationController>();
			this.shouldApply = NetworkServer.active;
			base.OnEnter();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000F833 File Offset: 0x0000DA33
		public override void OnExit()
		{
			this.ApplyElement();
			base.OnExit();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		public override void FixedUpdate()
		{
			this.outer.SetNextStateToMain();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000F841 File Offset: 0x0000DA41
		private void ApplyElement()
		{
			Debug.Log("MageCalibrate.ApplyElement()");
			if (this.shouldApply && this.calibrationController)
			{
				this.shouldApply = false;
				this.calibrationController.SetElement(this.element);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000F87A File Offset: 0x0000DA7A
		public override void OnSerialize(NetworkWriter writer)
		{
			writer.Write((byte)this.element);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000F888 File Offset: 0x0000DA88
		public override void OnDeserialize(NetworkReader reader)
		{
			this.element = (MageElement)reader.ReadByte();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040003C7 RID: 967
		public MageElement element;

		// Token: 0x040003C8 RID: 968
		public MageCalibrationController calibrationController;

		// Token: 0x040003C9 RID: 969
		private bool shouldApply;
	}
}
