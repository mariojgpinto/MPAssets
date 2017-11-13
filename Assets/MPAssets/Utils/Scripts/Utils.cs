using System;
using System.Collections.Generic;

namespace MPAssets {
	public class Utils {
		/// <summary>
		/// Cleans the string removing the special characters.
		/// </summary>
		/// <param name="str">Original string.</param>
		/// <returns>Cleaned string.</returns>
		public static string RemoveSpecialCharacters(string str) {
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach (char c in str) {
				if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_') {
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Checks if the element <c>elem</c> exists on <c>list</c>.
		/// </summary>
		/// <typeparam name="T">Type of the elements.</typeparam>
		/// <param name="list">List of elements.</param>
		/// <param name="elem">Element to compare with.</param>
		/// <returns>True if exists, False otherwise.</returns>
		public static bool ExistsInList<T>(List<T> list, T elem) {
			foreach (T e in list) {
				if (elem.Equals(e))
					return true;
			}
			return false;
		}

		public static System.Collections.Generic.List<T> Randomize<T>(System.Collections.Generic.List<T> list) {
			System.Collections.Generic.List<T> randomizedList = new System.Collections.Generic.List<T>();
			Random rnd = new Random();
			while (list.Count > 0) {
				int index = rnd.Next(0, list.Count); //pick a random item from the master list
				randomizedList.Add(list[index]); //place it at the end of the randomized list
				list.RemoveAt(index);
			}
			return randomizedList;
		}

		public static System.Collections.Generic.List<int> GenerateRandomList(int min, int max, int count) {
			Random random = new Random(DateTime.Now.Millisecond);
			System.Collections.Generic.List<int> result = new System.Collections.Generic.List<int>();

			while (result.Count < count) {
				int num = random.Next(min, max);

				bool exists = false;
				for (int i = 0; i < result.Count; ++i) {
					if (result[i] == num) {
						exists = true;
						break;
					}
				}

				if (!exists)
					result.Add(num);
			}

			return result;
		}

		public static void Swap<T>(System.Collections.Generic.IList<T> list, int indexA, int indexB) {
			T tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;
		}
	}
}