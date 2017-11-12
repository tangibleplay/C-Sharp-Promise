using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSG {
	public static class MonoBehaviourExtensions {
		// PRAGMA MARK - Static Public Interface
		public static IPromiseCancellable DelayPromise(this MonoBehaviour m, float delay) {
			return m.StartPromiseCoroutine(DelayCoroutine(delay));
		}


		// PRAGMA MARK - Static Internal
		private static IEnumerator DelayCoroutine(float delay) {
			yield return new WaitForSeconds(delay);
		}
	}
}
