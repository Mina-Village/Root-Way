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
using Avalonia;

namespace Root_Way.ViewModels;

public class EnumerationWindowViewModel : ViewModelBase, IReactiveObject
{
    private string _target;
    private string? _port;
    private string _lootDir;
    private string _output;
    private string _errorMessage;
    
    private bool _isServiceDetectionSelected;
    private bool _isOsDetectionSelected;
    private bool _isAttemptOsGuessingSelected;
    private bool _isVerboseModeSelected;
    
    private bool _isHelpSelected;
    private bool _isOpenPortsServiceInfoSelected;
    private bool _isUdpScanSelected;
    private bool _isAggressiveScanSelected;

    private ComboBoxItem _test;

    public ICommand ScanCommand1 { get; }

    public EnumerationWindowViewModel()
    {
        //_scanCommand = ReactiveCommand.Create(ScanButton_Click);
        ScanCommand1 = new ViewModelCommand(ExecuteScanCommand);
        
    }

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
    
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
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
    
    //__________________________________________________________________________________________________________________
    //VERSION DETETCTION
    
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
    
    //__________________________________________________________________________________________________________________
    //COMMON SWITCHES

    public bool IsHelpSelected
    {
        get => _isHelpSelected;
        set
        {
            _isHelpSelected = value;
            OnPropertyChanged(nameof(IsHelpSelected));
        }
    }
    
    public bool IsOpenPortsServiceInfoSelected
    {
        get => _isOpenPortsServiceInfoSelected;
        set
        {
            _isOpenPortsServiceInfoSelected = value;
            OnPropertyChanged(nameof(IsOpenPortsServiceInfoSelected));
        }
    }
    
    public bool IsUdpScanSelected
    {
        get => _isUdpScanSelected;
        set
        {
            _isUdpScanSelected = value;
            OnPropertyChanged(nameof(IsUdpScanSelected));
        }
    }
    
    public bool IsAggressiveScanSelected
    {
        get => _isAggressiveScanSelected;
        set
        {
            _isAggressiveScanSelected = value;
            OnPropertyChanged(nameof(IsAggressiveScanSelected));
        }
    }
    
    public ComboBoxItem Test
    {
        get => _test;
        set
        {
            _test = value;
            OnPropertyChanged(nameof(_test));
        }
    }

    private void ExecuteScanCommand(object obj)
    {
        string target = Target;
        string lootDir = LootDir;
        string? port = Port;
        string nmapArguments = "";

        if (string.IsNullOrEmpty(Target) || string.IsNullOrEmpty(LootDir) )
        {
            ErrorMessage = "ERROR, Target or Loot directory missing";
            return;
        }
        else
            ErrorMessage = " ";

        Output = "Starting NMAP scan for " + target + "\n";

        // Verificar los argumentos seleccionados
        //VERSION DETETCTION
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
        
        //COMMON SWITCHES
        if (IsHelpSelected)
        {
            nmapArguments += " -h";
        }
        if (IsOpenPortsServiceInfoSelected)
        {
            nmapArguments += " -sV";
        }
        if (IsUdpScanSelected)
        {
            nmapArguments += " -sU";
        }
        if (IsAggressiveScanSelected)
        {
            nmapArguments += " -A";
        }
        
        //PORTS
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
        Console.WriteLine();
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