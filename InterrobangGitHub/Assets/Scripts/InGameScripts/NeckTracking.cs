using UnityEngine;
using System.Collections;

public class NeckTracking : MonoBehaviour
{

    public float maxStretch = 3.0f;
    public LineRenderer catapultLineFront;

    private SpringJoint2D spring;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
    }

    // Use this for initialization
    void Start()
    {
        LineRendererSetup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LineRendererSetup()
    {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
    }
}
