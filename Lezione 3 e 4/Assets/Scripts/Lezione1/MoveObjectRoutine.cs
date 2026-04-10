using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectRoutine : MonoBehaviour
{
    [System.Serializable]
    public class MoveStep
    {
        public Vector3 direction = Vector3.forward;
        public float distance = 2f;
        public float speed = 5f;
        public float waitAfter = 0f; // optional pause after this step
    }

    public List<MoveStep> steps = new List<MoveStep>();
    public bool loop = false;
    public bool playOnStart = true;

    private Coroutine _activeRoutine;

    void Start()
    {
        if (playOnStart)
            Play();
    }

    public void Play()
    {
        if (_activeRoutine != null)
            StopCoroutine(_activeRoutine);

        _activeRoutine = StartCoroutine(RunSequence());
    }

    public void Stop()
    {
        if (_activeRoutine != null)
        {
            StopCoroutine(_activeRoutine);
            _activeRoutine = null;
        }
    }

    private IEnumerator RunSequence()
    {
        do
        {
            foreach (var step in steps)
            {
                Vector3 target = transform.position + step.direction.normalized * step.distance;

                while (Vector3.Distance(transform.position, target) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, step.speed * Time.deltaTime);
                    yield return null;
                }

                transform.position = target;

                if (step.waitAfter > 0f)
                    yield return new WaitForSeconds(step.waitAfter);
            }
        }
        while (loop);

        _activeRoutine = null;
    }
}