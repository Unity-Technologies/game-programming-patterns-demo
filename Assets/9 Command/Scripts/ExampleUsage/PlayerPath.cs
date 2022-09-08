using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DesignPatterns.Command
{
    // extra visualization tool for the player's path
    public class PlayerPath : MonoBehaviour
    {
        // one dot to represent the path
        [SerializeField] private GameObject pathPointPrefab;
        [SerializeField] Transform pathTransform;

        // amount to offset each path point
        [SerializeField] Vector3 offset;
        private Stack<GameObject> pathObjects = new Stack<GameObject>();

        // LineRenderer to connect the point prefabs
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] List<Vector3> pointList;

        private void Start()
        {
            // starting position
            AddToPath(transform.position);
        }

        public void AddToPath(Vector3 position)
        {
            if (pathPointPrefab == null)
            {
                return;
            }

            GameObject newPathObject = Object.Instantiate(pathPointPrefab, position + offset, Quaternion.identity) as GameObject;

            pathObjects?.Push(newPathObject);

            if (pathTransform != null)
            {
                newPathObject.transform.parent = pathTransform;
            }

            // update points for LineRenderer
            pointList = pathObjects.Select(x => x.transform.position).ToList();
            lineRenderer.positionCount = pointList.Count;

        }

        public void RemoveFromPath()
        {
            GameObject lastObject = pathObjects.Pop();
            Destroy(lastObject.gameObject);

            // update points for LineRenderer
            pointList = pathObjects.Select(x => x.transform.position).ToList();
            lineRenderer.positionCount = pointList.Count;
        }

        private void Update()
        {
            // LineRenderer needs per-frame update
            lineRenderer.SetPositions(pointList.ToArray());
        }
    }
}