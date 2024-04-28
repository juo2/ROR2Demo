using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rewired;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A25 RID: 2597
	public class SaveFieldAttribute : Attribute
	{
		// Token: 0x06003C0C RID: 15372 RVA: 0x000F8C74 File Offset: 0x000F6E74
		public void Setup(FieldInfo fieldInfo)
		{
			this.fieldInfo = fieldInfo;
			Type fieldType = fieldInfo.FieldType;
			this.fieldName = fieldInfo.Name;
			if (this.explicitSetupMethod != null)
			{
				MethodInfo method = typeof(SaveFieldAttribute).GetMethod(this.explicitSetupMethod, BindingFlags.Instance | BindingFlags.Public);
				if (method == null)
				{
					Debug.LogErrorFormat("Explicit setup {0} specified by field {1} could not be found. Use the nameof() operator to ensure the method exists.", Array.Empty<object>());
					return;
				}
				if (method.GetParameters().Length > 1)
				{
					Debug.LogErrorFormat("Explicit setup method {0} for field {1} must have one parameter.", new object[]
					{
						this.explicitSetupMethod,
						fieldInfo.Name
					});
					return;
				}
				method.Invoke(this, new object[]
				{
					fieldInfo
				});
				return;
			}
			else
			{
				if (fieldType == typeof(string))
				{
					this.SetupString(fieldInfo);
					return;
				}
				if (fieldType == typeof(int))
				{
					this.SetupInt(fieldInfo);
					return;
				}
				if (fieldType == typeof(uint))
				{
					this.SetupUint(fieldInfo);
					return;
				}
				if (fieldType == typeof(float))
				{
					this.SetupFloat(fieldInfo);
					return;
				}
				if (fieldType == typeof(bool))
				{
					this.SetupBool(fieldInfo);
					return;
				}
				if (fieldType == typeof(SurvivorDef))
				{
					this.SetupSurvivorDef(fieldInfo);
					return;
				}
				Debug.LogErrorFormat("No explicit setup method or supported type for save field {0}", new object[]
				{
					fieldInfo.Name
				});
				return;
			}
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000F8DCC File Offset: 0x000F6FCC
		public void SetupString(FieldInfo fieldInfo)
		{
			this.getter = ((UserProfile userProfile) => (string)fieldInfo.GetValue(userProfile));
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				fieldInfo.SetValue(userProfile, valueString);
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				fieldInfo.SetValue(destProfile, fieldInfo.GetValue(srcProfile));
			};
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000F8E1C File Offset: 0x000F701C
		public void SetupFloat(FieldInfo fieldInfo)
		{
			this.getter = ((UserProfile userProfile) => TextSerialization.ToStringInvariant((float)fieldInfo.GetValue(userProfile)));
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				float num;
				if (TextSerialization.TryParseInvariant(valueString, out num))
				{
					fieldInfo.SetValue(userProfile, num);
				}
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				fieldInfo.SetValue(destProfile, fieldInfo.GetValue(srcProfile));
			};
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000F8E6C File Offset: 0x000F706C
		public void SetupInt(FieldInfo fieldInfo)
		{
			this.getter = ((UserProfile userProfile) => TextSerialization.ToStringInvariant((int)fieldInfo.GetValue(userProfile)));
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				int num;
				if (TextSerialization.TryParseInvariant(valueString, out num))
				{
					fieldInfo.SetValue(userProfile, num);
				}
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				fieldInfo.SetValue(destProfile, fieldInfo.GetValue(srcProfile));
			};
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x000F8EBC File Offset: 0x000F70BC
		public void SetupUint(FieldInfo fieldInfo)
		{
			this.getter = ((UserProfile userProfile) => TextSerialization.ToStringInvariant((uint)fieldInfo.GetValue(userProfile)));
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				uint num;
				if (TextSerialization.TryParseInvariant(valueString, out num))
				{
					fieldInfo.SetValue(userProfile, num);
				}
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				fieldInfo.SetValue(destProfile, fieldInfo.GetValue(srcProfile));
			};
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x000F8F0C File Offset: 0x000F710C
		public void SetupBool(FieldInfo fieldInfo)
		{
			this.getter = delegate(UserProfile userProfile)
			{
				if (!(bool)fieldInfo.GetValue(userProfile))
				{
					return "0";
				}
				return "1";
			};
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				int num;
				if (TextSerialization.TryParseInvariant(valueString, out num))
				{
					fieldInfo.SetValue(userProfile, num > 0);
				}
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				fieldInfo.SetValue(destProfile, fieldInfo.GetValue(srcProfile));
			};
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x000F8F5C File Offset: 0x000F715C
		public void SetupTokenList(FieldInfo fieldInfo)
		{
			this.getter = ((UserProfile userProfile) => string.Join(" ", (List<string>)fieldInfo.GetValue(userProfile)));
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				List<string> list = (List<string>)fieldInfo.GetValue(userProfile);
				list.Clear();
				foreach (string item in valueString.Split(new char[]
				{
					' '
				}))
				{
					list.Add(item);
				}
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				List<string> src = (List<string>)fieldInfo.GetValue(srcProfile);
				List<string> dest = (List<string>)fieldInfo.GetValue(destProfile);
				Util.CopyList<string>(src, dest);
			};
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000F8FAC File Offset: 0x000F71AC
		public void SetupPickupsSet(FieldInfo fieldInfo)
		{
			this.getter = delegate(UserProfile userProfile)
			{
				bool[] pickupsSet = (bool[])fieldInfo.GetValue(userProfile);
				return string.Join(" ", from pickupDef in PickupCatalog.allPickups
				where pickupsSet[pickupDef.pickupIndex.value]
				select pickupDef.internalName);
			};
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				bool[] array = (bool[])fieldInfo.GetValue(userProfile);
				Array.Clear(array, 0, 0);
				string[] array2 = valueString.Split(new char[]
				{
					' '
				});
				for (int i = 0; i < array2.Length; i++)
				{
					PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(array2[i]);
					if (pickupIndex.isValid)
					{
						array[pickupIndex.value] = true;
					}
				}
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				Array sourceArray = (bool[])fieldInfo.GetValue(srcProfile);
				bool[] array = (bool[])fieldInfo.GetValue(destProfile);
				Array.Copy(sourceArray, array, array.Length);
			};
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000F8FFC File Offset: 0x000F71FC
		public void SetupSurvivorDef(FieldInfo fieldInfo)
		{
			this.getter = ((UserProfile userProfile) => ((SurvivorDef)fieldInfo.GetValue(userProfile)).cachedName);
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				SurvivorDef value = SurvivorCatalog.FindSurvivorDef(valueString);
				fieldInfo.SetValue(userProfile, value);
			};
			this.copier = new Action<UserProfile, UserProfile>(this.DefaultCopier);
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x000F904C File Offset: 0x000F724C
		public void SetupKeyboardMap(FieldInfo fieldInfo)
		{
			this.SetupControllerMap(fieldInfo, ControllerType.Keyboard);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x000F9056 File Offset: 0x000F7256
		public void SetupMouseMap(FieldInfo fieldInfo)
		{
			this.SetupControllerMap(fieldInfo, ControllerType.Mouse);
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x000F9060 File Offset: 0x000F7260
		public void SetupJoystickMap(FieldInfo fieldInfo)
		{
			this.SetupControllerMap(fieldInfo, ControllerType.Joystick);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000F906C File Offset: 0x000F726C
		private void SetupControllerMap(FieldInfo fieldInfo, ControllerType controllerType)
		{
			this.getter = delegate(UserProfile userProfile)
			{
				ControllerMap controllerMap = (ControllerMap)fieldInfo.GetValue(userProfile);
				return ((controllerMap != null) ? controllerMap.ToXmlString() : null) ?? string.Empty;
			};
			this.setter = delegate(UserProfile userProfile, string valueString)
			{
				fieldInfo.SetValue(userProfile, ControllerMap.CreateFromXml(controllerType, valueString));
			};
			this.copier = delegate(UserProfile srcProfile, UserProfile destProfile)
			{
				switch (controllerType)
				{
				case ControllerType.Keyboard:
					fieldInfo.SetValue(destProfile, new KeyboardMap((KeyboardMap)fieldInfo.GetValue(srcProfile)));
					return;
				case ControllerType.Mouse:
					fieldInfo.SetValue(destProfile, new MouseMap((MouseMap)fieldInfo.GetValue(srcProfile)));
					return;
				case ControllerType.Joystick:
					fieldInfo.SetValue(destProfile, new JoystickMap((JoystickMap)fieldInfo.GetValue(srcProfile)));
					return;
				default:
					throw new NotImplementedException();
				}
			};
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x000F90C3 File Offset: 0x000F72C3
		private void DefaultCopier(UserProfile srcProfile, UserProfile destProfile)
		{
			this.fieldInfo.SetValue(destProfile, this.fieldInfo.GetValue(srcProfile));
		}

		// Token: 0x04003B93 RID: 15251
		public Action<UserProfile, string> setter;

		// Token: 0x04003B94 RID: 15252
		public Func<UserProfile, string> getter;

		// Token: 0x04003B95 RID: 15253
		public Action<UserProfile, UserProfile> copier;

		// Token: 0x04003B96 RID: 15254
		public string defaultValue = string.Empty;

		// Token: 0x04003B97 RID: 15255
		public string fieldName;

		// Token: 0x04003B98 RID: 15256
		public string explicitSetupMethod;

		// Token: 0x04003B99 RID: 15257
		private FieldInfo fieldInfo;
	}
}
