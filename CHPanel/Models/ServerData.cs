using System.Windows.Media;
using CHPanel.Models.Dtos;

namespace CHPanel.Models; 

/// <summary>
/// Truncated data about the server.
/// </summary>
public class ServerData {
	public ServerData(ServerInfo? serverInfo, ServerResources? resources) {
		if (serverInfo == null || resources == null) {
			Allocation = new ServerInfo();
			Info = new ServerInfo();
			Resources = new ServerResources();
			return;
		}

		Update(serverInfo, resources);
	}

	public void Update(ServerInfo serverInfo, ServerResources resources) {
		Info = serverInfo;
		Resources = resources;
		Allocation = Info.attributes!.relationships?.allocations?.data[0] ?? new ServerInfo();
		
		Build();
	}
	
	/// <summary>
	/// Used to access info about the allocation, IE IP, port, etc.
	/// </summary>
	public ServerInfo Allocation { get; private set; } = null!;
	/// <summary>
	/// The raw server info
	/// </summary>
	public ServerInfo Info { get; private set; } = null!;
	/// <summary>
	/// Used to access info about server resources, IE CPU, memory, etc.
	/// </summary>
	public ServerResources Resources { get; set; } = null!;

	public string Name { get; set; } = "N/A";
	public string Ip { get; set; } = "N/A";
	public bool IsOnline { get; set; }
	public Brush StatusColor => IsOnline ? Brushes.Green : Brushes.Red;
	public string Status => IsOnline ? "Online" : "Offline";
	public string RawStatus => Resources.attributes?.current_state ?? string.Empty;
    
	public string MaxCpu { get; set; } = string.Empty;
	public string CurrentCpu { get; set; } = string.Empty;
	
	public string MaxMemory { get; set; } = string.Empty;
	public string CurrentMemory { get; set; } = string.Empty;
	
	public string MaxDisk { get; set; } = string.Empty;
	public string CurrentDisk { get; set; } = string.Empty;
	
	public string RawCpuStats => $"{RawCurrentCpu:F1}% / {RawMaxCpu:F1}%";
	public int CpuUsage => (int) (RawCurrentCpu / RawMaxCpu * 100);
	public double RawMaxCpu { get; set; }
	public double RawCurrentCpu { get; set; }
	
	public string RawMemoryStats => $"{RawCurrentMemory:F1} GB / {FormatGb(RawMaxMemory, true)}";
	public int MemoryUsage => (int) (RawCurrentMemory / RawMaxMemory * 100);
	public double RawMaxMemory { get; set; }
	public double RawCurrentMemory { get; set; }
    
	public string RawDiskStats => $"{RawCurrentDisk:F1} GB / {FormatGb(RawMaxDisk, true)}";
	public int DiskUsage => (int) (RawCurrentDisk / RawMaxDisk * 100);
	public double RawMaxDisk { get; set; }
	public double RawCurrentDisk { get; set; }
    
	private void Build() {
		//I hate everything about this but basically this just formats the data from the ServerInfo and ServerResources objects to use in the UI
		Name = Info.attributes.name;
		IsOnline = Resources.attributes?.current_state == "running";
		Ip = (string.IsNullOrEmpty((string) Allocation.attributes?.ip_alias!) ? Allocation.attributes?.ip : (string) Allocation.attributes.ip_alias) ?? string.Empty;
		Ip += $":{Allocation.attributes?.port}";
		
		CurrentCpu = Resources.attributes.resources.cpu_absolute.ToString("F1") + "%";
		MaxCpu = "of " + Info.attributes.limits.cpu.ToString("F1") + "%";

		CurrentMemory = ByteToGb(Resources.attributes.resources.memory_bytes);
		MaxMemory = "of " + ByteToGb(Info.attributes.limits.memory * 1000L * 1000L, true);

		CurrentDisk = ByteToGb(Resources.attributes.resources.disk_bytes);
		MaxDisk = "of " + ByteToGb(Info.attributes.limits.disk * 1000L * 1000L, true);
		
		RawMaxCpu = Info.attributes.limits.cpu;
		RawCurrentCpu = Resources.attributes.resources.cpu_absolute;
		
		RawMaxMemory = ByteToGbDouble(Info.attributes.limits.memory * 1000L * 1000L);
		RawCurrentMemory = ByteToGbDouble(Resources.attributes.resources.memory_bytes);
		
		RawMaxDisk = ByteToGbDouble(Info.attributes.limits.disk * 1000L * 1000L);
		RawCurrentDisk = ByteToGbDouble(Resources.attributes.resources.disk_bytes);
	}
	
	private string FormatGb(double gb, bool zeroIsInfinite = false) => zeroIsInfinite && gb == 0 ? "\u221e" : gb.ToString("F1") + " GB";
	private string ByteToGb(long bytes, bool zeroIsInfinite = false) => zeroIsInfinite && bytes == 0 ? "\u221e" : FormatGb(ByteToGbDouble(bytes));
	private double ByteToGbDouble(long bytes) => (double)(bytes / 1000L / 1000L) / 1000;
}