using UnityEngine;
using MxMGameplay;
using MxM;
public class MxMTestScript : MonoBehaviour
{
    private MxMAnimator _mxmAnimator;
    [SerializeField]
    private MxMEventDefinition _eventDefinition;
    void Start()
    {
       _mxmAnimator = GetComponent<MxMAnimator>(); 
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            Debug.Log("keydown");
            _mxmAnimator.BeginEvent(_eventDefinition);
        }
    }
}
