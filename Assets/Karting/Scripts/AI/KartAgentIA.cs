using MLAgents;
using KartGame.KartSystems;
using UnityEngine;

namespace KartGame.AI
{

    /// <summary>
    /// The KartAgent will drive the inputs for the KartController.
    /// </summary>
    public class KartAgentIA : ArcadeKart
    {

        #region Training Modes
        [Tooltip("What is the initial checkpoint the agent will go to? This value is only for inferencing.")]
        public ushort InitCheckpointIndex;
        #endregion

        #region Senses
        [Header("Checkpoints")]
        [Tooltip("What are the series of checkpoints for the agent to seek and pass through?")]
        public Collider[] Colliders;
        [Tooltip("What layer are the checkpoints on? This should be an exclusive layer for the agent to use.")]
        public LayerMask CheckpointMask;
        #endregion

        public ArcadeKart kart;

        public float speed = 4.0f;

        float acceleration;
        float steering;

        int checkpointIndex;

        void Start()
        {
            // If the agent is training, then at the start of the simulation, pick a random checkpoint to train the agent.
            //AgentReset();

            checkpointIndex = InitCheckpointIndex;
        }

        void FixedUpdate()
        {
            // Find the next checkpoint when registering the current checkpoint that the agent has passed.
            int next = (checkpointIndex + 1) % Colliders.Length;
            Collider nextCollider = Colliders[next];

            var step = speed * Time.deltaTime; // calculate distance to move
            kart.transform.position = Vector3.MoveTowards(kart.transform.position, nextCollider.transform.position, step);
            kart.transform.forward = Vector3.RotateTowards(kart.transform.forward, nextCollider.transform.position - kart.transform.position, step, 0.0f);

            //Vector3 direction = Vector3.MoveTowards(kart.transform.position, nextCollider.transform.position, step);

            //AutoMoveVehicle(direction.x, direction.y);

        }

        void OnTriggerEnter(Collider other)
        {
            int maskedValue = 1 << other.gameObject.layer;
            int triggered = maskedValue & CheckpointMask;

            FindCheckpointIndex(other, out int index);

            // Ensure that the agent touched the checkpoint and the new index is greater than the m_CheckpointIndex.
            if (triggered > 0 && index > checkpointIndex || index == 0 && checkpointIndex == Colliders.Length - 1)
            {
                checkpointIndex++;
            }
        }

        void FindCheckpointIndex(Collider checkPoint, out int index)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i].GetInstanceID() == checkPoint.GetInstanceID())
                {
                    index = i;
                    return;
                }
            }
            index = -1;
        }

        public Vector2 GenerateInput()
        {
            return new Vector2(steering, acceleration);
        }
    }
}
