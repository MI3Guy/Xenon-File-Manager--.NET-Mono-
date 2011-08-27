using System;
using System.IO;

namespace PluginUtil
{
	public class XeFileInfo
	{
		public delegate Stream FSIOFunc(XeFileInfo info);
		
		public XeFileInfo(FileInfo fi) {
			fullPath = fi.FullName;
			name = fi.Name;
			dateCreated = fi.CreationTime;
			dateAccessed = fi.LastAccessTime;
			dateModified = fi.LastWriteTime;
			size = fi.Length;
			isFile = true;
			icon = CommonUtil.GetIconForFile(this);
		}
		
		public XeFileInfo(DirectoryInfo di) {
			fullPath = di.FullName;
			name = di.Name;
			dateCreated = di.CreationTime;
			dateAccessed = di.LastAccessTime;
			dateModified = di.LastWriteTime;
			size = 0;
			isFile = false;
			icon = CommonUtil.GetIconForDirectory(this);
		}
		
		private string fullPath;
		private string name;
		private DateTime dateCreated;
		private DateTime dateAccessed;
		private DateTime dateModified;
		private long size;
		private FileAttributes attributes;
		private bool isFile;
		private object icon;
		
		public string FullPath {
			get { return fullPath; }
			set { fullPath = value; }
		}
		
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		public DateTime DateCreated {
			get { return dateCreated; }
			set { dateCreated = value; }
		}
		
		public DateTime DateAccessed {
			get { return dateAccessed; }
			set { dateAccessed = value; }
		}
		
		public DateTime DateModified {
			get { return dateModified; }
			set { dateModified = value; }
		}
		
		public long Size {
			get { return size; }
			set { size = value; }
		}
		
		public string FormattedSize {
			get { if(!IsFile) return string.Empty; return string.Format(new FileSizeFormatProvider(), "{0:fs}", size); }
		}
		
		public FileAttributes Attributes {
			get { return attributes; }
			set { attributes = value; }
		}
		
		public bool IsHidden {
			get {
				if(CommonUtil.OSType == PluginOSType.Windows) return (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
				return name[0] == '.';
			}
		}
		
		public bool IsFile {
			get { return isFile; }
			set { isFile = value; }
		}
		
		public object Icon {
			get { return icon; }
			set { icon = value; }
		}
		
		public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter {
		    public object GetFormat(Type formatType) {
		        if(formatType == typeof(ICustomFormatter)) return this;
		        return null;
		    }
			
		    private const string fileSizeFormat = "fs";
		    private const Decimal OneKiloByte = 1024M;
		    private const Decimal OneMegaByte = OneKiloByte * 1024M;
		    private const Decimal OneGigaByte = OneMegaByte * 1024M;
		
		    public string Format(string format, object arg, IFormatProvider formatProvider) {    
				if(format == null || !format.StartsWith(fileSizeFormat)) {
					return defaultFormat(format, arg, formatProvider);    
				}
				
				if(arg is string) {
					return defaultFormat(format, arg, formatProvider);    
				}
				
				Decimal size;
				
				try {
					size = Convert.ToDecimal(arg);    
				}    
				catch(InvalidCastException) {
					return defaultFormat(format, arg, formatProvider);    
				}
				
				string suffix;
				if(size > OneGigaByte) {
					size /= OneGigaByte;
					suffix = "GB";
				}
				else if(size > OneMegaByte) {
					size /= OneMegaByte;
					suffix = "MB";
				}
				else if(size > OneKiloByte) {
					size /= OneKiloByte;
					suffix = "kB";
				}
				else {
					suffix = " B";
				}
				
				string precision = format.Substring(2);
				if(String.IsNullOrEmpty(precision)) precision = "2";
				return String.Format("{0:N" + precision + "} {1}", size, suffix);
				
			}
			
			private static string defaultFormat(string format, object arg, IFormatProvider formatProvider) {
				IFormattable formattableArg = arg as IFormattable;
				if(formattableArg != null) {
					return formattableArg.ToString(format, formatProvider);
				}
				return arg.ToString();
			}
				
		}
	}
}

