using UnityEngine;
using System.Collections;

public class CollectableStarScript : MonoBehaviour
{
    CollectableStarManager manager;
    // Use this for initialization
    void Start()
    {
        manager = GameObject.FindObjectOfType<CollectableStarManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.totalNumberOfCollectables++;
        gameObject.SetActive(false);
    }
}
