using Volo.Abp.Settings;

namespace SAO.Settings;

public class SAOSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SAOSettings.MySetting1));
    }
}
