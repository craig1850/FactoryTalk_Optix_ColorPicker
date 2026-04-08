using System;
using UAManagedCore;

//-------------------------------------------
// WARNING: AUTO-GENERATED CODE, DO NOT EDIT!
//-------------------------------------------

[MapType(NamespaceUri = "FactoryTalk_Optix_ColorPicker", Guid = "b82f1c8faa8616e3112b21fedcc6f6a4")]
public class BlueARGBtoDecimal : FTOptix.CoreBase.ExpressionEvaluator
{
#region Children properties
    //-------------------------------------------
    // WARNING: AUTO-GENERATED CODE, DO NOT EDIT!
    //-------------------------------------------
    public object Source0
    {
        get
        {
            return (object)Refs.GetVariable("Source0").Value.Value;
        }
        set
        {
            Refs.GetVariable("Source0").SetValue(value);
        }
    }
    public IUAVariable Source0Variable
    {
        get
        {
            return (IUAVariable)Refs.GetVariable("Source0");
        }
    }
#endregion
}
