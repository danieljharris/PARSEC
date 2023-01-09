using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public bool isPresenter = false;
    public List<NodeSpecs> nodeSpecs = new List<NodeSpecs>();

    // Software
    public void Highlight(Software software, Color color)
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.software.Contains(software))
                    specs.RPC_Highlight(color);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.software.Contains(software))
                    specs.Highlight(color);
        }
    }
    public void UnHighlight(Software software, Color color)
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.software.Contains(software))
                    specs.RPC_UnHighlight(color);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.software.Contains(software))
                    specs.UnHighlight(color);
        }
    }

    // Hardware
    public void Highlight(Hardware hardware, Color color)
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.hardware.Contains(hardware))
                    specs.RPC_Highlight(color);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.hardware.Contains(hardware))
                    specs.Highlight(color);
        }
    }
    public void UnHighlight(Hardware hardware, Color color)
    {
        if (isPresenter)
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.hardware.Contains(hardware))
                    specs.RPC_UnHighlight(color);
        }
        else
        {
            foreach (NodeSpecs specs in nodeSpecs)
                if (specs.hardware.Contains(hardware))
                    specs.UnHighlight(color);
        }
    }
}