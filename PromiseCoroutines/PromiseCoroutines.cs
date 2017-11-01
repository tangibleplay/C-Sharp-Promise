using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSG {
	public static class PromiseCoroutines {
		// PRAGMA MARK - Static Public Interface
		public static IPromiseCancellable StartPromiseCoroutine(this MonoBehaviour m, IEnumerator wrappedCoroutine) {
			var promise = new PromiseCoroutine();
			var coroutine = m.StartCoroutine(PromiseWrapperCoroutine(promise, wrappedCoroutine));
			promise.Init(m, coroutine);
			return promise;
		}


		// PRAGMA MARK - Internal
		private static IEnumerator PromiseWrapperCoroutine(Promise promise, IEnumerator wrappedCoroutine) {
			yield return wrappedCoroutine;

			promise.Resolve();
		}

		private class PromiseCoroutine : Promise, IPromiseCancellable {
			// PRAGMA MARK - IPromiseCancellable Implementation
			void IPromiseCancellable.Cancel() {
				if (coroutine_ == null || behaviour_ == null) {
					Debug.LogWarning("Cancel - missing coroutine or behaviour, can't cancel!");
					return;
				}

				behaviour_.StopCoroutine(coroutine_);
				coroutine_ = null;
			}



			// PRAGMA MARK - Public Interface
			public void Init(MonoBehaviour behaviour, Coroutine coroutine) {
				behaviour_ = behaviour;
				coroutine_ = coroutine;
			}


			// PRAGMA MARK - Internal
			private MonoBehaviour behaviour_;
			private Coroutine coroutine_;
		}
	}
}
