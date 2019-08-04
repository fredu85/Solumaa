using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleanerHose : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform VacuumCleaner001;
    public Transform VacuumCleanerOtherPart001;

    public float lineDrawSpeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, VacuumCleaner001.position);
        lineRenderer.SetWidth(0.4f, 0.4f);

        dist = Vector3.Distance(VacuumCleaner001.position, VacuumCleanerOtherPart001.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 NewLocation = new Vector3(1.0f, 1.0f, 1.0f);
        //VacuumCleaner001.position = NewLocation;
        lineRenderer.SetPosition(0, VacuumCleaner001.position);
        lineRenderer.SetPosition(1, VacuumCleanerOtherPart001.position);
        if (counter < dist)
        {
            counter += 0.1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = VacuumCleaner001.position;
            Vector3 pointB = VacuumCleanerOtherPart001.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
        }
    }
}
