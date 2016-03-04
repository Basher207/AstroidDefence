using UnityEngine;
using System.Collections;

public class ColorBasedHS : HealthScript {

	Material thisMaterial;

	[SerializeField] public Color maxHealthColor;
	[SerializeField] public Color deadColor;

	public override void Start () {
		base.Start ();
		thisMaterial = GetComponent<MeshRenderer> ().material;
		RecalculateColor ();
	}
	public override void TakeDamage (float damageAmount)
	{
		base.TakeDamage (damageAmount);
		RecalculateColor ();
	}
	public void RecalculateColor () {
		Color newColor = Color.Lerp (deadColor, maxHealthColor, currentHealth / base.maxHealth);
		thisMaterial.SetColor ("_Color", newColor);

	}
}
