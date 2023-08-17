using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelActionClick : MonoBehaviour
{
    public string type;
    string name;
    public Animator animator;
    public bool PlayAnimationOut=true;

    public delegate void TriggerCallback(string type);
    static public TriggerCallback CompleteAnimation = null;
    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.name; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Click ne"+ hit.transform.name);
                if (hit.transform.name == name)
                {
                    GameController.instance.VisibleDescription(false);
                    animator.Play(GameController.TRANSITION_IN);
                    Invoke(nameof(OnCompleteAnimationIn),2.5f);
                }
            }
        }
    }
    void OnCompleteAnimationIn()
    {        
        // TODO: Do something when animation did complete
        Debug.Log("Animation complete");
        if (PlayAnimationOut)
        {
            animator.Play(GameController.TRANSITION_OUT);
            Invoke(nameof(OnCompleteAnimationOut), 2.5f);
        }
        else
        {
            OnCompleteAnimationOut();
        }
        
        
    }
    void OnCompleteAnimationOut()
    {
        CompleteAnimation?.Invoke(type);
    }

}
