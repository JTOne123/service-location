﻿namespace Landorphan.Abstractions.Tests.IO
{
   using System;
   using System.IO;
   using FluentAssertions;
   using Landorphan.Abstractions.Interfaces;
   using Landorphan.Abstractions.IO;
   using Landorphan.Abstractions.IO.Interfaces;
   using Landorphan.Ioc.ServiceLocation;
   using Landorphan.TestUtilities;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // ReSharper disable InconsistentNaming

   public static class FileUtilities_Tests
   {
      [TestClass]
      public class Given_I_have_a_FileUtilities : TestBase
      {
         // b/c this is such a thin wrapper over tested implementation, negative testing is not implemented.

         private static readonly IEnvironmentUtilities _environmentUtilities = IocServiceLocator.Resolve<IEnvironmentUtilities>();
         private static readonly IPathUtilities _pathUtilities = IocServiceLocator.Resolve<IPathUtilities>();
         private static readonly IFileUtilities _target = IocServiceLocator.Resolve<IFileUtilities>();
         private static readonly String _tempPath = _environmentUtilities.GetTemporaryDirectoryPath();

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_CreateFile_It_should_create_the_file()
         {
            var path = _pathUtilities.Combine(_pathUtilities.GetFullPath(_tempPath), Guid.NewGuid() + "When_I_call_FileUtilities_CreateFile.tmp");
            try
            {
               _target.FileExists(path).Should().BeFalse();
               var actual = _target.CreateFile(path);

               _target.FileExists(actual).Should().BeTrue();
               _target.FileExists(path).Should().BeTrue();
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_CreateTemporaryFile_It_should_create_a_temporary_file()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               _target.FileExists(path).Should().BeTrue();
               _pathUtilities.GetParentPath(path).ToUpperInvariant().Should().Be(_pathUtilities.GetFullPath(_tempPath).ToUpperInvariant());
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_DeleteFile_It_should_delete_the_file()
         {
            var path = _target.CreateTemporaryFile();
            _target.FileExists(path).Should().BeTrue();
            _target.DeleteFile(path);
            _target.FileExists(path).Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_FileExists_It_should_distinguish_between_extant_and_non_extant_files()
         {
            var extant = _target.CreateTemporaryFile();
            var nonExtant = _pathUtilities.Combine(_tempPath, Guid.NewGuid() + ".tmp");
            try
            {
               _target.FileExists(extant).Should().BeTrue();
               _target.FileExists(nonExtant).Should().BeFalse();
            }
            finally
            {
               _target.DeleteFile(extant);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_GetAttributes_It_should_get_the_attributes()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               var actual = _target.GetAttributes(path);
               actual.Should().NotBeNull();
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_GetCreationTime_It_should_get_the_creation_time()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               var actual = _target.GetCreationTime(path);
               actual.Should().BeOnOrBefore(DateTimeOffset.UtcNow);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_GetLastAccessTime_It_should_get_the_last_access_time()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               var actual = _target.GetLastAccessTime(path);
               actual.Should().BeOnOrBefore(DateTimeOffset.UtcNow);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_GetLastWriteTime_It_should_get_the_last_write_time()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               var actual = _target.GetLastWriteTime(path);
               actual.Should().BeOnOrBefore(DateTimeOffset.UtcNow);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_GetRandomFileName_It_should_return_the_file_name_that_is_not_rooted_and_does_not_exist()
         {
            var actual = _target.GetRandomFileName();
            _target.FileExists(actual).Should().BeFalse();
            _pathUtilities.IsPathRooted(actual).Should().BeFalse();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_SetAttributes_It_should_set_the_attributes()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               var fileAttributes = FileAttributes.Archive | FileAttributes.Hidden;
               _target.SetAttributes(path, fileAttributes);
               _target.GetAttributes(path).Should().Be(fileAttributes);

               fileAttributes = FileAttributes.Normal;
               _target.SetAttributes(path, fileAttributes);
               _target.GetAttributes(path).Should().Be(fileAttributes);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_SetCreationTime_It_should_set_the_creation_time()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               _target.SetCreationTime(path, _target.MinimumFileTimeAsDateTimeOffset);
               _target.GetCreationTime(path).Should().Be(_target.MinimumFileTimeAsDateTimeOffset);

               var expected = DateTimeOffset.UtcNow;
               _target.SetCreationTime(path, expected);
               _target.GetCreationTime(path).Should().Be(expected);

               _target.SetCreationTime(path, _target.MaximumFileTimeAsDateTimeOffset);
               _target.GetCreationTime(path).Should().Be(_target.MaximumFileTimeAsDateTimeOffset);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_SetLastAccessTime_It_should_set_the_last_access_time()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               _target.SetLastAccessTime(path, _target.MinimumFileTimeAsDateTimeOffset);
               _target.GetLastAccessTime(path).Should().Be(_target.MinimumFileTimeAsDateTimeOffset);

               var expected = DateTimeOffset.UtcNow;
               _target.SetLastAccessTime(path, expected);
               _target.GetLastAccessTime(path).Should().Be(expected);

               _target.SetLastAccessTime(path, _target.MaximumFileTimeAsDateTimeOffset);
               _target.GetLastAccessTime(path).Should().Be(_target.MaximumFileTimeAsDateTimeOffset);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void When_I_call_FileUtilities_SetLastWriteTime_It_should_set_the_last_write_time()
         {
            var path = _target.CreateTemporaryFile();
            try
            {
               _target.SetLastWriteTime(path, _target.MinimumFileTimeAsDateTimeOffset);
               _target.GetLastWriteTime(path).Should().Be(_target.MinimumFileTimeAsDateTimeOffset);

               var expected = DateTimeOffset.UtcNow;
               _target.SetLastWriteTime(path, expected);
               _target.GetLastWriteTime(path).Should().Be(expected);

               _target.SetLastWriteTime(path, _target.MaximumFileTimeAsDateTimeOffset);
               _target.GetLastWriteTime(path).Should().Be(_target.MaximumFileTimeAsDateTimeOffset);
            }
            finally
            {
               _target.DeleteFile(path);
            }
         }
      }

      [TestClass]
      public class When_I_service_locate_IFileUtilities : ArrangeActAssert
      {
         private IFileUtilities actual;

         protected override void ActMethod()
         {
            actual = IocServiceLocator.Resolve<IFileUtilities>();
         }

         [TestMethod]
         [TestCategory(TestTiming.CheckIn)]
         public void It_should_give_me_an_FileUtilities()
         {
            actual.Should().BeOfType<FileUtilities>();
         }
      }
   }
}