using Canopy.Provider.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Canopy.Provider.Extensions
{
    internal static class AccountExtensions
    {
        public static string AuthenticatedEndpoint(this Account acct, string apiEndPoint)
        {
            var isoDateTime = DateTime.Now.ToString("O");
            var signature = GetSignature(acct, isoDateTime);

            return QueryHelpers.AddQueryString(apiEndPoint, new Dictionary<string, string>() {
                {"signature", signature },
                {"account", acct.UUID },
                {"timestamp", isoDateTime },
            });
        }

        private static string GetSignature(Account acct, string isoDateTime)
        {
            var signature = Encoding.UTF8.GetBytes($"{acct.UUID}{isoDateTime}");
            var key = Encoding.UTF8.GetBytes(acct.HMACSHA256_APIKey);

            var shaAlgorithm = new HMACSHA256(key);
            var signatureHashBytes = shaAlgorithm.ComputeHash(signature);

            return Convert.ToBase64String(signatureHashBytes);
        }
    }
}
