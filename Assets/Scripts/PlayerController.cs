using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> NearObj = new List<GameObject>();

    [Header("Space: grass (testgrass)")]
    [Tooltip("Horizontal distance within which Space collects the object named \"testgrass\".")]
    public float testGrassPickupRadius = 3.5f;

    [Header("Space: tree (TreeDemo)")]
    [Tooltip("Horizontal distance within which Space chops the object with TreeChoppable (scene name example: TreeDemo).")]
    public float treeChopRadius = 3.5f;

    [Tooltip("Minimum seconds between tree chops when pressing Space.")]
    public float treeChopCooldown = 0.35f;

    private Animator animator;
    private float speed = 4f;
    private CharacterController controller;

    const string TestGrassObjectName = "testgrass";
    const string TreeDemoObjectName = "TreeDemo";
    static readonly int GatheringStateHash = Animator.StringToHash("Gathering");

    bool interactionBusy;
    float nextTreeChopTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        if (dir.sqrMagnitude > 0.0001f)
            transform.LookAt(transform.position + dir.normalized, Vector3.up);
        controller.SimpleMove(dir * speed);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);

        if (Input.GetKeyDown(KeyCode.Space) && !interactionBusy)
            TrySpaceInteract();
    }

    static float HorizontalDistance(Vector3 a, Vector3 b)
    {
        a.y = 0f;
        b.y = 0f;
        return Vector3.Distance(a, b);
    }

    void TrySpaceInteract()
    {
        GameObject grassGo = GameObject.Find(TestGrassObjectName);
        TreeChoppable tree = FindTreeChoppable();

        bool grassOk = grassGo != null
            && HorizontalDistance(transform.position, grassGo.transform.position) <= testGrassPickupRadius
            && grassGo.GetComponent<ItemOnWorld>() != null;

        bool treeOk = tree != null
            && tree.CanChop
            && HorizontalDistance(transform.position, tree.transform.position) <= treeChopRadius
            && Time.time >= nextTreeChopTime;

        if (!grassOk && !treeOk)
            return;

        if (grassOk && treeOk)
        {
            float dGrass = HorizontalDistance(transform.position, grassGo.transform.position);
            float dTree = HorizontalDistance(transform.position, tree.transform.position);
            if (dTree <= dGrass)
                StartCoroutine(TreeChopRoutine(tree));
            else
                StartCoroutine(PickupTestGrassRoutine(grassGo, grassGo.GetComponent<ItemOnWorld>()));
            return;
        }

        if (treeOk)
            StartCoroutine(TreeChopRoutine(tree));
        else
            StartCoroutine(PickupTestGrassRoutine(grassGo, grassGo.GetComponent<ItemOnWorld>()));
    }

    TreeChoppable FindTreeChoppable()
    {
        GameObject named = GameObject.Find(TreeDemoObjectName);
        if (named != null)
        {
            TreeChoppable t = named.GetComponent<TreeChoppable>();
            if (t != null)
                return t;
        }

        return FindObjectOfType<TreeChoppable>();
    }

    IEnumerator TreeChopRoutine(TreeChoppable tree)
    {
        interactionBusy = true;
        tree.Chop();
        nextTreeChopTime = Time.time + treeChopCooldown;
        yield return new WaitForSeconds(treeChopCooldown);
        interactionBusy = false;
    }

    IEnumerator PickupTestGrassRoutine(GameObject grass, ItemOnWorld itemOnWorld)
    {
        interactionBusy = true;

        bool playGather = animator != null && animator.HasState(0, GatheringStateHash);
        if (playGather)
            animator.Play("Gathering", 0, 0f);

        if (playGather)
            yield return new WaitForSeconds(2f);

        if (grass != null && itemOnWorld != null)
        {
            itemOnWorld.AddNewItem();
            Destroy(grass);
        }

        interactionBusy = false;
    }
}
