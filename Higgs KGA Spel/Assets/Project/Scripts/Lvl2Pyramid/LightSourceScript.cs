using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceScript : MonoBehaviour
{
    private int maxReflections = 5;
    private float maxLightDistance = 100f;

    [SerializeField] private Vector2 originDir;

    [SerializeField] private LineRenderer line;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(updateLight());
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
	}

    private IEnumerator updateLight()
    {
        if (originDir.y == 0)
            CalculateLight(new Vector2(transform.position.x + originDir.x * (transform.localScale.x / 2), transform.position.y), originDir, maxReflections);
        else
            CalculateLight(new Vector2(transform.position.x, transform.position.y + originDir.y * (transform.localScale.y / 2)), originDir, maxReflections);

        yield return new WaitForSeconds(1/60);

        StartCoroutine(updateLight());
    }

    private void CalculateLight(Vector2 position, Vector2 direction, int remainingReflexions)
    {
        if (remainingReflexions <= 0)
            return;

        Vector2 startPos = position;

        RaycastHit2D hit = Physics2D.Raycast(position + direction * 0.001f, direction, maxLightDistance);

        if (hit)
        {
            direction = Vector2.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
            position += direction * maxLightDistance;

        line.SetPosition(maxReflections - remainingReflexions, new Vector3(startPos.x, startPos.y, 0f));

        for (int i = 0; i < line.positionCount; i++)
        {
            if (i + (maxReflections - remainingReflexions) + 1 > line.positionCount - 1)
                break;

            line.SetPosition(i + (maxReflections - remainingReflexions) + 1, new Vector3(position.x, position.y, 0f));
        }

        CalculateLight(position, direction, remainingReflexions - 1);
    }  
}
