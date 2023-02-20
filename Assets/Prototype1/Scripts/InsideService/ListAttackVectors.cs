using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ListAttackTypes : MonoBehaviour
{
    public TextMeshPro text;
    public NodeSpecs nodeSpecs;
    void Start()
    {
        // Get all the names of the AttackType enum
        List<string> attackTypes = Enum.GetNames(typeof(AttackType)).ToList();

        for (int i = 0; i < attackTypes.Count; i++)
        {
            string AttackType = attackTypes[i];
            string detectability = nodeSpecs.AttackTypeDetectability[i].ToString();

            text.text += AttackType + ": " + detectability + "\n";
        }
    }
}