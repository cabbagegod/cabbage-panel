using System.Windows.Media;

namespace CHPanel.Models.Dtos;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
public class ServerList {
	public string @object { get; set; } = string.Empty;
	public List<ServerInfo> data { get; set; } = new();
	public Meta? meta { get; set; }

	public class Meta {
		public Pagination? pagination { get; set; }
		
		public class Pagination {
			public int total { get; set; }
			public int count { get; set; }
			public int per_page { get; set; }
			public int current_page { get; set; }
			public int total_pages { get; set; }
			public Links? links { get; set; }
			
			public class Links { }
		}
	}
}

public class ServerInfo {
	public string @object { get; set; } = string.Empty;
	public Attributes? attributes { get; set; }

	public Brush statusColor => attributes?.is_suspended == true ? Brushes.Red : Brushes.Green; 
	
	public class Allocations {
		public string @object { get; set; } = string.Empty;
		public List<ServerInfo> data { get; set; } = new();
	}

	public class Attributes {
		public bool server_owner { get; set; }
		public string identifier { get; set; } = string.Empty;
		public string uuid { get; set; } = string.Empty;
		public string name { get; set; } = string.Empty;
		public string node { get; set; } = string.Empty;
		public SftpDetails? sftp_details { get; set; }
		public string description { get; set; } = string.Empty;
		public Limits? limits { get; set; }
		public FeatureLimits? feature_limits { get; set; }
		public bool is_suspended { get; set; }
		public bool is_installing { get; set; }
		public Relationships? relationships { get; set; }
		public int id { get; set; }
		public string ip { get; set; } = string.Empty;
		public object? ip_alias { get; set; }
		public int port { get; set; }
		public string notes { get; set; } = string.Empty;
		public bool is_default { get; set; }
	}

	public class FeatureLimits {
		public int databases { get; set; }
		public int allocations { get; set; }
		public int backups { get; set; }
	}

	public class Limits {
		public int memory { get; set; }
		public int swap { get; set; }
		public int disk { get; set; }
		public int io { get; set; }
		public int cpu { get; set; }
	}

	public class Relationships {
		public Allocations? allocations { get; set; }
	}

	public class SftpDetails {
		public string ip { get; set; } = string.Empty;
		public int port { get; set; }
	}
}