using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform itemHolder;
    public Transform camera_;

    bool isOff = true;

    GameObject item;

    public GameObject[] ovenSlotHolow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(itemHolder.childCount != 0 && isOff == false)
        {
            if(itemHolder.GetChild(0).tag == "Pen")
            {
                for (int i = 0; i < ovenSlotHolow.Length; i++)
                {
                    ovenSlotHolow[i].SetActive(true);
                }
                isOff = true;

            }

        }
        else if(isOff && itemHolder.childCount == 0)
        {
            for (int i = 0; i < ovenSlotHolow.Length; i++)
            {
                ovenSlotHolow[i].SetActive(false);
            }
            isOff = false;
        }

        RaycastHit hit;

        if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, 10))
        {
            if (hit.collider.tag == "Food" && itemHolder.childCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    item = hit.transform.gameObject;
                    hit.transform.position = itemHolder.position;
                    hit.transform.parent = itemHolder;
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else if (hit.collider.tag == "Pen" && itemHolder.childCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    

                    item = hit.transform.gameObject;
                    hit.transform.position = itemHolder.position;
                    item.GetComponent<PanScript>().onStove = false;

                    hit.transform.parent = itemHolder;
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else if (hit.collider.tag == "PenHollow" && item.tag == "Pen")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    item.GetComponent<PanScript>().onStove = true;
                    item.transform.position = hit.transform.position;
                    item.transform.rotation = hit.transform.rotation;
                    item.transform.parent = hit.transform.parent;
                }
            }
            else if (hit.collider.tag == "Pen" && item.tag == "Food")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    int ing = hit.collider.GetComponent<PanScript>().ingredientCount;
                    item.transform.position = hit.transform.GetChild(ing).position;
                    item.transform.rotation = hit.transform.GetChild(ing).rotation;
                    item.transform.parent = hit.transform.GetChild(ing);
                    item.GetComponent<Rigidbody>().isKinematic = true;
                    item.GetComponent<Collider>().enabled = false;
                    hit.collider.GetComponent<PanScript>().AddIngredient();
                }
            }            


            Debug.DrawRay(camera_.transform.position, camera_.transform.forward, Color.red, 3);
        }

        if (Input.GetKeyDown(KeyCode.G) && itemHolder.childCount != 0)
        {
            item.transform.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.GetComponent<Rigidbody>().AddForce(camera_.forward * 10, ForceMode.Impulse);
            item.transform.parent = null;
            
        }

    }
}
