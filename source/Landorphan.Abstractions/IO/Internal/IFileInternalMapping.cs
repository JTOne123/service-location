﻿namespace Landorphan.Abstractions.IO.Internal
{
   using System;
   using System.Collections.Generic;
   using System.Collections.Immutable;
   using System.Diagnostics.CodeAnalysis;
   using System.IO;
   using System.Security;
   using System.Text;
   using Landorphan.Abstractions.IO.Interfaces;

   /// <summary>
   /// Represents the internal mapping from the static BCL <see cref="File"/> class to an interface.
   /// </summary>
   /// <remarks>
   /// No <see cref="FileStream"/> methods have been mapped. 
   /// </remarks>
   internal interface IFileInternalMapping
   {
      /// <summary>
      /// Gets the maximum file time as a <see cref="DateTimeOffset"/>.
      /// </summary>
      /// <value>
      /// The maximum file time as a <see cref="DateTimeOffset"/>.
      /// </value>
      DateTimeOffset MaximumFileTimeAsDateTimeOffset { get; }

      /// <summary>
      /// Gets the minimum file time as a <see cref="DateTimeOffset"/>.
      /// </summary>
      /// <value>
      /// The minimum file time as a <see cref="DateTimeOffset"/>.
      /// </value>
      DateTimeOffset MinimumFileTimeAsDateTimeOffset { get; }

      /// <summary>
      /// Appends lines to a file, and then closes the file.  If the specified path root exits, this method will create intermediate directories and 
      /// the file itself as needed.  It then appends the specified lines to the file, and closes the file.
      /// </summary>
      /// <param name="path">
      /// The file to append the lines to. The file is created if it does not already exist.
      /// </param>
      /// <param name="contents">
      /// The lines to append to the file (<c>null</c> line items are coalesced to <see cref="string.Empty"/>).
      /// </param>
      /// <param name="encoding">
      /// The character encoding to use.
      /// </param>
      /// <exception cref="ArgumentNullException">
      /// <paramref name=" path "/> is <c>null</c>
      /// -or- 
      /// <paramref name="contents"/> is <c>null</c>
      /// -or- 
      /// <paramref name="encoding"/> is <c>null</c>.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string,
      /// -or-
      /// contains only white space,
      /// -or-
      /// contains one more invalid characters defined by the <see cref="IPathUtilities.GetInvalidPathCharacters"/> method,
      /// -or-
      /// contains one more invalid characters defined by the <see cref="IPathUtilities.GetInvalidFileNameCharacters"/> method.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// When an I/O error occurs while opening the file (for example, the path identifies an existing directory).
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// <paramref name="path"/> exceeds the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have permission to write to the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specifies a file that is read-only.
      /// -or-
      /// specifies directory path for which the caller does not have write permissions.
      /// -or-
      /// This operation is not supported on the current platform.
      /// </exception>
      // TODO: change the implementation to throw ArgumentException instead of NotSupportedException.
      // NOTE: have yet to see this exception
      void AppendAllLines(String path, IEnumerable<String> contents, Encoding encoding);

      /// <summary>
      /// Appends the specified string to the file, creating the file if it does not already exist.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file specified in <paramref name="path"/> was not found.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to append the specified string to.
      /// </param>
      /// <param name="contents">
      /// The string to append to the file.
      /// </param>
      /// <param name="encoding">
      /// The character encoding to use.
      /// </param>
      void AppendAllText(String path, String contents, Encoding encoding);

      /// <summary>
      /// Copies an existing file to a new file. Overwriting a file of the same name is not allowed.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is a zero-length string,
      /// contains only white space, or contains one or more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// -or-
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> specifies a directory.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The path specified in <paramref name="sourceFileName"/> or
      /// <paramref name="destFileName"/> is invalid (for example, it is on an unmapped
      /// drive).
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// <paramref name="sourceFileName"/> was not found.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="destFileName"/> exists.-or- An I/O error has occurred.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is in an invalid
      /// format.
      /// </exception>
      /// <param name="sourceFileName">
      /// The file to copy.
      /// </param>
      /// <param name="destFileName">
      /// The name of the destination file. This cannot be a directory or an existing file.
      /// </param>
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dest")]
      void CopyNoOverwrite(String sourceFileName, String destFileName);

      /// <summary>
      /// Copies an existing file to a new file. Overwriting a file of the same name is allowed.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission. -or-<paramref name="destFileName"/>
      /// is read-only.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is a zero-length string,
      /// contains only white space, or contains one or more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// -or-
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> specifies a directory.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The path specified in <paramref name="sourceFileName"/> or
      /// <paramref name="destFileName"/> is invalid (for example, it is on an unmapped
      /// drive).
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// <paramref name="sourceFileName"/> was not found.
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurs.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is in an invalid
      /// format.
      /// </exception>
      /// <param name="sourceFileName">
      /// The file to copy.
      /// </param>
      /// <param name="destFileName">
      /// The name of the destination file. This cannot be a directory.
      /// </param>
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dest")]
      void CopyWithOverwrite(String sourceFileName, String destFileName);

      /// <summary>
      /// Creates or overwrites a file in the specified path as a zero-byte file, and then closes the file.  If the specified path root exists, 
      /// this method will create intermediate directories, as well as the file itself as needed.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// -or-
      /// <paramref name="path"/> specified a file that is read-only.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while creating the file.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <param name="path">
      /// The path and name of the file to create.
      /// </param>
      /// <returns>
      /// The full path of the temporary file.
      /// </returns>
      String CreateFile(String path);

      /// <summary>
      /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
      /// </summary>
      /// <exception cref="IOException">
      /// An I/O error occurs, such as no unique temporary file name is available.
      /// - or -
      /// This method was unable to create a temporary file.
      /// </exception>
      /// <returns>
      /// The full path of the temporary file.
      /// </returns>
      String CreateTemporaryFile();

      /// <summary>
      /// Deletes the specified file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// The specified file is in use. -or-There is an open handle on the file, and the operating system is
      /// Windows XP or earlier. This open handle can result from enumerating directories and files. For more
      /// information, see How to: Enumerate Directories and Files.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.-or- <paramref name="path"/> is a
      /// directory.
      /// -or-
      /// <paramref name="path"/> specified a read-only file.
      /// </exception>
      /// <param name="path">
      /// The name of the file to be deleted. Wild-card characters are not supported.
      /// </param>
      void DeleteFile(String path);

      /// <summary>
      /// Attempts to determine whether the specified file exists.
      /// </summary>
      /// <param name="path">
      /// The file to check.
      /// </param>
      /// <returns>
      /// <c>true</c> if the caller has the required permissions and <paramref name="path"/> contains the name of an existing file; otherwise,
      /// <c>false</c>.  
      /// <p>This method also returns <c>false</c> when <paramref name="path"/> is null, an invalid path, or a zero-length string.</p>
      /// <p>This method will also return <c>false</c> when the caller does not have sufficient permissions to read the specified file.</p>
      /// </returns>
      Boolean FileExists(String path);

      /// <summary>
      /// Gets the <see cref="FileAttributes"/> of the file on the path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is empty, contains only white spaces, or contains invalid characters.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// <paramref name="path"/> represents a file and is invalid, such as being on an unmapped
      /// drive, or the file cannot be found.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> represents a directory and is invalid, such as being on an
      /// unmapped drive, or the directory cannot be found.
      /// </exception>
      /// <exception cref="IOException">
      /// This file is being used by another process.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The path to the file.
      /// </param>
      /// <returns>
      /// The <see cref="FileAttributes"/> of the file on the path.
      /// </returns>
      FileAttributes GetAttributes(String path);

      /// <summary>
      /// Returns the creation date and time of the specified file or directory.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <param name="path">
      /// The file or directory for which to obtain creation date and time information.
      /// </param>
      /// <returns>
      /// A <see cref="DateTimeOffset"/> structure set to the creation date and time for the specified file or directory.
      /// </returns>
      DateTimeOffset GetCreationTime(String path);

      /// <summary>
      /// Returns the date and time the specified file or directory was last accessed.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <param name="path">
      /// The file or directory for which to obtain access date and time information.
      /// </param>
      /// <returns>
      /// A <see cref="DateTimeOffset"/> structure set to the date and time that the specified file or directory was last accessed.
      /// </returns>
      DateTimeOffset GetLastAccessTime(String path);

      /// <summary>
      /// Returns the date and time the specified file or directory was last written to.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <param name="path">
      /// The file or directory for which to obtain write date and time information.
      /// </param>
      /// <returns>
      /// A <see cref="DateTimeOffset"/> structure set to the date and time that the specified file or directory was last written to.
      /// </returns>
      DateTimeOffset GetLastWriteTime(String path);

      /// <summary>
      /// Generates a a cryptographically strong, random string that can be used as a file name.
      /// </summary>
      /// <returns>
      /// A random file name.
      /// </returns>
      /// <remarks>
      /// Does not create a file.
      /// </remarks>
      [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
      String GetRandomFileName();

      /// <summary>
      /// Moves a specified file to a new location, providing the option to specify a new file name.
      /// </summary>
      /// <exception cref="IOException">
      /// The destination file already exists.-or-<paramref name="sourceFileName"/> was not found.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is null.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is a zero-length string,
      /// contains only white space, or contains invalid characters as defined in
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The path specified in <paramref name="sourceFileName"/> or
      /// <paramref name="destFileName"/> is invalid, (for example, it is on an unmapped
      /// drive).
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="sourceFileName"/> or <paramref name="destFileName"/> is in an invalid
      /// format.
      /// </exception>
      /// <param name="sourceFileName">
      /// The name of the file to move.
      /// </param>
      /// <param name="destFileName">
      /// The new path for the file.
      /// </param>
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dest")]
      void Move(String sourceFileName, String destFileName);

      /// <summary>
      /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// This operation is not supported on the current platform.-or- <paramref name="path"/>
      /// specified a directory.-or- The caller does not have the required permission.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file specified in <paramref name="path"/> was not found.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to open for reading.
      /// </param>
      /// <returns>
      /// A list of byte containing the contents of the file.
      /// </returns>
      IImmutableList<Byte> ReadAllBytes(String path);

      /// <summary>
      /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.-or- The caller does not have the
      /// required permission.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file specified in <paramref name="path"/> was not found.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to open for reading.
      /// </param>
      /// <param name="encoding">
      /// The encoding applied to the contents of the file.
      /// </param>
      /// <returns>
      /// A list containing all lines of the file.
      /// </returns>
      IImmutableList<String> ReadAllLines(String path, Encoding encoding);

      /// <summary>
      /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.-or- The caller does not have the
      /// required permission.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file specified in <paramref name="path"/> was not found.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to open for reading.
      /// </param>
      /// <param name="encoding">
      /// The encoding applied to the contents of the file.
      /// </param>
      /// <returns>
      /// A string containing all lines of the file.
      /// </returns>
      String ReadAllText(String path, Encoding encoding);

      /// <summary>
      /// Replaces the contents of a file with the contents from another file, deleting the original file.
      /// </summary>
      /// <param name="sourceFileName">
      /// The name of a file that holds the replacement contents, and will be deleted.
      /// </param>
      /// <param name="destinationFileName">
      /// The name of the file whose contents will be replaced.
      /// </param>
      /// <exception cref="ArgumentNullException">
      /// The <paramref name="sourceFileName"/> parameter is null.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter is null.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// The path described by the <paramref name="sourceFileName"/> parameter was not of a legal form.
      /// -or-
      /// The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file described by the <paramref name="sourceFileName"/> parameter could not be found.
      /// -or-
      /// The file described by the <paramref name="destinationFileName"/> parameter could not be found.
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// -or-
      /// The <paramref name="sourceFileName"/> and <paramref name="destinationFileName"/> parameters specify the same file.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The operating system is Windows 98 Second Edition or earlier.
      /// -or-
      /// the file system is not NTFS.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The <paramref name="sourceFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// -or-
      /// This operation is not supported on the current platform.
      /// -or- 
      /// The caller does not have the required permission.
      /// </exception>
      void ReplaceContentsNoBackup(String sourceFileName, String destinationFileName);

      /// <summary>
      /// Replaces the contents of a file with the contents from another file, deleting the original file and ignoring metadata errors.
      /// </summary>
      /// <param name="sourceFileName">
      /// The name of a file that holds the replacement contents, and will be deleted.
      /// </param>
      /// <param name="destinationFileName">
      /// The name of the file whose contents will be replaced.
      /// </param>
      /// <exception cref="ArgumentNullException">
      /// The <paramref name="sourceFileName"/> parameter is null.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter is null.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// The path described by the <paramref name="sourceFileName"/> parameter was not of a legal form.
      /// -or-
      /// The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file described by the <paramref name="sourceFileName"/> parameter could not be found.
      /// -or-
      /// The file described by the <paramref name="destinationFileName"/> parameter could not be found.
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// -or-
      /// The <paramref name="sourceFileName"/> and <paramref name="destinationFileName"/> parameters specify the same file.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The operating system is Windows 98 Second Edition or earlier.
      /// -or-
      /// the file system is not NTFS.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The <paramref name="sourceFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// -or-
      /// This operation is not supported on the current platform.
      /// -or- 
      /// The caller does not have the required permission.
      /// </exception>
      void ReplaceContentsNoBackupIgnoringMetadataErrors(String sourceFileName, String destinationFileName);

      /// <summary>
      /// Replaces the contents of a file with the contents from another file, deleting the original file, and backing up the original contents of the replaced file
      /// </summary>
      /// <param name="sourceFileName">
      /// The name of a file that holds the replacement contents, and will be deleted.
      /// </param>
      /// <param name="destinationFileName">
      /// The name of the file whose contents will be replaced.
      /// </param>
      /// <param name="destinationBackupFileName">
      /// The name of the destination backup file.
      /// </param>
      /// <exception cref="ArgumentNullException">
      /// The <paramref name="sourceFileName"/> parameter is null.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter is null.
      /// -or-
      /// The <paramref name="destinationBackupFileName"/> parameter is null.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// The path described by the <paramref name="sourceFileName"/> parameter was not of a legal form.
      /// -or-
      /// The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.
      /// -or-
      /// The path described by the <paramref name="destinationBackupFileName"/> parameter was not of a legal form.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file described by the <paramref name="sourceFileName"/> parameter could not be found.
      /// -or-
      /// The file described by the <paramref name="destinationFileName"/> parameter could not be found.
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationBackupFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="sourceFileName"/> and <paramref name="destinationFileName"/> parameters specify the same file.
      /// -or-
      /// The <paramref name="sourceFileName"/> and <paramref name="destinationBackupFileName"/> parameters specify the same file.
      /// -or-
      /// The <paramref name="destinationFileName"/> and <paramref name="destinationBackupFileName"/> parameters specify the same file.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The operating system is Windows 98 Second Edition or earlier.
      /// -or-
      /// the file system is not NTFS.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The <paramref name="sourceFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationBackupFileName"/> parameter specifies a file that is read-only.
      /// -or- 
      /// This operation is not supported on the current platform.
      /// -or- 
      /// The caller does not have the required permission.
      /// </exception>
      void ReplaceContentsWithBackup(String sourceFileName, String destinationFileName, String destinationBackupFileName);

      /// <summary>
      /// Replaces the contents of a file with the contents from another file, deleting the original file, and backing up the original contents of the replaced file and ignoring metadata errors.
      /// </summary>
      /// <param name="sourceFileName">
      /// The name of a file that holds the replacement contents, and will be deleted.
      /// </param>
      /// <param name="destinationFileName">
      /// The name of the file whose contents will be replaced.
      /// </param>
      /// <param name="destinationBackupFileName">
      /// The name of the destination backup file.
      /// </param>
      /// <exception cref="ArgumentNullException">
      /// The <paramref name="sourceFileName"/> parameter is null.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter is null.
      /// -or-
      /// The <paramref name="destinationBackupFileName"/> parameter is null.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// The path described by the <paramref name="sourceFileName"/> parameter was not of a legal form.
      /// -or-
      /// The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.
      /// -or-
      /// The path described by the <paramref name="destinationBackupFileName"/> parameter was not of a legal form.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file described by the <paramref name="sourceFileName"/> parameter could not be found.
      /// -or-
      /// The file described by the <paramref name="destinationFileName"/> parameter could not be found.
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationBackupFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="sourceFileName"/> and <paramref name="destinationFileName"/> parameters specify the same file.
      /// -or-
      /// The <paramref name="sourceFileName"/> and <paramref name="destinationBackupFileName"/> parameters specify the same file.
      /// -or-
      /// The <paramref name="destinationFileName"/> and <paramref name="destinationBackupFileName"/> parameters specify the same file.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The operating system is Windows 98 Second Edition or earlier.
      /// -or-
      /// the file system is not NTFS.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The <paramref name="sourceFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="sourceFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a file that is read-only.
      /// -or-
      /// The <paramref name="destinationFileName"/> parameter specifies a directory.
      /// -or-
      /// The <paramref name="destinationBackupFileName"/> parameter specifies a file that is read-only.
      /// -or- 
      /// This operation is not supported on the current platform.
      /// -or- 
      /// The caller does not have the required permission.
      /// </exception>
      void ReplaceContentsWithBackupIgnoringMetadataErrors(String sourceFileName, String destinationFileName, String destinationBackupFileName);

      /// <summary>
      /// Sets the specified <see cref="FileAttributes"/> of the file on the specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is empty, contains only white spaces, contains invalid characters, or
      /// the file attribute is invalid.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid, (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The file cannot be found.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.-or- The caller does not have the
      /// required permission.
      /// </exception>
      /// <param name="path">
      /// The path to the file.
      /// </param>
      /// <param name="fileAttributes">
      /// A bitwise combination of the enumeration values.
      /// </param>
      void SetAttributes(String path, FileAttributes fileAttributes);

      /// <summary>
      /// Sets the date and time the file was created.
      /// </summary>
      /// <exception cref="FileNotFoundException">
      /// The specified path was not found.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while performing the operation.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="creationTime"/> specifies a value outside the range of dates, times,
      /// or both permitted for this operation.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <param name="path">
      /// The file for which to set the creation date and time information.
      /// </param>
      /// <param name="creationTime">
      /// A <see cref="DateTimeOffset"/> containing the value to set for the creation date and time of
      /// <paramref name="path"/>.
      /// </param>
      void SetCreationTime(String path, DateTimeOffset creationTime);

      /// <summary>
      /// Sets the date and time the specified file was last accessed.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The specified path was not found.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="lastAccessTime"/> specifies a value outside the range of dates or
      /// times permitted for this operation.
      /// </exception>
      /// <param name="path">
      /// The file for which to set the access date and time information.
      /// </param>
      /// <param name="lastAccessTime">
      /// A <see cref="DateTimeOffset"/> containing the value to set for the last access date and time of
      /// <paramref name="path"/>. 
      /// </param>
      void SetLastAccessTime(String path, DateTimeOffset lastAccessTime);

      /// <summary>
      /// Sets the date and time that the specified file was last written to.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The specified path was not found.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="lastWriteTime"/> specifies a value outside the range of dates or times
      /// permitted for this operation.
      /// </exception>
      /// <param name="path">
      /// The file for which to set the date and time information.
      /// </param>
      /// <param name="lastWriteTime">
      /// A <see cref="DateTimeOffset"/> containing the value to set for the last write date and time of
      /// <paramref name="path"/>. 
      /// </param>
      void SetLastWriteTime(String path, DateTimeOffset lastWriteTime);

      /// <summary>
      /// Creates or overwrites the contents of the specified file, writing the bytes, and closing the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string.
      /// -or-
      /// <paramref name="path"/> contains only white space
      /// -or-
      /// <paramref name="path"/> contains one or more invalid characters as defined by <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null 
      /// -or-
      /// <paramref name="bytes"/> is null 
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.
      /// -or- 
      /// This operation is not supported on the current platform.
      /// -or-
      /// <paramref name="path"/> specified a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to write to.
      /// </param>
      /// <param name="bytes">
      /// The bytes to write to the file.
      /// </param>
      [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bytes")]
      void WriteAllBytes(String path, Byte[] bytes);

      /// <summary>
      /// Creates or overwrites the contents of the specified file, writing the bytes, and closing the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string.
      /// -or-
      /// <paramref name="path"/> contains only white space
      /// -or-
      /// <paramref name="path"/> contains one or more invalid characters as defined by <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null 
      /// -or-
      /// <paramref name="bytes"/> is null 
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.
      /// -or- 
      /// This operation is not supported on the current platform.
      /// -or-
      /// <paramref name="path"/> specified a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to write to.
      /// </param>
      /// <param name="bytes">
      /// The bytes to write to the file.
      /// </param>
      [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bytes")]
      void WriteAllBytes(String path, IImmutableList<Byte> bytes);

      /// <summary>
      /// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// Either <paramref name="path"/> or <paramref name="contents"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to write to.
      /// </param>
      /// <param name="contents">
      /// The string array to write to the file.
      /// </param>
      /// <param name="encoding">
      /// An <see cref="Encoding"/> object that represents the character encoding applied to the string array.
      /// </param>
      void WriteAllLines(String path, String[] contents, Encoding encoding);

      /// <summary>
      /// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// Either <paramref name="path"/> or <paramref name="contents"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to write to.
      /// </param>
      /// <param name="contents">
      /// The string array to write to the file.
      /// </param>
      /// <param name="encoding">
      /// An <see cref="Encoding"/> object that represents the character encoding applied to the string array.
      /// </param>
      void WriteAllLines(String path, IImmutableList<String> contents, Encoding encoding);

      /// <summary>
      /// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters defined by the
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/> method.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// Either<paramref name=" path"/>,<paramref name=" contents"/>, or
      /// <paramref name="encoding"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// <paramref name="path"/> exceeds the system-defined maximum length.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specifies a file that is read-only.-or-This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> is a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to write to.
      /// </param>
      /// <param name="contents">
      /// The lines to write to the file.
      /// </param>
      /// <param name="encoding">
      /// The character encoding to use.
      /// </param>
      void WriteAllLines(String path, IEnumerable<String> contents, Encoding encoding);

      /// <summary>
      /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target
      /// file already exists, it is overwritten.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null or <paramref name="contents"/> is empty.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <exception cref="IOException">
      /// An I/O error occurred while opening the file.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// <paramref name="path"/> specified a file that is read-only.-or- This operation is not
      /// supported on the current platform.-or-
      /// <paramref name="path"/> specified a directory.
      /// -or-
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> is in an invalid format.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The file to write to.
      /// </param>
      /// <param name="contents">
      /// The string to write to the file.
      /// </param>
      /// <param name="encoding">
      /// The encoding to apply to the string.
      /// </param>
      void WriteAllText(String path, String contents, Encoding encoding);
   }
}
