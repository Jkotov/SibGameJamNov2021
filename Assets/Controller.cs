using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public float Timeout => timeout + startTimeout;
    [SerializeField] private protected float timeout;
    [SerializeField] private protected float startTimeout;

    public virtual void ActiveCrisis()
    {
        StartCoroutine(ShowWarning());
    }

    private protected virtual IEnumerator ShowWarning()
    {
        yield return new WaitForSeconds(startTimeout);
        StartCoroutine(Crisis());
    }

    private protected virtual IEnumerator Crisis()
    {
        yield return new WaitForSeconds(timeout);
        
    }
}
