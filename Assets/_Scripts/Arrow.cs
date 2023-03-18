using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowHitCollisionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Wall")) return;
        var prefab = Instantiate(arrowHitCollisionPrefab, this.transform.position + this.transform.up, this.transform.rotation);
        prefab.transform.SetParent(collision.transform);
        Destroy(prefab, 3);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Wallaaaaaaa")) return;
        var prefab = Instantiate(arrowHitCollisionPrefab, this.transform.position + this.transform.up, this.transform.rotation);
        prefab.transform.SetParent(other.transform);
        Destroy(prefab, 3);
        Destroy(this.gameObject);
    }
}
