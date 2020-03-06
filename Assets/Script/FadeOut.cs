using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public float maxRayDistance = 10f;
	private GameObject hitObject;
	private bool hitGameObject = false;


	void FixedUpdate()
    {
		Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

		Debug.DrawLine(transform.position, transform.position + transform.forward * maxRayDistance, Color.red);

		if (Physics.Raycast(ray, out hit, maxRayDistance) && hit.collider.gameObject.layer == 9)
		{
			hitObject = hit.transform.gameObject;
			print("Looking at " + hit.transform.name);

			if (hit.distance < 0.5f )
			{
				print("change material of wall object");
				hitObject.GetComponent<MeshRenderer>().enabled = false;
			}
			if (hit.distance > 0.5f)
			{
				hitObject.GetComponent<MeshRenderer>().enabled = true;
			}
			hitGameObject = true;
		}

		if (Physics.Raycast(ray, out hit, maxRayDistance) && hit.collider.gameObject.layer != 9)
		{
			if (hitGameObject)
			{
				hitObject.GetComponent<MeshRenderer>().enabled = true;

				hitGameObject = false;
			}
		}

		//hur återställer jag meshen av senast träffade spelobjekt när jag int längre träffar det spelobjektet
		// - referens till det senast träffade spelobjektet
		// - bool som kollar om jag precis har träffat ett spelobjekt



	}

}
