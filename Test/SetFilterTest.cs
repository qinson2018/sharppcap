using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpPcap;

namespace Test
{
    [TestFixture]
    public class SetFilterTest
    {
        [Test]
        public void SimpleFilter()
        {
            var devices = PcapDeviceList.Instance;
            if(devices.Count == 0)
            {
                throw new System.InvalidOperationException("No pcap supported devices found, are you running" +
                                                           " as a user with access to adapters (root on Linux)?");
            }

            devices[0].Open();
            devices[0].SetFilter("tcp port 80");
            devices[0].Close(); // close the device
        }

        /// <summary>
        /// Test that we get the expected exception if PcapDevice.SetFilter()
        /// is called on a PcapDevice that has not been opened
        /// </summary>
        [Test]
        public void SetFilterExceptionIfDeviceIsClosed()
        {
            var devices = PcapDeviceList.Instance;
            if(devices.Count == 0)
            {
                throw new System.InvalidOperationException("No pcap supported devices found, are you running" +
                                                           " as a user with access to adapters (root on Linux)?");
            }

            bool caughtExpectedException = false;
            try
            {
                devices[0].SetFilter("tcp port 80");
            } catch(DeviceNotReadyException)
            {
                caughtExpectedException = true;
            }

            Assert.IsTrue(caughtExpectedException, "Did not catch the expected PcapDeviceNotReadyException");
        }
    }
}
