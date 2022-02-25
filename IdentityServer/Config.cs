using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId()
            };

        /* ESCOPOS
         * São adicionados aos token de acesso e são usados pelas APIs para saber se o cliente possui acesso a tal
         * funcionalidade através dos escopos que ele possui
         * 
         * Aqui vamos declarar todos os escopos que possiveis cliente EVENTUALMENTE podem ter
         */
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                { 
                    new ApiScope(name: "api1", displayName: "MyAPI")
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    new Client
                    {
                        ClientId = "client",
                        // Tipo de fluxo que será usado para autenticar esse cliente (maquina X maquina)
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        // Secret que esse cliente vai ter que usar para conseguir o token de acesso
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        // Escopos que esse cliente tem acesso
                        AllowedScopes = { "api" }
                        
                    }
                };
    }
}