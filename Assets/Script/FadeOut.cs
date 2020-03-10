using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

	public Camera mainCamera;
	public float maxRayDistance = 10f;

	private bool raycastCollision;
	private GameObject hitObject;

	List<GameObject> objects = new List<GameObject>();

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
		//Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.BoxCast(transform.position, CameraHalfExtends, transform.forward, out hit, transform.rotation, maxRayDistance)
								&& hit.collider.gameObject.layer == 9)
		{
			hitObject = hit.transform.gameObject;
			raycastCollision = true;
		}

		if(raycastCollision)
		{
			print("Looking at " + hit.transform.name);

			objects.Add(hitObject);
			MeshRenderer wallMesh = hitObject.GetComponent<MeshRenderer>();
			Color newColor = wallMesh.material.color;
			wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, 0.5f);
		}
		if(hit.collider.gameObject.layer != 9 && hitObject != null)
		{
			objects.RemoveAt(objects.Count - 1);
			MeshRenderer wallMesh = hitObject.GetComponent<MeshRenderer>();
			Color newColor = wallMesh.material.color;
			wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, 1f);
		}
	}
}
