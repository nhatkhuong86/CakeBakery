using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    STEP curStep = STEP.STEP1;
    //  Game Const
    public static string TRANSITION_IN = "TransitionIn";
    public static string TRANSITION_OUT = "TransitionOut";

    [SerializeField] GameObject FlowDesc;
    [SerializeField] Button btnNext;
    [SerializeField] GameObject bowl;
    [SerializeField] GameObject step1Container;
    [SerializeField] GameObject step2Container;
    [SerializeField] GameObject step3Container;
    [Header("=======================STEP 1=======================")]    
    [SerializeField] GameObject cakeFlour;
    [SerializeField] GameObject cakeFlourInBowl;

    [Header("=======================STEP 2=======================")]    
    [SerializeField] GameObject milk;
    [SerializeField] GameObject milkInBowl;
    [SerializeField] GameObject butterBox;
    [SerializeField] GameObject spoon;

    [Header("=======================STEP 3=======================")]    
    [SerializeField] GameObject blender;
    [SerializeField] GameObject mixedBatter;
    [SerializeField] GameObject mixedBatter1;
    [SerializeField] GameObject mixedBatter2;
    [SerializeField] GameObject mixedBatter3;
    [SerializeField] GameObject splatteredBatter;


    public static GameController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Add Event Click Next Step
        btnNext.onClick.AddListener(()=> {
            Debug.Log("Next");
        });

        // Add Event Call Back
        ModelActionClick.CompleteAnimation += AnimationPlayComplete;

        // Show Step 1        
        ShowStep(step1Container);

        // Set Description
        VisibleDescription(true);
        SetDescription("STEP 1 \n PLEASE CHOOSE CAKE FLOUR TO BEGIN");
    }

    private void AnimationPlayComplete(string type)
    {
        switch (type)
        {
            case "0":
                curStep = STEP.STEP2;
                VisibleDescription(true);
                SetDescription("STEP 2-1 \n WE NEED PUSH MILK TO BOWL");
                ShowStep(step2Container);
                break;
            case "1":
                curStep = STEP.STEP2_2;
                VisibleDescription(true);
                SetDescription("STEP 2-2 \n WE NEED PUSH LITTLE BUTTER TO MAKE IT MORE FLAVORFUL");
                milk.SetActive(false);
                break;
            case "2":
                curStep = STEP.STEP3;
                VisibleDescription(true);
                SetDescription("STEP 3 \n GREAT! KEEP GOING... NOW WITH BLENDER WE CAN COMPLETE YOUR JOB");
                ShowStep(step3Container);
                break;
            case "3":
                curStep = STEP.STEP4;
                VisibleDescription(true);
                SetDescription("STEP \n TIME UP... THANKS FOR YOUR TEST INTERVIEW. HAVE A GREAT DAY TO YOU. BEST REGARDS");
                ShowStep(step3Container);
                break;
        }
    }
    public void SetDescription(string desc)
    {
        FlowDesc.GetComponentInChildren<Text>().text = desc;
    }
    public void VisibleDescription(bool val)
    {
        FlowDesc.SetActive(val);
        DoFade(val);
    }
    void ShowStep(GameObject stepContainer)
    {
        HideAllStep();
        stepContainer.SetActive(true);
    }
    void HideAllStep()
    {
        //Hide All Step
        step1Container.SetActive(false);
        step2Container.SetActive(false);
        step3Container.SetActive(false);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }


    public void DoFade(bool fadeIn)
    {
        if (this.gameObject.activeInHierarchy == false)
        {
            return;
        }

        if (fadeIn)
        {
            StartCoroutine(DoFadeIn(FlowDesc.GetComponent<CanvasGroup>()));
        }
        else
        {
            StartCoroutine(DoFadeOut(FlowDesc.GetComponent<CanvasGroup>(), 0.5f));
        }
    }

    IEnumerator DoFadeIn(CanvasGroup canvasGroup, float time = 1)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }

        canvasGroup.alpha = 1;
        yield return null;
    }

    IEnumerator DoFadeOut(CanvasGroup canvasGroup, float time = 1)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }

        canvasGroup.alpha = 0;
        yield return null;
    }

    public enum STEP{
        STEP1,
        STEP2,
        STEP2_2,
        STEP3,
        STEP4,
    }
}
