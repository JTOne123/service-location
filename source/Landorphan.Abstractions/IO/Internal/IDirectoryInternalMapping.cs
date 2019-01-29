﻿namespace Landorphan.Abstractions.IO.Internal
{
   using System;
   using System.Collections.Immutable;
   using System.Diagnostics.CodeAnalysis;
   using System.IO;
   using System.Security;
   using Landorphan.Abstractions.IO.Interfaces;

   /// <summary>
   /// Represents the internal mapping from the static BCL <see cref="Directory"/> class to an interface.
   /// </summary>
   internal interface IDirectoryInternalMapping
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
      /// Copies a directory and its contents to a new location.
      /// </summary>
      /// <param name="sourceDirName">
      /// The path of directory to copy.
      /// </param>
      /// <param name="destDirName">
      /// The path to the destination directory.
      /// </param>
      /// <exception cref="IOException">
      /// <paramref name="destDirName"/> already exists. 
      /// -or-
      /// The <paramref name="sourceDirName"/> and <paramref name="destDirName"/> parameters refer to the same file or directory.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permissions.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="sourceDirName"/> 
      /// -or-
      /// <paramref name="destDirName"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="sourceDirName"/> or <paramref name="destDirName"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The path specified by <paramref name="sourceDirName"/> is invalid (for example, it on an unmapped drive).
      /// </exception>
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dir")]
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dest")]
      void Copy(String sourceDirName, String destDirName);

      /// <summary>
      /// Creates all directories and subdirectories in the specified path.
      /// </summary>
      /// <exception cref="IOException">
      /// The directory specified by <paramref name="path"/> is a file.
      /// -or-
      /// The network name is not known.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or
      /// more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// -or-
      /// <paramref name="path"/> is prefixed with, or contains only a colon character (:).
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
      /// <exception cref="NotSupportedException">
      /// <paramref name="path"/> contains a colon character (:) that is not part of a drive label (e.g., "C:\").
      /// </exception>
      /// <param name="path">
      /// The directory path to create.
      /// </param>
      /// <returns>
      /// An object that represents the directory for the specified path.
      /// </returns>
      String CreateDirectory(String path);

      /// <summary>
      /// Deletes an empty directory from a specified path.
      /// </summary>
      /// <exception cref="IOException">
      /// A file with the same name and location specified by <paramref name="path"/> exists.
      /// -or-
      /// The directory is the application's current working directory.
      /// -or-
      /// The directory specified by <paramref name="path"/> is not empty.
      /// -or-
      /// The directory is read-only or contains a read-only file.
      /// -or-
      /// The directory is being used by another process.
      /// -or-
      /// There is an open handle on the directory, and
      /// the operating system is Windows XP or earlier. This open handle can result from directories. For
      /// more information, see How to: Enumerate Directories and Files.
      /// </exception>
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
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> does not exist or could not be found.
      /// -or-
      /// <paramref name="path"/> refers to a file instead of a directory.
      /// -or-
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <param name="path">
      /// The name of the empty directory to remove. This directory must be writable or empty.
      /// </param>
      void DeleteEmpty(String path);

      /// <summary>
      /// Deletes the specified directory and any subdirectories and files in the directory.
      /// </summary>
      /// <exception cref="IOException">
      /// A file with the same name and location specified by <paramref name="path"/> exists.
      /// -or-
      /// The directory specified by <paramref name="path"/> is read-only.
      /// -or-
      /// The directory is the application's current working directory. 
      /// -or-
      /// The directory contains a read-only file.
      /// -or-
      /// The directory is being used by another process.There is an open handle on the directory or on one of its files, and the operating
      /// system is Windows XP or earlier. This open handle can result from enumerating directories and
      /// files. For more information, see How to: Enumerate Directories and Files.
      /// </exception>
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
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> does not exist or could not be found.
      /// -or-
      /// <paramref name="path"/> refers to a file instead of a directory.
      /// -or-
      /// The specified path is invalid (for example, it is on an unmapped drive).
      /// </exception>
      /// <param name="path">
      /// The name of the directory to remove.
      /// </param>
      void DeleteRecursively(String path);

      /// <summary>
      /// Determines whether the given path refers to an existing directory on disk.
      /// </summary>
      /// <param name="path">
      /// The path to test.
      /// </param>
      /// <returns>
      /// true if <paramref name="path"/> refers to an existing directory; otherwise, false.
      /// </returns>
      Boolean DirectoryExists(String path);

      /// <summary>
      /// Returns an enumerable collection of directory names in a specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by <see cref="Path.GetInvalidPathChars"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <returns>
      /// An enumerable collection of the full names (including paths) for the directories in the directory specified by
      /// <paramref name="path"/>.
      /// </returns>
      IImmutableSet<String> EnumerateDirectories(String path);

      /// <summary>
      /// Returns an enumerable collection of directory names that match a search pattern in a specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by <see cref="Path.GetInvalidPathChars"/>.
      /// - or -
      /// <paramref name="searchPattern"/> does not contain a valid pattern.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      ///  -or-
      /// <paramref name="searchPattern"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <param name="searchPattern">
      /// The search string to match against the names of directories in <paramref name="path"/>.
      /// </param>
      /// <returns>
      /// An enumerable collection of the full names (including paths) for the directories in the directory specified by
      /// <paramref name="path"/> and that match the specified search pattern.
      /// </returns>
      IImmutableSet<String> EnumerateDirectories(String path, String searchPattern);

      /// <summary>
      /// Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches
      /// subdirectories.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// - or -
      /// <paramref name="searchPattern"/> does not contain a valid pattern.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.-or-<paramref name="searchPattern"/> is null.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="searchOption"/> is not a valid <see cref="SearchOption"/>
      /// value.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <param name="searchPattern">
      /// The search string to match against the names of directories in <paramref name="path"/>.
      /// </param>
      /// <param name="searchOption">
      /// One of the enumeration values  that specifies whether the search operation should include only the current
      /// directory or should include all subdirectories.The default value is
      /// <see cref="SearchOption.TopDirectoryOnly"/>.
      /// </param>
      /// <returns>
      /// An enumerable collection of the full names (including paths) for the directories in the directory specified by
      /// <paramref name="path"/> and that match the specified search pattern and option.
      /// </returns>
      IImmutableSet<String> EnumerateDirectories(String path, String searchPattern, SearchOption searchOption);

      /// <summary>
      /// Returns an enumerable collection of file names in a specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <returns>
      /// An enumerable collection of the full names (including paths) for the files in the directory specified by
      /// <paramref name="path"/>.
      /// </returns>
      IImmutableSet<String> EnumerateFiles(String path);

      /// <summary>
      /// Returns an enumerable collection of file names that match a search pattern in a specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// - or -
      /// <paramref name="searchPattern"/> does not contain a valid pattern.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.-or-<paramref name="searchPattern"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <param name="searchPattern">
      /// The search string to match against the names of directories in <paramref name="path"/>.
      /// </param>
      /// <returns>
      /// An enumerable collection of the full names (including paths) for the files in the directory specified by
      /// <paramref name="path"/> and that match the specified search pattern.
      /// </returns>
      IImmutableSet<String> EnumerateFiles(String path, String searchPattern);

      /// <summary>
      /// Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches
      /// subdirectories.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// - or -<paramref name="searchPattern"/> does not contain a valid pattern.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.-or-<paramref name="searchPattern"/> is null.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="searchOption"/> is not a valid <see cref="SearchOption"/>
      /// value.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <param name="searchPattern">
      /// The search string to match against the names of directories in <paramref name="path"/>.
      /// </param>
      /// <param name="searchOption">
      /// One of the enumeration values that specifies whether the search operation should include only the current
      /// directory or should include all subdirectories.The default value is
      /// <see cref="SearchOption.TopDirectoryOnly"/>.
      /// </param>
      /// <returns>
      /// An enumerable collection of the full names (including paths) for the files in the directory specified by
      /// <paramref  name="path"/> and that match the specified search pattern and option.
      /// </returns>
      IImmutableSet<String> EnumerateFiles(String path, String searchPattern, SearchOption searchOption);

      /// <summary>
      /// Returns an enumerable collection of file-system entries in a specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <returns>
      /// An enumerable collection of file-system entries in the directory specified by <paramref name="path"/>.
      /// </returns>
      IImmutableSet<String> EnumerateFileSystemEntries(String path);

      /// <summary>
      /// Returns an enumerable collection of file-system entries that match a search pattern in a specified path.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// - or -
      /// <paramref name="searchPattern"/> does not contain a valid pattern.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.-or-<paramref name="searchPattern"/> is null.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <param name="searchPattern">
      /// The search string to match against the names of directories in <paramref name="path"/>.
      /// </param>
      /// <returns>
      /// An enumerable collection of file-system entries in the directory specified by <paramref name="path"/> and that match the specified
      /// search pattern.
      /// </returns>
      IImmutableSet<String> EnumerateFileSystemEntries(String path, String searchPattern);

      /// <summary>
      /// Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally
      /// searches subdirectories.
      /// </summary>
      /// <exception cref="ArgumentException">
      /// <paramref name="path "/>is a zero-length string, contains only white space, or contains invalid
      /// characters as defined by
      /// <see cref="Path.GetInvalidPathChars"/>.
      /// - or -<paramref name="searchPattern"/> does not contain a valid pattern.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.-or-<paramref name="searchPattern"/> is null.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="searchOption"/> is not a valid <see cref="SearchOption"/>
      /// value.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// <paramref name="path"/> is invalid, such as referring to an unmapped drive.
      /// </exception>
      /// <exception cref="IOException">
      /// <paramref name="path"/> is a file name.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or combined exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <param name="path">
      /// The directory to search.
      /// </param>
      /// <param name="searchPattern">
      /// The search string to match against the names of directories in <paramref name="path"/>.
      /// </param>
      /// <param name="searchOption">
      /// One of the enumeration values  that specifies whether the search operation should include only the current
      /// directory or should include all subdirectories.The default value is
      /// <see cref="SearchOption.TopDirectoryOnly"/>.
      /// </param>
      /// <returns>
      /// An enumerable collection of file-system entries in the directory specified by <paramref name="path"/> and that match the specified
      /// search pattern and option.
      /// </returns>
      IImmutableSet<String> EnumerateFileSystemEntries(String path, String searchPattern, SearchOption searchOption);

      /// <summary>
      /// Gets the creation date and time of a directory.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="path"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <param name="path">
      /// The path of the directory.
      /// </param>
      /// <returns>
      /// A structure that is set to the creation date and time for the specified directory.
      /// </returns>
      DateTimeOffset GetCreationTime(String path);

      /// <summary>
      /// Gets the current working directory of the application.
      /// </summary>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="NotSupportedException">
      /// The operating system is Windows CE, which does not have current directory functionality. This
      /// method is available in the .NET Compact Framework, but is not currently supported.
      /// </exception>
      /// <returns>
      /// A string that contains the path of the current working directory, and does not end with a backslash (\).
      /// </returns>
      [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
      String GetCurrentDirectory();

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
      /// The <paramref name="path"/> parameter is in an invalid format.
      /// </exception>
      /// <param name="path">
      /// The file or directory for which to obtain access date and time information.
      /// </param>
      /// <returns>
      /// A structure that is set to the date and time the specified file or directory was last accessed.
      /// time.
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
      /// <param name="path">
      /// The file or directory for which to obtain modification date and time information.
      /// </param>
      /// <returns>
      /// A structure that is set to the date and time the specified file or directory was last written to.
      /// time.
      /// </returns>
      DateTimeOffset GetLastWriteTime(String path);

      /// <summary>
      /// Generates a a cryptographically strong, random string that can be used as a directory name.
      /// </summary>
      /// <returns>
      /// A random directory name.
      /// </returns>
      /// <remarks>
      /// Does not create a directory.
      /// </remarks>
      [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
      String GetRandomDirectoryName();

      /// <summary>
      /// Returns the path of the current user's temporary folder.
      /// </summary>
      /// <exception cref="SecurityException">
      /// The caller does not have the required permissions.
      /// </exception>
      /// <returns>
      /// The path to the temporary folder, ending with a backslash.
      /// </returns>
      [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
      String GetTemporaryDirectoryPath();

      /// <summary>
      /// Moves a directory and its contents to a new location.
      /// </summary>
      /// <param name="sourceDirName">
      /// The path of the directory to move.
      /// </param>
      /// <param name="destDirName">
      /// The path to the new location for <paramref name="sourceDirName"/>.
      /// </param>
      /// <exception cref="IOException">
      /// An attempt was made to move a directory to a different volume. 
      /// -or- 
      /// <paramref name="destDirName"/> already exists. 
      /// -or- 
      /// The <paramref name="sourceDirName"/> and <paramref name="destDirName"/> parameters refer to the same directory.
      /// </exception>
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permissions.
      /// </exception>
      /// <exception cref="ArgumentException">
      /// <paramref name="sourceDirName"/> or <paramref name="destDirName"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by
      /// <see cref="IPathUtilities.GetInvalidPathCharacters"/>.
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// <paramref name="sourceDirName"/> or <paramref name="destDirName"/> is null.
      /// </exception>
      /// <exception cref="PathTooLongException">
      /// The specified path, file name, or both exceed the system-defined maximum length.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The path specified by <paramref name="sourceDirName"/> is invalid (for example, it
      /// is on an unmapped drive).
      /// </exception>
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dir")]
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dest")]
      void Move(String sourceDirName, String destDirName);

      /// <summary>
      /// Sets the creation date and time for the specified file or directory.
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
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="creationTime"/> specifies a value outside the range of dates or times
      /// permitted for this operation.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The current operating system is not Windows NT or later.
      /// </exception>
      /// <param name="path">
      /// The file or directory for which to set the creation date and time information.
      /// </param>
      /// <param name="creationTime">
      /// An object that contains the value to set for the creation date and time of <paramref name="path"/>.
      /// </param>
      void SetCreationTime(String path, DateTimeOffset creationTime);

      /// <summary>
      /// Sets the application's current working directory to the specified directory.
      /// </summary>
      /// <exception cref="IOException">
      /// An I/O error occurred.
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
      /// <exception cref="SecurityException">
      /// The caller does not have the required permission to access unmanaged code.
      /// </exception>
      /// <exception cref="FileNotFoundException">
      /// The specified path was not found.
      /// </exception>
      /// <exception cref="DirectoryNotFoundException">
      /// The specified directory was not found.
      /// </exception>
      /// <param name="path">
      /// The path to which the current working directory is set.
      /// </param>
      void SetCurrentDirectory(String path);

      /// <summary>
      /// Sets the date and time the specified file or directory was last accessed.
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
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The current operating system is not Windows NT or later.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="lastAccessTime"/> specifies a value outside the range of dates or times
      /// permitted for this operation.
      /// </exception>
      /// <param name="path">
      /// The file or directory for which to set the access date and time information.
      /// </param>
      /// <param name="lastAccessTime">
      /// An object that contains the value to set for the access date and time of <paramref name="path"/>.
      /// </param>
      void SetLastAccessTime(String path, DateTimeOffset lastAccessTime);

      /// <summary>
      /// Sets the date and time a directory was last written to.
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
      /// <exception cref="UnauthorizedAccessException">
      /// The caller does not have the required permission.
      /// </exception>
      /// <exception cref="PlatformNotSupportedException">
      /// The current operating system is not Windows NT or later.
      /// </exception>
      /// <exception cref="ArgumentOutOfRangeException">
      /// <paramref name="lastWriteTime"/> specifies a value outside the range of dates or times
      /// permitted for this operation.
      /// </exception>
      /// <param name="path">
      /// The path of the directory.
      /// </param>
      /// <param name="lastWriteTime">
      /// The date and time the directory was last written to.
      /// </param>
      void SetLastWriteTime(String path, DateTimeOffset lastWriteTime);
   }
}
