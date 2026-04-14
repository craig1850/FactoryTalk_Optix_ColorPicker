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
#endregion

public class ParseHexLogic : BaseNetLogic    
{
    private IUAVariable hexboxstring;

    public override void Start()
    {
        //private hexbox;
        hexboxstring = Owner.GetVariable("TextBox1");
        hexboxstring.VariableChange += Hexboxstring_VariableChange;
    }

    private void Hexboxstring_VariableChange(object sender, VariableChangeEventArgs e)
    {
        //throw new NotImplementedException();
    }

    public override void Stop()
    {
        hexboxstring.VariableChange -= Hexboxstring_VariableChange;
    }

    
}

