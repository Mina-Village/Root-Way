using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Input;

namespace Root_Way.ViewModels;

public class EnumerationWindowViewModel : ViewModelBase, IReactiveObject
{
    private string _target;
    private string? _port;
    private string _lootDir;
    private string _output;
    private bool _isServiceDetectionSelected;
    private bool _isOsDetectionSelected;
    private bool _isAttemptOsGuessingSelected;
    private bool _isVerboseModeSelected;
    
    private ICommand _scanCommand;
    
    public ICommand ScanCommand1 { get; }

    public EnumerationWindowViewModel()
    {
        //_scanCommand = ReactiveCommand.Create(ScanButton_Click);
        ScanCommand1 = new ViewModelCommand(ExecuteScanCommand);
    }

    public ICommand ScanCommand => _scanCommand;

    public string Target
    {
        get => _target;
        set
        {
            _target = value;
            OnPropertyChanged(nameof(Target));
        }
    }

    public string LootDir
    {
        get => _lootDir;
        set
        {
            _lootDir = value;
            OnPropertyChanged(nameof(LootDir));
        }
    }

    public string Output
    {
        get => _output;
        set
        {
            _output = value;
            OnPropertyChanged(nameof(Output));
        }
    }
    
    public string? Port
    {
        get => _port;
        set
        {
            _port = value;
            OnPropertyChanged(nameof(Port));
        }
    }
    
    //CHECK BOXES 
    public bool IsServiceDetectionSelected
    {
        get => _isServiceDetectionSelected;
        set
        {
            _isServiceDetectionSelected = value;
            OnPropertyChanged(nameof(IsServiceDetectionSelected));
        }
    }
    
    public bool IsOsDetectionSelected
    {
        get => _isOsDetectionSelected;
        set
        {
            _isOsDetectionSelected = value;
            OnPropertyChanged(nameof(IsOsDetectionSelected));
        }
    }
    
    public bool IsAttemptOsGuessingSelected
    {
        get => _isAttemptOsGuessingSelected;
        set
        {
            _isAttemptOsGuessingSelected = value;
            OnPropertyChanged(nameof(IsAttemptOsGuessingSelected));
        }
    }
    
    public bool IsVerboseModeSelected
    {
        get => _isVerboseModeSelected;
        set
        {
            _isVerboseModeSelected = value;
            OnPropertyChanged(nameof(IsVerboseModeSelected));
        }
    }
    
    private void ExecuteScanCommand(object obj)
    {
        string target = Target;
        string lootDir = LootDir;
        string? port = Port;
        string nmapArguments = "";

        Output += "Starting NMAP scan for " + target + "\n";

        // Verificar los argumentos seleccionados
        if (IsServiceDetectionSelected)
        {
            nmapArguments += " -sS";
        }
        if (IsOsDetectionSelected)
        {
            nmapArguments += " -O";
        }
        if (IsAttemptOsGuessingSelected)
        {
            nmapArguments += " --osscan-guess";
        }
        if (IsVerboseModeSelected)
        {
            nmapArguments += " -v";
        }
        
        if (!string.IsNullOrEmpty(Port))
        {
            nmapArguments += " -p " + Port + " " + target;
        }
        else
        {
            nmapArguments += " " + target;
        }
        

        // Gather NMAP info
        Output += "Gathering ports scan info...\n";
        Console.WriteLine("/usr/bin/nmap" + nmapArguments);
        //ProcessStartInfo nmapInfo = new ProcessStartInfo( "sudo", "/usr/bin/nmap " + nmapArguments);
        ProcessStartInfo nmapInfo = new ProcessStartInfo( "/usr/bin/nmap" , nmapArguments);
        nmapInfo.RedirectStandardOutput = true;
        nmapInfo.UseShellExecute = false;
        Process nmap = Process.Start(nmapInfo);
        string nmapOutput = nmap.StandardOutput.ReadToEnd();
        nmap.WaitForExit();
        string nmapFilePath = Path.Combine(lootDir, "nmap", "nmap-" + target + ".txt");
        // Create the directory if it doesn't exist
        Directory.CreateDirectory(Path.GetDirectoryName(nmapFilePath));
        File.WriteAllText(nmapFilePath, nmapOutput);
        Output += "NMAP info saved to " + nmapFilePath + "\n";
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