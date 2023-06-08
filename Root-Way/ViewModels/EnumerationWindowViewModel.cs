using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;

namespace Root_Way.ViewModels;

public class EnumerationWindowViewModel : ViewModelBase, IReactiveObject
{
    
    private TextBox _targetTextBox;
    private TextBox _commandTextBox;
    private TextBlock _scanResultTextBlock;
    private string _scanResult;
    private ICommand _scanCommand;
    
    public EnumerationWindowViewModel()
    {
        _scanCommand = ReactiveCommand.CreateFromTask(ExecuteScan);
    }

    public ICommand ScanCommand
    {
        get => _scanCommand;
        set => this.RaiseAndSetIfChanged(ref _scanCommand, value);
    }

    public async Task ExecuteScan()
    {
        // Obtener los valores de los controles
        string target = _targetTextBox.Text;
        string command = _commandTextBox.Text;

        // Ejecutar el escaneo en segundo plano
        string result = await Task.Run(() => RunNmapScan(target, command));

        // Mostrar los resultados en el bloque de texto
        ScanResult = result;
    }

    private string RunNmapScan(string target, string command)
    {
        // Construir los argumentos para ejecutar Nmap
        string nmapPath = "/bin/bash"; // Ruta al ejecutable de Nmap
        string arguments = $"{target} {command}";

        // Ejecutar el comando y obtener los resultados
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = nmapPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        process.StartInfo = startInfo;
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return output;
    }

    public string ScanResult
    {
        get => _scanResult;
        set => this.RaiseAndSetIfChanged(ref _scanResult, value);
    }

    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new System.NotImplementedException();
    }
}