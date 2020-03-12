using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FadeOut : MonoBehaviour
{
	public LayerMask objectsToHit;
	public Camera mainCamera;
	public GameObject player;

	private float maxRayDistance;
	private RaycastHit[] hits;

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
		//getting the distance between the camera and the player to set the raycast max distance
		maxRayDistance = Vector2.Distance(transform.position, player.transform.position);

		if (hits != null)
		{
			//resting material on walls last hit by the raycast
			foreach (var hit in hits)
			{
				MeshRenderer wallMesh = hit.transform.gameObject.GetComponent<MeshRenderer>();
				Color newColor = wallMesh.material.color;
				wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, 1.0f);
			}
		}

		//gets all walls in between the camera and the player
		hits = Physics.BoxCastAll(transform.position, CameraHalfExtends, transform.forward, transform.rotation, maxRayDistance, objectsToHit);

		//setting the walls to transparent when hit by the raycast 
		foreach (var hit in hits)
		{
			MeshRenderer wallMesh = hit.transform.gameObject.GetComponent<MeshRenderer>();
			Color newColor = wallMesh.material.color;
			wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, 0.5f);
		}
	}
}
