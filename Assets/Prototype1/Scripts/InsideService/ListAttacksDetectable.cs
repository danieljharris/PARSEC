using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ListAttacksDetectable : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    void Start()
    {
        // Wait for the NodeSpecs to be initialized
        StartCoroutine(LateStart(0.5f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CreateListOfAttacksDetectable();
    }

    void CreateListOfAttacksDetectable()
    {
        // Get all the names of the AttackType enum
        List<string> attackTypes = Enum.GetNames(typeof(AttackType)).ToList();

        for (int i = 0; i < attackTypes.Count; i++)
        {
            string AttackType = attackTypes[i];
            string detectability = nodeSpecs.AttackTypeDetectability[i].ToString();

            // Add space before capital letters
            AttackType = string.Concat(AttackType.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
            
            text.text += "- " + AttackType + ": " + detectability + "\n";
        }
    }
}