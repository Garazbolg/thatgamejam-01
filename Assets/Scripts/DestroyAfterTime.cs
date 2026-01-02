using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 1f;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}