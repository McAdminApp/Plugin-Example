using McAdminPlugins;

namespace PluginExample;

public class MyPlugin(IPluginPages pages, IServerPluginFiles files) : IPlugin
{
    public static IServerPluginFiles? Files { get; private set; }

    private static readonly PluginSettingsSection SettingsSection = new PluginSettingsSection
    {
        Title = "Settings example",
        Description = "This is the PluginSettingsSection",
        LoadAsync = async ct => [
            new PluginField("motd", "Message of the day")
            {
                Value = "test",
                Description = "The message of the day"
            },
            new PluginField("ip-address", "IP Address")
            {
                Value = "127.0.0.1",
                Description = "The IP address of the server",
                Group = "Network"
            },
            new PluginField("port", "Port")
            {
                Value = "25565",
                Description = "The port of the server",
                Group = "Network"
            },
            new PluginField("some-bool", "Boolean value")
            {
                Value = "true",
                Description = "Boolean value setting",
                Kind = PluginFieldKind.Toggle,
                Group = "Setting types"
            },
            new PluginField("some-int", "Integer value")
            {
                Value = "100",
                Description = "Integer value setting",
                Kind = PluginFieldKind.Number,
                Group = "Setting types",
                Maximum = 100,
                Minimum = 1
            },
            new PluginField("some-choice", "Choice value")
            {
                Description = "Choice value setting",
                Group = "Setting types",
                Kind = PluginFieldKind.Choice,
                Choices = ["A choice", "Another choice", "Preselected choice"],
                Value = "Preselected choice"
            }
        ],
        SaveAsync = async (changes, ct) =>
        {
            foreach (var (key, value) in changes)
            {
                Console.WriteLine(key + " : " + value);
            }

            return PluginResult.Success();
        }
    };

    private static readonly PluginTableSection TableSection = new PluginTableSection
    {
        Title = "Table example",
        Description = "This is the PluginTableSection",
        Columns =
        [
            new PluginColumn("Id"),
            new PluginColumn("Player"),
            new PluginColumn("Level")
        ],
        LoadAsync = async ct =>
        [
            new PluginRow
            {
                Cells = ["1", "rlHypr", "1337"]
            },
            new PluginRow
            {
                Cells = ["2", "jeb_", "9999999+"],
                Highlight = true,
                Note = "This row is highlighted!"
            }
        ]
    };

    private static readonly PluginFormSection FormSection = new PluginFormSection
    {
        Title = "Form example",
        Description = "This is the PluginFormSection",
        Fields = [
            new PluginField("a-key", "Label of the field")
            {
                Description = "Description of the field",
                Placeholder = "It can use placeholders!"
            },
            new PluginField("password", "Password")
            {
                Description = "Enter a password. This is required!",
                Required = true,
                Kind = PluginFieldKind.Password
            }
        ],
        SubmitAsync = async (data, ct) =>
        {
            foreach (var (key, value) in data)
            {
                Console.WriteLine(key + " : " + value);
            }
            
            return PluginResult.Success();
        }
    };

    private static readonly PluginActionsSection ActionsSection = new PluginActionsSection
    {
        Title = "Action example",
        Description = "This is the PluginActionsSection",
        Actions = [
            new PluginAction("An action", async token =>
            {
                Console.WriteLine("An action has been performed!");
                
                return PluginResult.Failure("Example of a failed action");
            })
            {
                Style = PluginButtonStyle.Primary,
                Description = "This sends a log in console"
            },
            new PluginAction("Close service", async ct =>
            {
                Environment.Exit(1);
                return PluginResult.Success();
            })
            {
                Style = PluginButtonStyle.Danger,
                Description = "This will stop the web server entirely"
            }
        ]
    };

    private static readonly PluginNoticeSection NoticeSection = new PluginNoticeSection
    {
        Title = "Notice example",
        Description = "This is the PluginNoticeSection",
        Heading = "A heading",
        Text = "Some text goes in here! Available in several colors depending on the type of notice",
        Kind = PluginNoticeKind.Warning
    };

    private static readonly PluginTextSection TextSection = new PluginTextSection
    {
        Title = "Text example",
        Description = "This is the PluginTextSection",
        Facts =
        [
            new PluginFact("A fact", "This minecraft web manager is the best"),
            new PluginFact("Did you know?", "Minecraft was made in 2011")
        ],
        Paragraphs =
        [
            "Paragraph 1",
            "Paragraph 2"
        ]
    };
    

    public Task Load()
    {
        Files = files;
        
        pages.AddPage(new PluginPage("example", "Example Addon")
        {
            Description = "This page showcases different section types.",
            Sections = [
                SettingsSection,
                TableSection,
                FormSection,
                ActionsSection,
                NoticeSection,
                TextSection
            ]
        });
        
        return Task.CompletedTask;
    }
}