using System;
using System.Collections.Generic;
using System.Reflection;
using HG;
using HG.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BCC RID: 3020
	public abstract class BaseItemBodyBehavior : MonoBehaviour
	{
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x0011DD9A File Offset: 0x0011BF9A
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x0011DDA2 File Offset: 0x0011BFA2
		public CharacterBody body { get; private set; }

		// Token: 0x060044B0 RID: 17584 RVA: 0x0011DDAB File Offset: 0x0011BFAB
		protected void Awake()
		{
			this.body = BaseItemBodyBehavior.earlyAssignmentBody;
			BaseItemBodyBehavior.earlyAssignmentBody = null;
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x0011DDC0 File Offset: 0x0011BFC0
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			List<BaseItemBodyBehavior.ItemTypePair> list = new List<BaseItemBodyBehavior.ItemTypePair>();
			List<BaseItemBodyBehavior.ItemTypePair> list2 = new List<BaseItemBodyBehavior.ItemTypePair>();
			List<BaseItemBodyBehavior.ItemTypePair> list3 = new List<BaseItemBodyBehavior.ItemTypePair>();
			List<BaseItemBodyBehavior.ItemDefAssociationAttribute> list4 = new List<BaseItemBodyBehavior.ItemDefAssociationAttribute>();
			SearchableAttribute.GetInstances<BaseItemBodyBehavior.ItemDefAssociationAttribute>(list4);
			Type typeFromHandle = typeof(BaseItemBodyBehavior);
			Type typeFromHandle2 = typeof(ItemDef);
			foreach (BaseItemBodyBehavior.ItemDefAssociationAttribute itemDefAssociationAttribute in list4)
			{
				MethodInfo methodInfo;
				if ((methodInfo = (itemDefAssociationAttribute.target as MethodInfo)) == null)
				{
					Debug.LogError("ItemDefAssociationAttribute cannot be applied to object of type '" + ((itemDefAssociationAttribute != null) ? itemDefAssociationAttribute.GetType().FullName : null) + "'");
				}
				else if (!methodInfo.IsStatic)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"ItemDefAssociationAttribute cannot be applied to method ",
						methodInfo.DeclaringType.FullName,
						".",
						methodInfo.Name,
						": Method is not static."
					}));
				}
				else
				{
					Type type = itemDefAssociationAttribute.behaviorTypeOverride ?? methodInfo.DeclaringType;
					if (!typeFromHandle.IsAssignableFrom(type))
					{
						Debug.LogError(string.Concat(new string[]
						{
							"ItemDefAssociationAttribute cannot be applied to method ",
							methodInfo.DeclaringType.FullName,
							".",
							methodInfo.Name,
							": ",
							methodInfo.DeclaringType.FullName,
							" does not derive from ",
							typeFromHandle.FullName,
							"."
						}));
					}
					else if (type.IsAbstract)
					{
						Debug.LogError(string.Concat(new string[]
						{
							"ItemDefAssociationAttribute cannot be applied to method ",
							methodInfo.DeclaringType.FullName,
							".",
							methodInfo.Name,
							": ",
							methodInfo.DeclaringType.FullName,
							" is an abstract type."
						}));
					}
					else if (!typeFromHandle2.IsAssignableFrom(methodInfo.ReturnType))
					{
						string format = "{0} cannot be applied to method {1}.{2}: {3}.{4} returns type '{5}' instead of {6}.";
						object[] array = new object[7];
						array[0] = "ItemDefAssociationAttribute";
						array[1] = methodInfo.DeclaringType.FullName;
						array[2] = methodInfo.Name;
						array[3] = methodInfo.DeclaringType.FullName;
						array[4] = methodInfo;
						int num = 5;
						Type returnType = methodInfo.ReturnType;
						array[num] = (((returnType != null) ? returnType.FullName : null) ?? "void");
						array[6] = typeFromHandle2.FullName;
						Debug.LogError(string.Format(format, array));
					}
					else if (methodInfo.GetGenericArguments().Length != 0)
					{
						Debug.LogError(string.Format("{0} cannot be applied to method {1}.{2}: {3}.{4} must take no arguments.", new object[]
						{
							"ItemDefAssociationAttribute",
							methodInfo.DeclaringType.FullName,
							methodInfo.Name,
							methodInfo.DeclaringType.FullName,
							methodInfo
						}));
					}
					else
					{
						ItemDef itemDef = (ItemDef)methodInfo.Invoke(null, Array.Empty<object>());
						if (!itemDef)
						{
							Debug.LogError(methodInfo.DeclaringType.FullName + "." + methodInfo.Name + " returned null.");
						}
						else if (itemDef.itemIndex < (ItemIndex)0)
						{
							Debug.LogError(string.Format("{0}.{1} returned an ItemDef that's not registered in the ItemCatalog. result={2}", methodInfo.DeclaringType.FullName, methodInfo.Name, itemDef));
						}
						else
						{
							if (itemDefAssociationAttribute.useOnServer)
							{
								list.Add(new BaseItemBodyBehavior.ItemTypePair
								{
									itemIndex = itemDef.itemIndex,
									behaviorType = type
								});
							}
							if (itemDefAssociationAttribute.useOnClient)
							{
								list2.Add(new BaseItemBodyBehavior.ItemTypePair
								{
									itemIndex = itemDef.itemIndex,
									behaviorType = type
								});
							}
							if (itemDefAssociationAttribute.useOnServer || itemDefAssociationAttribute.useOnClient)
							{
								list3.Add(new BaseItemBodyBehavior.ItemTypePair
								{
									itemIndex = itemDef.itemIndex,
									behaviorType = type
								});
							}
						}
					}
				}
			}
			BaseItemBodyBehavior.server.SetItemTypePairs(list);
			BaseItemBodyBehavior.client.SetItemTypePairs(list2);
			BaseItemBodyBehavior.shared.SetItemTypePairs(list3);
			CharacterBody.onBodyAwakeGlobal += BaseItemBodyBehavior.OnBodyAwakeGlobal;
			CharacterBody.onBodyDestroyGlobal += BaseItemBodyBehavior.OnBodyDestroyGlobal;
			CharacterBody.onBodyInventoryChangedGlobal += BaseItemBodyBehavior.OnBodyInventoryChangedGlobal;
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x0011E218 File Offset: 0x0011C418
		private static ref BaseItemBodyBehavior.NetworkContextSet GetNetworkContext()
		{
			bool active = NetworkServer.active;
			bool active2 = NetworkClient.active;
			if (active)
			{
				if (active2)
				{
					return ref BaseItemBodyBehavior.shared;
				}
				return ref BaseItemBodyBehavior.server;
			}
			else
			{
				if (active2)
				{
					return ref BaseItemBodyBehavior.client;
				}
				throw new InvalidOperationException("Neither server nor client is running.");
			}
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x0011E254 File Offset: 0x0011C454
		private static void OnBodyAwakeGlobal(CharacterBody body)
		{
			BaseItemBodyBehavior[] value = BaseItemBodyBehavior.GetNetworkContext().behaviorArraysPool.Request();
			BaseItemBodyBehavior.bodyToItemBehaviors.Add(body, value);
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0011E284 File Offset: 0x0011C484
		private static void OnBodyDestroyGlobal(CharacterBody body)
		{
			BaseItemBodyBehavior[] array = BaseItemBodyBehavior.bodyToItemBehaviors[body];
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i]);
			}
			BaseItemBodyBehavior.bodyToItemBehaviors.Remove(body);
			if (NetworkServer.active || NetworkClient.active)
			{
				BaseItemBodyBehavior.GetNetworkContext().behaviorArraysPool.Return(array);
			}
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x0011E2E7 File Offset: 0x0011C4E7
		private static void OnBodyInventoryChangedGlobal(CharacterBody body)
		{
			BaseItemBodyBehavior.UpdateBodyItemBehaviorStacks(body);
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x0011E2F0 File Offset: 0x0011C4F0
		private static void UpdateBodyItemBehaviorStacks(CharacterBody body)
		{
			ref BaseItemBodyBehavior.NetworkContextSet networkContext = ref BaseItemBodyBehavior.GetNetworkContext();
			BaseItemBodyBehavior[] array = BaseItemBodyBehavior.bodyToItemBehaviors[body];
			BaseItemBodyBehavior.ItemTypePair[] itemTypePairs = networkContext.itemTypePairs;
			Inventory inventory = body.inventory;
			if (inventory)
			{
				for (int i = 0; i < itemTypePairs.Length; i++)
				{
					BaseItemBodyBehavior.ItemTypePair itemTypePair = itemTypePairs[i];
					ref BaseItemBodyBehavior behavior = ref array[i];
					BaseItemBodyBehavior.SetItemStack(body, ref behavior, itemTypePair.behaviorType, inventory.GetItemCount(itemTypePair.itemIndex));
				}
				return;
			}
			for (int j = 0; j < itemTypePairs.Length; j++)
			{
				ref BaseItemBodyBehavior ptr = ref array[j];
				if (ptr != null)
				{
					UnityEngine.Object.Destroy(ptr);
					ptr = null;
				}
			}
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x0011E394 File Offset: 0x0011C594
		private static void SetItemStack(CharacterBody body, ref BaseItemBodyBehavior behavior, Type behaviorType, int stack)
		{
			if (behavior == null != stack <= 0)
			{
				if (stack <= 0)
				{
					UnityEngine.Object.Destroy(behavior);
					behavior = null;
				}
				else
				{
					BaseItemBodyBehavior.earlyAssignmentBody = body;
					behavior = (BaseItemBodyBehavior)body.gameObject.AddComponent(behaviorType);
					BaseItemBodyBehavior.earlyAssignmentBody = null;
				}
			}
			if (behavior != null)
			{
				behavior.stack = stack;
			}
		}

		// Token: 0x0400432F RID: 17199
		public int stack;

		// Token: 0x04004330 RID: 17200
		private static BaseItemBodyBehavior.NetworkContextSet server;

		// Token: 0x04004331 RID: 17201
		private static BaseItemBodyBehavior.NetworkContextSet client;

		// Token: 0x04004332 RID: 17202
		private static BaseItemBodyBehavior.NetworkContextSet shared;

		// Token: 0x04004333 RID: 17203
		private static CharacterBody earlyAssignmentBody = null;

		// Token: 0x04004334 RID: 17204
		private static Dictionary<UnityObjectWrapperKey<CharacterBody>, BaseItemBodyBehavior[]> bodyToItemBehaviors = new Dictionary<UnityObjectWrapperKey<CharacterBody>, BaseItemBodyBehavior[]>();

		// Token: 0x02000BCD RID: 3021
		private struct ItemTypePair
		{
			// Token: 0x04004335 RID: 17205
			public ItemIndex itemIndex;

			// Token: 0x04004336 RID: 17206
			public Type behaviorType;
		}

		// Token: 0x02000BCE RID: 3022
		private struct NetworkContextSet
		{
			// Token: 0x060044BA RID: 17594 RVA: 0x0011E3FC File Offset: 0x0011C5FC
			public void SetItemTypePairs(List<BaseItemBodyBehavior.ItemTypePair> itemTypePairs)
			{
				this.itemTypePairs = itemTypePairs.ToArray();
				this.behaviorArraysPool = new FixedSizeArrayPool<BaseItemBodyBehavior>(this.itemTypePairs.Length);
			}

			// Token: 0x04004337 RID: 17207
			public BaseItemBodyBehavior.ItemTypePair[] itemTypePairs;

			// Token: 0x04004338 RID: 17208
			public FixedSizeArrayPool<BaseItemBodyBehavior> behaviorArraysPool;
		}

		// Token: 0x02000BCF RID: 3023
		[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
		[MeansImplicitUse]
		public class ItemDefAssociationAttribute : SearchableAttribute
		{
			// Token: 0x04004339 RID: 17209
			public Type behaviorTypeOverride;

			// Token: 0x0400433A RID: 17210
			public bool useOnServer = true;

			// Token: 0x0400433B RID: 17211
			public bool useOnClient = true;
		}
	}
}
