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
       
    public override void Start()
    {
        hexstring = Project.Current.GetVariable("Model/TextBoxHex");
        hexstring.VariableChange += Hexstring_VariableChange; 
    }
    private void Hexstring_VariableChange(object sender, VariableChangeEventArgs e)
    {      
        if (IsHex(e.NewValue))  
        {
            try
            {
                var redbyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/RedByte");
                redbyte.Value = HexToColor(e.NewValue).R;
               
                var greenbyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/GreenByte");
                greenbyte.Value = HexToColor(e.NewValue).G;
                
                var bluebyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/BlueByte");
                bluebyte.Value = HexToColor(e.NewValue).B;
                
                var alphabyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/AlphaByte");
                alphabyte.Value = HexToColor(e.NewValue).A;
               
            }
            catch (Exception)
            {

                //throw;
            }           
        }      

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

}
