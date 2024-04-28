using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007BE RID: 1982
	public class MorsecodeFlasher : MonoBehaviour
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x000B530E File Offset: 0x000B350E
		private void FixedUpdate()
		{
			this.age -= Time.fixedDeltaTime;
			if (this.age <= 0f)
			{
				this.age = this.messageRepeatDelay;
				this.PlayMorseCodeMessage(this.morsecodeMessage);
			}
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000B5347 File Offset: 0x000B3547
		public void PlayMorseCodeMessage(string message)
		{
			base.StartCoroutine("_PlayMorseCodeMessage", message);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000B5356 File Offset: 0x000B3556
		private IEnumerator _PlayMorseCodeMessage(string message)
		{
			Regex regex = new Regex("[^A-z0-9 ]");
			message = regex.Replace(message.ToUpper(), "");
			foreach (char c in message)
			{
				if (c == ' ')
				{
					yield return new WaitForSeconds(this.spaceDelay);
				}
				else
				{
					int num = (int)(c - 'A');
					if (num < 0)
					{
						num = (int)(c - '0' + '\u001a');
					}
					string text2 = this.alphabet[num];
					foreach (int num2 in text2)
					{
						float num3 = this.dotDuration;
						if (num2 == 45)
						{
							num3 = this.dashDuration;
						}
						base.StartCoroutine("_FlashMorseCodeObject", num3);
						yield return new WaitForSeconds(num3 + this.delayBetweenCharacters);
					}
					string text3 = null;
				}
			}
			string text = null;
			yield break;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000B536C File Offset: 0x000B356C
		private IEnumerator _FlashMorseCodeObject(float duration)
		{
			this.flashRootObject.SetActive(true);
			yield return new WaitForSeconds(duration);
			this.flashRootObject.SetActive(false);
			yield break;
		}

		// Token: 0x04002D3E RID: 11582
		private string[] alphabet = new string[]
		{
			".-",
			"-...",
			"-.-.",
			"-..",
			".",
			"..-.",
			"--.",
			"....",
			"..",
			".---",
			"-.-",
			".-..",
			"--",
			"-.",
			"---",
			".--.",
			"--.-",
			".-.",
			"...",
			"-",
			"..-",
			"...-",
			".--",
			"-..-",
			"-.--",
			"--..",
			"-----",
			".----",
			"..---",
			"...--",
			"....-",
			".....",
			"-....",
			"--...",
			"---..",
			"----."
		};

		// Token: 0x04002D3F RID: 11583
		public string morsecodeMessage;

		// Token: 0x04002D40 RID: 11584
		public float spaceDelay;

		// Token: 0x04002D41 RID: 11585
		public float delayBetweenCharacters;

		// Token: 0x04002D42 RID: 11586
		public float dotDuration;

		// Token: 0x04002D43 RID: 11587
		public float dashDuration;

		// Token: 0x04002D44 RID: 11588
		public float messageRepeatDelay;

		// Token: 0x04002D45 RID: 11589
		public GameObject flashRootObject;

		// Token: 0x04002D46 RID: 11590
		private float age;
	}
}
