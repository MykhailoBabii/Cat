using UnityEngine;


public class Billboard : MonoBehaviour
{
	void Update() 
	{
		//transform.LookAt(Vector3(this.transform.position.x, Camera.main.transform.position, this.transform.position.z), -Vector3.up);
		//transform.LookAt(Vector3(transform.position.x, Camera.main.transform.position, transform.position.z));
		transform.LookAt(Vector3.zero);
		//transform.LookAt(Vector3(0, -90, 0), Vector3.up);
		//transform.LookAt(Camera.main.transform.position, -Vector3.up);
	}
}