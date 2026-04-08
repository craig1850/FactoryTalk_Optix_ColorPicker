using System;
using UAManagedCore;

//-------------------------------------------
// WARNING: AUTO-GENERATED CODE, DO NOT EDIT!
//-------------------------------------------

[MapType(NamespaceUri = "FactoryTalk_Optix_ColorPicker", Guid = "af5cb295c2b5c257ddb546654642e79c")]
public class AlphaARGBtoDecimal : FTOptix.CoreBase.ExpressionEvaluator
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
