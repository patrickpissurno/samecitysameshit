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
        bool Found = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 5);
        float breakMulti = (Found ? .3f : 1f);
        //Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * 5, Found ? Color.red : Color.white);
        transform.Translate(Vector3.forward * Speed * breakMulti * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("EntityWall"))
        {
            SafeDestroy();
        }
    }
}
