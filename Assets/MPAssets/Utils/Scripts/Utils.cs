using UnityEngine;
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
	}
}