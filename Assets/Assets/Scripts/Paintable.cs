using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public GameObject Brush;
    public float BrushSize = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);

                var go = Instantiate(Brush, new Vector3(hit.point.x, hit.point.y, -1.0f), Quaternion.identity, transform);
                go.transform.localScale = Vector3.one * BrushSize;
                go.transform.rotation = rot;
            }
        }
    }
}
