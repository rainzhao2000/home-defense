using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public float speed = 10;
    public LayerMask blockingLayer;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb2D;

    public Animator animator;

    public float health = 100;

    private float inverseMoveTime;

    void Start()
    {
        inverseMoveTime = 1f / speed;
    }

    bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPos);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    void AttemptMove(int xDir, int yDir)
       // where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
        {
            return;
        }
        /*
        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
        */
    }

    void Update()
    {/*
        int xDir = 0;
        int yDir = 0;
        Vector3 target = findTarget();
        if (Mathf.Abs(target.x - transform.position.x) < float.Epsilon)

            //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            yDir = target.y > transform.position.y ? 1 : -1;

        //If the difference in positions is not approximately zero (Epsilon) do the following:
        else
            //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            xDir = target.x > transform.position.x ? 1 : -1;

        //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player

        //AttemptMove<Player>(xDir, yDir);*/
        Vector3 target = findTarget();
        float distance = Vector3.Distance(transform.position, target);
        Vector3 targetDir = target - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward); 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void OnCantMove<T>(T component)
        where T : Component
    {
        // inflict damage
        animator.SetTrigger("enemyAttack");
    }

    Vector3 findTarget()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, 100, blockingLayer);
        if (collision == null)
        {
            return transform.position;
        }
        Vector2 cell = collision.Distance(boxCollider).pointA;
        return collision.gameObject.GetComponent<Tilemap>().WorldToCell(cell);
    }

}
