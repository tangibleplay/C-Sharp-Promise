#if DT_CORE_MODULE
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RSG;

namespace DT {
	public partial class CoroutineWrapper {
		// PRAGMA MARK - Static Public Interface
		public static IPromiseCancellable StartPromiseCoroutine(IEnumerator wrappedCoroutine) {
			var promise = new PromiseCoroutineWrapper();
			var coroutine = CoroutineWrapper.StartCoroutine(PromiseWrapperCoroutine(promise, wrappedCoroutine));
			promise.Init(coroutine);
			return promise;
		}


		// PRAGMA MARK - Internal
		private static IEnumerator PromiseWrapperCoroutine(Promise promise, IEnumerator wrappedCoroutine) {
			while (wrappedCoroutine != null && wrappedCoroutine.MoveNext()) {
				yield return wrappedCoroutine.Current;
			}

			promise.Resolve();
		}

		private class PromiseCoroutineWrapper : Promise, IPromiseCancellable {
			// PRAGMA MARK - IPromiseCancellable Implementation
			void IPromiseCancellable.Cancel() {
				if (coroutine_ == null) {
					Debug.LogWarning("Cancel - no coroutine to cancel!");
				}

				coroutine_.Cancel();
				coroutine_ = null;
			}


			// PRAGMA MARK - Public Interface
			public void Init(CoroutineWrapper coroutine) {
				coroutine_ = coroutine;
			}


			// PRAGMA MARK - Internal
			private CoroutineWrapper coroutine_;
		}
	}
}
#endif