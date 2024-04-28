using System;
using System.Collections;
using System.Collections.Generic;

namespace HG.Coroutines
{
	// Token: 0x0200001E RID: 30
	public class ParallelProgressCoroutine : IEnumerator
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005078 File Offset: 0x00003278
		public ParallelProgressCoroutine(IProgress<float> progressReceiver)
		{
			this.progressReceiver = progressReceiver;
			this.internalCoroutine = this.InternalCoroutine();
			this.maxProgress = 0f;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000050AC File Offset: 0x000032AC
		public void Add(IEnumerator coroutine, ReadableProgress<float> coroutineProgressReceiver)
		{
			this.maxProgress += 1f;
			this.coroutinesList.Add(new ParallelProgressCoroutine.CoroutineWrapper
			{
				coroutine = coroutine,
				progressReceiver = coroutineProgressReceiver
			});
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000050EF File Offset: 0x000032EF
		public object Current
		{
			get
			{
				return this.internalCoroutine.Current;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000050FC File Offset: 0x000032FC
		public bool MoveNext()
		{
			return this.internalCoroutine.MoveNext();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005109 File Offset: 0x00003309
		public void Reset()
		{
			this.internalCoroutine.Reset();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005116 File Offset: 0x00003316
		private static float Clamp01(float value)
		{
			if (value > 1f)
			{
				return 1f;
			}
			if (value >= 0f)
			{
				return value;
			}
			return 0f;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005138 File Offset: 0x00003338
		private float CalcProgress()
		{
			if (this.coroutinesList.Count == 0)
			{
				return 1f;
			}
			float num = (float)this.completedSubCoroutinesCount;
			for (int i = 0; i < this.coroutinesList.Count; i++)
			{
				float num2 = ParallelProgressCoroutine.Clamp01(this.coroutinesList[i].progressReceiver.value);
				num += num2;
			}
			return ParallelProgressCoroutine.Clamp01(num / this.maxProgress);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000051A3 File Offset: 0x000033A3
		private IEnumerator InternalCoroutine()
		{
			yield return null;
			bool encounteredUnfinished = true;
			while (encounteredUnfinished)
			{
				encounteredUnfinished = false;
				int num;
				for (int i = this.coroutinesList.Count - 1; i >= 0; i = num)
				{
					IEnumerator coroutine = this.coroutinesList[i].coroutine;
					if (coroutine.MoveNext())
					{
						encounteredUnfinished = true;
						yield return coroutine.Current;
					}
					else
					{
						this.completedSubCoroutinesCount++;
						this.coroutinesList.RemoveAt(i);
					}
					num = i - 1;
				}
				this.progressReceiver.Report(this.CalcProgress());
			}
			yield break;
		}

		// Token: 0x0400003B RID: 59
		private IProgress<float> progressReceiver;

		// Token: 0x0400003C RID: 60
		private readonly List<ParallelProgressCoroutine.CoroutineWrapper> coroutinesList = new List<ParallelProgressCoroutine.CoroutineWrapper>();

		// Token: 0x0400003D RID: 61
		private IEnumerator internalCoroutine;

		// Token: 0x0400003E RID: 62
		private float maxProgress;

		// Token: 0x0400003F RID: 63
		private int completedSubCoroutinesCount;

		// Token: 0x02000030 RID: 48
		private struct CoroutineWrapper
		{
			// Token: 0x04000067 RID: 103
			public ReadableProgress<float> progressReceiver;

			// Token: 0x04000068 RID: 104
			public IEnumerator coroutine;
		}
	}
}
