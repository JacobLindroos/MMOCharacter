using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Fade material, Inside object: " + other.transform.name);
        other.GetComponent<MeshRenderer>().enabled = false;
	}

    private void OnTriggerExit(Collider other)
    {
		Debug.Log("Fade out material: " + other.transform.name);
		other.GetComponent<MeshRenderer>().enabled = true;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
