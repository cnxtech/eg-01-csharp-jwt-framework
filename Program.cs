using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;

namespace eg_01_csharp_jwt
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiClient = new ApiClient();

            EnvelopeSummary result = new SendEnvelope(apiClient).Send();
            Console.WriteLine("Envelope status: {0}. Envelope ID: {1}", result.Status, result.EnvelopeId);

            Console.WriteLine("\nList envelopes in the account...");
            EnvelopesInformation envelopesList = new ListEnvelopes(apiClient).List();

            List<Envelope> envelopes = envelopesList.Envelopes;

            if (envelopesList != null && envelopes.Count > 2)
            {
                //Console.WriteLine("Results for {0} envelopes were returned. Showing the first two: ", envelopes.Count);
                envelopesList.Envelopes = new List<Envelope>()
                {
                    envelopes[0],
                    envelopes[1]
                };
            }

            DSHelper.PrintPrettyJSON(envelopesList);

            Console.WriteLine("Done. Hit enter to exit...");
            Console.ReadKey();
        }
    }
}
