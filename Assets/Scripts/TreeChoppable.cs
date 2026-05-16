using UnityEngine;

public class TreeChoppable : MonoBehaviour
{
    public Animation treeAnimator;
    public GameObject treeCompleteVisual;
    public int hitsToComplete = 10;

    int hits;
    const string ShakeStateName = "TreeShakeAnim";

    void Awake()
    {
        if (treeAnimator == null)
            treeAnimator =GetComponentInParent<Animation>();
        if (treeCompleteVisual == null)
        {
           // Transform t = transform.Find("TreeComplete");
            //if (t != null)
            //    treeCompleteVisual = t.gameObject;
            //else
            //    CreateDemoTreeComplete();
        }
    }

    void CreateDemoTreeComplete()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = "TreeComplete";
        go.transform.SetParent(transform, false);
        go.transform.localPosition = new Vector3(0f, 1.2f, 0f);
        go.transform.localScale = new Vector3(0.7f, 0.35f, 0.7f);
        treeCompleteVisual = go;
    }

    public bool CanChop => hits < hitsToComplete;

    public void Chop()
    {
        if (!CanChop)
            return;

        hits++;

        if (treeAnimator != null)
            treeAnimator.Play(ShakeStateName);

        if (hits >= hitsToComplete) {
            treeAnimator.Play("FallDown");
            transform.parent.parent.parent.GetComponentInChildren<TreeManager>().isDead = true;
        }
           
    }
}
