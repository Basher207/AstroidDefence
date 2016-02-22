using UnityEngine;
using System.Collections;

namespace Game.Guns {
	public abstract class Gun : MonoBehaviour {
		public virtual void RotateTo (Quaternion RotateTo) {
			transform.rotation = RotateTo;
		}
	}
}