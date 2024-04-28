using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D10 RID: 3344
	public class HUDSpeedometer : MonoBehaviour
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x0013A2D5 File Offset: 0x001384D5
		// (set) Token: 0x06004C3B RID: 19515 RVA: 0x0013A2E0 File Offset: 0x001384E0
		public Transform targetTransform
		{
			get
			{
				return this._targetTransform;
			}
			set
			{
				if (this._targetTransform == value)
				{
					return;
				}
				this._targetTransform = value;
				if (this._targetTransform)
				{
					this.lastTargetPosition = this._targetTransform.position;
					this.lastTargetPositionFixedUpdate = this._targetTransform.position;
				}
			}
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0013A334 File Offset: 0x00138534
		private float EstimateSpeed(ref Vector3 oldPosition, Vector3 newPosition, float deltaTime)
		{
			float result = 0f;
			if (deltaTime != 0f)
			{
				result = (newPosition - oldPosition).magnitude / deltaTime;
			}
			oldPosition = newPosition;
			return result;
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0013A370 File Offset: 0x00138570
		private void Update()
		{
			if (this._targetTransform)
			{
				float num = this.EstimateSpeed(ref this.lastTargetPosition, this._targetTransform.position, Time.deltaTime);
				this.label.text = string.Format("{0:0.00} m/s", num);
			}
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0013A3C4 File Offset: 0x001385C4
		private void FixedUpdate()
		{
			if (this._targetTransform)
			{
				float num = this.EstimateSpeed(ref this.lastTargetPositionFixedUpdate, this._targetTransform.position, Time.deltaTime);
				this.fixedUpdateLabel.text = string.Format("{0:0.00} m/s", num);
			}
		}

		// Token: 0x0400491E RID: 18718
		public TextMeshProUGUI label;

		// Token: 0x0400491F RID: 18719
		public TextMeshProUGUI fixedUpdateLabel;

		// Token: 0x04004920 RID: 18720
		private Transform _targetTransform;

		// Token: 0x04004921 RID: 18721
		private Vector3 lastTargetPosition;

		// Token: 0x04004922 RID: 18722
		private Vector3 lastTargetPositionFixedUpdate;
	}
}
