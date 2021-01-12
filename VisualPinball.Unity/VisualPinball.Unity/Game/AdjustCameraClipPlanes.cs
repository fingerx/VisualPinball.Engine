using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace VisualPinball.Unity
{
    [RequireComponent(typeof(Camera))]
	[ExecuteAlways]
    public class AdjustCameraClipPlanes : MonoBehaviour
    {
        [NonSerialized]
        public Camera camera;

        TableAuthoring table;
        Transform trans;

        // Start is called before the first frame update
        private void OnEnable()
        {
            if(!TableSelector.Instance.HasSelectedTable)
            {
                return;
            }

            camera = GetComponent<Camera>(); 

            TableAuthoring table = TableSelector.Instance.SelectedTable;
            //Transform trans = this.transform;

        }

        // Update is called once per frame
        void Update()
        {
			if(!TableSelector.Instance.HasSelectedTable)
			{
				return;
			}
			if(transform.hasChanged)
            {
				table = TableSelector.Instance.SelectedTable;
				Bounds tb = table.GetTableBounds();
                Vector3 p = transform.position;
                Vector3 nearPoint = tb.ClosestPoint(p);
                float deltaN = Vector3.Magnitude(p - nearPoint);
                float deltaF = math.max(Vector3.Distance(p, tb.max), Vector3.Distance(p, tb.min));

                //TODO: Replace this with proper frustum distances.
                float nearPlane = math.max(0.001f, math.abs(deltaN * 0.9f));
                float farPlane = math.max(1f, math.abs(deltaF * 1.1f));

                Camera.main.nearClipPlane = nearPlane;
                Camera.main.farClipPlane = farPlane;
            }
            
        }
    }
}
