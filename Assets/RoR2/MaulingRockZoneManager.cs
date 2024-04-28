using System;
using System.Collections;
using System.Collections.Generic;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200000B RID: 11
public class MaulingRockZoneManager : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00002A4D File Offset: 0x00000C4D
	private void Awake()
	{
		if (!NetworkServer.active)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002A60 File Offset: 0x00000C60
	private void Start()
	{
		this.vectorBetweenStartPoints = this.startZonePoint2.position - this.startZonePoint1.position;
		this.vectorBetweenEndPoints = this.endZonePoint2.position - this.endZonePoint1.position;
		this.FireSalvo();
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002AB5 File Offset: 0x00000CB5
	private void FireSalvo()
	{
		this.salvoRockCount = UnityEngine.Random.Range(0, this.salvoMaximumCount);
		this.currentSalvoCount = 0;
		this.FireRock();
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002AD8 File Offset: 0x00000CD8
	private void FireRock()
	{
		GameObject gameObject = this.maulingRockProjectilePrefabs[UnityEngine.Random.Range(0, this.maulingRockProjectilePrefabs.Count)];
		Vector3 vector = this.startZonePoint1.position + UnityEngine.Random.Range(0f, 1f) * this.vectorBetweenStartPoints;
		Vector3 vector2 = this.endZonePoint1.position + UnityEngine.Random.Range(0f, 1f) * this.vectorBetweenEndPoints;
		MaulingRock component = gameObject.GetComponent<MaulingRock>();
		float num = UnityEngine.Random.Range(0f, 4f);
		num += component.verticalOffset;
		vector = new Vector3(vector.x, vector.y + num, vector.z);
		num = UnityEngine.Random.Range(0f, 4f);
		num += component.verticalOffset;
		vector2 = new Vector3(vector2.x, vector2.y + num, vector2.z);
		Ray ray = new Ray(vector, vector2 - vector);
		FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
		{
			projectilePrefab = gameObject,
			position = ray.origin,
			rotation = Util.QuaternionSafeLookRotation(ray.direction),
			owner = null,
			damage = component.damage * component.damageCoefficient,
			force = this.knockbackForce,
			crit = false,
			damageColorIndex = DamageColorIndex.Default,
			target = null
		};
		ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		this.currentSalvoCount++;
		base.StartCoroutine(this.WaitToFireAnotherRock());
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002C7B File Offset: 0x00000E7B
	public IEnumerator WaitToFireAnotherSalvo()
	{
		float seconds = UnityEngine.Random.Range(this.timeBetweenSalvosLow, this.timeBetweenSalvosHigh);
		yield return new WaitForSeconds(seconds);
		this.FireSalvo();
		yield break;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002C8A File Offset: 0x00000E8A
	public IEnumerator WaitToFireAnotherRock()
	{
		float seconds = UnityEngine.Random.Range(this.timeBetweenSalvoShotsLow, this.timeBetweenSalvoShotsHigh);
		if (this.currentSalvoCount >= this.salvoRockCount)
		{
			yield return null;
			base.StartCoroutine(this.WaitToFireAnotherSalvo());
		}
		else
		{
			yield return new WaitForSeconds(seconds);
			this.FireRock();
		}
		yield break;
	}

	// Token: 0x0400002C RID: 44
	public List<GameObject> maulingRockProjectilePrefabs = new List<GameObject>();

	// Token: 0x0400002D RID: 45
	public Transform startZonePoint1;

	// Token: 0x0400002E RID: 46
	public Transform startZonePoint2;

	// Token: 0x0400002F RID: 47
	public Transform endZonePoint1;

	// Token: 0x04000030 RID: 48
	public Transform endZonePoint2;

	// Token: 0x04000031 RID: 49
	public static float baseDuration = 60f;

	// Token: 0x04000032 RID: 50
	public float knockbackForce = 10000f;

	// Token: 0x04000033 RID: 51
	private Vector3 vectorBetweenStartPoints = Vector3.zero;

	// Token: 0x04000034 RID: 52
	private Vector3 vectorBetweenEndPoints = Vector3.zero;

	// Token: 0x04000035 RID: 53
	private Vector3 MediumRockBump = Vector3.zero;

	// Token: 0x04000036 RID: 54
	private Vector3 LargeRockBump = Vector3.zero;

	// Token: 0x04000037 RID: 55
	private int salvoMaximumCount = 10;

	// Token: 0x04000038 RID: 56
	private float timeBetweenSalvoShotsLow = 0.1f;

	// Token: 0x04000039 RID: 57
	private float timeBetweenSalvoShotsHigh = 1f;

	// Token: 0x0400003A RID: 58
	private float timeBetweenSalvosLow = 3f;

	// Token: 0x0400003B RID: 59
	private float timeBetweenSalvosHigh = 5f;

	// Token: 0x0400003C RID: 60
	private int salvoRockCount;

	// Token: 0x0400003D RID: 61
	private int currentSalvoCount;
}
