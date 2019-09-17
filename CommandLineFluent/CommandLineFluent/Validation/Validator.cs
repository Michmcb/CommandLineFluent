using System.IO;

namespace CommandLineFluent.Validation
{
	/// <summary>
	/// Has some commonly-used validators. So for example, you can do this:
	/// verb.WithValidator(Validators.FileExists)
	/// </summary>
	public static class Validators
	{
		/// <summary>
		/// Makes sure the provided path is a file that exists.
		/// Equivalent to returning null on File.Exists(path) == true
		/// </summary>
		/// <param name="path">The path to check</param>
		public static string FileExists(string path)
		{
			return File.Exists(path) ? null : $@"The file ""{path}"" does not exist";
		}
		/// <summary>
		/// Makes sure the provided path is a directory that exists.
		/// Equivalent to returning null on Directory.Exists(path) == true
		/// </summary>
		/// <param name="path">The path to check</param>
		public static string DirectoryExists(string path)
		{
			return Directory.Exists(path) ? null : $@"The directory ""{path}"" does not exist";
		}
	}
}
