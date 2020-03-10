using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Fade material, Inside object: " + other.transform.name);

		if (other.gameObject.layer == 9)
		{
			other.GetComponent<MeshRenderer>().enabled = false;
		}
	}

    private void OnTriggerExit(Collider other)
    {
		Debug.Log("Fade out material: " + other.transform.name);

		other.GetComponent<MeshRenderer>().enabled = true;
	}
}
