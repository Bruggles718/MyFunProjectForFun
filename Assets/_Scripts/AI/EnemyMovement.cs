using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveEnemy(Vector2 moveVector)
    {
        this.rb.velocity = moveVector * this.speed;
    }

    public void Attack()
    {
        return;
    }

    public void OnPointerInput(Vector2 pointVector)
    {
        return;
    }
}
