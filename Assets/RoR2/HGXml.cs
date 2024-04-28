using System;
using System.Collections.Generic;
using System.Xml.Linq;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x0200006B RID: 107
public static class HGXml
{
	// Token: 0x060001D3 RID: 467 RVA: 0x00008E61 File Offset: 0x00007061
	public static void Register<T>(HGXml.Serializer<T> serializer, HGXml.Deserializer<T> deserializer)
	{
		HGXml.SerializationRules<T>.defaultRules = new HGXml.SerializationRules<T>
		{
			serializer = serializer,
			deserializer = deserializer
		};
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00008E7B File Offset: 0x0000707B
	[NotNull]
	public static XElement ToXml<T>(string name, T value)
	{
		return HGXml.ToXml<T>(name, value, HGXml.SerializationRules<T>.defaultRules);
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00008E8C File Offset: 0x0000708C
	[NotNull]
	public static XElement ToXml<T>(string name, T value, HGXml.SerializationRules<T> rules)
	{
		XElement xelement = new XElement(name);
		rules.serializer(xelement, value);
		return xelement;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00008EB3 File Offset: 0x000070B3
	public static bool FromXml<T>([NotNull] XElement element, ref T value)
	{
		return HGXml.FromXml<T>(element, ref value, HGXml.SerializationRules<T>.defaultRules);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00008EC1 File Offset: 0x000070C1
	public static bool FromXml<T>([NotNull] XElement element, ref T value, HGXml.SerializationRules<T> rules)
	{
		if (rules == null)
		{
			Debug.LogFormat("Serialization rules not defined for type <{0}>", new object[]
			{
				typeof(T).Name
			});
			return false;
		}
		return rules.deserializer(element, ref value);
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00008EF7 File Offset: 0x000070F7
	public static bool FromXml<T>([NotNull] XElement element, [NotNull] Action<T> setter)
	{
		return HGXml.FromXml<T>(element, setter, HGXml.SerializationRules<T>.defaultRules);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00008F08 File Offset: 0x00007108
	public static bool FromXml<T>([NotNull] XElement element, [NotNull] Action<T> setter, [NotNull] HGXml.SerializationRules<T> rules)
	{
		T obj = default(T);
		if (HGXml.FromXml<T>(element, ref obj, rules))
		{
			setter(obj);
			return true;
		}
		return false;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00008F34 File Offset: 0x00007134
	static HGXml()
	{
		HGXml.Register<int>(delegate(XElement element, int contents)
		{
			element.Value = TextSerialization.ToStringInvariant(contents);
		}, delegate(XElement element, ref int contents)
		{
			int num;
			if (TextSerialization.TryParseInvariant(element.Value, out num))
			{
				contents = num;
				return true;
			}
			return false;
		});
		HGXml.Register<uint>(delegate(XElement element, uint contents)
		{
			element.Value = TextSerialization.ToStringInvariant(contents);
		}, delegate(XElement element, ref uint contents)
		{
			uint num;
			if (TextSerialization.TryParseInvariant(element.Value, out num))
			{
				contents = num;
				return true;
			}
			return false;
		});
		HGXml.Register<ulong>(delegate(XElement element, ulong contents)
		{
			element.Value = TextSerialization.ToStringInvariant(contents);
		}, delegate(XElement element, ref ulong contents)
		{
			ulong num;
			if (TextSerialization.TryParseInvariant(element.Value, out num))
			{
				contents = num;
				return true;
			}
			return false;
		});
		HGXml.Register<bool>(delegate(XElement element, bool contents)
		{
			element.Value = (contents ? "1" : "0");
		}, delegate(XElement element, ref bool contents)
		{
			int num;
			if (TextSerialization.TryParseInvariant(element.Value, out num))
			{
				contents = (num != 0);
				return true;
			}
			return false;
		});
		HGXml.Register<float>(delegate(XElement element, float contents)
		{
			element.Value = TextSerialization.ToStringInvariant(contents);
		}, delegate(XElement element, ref float contents)
		{
			float num;
			if (TextSerialization.TryParseInvariant(element.Value, out num))
			{
				contents = num;
				return true;
			}
			return false;
		});
		HGXml.Register<double>(delegate(XElement element, double contents)
		{
			element.Value = TextSerialization.ToStringInvariant(contents);
		}, delegate(XElement element, ref double contents)
		{
			double num;
			if (TextSerialization.TryParseInvariant(element.Value, out num))
			{
				contents = num;
				return true;
			}
			return false;
		});
		HGXml.Register<string>(delegate(XElement element, string contents)
		{
			element.Value = contents;
		}, delegate(XElement element, ref string contents)
		{
			contents = element.Value;
			return true;
		});
		HGXml.Register<Guid>(delegate(XElement element, Guid contents)
		{
			element.Value = contents.ToString();
		}, delegate(XElement element, ref Guid contents)
		{
			Guid guid;
			if (Guid.TryParse(element.Value, out guid))
			{
				contents = guid;
				return true;
			}
			return false;
		});
		HGXml.Register<DateTime>(delegate(XElement element, DateTime contents)
		{
			element.Value = TextSerialization.ToStringInvariant(contents.ToBinary());
		}, delegate(XElement element, ref DateTime contents)
		{
			long dateData;
			if (TextSerialization.TryParseInvariant(element.Value, out dateData))
			{
				try
				{
					contents = DateTime.FromBinary(dateData);
					return true;
				}
				catch (ArgumentException)
				{
				}
				return false;
			}
			return false;
		});
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000908E File Offset: 0x0000728E
	public static void Deserialize<T>(this XElement element, ref T dest)
	{
		HGXml.FromXml<T>(element, ref dest);
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00009098 File Offset: 0x00007298
	public static void Deserialize<T>(this XElement element, ref T dest, HGXml.SerializationRules<T> rules)
	{
		HGXml.FromXml<T>(element, ref dest, rules);
	}

	// Token: 0x0200006C RID: 108
	// (Invoke) Token: 0x060001DE RID: 478
	public delegate void Serializer<T>(XElement element, T contents);

	// Token: 0x0200006D RID: 109
	// (Invoke) Token: 0x060001E2 RID: 482
	public delegate bool Deserializer<T>(XElement element, ref T contents);

	// Token: 0x0200006E RID: 110
	public class SerializationRules<T>
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000090A3 File Offset: 0x000072A3
		static SerializationRules()
		{
			if (typeof(T).IsEnum)
			{
				HGXml.SerializationRules<T>.RegisterEnum();
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000090BC File Offset: 0x000072BC
		private static void RegisterEnum()
		{
			HGXml.SerializationRules<T>.<>c__DisplayClass4_0 CS$<>8__locals1 = new HGXml.SerializationRules<T>.<>c__DisplayClass4_0();
			Type typeFromHandle = typeof(T);
			CS$<>8__locals1.nameToValue = new Dictionary<string, T>();
			string[] names = Enum.GetNames(typeFromHandle);
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				Enum.Parse(typeFromHandle, names[i]);
				CS$<>8__locals1.nameToValue[text] = (T)((object)Enum.Parse(typeFromHandle, text));
			}
			CS$<>8__locals1.valueToName = new Dictionary<T, string>();
			Array values = Enum.GetValues(typeFromHandle);
			for (int j = 0; j < values.Length; j++)
			{
				object value = values.GetValue(j);
				CS$<>8__locals1.valueToName[(T)((object)value)] = Enum.GetName(typeFromHandle, value);
			}
			HGXml.SerializationRules<T>.defaultRules = new HGXml.SerializationRules<T>
			{
				serializer = new HGXml.Serializer<T>(CS$<>8__locals1.<RegisterEnum>g__Serializer|0),
				deserializer = new HGXml.Deserializer<T>(CS$<>8__locals1.<RegisterEnum>g__Deserializer|1)
			};
		}

		// Token: 0x040001D6 RID: 470
		public HGXml.Serializer<T> serializer;

		// Token: 0x040001D7 RID: 471
		public HGXml.Deserializer<T> deserializer;

		// Token: 0x040001D8 RID: 472
		public static HGXml.SerializationRules<T> defaultRules;
	}
}
