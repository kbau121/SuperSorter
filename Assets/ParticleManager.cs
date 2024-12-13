using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem successParticle;

    public void PlaySuccessParticle(Vector3 worldPos)
    {
        ParticleSystem particleInstance = Instantiate(successParticle, worldPos, Quaternion.identity);
        particleInstance.transform.localScale *= 0.3f;

        float playTime = .5f; 
        StartCoroutine(DestroyParticleAfterTime(particleInstance, playTime));
    }

    private IEnumerator DestroyParticleAfterTime(ParticleSystem particle, float delay)
    {
        yield return new WaitForSeconds(delay);

        particle.Stop();

        Destroy(particle.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
