using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class BeginRapidlyActivatingAndDeactivating : MonoBehaviour
{
	// Token: 0x060000E7 RID: 231 RVA: 0x00005118 File Offset: 0x00003318
	private void FixedUpdate()
	{
		this.fixedAge += Time.fixedDeltaTime;
		if (this.fixedAge >= this.delayBeforeBeginningBlinking)
		{
			this.blinkAge += Time.fixedDeltaTime;
			if (this.blinkAge >= 1f / this.blinkFrequency)
			{
				this.blinkAge -= 1f / this.blinkFrequency;
				this.blinkingRootObject.SetActive(!this.blinkingRootObject.activeSelf);
			}
		}
	}

	// Token: 0x040000D6 RID: 214
	public float blinkFrequency = 10f;

	// Token: 0x040000D7 RID: 215
	public float delayBeforeBeginningBlinking = 30f;

	// Token: 0x040000D8 RID: 216
	public GameObject blinkingRootObject;

	// Token: 0x040000D9 RID: 217
	private float fixedAge;

	// Token: 0x040000DA RID: 218
	private float blinkAge;
}
