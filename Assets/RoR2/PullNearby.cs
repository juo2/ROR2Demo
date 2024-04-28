using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000830 RID: 2096
	public class PullNearby : MonoBehaviour
	{
		// Token: 0x06002D9D RID: 11677 RVA: 0x000C256F File Offset: 0x000C076F
		private void Start()
		{
			this.teamFilter = base.GetComponent<TeamFilter>();
			if (this.pullOnStart)
			{
				this.InitializePull();
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000C258B File Offset: 0x000C078B
		private void FixedUpdate()
		{
			this.fixedAge += Time.fixedDeltaTime;
			if (this.fixedAge <= this.pullDuration)
			{
				this.UpdatePull(Time.fixedDeltaTime);
			}
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000C25B8 File Offset: 0x000C07B8
		private void UpdatePull(float deltaTime)
		{
			if (!this.pulling)
			{
				return;
			}
			for (int i = 0; i < this.victimBodyList.Count; i++)
			{
				CharacterBody characterBody = this.victimBodyList[i];
				Vector3 vector = base.transform.position - characterBody.corePosition;
				float d = this.pullStrengthCurve.Evaluate(vector.magnitude / this.pullRadius);
				Vector3 b = vector.normalized * d * deltaTime;
				CharacterMotor component = characterBody.GetComponent<CharacterMotor>();
				if (component)
				{
					component.rootMotion += b;
				}
				else
				{
					Rigidbody component2 = characterBody.GetComponent<Rigidbody>();
					if (component2)
					{
						component2.velocity += b;
					}
				}
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000C268C File Offset: 0x000C088C
		public void InitializePull()
		{
			if (this.pulling)
			{
				return;
			}
			this.pulling = true;
			Collider[] array = Physics.OverlapSphere(base.transform.position, this.pullRadius, LayerIndex.defaultLayer.mask);
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 < this.maximumPullCount)
			{
				HealthComponent component = array[num].GetComponent<HealthComponent>();
				if (component)
				{
					TeamComponent component2 = component.GetComponent<TeamComponent>();
					bool flag = false;
					if (component2 && this.teamFilter)
					{
						flag = (component2.teamIndex == this.teamFilter.teamIndex);
					}
					if (!flag)
					{
						this.AddToList(component.gameObject);
						num2++;
					}
				}
				num++;
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000C274C File Offset: 0x000C094C
		private void AddToList(GameObject affectedObject)
		{
			CharacterBody component = affectedObject.GetComponent<CharacterBody>();
			if (!this.victimBodyList.Contains(component))
			{
				this.victimBodyList.Add(component);
			}
		}

		// Token: 0x04002FA0 RID: 12192
		public float pullRadius;

		// Token: 0x04002FA1 RID: 12193
		public float pullDuration;

		// Token: 0x04002FA2 RID: 12194
		public AnimationCurve pullStrengthCurve;

		// Token: 0x04002FA3 RID: 12195
		public bool pullOnStart;

		// Token: 0x04002FA4 RID: 12196
		public int maximumPullCount = int.MaxValue;

		// Token: 0x04002FA5 RID: 12197
		private List<CharacterBody> victimBodyList = new List<CharacterBody>();

		// Token: 0x04002FA6 RID: 12198
		private bool pulling;

		// Token: 0x04002FA7 RID: 12199
		private TeamFilter teamFilter;

		// Token: 0x04002FA8 RID: 12200
		private float fixedAge;
	}
}
