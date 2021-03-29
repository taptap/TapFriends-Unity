using UnityEngine;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TapCommon.TapCommon.GetRegionCode(isMainland => { Debug.Log($"hello world:{isMainland}"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
    }
}