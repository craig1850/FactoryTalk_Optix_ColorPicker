#region Using directives
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.NetLogic;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.UI;
using System;
using System.Drawing;
using System.Globalization;
using UAManagedCore;
using Color = System.Drawing.Color;
using OpcUa = UAManagedCore.OpcUa;
#endregion

public class HexToRGBA : BaseNetLogic
{
    private IUAVariable hexstring;
    private bool blockrepeat;

    public override void Start()
    {
        UpdateCurrentColorLabel();
        UpdateNewColorLabel();
        hexstring = Project.Current.GetVariable("Model/TextBoxHex");
        hexstring.VariableChange += Hexstring_VariableChange; 
    }
    private void Hexstring_VariableChange(object sender, VariableChangeEventArgs e)
    {
       
        blockrepeat = true;
        if (IsHex(e.NewValue))  
        {
            try
            {    
                LogicObject.GetVariable("RedGuageVal").Value = HexToColor(e.NewValue).R;               
                LogicObject.GetVariable("GreenGuageVal").Value = HexToColor(e.NewValue).G;               
                LogicObject.GetVariable("BlueGuageVal").Value = HexToColor(e.NewValue).B;
                LogicObject.GetVariable("AlphaGuageVal").Value = HexToColor(e.NewValue).A;
            }
            catch (Exception ex)
            {
                Log.Info($"Error: {ex.Message}");                
            }           
        }  
        blockrepeat = false;
    }   

    private static bool IsHex(string input)
    {
        input = input.Replace("#", "");
        foreach (char c in input)
        {
            bool isHexChar = (c >= '0' && c <= '9') ||
                             (c >= 'a' && c <= 'f') ||
                             (c >= 'A' && c <= 'F');
            if (!isHexChar) return false;
        }
        return true;
    }

    private static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");
       
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        byte a = 255;

        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
        }
        
        return Color.FromArgb(a, r, g, b);
    }

    public override void Stop()
    {
        hexstring.VariableChange -= Hexstring_VariableChange;        
    }

    [ExportMethod]
    public void UpdateNewColorLabel()
    {
        int decimalcolor = LogicObject.GetVariable("NewColorFillColor").Value;
        string hex = $"#{(decimalcolor & 0xFFFFFFFF):x8}";

        hex = hex.Replace("#", "");
        string one = hex.Substring(0, 2);
        string two = hex.Substring(2, 6);

        string result = "#" + two + one;
        LogicObject.GetVariable("NewColorLabelUpdate").Value = "New [RGBA]" + Environment.NewLine + result;

        if (!blockrepeat)
        {
            //Log.Info("UpdatingHexyBox");
            LogicObject.GetVariable("UpdateHexBoxText").Value = result;
        }
    }

    [ExportMethod]
    public void UpdateCurrentColorLabel()
    {
        int decimalcolor = LogicObject.GetVariable("CurrentColorFillColor").Value;
        string hex = $"#{(decimalcolor & 0xFFFFFFFF):x8}";

        hex = hex.Replace("#", "");
        string one = hex.Substring(0, 2);
        string two = hex.Substring(2, 6);

        string result = "#" + two + one;       
        LogicObject.GetVariable("CurrentColorLabelUpdate").Value = "Current [RGBA]" + Environment.NewLine + result;        
    }
}
