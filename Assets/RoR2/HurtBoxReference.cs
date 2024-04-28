using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200074B RID: 1867
	[Serializable]
	public struct HurtBoxReference : IEquatable<HurtBoxReference>
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x000A89AC File Offset: 0x000A6BAC
		public static HurtBoxReference FromHurtBox(HurtBox hurtBox)
		{
			HurtBoxReference result;
			if (!hurtBox)
			{
				result = default(HurtBoxReference);
				return result;
			}
			result = new HurtBoxReference
			{
				rootObject = (hurtBox.healthComponent ? hurtBox.healthComponent.gameObject : null),
				hurtBoxIndexPlusOne = (byte)(hurtBox.indexInGroup + 1)
			};
			return result;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000A8A08 File Offset: 0x000A6C08
		public static HurtBoxReference FromRootObject(GameObject rootObject)
		{
			return new HurtBoxReference
			{
				rootObject = rootObject,
				hurtBoxIndexPlusOne = 0
			};
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000A8A30 File Offset: 0x000A6C30
		public GameObject ResolveGameObject()
		{
			if (this.hurtBoxIndexPlusOne == 0)
			{
				return this.rootObject;
			}
			if (this.rootObject)
			{
				ModelLocator component = this.rootObject.GetComponent<ModelLocator>();
				if (component && component.modelTransform)
				{
					HurtBoxGroup component2 = component.modelTransform.GetComponent<HurtBoxGroup>();
					if (component2 && component2.hurtBoxes != null)
					{
						int num = (int)(this.hurtBoxIndexPlusOne - 1);
						if (num < component2.hurtBoxes.Length)
						{
							return component2.hurtBoxes[num].gameObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000A8ABC File Offset: 0x000A6CBC
		public HurtBox ResolveHurtBox()
		{
			GameObject gameObject = this.ResolveGameObject();
			if (!gameObject)
			{
				return null;
			}
			return gameObject.GetComponent<HurtBox>();
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000A8AE0 File Offset: 0x000A6CE0
		public void Write(NetworkWriter writer)
		{
			writer.Write(this.rootObject);
			writer.Write(this.hurtBoxIndexPlusOne);
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000A8AFA File Offset: 0x000A6CFA
		public void Read(NetworkReader reader)
		{
			this.rootObject = reader.ReadGameObject();
			this.hurtBoxIndexPlusOne = reader.ReadByte();
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000A8B14 File Offset: 0x000A6D14
		public bool Equals(HurtBoxReference other)
		{
			return object.Equals(this.rootObject, other.rootObject) && this.hurtBoxIndexPlusOne == other.hurtBoxIndexPlusOne;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000A8B3C File Offset: 0x000A6D3C
		public override bool Equals(object obj)
		{
			if (obj is HurtBoxReference)
			{
				HurtBoxReference other = (HurtBoxReference)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000A8B63 File Offset: 0x000A6D63
		public override int GetHashCode()
		{
			return ((this.rootObject != null) ? this.rootObject.GetHashCode() : 0) * 397 ^ this.hurtBoxIndexPlusOne.GetHashCode();
		}

		// Token: 0x04002AB4 RID: 10932
		public GameObject rootObject;

		// Token: 0x04002AB5 RID: 10933
		public byte hurtBoxIndexPlusOne;
	}
}
