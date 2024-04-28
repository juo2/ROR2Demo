using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200078E RID: 1934
	public class JitterBones : MonoBehaviour
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x000B14F6 File Offset: 0x000AF6F6
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x000B14FE File Offset: 0x000AF6FE
		public SkinnedMeshRenderer skinnedMeshRenderer
		{
			get
			{
				return this._skinnedMeshRenderer;
			}
			set
			{
				if (this._skinnedMeshRenderer == value)
				{
					return;
				}
				this._skinnedMeshRenderer = value;
				this.RebuildBones();
			}
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000B1518 File Offset: 0x000AF718
		private void RebuildBones()
		{
			if (!this._skinnedMeshRenderer)
			{
				this.bones = Array.Empty<JitterBones.BoneInfo>();
				return;
			}
			Transform[] array = this._skinnedMeshRenderer.bones;
			Array.Resize<JitterBones.BoneInfo>(ref this.bones, array.Length);
			for (int i = 0; i < this.bones.Length; i++)
			{
				Transform transform = array[i];
				string text = transform.name.ToLower();
				this.bones[i] = new JitterBones.BoneInfo
				{
					transform = transform,
					isHead = text.Contains("head"),
					isRoot = text.Contains("root")
				};
			}
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000B15BD File Offset: 0x000AF7BD
		private void Start()
		{
			this.RebuildBones();
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000B15C8 File Offset: 0x000AF7C8
		private void LateUpdate()
		{
			if (this.skinnedMeshRenderer)
			{
				this.age += Time.deltaTime;
				for (int i = 0; i < this.bones.Length; i++)
				{
					JitterBones.BoneInfo boneInfo = this.bones[i];
					if (!boneInfo.isRoot)
					{
						float num = this.age * this.perlinNoiseFrequency;
						float num2 = (float)i;
						Vector3 vector = new Vector3(Mathf.PerlinNoise(num, num2), Mathf.PerlinNoise(num + 4f, num2 + 3f), Mathf.PerlinNoise(num + 6f, num2 - 7f));
						vector = HGMath.Remap(vector, this.perlinNoiseMinimumCutoff, this.perlinNoiseMaximumCutoff, -1f, 1f);
						vector = HGMath.Clamp(vector, 0f, 1f);
						vector *= this.perlinNoiseStrength;
						if (this.headBonusStrength >= 0f && boneInfo.isHead)
						{
							vector *= this.headBonusStrength;
						}
						boneInfo.transform.rotation *= Quaternion.Euler(vector);
					}
				}
			}
		}

		// Token: 0x04002C55 RID: 11349
		[SerializeField]
		private SkinnedMeshRenderer _skinnedMeshRenderer;

		// Token: 0x04002C56 RID: 11350
		private JitterBones.BoneInfo[] bones = Array.Empty<JitterBones.BoneInfo>();

		// Token: 0x04002C57 RID: 11351
		public float perlinNoiseFrequency;

		// Token: 0x04002C58 RID: 11352
		public float perlinNoiseStrength;

		// Token: 0x04002C59 RID: 11353
		public float perlinNoiseMinimumCutoff;

		// Token: 0x04002C5A RID: 11354
		public float perlinNoiseMaximumCutoff = 1f;

		// Token: 0x04002C5B RID: 11355
		public float headBonusStrength;

		// Token: 0x04002C5C RID: 11356
		private float age;

		// Token: 0x0200078F RID: 1935
		private struct BoneInfo
		{
			// Token: 0x04002C5D RID: 11357
			public Transform transform;

			// Token: 0x04002C5E RID: 11358
			public bool isHead;

			// Token: 0x04002C5F RID: 11359
			public bool isRoot;
		}
	}
}
