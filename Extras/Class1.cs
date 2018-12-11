using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extras
{
	public static class ExtraConverters
	{
		public static string ToUnicodeString(this byte[] bytes)
		{
			var str = Encoding.Unicode.GetString(bytes).Replace("\0", "");

			return str;
		}
		public static byte[] ToUnicodeBytes(this string str, int requiredCountOfBytes)
		{
			return Encoding.Unicode.GetBytes(str).TrimOrExpandTo(requiredCountOfBytes);
		}
		public static T[] TrimOrExpandTo<T>(this T[] array, int requiredLength)
		{
			var list = array.ToList();
			if (list.Count < requiredLength)
			{
				list.AddRange(new T[requiredLength - list.Count]);
			}
			else if (list.Count > requiredLength)
			{
				list.RemoveRange(requiredLength, list.Count - requiredLength);
			}
			return list.ToArray();
		}
	}

	public class FileHeader
	{
		public Byte WriteAllowed;
		public Byte ReadAllowed;
		public static int SizeInBytes = 2;
	}
}
