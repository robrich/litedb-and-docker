using System;

namespace Site.Models
{
	public class PageView
	{
		public int Id { get; set; }
		public DateTime ViewDate { get; set; }
		public string Server { get; set; }
		public string Url { get; set; }
	}
}
