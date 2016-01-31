using UnityEngine;

public class TransportEntityView : MonoBehaviour {
    [HideInInspector]
    public SpawnerView Spawner;
    [HideInInspector]
    public SpawnerView.EntityType Type;
    public float Speed = 3;
	public void SafeDestroy()
    {
        if (Spawner != null)
            Spawner.Recicle(Type, gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("EntityWall"))
        {
            SafeDestroy();
        }
    }
}
