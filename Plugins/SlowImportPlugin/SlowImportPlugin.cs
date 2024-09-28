using COMPASS.Sdk.Helpers;
using COMPASS.Sdk.Interfaces;
using COMPASS.Sdk.Interfaces.Models;
using COMPASS.Sdk.Interfaces.Plugins;
using Microsoft.Win32;
using SlowImportPlugin.ViewModels;
using SlowImportPlugin.Views;
using System.IO;
using System.Reflection;
using System.Windows;

namespace SlowImportPlugin
{
    public class SlowImportPlugin : IPlugin, IImportSourcePlugin
    {

        public PluginDescriptor? PluginDescriptor => new()
        {
            Author = "Anthony Bernaert",
            Description = "This plugin does a file import, but slowly to test asynchronous behavior.",
            FriendlyName = "Slow Import Plugin",
            MinimumHostVersion = new("1.7.6"),
            Version = Assembly.GetExecutingAssembly().GetName().Version,
        };

        // Define the capabilities of this plugin
        public IEnumerable<ImportSourceItem> ImportSources => new List<ImportSourceItem>()
        {
            new()
            {
                Caption = "Slow File Import...",
                ImportAsync = ImportAsync
            },
            new()
            {
                Caption = "Dummy",
                ImportAsync = DummyAsync
            }
        };

        public static async IAsyncEnumerable<ICodex> ImportAsync(ICodexFactory codexFactory, ImportProgressReporter progressReporter, CancellationTokenSource cancellationTokenSource)
        {
            var viewModel = new ImportViewModel();
            var window = new SlowImportWindow(viewModel);
            if (window.ShowDialog().GetValueOrDefault())
            {
                OpenFileDialog ofd = new()
                {
                    AddExtension = false,
                    Multiselect = true,
                };
                if (ofd.ShowDialog().GetValueOrDefault())
                {
                    var files = ofd.FileNames.ToList();
                    int itemsDone = 0;
                    progressReporter.Report(0, files.Count);
                    foreach ( var file in files) { // Currently there is no check for already imported or banished files
                        await Task.Delay((int)(viewModel.IntervalInput * 1000f));
                        cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        var codex = codexFactory.CreateCodex();
                        codex.Title = Path.GetFileName(file);
                        codex.Description = "Imported by 'Slow Import Plugin'";
                        progressReporter.Report(++itemsDone, files.Count);
                        yield return codex;
                    }
                }
            }
        }

        public static async IAsyncEnumerable<ICodex> DummyAsync(ICodexFactory codexFactory, ImportProgressReporter progressReporter, CancellationTokenSource cancellationTokenSource)
        {
            MessageBox.Show("This dummy import source doesn't do anything.");
            await Task.CompletedTask;
            yield break;
        }



    }
}