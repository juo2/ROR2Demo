using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class SetRandomRotation : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x00003250 File Offset: 0x00001450
	private void Start()
	{
		Vector3 localEulerAngles = base.transform.localEulerAngles;
		if (this.setRandomXRotation)
		{
			float x = UnityEngine.Random.Range(0f, 359f);
			localEulerAngles.x = x;
		}
		if (this.setRandomYRotation)
		{
			float y = UnityEngine.Random.Range(0f, 359f);
			localEulerAngles.y = y;
		}
		if (this.setRandomZRotation)
		{
			float z = UnityEngine.Random.Range(0f, 359f);
			localEulerAngles.z = z;
		}
		base.transform.localEulerAngles = localEulerAngles;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000026ED File Offset: 0x000008ED
	private void Update()
	{
	}

	// Token: 0x0400005B RID: 91
	public bool setRandomXRotation;

	// Token: 0x0400005C RID: 92
	public bool setRandomYRotation;

	// Token: 0x0400005D RID: 93
	public bool setRandomZRotation;
}
