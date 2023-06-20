using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;

namespace Root_Way.ViewModels;

public class OsintWindowViewModel : ViewModelBase, IReactiveObject
{
    private string _target;
    private string _lootDir;
    private string _output;
    private ICommand _scanCommand;
    
    public ICommand ScanCommand1 { get; }

    public OsintWindowViewModel()
    {
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
    
    private void ExecuteScanCommand(object obj)
    {
        string target = Target;
        string lootDir = LootDir;
        
        Output += "Starting OSINT scan for " + target + "\n";
        
        // Gather WHOIS info
        Output += "Gathering WHOIS info...\n";
        ProcessStartInfo whoisInfo = new ProcessStartInfo("/usr/bin/whois", target);
        whoisInfo.RedirectStandardOutput = true;
        whoisInfo.UseShellExecute = false;
        Process whois = Process.Start(whoisInfo);
        string whoisOutput = whois.StandardOutput.ReadToEnd();
        whois.WaitForExit();
        string whoisFilePath = Path.Combine(lootDir, "osint", "whois-" + target + ".txt");
        //create the directory if not exists
        Directory.CreateDirectory(Path.GetDirectoryName(whoisFilePath));
        File.WriteAllText(whoisFilePath, whoisOutput);
        Output += "WHOIS info saved to " + whoisFilePath + "\n";
        
        // Check email security
        Output += "Checking for email security...\n";
        string iportDomainKey = "iport._domainkey." + target;
        string dmarc = "_dmarc." + target;
        string emailFilePath = Path.Combine(lootDir, "osint/email-" + target + ".txt");
        Directory.CreateDirectory(Path.GetDirectoryName(emailFilePath));
        using (StreamWriter writer = new StreamWriter(emailFilePath))
        {
            writer.WriteLine("SPF records:");
            writer.WriteLine("------------");
            RunDnsQuery(target, "txt", "spf", writer);
            RunDnsQuery(iportDomainKey, "txt", "spf", writer);
            RunDnsQuery(dmarc, "txt", "spf", writer);
            writer.WriteLine();
            writer.WriteLine("DMARC records:");
            writer.WriteLine("--------------");
            RunDnsQuery(dmarc, "txt", "dmarc", writer);
            writer.WriteLine();
            writer.WriteLine("DKIM records:");
            writer.WriteLine("-------------");
            RunDnsQuery(iportDomainKey, "txt", "dkim", writer);
        }
        Output += "Email security info saved to " + emailFilePath + "\n";
        
        // Gather UltraTools DNS info
        Output += "Gathering MxToolBox DNS info...\n";
        string ultraToolsUrl = "https://mxtoolbox.com/SuperTool.aspx?action=mx%3a "+ target + "&run=toolpage" ;
        string ultraToolsFilePath = Path.Combine(lootDir, "osint/ultratools-" + target + ".html");
        using (WebClient client = new WebClient())
        {
            string ultraToolsOutput = client.DownloadString(ultraToolsUrl);
            Directory.CreateDirectory(Path.GetDirectoryName(ultraToolsFilePath));
            File.WriteAllText(ultraToolsFilePath, ultraToolsOutput);
        }
        Output += "UltraTools DNS info saved to " + ultraToolsFilePath + "\n";
    }

    /*private void ScanButton_Click()
    {
        
        // Gather Shodan info
        Output += "Gathering Shodan info...\n";
        string shodanApiKey = "YOUR_SHODAN_API_KEY_HERE";
        string shodanUrl = "https://api.shodan.io/shodan/host/" + target + "?key=" + shodanApiKey;
        string shodanFilePath = Path.Combine(lootDir, "osint/shodan-" + target + ".json");
        using (WebClient client = new WebClient())
        {
            string shodanOutput = client.DownloadString(shodanUrl);
            File.WriteAllText(shodanFilePath, shodanOutput);
        }
        Output += "Shodan info saved to " + shodanFilePath + "\n";

        // Gather VirusTotal info
        Output += "Gathering VirusTotal info...\n";
        string virusTotalApiKey = "YOUR_VIRUSTOTAL_API_KEY_HERE";
        string virusTotalUrl = "https://www.virustotal.com/vtapi/v2/url/report?apikey=" + virusTotalApiKey + "&resource=" + target;
        string virusTotalFilePath = Path.Combine(lootDir, "osint/virustotal-" + target + ".json");
        using (WebClient client = new WebClient())
        {
            string virusTotalOutput = client.DownloadString(virusTotalUrl);
            File.WriteAllText(virusTotalFilePath, virusTotalOutput);
        }
        Output += "VirusTotal info saved to " + virusTotalFilePath + "\n";

        Output += "OSINT scan completed.\n";
    }*/

    private void RunDnsQuery(string domain, string type, string record, StreamWriter writer)
    {
        string dnsQuery = "-type=" + type + " " + domain;
        ProcessStartInfo dnsQueryInfo = new ProcessStartInfo("/usr/bin/nslookup", dnsQuery);
        dnsQueryInfo.RedirectStandardOutput = true;
        dnsQueryInfo.UseShellExecute = false;
        Process dns = Process.Start(dnsQueryInfo);
        string dnsOutput = dns.StandardOutput.ReadToEnd();
        dns.WaitForExit();
        writer.WriteLine(record + " record for " + domain + ":");
        writer.WriteLine("-----------------------------");
        writer.WriteLine(dnsOutput);
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

