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

		public static IEnumerable<Bakhsh> GetBakhsh()
		{
			var result = new List<Bakhsh>();
			var file = Assembly.GetExecutingAssembly().GetManifestResourceText("Bakhsh.csv");
			var records = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in records.Skip(1))
			{
				var items = record.Split(',').Select(x => x.Trim()).ToArray();
				result.Add(new Bakhsh()
				{
					Id = items[0],
					Name = items[1],
					OstanId = items[2],
					ShahrestanId = items[3],
					AmarCode = items[4]
				});
			}
			return result;
		}

		public static IEnumerable<Dehestan> GetDehestan()
		{
			var result = new List<Dehestan>();
			var file = Assembly.GetExecutingAssembly().GetManifestResourceText("Dehestan.csv");
			var records = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in records.Skip(1))
			{
				var items = record.Split(',').Select(x => x.Trim()).ToArray();
				result.Add(new Dehestan()
				{
					Id = items[0],
					Name = items[1],
					OstanId = items[2],
					ShahrestanId = items[3],
					BakhshId = items[4],
					AmarCode = items[5]
				});
			}
			return result;
		}
		public static IEnumerable<Ostan> GetOstan()
		{
			var result = new List<Ostan>();
			var file = Assembly.GetExecutingAssembly().GetManifestResourceText("Ostan.csv");
			var records = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in records.Skip(1))
			{
				var items = record.Split(',').Select(x => x.Trim()).ToArray();
				result.Add(new Ostan()
				{
					Id = items[0],
					Name = items[1],
					AmarCode = items[2]
				});
			}
			return result;
		}

		public static IEnumerable<Shahr> GetShahr()
		{
			var result = new List<Shahr>();
			var file = Assembly.GetExecutingAssembly().GetManifestResourceText("Shahr.csv");
			var records = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in records.Skip(1))
			{
				var items = record.Split(',').Select(x => x.Trim()).ToArray();
				result.Add(new Shahr()
				{
					Id = items[0],
					Name = items[1],
					ShahrType = items[2],
					OstanId = items[3],
					ShahrestanId = items[4],
					BakhshId = items[5],
					AmarCode = items[6]
				});
			}
			return result;
		}

		public static IEnumerable<Shahrestan> GetShahrestan()
		{
			var result = new List<Shahrestan>();
			var file = Assembly.GetExecutingAssembly().GetManifestResourceText("Shahrestan.csv");
			var records = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var record in records.Skip(1))
			{
				var items = record.Split(',').Select(x => x.Trim()).ToArray();
				result.Add(new Shahrestan()
				{
					Id = items[0],
					Name = items[1],
					OstanId = items[3],
					AmarCode = items[4]
				});
			}
			return result;
		}
	}
}