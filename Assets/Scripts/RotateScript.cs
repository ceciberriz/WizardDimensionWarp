using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public Transform player;

    private const int numPlatforms = 3;
    private const float rotAngle = 360f / numPlatforms;

    private bool m_turn;
    private Quaternion m_goalRot;


    void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        if (!m_turn && h != 0)
        {
            m_turn = true;
            m_goalRot = Quaternion.Euler(0, 0, rotAngle * Mathf.Sign(h) + transform.rotation.eulerAngles.z);
        }
    }

    private void FixedUpdate()
    {
        if (m_turn)
        {
            if (Quaternion.Angle(transform.rotation, m_goalRot) <= 0.1f)
            {
                transform.rotation = m_goalRot;
                m_turn = false;
                player.position = new Vector3(0, player.position.y, player.position.z);
            }
            else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, m_goalRot, Time.deltaTime * rotateSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_goalRot, rotateSpeed);
                player.position = Vector3.Lerp(player.position, new Vector3(0, player.position.y, player.position.z), Time.deltaTime * rotateSpeed);
            }
        }
    }
}
