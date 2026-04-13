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

public class HexBoxInputChangedRed : BaseNetLogic
{
    private IUAVariable hexstring;
    
    public override void Start()
    {
        hexstring = Project.Current.GetVariable("Model/TextBoxHex");        
        hexstring.VariableChange += Hexstring_VariableChange;
        var returnerror = Project.Current.GetVariable("Model/HexInputReturnError").Value = "Pick a nice one";
    }

    private void Hexstring_VariableChange(object sender, VariableChangeEventArgs e)
    {
       
        if ((e.NewValue.ToString().Length >= 6) && (IsHex(e.NewValue)))
        {
            //Owner.GetVariable("Value").Value = HexToColor(e.NewValue).R;
            //    //var redguage = Project.Current.Get<LinearGauge>("UI/DialogBoxes/ColorPickerDialog/ColorPicker/VerticalLayout1/RedHorizontalLayout/RedGuage");
            //    //redguage.Value = HexToColor(hex).R;
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
        try
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

                var returnerror = Project.Current.GetVariable("Model/HexInputReturnError").Value = "Sweet Choice Bro";
                return Color.FromArgb(a, r, g, b);

            }
            else
            {
                var returnerror = Project.Current.GetVariable("Model/HexInputReturnError").Value = "Invalid Input, returning White";
                //returnerror.Value = "Invalid Input";
                Log.Info(returnerror.Value.ToString());
                //returnerror.Value = "hello";
                return Color.White;
            }
        }
        catch (Exception)
        {

            throw new Exception("ERROR");

        }

        
    }


    public override void Stop()
    {
        hexstring.VariableChange -= Hexstring_VariableChange;        
    }
}
