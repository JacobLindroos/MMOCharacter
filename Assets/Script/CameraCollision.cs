using UnityEngine;

public class CameraCollision : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.layer == 9)
		{
			other.GetComponent<MeshRenderer>().enabled = false;
		}
	}

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.layer == 9)
		{
			other.GetComponent<MeshRenderer>().enabled = true;
		}
	}
}
