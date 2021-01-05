using Cult.PersianDataset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cult.PersianDataset
{
	public static class PersianDataKit
	{
		public static IEnumerable<string> GetBoyNames()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceText("Boys.csv").Split(',');
		}
		public static IEnumerable<string> GetGirlNames()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceText("Girls.csv").Split(',');
		}
		public static IEnumerable<string> GetFamilyNames()
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceText("Family.csv").Split(',');
		}

		public static IEnumerable<Abadi> GetAbadi()
		{
			var result = new List<Abadi>();
			var file = Assembly.GetExecutingAssembly().GetManifestResourceText("Abadi.csv");
			var records = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in records.Skip(1))
			{
				var items = record.Split(',').Select(x => x.Trim()).ToArray();
				result.Add(new Abadi()
				{
					Id = items[0],
					Name = items[1],
					AbadiType = items[2],
					Diag = items[3],
					OstanId = items[4],
					ShahrestanId = items[5],
					BakhshId = items[6],
					DehestanId = items[7],
					AmarCode = items[8]
				});
			}
			return result;
		}
	}


}