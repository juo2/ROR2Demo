using System;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000015 RID: 21
public class Spinner : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003310 File Offset: 0x00001510
	private void Start()
	{
		if (!NetworkServer.active)
		{
			base.enabled = false;
			return;
		}
		UnityEngine.Random.Range(0f, 360f);
		UnityEngine.Random.Range(0f, 360f);
		UnityEngine.Random.Range(0f, 360f);
		UnityEngine.Random.Range(0f, 360f);
		base.gameObject.transform.rotation = UnityEngine.Random.rotationUniform;
		this.randRotationSpeed = UnityEngine.Random.Range(0.2f, 1f);
		this.randNumX = UnityEngine.Random.Range(0, 2);
		this.randNumY = UnityEngine.Random.Range(0, 2);
		this.randNumZ = UnityEngine.Random.Range(0, 2);
		this.randZeroOrOneX = (float)this.randNumX;
		this.randZeroOrOneY = (float)this.randNumY;
		this.randZeroOrOneZ = (float)this.randNumZ;
		if (this.randZeroOrOneX == 0f && this.randZeroOrOneY == 0f && this.randZeroOrOneZ == 0f)
		{
			this.randZeroOrOneX = 1f;
			this.randZeroOrOneY = 1f;
			this.randZeroOrOneZ = 1f;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000342C File Offset: 0x0000162C
	private void FixedUpdate()
	{
		base.gameObject.transform.Rotate(new Vector3(this.randZeroOrOneX, this.randZeroOrOneY, this.randZeroOrOneZ), this.randRotationSpeed * Time.timeScale);
	}

	// Token: 0x04000060 RID: 96
	private float randRotationSpeed;

	// Token: 0x04000061 RID: 97
	private int randNumX;

	// Token: 0x04000062 RID: 98
	private int randNumY;

	// Token: 0x04000063 RID: 99
	private int randNumZ;

	// Token: 0x04000064 RID: 100
	private float randZeroOrOneX;

	// Token: 0x04000065 RID: 101
	private float randZeroOrOneY;

	// Token: 0x04000066 RID: 102
	private float randZeroOrOneZ;
}
