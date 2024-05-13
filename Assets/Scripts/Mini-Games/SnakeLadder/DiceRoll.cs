using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    private Rigidbody rb;
    public float rollStrength = 5f;
    private bool isResting = false;
    public Transform[] faceMarkers;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RollDice()
    {
        // Reset dice position and velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Apply force for the roll
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        rb.AddTorque(Random.Range(-rollStrength, rollStrength), Random.Range(-rollStrength, rollStrength), Random.Range(-rollStrength, rollStrength));
    }

    // Call this method after the dice comes to rest
    public int CheckDiceNumber()
    {
        float maxDot = -Mathf.Infinity;
        int topFaceIndex = -1;

        for (int i = 0; i < faceMarkers.Length; i++)
        {
            float dot = Vector3.Dot(Vector3.up, faceMarkers[i].up);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFaceIndex = i + 1; // Assuming faces are 1-indexed
            }
        }
        Debug.Log("Dice face: " + topFaceIndex);
        return topFaceIndex; // Returns the index of the top face (1 to 6)
    }

    void Update()
    {
        // Check if the Rigidbody's velocity and angular velocity are close to zero
        if (!isResting && IsRigidbodyAtRest(rb))
        {
            isResting = true;
            Debug.Log("The dice has come to rest.");
            // You can now safely check the dice result or trigger other actions
            CheckDiceNumber();
        }
        else if (isResting && !IsRigidbodyAtRest(rb))
        {
            isResting = false;
            Debug.Log("The dice is moving.");
            // The dice has started moving again, perhaps due to another roll
        }
    }

    private bool IsRigidbodyAtRest(Rigidbody rigidbody)
    {
        return rigidbody.velocity.sqrMagnitude < 0.01f && rigidbody.angularVelocity.sqrMagnitude < 0.01f;
    }
}
