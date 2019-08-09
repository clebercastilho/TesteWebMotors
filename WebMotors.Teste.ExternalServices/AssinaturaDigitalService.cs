using Amil.Atendimentos.ExternalServices.Configuration;
using Amil.Atendimentos.ExternalServices.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Amil.Atendimentos.ExternalServices
{
    public class AssinaturaDigitalService
    {
        public string EvalDigitalSignEndpoint { get; set; }

        public TResult Verificar<TResult>(DigitalSign.RequestDocument assinaturaValidacao, Func<DigitalSign.ResponseMessage, TResult> convertResponseFunc)
        {
            try
            {
                var bodyRequest = new DigitalSign.VerifyRequestBody("Eval", "VerifyBasicData", "Verify", 
                    new List<DigitalSign.RequestDocument> { assinaturaValidacao });

                var client = ServiceConnection();
                var request = new DigitalSign.VerifyRequest(bodyRequest);

                var response = client.VerifyAsync(request).Result;
                return convertResponseFunc(response.Body.VerifyResult);
            }
            catch(Exception ex)
            {
                throw new DigitalSignException("Ocorreu um problema ao analisar a assinatura do atendimento solicitado.", ex);
            }
        }

        private DigitalSign.WSCryptoServerSoapClient ServiceConnection()
        {
            if (string.IsNullOrEmpty(EvalDigitalSignEndpoint))
                throw new DigitalSignException("URL do serviço EVAL inválido.");

            return new DigitalSign.WSCryptoServerSoapClient(
                DigitalSign.WSCryptoServerSoapClient.EndpointConfiguration.WSCryptoServerSoap,
                EvalDigitalSignEndpoint);
        }
    }
}
