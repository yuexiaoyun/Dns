﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dns.Test.DnsNameTests
{
    [TestFixture]
    public sealed class Constructor
    {
        private readonly static byte[] ultraasp_dot_net_A_Record = new byte[] { 0, 0, 129, 128, 0, 1, 0, 1, 0, 0, 0, 0, 8, 117, 108, 116, 114, 97, 97, 115, 112, 3, 110, 101, 116, 0, 0, 1, 0, 1, 192, 12, 0, 1, 0, 1, 0, 0, 111, 69, 0, 4, 91, 209, 187, 19 };
        private readonly static byte[] test_dot_cocktail_dot_local_A_Record = new byte[] { 0, 0, 133, 128, 0, 1, 0, 2, 0, 0, 0, 0, 4, 116, 101, 115, 116, 8, 99, 111, 99, 107, 116, 97, 105, 108, 5, 108, 111, 99, 97, 108, 0, 0, 1, 0, 1, 192, 12, 0, 5, 0, 1, 0, 0, 14, 16, 0, 9, 6, 118, 111, 105, 112, 45, 49, 192, 17, 192, 49, 0, 1, 0, 1, 0, 0, 14, 16, 0, 4, 10, 1, 1, 231 };

        [Test]
        public void Sets_Host_Property_Correctly()
        {
            var result = new DnsName("test.local");

            Assert.That(result.Host, Is.EqualTo("test.local"));
        }

        [Test]
        public void Extracts_Name_Correctly_From_Well_Formed_Question_In_Dns_Response()
        {
            var parser = new DnsNameParser(ultraasp_dot_net_A_Record);
            var result = new DnsName(parser, ultraasp_dot_net_A_Record , 12);
            Assert.That(result.Host, Is.EqualTo("ultraasp.net"));
        }

        [Test]
        public void Extract_Name_Correctly_From_Well_Formed_Question_In_Dns_Response()
        {
            var parser = new DnsNameParser(test_dot_cocktail_dot_local_A_Record);
            var result = new DnsName(parser ,test_dot_cocktail_dot_local_A_Record, 12);
            Assert.That(result.Host, Is.EqualTo("test.cocktail.local"));
        }

        /// <summary>
        /// Scenario: A name is extracted from an answer, where part of the name is obtained by "look back" to a previous name in the packet
        /// Expected: The name is extracted fully and correctly
        /// </summary>
        [Test]
        public void Extract_Name_Correctly_From_Well_Formed_Answer_In_Dns_Response_That_Involves_Lookback()
        {
            var parser = new DnsNameParser(test_dot_cocktail_dot_local_A_Record);
            var result = new DnsName(parser, test_dot_cocktail_dot_local_A_Record, 49);
            Assert.That(result.Host, Is.EqualTo("voip-1.cocktail.local"));
        }
    }
}
