using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008C3 RID: 2243
	public class TemporaryVisualEffect : MonoBehaviour
	{
		// Token: 0x06003241 RID: 12865 RVA: 0x000D4435 File Offset: 0x000D2635
		private void Start()
		{
			this.RebuildVisualComponents();
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000D443D File Offset: 0x000D263D
		private void FixedUpdate()
		{
			if (this.previousVisualState != this.visualState)
			{
				this.RebuildVisualComponents();
			}
			this.previousVisualState = this.visualState;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000D4460 File Offset: 0x000D2660
		private void RebuildVisualComponents()
		{
			TemporaryVisualEffect.VisualState visualState = this.visualState;
			MonoBehaviour[] array;
			if (visualState == TemporaryVisualEffect.VisualState.Enter)
			{
				array = this.enterComponents;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = true;
				}
				array = this.exitComponents;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = false;
				}
				return;
			}
			if (visualState != TemporaryVisualEffect.VisualState.Exit)
			{
				return;
			}
			array = this.enterComponents;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			array = this.exitComponents;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000D44F8 File Offset: 0x000D26F8
		private void LateUpdate()
		{
			bool flag = this.healthComponent;
			if (this.parentTransform)
			{
				base.transform.position = this.parentTransform.position;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (!flag || (flag && !this.healthComponent.alive))
			{
				this.visualState = TemporaryVisualEffect.VisualState.Exit;
			}
			if (this.visualTransform)
			{
				this.visualTransform.localScale = new Vector3(this.radius, this.radius, this.radius);
			}
		}

		// Token: 0x04003362 RID: 13154
		public float radius = 1f;

		// Token: 0x04003363 RID: 13155
		public Transform parentTransform;

		// Token: 0x04003364 RID: 13156
		public Transform visualTransform;

		// Token: 0x04003365 RID: 13157
		public MonoBehaviour[] enterComponents;

		// Token: 0x04003366 RID: 13158
		public MonoBehaviour[] exitComponents;

		// Token: 0x04003367 RID: 13159
		public TemporaryVisualEffect.VisualState visualState;

		// Token: 0x04003368 RID: 13160
		private TemporaryVisualEffect.VisualState previousVisualState;

		// Token: 0x04003369 RID: 13161
		[HideInInspector]
		public HealthComponent healthComponent;

		// Token: 0x020008C4 RID: 2244
		public enum VisualState
		{
			// Token: 0x0400336B RID: 13163
			Enter,
			// Token: 0x0400336C RID: 13164
			Exit
		}
	}
}
