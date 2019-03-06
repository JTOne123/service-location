﻿namespace Landorphan.Abstractions.Tests
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
   using FluentAssertions;
   using Landorphan.Abstractions.Interfaces;
   using Landorphan.Common;
   using Landorphan.Ioc.ServiceLocation;
   using Landorphan.TestUtilities;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // ReSharper disable InconsistentNaming

   [TestClass]
   public class When_I_call_EnvironmentUtilitiesFactory_Create : ArrangeActAssert
   {
      private readonly EnvironmentUtilitiesFactory target = new EnvironmentUtilitiesFactory();
      private IEnvironmentUtilities actual;

      protected override void ActMethod()
      {
         actual = target.Create();
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void TODO_REMOVE()
      {
         Trace.WriteLine($"TimeSpan.TicksPerSecond = {TimeSpan.TicksPerSecond}");
         Trace.WriteLine($"DateTimeOffset.MinValue.Ticks = {DateTimeOffset.MinValue.Ticks}");
         Trace.WriteLine($"DateTimeOffset.MaxValue.Ticks = {DateTimeOffset.MaxValue.Ticks}");
         Trace.WriteLine($"Windows Epoch = {new DateTime(504_911_232_000_000_001, DateTimeKind.Utc)}");
         Trace.WriteLine($"Windows Epoch Ticks = {new DateTime(504_911_232_000_000_001, DateTimeKind.Utc).Ticks}");
         Trace.WriteLine($"Guessed Linux Epoch = {new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)}");
         Trace.WriteLine($"Guessed Linux Epoch Ticks = {new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks}");
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_create_an_IEnvironment_instance()
      {
         actual.Should().BeAssignableTo<IEnvironmentUtilities>();
      }
   }

   [TestClass]
   public class When_I_call_EnvironmentUtilitiesFactory_Create_multiple_times : ArrangeActAssert
   {
      private readonly EnvironmentUtilitiesFactory target = new EnvironmentUtilitiesFactory();
      private HashSet<IEnvironmentUtilities> actuals;

      protected override void ArrangeMethod()
      {
         actuals = new HashSet<IEnvironmentUtilities>(new ReferenceEqualityComparer<IEnvironmentUtilities>());
      }

      protected override void ActMethod()
      {
         actuals.Add(target.Create());
         actuals.Add(target.Create());
         actuals.Add(target.Create());
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "anew")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_return_a_new_instance_each_time()
      {
         actuals.Count.Should().Be(3);
      }
   }

   [TestClass]
   public class When_I_service_locate_IEnvironmentUtilitiesFactory : ArrangeActAssert
   {
      private IEnvironmentUtilitiesFactory actual;

      protected override void ActMethod()
      {
         actual = IocServiceLocator.Resolve<IEnvironmentUtilitiesFactory>();
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "mean")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_give_me_a_EnvironmentUtilitiesFactory()
      {
         actual.Should().BeOfType<EnvironmentUtilitiesFactory>();
      }
   }
}
