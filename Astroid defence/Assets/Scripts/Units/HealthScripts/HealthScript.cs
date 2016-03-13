using UnityEngine;
using System.Collections;
using Game.Resource;

//script used for enemies
public class HealthScript : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;

	public GameObject reactor;//saves ref to reactor gameObject

	public virtual void Start () {
		currentHealth = maxHealth;
		reactor = GameObject.FindGameObjectWithTag ("reactor");
	}
	public virtual void TakeDamage (float damageAmount) {
		currentHealth -= damageAmount;
		if (currentHealth < 0) {
			GameResources.GetIron(20);//when enemy dies, player gets some iron
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject == reactor) {
			//if enemy hit the reactor, it destroys itself and the reactor loses life
			GameResources.damageShip(2);//for now, damage is hard coded
			Destroy(gameObject);
		}

	}
}
