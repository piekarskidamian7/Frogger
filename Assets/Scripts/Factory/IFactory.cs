using UnityEngine;

public interface IFactory
{
   void Spawn(GameObject spawnObject, Vector3 spawnPosition, bool directionRight, float speed, out GameObject prop);
}