using UnityEngine;

public static class Utils
{
    public static GameObject FindChildWithTag(this GameObject parent, string tag)
    {
        GameObject child = null;
 
        foreach(Transform transform in parent.transform) {
            if(transform.CompareTag(tag)) {
                child = transform.gameObject;
                break;
            }

            foreach (var Transform in transform)
            {
                if(transform.CompareTag(tag)) {
                    child = transform.gameObject;
                    break;
                }
            }
        }
        
        return child;
    }
}