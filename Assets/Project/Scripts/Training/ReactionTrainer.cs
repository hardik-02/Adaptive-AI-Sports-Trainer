using UnityEngine;

public class ReactionTrainer : MonoBehaviour
{
    public ReactionTarget target;

    [Header("Spawn Area")]
    public float spawnRadius = 8f;

    private float spawnTime;
    private bool isActive;

    void Start()
    {
        SpawnTarget();
    }

    void SpawnTarget()
    {
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0f;

        target.transform.position = randomPos;
        target.gameObject.SetActive(true);

        spawnTime = Time.time;
        isActive = true;

        target.onReached = OnTargetReached;

        Debug.Log("Target spawned. React!");
    }

    void OnTargetReached()
    {
        if (!isActive) return;

        float reactionTime = Time.time - spawnTime;
        isActive = false;

        Debug.Log($"Reaction Time: {reactionTime:F2} seconds");

        Invoke(nameof(SpawnTarget), 1.5f);
    }
}
