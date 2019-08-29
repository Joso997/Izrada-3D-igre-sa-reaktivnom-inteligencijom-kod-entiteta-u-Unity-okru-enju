using UnityEngine;

public class DiggingFunction : MonoBehaviour {

    public bool belowground = false;
    public bool eaten = false;
    private bool buttonpressed = false;
    private CharacterController controller;
    int timepassed = 10;
    int timepassed_hodl;
    TrailRenderer trailRenderer;
    BoxCollider[] boxCollider;
    MeshRenderer[] meshRenderer;
    float color = 0f;
    public delegate void NestBroken();
    public NestBroken nestBroken;

    // Use this for initialization
    void Start () {
        timepassed_hodl = timepassed;
        trailRenderer = this.GetComponent<TrailRenderer>();
        boxCollider = this.GetComponents<BoxCollider>();
        meshRenderer = this.GetComponents<MeshRenderer>();
        boxCollider = this.GetComponentsInChildren<BoxCollider>();
        meshRenderer = this.GetComponentsInChildren<MeshRenderer>();
        controller = GetComponent<CharacterController>();
        trailRenderer.enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        
        
        if (Input.GetButtonDown("Hide") && controller.isGrounded)
        {
            color = 0;
            RaycastHit hit;
            //Vector3 down = transform.TransformDirection(Vector3.down) * distance;
            //Debug.DrawRay(transform.position, (down), Color.blue);
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                MeshRenderer meshRend = hit.transform.GetComponent<MeshRenderer>();
                Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if ((rend != null || rend.sharedMaterial != null || rend.sharedMaterial.mainTexture != null || meshCollider != null) && meshRend.enabled)
                    {
                        Texture2D tex = rend.material.mainTexture as Texture2D;
                        Vector2 pixelUV = hit.textureCoord;
                        pixelUV.x *= tex.width;
                        pixelUV.y *= tex.height;
                        color = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).g;
                        //Debug.Log(tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).g);
                }
            }
        }

        if ((Input.GetButton("Hide") || buttonpressed) && controller.isGrounded && !belowground && timepassed >= timepassed_hodl && color >= 0.337 && color <= 0.338)
        {
            //transform.position = new Vector3(transform.position.x, 5.1f, transform.position.z);
            belowground = true;
            timepassed = 0;
            trailRenderer.time = 5;
            trailRenderer.material.color = Color.red;
            trailRenderer.startWidth = 1.25f;
            trailRenderer.endWidth = 1.25f;
            trailRenderer.enabled = true;
            //boxCollider[0].enabled = false;     
            meshRenderer[0].enabled = false;
            boxCollider[1].enabled = false;
            meshRenderer[1].enabled = false;
            Physics.IgnoreLayerCollision(8, 9, true);
            Physics.IgnoreLayerCollision(8, 10, false);
        }
        else 
        if((Input.GetButton("Hide") || buttonpressed) && belowground && timepassed >= timepassed_hodl && color >= 0.337 && color <= 0.338)
        {
            //transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);
            belowground = false;
            timepassed = 0;
            trailRenderer.enabled = false;
            //boxCollider[0].enabled = true;
            meshRenderer[0].enabled = true;
            boxCollider[1].enabled = true;
            meshRenderer[1].enabled = true;
            Physics.IgnoreLayerCollision(8, 9, false);
            Physics.IgnoreLayerCollision(8, 10, true);
        }
        timepassed++;
        buttonpressed = false;

    }
    /*void OnTriggerEnter(Collider other)//NEED TO PUT IN IT'S OWN SCRIPT
    {
        if (!belowground)
        {
            if (other.gameObject.tag == "Predator")
                eaten = true;
        }
        
    }*/

    public void Hide()
    {
        buttonpressed = true;
    }
    public void Reset()
    {
        belowground = false;
        timepassed = 0;
        trailRenderer.enabled = false;
        //boxCollider[0].enabled = true;
        meshRenderer[0].enabled = true;
        boxCollider[1].enabled = true;
        meshRenderer[1].enabled = true;
        Physics.IgnoreLayerCollision(8, 9, false);
        Physics.IgnoreLayerCollision(8, 10, true);
    }
}