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
	
	public ServerInfo Allocation { get; private set; } = null!;
	public ServerInfo Info { get; private set; } = null!;
	public ServerResources Resources { get; private set; } = null!;

	public string Name { get; set; } = string.Empty;
	public string Ip { get; set; } = string.Empty;
	public bool IsOnline { get; set; }
	public Brush StatusColor => IsOnline ? Brushes.Green : Brushes.Red;

	public string MaxCpu { get; set; } = string.Empty;
	public string CurrentCpu { get; set; } = string.Empty;
	
	public string MaxMemory { get; set; } = string.Empty;
	public string CurrentMemory { get; set; } = string.Empty;
	
	public string MaxDisk { get; set; } = string.Empty;
	public string CurrentDisk { get; set; } = string.Empty;

	public void Build() {
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
	}
	
	private string ByteToGb(long bytes, bool zeroIsInfinite = false) => zeroIsInfinite && bytes == 0 ? "\u221e" : ((float)(bytes / 1000L / 1000L) / 1000).ToString("F1") + " GB";
}