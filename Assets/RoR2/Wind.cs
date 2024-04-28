using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class Wind : MonoBehaviour
{
	// Token: 0x060001A8 RID: 424 RVA: 0x0000890B File Offset: 0x00006B0B
	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
		this.props = new MaterialPropertyBlock();
		this.SetWind(this.props, this.windVector);
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00008938 File Offset: 0x00006B38
	private void Update()
	{
		this.time += Time.deltaTime;
		this.windVector.x = (0.5f + 0.5f * Mathf.Sin(this.MainWindSpeed * this.time * 0.017453292f)) * this.MainWindAmplitude;
		this.SetWind(this.props, this.windVector);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000899F File Offset: 0x00006B9F
	private void SetWind(MaterialPropertyBlock block, Vector4 input)
	{
		this.rend.GetPropertyBlock(block);
		block.Clear();
		block.SetVector("_Wind", input);
		this.rend.SetPropertyBlock(block);
	}

	// Token: 0x040001CA RID: 458
	private Renderer rend;

	// Token: 0x040001CB RID: 459
	private MaterialPropertyBlock props;

	// Token: 0x040001CC RID: 460
	public Vector4 windVector;

	// Token: 0x040001CD RID: 461
	public float MainWindAmplitude = 1f;

	// Token: 0x040001CE RID: 462
	public float MainWindSpeed = 3f;

	// Token: 0x040001CF RID: 463
	private float time;
}
