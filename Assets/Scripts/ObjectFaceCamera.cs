using UnityEngine;

public class ObjectFaceCamera : MonoBehaviour
{
    private void OnEnable()
    {
        if (Camera.main != null)
        {
            Vector3 directionToCamera = Camera.main.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
