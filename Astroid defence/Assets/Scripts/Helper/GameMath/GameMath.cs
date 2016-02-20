using UnityEngine;
using System.Collections;
public static class Math {
	public static Vector3 VectorToIntercept (Rigidbody toIntercept, Vector3 fromPosition, float bulletVelocity) {
		Vector3 toTarget = toIntercept.position - fromPosition;
		
		float a = Vector3.Dot (toIntercept.velocity, toIntercept.velocity) - bulletVelocity * bulletVelocity;
		float b = 2 * Vector3.Dot (toIntercept.velocity, toTarget);
		float c = Vector3.Dot (toTarget, toTarget);
		
		float positiveQuad = PositiveQuadratic (a,b,c);
		float negativeQuad = NegativeQuadratic (a,b,c);
		
		float time = positiveQuad > negativeQuad ? positiveQuad : negativeQuad;
		
		Vector3 targetPos = toIntercept.velocity;
		targetPos *= time;
		targetPos += toIntercept.position;
		
		Vector3 bulletVel = (targetPos - fromPosition).normalized;
		return bulletVel;
	}
	public static Vector3 SetMag (Vector3 vector, float magnitude) {
		return vector.normalized * magnitude;
	}
	public static float PositiveQuadratic (float a, float b, float c)  {
		return (-b + Mathf.Sqrt (b * b - 4 * a * c)) / (2 * a);
	}
	public static float NegativeQuadratic (float a, float b, float c)  {
		return (-b - Mathf.Sqrt (b * b - 4 * a * c)) / (2 * a);
	}
}