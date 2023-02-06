using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MovingWall : MonoBehaviour
{
    [SerializeField] bool setPos1;
    [SerializeField] Vector3 pos1;
    [SerializeField] bool setPos2;
    [SerializeField] Vector3 pos2;
    bool movingToPos1;

    private void Update()
    {
        if (setPos1) {
            setPos1 = false;
            pos1 = transform.position;
        }
        if (setPos2) {
            setPos2 = false;
            pos2 = transform.position;
        }
    }

    public void Move()
    {
        StopAllCoroutines();
        movingToPos1 = !movingToPos1;
        StartCoroutine(MoveObj());
    }

    IEnumerator MoveObj()
    {
        var targetPos = pos1;
        if (!movingToPos1) targetPos = pos2;

        while(Vector3.Distance(transform.position, targetPos) > 0.01f) {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.025f);
            yield return new WaitForEndOfFrame();
        }
    }
}
