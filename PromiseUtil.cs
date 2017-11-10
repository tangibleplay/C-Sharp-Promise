using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RSG {
	public static class PromiseUtil {
		// PRAGMA MARK - Public Interface
		public static IPromise ChainPromise(IPromise promise, Func<IPromise> promiseCallback) {
			if (promise == null) {
				return promiseCallback.Invoke();
			} else {
				return promise.Then(promiseCallback);
			}
		}
	}
}
