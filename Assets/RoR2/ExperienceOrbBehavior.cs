using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006D3 RID: 1747
	public class ExperienceOrbBehavior : MonoBehaviour
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000949A4 File Offset: 0x00092BA4
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x000949AC File Offset: 0x00092BAC
		public Transform targetTransform { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000949B5 File Offset: 0x00092BB5
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x000949BD File Offset: 0x00092BBD
		public float travelTime { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000949C6 File Offset: 0x00092BC6
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x000949CE File Offset: 0x00092BCE
		public ulong exp { get; set; }

		// Token: 0x06002272 RID: 8818 RVA: 0x000949D7 File Offset: 0x00092BD7
		private void Awake()
		{
			this.transform = base.transform;
			this.trail = base.GetComponent<TrailRenderer>();
			this.light = base.GetComponent<Light>();
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00094A00 File Offset: 0x00092C00
		private void Start()
		{
			this.localTime = 0f;
			this.consumed = false;
			this.startPos = this.transform.position;
			this.previousPos = this.startPos;
			this.scale = 2f * Mathf.Log(this.exp + 1f, 6f);
			this.initialVelocity = (Vector3.up * 4f + UnityEngine.Random.insideUnitSphere * 1f) * this.scale;
			this.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
			this.trail.startWidth = 0.05f * this.scale;
			if (this.light)
			{
				this.light.range = 1f * this.scale;
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00094AF4 File Offset: 0x00092CF4
		private void Update()
		{
			this.localTime += Time.deltaTime;
			if (!this.targetTransform)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			float num = Mathf.Clamp01(this.localTime / this.travelTime);
			this.previousPos = this.transform.position;
			this.transform.position = ExperienceOrbBehavior.CalculatePosition(this.startPos, this.initialVelocity, this.targetTransform.position, num);
			if (num >= 1f)
			{
				this.OnHitTarget();
				return;
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00094B88 File Offset: 0x00092D88
		private static Vector3 CalculatePosition(Vector3 startPos, Vector3 initialVelocity, Vector3 targetPos, float t)
		{
			Vector3 a = startPos + initialVelocity * t;
			float t2 = t * t * t;
			return Vector3.LerpUnclamped(a, targetPos, t2);
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00094BB1 File Offset: 0x00092DB1
		private void OnTriggerStay(Collider other)
		{
			if (other.transform == this.targetTransform)
			{
				this.OnHitTarget();
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00094BCC File Offset: 0x00092DCC
		private void OnHitTarget()
		{
			if (!this.consumed)
			{
				this.consumed = true;
				Util.PlaySound(ExperienceOrbBehavior.expSoundString, this.targetTransform.gameObject);
				UnityEngine.Object.Instantiate<GameObject>(this.hitEffectPrefab, this.transform.position, Util.QuaternionSafeLookRotation(this.previousPos - this.startPos)).transform.localScale *= this.scale;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0400276C RID: 10092
		public GameObject hitEffectPrefab;

		// Token: 0x0400276D RID: 10093
		private new Transform transform;

		// Token: 0x0400276E RID: 10094
		private TrailRenderer trail;

		// Token: 0x0400276F RID: 10095
		private Light light;

		// Token: 0x04002773 RID: 10099
		private float localTime;

		// Token: 0x04002774 RID: 10100
		private Vector3 startPos;

		// Token: 0x04002775 RID: 10101
		private Vector3 previousPos;

		// Token: 0x04002776 RID: 10102
		private Vector3 initialVelocity;

		// Token: 0x04002777 RID: 10103
		private float scale;

		// Token: 0x04002778 RID: 10104
		private bool consumed;

		// Token: 0x04002779 RID: 10105
		private static readonly string expSoundString = "Play_UI_xp_gain";
	}
}
