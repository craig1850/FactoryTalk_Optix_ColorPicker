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
        //private RemoteVariableSynchronizer variableSynchronizer;

    public override void Start()
    {
        hexstring = Project.Current.GetVariable("Model/TextBoxHex");
        hexstring.VariableChange += Hexstring_VariableChange;
       
        //the following three rows are necessary when variables are exchanged with external device (e.g. PLC) and they are not in use
        //because not linked to any object in page
        //variableSynchronizer = new RemoteVariableSynchronizer();
        //variableSynchronizer.Add(hexstring);
        //variableSynchronizer.Add(addend2);

    }
    private void Hexstring_VariableChange(object sender, VariableChangeEventArgs e)
    {

        //var redguage = Project.Current.Get<LinearGauge>("UI/DialogBoxes/ColorPickerDialog/ColorPicker/VerticalLayout1/RedHorizontalLayout/RedGuage");
        //redguage.Value = HexToColor(e.NewValue).R;
        //Log.Info(redguage.Value.ToString());

        //var greenguage = Project.Current.Get<LinearGauge>("UI/DialogBoxes/ColorPickerDialog/ColorPicker/VerticalLayout1/GreenHorizontalLayout/GreenGuage");
        //greenguage.Value = HexToColor(hex).G;
        //Log.Info(greenguage.Value.ToString());

        //var blueguage = Project.Current.Get<LinearGauge>("UI/DialogBoxes/ColorPickerDialog/ColorPicker/VerticalLayout1/BlueHorizontalLayout/BlueGuage");
        //blueguage.Value = HexToColor(hex).B;
        //Log.Info(blueguage.Value.ToString());

        //var alphaguage = Project.Current.Get<LinearGauge>("UI/DialogBoxes/ColorPickerDialog/ColorPicker/VerticalLayout1/AlphaHorizontalLayout/AlphaGuage");
        //alphaguage.Value = HexToColor(e.NewValue).A;
        //Log.Info(alphaguage.Value.ToString());

        if (IsHex(e.NewValue) && (e.NewValue.ToString().Trim().Length >= 6))
            Log.Info(e.NewValue.ToString());
            Log.Info(e.NewValue.ToString().Length.ToString());
        {
            var redbyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/RedByte"); //= (HexToColor(e.NewValue));
            redbyte.Value = HexToColor(e.NewValue).R;
            Log.Info(redbyte.Value.ToString());

            var greenbyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/GreenByte"); //= (HexToColor(e.NewValue));
            greenbyte.Value = HexToColor(e.NewValue).G;
            Log.Info(greenbyte.Value.ToString());

            var bluebyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/BlueByte"); //= (HexToColor(e.NewValue));
            bluebyte.Value = HexToColor(e.NewValue).B;
            Log.Info(bluebyte.Value.ToString());

            var alphabyte = Project.Current.GetVariable("Model/NetLogixHexReturnRGBA/AlphaByte"); //= (HexToColor(e.NewValue));
            alphabyte.Value = HexToColor(e.NewValue).A;
            Log.Info(alphabyte.Value.ToString());
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
   
    public override void Stop()
    {
        hexstring.VariableChange -= Hexstring_VariableChange;        
    }

}
