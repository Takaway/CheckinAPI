using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

namespace CheckinAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HController : ControllerBase
	{
		private static List<string> list = new List<string>();
		public HController()
		{
			fullPath(ref list, "C:\\Users\\ajisaii\\OneDrive\\插画\\share");
		}
		[HttpGet]
		public async Task<ActionResult> GetH()
		{
			MemoryStream stream;
			using (FileStream fileStream = new FileStream(list[new Random().Next(list.Count)], FileMode.Open))
			{
				int len = (int)fileStream.Length;
				byte[] buffer = new byte[len];
				fileStream.Read(buffer, 0, len);
				stream = new MemoryStream();
				stream.Write(buffer, 0, len);
			}
			return await Task.FromResult<FileResult>(File(stream.ToArray(), "image/jpeg"));
		}
		private void fullPath(ref List<string> list, string dirs)
		{
			DirectoryInfo dir = new DirectoryInfo(dirs);
			FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
			foreach (FileSystemInfo fsi in fsinfos)
				if (fsi is DirectoryInfo)
					fullPath(ref list, fsi.FullName);
				else
					list.Add(fsi.FullName);
		}
		[HttpPost]
		public async Task<IActionResult> PostH(Models.H h)
		{
			try
			{
				var httpCilent = new HttpClient();
				var src = new MemoryStream(await httpCilent.GetByteArrayAsync(h.pic_url));
				var image = Image.FromStream(src);
				Func<Image, string> GetName = (Image image) =>
				{
					if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
						return "jpeg";
					else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
						return "png";
					else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
						return "gif";
					else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
						return "bmp";
					else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
						return "tiff";
					else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Webp))
						return "webp";
					else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
						return "icon";
					return "";
				};
				string name = Path.GetRandomFileName();
				if (GetName(image) == "")
					return NotFound("Can't analyse type of the picture.");
				else name += $" from{h.post_name}." + GetName(image);
				using (FileStream fileStream = new FileStream($"C:\\Users\\ajisaii\\OneDrive\\插画\\share_buffer\\{name}", FileMode.Create))
				{
					image.Save(fileStream, image.RawFormat);
				}
			}
			catch (HttpRequestException ex)
			{
				if (ex.StatusCode == HttpStatusCode.Forbidden)
					return Forbid();
				else if (ex.StatusCode == HttpStatusCode.NotFound)
					return NotFound();
			}
			return NoContent();
		}
	}
}
