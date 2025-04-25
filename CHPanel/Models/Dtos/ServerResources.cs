namespace CHPanel.Models.Dtos;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

public class ServerResources {
	public string @object { get; set; } = string.Empty;
	public Attributes? attributes { get; set; }

	public class Attributes {
		public string current_state { get; set; } = string.Empty;
		public bool is_suspended { get; set; }
		public Resources? resources { get; set; }
	}

	public class Resources {
		public long memory_bytes { get; set; }
		public double cpu_absolute { get; set; }
		public long disk_bytes { get; set; }
		public long network_rx_bytes { get; set; }
		public long network_tx_bytes { get; set; }
		public long uptime { get; set; }
	}
}