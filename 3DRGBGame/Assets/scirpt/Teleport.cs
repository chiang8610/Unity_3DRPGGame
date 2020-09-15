using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("目標位置")]
    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "瘦長人")
        {
            target.GetComponent<CapsuleCollider>().enabled = false;
            other.transform.position = target.position;
           Invoke("OpenCollider", 3f);

        }
           
    }

  private void OpenCollider()
    {
        target.GetComponent<CapsuleCollider>().enabled = true;
   }
}
