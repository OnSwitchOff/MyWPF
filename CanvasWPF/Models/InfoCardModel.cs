using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CanvasWPF.Models
{
	public class InfoCardModel
	{
		public string title;
		public string imagePath;
		public Dictionary<string,string> mainFields;
		public Dictionary<string,string> additionalProperties;

		public	InfoCardModel()
		{
			mainFields = new Dictionary<string, string>();
			additionalProperties = new Dictionary<string, string>();
		}
	}
}
