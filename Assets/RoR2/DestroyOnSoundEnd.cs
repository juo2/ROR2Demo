using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
[RequireComponent(typeof(AudioSource))]
public class DestroyOnSoundEnd : MonoBehaviour
{
	// Token: 0x06000104 RID: 260 RVA: 0x000057B8 File Offset: 0x000039B8
	private void Awake()
	{
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000057C6 File Offset: 0x000039C6
	private void Update()
	{
		if (!this.audioSource.isPlaying)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040000F2 RID: 242
	private AudioSource audioSource;
}
