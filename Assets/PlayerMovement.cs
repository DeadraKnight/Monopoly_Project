using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Route currentRoute;

    int routePostion;

    public int steps;

    bool isMoving;

    int currentMoney;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = Random.Range(1, 7);

            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            routePostion++;
            routePostion %= currentRoute.childNodeList.Count;

            Vector3 nextPos = currentRoute.childNodeList[routePostion].position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePostion++;
        }

        isMoving = false;
    }
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 5f * Time.deltaTime));
    }
}
