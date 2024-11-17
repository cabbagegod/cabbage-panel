using Newtonsoft.Json;

namespace CHPanel.Models.Dtos; 

public class Signal {
	[JsonIgnore]
	public SignalType ServerSignal { get; set; }
	
	public string signal => ServerSignal.ToString();
	
	// ReSharper disable InconsistentNaming
	public enum SignalType {
		start,
		stop,
		restart
	}
}