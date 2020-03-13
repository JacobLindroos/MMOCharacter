using UnityEngine;

public class FadeOut : MonoBehaviour
{
	[Header("Raycast settings")]
	public LayerMask objectsToHit;
	public float SphereCastRadius = 0.5f;
	public Camera mainCamera;
	public GameObject player;

	[Header("Fade settings")]
	public float fadeInAlpha = 0.5f;
	public float fadeOutAlpha = 1f;

	private float maxRayDistance;
	private RaycastHit[] hits;
	private Vector3 currentCameraPosition;
	private Vector3 raycastDirection;

	private void Start()
	{
		mainCamera = GetComponent<Camera>();
	}

	void FixedUpdate()
	{
		//gets the distance between the camera and the player to set the raycast max distance
		maxRayDistance = Vector2.Distance(transform.position, player.transform.position);
		//gets camera current position
		currentCameraPosition = transform.position;
		//gets camera forward direction
		raycastDirection = transform.forward;

		//to avoid null ref from start, only want to change back the materials alpha after the ray have actually hit something
		if (hits != null)
		{
			//resting material on walls last hit by the raycast
			ChangeAlphaOfMaterial(fadeOutAlpha);
		}

		//hits all walls in between the camera and the player
		hits = Physics.SphereCastAll(currentCameraPosition, SphereCastRadius, raycastDirection, maxRayDistance, objectsToHit);

		//setting the walls to transparent when hit by the raycast 
		ChangeAlphaOfMaterial(fadeInAlpha);
	}


	/// <summary>
	/// Changes the alpha of specific material
	/// </summary>
	/// <param name="alpha"> of material that is hit by raycast </param>
	private void ChangeAlphaOfMaterial(float alpha)
	{
		//resting material on walls last hit by the raycast
		foreach (var hit in hits)
		{
			MeshRenderer wallMesh = hit.transform.gameObject.GetComponent<MeshRenderer>();
			Color newColor = wallMesh.material.color;
			wallMesh.material.color = new Color(newColor.r, newColor.g, newColor.b, alpha);
		}
	}


	/// <summary>
	/// Drawing out a visual sphere and line for raycast
	/// </summary>
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * maxRayDistance);
		Gizmos.DrawWireSphere(transform.position, .5f);
	}
}
