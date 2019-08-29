using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_raycast : MonoBehaviour {

    public float distance = 3f;
    public bool click = false;

    void Start()
    {
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down) * distance;
        Debug.DrawRay(transform.position, (down), Color.blue);
        if (click)
        {
            if (Physics.Raycast(transform.position, down, out hit, distance))
            {
                Renderer rend = hit.transform.GetComponent<Renderer>();
                MeshCollider meshCollider = hit.collider as MeshCollider;
                Debug.Log(hit.collider);

                /*if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                    return;*/

                Texture2D tex = rend.material.mainTexture as Texture2D;
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= tex.width;
                pixelUV.y *= tex.height;
                Debug.Log(tex.GetPixel((int)pixelUV.x, (int)pixelUV.y));
            }   

            
        }
        
        /*tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
        tex.Apply();*/
    }
}
