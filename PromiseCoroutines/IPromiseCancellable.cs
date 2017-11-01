using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSG {
	public interface IPromiseCancellable : IPromise {
		void Cancel();
	}
}
