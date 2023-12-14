using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backtomenu : MonoBehaviour
{
    private SceneBehaviour _sceneBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        _sceneBehaviour = GameObject.Find("SceneBehaviour").GetComponent<SceneBehaviour>();
    }

    // Update is called once per frame
    public void ReturntoMain(int index)
    {
        _sceneBehaviour.LoadSceneByIndex(index);
    }
}
