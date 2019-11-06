using UnityEngine;

public class Spawner : MonoBehaviour, IFactory
{
    public void Spawn(GameObject spawnObject, Vector3 spawnPosition, bool directionRight, float speed, out GameObject prop)
    {
        transform.position = spawnPosition;
        prop  = Instantiate(spawnObject, transform);
        prop.transform.parent = null;

        prop.GetComponent<IMovement>().MovementConstructor(speed, directionRight);
    }
}
