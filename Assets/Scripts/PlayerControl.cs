using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {
	public float forwardSpeed = 10;
	public float jumpHeight = 7;



	private bool m_turn = false;
	private bool landed = true;
	
	// Update is called once per frame
	void Update () {
		float h = CrossPlatformInputManager.GetAxis("Jump");
		if (!m_turn && h != 0 && landed)
		{
			m_turn = true;
			landed = false;
		}
	}

	private void FixedUpdate()
	{
		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		if (m_turn)
		{
			vel.y = jumpHeight;
			m_turn = false;
		}
		vel.z = forwardSpeed;
		GetComponent<Rigidbody> ().velocity = vel;
	}

	void OnCollisionEnter (Collision col)
	{
		landed = true;
		Debug.Log ("Landed");
	}
}
