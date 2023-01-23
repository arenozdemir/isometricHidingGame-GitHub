using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncer : MonoBehaviour
{
    [SerializeField] Rigidbody ball;
    [SerializeField] Transform target;
    float gravity = -9.81f;
    [SerializeField] float height = 2f;
    [SerializeField] LineRenderer lineRenderer;
    int resolution = 30;
    private void Start()
    {
        lineRenderer.positionCount = resolution;
    }
    void Update()
    {
        
        DrawPath();
    }

    public void Launch()
    {
        ball.transform.parent = null;
        lineRenderer.enabled = false;
        ball.isKinematic = false;
        ball.velocity = CalculateVelocity().initialVelocity;
    }

    LaunchData CalculateVelocity()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0,
                                                target.position.z - ball.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        float time = Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity);
        Vector3 veclocityZX = displacementXZ / time;
        return new LaunchData(veclocityZX + velocityY * -Mathf.Sign(gravity), time);
    }
    void DrawPath()
    {
        LaunchData launchData = CalculateVelocity();
        Vector3 previousPoint = ball.position;
        lineRenderer.SetPosition(0, previousPoint);
        for (int i = 1; i < resolution; i++)
        {
            float simulationTime = i / (float)(resolution-1) * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousPoint, drawPoint);
            previousPoint = drawPoint;
            lineRenderer.SetPosition(i, ball.position + displacement);
          //  previousPoint = ball.position + displacement;
        }
        
    }
    struct LaunchData
    {
        public Vector3 initialVelocity;
        public float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

}
