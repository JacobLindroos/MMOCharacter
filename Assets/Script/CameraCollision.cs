using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

	private float fadeTimer = 0.4f;

	// Start is called before the first frame update
	void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Fade material, Inside object: " + other.transform.name);
		
		MeshRenderer wallMesh = other.GetComponent<MeshRenderer>();
		Color newColor = wallMesh.material.color;
		wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, 0.5f);
	}

    private void OnTriggerExit(Collider other)
    {
		Debug.Log("Fade out material: " + other.transform.name);
		MeshRenderer wallMesh = other.GetComponent<MeshRenderer>();
		Color newColor = wallMesh.material.color;
		wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, 1f);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
