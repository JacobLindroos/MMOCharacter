using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public float maxRayDistance = 10f;
	private GameObject hitObject;
	private bool hitGameObject = false;

	Camera mainCamera = null;

	Vector3 CameraHalfExtends
	{
		get
		{
			Vector3 halfExtends;
			halfExtends.y = mainCamera.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * mainCamera.fieldOfView);
			halfExtends.x = halfExtends.y * mainCamera.aspect;
			halfExtends.z = 0.0f;
			return halfExtends;
		}
	}

	private void Start()
	{
		mainCamera = GetComponent<Camera>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * maxRayDistance);
		Gizmos.DrawWireCube(transform.position, CameraHalfExtends);
	}


	void FixedUpdate()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		//Debug.DrawLine(transform.position, transform.position + transform.forward * maxRayDistance, Color.red);

		//Physics.BoxCast(transform.position, CameraHalfExtends, transform.forward, out hit ,transform.rotation, maxRayDistance);

		if (Physics.BoxCast(transform.position, CameraHalfExtends, transform.forward, out hit, transform.rotation, maxRayDistance)
								&& hit.collider.gameObject.layer == 9)
		{
			hitObject = hit.transform.gameObject;
			print("Looking at " + hit.transform.name);

			if (hit.distance < 0.5f)
			{
				print("change material of wall object");
				//hitObject.SetActive(false);
				hitObject.GetComponent<MeshRenderer>().enabled = false;
			}
			if (hit.distance > 0.5f)
			{
				hitObject.GetComponent<MeshRenderer>().enabled = true;
			}
			hitGameObject = true;
		}

		if (Physics.BoxCast(transform.position, CameraHalfExtends, transform.forward, out hit, transform.rotation, maxRayDistance)
						&& hit.collider.gameObject.layer != 9)
		{
			if (hitGameObject)
			{
				hitObject.GetComponent<MeshRenderer>().enabled = true;

				hitGameObject = false;
			}
		}


		//if (Physics.Raycast(ray, out hit, maxRayDistance) && hit.collider.gameObject.layer != 9)
		//{
		//	if (hitGameObject)
		//	{
		//		hitObject.GetComponent<MeshRenderer>().enabled = true;

		//		hitGameObject = false;
		//	}
		//}



		/* 
		 * 
		 * få bort clippingen när kameran är i vissa vinklar
		 * få till fade på materialet för spelobjektet kameran åker in i spelobjektet
		 * 
		 * 
		 */



	}

}
