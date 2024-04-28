using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x0200082C RID: 2092
	public class PressurePlateController : MonoBehaviour
	{
		// Token: 0x06002D7E RID: 11646 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000C1DD4 File Offset: 0x000BFFD4
		private void FixedUpdate()
		{
			if (this.enableOverlapSphere)
			{
				this.overlapSphereStopwatch += Time.fixedDeltaTime;
				if (this.overlapSphereStopwatch >= 1f / this.overlapSphereFrequency)
				{
					this.overlapSphereStopwatch -= 1f / this.overlapSphereFrequency;
					Collider[] array = Physics.OverlapSphere(base.transform.position, this.overlapSphereRadius, LayerIndex.defaultLayer.mask | LayerIndex.fakeActor.mask, QueryTriggerInteraction.UseGlobal);
					bool @switch = array.Length > 1 || (array.Length == 1 && array[0] != this.pingCollider);
					this.SetSwitch(@switch);
				}
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000C1E90 File Offset: 0x000C0090
		public void EnableOverlapSphere(bool input)
		{
			this.enableOverlapSphere = input;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000C1E9C File Offset: 0x000C009C
		public void SetSwitch(bool switchIsDown)
		{
			if (switchIsDown != this.switchDown)
			{
				if (switchIsDown)
				{
					this.animationStopwatch = 0f;
					Util.PlaySound(this.switchDownSoundString, base.gameObject);
					UnityEvent onSwitchDown = this.OnSwitchDown;
					if (onSwitchDown != null)
					{
						onSwitchDown.Invoke();
					}
				}
				else
				{
					this.animationStopwatch = 0f;
					Util.PlaySound(this.switchUpSoundString, base.gameObject);
					UnityEvent onSwitchUp = this.OnSwitchUp;
					if (onSwitchUp != null)
					{
						onSwitchUp.Invoke();
					}
				}
				this.switchDown = switchIsDown;
			}
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000C1F1C File Offset: 0x000C011C
		private void Update()
		{
			this.animationStopwatch += Time.deltaTime;
			if (this.switchVisualTransform)
			{
				Vector3 localPosition = this.switchVisualTransform.transform.localPosition;
				bool flag = this.switchDown;
				if (flag)
				{
					if (flag)
					{
						localPosition.z = this.switchVisualPositionFromUpToDown.Evaluate(this.animationStopwatch);
					}
				}
				else
				{
					localPosition.z = this.switchVisualPositionFromDownToUp.Evaluate(this.animationStopwatch);
				}
				this.switchVisualTransform.localPosition = localPosition;
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000C1FA5 File Offset: 0x000C01A5
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(base.transform.position, this.overlapSphereRadius);
		}

		// Token: 0x04002F76 RID: 12150
		public bool enableOverlapSphere = true;

		// Token: 0x04002F77 RID: 12151
		public float overlapSphereRadius;

		// Token: 0x04002F78 RID: 12152
		public float overlapSphereFrequency;

		// Token: 0x04002F79 RID: 12153
		public string switchDownSoundString;

		// Token: 0x04002F7A RID: 12154
		public string switchUpSoundString;

		// Token: 0x04002F7B RID: 12155
		public UnityEvent OnSwitchDown;

		// Token: 0x04002F7C RID: 12156
		public UnityEvent OnSwitchUp;

		// Token: 0x04002F7D RID: 12157
		public Collider pingCollider;

		// Token: 0x04002F7E RID: 12158
		public AnimationCurve switchVisualPositionFromUpToDown;

		// Token: 0x04002F7F RID: 12159
		public AnimationCurve switchVisualPositionFromDownToUp;

		// Token: 0x04002F80 RID: 12160
		public Transform switchVisualTransform;

		// Token: 0x04002F81 RID: 12161
		private float overlapSphereStopwatch;

		// Token: 0x04002F82 RID: 12162
		private float animationStopwatch;

		// Token: 0x04002F83 RID: 12163
		private bool switchDown;
	}
}
