using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;

	void Awake () {
		currentHealth = maxHealth;
	}
	public void TakeDamage (float damageAmount) {
		currentHealth -= damageAmount;
		if (currentHealth < 0)
			Destroy (gameObject);
	}
}
