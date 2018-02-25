using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
	public float forwardSpeed = 10;
	public float jumpHeight = 7;
    public Text score;


	private bool m_turn = false;
	private bool landed = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        score.text = (Mathf.Round(10 * transform.position.z)).ToString();
        
		Vector3 vel = rb.velocity;
		if (m_turn)
		{
			vel.y = jumpHeight;
			m_turn = false;
		}
		vel.z = forwardSpeed;
		GetComponent<Rigidbody> ().velocity = vel;

        if (transform.position.y <= -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}

	void OnCollisionEnter (Collision col)
	{
		landed = true;
	}
}
