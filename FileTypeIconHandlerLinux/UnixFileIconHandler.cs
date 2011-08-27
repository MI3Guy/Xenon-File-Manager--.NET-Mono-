using System;

// http://standards.freedesktop.org/shared-mime-info-spec/shared-mime-info-spec-latest.html#s2_layout
namespace FileTypeIconHandlerLinux
{
	public class UnixFileIconHandler
	{
		public static readonly string[] Directories = new string[] {
			"/usr/share/mime/",
			"",
			""
		};
		public UnixFileIconHandler ()
		{
		}
	}
}

