using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Controller : LocalSingleton<Scene2Controller>
{
    [SerializeField] Hitler hitler;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hitler.PlaySittingAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
