﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IdentityModel.Selectors;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace IdSrv.IdSrvCfg
{
    static class Certificate
    {
        public static X509Certificate2 Get()
        {

            //var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.ReadOnly);

            //var cert = store.Certificates.Find(X509FindType.FindBySubjectName, "xx", false)[0]; //false for searching also for invalid certs; usually would go for true

            ////validating certs
            //var chain = new X509Chain();
            //var policy = new X509ChainPolicy()
            //{
            //    RevocationFlag = X509RevocationFlag.EntireChain,
            //    VerificationTime = DateTime.Today,
            //    RevocationMode = X509RevocationMode.Online
            //};

            //chain.ChainPolicy = policy;

            ////validating certs!
            //if (!chain.Build(cert))
            //{
            //    foreach (var element in chain.ChainElements)
            //    {
            //        foreach (var status in element.ChainElementStatus)
            //        {
            //            Console.WriteLine(status.StatusInformation);
            //        }
            //    }
            //}

            //var validator = X509CertificateValidator.ChainTrust;
            //validator.Validate(cert); //this will throw if not valid of course

            //store.Close();

            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Certs\idsrv3test.pfx"), "idsrv3test");

            //TODO - load certificate from store. Having a signing certificate here is not a good idea; certicate should be installed to the windows certificate store and loaded from there.

            //X509Store store = new X509Store(StoreLocation.CurrentUser);
            //X509Certificate2 cer;
            //X509Certificate2Collection cers = store.Certificates.Find(X509FindType.FindBySubjectName, "My Cert's Subject Name", false);
            //if (cers.Count > 0)
            //{
            //    cer = cers[0];
            //};

            //var assembly = typeof(Certificate).Assembly;
            //using (var stream = assembly.GetManifestResourceStream("WebHost.IdSvr.idsrv3test.pfx"))
            //{
            //    return new X509Certificate2(ReadStream(stream), "idsrv3test");
            //}
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}