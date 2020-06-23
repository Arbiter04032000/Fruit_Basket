using UnityEngine;

[ExecuteInEditMode]
public class GrassRotator : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(-360, 360), 0);
    }
}
