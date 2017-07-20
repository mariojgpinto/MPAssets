using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPAssets {
	public class GPS {
		public static float Distance(float latitude1, float longitude1, float latitude2, float longitude2) {
			float earth_radius = 6371f;

			float dLat = Mathf.Deg2Rad * (latitude2 - latitude1);
			float dLon = Mathf.Deg2Rad * (longitude2 - longitude1);

			float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) + Mathf.Cos(Mathf.Deg2Rad * latitude1) * Mathf.Cos(Mathf.Deg2Rad * latitude2) * Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
			float c = 2 * Mathf.Asin(Mathf.Sqrt(a));
			float d = earth_radius * c;

			float toMeters = d * 1000;

			return toMeters;
		}
	}
}