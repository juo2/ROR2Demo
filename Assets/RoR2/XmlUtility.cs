using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using RoR2.Stats;

namespace RoR2
{
	// Token: 0x020009EC RID: 2540
	public static class XmlUtility
	{
		// Token: 0x06003AC0 RID: 15040 RVA: 0x000F3983 File Offset: 0x000F1B83
		private static XElement CreateStringField(string name, string value)
		{
			return new XElement(name, value);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000F3991 File Offset: 0x000F1B91
		private static XElement CreateUintField(string name, uint value)
		{
			return new XElement(name, TextSerialization.ToStringInvariant(value));
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000F39A4 File Offset: 0x000F1BA4
		private static XElement CreateStatsField(string name, StatSheet statSheet)
		{
			XElement xelement = new XElement(name);
			for (int i = 0; i < statSheet.fields.Length; i++)
			{
				XElement xelement2 = new XElement("stat", new XText(statSheet.fields[i].ToString()));
				xelement2.SetAttributeValue("name", statSheet.fields[i].name);
				xelement.Add(xelement2);
			}
			int unlockableCount = statSheet.GetUnlockableCount();
			for (int j = 0; j < unlockableCount; j++)
			{
				UnlockableDef unlockable = statSheet.GetUnlockable(j);
				XElement content = new XElement("unlock", new XText(unlockable.cachedName));
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000F3A6B File Offset: 0x000F1C6B
		private static XElement CreateLoadoutField(string name, Loadout loadout)
		{
			return loadout.ToXml(name);
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000F3A74 File Offset: 0x000F1C74
		private static uint GetUintField(XElement container, string fieldName, uint defaultValue)
		{
			XElement xelement = container.Element(fieldName);
			if (xelement != null)
			{
				XNode firstNode = xelement.FirstNode;
				if (firstNode != null && firstNode.NodeType == XmlNodeType.Text)
				{
					uint result;
					if (!TextSerialization.TryParseInvariant(((XText)firstNode).Value, out result))
					{
						return defaultValue;
					}
					return result;
				}
			}
			return defaultValue;
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000F3AC0 File Offset: 0x000F1CC0
		private static string GetStringField(XElement container, string fieldName, string defaultValue)
		{
			XElement xelement = container.Element(fieldName);
			if (xelement != null)
			{
				XNode firstNode = xelement.FirstNode;
				if (firstNode != null && firstNode.NodeType == XmlNodeType.Text)
				{
					return ((XText)firstNode).Value;
				}
			}
			return defaultValue;
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000F3B00 File Offset: 0x000F1D00
		private static void GetStatsField(XElement container, string fieldName, StatSheet dest)
		{
			XElement xelement = container.Element(fieldName);
			if (xelement == null)
			{
				return;
			}
			foreach (XElement xelement2 in from element in xelement.Elements()
			where element.Name == "stat"
			select element)
			{
				XAttribute xattribute = xelement2.Attributes().FirstOrDefault((XAttribute attribute) => attribute.Name == "name");
				string statName = (xattribute != null) ? xattribute.Value : null;
				XText xtext = xelement2.Nodes().FirstOrDefault((XNode node) => node.NodeType == XmlNodeType.Text) as XText;
				string value = (xtext != null) ? xtext.Value : null;
				dest.SetStatValueFromString(StatDef.Find(statName), value);
			}
			foreach (XElement xelement3 in from element in xelement.Elements()
			where element.Name == "unlock"
			select element)
			{
				XText xtext2 = xelement3.Nodes().FirstOrDefault((XNode node) => node.NodeType == XmlNodeType.Text) as XText;
				UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef((xtext2 != null) ? xtext2.Value : null);
				if (unlockableDef != null)
				{
					dest.AddUnlockable(unlockableDef);
				}
			}
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000F3CAC File Offset: 0x000F1EAC
		private static void GetLoadoutField(XElement container, string fieldName, Loadout dest)
		{
			XElement xelement = container.Element(fieldName);
			if (xelement == null)
			{
				return;
			}
			Loadout loadout = new Loadout();
			if (!loadout.FromXml(xelement))
			{
				return;
			}
			loadout.Copy(dest);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000F3CE4 File Offset: 0x000F1EE4
		public static XDocument ToXml(UserProfile userProfile)
		{
			object[] array = new object[UserProfile.saveFields.Length];
			for (int i = 0; i < UserProfile.saveFields.Length; i++)
			{
				SaveFieldAttribute saveFieldAttribute = UserProfile.saveFields[i];
				array[i] = XmlUtility.CreateStringField(saveFieldAttribute.fieldName, saveFieldAttribute.getter(userProfile));
			}
			object[] element = new object[]
			{
				XmlUtility.CreateStatsField("stats", userProfile.statSheet),
				XmlUtility.CreateUintField("tutorialDifficulty", userProfile.tutorialDifficulty.showCount),
				XmlUtility.CreateUintField("tutorialEquipment", userProfile.tutorialEquipment.showCount),
				XmlUtility.CreateUintField("tutorialSprint", userProfile.tutorialSprint.showCount),
				XmlUtility.CreateLoadoutField("loadout", userProfile.loadout)
			};
			return new XDocument(new object[]
			{
				new XElement("UserProfile", array.Append(element).ToArray<object>())
			});
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000F3DD0 File Offset: 0x000F1FD0
		public static UserProfile FromXml(XDocument doc)
		{
			UserProfile userProfile = new UserProfile();
			XElement root = doc.Root;
			foreach (SaveFieldAttribute saveFieldAttribute in UserProfile.saveFields)
			{
				string stringField = XmlUtility.GetStringField(root, saveFieldAttribute.fieldName, null);
				if (stringField != null)
				{
					saveFieldAttribute.setter(userProfile, stringField);
				}
			}
			XmlUtility.GetStatsField(root, "stats", userProfile.statSheet);
			XmlUtility.GetLoadoutField(root, "loadout", userProfile.loadout);
			userProfile.tutorialDifficulty.showCount = XmlUtility.GetUintField(root, "tutorialDifficulty", userProfile.tutorialDifficulty.showCount);
			userProfile.tutorialEquipment.showCount = XmlUtility.GetUintField(root, "tutorialEquipment", userProfile.tutorialEquipment.showCount);
			userProfile.tutorialSprint.showCount = XmlUtility.GetUintField(root, "tutorialSprint", userProfile.tutorialSprint.showCount);
			return userProfile;
		}
	}
}
