using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;

	public virtual void Start () {
		currentHealth = maxHealth;
	}
	public virtual void TakeDamage (float damageAmount) {
		currentHealth -= damageAmount;
		if (currentHealth < 0)
			Destroy (gameObject);
	}
}
