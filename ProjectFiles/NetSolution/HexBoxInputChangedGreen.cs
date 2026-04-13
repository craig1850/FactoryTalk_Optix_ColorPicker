#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.System;
using FTOptix.CoreBase;
using FTOptix.Retentivity;
using FTOptix.NetLogic;
using FTOptix.SerialPort;
using FTOptix.Core;
using Color = System.Drawing.Color;
#endregion

public class HexBoxInputChangedGreen : BaseNetLogic
{
    private IUAVariable hexstring;
    public override void Start()
    {
        hexstring = Project.Current.GetVariable("Model/TextBoxHex");
        hexstring.VariableChange += Hexstring_VariableChange;
    }

    private void Hexstring_VariableChange(object sender, VariableChangeEventArgs e)
    {  
        //Owner.GetVariable("Value").Value = HexToColor(e.NewValue).G; 
        //var greenguage = Project.Current.Get<LinearGauge>("UI/DialogBoxes/ColorPickerDialog/ColorPicker/VerticalLayout1/GreenHorizontalLayout/GreenGuage");
        //greenguage.Value = HexToColor(hex).G;      
    }

    private static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");

        if (hex.Length >= 6)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = 255;


            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return Color.FromArgb(a, r, g, b);
        }
        else
        {
            return Color.White;
        }

    }

    public override void Stop()
    {
        hexstring.VariableChange -= Hexstring_VariableChange;
    }
}
