using COMPASS.Sdk.Helpers;
using COMPASS.Sdk.Interfaces.Models;
using COMPASS.Sdk.Interfaces.Plugins;
using QuickAccessPlugin.ViewModels;
using QuickAccessPlugin.Views;
using System.Reflection;

namespace QuickAccessPlugin
{
    public class QuickAccessPlugin : IPlugin, ICodexContextMenuPlugin
    {

        public PluginDescriptor? PluginDescriptor => new()
        {
            Author = "Anthony Bernaert",
            Description = "This plugin allows to set the codex description from the context menu.",
            FriendlyName = "Quick Access Plugin",
            MinimumHostVersion = new("1.7.6"),
            Version = Assembly.GetExecutingAssembly().GetName().Version,
        };

        // Define the capabilities of this plugin
        private readonly IEnumerable<CodexContextMenuItem> _items = new List<CodexContextMenuItem>()
        {
            new(){ Caption = "Edit description...", Description = "", Icon = "", Action = EditDescriptionAction},
            new(){ Caption = "Clear description", Description = "", Icon = "", Action = ClearDescriptionAction},
        };
        public IEnumerable<CodexContextMenuItem> MenuItems { get => _items; }

        public static void EditDescriptionAction(IEnumerable<ICodex> codices)
        {
            string initialDescription = codices.First().Description;
            if (!codices.All(x => x.Description == initialDescription))
            {
                initialDescription = string.Empty;
            }
            var viewModel = new DescriptionViewModel(initialDescription);
            var window = new DescriptionWindow(viewModel);
            if (window.ShowDialog().GetValueOrDefault())
            {
                foreach(var codex in codices)
                {
                    codex.Description = viewModel.DescriptionInput;
                }
            }
        }

        public static void ClearDescriptionAction(IEnumerable<ICodex> codices)
        {
            foreach(var codex in codices)
            {
                codex.Description = "";
            }
        }
    }
}