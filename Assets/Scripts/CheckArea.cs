using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag!="Player" && !PlayerController.Instance.NearObj.Contains(other.gameObject))
        {
            
            PlayerController.Instance.NearObj .Add( other.gameObject);
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if ( PlayerController.Instance.NearObj.Contains(other.gameObject))
        {
            PlayerController.Instance.NearObj.Remove(other.gameObject);
            if (PlayerController.Instance.NearObjItem ==other.gameObject)
            {
                PlayerController.Instance.NearObjItem = null;
                PlayerController.Instance.distance = -1;
            }
        }

    }
}
