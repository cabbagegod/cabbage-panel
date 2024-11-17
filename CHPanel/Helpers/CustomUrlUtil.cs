using System.IO;

namespace CHPanel.Helpers; 

public class CustomUrlUtil {
	public static string TryGetCustomUrl() {
		string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
		string? dir = Path.GetDirectoryName(path);
		if (dir == null)
			return string.Empty;
		
		string finalPath = Path.Combine(dir, "customUrl.panel");
		
		return File.Exists(finalPath) ? File.ReadAllText(finalPath) : string.Empty;
	}
}