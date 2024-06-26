using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseaMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3f, 1f), 0, Random.Range(-2f, 2f));
        targetTransform.localPosition = new Vector3(Random.Range(1.96f, 3.43f), 0, Random.Range(-2f, 2f));

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); //tells AI our player position
        sensor.AddObservation(targetTransform.localPosition); //tells our goal position

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 5f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);  //value doesn't matter. Only matters relative to other rewards/goals
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);  //value doesn't matter. Only matters relative to other rewards/goals
            floorMeshRenderer.material = loseaMaterial;
            EndEpisode();
        }
    }
}
