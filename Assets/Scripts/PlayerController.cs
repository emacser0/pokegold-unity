using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (input != Vector2.zero)
            {
                if (input.x != 0.0f)
                {
                    input.y = 0.0f;
                }

                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);

                StartCoroutine(Move(targetPosition));
            }
        }
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;
        animator.SetBool("IsMoving", true);

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        animator.SetBool("IsMoving", false);
    }
}
