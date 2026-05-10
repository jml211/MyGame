using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerController pc = other.GetComponent<PlayerController>();
                if (pc != null && pc.NearObj.Contains(gameObject))
                {
                    other.GetComponent<Animator>().Play("Gathering");
                    Invoke(nameof(WaitDestroy), 2);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>()!=null&& other.GetComponent<PlayerController>().NearObj.Contains(this.gameObject))
        {
            other.GetComponent<PlayerController>().NearObj.Remove(this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && !other.GetComponent<PlayerController>().NearObj.Contains(this.gameObject))
        {
            other.GetComponent<PlayerController>().NearObj.Add(this.gameObject);
        }
    }

    public void WaitDestroy() {
        GetComponent<ItemOnWorld>().AddNewItem();
        Destroy(this.gameObject);
    }
}
