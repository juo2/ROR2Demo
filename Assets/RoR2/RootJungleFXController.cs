using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class RootJungleFXController : MonoBehaviour
{
	// Token: 0x06000048 RID: 72 RVA: 0x0000309D File Offset: 0x0000129D
	private void Start()
	{
		this.TurnOffFX(this.FXParticles.Count);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000030B0 File Offset: 0x000012B0
	private void FixedUpdate()
	{
		if (this.timeFX)
		{
			this.effectsTimer -= Time.fixedDeltaTime;
			if (this.effectsTimer <= 0f)
			{
				this.timeFX = false;
				this.TurnOffFX(this.numActive);
			}
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000030EC File Offset: 0x000012EC
	public void SetupParticles()
	{
		this.numActive = UnityEngine.Random.Range(this.minSystemsActive, this.maxSystemsActive + 1);
		this.timeActive = UnityEngine.Random.Range(this.minActiveTime, this.maxActiveTime);
		this.effectsTimer = this.timeActive;
		this.FXParticles = RootJungleFXController.Shuffle<ParticleSystem>(this.FXParticles);
		this.TurnOnFX(this.numActive);
		this.timeFX = true;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000315C File Offset: 0x0000135C
	public void TurnOffFX(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			this.FXParticles[i].emission.enabled = false;
		}
		this.SetupParticles();
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003198 File Offset: 0x00001398
	public void TurnOnFX(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			this.FXParticles[i].emission.enabled = true;
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000031CC File Offset: 0x000013CC
	public static List<T> Shuffle<T>(List<T> list)
	{
		System.Random random = new System.Random();
		for (int i = 0; i < list.Count; i++)
		{
			int index = random.Next(0, i);
			T value = list[index];
			list[index] = list[i];
			list[i] = value;
		}
		return list;
	}

	// Token: 0x04000052 RID: 82
	public int minSystemsActive = 2;

	// Token: 0x04000053 RID: 83
	public int maxSystemsActive = 4;

	// Token: 0x04000054 RID: 84
	public float minActiveTime = 10f;

	// Token: 0x04000055 RID: 85
	public float maxActiveTime = 30f;

	// Token: 0x04000056 RID: 86
	private int numActive;

	// Token: 0x04000057 RID: 87
	private float timeActive;

	// Token: 0x04000058 RID: 88
	private float effectsTimer;

	// Token: 0x04000059 RID: 89
	private bool timeFX;

	// Token: 0x0400005A RID: 90
	public List<ParticleSystem> FXParticles = new List<ParticleSystem>();
}
