using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Vector3 localOpenOffset = new Vector3(0f, 0f, 0.33f);

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDrawer()
    {
        this.transform.localPosition = this.transform.localPosition + localOpenOffset;  
    }

    public IEnumerator MoveDrawer()
    {
        while (true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + localOpenOffset, Time.deltaTime);
            yield return null;
        }
    }

    
}
