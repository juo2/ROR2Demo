using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200079E RID: 1950
	[RequireComponent(typeof(CharacterBody))]
	[RequireComponent(typeof(CharacterMotor))]
	public class LoopSoundWhileCharacterMoving : MonoBehaviour
	{
		// Token: 0x06002924 RID: 10532 RVA: 0x000B232C File Offset: 0x000B052C
		private void Start()
		{
			this.isLooping = false;
			this.body = base.GetComponent<CharacterBody>();
			this.motor = base.GetComponent<CharacterMotor>();
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000B234D File Offset: 0x000B054D
		private void OnDestroy()
		{
			if (this.isLooping)
			{
				this.StopLoop();
			}
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000B2360 File Offset: 0x000B0560
		private void FixedUpdate()
		{
			if (this.body && this.motor)
			{
				if (this.isLooping)
				{
					float magnitude = this.motor.velocity.magnitude;
					if (this.applyScale)
					{
						float num = this.body.moveSpeed;
						float num2 = num;
						if (!this.body.isSprinting)
						{
							num *= this.body.sprintingSpeedMultiplier;
						}
						else
						{
							num2 /= this.body.sprintingSpeedMultiplier;
						}
						float in_value = Mathf.Lerp(0f, 50f, magnitude / num2) + Mathf.Lerp(0f, 50f, (magnitude - num2) / (num - num2));
						AkSoundEngine.SetRTPCValueByPlayingID("charMultSpeed", in_value, this.soundId);
					}
					if ((this.body.isSprinting && this.disableWhileSprinting) || (this.requireGrounded && !this.motor.isGrounded) || magnitude < this.minSpeed)
					{
						this.StopLoop();
						return;
					}
				}
				else if ((!this.body.isSprinting || !this.disableWhileSprinting) && (!this.requireGrounded || this.motor.isGrounded) && this.motor.velocity.sqrMagnitude >= this.minSpeed)
				{
					this.StartLoop();
				}
			}
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000B24A8 File Offset: 0x000B06A8
		private void StartLoop()
		{
			this.soundId = Util.PlaySound(this.startSoundName, base.gameObject);
			this.isLooping = true;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000B24C8 File Offset: 0x000B06C8
		private void StopLoop()
		{
			Util.PlaySound(this.stopSoundName, base.gameObject);
			this.isLooping = false;
		}

		// Token: 0x04002C99 RID: 11417
		[SerializeField]
		private string startSoundName;

		// Token: 0x04002C9A RID: 11418
		[SerializeField]
		private string stopSoundName;

		// Token: 0x04002C9B RID: 11419
		[SerializeField]
		private float minSpeed;

		// Token: 0x04002C9C RID: 11420
		[SerializeField]
		private bool requireGrounded;

		// Token: 0x04002C9D RID: 11421
		[SerializeField]
		private bool disableWhileSprinting;

		// Token: 0x04002C9E RID: 11422
		[SerializeField]
		private bool applyScale;

		// Token: 0x04002C9F RID: 11423
		private CharacterBody body;

		// Token: 0x04002CA0 RID: 11424
		private CharacterMotor motor;

		// Token: 0x04002CA1 RID: 11425
		private bool isLooping;

		// Token: 0x04002CA2 RID: 11426
		private uint soundId;
	}
}
