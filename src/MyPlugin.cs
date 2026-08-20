using McAdminPlugins;

namespace PluginExample;

public class BipBop(IPluginNavigation nav, IServerPluginFiles files) : IPlugin
{
    public static IServerPluginFiles? Files { get; private set; }
    
    public Task Load()
    {
        Files = files;
        
        nav.AddPage(
            text: "Example plugin",
            href: "/example",
            order: 10,
            administratorOnly: true,
            glyph: "glyph-plugin"
        );
        
        return Task.CompletedTask;
    }
}