using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    //private ItemDatabase databaseItem;
    private string code;
    private bool estart;
    private bool refresh;
    public Collider oitem;
    public BoxCollider boxCollider;

    void Start()
    {
        /*inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        databaseChest = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ChestDatabase>();
        databaseItem = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemDatabase>();*/
        boxCollider = this.GetComponent<BoxCollider>();

    }

    /*void FixedUpdate()
    {
        boxCollider.enabled = true;
        if (Input.GetButtonDown("Action") && estart)
        {
            code = oitem.gameObject.name;
            GetItems(code);
            if (oitem.transform.parent != null)
            {
                Destroy(oitem.transform.parent.gameObject);
                oitem = null;
                estart = false;
                boxCollider.enabled = false;
            }
            else
            {
                Destroy(oitem.gameObject);
                oitem = null;
                estart = false;
                boxCollider.enabled = false;
            }
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            Debug.Log("Hey there!");
        }
        /*if (other.gameObject.tag == "Chest")
        {
            code = other.gameObject.name;
            GetItemsFromChest(code);
            //Destroy(other);
        }*/
        
        if (other.gameObject.tag == "Item")
        {
                oitem = other;
                estart = true;
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            refresh = true;
        }
    }*/



    void OnTriggerExit(Collider other)
    {
        estart = false;
        oitem = null;
    }

    /*public void GetItems(string code)
    {
        if (code != null)
        {
            Item itemToAdd = databaseItem.FetchItemByCode(code);
            int id = itemToAdd.ID;
            inv.AddItem(id);
        }
        else
        {
            Debug.Log("Error");
        }
    }*/

    /*public void GetItemsFromChest(string code)
    {
        if (code != null)
        {
            Chest ChestToGet = databaseChest.FetchChestByCode(code);
            int id = ChestToGet.ItemsId;
            if (!ChestToGet.Open) {
                inv.AddItem(id);
                ChestToGet.Open = true;
                Debug.Log(ChestToGet.Title + " Open" );
            }else
            {
                Debug.Log(ChestToGet.Title + " AlreadyOpen");
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }*/

}
